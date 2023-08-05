using MedicalNews.Areas.Admin.Repository;
using MedicalNews.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MedicalNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            using (var repo = new UserRepository())
            {
                
                model.Password = Stringcl.Encrypt(model.Password);
                var data = repo.CreateUser(model);
                return Json(data);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            using (var repo = new UserRepository())
            {
                var i = repo.DeleteUser(id);
                return Json(i);
            }

        }

        [HttpGet]
        public IActionResult GetUsersJson()
        {
            using (var repo = new UserRepository())
            {
                var data = repo.GetAllUsers();
                return Json(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            using (var repo = new UserRepository())
            {
                var data = await repo.GetUserById(id);
                return Json(data);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            using (var repo = new UserRepository())
            {
                model.Password = Stringcl.Encrypt(model.Password);
                var i = repo.UpdateUser(model);
                return Json(i);
            }
        }
    }
}
