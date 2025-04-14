namespace BlogApp.Web.Models.CommentViewModels
{
    public class CreateCommentViewModel
    {
        public string Text { get; set; } 
        public int PostId { get; set; }
        public string UserName { get; set; } 
        public string Avatar { get; set; } 
        public DateTime PublishedDate { get; set; }
    }
}
