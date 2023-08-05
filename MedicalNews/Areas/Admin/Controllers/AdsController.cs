using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAds(AdsModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using (var repo = new AdsRespository())
            {
                model.ImageUrl = await UploadImage(model.AdsImage);
                var data = repo.CreateAds(model);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteAds(int id, string filepath)
        {
            if (filepath != null)
            {
                if (!DeleteImage(filepath))
                {
                    return Json(false);
                }

            }
            using (var repo = new AdsRespository())
            {
                var i = repo.DeleteAds(id);
                return Json(i);
            }
        }

        [HttpGet]
        public IActionResult GetAdsJson()
        {
            using(var repo = new AdsRespository())
            {
                var data = repo.GetAllAds();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdsById(int id)
        {
            using(var repo= new AdsRespository())
            {
                var data = await repo.GetAdsById(id);
                return Json(data);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAds(AdsUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            if(model.AdsImage != null)
            {
                if (!DeleteImage(model.ImageUrl)) { 
                    return Json(false);
                }
                model.ImageUrl = await UploadImage(model.AdsImage);
            }

            using(var repo = new AdsRespository())
            {
                var i = repo.UpdateAds(model);
                return Json(i); 
            }
        }

        private async Task<string?> UploadImage(IFormFile file)
        {
            var filename = string.Format("{0 : ddMMyyyyhhmmss}", DateTime.Now) + "_ads" + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(@"wwwroot\upload\ads", filename);

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
