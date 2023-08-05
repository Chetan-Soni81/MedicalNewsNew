using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.EventCategories = repo.GetAllEventCategories();
            }

            using (var repo = new EventRepository())
            {
                ViewBag.Events = repo.GetAllEvents();
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetEventsJson()
        {
            using(var repo = new EventRepository())
            {
                var data = repo.GetAllEvents();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(int id)
        {
            using (var repo = new EventRepository())
            {
                var data = await repo.GetEventById(id);
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] EventModel model)
        {
            using (var repo = new CategoryRepository())
            {
                ViewBag.EventCategories = repo.GetAllEventCategories();
            };

            int i = 0;

            using (var repo = new EventRepository())
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Events = repo.GetAllEvents();
                    return View(model);
                }
                model.ImageUrl = await UploadImage(model.CoverImage);

                i = repo.CreateEvent(model);
                ViewBag.Events = repo.GetAllEvents();
            }

            ViewData["Message"] = i == 0 ? "Event Cannot be created" : "Event Created successfull";

            ModelState.Clear();

            return View();
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int id, string? filepath)
        {
            if (filepath != null)
            {
                if (!DeleteImage(filepath))
                {
                    return Json(false); ;
                }
            }


            using (var repo = new EventRepository())
            {
                var i = repo.DeleteEvent(id);
            }

            return Json(true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if(model.CoverImage != null)
            {
                if (DeleteImage(model.ImageUrl))
                {
                    model.ImageUrl = await UploadImage(model.CoverImage);
                }
            }

            using(var repo = new EventRepository())
            {
                var i = repo.UpdateEvent(model);
                return Json(i);
            }
        }

        private async Task<string?> UploadImage(IFormFile file)
        {
            var filename = string.Format("{0 : ddMMyyyyhhmmss}", DateTime.Now) + "_event" + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(@"wwwroot\upload\event", filename);

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
