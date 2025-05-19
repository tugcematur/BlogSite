using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models.Data.Classes
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string CommentText { get; set; }

        [ForeignKey("PostId")]
        public Posts Post { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

    }
}
