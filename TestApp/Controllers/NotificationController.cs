using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Models.DBManager;
using TestApp.Utils;

namespace TestApp.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public NotificationController(ILogger<HomeController> logger, Context context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _logger = logger;
            this.context = context;
            hostingEnvironment = environment;
        }

        public IActionResult CreateNotification()
        {
            return View(context.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(Notification notification , IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(notification.Title) ||
                string.IsNullOrWhiteSpace(notification.Content) ||
                file is null)
                return Problem("wrong data");

            notification.Image = await FileManager.UploadFile(hostingEnvironment.WebRootPath, Guid.NewGuid().ToString(), file);
            notification.UserId = notification.UserId == 0 ? null : notification.UserId;
            context.Notifications.Add(notification);
            context.SaveChanges();


            if (notification.UserId == 0)
                foreach (var token in context.Users.Select(x => x.Token))
                    FCM.SendNotification(token, notification.Title, notification.Content);
            else
                FCM.SendNotification(context.Users.First(x => x.Id == notification.UserId).Token
                    , notification.Title, notification.Content);

            return View(context.Users.ToList());
        }

        public IQueryable<Notification> GetNotifications(int userId)
        {
            if (userId == 0)
                return context.Notifications;
            else
                return context.Notifications.Where(x => x.UserId == userId);

        }

        public IActionResult SelectUser()
        {
            return View(context.Users.ToList());
        }

    }
}
