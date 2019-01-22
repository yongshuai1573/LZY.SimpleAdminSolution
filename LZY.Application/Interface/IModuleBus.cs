using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LZY.Model;
using LZY.Model.ViewModels;

namespace LZY.Application
{
    public interface IModuleBus : IBusBase<t_module>
    {
        List<LZY.Model.ViewModels.MenuLayerViewModel> GetLayerList();
        string GetMenuLayerStr(List<MenuLayerViewModel> models);
        List<t_module> GetRootMenus();
        List<t_module> FindList();
        string GetRoleMenuLayerStr(List<MenuLayerViewModel> models, int roleId);
    }
}
