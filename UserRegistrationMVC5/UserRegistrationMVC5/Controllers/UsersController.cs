using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistrationMVC5.Models;
using UserRegistrationMVC5.Repository;

namespace UserRegistrationMVC5.Controllers
{
    public class UsersController : Controller
    {
        readonly IUsersRepository _userRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _userRepository.GetAllUsers();
            return View(model);
        }

        [HttpGet]
        public ActionResult GetUser(int id)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Edit User Failed";
            }

            User model = _userRepository.GetUserById(id);
            return View(model);

        }

        [HttpPost]
        public ActionResult Update(User model)
        {
            if (ModelState.IsValid)
            {
                int result = _userRepository.UpdateUser(model);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {

                    return RedirectToAction("Index", "Users");
                }
            }

            return View();
        }


        public ActionResult NewUserView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewUser(User model)
        {
            if (ModelState.IsValid)
            {
                int result = _userRepository.AddUser(model);
                if(result > 0)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    TempData["Failed"] = "Failed";
                    return RedirectToAction("AddNewUser", "Users");
                }
            }
            return RedirectToAction("Index", "Users");
        }

        public ActionResult Delete(int id)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Delete User Failed";
            }

            _userRepository.DeleteUser(id);
            return RedirectToAction("Index", "Users");
        }
    }
}