using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 一次性订阅声明接口
    /// </summary>
    public interface IOnceSubscribeLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信一次性订阅实例本身</returns>
        OnceSubscribeLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 获取需要用户同意授权的访问地址
        /// </summary>
        /// <param name="scene">可以填0-10000的整形值，用来标识订阅场景值</param>
        /// <param name="templateId">订阅消息模板ID，与模板消息的ID不一样</param>
        /// <param name="reserved">用于保持请求和回调的状态，授权请后原样带回</param>
        /// <returns>订阅一次消息授权的访问地址</returns>
        string GetAuthorizedUrl(int scene, string templateId, string reserved);

        /// <summary>
        /// 设置（保存）用户同意的授权结果，结果暂存65天
        /// </summary>
        /// <param name="authResult">用户同意的授权结果</param>
        void SetAuthorizedResult(OnceSubscribeAuthResult authResult);

        /// <summary>
        /// 判断指定微信OpenId用户是否已同意授权某场景的一次性订阅
        /// </summary>
        /// <param name="openId">微信用户OpenId</param>
        /// <param name="scene">指定场景值</param>
        /// <returns>暂存区内用户是否已同意授权</returns>
        bool IsConfirmAuth(string openId, int scene);

        /// <summary>
        /// 推送订阅模板消息给到授权微信用户
        /// </summary>
        /// <param name="subscribeMsg">订阅模板消息</param>
        /// <returns>订阅模板消息推送结果</returns>
        WeChatResult SendSubscribe(OnceSubscribeMsg subscribeMsg);
    }
}
