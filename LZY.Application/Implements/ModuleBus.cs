using LZY.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Repository;
using LZY.Code;
using AspNetCorePage;
using System.Linq;
using LZY.Model.ViewModels;

namespace LZY.Application
{
    public class ModuleBus : IModuleBus
    {
        private It_moduleRepository _service;
        private It_role_moduleRepository _roleMenuService;
        public ModuleBus(It_moduleRepository service, It_role_moduleRepository roleMenuRepositor)
        {
            _service = service;
            _roleMenuService = roleMenuRepositor;
        }

        #region 基本操作 可复制

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public t_module FindModel(object keyValue)
        {

            var dbModel = _service.FindEntity(keyValue);

            return dbModel;

        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <returns></returns>
        public Tuple<bool, string> SaveModel(t_module sourceModel)
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        public PagedList<t_module> GetPageList(string code, int p = 1)
        {
            var query = _service.IQueryable();
            var express = ExtLinq.True<t_module>();
            express = express.And(model => model.p_deleted == false);
            if (!string.IsNullOrWhiteSpace(code))//查询 条件
            {
                express = express.And(m => m.p_name.Contains(code) || m.p_describe.Contains(code));
                query = query.Where(express);
            }
            return query.ToPagedList(p, PubliceParas.PageSize);
        }
        /// <summary>
        /// 递归获取列表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>

        public void GetChildren(List<t_module> source, List<MenuLayerViewModel> target)
        {

            foreach (var item in target)
            {
                var chidList = source.FindAll(a => a.p_parentid == item.id);
                if (chidList?.Count > 0)
                {
                    var returnList = chidList.ConvertAll(a => new MenuLayerViewModel { id = a.p_id, pid = a.p_parentid, text = a.p_name, icon = a.p_icon, url = a.p_relativeurl, tag = a.p_tag, sort = a.p_sort });//全部转换到
                    item.childList = returnList.OrderBy(a => a.sort).ToList();
                    GetChildren(source, returnList);
                }
            }

        }

        public List<MenuLayerViewModel> GetLayerList()
        {
            List<MenuLayerViewModel> returnList = new List<MenuLayerViewModel>();
            var list = FindList();
            if (list?.Count > 0)
            {

                var rootMenuList = list.FindAll(a => a.p_parentid == 0).OrderBy(a => a.p_sort).ToList();//获取首级目录              
                returnList = rootMenuList.ConvertAll(a => new MenuLayerViewModel { id = a.p_id, pid = a.p_parentid, text = a.p_name, icon = a.p_icon, url = a.p_relativeurl, tag = a.p_tag, sort = a.p_sort });//全部转换到
                GetChildren(list, returnList);//层级获取完毕
            }

            return returnList;
        }

        public string GetMenuLayerStr(List<MenuLayerViewModel> models)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var item in models)
            {
                //\"<span class=\"button_z\"><button type=\"button\" class=\"btn btn btn-info btn-xs\" onclick=\"edit({item.id});\">编辑</button>&nbsp;&nbsp;&nbsp;&nbsp;<button type=\"button\" class=\"btn btn-danger btn-xs\" onclick=\"del({item.id});\">删除</button></span>\"
                sb.Append("{");
                sb.AppendFormat($"id:{item.id},text:\"{item.text}\",icon:\"{item.icon}\",after_html:\'<span class=\"pull-right\"><button type=\"button\" class=\"btn btn btn-default btn-xs\" onclick=\"edit({item.id});\">编辑</button>&nbsp;&nbsp;&nbsp;&nbsp;<button type=\"button\" class=\"btn btn-danger btn-xs\" onclick=\"del({item.id});\">删除</button></span>\' ");
                if (item.HasChildren)
                {
                    sb.AppendFormat($",nodes:");
                    sb.Append(GetMenuLayerStr(item.childList));
                }
                sb.Append("}");
                if (models.IndexOf(item) < models.Count - 1)
                    sb.Append(",");
            }

            sb.Append("]");
            return sb.ToString();
        }

        public List<t_module> GetRootMenus()
        {
            var dbModel = _service.IQueryable().Where(a => a.p_deleted == false && a.p_parentid == 0).ToList();

            if (dbModel != null)
            {
                return dbModel;
            }
            else
                return null;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private string GetMenuLayerStr(List<MenuLayerViewModel> models, List<t_role_module> roleMenuData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var item in models)
            {
                //\"<span class=\"button_z\"><button type=\"button\" class=\"btn btn btn-info btn-xs\" onclick=\"edit({item.id});\">编辑</button>&nbsp;&nbsp;&nbsp;&nbsp;<button type=\"button\" class=\"btn btn-danger btn-xs\" onclick=\"del({item.id});\">删除</button></span>\"
                sb.Append("{");
                sb.AppendFormat($"id:{item.id},text:\"{item.text}\",icon:\"{item.icon}\" ");
                if (item.HasChildren)
                {
                    sb.Append(",nodes:");
                    sb.Append(GetMenuLayerStr(item.childList, roleMenuData));
                }
                if (roleMenuData?.Count > 0)
                {
                    sb.Append(",state:{");
                    if (roleMenuData.Exists(a => a.p_moduleid == item.id))
                    {
                        sb.Append(" checked:true");
                    }
                    else
                    {
                        sb.Append(" checked:false");
                    }
                    sb.Append("}");

                }

                sb.Append("}");

                if (models.IndexOf(item) < models.Count - 1)
                    sb.Append(",");
            }

            sb.Append("]");
            return sb.ToString();

        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public string GetRoleMenuLayerStr(List<MenuLayerViewModel> models, int roleId)
        {
            var result = _roleMenuService.IQueryable().Where(a => a.p_roleid == roleId).ToList();


            return GetMenuLayerStr(models, result);

        }
      
        public List<t_module> FindList()
        {
            return _service.IQueryable().Where(a => a.p_deleted == false).OrderBy(a => a.p_sort).ToList();
        }
    }

    #endregion




}
