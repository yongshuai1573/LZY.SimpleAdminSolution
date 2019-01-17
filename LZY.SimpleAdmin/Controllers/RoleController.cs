using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LZY.Application;
using LZY.Model;
namespace LZY.SimpleAdmin.Controllers
{
    public class RoleController : BaseController
    {
        private IRoleBus _bus = null;

        public RoleController(IRoleBus bus)
        {
            _bus = bus;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RoleDeleted(int id)
        {
            Tuple<bool, string> result = _bus.Deleted(id);
            if (result.Item1)
                return Success();
            else
                return Error(result.Item2);
        }


        public ActionResult RoleInfo(int? id)
        {
            t_role model = new t_role();
            if (id.GetValueOrDefault() > 0)
            {
                model = _bus.FindModel(id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RoleSave(t_role model)
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