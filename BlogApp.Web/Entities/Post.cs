namespace BlogApp.Web.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; } 
        public List<Comment>? Comments { get; set; } 
    }
}
