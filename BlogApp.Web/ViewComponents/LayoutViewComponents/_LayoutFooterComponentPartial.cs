using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.ViewComponents.LayoutViewComponents
{
    public class _LayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}