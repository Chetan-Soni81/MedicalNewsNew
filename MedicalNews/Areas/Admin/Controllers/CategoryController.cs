using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryController : Controller
    {
        #region Event Category
        public IActionResult Event()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Event(EventCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var repo = new CategoryRepository())
            {

                int i = repo.CreateEventCategory(model);

                if (i != 0)
                {
                    ViewData["Message"] = "Event Category Created Successful";
                }

                ModelState.Clear();
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetEventJson()
        {
            using (var repo = new CategoryRepository())
            {
                var data = repo.GetAllEventCategories();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var data = await repo.GetEventCategoryById(id);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult EventDelete(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var i = repo.DeleteEventCategory(id);
                return Json(i);
            }
        }

        [HttpPut]
        public IActionResult UpdateEvent(EventCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using (var repo = new CategoryRepository())
            {
                var i = repo.UpdateEventCategory(model);
                return Json(i);
            }
        }
        #endregion

        #region Interview Category
        public IActionResult Interview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Interview(InterviewCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var repo = new CategoryRepository())
            {

                var i = repo.CreateInterviewCategory(model);

                ViewData["Message"] = i == 0 ? "Cannot Create Interview Category" : "Interview Category Created Successful";

                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetInterviewJson()
        {
            using (var repo = new CategoryRepository())
            {
                var data = repo.GetAllInterviewCategories();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewById(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var data = await repo.GetInterviewCategoryById(id);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteInterview(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var i = repo.DeleteInterviewCategory(id);
                return Json(i);
            }

        }

        [HttpPut]
        public IActionResult UpdateInterview(InterviewCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using(var repo = new CategoryRepository())
            {
                var i = repo.UpdateInterviewCategory(model);
                return Json(i);
            }
        }
        #endregion

        #region Article Category

        public IActionResult Article()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Article(ArticleCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var repo = new CategoryRepository())
            {
                int i = repo.CreateArticleCategory(model);

                ViewData["Message"] = "Article Category Not Created";

                if (i != 0)
                {
                    ViewData["Message"] = "Article Category Created Successful";

                }

                ModelState.Clear();

                return View();
            }
        }

        [HttpGet]
        public IActionResult GetArticleJson()
        {
            using (var repo = new CategoryRepository())
            {
                var data = repo.GetAllArticleCategories();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetArticleById(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var data = await repo.GetArticleCategoryById(id);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            using (var repo = new CategoryRepository())
            {
                int i = repo.DeleteArticleCategory(id);
                return Json(i);
            }
        }

        [HttpPut]
        public IActionResult UpdateArticle(ArticleCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }


            using (var repo = new CategoryRepository())
            {
                var data = repo.UpdateArticleCategory(model);
                return Json(data);
            }
        }
        #endregion

        #region Article SubCategory

        public IActionResult ArticleSub(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult ArticleSub(ArticleSubCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var repo = new CategoryRepository())
            {
                int i = repo.CreateArticleSubCategory(model);

                ViewData["Message"] = "Article Category Not Created";

                if (i != 0)
                {
                    ViewData["Message"] = "Article Category Created Successful";

                }

                int? id = model.ArticleCategoryID;

                ModelState.Clear();

                return RedirectToAction("ArticleSub", new { id = id}) ;
            }
        }

        [HttpPost]
        public IActionResult CreateArticleSub(ArticleSubCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using (var repo = new CategoryRepository())
            {
                int i = repo.CreateArticleSubCategory(model);

                return Json(i);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetArticleSubJson(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var data = repo.GetAllArticleSubCategories(id);
                return Json(data);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticleSubById(int id)
        {
            using (var repo = new CategoryRepository())
            {
                var data = await repo.GetArticleSubCategoryById(id);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteArticleSub(int id)
        {
            using (var repo = new CategoryRepository())
            {
                int i = repo.DeleteArticleSubCategory(id);
                return Json(i);
            }
        }

        [HttpPut]
        public IActionResult UpdateArticleSub(ArticleSubCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }


            using (var repo = new CategoryRepository())
            {
                var data = repo.UpdateArticleSubCategory(model);
                return Json(data);
            }
        }
        #endregion
    }
}
