using System.Text.Json.Serialization;

namespace BlogApp.Web.Entities
{
    public class Comment
    {
        public int CommentId { get; set; } 
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; } 
    }
}
