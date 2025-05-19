namespace BlogApp.Models.DTO
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username  { get; set; }
        public string CategoryName { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
