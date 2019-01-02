using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Application.Interface;
namespace LZY.Application.Implements
{

    //这里把通用的方法，统一完成，减少代码量
    public class BusBase<T> : IBusBase<T>
    {

        public BusBase()
        {

        }

        public T FindDto(object keyValue)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string> SaveModel(T sourceModel)
        {
            throw new NotImplementedException();
        }

    }
}
