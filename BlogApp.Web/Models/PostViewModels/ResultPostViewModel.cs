namespace BlogApp.Web.Models.PostViewModels
{
    public class ResultPostViewModel
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        //public Guid AppUserId { get; set; }
    }
}
