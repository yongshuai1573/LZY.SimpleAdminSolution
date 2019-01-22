using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LZY.Repository;
using LZY.Model;
using AspNetCorePage;
using LZY.Code;
using LZY.Model.ViewModels;

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

        public Tuple<bool, string> ChangePwdSave(ChangePwdViewModel sourceModel)
        {
            var userid = LZY.Code.OperatorProvider.Provider.GetCurrent().UserId;
            if (sourceModel == null)
            {
                return Tuple.Create(false, "错误的请求对象！");
            }


            var oldModel =FindModel(userid);           
            if (sourceModel.OldPwd != oldModel.p_password)
            {
                return Tuple.Create(false, "原密码错误！");

            }
            oldModel.p_password = sourceModel.NewPwd;
            return SaveModel(oldModel);

        }

        public Tuple<bool, string> Deleted(int id)
        {
            var model = _service.FindEntity(id);
            if (model != null)
            {
                model.p_updatetime = DateTime.Now;
                model.p_updateuserid = OperatorProvider.Provider.GetCurrent().UserId;
                model.p_deleted = true;
                _service.Update(model);
                return Tuple.Create(true, "操作成功！");

            }
            return Tuple.Create(false, "操作异常，未找到有效的数据");
        }

        public Tuple<bool, string> EnabledMatch(int id)
        {
            var model = _service.FindEntity(id);
            if (model != null)
            {
                model.p_updatetime = DateTime.Now;
                model.p_updateuserid = OperatorProvider.Provider.GetCurrent().UserId;
                model.p_enabled = !model.p_enabled;
                _service.Update(model);
                return Tuple.Create(true, "操作成功！");

            }
            return Tuple.Create(false, "操作异常，未找到有效的数据");
        }

        public t_user FindModel(object keyValue)
        {
            var dbModel = _service.FindEntity(keyValue);

            return dbModel;
        }

        public PagedList<t_user> GetPageList(string code, int p = 1)
        {
            var query = _service.IQueryable();
            var express = ExtLinq.True<t_user>();
            express = express.And(model => model.p_deleted == false);
            if (!string.IsNullOrWhiteSpace(code))//查询 条件
            {
                express = express.And(m => m.p_account.Contains(code) || m.p_realname.Contains(code));
                query = query.Where(express);
            }
            return query.ToPagedList(p, PubliceParas.PageSize);
        }

        public Tuple<bool, string> SaveModel(t_user sourceModel)
        {
            bool result = false;

            if (sourceModel == null)
            {
                return Tuple.Create(false, "错误的请求对象！");
            }
            if (sourceModel.p_id > 0)//修改
            {
                sourceModel.p_updatetime = DateTime.Now;
                sourceModel.p_updateuserid = LZY.Code.OperatorProvider.Provider.GetCurrent().UserId;
                result = _service.Update(sourceModel) > 0;
            }
            else//新增
            {
                sourceModel.p_createtime = DateTime.Now;
                sourceModel.p_deleted = false;
                sourceModel.p_createuserid = LZY.Code.OperatorProvider.Provider.GetCurrent().UserId;
                result = _service.Insert(sourceModel) > 0;
            }

            if (result)
            {
                return Tuple.Create(true, "操作成功！");
            }
            else
            {
                return Tuple.Create(false, "操作失败！");

            }

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
