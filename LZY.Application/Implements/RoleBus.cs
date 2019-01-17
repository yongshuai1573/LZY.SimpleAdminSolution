using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspNetCorePage;
using LZY.Code;
using LZY.Model;
using LZY.Repository;
namespace LZY.Application
{
    public class RoleBus : IRoleBus
    {
        private It_roleRepository _service = null;
        public RoleBus(It_roleRepository service)
        {
            _service = service;

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

        public t_role FindModel(object keyValue)
        {
            var dbModel = _service.FindEntity(keyValue);

            return dbModel;
        }

        public PagedList<t_role> GetPageList(string code, int p = 1)
        {
            var query = _service.IQueryable();
            var express = ExtLinq.True<t_role>();
            express = express.And(model => model.p_deleted == false);
            if (!string.IsNullOrWhiteSpace(code))//查询 条件
            {
                express = express.And(m => m.p_name.Contains(code) || m.p_attribute1.Contains(code));
                query = query.Where(express);
            }
            return query.ToPagedList(p, PubliceParas.PageSize);
        }

        public Tuple<bool, string> SaveModel(t_role sourceModel)
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
    }
}
