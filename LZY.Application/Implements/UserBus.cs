using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LZY.Repository;

namespace LZY.Application
{
    public class UserBus:IUserBus
    {
        private It_userRepository _service;
        public UserBus(It_userRepository service)
        {
            this._service = service;
        }
        public int getCount()
        {
            return _service.IQueryable().Count();
        }
    }
}
