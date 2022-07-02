using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Models.DBManager;

namespace TestApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context context;

        public UserController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        public string CreateUser(User user)
        {
            if (user.Id == 0 || string.IsNullOrWhiteSpace(user.Token))
                return "Please enter all information";

            if (context.Users.Any(x => x.Id == user.Id || x.Token == user.Token))
                return "The data is already entered";

            context.Users.Add(new User
            {
                Id = user.Id,
                Token = user.Token
            });
            context.SaveChanges();
            return "User entered successfully";
        }
    }
}
