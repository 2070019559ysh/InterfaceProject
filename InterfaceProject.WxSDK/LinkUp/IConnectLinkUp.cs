using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 微信接入的接口
    /// </summary>
    public interface IConnectLinkUp
    {
        /// <summary>
        /// 当前处理的微信公众号信息
        /// </summary>
        WeChatConfig WeChatConfig { get; }

        /// <summary>
        /// 初始化或修改微信接入的内部配置参数
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        ConnectLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 验证请求中的签名，确认请求是否来自微信
        /// </summary>
        /// <param name="request">当前请求对象</param>
        /// <returns>请求是否来自微信</returns>
        bool CheckSignature(string signature, string timestamp, string nonce);

        /// <summary>
        /// 获取公众号的AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        string GetAccessToken();

        /// <summary>
        /// 获取微信服务器IP地址（公众号基于安全等考虑，需要获知微信服务器的IP地址列表）
        /// </summary>
        /// <returns>微信服务器的IP地址列表</returns>
        WeChatResult<WeChatIPInfo> GetWeChatServerIP();

        /// <summary>
        /// 对公众号的所有api调用次数进行清零，每个有接口调用限额的接口都进行清零操作
        /// </summary>
        /// <returns>清零结果</returns>
        WeChatResult ApiClearQuota();
    }
}
