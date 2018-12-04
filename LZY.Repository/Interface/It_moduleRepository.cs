using System.Collections.Generic;
using LZY.Code;
using LZY.Data;
using LZY.Model;
namespace LZY.Repository
{
    public interface It_moduleRepository : IRepositoryBase<t_module>
    {
        List<t_module> GetMenuList(OperatorModel userInfo);
    }
}