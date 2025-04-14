using Microsoft.AspNetCore.Identity;

namespace BlogApp.Web.Entities
{
    // Kullanıcı (Üye) Sınıfı
    public class AppUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Comment>? Comments { get; set; } 
    }
}
