using LZY.Model;
using LZY.Data;
using LZY.Code;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace LZY.Repository
{
    public class t_moduleRepository : RepositoryBase<t_module>, It_moduleRepository
    {
        /// <summary>
        /// 获取用户所有的菜单
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public List<t_module> GetMenuList(OperatorModel userInfo)
        {
            using (var db = new LZYDbContext())
            {
                var search = from r in db.Set<t_user_role>()
                             join rm in db.Set<t_role_module>()
                             on r.p_roleid equals rm.p_roleid
                             join m in db.Set<t_module>()
                             on rm.p_moduleid equals m.p_id
                             where r.p_userid == userInfo.UserId && m.p_enabled && m.p_deleted == false
                             orderby m.p_sort, m.p_createtime
                             select m;
                List<t_module> mList = new List<t_module>();
                if (userInfo.UserName == "admin")
                {
                    search = db.Set<t_module>().Where(p => p.p_enabled && p.p_deleted == false).OrderBy(a => a.p_sort).ThenBy(a => a.p_createtime);
                    mList = search.ToList();
                }
                else
                {

                    mList = search.Distinct().ToList();
                    var parentIds = mList.Select(a => a.p_parentid).ToList().Distinct();
                    var rootList = db.Set<t_module>().Where(m => parentIds.Contains(m.p_id)).OrderBy(a => a.p_sort).ThenBy(a => a.p_createtime);
                    if (rootList != null)
                    {
                        mList.AddRange(rootList);
                    }
                }
                return mList;
            }
        }
    }
}
