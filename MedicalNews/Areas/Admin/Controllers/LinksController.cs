﻿using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class LinksController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateIndex(LinksModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            using (var repo = new LinksRepository())
            {
                var data = repo.CreateLink(model);
                return Json(data);
            }
        }

        [HttpGet]
        public JsonResult GetLinkJson()
        {
            //pageno = pageno < 1 ? 1 : pageno;
            using (var repo = new LinksRepository())
            {
                var links = repo.GetAllLinks();
                return Json(links);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLinkById(int id)
        {
            using (var repo = new LinksRepository())
            {
                var links = await repo.GetLinkbyID(id);

                return Json(links);
            }

        }

        [HttpDelete]
        public IActionResult DeleteLink(int id)
        {
            using (var repo = new LinksRepository())
            {
                int i = repo.DeleteLink(id);

                return Json(i);
            }
        }

        [HttpPut]
        public IActionResult UpdateLink(LinksModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            using (var repo = new LinksRepository())
            {
                int i = repo.UpdateLink(model);

                if (i != 0)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
        }



    }
}
