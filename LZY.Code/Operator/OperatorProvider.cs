using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Web;
using System.Security.Claims;

namespace LZY.Code
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }


        public OperatorModel GetCurrent()
        {
            OperatorModel operatorModel = new OperatorModel();
            foreach (var claim in HttpContext.Current.User.Claims)
            {
                if (claim.Type == "OperatorModel")
                {
                    operatorModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatorModel>(claim.Value);
                }
            }
            return operatorModel;
        }

        /// <summary>
        /// 设置当前登录用户的信息
        /// </summary>
        /// <param name="operatorModel"></param>
        /// <returns></returns>
        public async Task AddCurrent(OperatorModel operatorModel)
        {

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, operatorModel.UserId.ToString()),
                new Claim("OperatorModel",Newtonsoft.Json.JsonConvert.SerializeObject(operatorModel))
            }, "IdentityCookieAuthenScheme"));

            await HttpContext.Current.SignInAsync("IdentityCookieAuthenScheme", principal, new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),//设置登录的Cookie的有效期时间
                AllowRefresh = true//50%时间内刷新，顺延到60分钟
            });

        }
        public async Task RemoveCurrent()
        {
            await HttpContext.Current.SignOutAsync("IdentityCookieAuthenScheme");
        }
    }
}
