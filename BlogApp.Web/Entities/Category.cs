namespace BlogApp.Web.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? Url { get; set; }

        public List<Post>? Posts { get; set; } 
    }
}
