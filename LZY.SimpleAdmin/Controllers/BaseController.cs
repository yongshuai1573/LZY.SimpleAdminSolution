using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LZY.SimpleAdmin.Controllers
{
    [Authorize]
    [AuthorizeFilter]
    public class BaseController : Controller
    {

        public static LZY.Code.OperatorModel currentUser
        {
            get
            {
                return LZY.Code.OperatorProvider.Provider.GetCurrent();
            }
        }


        public JsonResult TupleResult(Tuple<bool, string> source)
        {
            if (source.Item1)
            {
                return Success(message: source.Item2);
            }
            else
            {
                return Error(message: source.Item2);

            }
        }
        public JsonResult Success(object obj = null, string message = "操作成功")
        {
            return Json(new AjaxResult()
            {
                success = true,
                data = obj,
                message = message,
                state = 100
            });
        }

        public JsonResult Error(object obj = null, string message = "操作失败")
        {
            return Json(new AjaxResult()
            {
                success = false,
                data = obj,
                message = message,
                state = 0
            });
        }

    }
}