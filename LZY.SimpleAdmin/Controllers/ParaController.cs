using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.Application;
using Microsoft.AspNetCore.Mvc;

namespace LZY.SimpleAdmin.Controllers
{
    public class ParaController : BaseController
    {
       
        public ParaController()
        {

        }
        public IActionResult Index(int p = 1)
        {
            return View();
        }
    }
}