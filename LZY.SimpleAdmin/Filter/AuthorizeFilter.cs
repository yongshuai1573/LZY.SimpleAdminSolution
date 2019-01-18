
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LZY.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LZY.SimpleAdmin
{
    public class AuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public bool Ignore { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // var fromUrl = context..ToLower();

            var userInfo = LZY.Code.OperatorProvider.Provider.GetCurrent();
            if (!Ignore)
                if (userInfo != null)
                {
                    if (userInfo.UserCode != "admin")
                    {

                        //判断是否有权限访问该连接
                        var haveAuthorizeList = LZY.Code.WebHelper.GetSession<List<t_module>>("UserModules");
                        var routeItems = context.RouteData.Values;
                        string cName = routeItems["controller"]?.ToString();
                        string aName = routeItems["action"]?.ToString();
                        if (!CheckAuthorize(cName, aName, haveAuthorizeList))
                        {
                            //filterContext.Result = new HttpUnauthorizedResult("未经授权的请求，请与管理员联系。");     
                            //context.HttpContext.Response.Body = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("对不起，你的权限不足。"));
                            //context.HttpContext.Response.ContentType = "text/html";
                            //context.HttpContext.Response.StatusCode = 401;                           
                            //context.HttpContext.Response.End();
                            ContentResult result = new ContentResult();
                            result.Content = "Sorry，你无权访问该页面。";
                            context.Result = result;
                            return;
                        }

                    }
                }
                else
                {
                    LZY.Code.OperatorProvider.Provider.RemoveCurrent().GetAwaiter().GetResult();
                }

            //context.HttpContext.Session.TryGetValue("UserLoginInfo") as LoginViewModel;
            // if (sessionObj == null)
            // {
            //     FormsAuthentication.SignOut();//登出
            //     filterContext.Result = new RedirectResult("/login/index?actionUrl=" + HttpContext.Current.Server.UrlEncode(fromUrl));
            //     return;
            // }
            // if (!Ignore)
            // {

            //     if (sessionObj.UserName != "admin")
            //     {

            //         //判断是否有权限访问该连接
            //         var haveAuthorizeList = sessionObj.UserMenuList;
            //         var routeItems = filterContext.RouteData.Values;
            //         string cName = routeItems["controller"]?.ToString();
            //         string aName = routeItems["action"]?.ToString();
            //         if (!CheckAuthorize(cName, aName, haveAuthorizeList))
            //         {
            //             //filterContext.Result = new HttpUnauthorizedResult("未经授权的请求，请与管理员联系。");
            //             filterContext.HttpContext.Response.Write("未经授权的请求，请与管理员联系。");
            //             filterContext.HttpContext.Response.End();
            //             return;
            //         }

            //     }
            // }

        }


        private bool CheckAuthorize(string controller, string action, List<t_module> menuList)
        {

            bool flag = false;
            if ((controller ?? "home").ToLower() == "home" && (action ?? "index").ToLower() == "index")
            {
                return true;
            }
            if (controller?.ToLower() == "account")
            {
                return true;
            }

            flag = menuList.Exists(a => a.p_relativeurl?.ToLower() == $"/{controller}/{action}".ToLower() || a.p_relativeurl?.ToLower() + "/index" == $"/{controller}/index".ToLower());
            return flag;
        }


    }
}