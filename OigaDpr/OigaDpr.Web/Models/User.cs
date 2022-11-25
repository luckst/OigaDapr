using System.ComponentModel.DataAnnotations;

namespace OigaDpr.Web.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
