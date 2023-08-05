using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.Categories = repo.GetAllArticleCategories();
            }
            return View();
        }

        public IActionResult Add()
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.Categories = repo.GetAllArticleCategories();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleModel article)
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.Categories = repo.GetAllArticleCategories();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            article.ImageUrl1 = await UploadImage(article.Image1);

            if (article.Image2 != null)
            {
                article.ImageUrl2 = await UploadImage(article.Image2);
            }

            using (var repo = new ArticleRepository())
            {
                var i = repo.CreateAritcle(article);
                if (i == 0)
                {
                    return View(article);
                }
            }

            return RedirectToAction("Add");
        }

        [HttpGet]
        public IActionResult GetArticleJson()
        {
            using (var repo = new ArticleRepository())
            {
                var data = repo.GetAllArticles();
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            using (var repo = new ArticleRepository())
            {
                var i = repo.DeleteArticle(id);

                //if (i != 0)
                //{
                  //  DeleteImage(filepath);
                //}

                return Json(i);
            }
        }

        private async Task<string?> UploadImage(IFormFile file)
        {
            var filename = "article_" + string.Format("{0 : ddMMyyyyhhmmss}", DateTime.Now) + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(@"wwwroot\upload\article", filename);

            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return filepath;
        }

        private bool DeleteImage(string filepath)
        {
            try
            {

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
