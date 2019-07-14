using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterfaceProject.BLL.Handle
{
    /// <summary>
    /// 处理微信对接相关业务的接口声明
    /// </summary>
    public interface IWeChatHandle
    {
        /// <summary>
        /// 初始化处理的公众号参数
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>初始化处理后的对象</returns>
        WeChatHandle Initialize(string idOrAppId);

        /// <summary>
        /// 执行微信消息处理
        /// </summary>
        /// <param name="signature">微信消息签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="body">请求参数主体</param>
        /// <returns>响应结果的Xml</returns>
        string Execute(string signature, string timestamp, string nonce, Stream body);
    }
}
