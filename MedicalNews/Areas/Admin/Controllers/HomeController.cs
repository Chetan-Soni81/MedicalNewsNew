using MedicalNews.Areas.Admin.Models;
using MedicalNews.Areas.Admin.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var repo = new AdminRepository())
            {
                model.Password = Stringcl.Encrypt(model.Password);
                var admin = repo.LoginAdmin(model.Username, model.Password);

                if (admin.Id == 0)
                {
                    ViewData["Message"] = "* Invalid Credentials";
                    return View();
                }

                admin.RememberMe = model.RememberMe;

                model = null;

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Username),
                    new Claim(ClaimTypes.Role, admin.RoleId == 1 ? "SuperAdmin" : "User")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = admin.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
            }

           

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Links()
        {
            using (var repo = new LinksRepository())
            {
                ViewBag.Links = repo.GetAllLinks();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Links(LinksModel model)
        {
            int i = 0;
            using (var repo = new LinksRepository())
            {
                if (!ModelState.IsValid)
                {
                    var list = repo.GetAllLinks();
                    ViewBag.Links = list.Skip((i - 1)* 10).Take(10).ToList();
                    return View(model);
                }

                i = repo.CreateLink(model);
                ViewBag.Links = repo.GetAllLinks();
            }

            ViewData["Message"] = i == 0 ? "Link cannot be created" : "Link created successfull";

            ModelState.Clear();

            return View();
        }

        [HttpGet]
        public JsonResult GetLinkJson(int pageno)
        {
            pageno = pageno < 1 ? 1 : pageno;
            using(var repo = new LinksRepository())
            {
                var paginatedLinks = repo.GetPaginatedList(pageno, 10);
                return Json(paginatedLinks);
            }
        }

        public IActionResult DeleteLink(int id)
        {
            using (var repo = new LinksRepository())
            {
                var i = repo.DeleteLink(id);
            }

            return RedirectToAction("Links");
        }
    }
}
