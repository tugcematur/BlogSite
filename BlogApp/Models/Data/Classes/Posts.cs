using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models.Data.Classes
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }

        [ForeignKey("UserId")]
        public  Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
