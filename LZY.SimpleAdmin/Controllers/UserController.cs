using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LZY.Application;
using AspNetCorePage;
using LZY.Model;

namespace LZY.SimpleAdmin.Controllers
{
    public class UserController : BaseController
    {
        private IUserBus _bus = null;
        public UserController(IUserBus user)
        {
            _bus = user;
        }
        [HttpGet]
        public IActionResult Index(string s = "", int p = 1)
        {

            PagedList<t_user> list = _bus.GetPageList(s, p);
            return View(list);
        }
       

        /// <summary>
        /// 启用禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  JsonResult UserEnabled(int id)
        {
            Tuple<bool, string> result = _bus.EnabledMatch(id);
            return TupleResult(result);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult UserDeleted(int id)
        {
            Tuple<bool, string> result = _bus.Deleted(id);
            return TupleResult(result);
           
        }


        public  ActionResult Info(int? id)
        {
            t_user model = new t_user();
            if (id.GetValueOrDefault() > 0)
            {
                model = _bus.FindDto(id);
            }
            return View(model);
        }

        [HttpPost]
        public  ActionResult Save(t_user model)
        {
            if (ModelState.IsValid)
            {

                var result = _bus.SaveModel(model);
                if (result.Item1)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", result.Item2);
                    return View("Info", model);
                }
            }
            else
            {
                return View("Info", model);
            }
        }
    }
}