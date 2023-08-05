using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class PageController : Controller
    {
        public IActionResult Index(int id)
        {
            ViewData["magId"] = id;
            return View();
        }

        [HttpGet]
        public IActionResult GetPagesJson(int magId)
        {
            using(var repo = new PageRepository())
            {
                var data = repo.GetAllPages(magId);
                return Json(data);
            }
        }

        [HttpGet]
        public IActionResult GetPageById(int id)
        {
            using(var repo = new PageRepository())
            {
                var data = repo.GetPageById(id);
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

            if(model.formFile!= null)
            {
                model.PdfUrl = await UploadFile(model.formFile);
            }

            using(var repo = new PageRepository())
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

            if (model.formFile!= null)
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
            using(var repo = new PageRepository())
            {
                var data = repo.DeletePage(pageId);

                if(data != 0)
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
    }
}
