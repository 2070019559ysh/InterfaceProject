using InterfaceProject.Tool;
using InterfaceProject.Tool.Cache;
using InterfaceProject.Tool.HelpModel;
using InterfaceProject.Tool.Log;
using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 一次性订阅接入实现
    /// </summary>
    public class OnceSubscribeLinkUp: IOnceSubscribeLinkUp
    {
        /// <summary>
        /// 当前处理的微信公众号信息
        /// </summary>
        private WeChatConfig weChatConfig;
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;
        /// <summary>
        /// Redis缓存帮助器
        /// </summary>
        private RedisHelper redisHelper = new RedisHelper();

        /// <summary>
        /// 实例化微信一次性订阅处理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat">注入依赖的微信对接处理</param>
        public OnceSubscribeLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信一次性订阅实例本身</returns>
        public OnceSubscribeLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            weChatConfig = connect.WeChatConfig;
            return this;
        }

        /// <summary>
        /// 获取需要用户同意授权的访问地址
        /// </summary>
        /// <param name="scene">可以填0-10000的整形值，用来标识订阅场景值</param>
        /// <param name="templateId">订阅消息模板ID，与模板消息的ID不一样</param>
        /// <param name="reserved">用于保持请求和回调的状态，授权请后原样带回，可填a-zA-Z0-9的参数值，最多128字节</param>
        /// <returns>订阅一次消息授权的访问地址</returns>
        public string GetAuthorizedUrl(int scene,string templateId,string reserved)
        {
            string url = "https://mp.weixin.qq.com/mp/subscribemsg?action=get_confirm&appid={0}&scene={1}&template_id={2}&redirect_url={3}&reserved={4}#wechat_redirect";
            Uri wechatUri = new Uri(weChatConfig.Url);
            string hostUrl = WebUtility.UrlEncode(wechatUri.Scheme + "://" + wechatUri.Host);
            string reservedStr = WebUtility.UrlEncode(reserved);
            string authorizedUrl = string.Format(url, weChatConfig.AppID, scene, templateId, hostUrl, reservedStr);
            return authorizedUrl;
        }

        /// <summary>
        /// 设置（保存）用户同意的授权结果，结果暂存65天
        /// </summary>
        /// <param name="authResult">用户同意的授权结果</param>
        public void SetAuthorizedResult(OnceSubscribeAuthResult authResult)
        {
            if (authResult == null) return;
            if (string.IsNullOrWhiteSpace(authResult.OpenId)) return;
            string redisKey = string.Format(RedisKeyPrefix.WECHAT_ONCE_SUBSCRIBE, weChatConfig.AppID 
                + "_" + authResult.OpenId + "_" + authResult.Scene);
            redisHelper.StringSet(redisKey, authResult, TimeSpan.FromDays(65));
        }

        /// <summary>
        /// 判断指定微信OpenId用户是否已同意授权某场景的一次性订阅
        /// </summary>
        /// <param name="openId">微信用户OpenId</param>
        /// <param name="scene">指定场景值</param>
        /// <returns>暂存区内用户是否已同意授权</returns>
        public bool IsConfirmAuth(string openId,int scene)
        {
            string redisKey = string.Format(RedisKeyPrefix.WECHAT_ONCE_SUBSCRIBE, weChatConfig.AppID
                + "_" + openId + "_" + scene);
            OnceSubscribeAuthResult authResult = redisHelper.StringGet<OnceSubscribeAuthResult>(redisKey);
            if (authResult == null) return false;
            if ("confirm".Equals(authResult.Action))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 推送订阅模板消息给到授权微信用户
        /// </summary>
        /// <param name="subscribeMsg">订阅模板消息</param>
        /// <returns>订阅模板消息推送结果</returns>
        public WeChatResult SendSubscribe(OnceSubscribeMsg subscribeMsg)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/template/subscribe?access_token={accessToken}";
            string resultData = SimulateRequest.HttpPost(url, subscribeMsg);
            WeChatResult weChatResult = new WeChatResult(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"推送订阅模板消息给到授权微信用户SendSubscribe，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }
    }
}
