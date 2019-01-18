using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZY.Model
{
   public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            this.HasRoles = new List<t_role>();
            this.NoRoles = new List<t_role>();
        }
        public List<t_role> HasRoles { get; set; }
        public List<t_role> NoRoles { get; set; }
    }
}
