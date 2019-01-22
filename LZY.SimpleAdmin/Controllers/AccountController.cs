using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using LZY.Model;
using LZY.Code;
using LZY.Application;
using Microsoft.AspNetCore.Mvc;
using LZY.Model.ViewModels;

namespace LZY.SimpleAdmin.Controllers
{
    public class AccountController : BaseController
    {
        private IModuleBus _menuBus = null;
        private IUserBus _userBus = null;
        private IUserRoleBus _userRoleBus = null;
        private IRoleMenuBus _roleMenuBus = null;

        public AccountController(IModuleBus menuBus, IUserBus userBus, IUserRoleBus userRoleBus, IRoleMenuBus roleMenuBus)
        {
            _menuBus = menuBus;
            _userBus = userBus;
            _userRoleBus = userRoleBus;
            _roleMenuBus = roleMenuBus;
        }

        // GET: Account
        public ActionResult UserRole(int id)
        {
            ViewBag.Uid = id;
            var models = _userRoleBus.GetUserRoleInfo(id);
            return View(models);
        }

        public ActionResult RoleMenu(int id)
        {
            ViewBag.Rid = id;
            var models = _userRoleBus.GetUserRoleInfo(id);
            return View(models);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult AddRole(int id, int uid)
        {
            Tuple<bool, string> result =_userRoleBus.AddRole(id, uid);
            if (result.Item1)
                return Success();
            else
                return Error(result.Item2);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult CancleRole(int id, int uid)
        {
            Tuple<bool, string> result = _userRoleBus.CancleRole(id, uid);
            if (result.Item1)
                return Success();
            else
                return Error(result.Item2);
        }

        public JsonResult SetMenuToRole(int[] ids, int roleid)
        {
            Tuple<bool, string> result = _roleMenuBus.SetMenuToRole(roleid, ids);
            if (result.Item1)
                return Success();
            else
                return Error(result.Item2);
        }
        [HttpGet]
        public JsonResult GetRoleMenuJson(int id)
        {

            var list = _menuBus.GetLayerList();
            var resultList = _menuBus.GetRoleMenuLayerStr(list, id);
            return Success(resultList);

        }
        [HttpGet]
        public ActionResult UserCenter()
        {
            var userid = OperatorProvider.Provider.GetCurrent().UserId;
            var userInfo = _userBus.FindModel(userid);
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult UserSave(t_user model)
        {

            var result =  _userBus.SaveModel(model);
            if (result.Item1)
                return RedirectToAction("UserCenter");
            else
            {
                ModelState.AddModelError("", result.Item2);
                return View("UserCenter", model);
            }
        }

        [HttpGet]
        public ActionResult ChangePwd()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult ChangePwdSave(ChangePwdViewModel model)
        {

            var result = _userBus.ChangePwdSave(model);
            if (result.Item1)
                return RedirectToAction("ChangePwd");
            else
            {
                ModelState.AddModelError("", result.Item2);
                return View("ChangePwd", model);
            }
        }



    }
}