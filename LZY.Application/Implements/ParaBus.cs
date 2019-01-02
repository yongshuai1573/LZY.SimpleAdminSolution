using LZY.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Repository;
using LZY.Code;
using AspNetCorePage;
using System.Linq;
namespace LZY.Application.Implements
{
    public class ParaBus : IParaBus
    {
        private It_paratypeRepository _service;
        public ParaBus(It_paratypeRepository service)
        {
            _service = service;
        }

        #region 基本操作 可复制

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public t_paratype FindDto(object keyValue)
        {

            var dbModel = _service.FindEntity(keyValue);

            return dbModel;

        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <returns></returns>
        public Tuple<bool, string> SaveModel(t_paratype sourceModel)
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
        /// 删除参数
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

        public PagedList<t_paratype> GetPageList(string searchCode = "", int p = 1)
        {

            var query = _service.IQueryable();
            if (!string.IsNullOrWhiteSpace(searchCode))//查询 条件
            {
                var express = ExtLinq.True<t_paratype>();
                express = express.And(m => m.p_code.Contains(searchCode) || m.p_name.Contains(searchCode));
                query = query.Where(express);
            }
            return query.ToPagedList(p, PubliceParas.PageSize);

        }



    }

    #endregion




}
