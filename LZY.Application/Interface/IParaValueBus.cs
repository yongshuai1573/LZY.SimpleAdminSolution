using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Model;
namespace LZY.Application
{
    public interface IParaValueBus : IBusBase<t_para_values>
    {
        List<t_para_values> GetListByTypeId(int id);
    }
}
