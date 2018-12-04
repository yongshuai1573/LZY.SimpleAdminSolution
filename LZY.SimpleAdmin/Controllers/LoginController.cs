using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.SimpleAdmin.Models;
using Microsoft.AspNetCore.Mvc;

namespace LZY.SimpleAdmin.Controllers
{
    public class LoginController : Controller
    {
        private Application.IUserBus _user;
        public LoginController(LZY.Application.IUserBus user)
        {
            _user = user;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
              //  loginModel.UserName = loginModel.UserName.ToUpper();
                var loginResult = _user.userLogin(loginModel.UserName, loginModel.PassWord);
                if (loginResult.Item1)
                {

                    await LZY.Code.OperatorProvider.Provider.AddCurrent(loginResult.Item3);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", loginResult.Item2);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(loginModel);
        }

        public async Task<ActionResult> SignOut()
        {
            await LZY.Code.OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
    }
}