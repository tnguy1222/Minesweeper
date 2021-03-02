using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST247CLC.Models;
using CST247CLC.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace CST247CLC.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            bool isRegistered = false;

            UserBusinessService ubs = new UserBusinessService();
            isRegistered = ubs.createUser(userModel);
            if (isRegistered)
            {
                return View("Views/Register/RegisterSuccess.cshtml", userModel);
            }
            else
            {
                return View("Views/Register/RegisterFail.cshtml");
            }

        }
    }
}