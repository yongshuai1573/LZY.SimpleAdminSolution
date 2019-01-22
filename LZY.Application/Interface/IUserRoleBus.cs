using System;
using System.Collections.Generic;
using System.Text;
using LZY.Model;

namespace LZY.Application
{
    public interface IUserRoleBus : IBusBase<Model.t_user_role>
    {
        UserRoleViewModel GetUserRoleInfo(int id);
        Tuple<bool, string> AddRole(int roleId, int userId);
        Tuple<bool, string> CancleRole(int roleId, int userId);
    }
}
