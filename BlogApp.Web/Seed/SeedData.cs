using Azure;
using BlogApp.Web.Context;
using BlogApp.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Seed
{
    public static class SeedData
    {
        public static async Task AddDataAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser { Name = "Emin İlkay", Surname = "Şeki", UserName = "sekiilkay", Email = "sekiilkay@gmail.com", ImageUrl = "user.jpg" },
                    new AppUser { Name = "Doğuş Teknoloji", UserName = "dogusteknoloji", Email = "  ", ImageUrl = "dogus-teknoloji.jpg" },
                    new AppUser { UserName = "demouser", Email = "demouser@gmail.com", ImageUrl = "anonim.png"  },
                    new AppUser { Name = "Deneme", Surname = "Deneme", UserName = "deneme", Email = "deneme@gmail.com", ImageUrl = "anonim.png" }
                };

                /* Kullanıcılara Şifre Ata (Identity Kütüphanesi) */
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "YourSecurePassword123!");
                }
            }
        
            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(
                    new Category { Name = "Web Geliştirme", Url = "web-gelistirme" },
                    new Category { Name = "Mobil Uygulama Geliştirme", Url = "mobil-gelistirme" },
                    new Category { Name = "Full Stack Geliştirme", Url = "full-stack-development" },
                    new Category { Name = "Frontend Geliştirme", Url = "frontend-teknolojileri" },
                    new Category { Name = "Backend Geliştirme", Url = "backend-teknolojileri" },
                    new Category { Name = "Veritabanı Yönetimi", Url = "veritabani-yonetimi" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Posts.Any())
            {
                var user1 = await userManager.FindByNameAsync("sekiilkay");
                var user2 = await userManager.FindByNameAsync("dogusteknoloji");
                var user3 = await userManager.FindByNameAsync("demouser");
                var user4 = await userManager.FindByNameAsync("deneme");

                await context.Posts.AddRangeAsync(
                    new Post
                    {
                        Title = "Web Geliştirme Temelleri",
                        Description = "Web geliştirme sürecine başlarken HTML, CSS ve JavaScript'in temel rollerini anlamak büyük önem taşır. Bu üç teknoloji, web sayfalarının görsel düzeni ve etkileşimli işlevselliğini sağlar.",
                        Content = "Web geliştirme, üç temel teknolojiye dayanır: HTML, CSS ve JavaScript. HTML, web sayfalarının temel yapısını oluşturan etiketler sağlar. Sayfanın başlıkları, paragrafları, bağlantıları ve görselleri HTML etiketleriyle tanımlanır ve bu sayede web sayfasının iskeleti oluşturulur. CSS ise, HTML yapısına stil ekleyerek sayfanın görsel tasarımını belirler. Renkler, yazı tipleri, boyutlar, düzen ve animasyonlar gibi stil özellikleri CSS ile yönetilir. Responsive tasarım prensipleri sayesinde sayfanın farklı cihazlarda düzgün görüntülenmesi sağlanır. JavaScript ise sayfalara etkileşim ve dinamiklik katar. Kullanıcı etkileşimine bağlı olarak sayfa içeriği değişebilir, formlar doğrulanabilir ve dinamik öğeler güncellenebilir. HTML ve CSS ile birleştirilen JavaScript, web sayfalarını daha kullanıcı dostu ve işlevsel hale getirir.",
                        Image = "web-gelistirme-temelleri.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "web-gelistirme-temelleri",
                        IsActive = true,
                        CategoryId = 1,
                        AppUserId = user1!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "HTML, CSS ve JavaScript'in temel farklarını uzatmadan anlatan güzel bir yazı!", PublishedDate = DateTime.Now, AppUserId = user3!.Id },
                            new Comment { Text = "Yeni başlayanlar için harika bir tanımlama olmuş, temel bilgiler çok anlaşılır şekilde açıklanmış.", PublishedDate = DateTime.Now, AppUserId = user4!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "HTML5 ile Modern Sayfa Yapıları",
                        Description = "HTML5, modern web geliştirme sürecinde önemli bir rol oynar. Semantik etiketler sayesinde web sayfalarınız daha anlamlı hale gelir ve erişilebilirlik açısından büyük avantaj sağlar.",
                        Content = @"HTML5, web sayfalarının yapısını daha anlaşılır ve düzenli hale getirmek için güçlü semantik etiketler sunar. Bu etiketler, sayfanın bölümlerini net bir şekilde tanımlar ve arama motorları ile ekran okuyucular için sayfanın içeriğini erişilebilir kılar. <header>, <section>, <article>, <footer> gibi etiketler, içeriklerin doğru bir şekilde gruplanmasına yardımcı olur ve hem geliştiricilere hem de kullanıcılara daha iyi bir deneyim sunar. Semantik etiketlerin kullanımı, sadece erişilebilirlik açısından değil, aynı zamanda sayfanın SEO (arama motoru optimizasyonu) performansı için de oldukça faydalıdır. Bu etiketler sayesinde arama motorları, sayfanın yapısını daha iyi anlayarak içerikleri daha etkili bir şekilde dizineleyebilir. Ayrıca, semantik etiketler sayesinde web sayfalarınız daha hızlı yüklenebilir ve bu da kullanıcı deneyimini iyileştirir. Bu yazıda, HTML5’in sunduğu semantik gücü kullanarak daha anlamlı ve modern web sayfaları oluşturmayı öğrenecek ve temel web geliştirme sürecinize önemli bir katkı yapabileceksiniz.",
                        Image = "html5-modern-sayfa-yapilari.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "html5-modern-sayfa-yapilari",
                        IsActive = true,
                        CategoryId = 4,
                        AppUserId = user1!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "HTML5 ile ilgili faydalı bilgiler.", PublishedDate = DateTime.Now.AddDays(1), AppUserId = user3!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "JavaScript ile Etkileşimli Web Sayfaları",
                        Description = "JavaScript, etkileşimli web sayfaları oluşturmanın temelini atar. Event yönetimiyle kullanıcı etkileşimini kolayca yönetebilir ve web sayfalarına dinamik özellikler kazandırabilirsiniz.",
                        Content = "JavaScript, web sayfalarına etkileşim katmak için en önemli araçlardan biridir. Web sayfaları, sadece statik içeriklerle sınırlı kalmayıp, dinamik ve interaktif hale gelerek kullanıcı deneyimini iyileştirebilir. JavaScript'in en güçlü özelliklerinden biri, olay (event) yönetimi sistemidir. Kullanıcı etkileşimleri, örneğin bir butona tıklama, bir alana odaklanma ya da sayfa yükleme gibi durumlar JavaScript event'leri sayesinde yönetilir. Bu olaylar, kullanıcı etkileşimini algılayarak belirli işlevlerin tetiklenmesini sağlar. Örneğin, addEventListener fonksiyonu, kullanıcı tarafından gerçekleştirilen herhangi bir etkileşimi dinleyip, buna tepki verirken; onclick ve onchange gibi eventler de daha spesifik etkileşimleri işlemek için kullanılır. JavaScript ile etkileşimli bir web sayfası oluşturmak, sayfanın dinamikliğini artırır ve kullanıcılara daha iyi bir deneyim sunar. Bu yazıda, JavaScript'teki temel event yönetimi yöntemlerini keşfederek, web sayfalarınıza nasıl etkileşim ekleyeceğinizi öğreneceksiniz. Event yönetimiyle sayfanızın kullanıcı etkileşimini artırarak, dinamik web uygulamaları geliştirebilirsiniz.",
                        Image = "js-etkilesimli-sayfalar.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "javascript-etkilesimli-sayfalar",
                        IsActive = true,
                        CategoryId = 4,
                        AppUserId = user3!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "Event konusunu bu kadar net ve sade anlatmanız çok faydalı olmuş.", PublishedDate = DateTime.Now, AppUserId = user2!.Id },
                            new Comment { Text = "JavaScript'in etkileşim gücünü öğrenmek isteyenler için harika bir içerik!", PublishedDate = DateTime.Now, AppUserId = user4!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "React ile Bileşen Tabanlı Geliştirme",
                        Description = "React, esnek yapısı ve yeniden kullanılabilir bileşenler sunarak modern web uygulamaları geliştirmeyi kolaylaştırır. Bu yazı, bileşen tabanlı geliştirme anlayışını öğretir.",
                        Content = "React, kullanıcı arayüzlerini hızlı ve etkili bir şekilde oluşturmak için popüler bir JavaScript kütüphanesidir. React’in temel yapı taşı bileşenlerdir (components), ve bu bileşenler uygulamanın her parçasını bağımsız olarak yönetir. Bileşenler, props ve state gibi özelliklerle veri alıp verebilir, kullanıcı etkileşimlerine göre dinamik hale gelir. Bileşenler arası veri aktarımı, React’in en önemli özelliklerinden biridir. props kullanarak bir bileşenden diğerine veri geçirebilirken, state ile bileşen içinde dinamik verileri yönetebilirsiniz. Ayrıca React, yaşam döngüsü metodları ile bileşenlerin farklı aşamalarında işlem yapmanıza imkan tanır. SPA (Single Page Application) yapısına geçişte ise React Router kullanılır. Bu sayede, uygulamanın tüm sayfaları tek bir sayfada yüklenir ve sayfalar arası geçiş hızla yapılır. Bu yazıda, React’in temel özelliklerini ve gelişmiş kullanım yöntemlerini öğrenerek modern web projeleriniz için sürdürülebilir ve ölçeklenebilir yapılar oluşturabileceksiniz.",
                        Image = "react-bilesen-tabanli-gelistirme.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "react-bilesen-tabani-gelistirme",
                        IsActive = true,
                        CategoryId = 4,
                        AppUserId = user4!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "React bileşenlerinin nasıl çalıştığını bu yazı sayesinde çok daha iyi anladım, özellikle props ve state açıklamaları çok netti.", PublishedDate = DateTime.Now, AppUserId = user3!.Id },
                            new Comment { Text = "SPA mimarisine geçiş konusuna değinmeniz harika olmuş", PublishedDate = DateTime.Now.AddMinutes(30), AppUserId = user2!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "Tailwind CSS ile Modern Arayüz Tasarımı",
                        Description = "Tailwind CSS, sınıf tabanlı yaklaşımıyla hızlı ve esnek web tasarımları oluşturmanıza olanak tanır. Yardımcı sınıflar kullanarak, daha sade ve okunabilir yapılar elde edebilirsiniz.",
                        Content = "Tailwind CSS, utility-first (yardımcı sınıf öncelikli) bir CSS framework'üdür ve modern web tasarımında verimliliği artırır. Bu yaklaşımda, her bileşen için özel bir CSS sınıfı yazmak yerine, doğrudan HTML elemanlarına uygulanan yardımcı sınıflar sayesinde daha sade ve okunabilir tasarımlar elde edilir. Bu yöntem, özellikle büyük projelerde CSS yönetimini kolaylaştırır. Tailwind'in sunduğu responsive tasarım özellikleri ile, farklı ekran boyutlarına uyumlu sayfalar tasarlamak oldukça basittir. Mobil öncelikli yapısıyla, ekran boyutlarına göre düzenlemeler yapmak için sadece birkaç sınıf eklemeniz yeterlidir. Bu özellik, özellikle mobil cihazlar için optimize edilmiş arayüzler geliştirmede kullanışlıdır. Ayrıca, temel grid yapıları, renk skalası, margin/padding ayarları gibi konularda da Tailwind'in sunduğu hazır sınıfları kullanarak, zaman kaybı yaşamadan şık ve fonksiyonel tasarımlar oluşturabilirsiniz. Bu yazıda, bu özelliklerin nasıl kullanılacağına dair örneklerle gerçek projelerde nasıl uygulanabileceğinizi keşfedeceksiniz.",
                        Image = "tailwind-css.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "tailwind-css-arayuz-tasarimi",
                        IsActive = true,
                        CategoryId = 4,
                        AppUserId = user4!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "Tailwind ile çalışmaya yeni başlayanlar için çok açıklayıcı bir içerik olmuş.", PublishedDate = DateTime.Now.AddDays(1), AppUserId = user2!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "ASP.NET Core ile RESTful API Geliştirme",
                        Description = "ASP.NET Core, güçlü ve esnek bir framework olup, RESTful API geliştirmek için ideal bir platform sunar. Bu yazı, veri yönetimini API controller yapısı ve Entity Framework Core ile nasıl verimli şekilde gerçekleştirebileceğinizi anlatır.",
                        Content = "ASP.NET Core, açık kaynaklı ve performans odaklı bir framework’tür, modern web uygulamalarında sıklıkla tercih edilir. RESTful API geliştirmek, web ve mobil istemciler arasında veri alışverişi sağlamak için kritik bir rol oynar. ASP.NET Core ile, API controller'lar kullanarak veriyi kolayca yönetebilir ve farklı istemcilerle etkili bir şekilde iletişim kurabilirsiniz. Yazıda, ApiController yapısının nasıl kullanıldığını ve Entity Framework Core ile veritabanı işlemlerinin nasıl yönetileceğini adım adım öğreneceksiniz. Bu yapılar sayesinde, veritabanı ile etkileşimi kolaylaştırabilir ve verilerin doğru şekilde yönlendirilmesini sağlayabilirsiniz. Ayrıca, HTTP istek türleri (GET, POST, PUT, DELETE) ve bunların API controller metodlarıyla nasıl eşleştirileceği detaylandırılacaktır. Bu bilgiler ışığında, yazının sonunda, kendi CRUD (Create, Read, Update, Delete) işlemlerinizi gerçekleştirebilen tam işlevsel bir RESTful API geliştirme becerisine sahip olacaksınız.",
                        Image = "asp-net-core-restful-api.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "aspnet-core-restful-api",
                        IsActive = true,
                        CategoryId = 5,
                        AppUserId = user2!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "RESTful API geliştirmeye başlarken aradığım rehberdi, elinize sağlık!", PublishedDate = DateTime.Now.AddDays(1), AppUserId = user4!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "Entity Framework Core ile Veri Erişimi",
                        Description = "Entity Framework Core, .NET uygulamalarında veri işlemlerini nesne yönelimli bir şekilde gerçekleştiren güçlü bir ORM aracıdır. Code First yaklaşımıyla veritabanı işlemlerini hızlıca gerçekleştirebilir ve veritabanı yapısını doğrudan C# sınıflarınız üzerinden yönetebilirsiniz.",
                        Content = "Entity Framework Core (EF Core), .NET uygulamalarında veritabanı işlemleri için kullanılan en güçlü ORM (Object-Relational Mapping) araçlarından biridir. EF Core, nesne yönelimli tasarımı ve ilişkisel veritabanları arasındaki bağlantıyı kolaylaştırır. Code First yaklaşımı ise, veritabanı yapısını doğrudan C# sınıfları üzerinden tanımlayarak hızlı ve verimli bir geliştirme süreci sunar. Bu yaklaşım sayesinde, geliştiriciler veritabanı şemalarını koddan türetir ve daha esnek bir yapı oluşturur. Bu yazıda, EF Core’un temellerini öğrenirken DbContext sınıfının nasıl yapılandırılacağını, Migration komutlarıyla veritabanını nasıl güncelleyebileceğinizi ve LINQ ifadeleriyle veri sorgulamanın temellerini detaylı örneklerle keşfedeceksiniz. DbContext sınıfı, veritabanı işlemleri ile etkileşimde bulunmanıza yardımcı olurken, Migration araçları veritabanındaki yapısal değişiklikleri kolayca yönetmenizi sağlar. Ayrıca, EF Core ile güçlü, sürdürülebilir ve test edilebilir veri katmanları oluşturmayı öğrenecek ve uygulamanızda veritabanı işlemlerini daha verimli bir şekilde gerçekleştirebileceksiniz. Bu yazı sayesinde, veri katmanınızı modern ve etkin bir şekilde geliştirebilirsiniz.",
                        Image = "ef-core.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "entity-framework-core-veri-erisimi",
                        IsActive = true,
                        CategoryId = 5,
                        AppUserId = user3!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "Code First konusuna yepyeni başlayanlar için oldukça öğretici bir içerik olmuş, teşekkürler!", PublishedDate = DateTime.Now.AddDays(1), AppUserId = user4!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "Mobil Uygulama Geliştirme Temelleri",
                        Description = "Mobil uygulama geliştirmeye başlarken, platform bağımsızlık, kullanıcı deneyimi ve performans gibi temel unsurları anlamak önemlidir.",
                        Content = "Mobil uygulama geliştirme, platforma özgü araçlar ve dillerle yapılabileceği gibi, çapraz platform çözümleriyle de gerçekleştirilebilir. Android için Java veya Kotlin, iOS için Swift ya da Objective-C dillerinin kullanılması yaygındır. Bunun yanı sıra, Flutter veya React Native gibi çapraz platform araçları ile her iki platformda da çalışabilen uygulamalar geliştirilebilir. Uygulama geliştirmede, kullanıcı deneyimi (UX) tasarımı ve hızlı performans sağlamak, kullanıcıların uygulamayı sürekli kullanmasını sağlamada kritik rol oynar. Uygulamanın işlevselliği kadar, görsel tasarım, hız, animasyonlar ve veri yönetimi gibi unsurlar da dikkate alınmalıdır. Bu yazıda, mobil uygulama geliştirme süreçlerinde kullanılan araçlar ve teknolojiler ile ilgili temel bilgiler bulacaksınız.",
                        Image = "mobil-uygulama-temelleri.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "mobil-uygulama-gelistirme-temelleri",
                        IsActive = true,
                        CategoryId = 2,
                        AppUserId = user1!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "Mobil uygulama geliştirme sürecini iyi anlatmışsınız, çok faydalı bir yazı.", PublishedDate = DateTime.Now, AppUserId = user3!.Id },
                            new Comment { Text = "Çapraz platform çözümleri hakkında daha fazla örnek görmek isterim.", PublishedDate = DateTime.Now, AppUserId = user4!.Id },
                        },
                    },
                    new Post
                    {
                        Title = "Fullstack Web Geliştirme",
                        Description = "Fullstack web geliştirme, hem frontend (kullanıcı arayüzü) hem de backend (sunucu tarafı) teknolojilerini içerir.",
                        Content = "Fullstack web geliştirme, bir web uygulamasının her iki katmanını da (frontend ve backend) geliştirmeyi kapsar. Frontend tarafında HTML, CSS ve JavaScript gibi diller kullanılarak kullanıcı arayüzü oluşturulur. Bu arayüz, kullanıcının uygulama ile etkileşime girmesini sağlar. Backend tarafında ise veri tabanı yönetimi, sunucu tarafı iş mantığı ve API’ler bulunur. Node.js, Python (Django, Flask), Ruby on Rails gibi frameworkler backend geliştirme için yaygın olarak kullanılır. Fullstack geliştirici, her iki katmandaki teknolojileri kullanarak, kullanıcı arayüzünden veri işleme, veritabanı bağlantısı ve sunucu tarafı işlemleri gibi tüm geliştirme süreçlerini yönetebilir. Bu yazıda, fullstack geliştirmeye dair temel bilgiler ve kullanılan popüler teknolojiler hakkında bilgi edineceksiniz.",
                        Image = "fullstack-gelistirme.png",
                        PublishedDate = DateTime.Now,
                        Url = "fullstack-web-gelistirme",
                        IsActive = true,
                        CategoryId = 3,
                        AppUserId = user2!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "Fullstack geliştirme hakkında çok bilgilendirici bir yazı, teşekkürler.", PublishedDate = DateTime.Now, AppUserId = user4!.Id },
                            new Comment { Text = "Node.js kullanarak backend geliştirme üzerine daha fazla içerik ekleyebilir misiniz?", PublishedDate = DateTime.Now, AppUserId = user3!.Id },
                        }
                    },
                    new Post
                    {
                        Title = "Veritabanı Yönetimi ve SQL",
                        Description = "Veritabanı yönetimi, veri depolama, sorgulama ve veri güvenliğini sağlama işlemlerini kapsar.",
                        Content = "Veritabanı yönetimi, bir uygulamanın verilerini düzenlemek ve işlemek için kullanılan bir süreçtir. SQL (Structured Query Language), veritabanı yönetim sistemlerinde (DBMS) veri sorgulama ve yönetme için kullanılan bir dildir. SQL ile veri ekleyebilir, silebilir, güncelleyebilir ve sorgulamalar yapabilirsiniz. Veritabanları, ilişkisel (RDBMS) ve ilişkisel olmayan (NoSQL) olmak üzere iki ana kategoriye ayrılır. MySQL, PostgreSQL gibi RDBMS’ler, verilerin tablolarda ilişkili olarak saklanmasını sağlar. NoSQL veritabanları ise daha esnek veri yapıları ve ölçeklenebilirlik sunar. Bu yazıda, SQL kullanarak veritabanı yönetimi, veri modelleme ve temel sorgulama tekniklerini öğreneceksiniz.",
                        Image = "veritabani-yonetimi.jpg",
                        PublishedDate = DateTime.Now,
                        Url = "veritabani-yonetimi-ve-sql",
                        IsActive = true,
                        CategoryId = 6,
                        AppUserId = user3!.Id,
                        Comments = new List<Comment>
                        {
                            new Comment { Text = "SQL sorgularına dair örnekler çok faydalı olmuş, teşekkürler.", PublishedDate = DateTime.Now, AppUserId = user1!.Id },
                            new Comment { Text = "Veritabanı yönetimi ve NoSQL hakkında daha fazla içerik bekliyorum.", PublishedDate = DateTime.Now, AppUserId = user2!.Id },
                        }
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
