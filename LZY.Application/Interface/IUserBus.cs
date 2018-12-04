using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZY.Application
{
    public interface IUserBus
    {

        int getCount();
        Tuple<bool, string, LZY.Code.OperatorModel> userLogin(string userName, string passWord);
    }
}
