using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.Application;
using Microsoft.AspNetCore.Mvc;
using AspNetCorePage;
using LZY.Model;

namespace LZY.SimpleAdmin.Controllers
{
    public class ParaController : BaseController
    {
        private IParaBus _bus = null;
        private IParaValueBus _vbus = null;
        public ParaController(IParaBus bus, IParaValueBus vbus)
        {
            _vbus = vbus;
            _bus = bus;
        }
        [HttpGet]
        public IActionResult Index(string s = "", int p = 1)
        {

            PagedList<t_paratype> list = _bus.GetPageList(s, p);
            return View(list);
        }

        public ActionResult Info(int? id)
        {
            t_paratype model = new t_paratype();
            if (id.GetValueOrDefault() > 0)
            {
                model = _bus.FindDto(id);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(t_paratype model)
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


        public ActionResult ChildList(int id)
        {
            if (id <= 0)
            {
                return new NotFoundObjectResult("数据不存在");
            }
            var list = _vbus.GetListByTypeId(id);
            ViewBag.ValueList = list;
            ViewBag.Tid = id;
            return View();
        }
        [HttpPost]
        public ActionResult ValueSave(t_para_values model)
        {
            if (ModelState.IsValid)
            {


                var result = _vbus.SaveModel(model);
                if (result.Item1)
                    return RedirectToAction("ChildList", new { id = model.p_pid });
                else
                {
                    ModelState.AddModelError("", result.Item2);
                    return View("ChildList", model);
                }
            }
            else
            {
                return View("ChildList", model);
            }
        }

        [HttpPost]
        public ActionResult ParaDeleted(int id)
        {
            var result = _bus.Deleted(id);
            return TupleResult(result);
        }


        public ActionResult ChildDelete(int id)
        {
            var result = _vbus.Deleted(id);
            return TupleResult(result);
        }
    }
}