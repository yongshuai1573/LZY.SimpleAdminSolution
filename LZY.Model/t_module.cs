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
    
    public partial class t_module
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public string p_tag { get; set; }
        public int p_parentid { get; set; }
        public string p_describe { get; set; }
        public string p_relativeurl { get; set; }
        public string p_icon { get; set; }
        public Nullable<System.DateTime> p_createtime { get; set; }
        public Nullable<int> p_createuserid { get; set; }
        public Nullable<System.DateTime> p_updatetime { get; set; }
        public Nullable<int> p_updateuserid { get; set; }
        public bool p_enabled { get; set; }
        public bool p_deleted { get; set; }
        public int p_sort { get; set; }
    }
}
