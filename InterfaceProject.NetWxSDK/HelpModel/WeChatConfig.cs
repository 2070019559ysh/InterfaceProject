using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 微信配置信息
    /// </summary>
    public class WeChatConfig
    {
        /// <summary>
        /// 系统内部对微信公众号的唯一标识Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 应用服务号AppID
        /// </summary>
        public string AppID { get; private set; }
        
        /// <summary>
        /// AppSecret应用密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 微信服务器地址链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Token令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 消息加解密方式，是否启用加解密
        /// </summary>
        public bool EnCrypt { get; set; }

        /// <summary>
        /// 微信公众号的名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 如需让WxSDK轻松支持多公众号，请注入微信公众号信息提供服务
        /// </summary>
        public static IWxPublicNumberProvider WxPublicNumberProvider { get; set; }

        /// <summary>
        /// 内部初定的几个微信公众号，仅供测试
        /// </summary>
        private static List<WeChatConfig> weChatConfigList = new List<WeChatConfig>()
        {
            new WeChatConfig(){//远平给的测试号
                Id="itfc3476ksamhtx4bc5ed",
                AppName="远平给的测试版——公众号",
                AppID="wx521836f21e4ca8e5",
                AppSecret="3086fe68eef77f9a397cc41f67665e8f",
                Url="http://itfcweb.4kb.cn/wechat",
                Token="6BF0C6F39E514D6E2244B33E8B2A6C0B",
                EncodingAESKey="fdiYWLevGmXFiBM0Xb0FgvA2HAYym32SwRPQGG1Hydw",
                EnCrypt=false
            },
            new WeChatConfig(){//订阅号
                Id ="itfc0sgwj66vvj9cxkwv",
                AppName="测试版——订阅号",
                AppID="wxe164729ba1e7b5d1",
                AppSecret="bb4d91dc952a9e0c14cfe509968115a3",
                Url="http://tow070019559.imwork.net",
                Token="4AF0C6F39E514D6E2244B33E8B2A6C0B",
                EncodingAESKey="fdiYWLevGmXFiBM0Xb0FgvA2HAYym32SwRPQGG1Hydw",
                EnCrypt=false
            },
             new WeChatConfig(){//服务号
                 Id ="itfc22ugyadf8app32x9",
                 AppName="测试版——服务号",
                 AppID ="wx2f10f1cb149dd641",
                 AppSecret ="17be02231238b2aa5b403b47c412308b",
                 Url ="http://yshweb.wicp.net",
                 Token="5BF0C6F39E514D6E2244B33E8B2A6C0B",
                 EncodingAESKey="fdiYWLevGmXFiBM0Xb0FgvA2HAYym32SwRPQGG1Hydw",
                 EnCrypt=true
            }
        };

        /// <summary>
        /// 私有的构造函数，请用Init进行初始化
        /// </summary>
        private WeChatConfig()
        {
        }

        /// <summary>
        /// 初始化一个微信公众号信息，内部由微信公众号提供商提供支持
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns></returns>
        public static WeChatConfig Init(string idOrAppId)
        {
            WeChatConfig initWeChatConfig = null;
            if (WxPublicNumberProvider != null)
            {
                initWeChatConfig = WxPublicNumberProvider.GetWeChatConfig(idOrAppId);
            }
            else
            {
                initWeChatConfig = weChatConfigList.Where(weChat => weChat.Id == idOrAppId || weChat.AppID == idOrAppId).FirstOrDefault();
            }
            if (initWeChatConfig == null) initWeChatConfig = weChatConfigList.FirstOrDefault();
            return initWeChatConfig;
        }
    }
}
