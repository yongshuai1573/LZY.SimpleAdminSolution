using System;
using System.Collections.Generic;
using System.Text;
using LZY.Model;
namespace LZY.Application
{
    public interface IRoleMenuBus:IBusBase<t_role_module>
    {
        Tuple<bool, string> SetMenuToRole(int roleid, int[] uid);
    }
}
