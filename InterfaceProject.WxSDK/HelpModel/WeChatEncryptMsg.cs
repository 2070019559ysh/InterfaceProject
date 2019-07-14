using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 微信服务器请求过来的加密消息
    /// </summary>
    public class WeChatEncryptMsg
    {
        /// <summary>
        /// 消息加密的签名
        /// </summary>
        public string MsgSignature { get; set; }
        /// <summary>
        /// 请求参数中的时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 请求参数中的随机字符串
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// 请求消息中的数据流
        /// </summary>
        public Stream Body { get; set; }
    }
}
