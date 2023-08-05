using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class MagazineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pages(int id)
        {
            ViewData["magId"] = id;
            return View("AddPages");
        }

        [HttpGet]
        public async Task<IActionResult> GetMagazinesJson()
        {
            using (var repo = new MagazineRepository())
            {
                var data = await repo.GetAllMagazines();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMagazineById(int id)
        {
            using(var repo = new MagazineRepository())
            {
                var data = await repo.GetMagazineById(id);
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMagazine(MagazineModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(false);
            }

            if(model.CoverImage != null)
            {
                model.ImageUrl = await UploadImage(model.CoverImage);
            }

            using (var repo = new MagazineRepository())
            {
                var data = repo.CreateMagazine(model);
                return Json(data);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMagazine(MagazineModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (model.CoverImage != null)
            {
                DeleteImage(model.ImageUrl);
                model.ImageUrl = await UploadImage(model.CoverImage);
            }

            using (var repo = new MagazineRepository())
            {
                var data = repo.UpdateMagazine(model);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteMagazine(int magId, string filepath)
        {
            using(var repo = new MagazineRepository())
            {
                var data = repo.DeleteMagazine(magId);

                if (data != 0)
                {
                    DeleteImage(filepath);
                }

                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPagesJson(int magId)
        {
            using (var repo = new PageRepository())
            {
                var data = await repo.GetAllPages(magId);
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPageById(int id)
        {
            using (var repo = new PageRepository())
            {
                var data = await repo.GetPageById(id);
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(PageModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (model.formFile != null)
            {
                model.PdfUrl = await UploadFile(model.formFile);
            }

            using (var repo = new PageRepository())
            {
                var data = repo.CreatePage(model);
                return Json(data);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePage(PageModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if (model.formFile != null)
            {
                DeleteFile(model.PdfUrl);
                model.PdfUrl = await UploadFile(model.formFile);
            }

            using (var repo = new PageRepository())
            {
                var data = repo.UpdatePage(model);
                return Json(data);
            }
        }

        [HttpPost]
        public IActionResult DeletePage(int pageId, string filepath)
        {
            using (var repo = new PageRepository())
            {
                var data = repo.DeletePage(pageId);

                if (data != 0)
                {
                    DeleteFile(filepath);
                }

                return Json(data);
            }
        }

        private async Task<string?> UploadFile(IFormFile file)
        {
            var filename = string.Format("{0 : ddMMyyyyhhmmss}", DateTime.Now) + "_magpage" + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(@"wwwroot\upload\magazine\page", filename);

            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return filepath;
        }

        private bool DeleteFile(string filepath)
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

        private async Task<string?> UploadImage(IFormFile file)
        {
            var filename = string.Format("{0 : ddMMyyyyhhmmss}", DateTime.Now) + "_mag" + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(@"wwwroot\upload\magazine\cover", filename);

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
