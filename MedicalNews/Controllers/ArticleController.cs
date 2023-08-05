using Microsoft.AspNetCore.Mvc;

namespace MedicalNews.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
