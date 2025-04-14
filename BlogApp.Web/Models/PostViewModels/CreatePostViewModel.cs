namespace BlogApp.Web.Models.PostViewModels
{
    public class CreatePostViewModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; } 
        public int CategoryId { get; set; }
    }
}
