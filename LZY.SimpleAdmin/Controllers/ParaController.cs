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
        public ParaController(IParaBus bus)
        {
            _bus = bus;
        }
        [HttpGet]
        public IActionResult Index(int p = 1)
        {
            PagedList<t_paratype> list = _bus.GetPageList("", p);
            return View(list);
        }
    }
}