namespace BlogApp.Models.DTO
{
    public class CommentDTO
    {
        public string CommentText { get; set; }
        public string Username { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}
