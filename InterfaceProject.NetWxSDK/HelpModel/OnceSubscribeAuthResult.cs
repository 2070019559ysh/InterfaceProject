using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 一次性订阅的授权结果
    /// </summary>
    public class OnceSubscribeAuthResult
    {
        /// <summary>
        /// 用户唯一标识，只在用户确认授权时才会带上
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 订阅消息模板ID
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 用户点击动作，”confirm”代表用户确认授权，”cancel”代表用户取消授权
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 0-10000的整形的订阅场景值
        /// </summary>
        public int Scene { get; set; }
    }
}
