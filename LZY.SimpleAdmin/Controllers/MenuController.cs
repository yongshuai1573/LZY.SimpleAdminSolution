using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LZY.Model;
using LZY.Application;
using AspNetCorePage;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LZY.SimpleAdmin.Controllers
{
    public class MenuController : BaseController
    {
        private LZY.Application.IModuleBus _bus = null;
        public MenuController(IModuleBus bus)
        {
            _bus = bus;
        }
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }      



        [HttpGet]
        public JsonResult GetMenuJson()
        {

            var list = _bus.GetLayerList();
            var resultList = _bus.GetMenuLayerStr(list);
            return Success(resultList);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  JsonResult MenuDeleted(int id)
        {
            Tuple<bool, string> result = _bus.Deleted(id);
            if (result.Item1)
                return Success();
            else
                return Error(result.Item2);
        }


        public ActionResult MenuInfo(int? id)
        {
            t_module model = new t_module();
            if (id.GetValueOrDefault() > 0)
            {
                model = _bus.FindModel(id);
            }
            var list = _bus.GetRootMenus();
            List<SelectListItem> selectList = new SelectList(list, "p_id", "p_name", id).ToList();
            selectList.Insert(0, new SelectListItem() { Text = "根目录", Value = "0" });
            ViewBag.p_parentid = selectList;
            return View(model);
        }

        [HttpPost]
        public ActionResult MenuSave(t_module model)
        {
            if (ModelState.IsValid)
            {

                var result = _bus.SaveModel(model);
                if (result.Item1)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", result.Item2);
                    return View("MenuInfo", model);
                }
            }
            else
            {
                return View("MenuInfo", model);
            }
        }
    }
}