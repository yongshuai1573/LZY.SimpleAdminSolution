//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LZY.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class t_paratype
    {
        public int p_id { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string p_name { get; set; }
        [MaxLength(100)]
        public string p_describ { get; set; }

        public Nullable<int> p_sort { get; set; }
        public bool p_deleted { get; set; }
        public Nullable<System.DateTime> p_createtime { get; set; }
        public Nullable<int> p_createuserid { get; set; }
        public Nullable<System.DateTime> p_updatetime { get; set; }
        public Nullable<int> p_updateuserid { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string p_code { get; set; }
    }
}
