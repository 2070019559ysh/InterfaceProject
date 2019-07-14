using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 微信公众号信息提供商接口
    /// </summary>
    public interface IWxPublicNumberProvider
    {
        /// <summary>
        /// 根据公众号Id获取具体的微信公众号信息
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>微信公众号信息</returns>
        WeChatConfig GetWeChatConfig(string idOrAppId);
    }
}
