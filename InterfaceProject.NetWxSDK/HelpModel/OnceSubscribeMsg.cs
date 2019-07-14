using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 微信一次性订阅消息
    /// </summary>
    public class OnceSubscribeMsg
    {
        /// <summary>
        /// 接收消息的用户openid
        /// </summary>
        public string touser;
        /// <summary>
        /// 订阅消息模板ID
        /// </summary>
        public string template_id;
        /// <summary>
        /// 点击消息跳转的链接，需要有ICP备案
        /// </summary>
        public string url;
        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public MiniProgramParam miniprogram;
        /// <summary>
        /// 订阅场景值
        /// </summary>
        public int scene;
        /// <summary>
        /// 消息标题，15字以内
        /// </summary>
        public string title;
        /// <summary>
        /// 消息正文，value为消息内容文本（200字以内），没有固定格式，可用\n换行，color为整段消息内容的字体颜色（目前仅支持整段消息为一种颜色）
        /// </summary>
        public ContentData data;
    }
    /// <summary>
    /// 订阅内容数据类
    /// </summary>
    public class ContentData
    {
        /// <summary>
        /// 订阅消息内容
        /// </summary>
        public TKeyWord content;
    }
}
