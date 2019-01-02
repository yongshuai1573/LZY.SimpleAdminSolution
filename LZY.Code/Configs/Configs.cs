using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.Code
{
    public class Configs
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static T GetValue<T>(string key)
        {
            try
            {
                var str = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrWhiteSpace(str))
                {


                    str = str.Trim();
                    return (T)Convert.ChangeType(str, typeof(T));
                }
            }
            catch { }
            return default(T);



        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            throw new Exception("还未实现SetValue：根据Key修改Value");
        }
    }
}
