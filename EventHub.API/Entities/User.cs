using EventHub.API.Entities.Enums;

namespace EventHub.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role UserRole { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public User()
        {
        }

        public User(string name, string email, Role userRole)
        {
            Name = name;
            Email = email;
            UserRole = userRole;
        }


    }
}
