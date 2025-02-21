using System.ComponentModel.DataAnnotations;

namespace BlazorAppEnquiry.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; } = Roles.User;
    }

    public enum Roles
    {
        User,
        Admin
    }
}
