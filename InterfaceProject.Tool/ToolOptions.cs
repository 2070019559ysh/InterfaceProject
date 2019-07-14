using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// 整个工具类库的参数选项
    /// </summary>
    public class ToolOptions
    {
        /// <summary>
        /// 系统版本号
        /// </summary>
        public static string Version { get; set; } = "1.0.0";
        
        /// <summary>
        /// 系统发邮件服务地址
        /// </summary>
        public static string EmailServer { get; set; } = "smtp.163.com";
        
        /// <summary>
        /// 系统发邮件服务的邮箱地址
        /// </summary>
        public static string EmailFromAddress { get; set; } = "tow070019559@163.com";
        
        /// <summary>
        /// 系统发邮件服务的密码
        /// </summary>
        public static string EmailPassword { get; set; } = "503104plkj";
        
        /// <summary>
        /// 系统发短信服务的账号
        /// </summary>
        public static string SMSUId { get; set; } = "2070019559ysh";
        
        /// <summary>
        /// 系统发短信服务的密码
        /// </summary>
        public static string SMSPassword { get; set; } = "4bef75049eadb5c29cde";
    }
}
