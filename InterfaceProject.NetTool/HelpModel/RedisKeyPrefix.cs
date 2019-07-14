using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetTool.HelpModel
{
    /// <summary>
    /// 定义系统中使用到的所有Redis前缀常量
    /// </summary>
    public class RedisKeyPrefix
    {
        /// <summary>
        /// 系统日志RedisKey
        /// </summary>
        public const string SYSTEM_LOG = "SystemLog";
        /// <summary>
        /// 系统日志保存失败的补救RedisKey
        /// </summary>
        public const string SYSTEM_LOG_REMEDY = "SystemLog_Remedy";
        /// <summary>
        /// 记录系统Http请求日志RedisKey
        /// </summary>
        public const string REQUEST_LOG = "RequestLog";
        /// <summary>
        /// 记录系统Http请求日志保存失败的补救RedisKey
        /// </summary>
        public const string REQUEST_LOG_REMEDY = "RequestLog_Remedy";
        /// <summary>
        /// 系统配置信息_Key
        /// </summary>
        public const string SYSTEM_CONFIG = "SystemConfig_{0}";
        /// <summary>
        /// 保存笑话防止并发_{第一个笑话的Id}
        /// </summary>
        public const string SAVE_JOKE_CONCURRENCY = "SaveJokeConcurrency_{0}";

        #region 微信公众平台

        /// <summary>
        /// 微信公众号的AccessToken凭证{AppId}
        /// </summary>
        public const string WECHAT_ACCESS_TOKEN = "WeChatAccessToken_{0}";
        /// <summary>
        /// 微信公众号开发者接入就续
        /// </summary>
        public const string WECHAT_CONNECT_READY = "WeChatConnectReady";
        /// <summary>
        /// 微信公众号开发者接入就续的控制并发
        /// </summary>
        public const string WECHAT_READY_CONCURRENT = "WeChatReadyConcurrent";
        /// <summary>
        /// 微信公众号的AccessToken凭证定时更新的控制并发{AppId}
        /// </summary>
        public const string WECHAT_TOKEN_CONCURRENT = "WeChatTokenConcurrent_{0}";
        /// <summary>
        /// 微信公众号的一次性订阅已授权的结果信息{AppId_OpenId_Scene}
        /// </summary>
        public const string WECHAT_ONCE_SUBSCRIBE = "WeChatOnceSubscribe_{0}";
        #endregion
    }
}
