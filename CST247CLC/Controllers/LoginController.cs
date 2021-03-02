using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST247CLC.Models;
using CST247CLC.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace CST247CLC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            bool isRegistered = false;

            UserBusinessService ubs = new UserBusinessService();
            isRegistered = ubs.loginUser(userModel);
            if (isRegistered)
            {
                ViewBag.username = userModel.userName;
                return View("Views/Login/LoginSuccess.cshtml");
            }
            else
            {
                return View("Views/Login/LoginFail.cshtml");
            }

        }


    }
}