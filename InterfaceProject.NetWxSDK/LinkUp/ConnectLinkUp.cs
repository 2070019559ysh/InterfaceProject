using System;
using System.Reflection;
using System.Threading;
using InterfaceProject.NetTool;
using InterfaceProject.NetTool.Cache;
using InterfaceProject.NetTool.HelpModel;
using InterfaceProject.NetTool.Log;
using InterfaceProject.NetWxSDK.HelpModel;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 微信接入类
    /// </summary>
    public class ConnectLinkUp:IConnectLinkUp
    {
        /// <summary>
        /// 当前处理的微信公众号信息
        /// </summary>
        public WeChatConfig WeChatConfig { get; private set; }
        /// <summary>
        /// Redis访问
        /// </summary>
        private readonly RedisHelper redisHelper;

        /// <summary>
        /// 实例化微信接入实例，后期需Initialize初始化
        /// </summary>
        public ConnectLinkUp()
        {
            redisHelper = new RedisHelper();
        }

        /// <summary>
        /// 实例化微信接入实例，并指明操作的公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        public ConnectLinkUp(string idOrAppId)
        {
            WeChatConfig = WeChatConfig.Init(idOrAppId);
            redisHelper = new RedisHelper();
        }

        /// <summary>
        /// 初始化或修改微信接入的内部配置参数
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        public ConnectLinkUp Initialize(string idOrAppId)
        {
            WeChatConfig = WeChatConfig.Init(idOrAppId);
            return this;
        }

        /// <summary>
        /// 验证请求中的签名，确认请求是否来自微信
        /// </summary>
        /// <param name="request">当前请求对象</param>
        /// <returns>请求是否来自微信</returns>
        public bool CheckSignature(string signature,string timestamp,string nonce)
        {
            //string signature = request.QueryString["signature"];
            //string timestamp = request.QueryString["timestamp"];
            //string nonce = request.QueryString["nonce"];
            if (String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(timestamp) || String.IsNullOrEmpty(nonce))
            {
                return false;
            }
            if (WeChatConfig == null)
                throw new NullReferenceException("请使用构造ConnectLinkUp(idOrAppId)，或初始化Initialize(idOrAppId)");
            string token = WeChatConfig.Token;
            string[] strs = { token, timestamp, nonce };
            Array.Sort(strs);
            string joinStr = string.Join("", strs);
            //List<string> strList = new List<string>() { token, timestamp, nonce };
            //strList.Sort();
            //string joinStr = String.Empty;
            //strList.ForEach(str => joinStr += str);
            string sha1Str = EncryptHelper.HashSHA1(joinStr);//joinStr.SHA1Encrypt();
            return sha1Str.Equals(signature) ? true : false;
        }

        /// <summary>
        /// 获取公众号的AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        public string GetAccessToken()
        {
            if (WeChatConfig == null)
                throw new NullReferenceException("请使用构造ConnectLinkUp(idOrAppId)，或初始化Initialize(idOrAppId)");
            string accessToken = String.Empty;
            //先从数据库获取Access_Token，如果不存在或已过期，则重新跟微信拿Access_Token
            string appId = WeChatConfig.AppID;
            string accessTokenKey = string.Format(RedisKeyPrefix.WECHAT_ACCESS_TOKEN, appId);
            string updateTokenKey = string.Format(RedisKeyPrefix.WECHAT_TOKEN_CONCURRENT, appId);
            do
            {
                if ("Concurrent".Equals(accessToken))
                {
                    SystemLogHelper.Info(MethodBase.GetCurrentMethod(), "GetAccessToken(),已有线程直接去微信获取AccessToken，在此等待");
                    Thread.Sleep(500);//发生并发的线程需要回到这里，等待单线程更新完成
                }
                AccessToken wechatAccessToken = redisHelper.StringGet<AccessToken>(accessTokenKey);
                if (wechatAccessToken != null)
                {
                    accessToken = wechatAccessToken.access_token;
                }
                else//Redis里面的Token已经失效，需要单线程去更新Token
                {
                    accessToken = ConcurrentControl.SingleUserFunc(updateTokenKey, "Concurrent", () =>
                    {
                        SystemLogHelper.Info(MethodBase.GetCurrentMethod(), "获取公众号的AccessToken,GetAccessToken(),直接去微信获取AccessToken");
                        return GetWeChatAccessToken();
                    });
                }
            } while ("Concurrent".Equals(accessToken));
            return accessToken;
        }

        /// <summary>
        /// 直接去微信获取AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        private string GetWeChatAccessToken()
        {
            if (WeChatConfig == null)
                throw new NullReferenceException("请使用构造ConnectLinkUp(idOrAppId)，或初始化Initialize(idOrAppId)");
            string accessToken = String.Empty;
            string appId = WeChatConfig.AppID;
            string secret = WeChatConfig.AppSecret;
            string url = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult<AccessToken> accessTokenResult = new WeChatResult<AccessToken>(resultStr);
            if (accessTokenResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"直接去微信获取AccessToken,GetWeChatAccessToken()，{accessTokenResult}");
            }
            else
            {
                accessToken = accessTokenResult.resultData.access_token;
                string accessTokenKey = string.Format(RedisKeyPrefix.WECHAT_ACCESS_TOKEN, appId);
                int validTime = accessTokenResult.resultData.expires_in - 600;//有效时间提前10分钟
                redisHelper.StringSet(accessTokenKey, accessTokenResult.resultData, TimeSpan.FromSeconds(validTime));
            }
            return accessToken;
        }

        /// <summary>
        /// 获取微信服务器IP地址（公众号基于安全等考虑，需要获知微信服务器的IP地址列表）
        /// </summary>
        /// <returns>微信服务器的IP地址列表</returns>
        public WeChatResult<WeChatIPInfo> GetWeChatServerIP()
        {
            //如果公众号基于安全等考虑，需要获知微信服务器的IP地址列表，以便进行相关限制，可以通过该接口获得微信服务器IP地址列表或者IP网段信息
            string url = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";//http请求方式: GET
            string accessToken = this.GetAccessToken();
            url = String.Format(url, accessToken);
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult<WeChatIPInfo> serviceIPResult = new WeChatResult<WeChatIPInfo>(resultStr);
            if (serviceIPResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取微信服务器IP地址GetWeChatServerIP()，微信服务报错：{serviceIPResult}");
            }
            return serviceIPResult;
        }

        /// <summary>
        /// 对公众号的所有api调用次数进行清零，每个有接口调用限额的接口都进行清零操作
        /// </summary>
        /// <returns>清零结果</returns>
        public WeChatResult ApiClearQuota()
        {
            //每月共10次清零操作机会，清零生效一次即用掉一次机会（10次包括了平台上的清零和调用接口API的清零）
            string url = "https://api.weixin.qq.com/cgi-bin/clear_quota?access_token={0}";
            string accessToken = this.GetAccessToken();
            url = String.Format(url, accessToken);
            string resultData = SimulateRequest.HttpPost(url, new { appid = WeChatConfig.AppID });
            WeChatResult weChatResult = new WeChatResult(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取微信服务器IP地址GetWeChatServerIP()，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }
    }
}
