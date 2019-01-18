using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using LZY.Model;
using LZY.Code;
using LZY.Application;
using Microsoft.AspNetCore.Mvc;

namespace LZY.SimpleAdmin.Controllers
{
    public class AccountController : BaseController
    {
        private IModuleBus _menuBus = null;
        private IUserBus _userBus = null;        
      
        //private UserRoleBus userRoleBus = new UserRoleBus();
        //private RoleMenuBus roleMenuBus = new RoleMenuBus();

        //// GET: Account
        //public ActionResult UserRole(int id)
        //{
        //    ViewBag.Uid = id;
        //    var models = await userRoleBus.GetUserRoleInfo(id);
        //    return View(models);
        //}

        //public ActionResult RoleMenu(int id)
        //{
        //    ViewBag.Rid = id;
        //    var models = await userRoleBus.GetUserRoleInfo(id);
        //    return View(models);
        //}
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<JsonResult> AddRole(int id, int uid)
        //{
        //    Tuple<bool, string> result = await userRoleBus.AddRole(id, uid);
        //    if (result.Item1)
        //        return Success();
        //    else
        //        return Error(result.Item2);
        //}
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<JsonResult> CancleRole(int id, int uid)
        //{
        //    Tuple<bool, string> result = await userRoleBus.CancleRole(id, uid);
        //    if (result.Item1)
        //        return Success();
        //    else
        //        return Error(result.Item2);
        //}

        //public async Task<JsonResult> SetMenuToRole(int[] ids, int roleid)
        //{
        //    Tuple<bool, string> result = await roleMenuBus.SetMenuToRole(roleid, ids);
        //    if (result.Item1)
        //        return Success();
        //    else
        //        return Error(result.Item2);
        //}
        //[HttpGet]
        //public async Task<JsonResult> GetRoleMenuJson(int id)
        //{

        //    var list = await menuBus.GetLayerList();
        //    var resultList = menuBus.GetRoleMenuLayerStr(list, id);
        //    return Success(resultList);

        //}
        //[HttpGet]
        //public ActionResult UserCenter()
        //{
        //    var userid = Common.CommonTool.CurrentUserModel.UserId;
        //    var userInfo = await userbll.FindDto(userid);
        //    return View(userInfo);
        //}

        //[HttpPost]
        //public ActionResult UserSave(UserViewModel model)
        //{         

        //        var result = await userbll.SelfSaveModel(model);
        //        if (result.Item1)
        //            return RedirectToAction("UserCenter");
        //        else
        //        {
        //            ModelState.AddModelError("", result.Item2);
        //            return View("UserCenter", model);
        //        }          
        //}

        //[HttpGet]
        //public ActionResult ChangePwd()
        //{
        //    await Task.Delay(0);          
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ChangePwdSave(ChangePwdViewModel model)
        //{

        //    var result = await userbll.ChangePwdSave(model);
        //    if (result.Item1)
        //        return RedirectToAction("ChangePwd");
        //    else
        //    {
        //        ModelState.AddModelError("", result.Item2);
        //        return View("ChangePwd", model);
        //    }
        //}



    }
}