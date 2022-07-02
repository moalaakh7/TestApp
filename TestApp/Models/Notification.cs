using TestApp.Models.Enum;

namespace TestApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public NotificationType NotificationType { get; set; }

        public List<User> Users { get; set; }
    }
}