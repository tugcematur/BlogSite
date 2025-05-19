using BlogApp.Models.Data.Classes;

namespace BlogApp.Models.ViewModels
{
    public class CommentModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }

        public int CommentId { get; set; }             
    }
}
