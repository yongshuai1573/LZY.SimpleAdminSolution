using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LZY.Repository;

namespace LZY.Application
{
    public class UserBus : IUserBus
    {
        private It_userRepository _service;
        private It_moduleRepository _moduleRepository;
        public UserBus(It_userRepository service, It_moduleRepository moduleRepository)
        {
            this._service = service;
            this._moduleRepository = moduleRepository;
        }
        public int getCount()
        {
            return _service.IQueryable().Count();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public Tuple<bool, string, LZY.Code.OperatorModel> userLogin(string userName, string passWord)
        {
            LZY.Code.OperatorModel userInfo = new Code.OperatorModel();
            var user = _service.IQueryable().FirstOrDefault(a => a.p_account == userName && a.p_password == passWord && a.p_deleted == false);
            if (user == null)
            {
                return Tuple.Create(false, "用户名或者密码错误，请重试！", userInfo);
            }
            if (user.p_enabled == false)
            {
                return Tuple.Create(false, "用户当前已被禁用，请与管理员联系！", userInfo);
            }
            //登录成功！
            //存值 
            #region 组装用户登录后的用户数据        

            userInfo.UserId = user.p_id;
            userInfo.UserName = string.IsNullOrWhiteSpace(user.p_realname) ? user.p_account : user.p_realname;
            userInfo.LoginTime = DateTime.Now;
            userInfo.UserCode = user.p_account;
            #endregion
            //加载菜单放入Session中
            List<LZY.Model.t_module> menuList = _moduleRepository.GetMenuList(userInfo);
            LZY.Code.WebHelper.WriteSession("UserModules", menuList);

            return Tuple.Create(true, "登录成功！", userInfo);
        }

    }
}
