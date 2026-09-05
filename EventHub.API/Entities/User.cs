using EventHub.API.Entities.Enums;

namespace EventHub.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role UserRole { get; set; }

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
