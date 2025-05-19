using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Data.Classes
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
       
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
