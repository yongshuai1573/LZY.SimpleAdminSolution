using LZY.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Repository;
using LZY.Code;
using AspNetCorePage;
using System.Linq;
namespace LZY.Application
{
    public class UserRoleBus : IUserRoleBus
    {
        private It_user_roleRepository _service;
        private It_roleRepository _roleService;

        public UserRoleBus(It_user_roleRepository service, It_roleRepository roleService)
        {
            _service = service;
            _roleService = roleService;
        }
        public t_user_role FindModel(object keyValue)
        {
            var dbModel = _service.FindEntity(keyValue);

            return dbModel;
        }

        public Tuple<bool, string> SaveModel(t_user_role sourceModel)
        {
            bool result = false;

            if (sourceModel == null)
            {
                return Tuple.Create(false, "错误的请求对象！");
            }
            if (sourceModel.p_id > 0)//修改
            {
                result = _service.Update(sourceModel) > 0;
            }
            else//新增
            {
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
        public UserRoleViewModel GetUserRoleInfo(int id)
        {

            UserRoleViewModel model = new UserRoleViewModel();
            var roleList = _roleService.IQueryable().ToList();
            var userroleList = _service.IQueryable().Where(a => a.p_userid == id);
            model.HasRoles = (roleList).FindAll(a => userroleList.Select(b => b.p_roleid).Contains(a.p_id));
            model.NoRoles = roleList.Except(model.HasRoles).ToList();

            return model;

        }

        public PagedList<t_user_role> GetPageList(string code, int p)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<bool, string> AddRole(int roleId, int userId)
        {
            if (roleId == 0 || userId == 0)
            {
                return Tuple.Create(false, "错误的请求");
            }
            t_user_role entity = new t_user_role();
            entity.p_roleid = roleId;
            entity.p_userid = userId;
            return SaveModel(entity);
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Tuple<bool, string> CancleRole(int roleId, int userId)
        {
            if (roleId == 0 || userId == 0)
            {
                return Tuple.Create(false, "错误的请求");
            }
            var model = _service.FindEntity(a => a.p_userid == userId && a.p_roleid == roleId);
            if (model != null)
            {
                _service.Delete(model);
                return Tuple.Create(true, "操作成功！");

            }
            return Tuple.Create(false, "操作异常，未找到有效的数据");
        }

        public Tuple<bool, string> Deleted(int id)
        {
            var model = _service.FindEntity(id);
            if (model != null)
            {
                _service.Delete(model);
                return Tuple.Create(true, "操作成功！");

            }
            return Tuple.Create(false, "操作异常，未找到有效的数据");
        }
    }







}
