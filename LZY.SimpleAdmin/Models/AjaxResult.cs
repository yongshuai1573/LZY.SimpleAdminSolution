using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LZY.SimpleAdmin
{
    public class AjaxResult
    {
        public bool success { get; set; }
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public object state { get; set; }
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object data { get; set; }

    }
}