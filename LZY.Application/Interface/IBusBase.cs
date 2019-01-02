using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LZY.Application
{
    public interface IBusBase<T>
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        T FindDto(object keyValue);
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <returns></returns>
        Tuple<bool, string> SaveModel(T sourceModel);


        AspNetCorePage.PagedList<T> GetPageList(string code, int p = 1);



    }
}
