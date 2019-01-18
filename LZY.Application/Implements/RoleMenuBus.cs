using System;
using System.Collections.Generic;
using System.Text;
using AspNetCorePage;
using LZY.Model;
using LZY.Data;
using System.Linq;

namespace LZY.Application
{
    public class RoleMenuBus : IRoleMenuBus
    {
        public Tuple<bool, string> Deleted(int id)
        {
            throw new NotImplementedException();
        }

        public t_role_module FindModel(object keyValue)
        {
            throw new NotImplementedException();
        }

        public PagedList<t_role_module> GetPageList(string code, int p = 1)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string> SaveModel(t_role_module sourceModel)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Tuple<bool, string> SetMenuToRole(int roleid, int[] uid)
        {
            using (var db=new RepositoryBase().BeginTrans())
            {
                var insertEntitys = uid.ToList().ConvertAll(id => new t_role_module
                {
                    p_moduleid = id,
                    p_roleid = roleid
                });

                try
                {
                    var haveList = db.IQueryable<t_role_module>().Where(a => a.p_roleid == roleid).ToList();
                    db.Delete(haveList);
                    if (insertEntitys.Count > 0)
                    {
                        db.Insert(insertEntitys);
                    }
                    db.Commit();//事务
                    return Tuple.Create(true, "操作成功");
                }
                catch (Exception ex)
                {

                    return Tuple.Create(true, "操作失败:" + ex.Message);

                }

            }
        }
    }
}
