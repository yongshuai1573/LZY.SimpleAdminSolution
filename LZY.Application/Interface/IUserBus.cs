using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LZY.Model;

namespace LZY.Application
{
    public interface IUserBus : IBusBase<t_user>
    {      
        Tuple<bool, string, LZY.Code.OperatorModel> userLogin(string userName, string passWord);
        Tuple<bool, string> EnabledMatch(int id);
    }
}
