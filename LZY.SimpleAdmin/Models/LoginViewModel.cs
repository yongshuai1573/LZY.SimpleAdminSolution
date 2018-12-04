using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LZY.SimpleAdmin.Models
{
    public class LoginViewModel
    {
        [DisplayName("用户名")]
        [Required, StringLength(maximumLength: 20, MinimumLength = 3)]
        public string UserName { get; set; }
        [DisplayName("密码")]

        [Required, StringLength(maximumLength: 16, MinimumLength = 6)]
        public string PassWord { get; set; }

        public string RememberMe { get; set; }
    }
}
