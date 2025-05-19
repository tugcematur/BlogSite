using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Data.Classes
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }

        public virtual  ICollection<Posts> Posts { get; set; }
        public virtual  ICollection<Comments> Comments {  get; set; }
    }
}
