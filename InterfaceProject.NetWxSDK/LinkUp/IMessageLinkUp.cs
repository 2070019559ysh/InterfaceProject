using InterfaceProject.NetWxSDK.HelpModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 微信消息管理处理接口
    /// </summary>
    public interface IMessageLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        MessageLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 从请求中提取微信消息推送的请求数据
        /// </summary>
        /// <param name="request">推送过来的请求</param>
        /// <returns>消息推送的请求数据</returns>
        MsgRequest GetMsgRequestData(WeChatEncryptMsg requestEncryptMsg);

        /// <summary>
        /// 根据请求确认是否要加密，处理出消息响应文本
        /// </summary>
        /// <param name="request">推送过来的请求</param>
        /// <param name="msgResponse">消息响应对象</param>
        /// <returns>消息响应文本</returns>
        string GetResponseText(MsgResponse msgResponse);

        /// <summary>
        /// 获取公众号的自动回复规则
        /// </summary>
        /// <returns>自动回复规则</returns>
        WeChatResult<JObject> GetAutoReplyRule();
    }
}
