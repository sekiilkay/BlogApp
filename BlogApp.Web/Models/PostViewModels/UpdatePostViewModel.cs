namespace BlogApp.Web.Models.PostViewModels
{
    public class UpdatePostViewModel
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public Guid AppUserId { get; set; }
    }
}
