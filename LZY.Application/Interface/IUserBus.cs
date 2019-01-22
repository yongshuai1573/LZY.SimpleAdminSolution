using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LZY.Model;
using LZY.Model.ViewModels;

namespace LZY.Application
{
    public interface IUserBus : IBusBase<t_user>
    {      
        Tuple<bool, string, LZY.Code.OperatorModel> userLogin(string userName, string passWord);
        Tuple<bool, string> EnabledMatch(int id);
        Tuple<bool, string> ChangePwdSave(ChangePwdViewModel model);
    }
}
