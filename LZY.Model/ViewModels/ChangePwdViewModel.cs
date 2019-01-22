using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZY.Model.ViewModels
{
    public class ChangePwdViewModel
    {
        [Required]
        [DisplayName("原密码")]
        public string OldPwd { get; set; }
        [Required, StringLength(maximumLength: 20, MinimumLength = 6)]
        [Compare("NewPwdConfirm")]
        [DisplayName("新密码")]

        public string NewPwd { get; set; }
        [DisplayName("确认新密码")]

        [Required, StringLength(maximumLength: 20, MinimumLength = 6)]
        public string NewPwdConfirm { get; set; }
    }
}
