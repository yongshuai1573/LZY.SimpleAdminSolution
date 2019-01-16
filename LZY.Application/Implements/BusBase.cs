using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCorePage;

namespace LZY.Application
{

    //这里把通用的方法，统一完成，减少代码量
    public class BusBase<T> : IBusBase<T>
    {

        public BusBase()
        {

        }

        public Tuple<bool, string> Deleted(int id)
        {
            throw new NotImplementedException();
        }

        public T FindModel(object keyValue)
        {
            throw new NotImplementedException();
        }

        public PagedList<T> GetPageList(string code, int p = 1)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string> SaveModel(T sourceModel)
        {
            throw new NotImplementedException();
        }

    }
}
