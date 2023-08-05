using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class InterviewController : Controller
    {
        public IActionResult Index()
        {
            using (var repo = new InterviewRepository())
            {
                ViewBag.Interviews = repo.GetAllInterviews();
            }

            using(var repo = new CategoryRepository()) { 
                ViewBag.InterviewCategories = repo.GetAllInterviewCategories();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(InterviewModel model)
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.InterviewCategories = repo.GetAllInterviewCategories();
            }
            int i = 0;
            using (var repo = new InterviewRepository())
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Interviews = repo.GetAllInterviews();
                    return View(model);
                }

                i = repo.CreateInterview(model);
                ViewBag.Interviews = repo.GetAllInterviews();
            }

            ViewData["Message"] = i == 0 ? "Interview cannot be created" : " Interview created successful";

            ModelState.Clear();
            return View();
        }

        [HttpDelete]
        public IActionResult DeleteInterview(int id)
        {
            using (var repo = new InterviewRepository())
            {
                var i = repo.DeleteInterview(id);
                return Json(i);
            }
        }

        [HttpGet]
        public IActionResult GetInterviewsJson()
        {
            using( var repo = new InterviewRepository())
            {
                var data = repo.GetAllInterviews();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewById(int id)
        {
            using (var repo = new InterviewRepository())
            {
                var data = await repo.GetInterviewById(id);
                return Json(data);
            }
        }

        [HttpPut]
        public IActionResult UpdateInterview(InterviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using(var repo = new InterviewRepository())
            {
                var i = repo.UpdateInterview(model);
                return Json(i);
            }
        }
    }
}
