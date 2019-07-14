using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 微信公众号的客服消息基类
    /// </summary>
    public class CustomerServiceMsg
    {
        /// <summary>
        /// 发送给指定微信用户OpenId
        /// </summary>
        public string touser;
        /// <summary>
        /// 如果需要以某个客服帐号来发消息（在微信6.0.2及以上版本中显示自定义头像），则需在JSON数据包的后半部分加入customservice参数
        /// </summary>
        public KFAccount customservice;
    }

    /// <summary>
    /// 单独个客服帐号
    /// </summary>
    public class KFAccount
    {
        /// <summary>
        /// 客服帐号
        /// </summary>
        public string kf_account;
    }

    /// <summary>
    /// 文本客服消息
    /// </summary>
    public class ContentKFMsg:CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，文本为text
        /// </summary>
        [JsonProperty]
        public const string msgtype= "text";
        /// <summary>
        /// 封装个单独文本消息
        /// </summary>
        public Text_Msg text;
    }
    /// <summary>
    /// 单独个文本消息
    /// </summary>
    public class Text_Msg
    {
        /// <summary>
        /// <para>文本消息内容</para>
        /// <para>发送文本消息时，支持插入跳小程序的文字链</para>
        /// <para>文本内容&lt;a href="http://www.qq.com" data-miniprogram-appid="appid" data-miniprogram-path="pages/index/index"&gt;点击跳小程序&lt;/a&gt;</para>
        /// <para>说明：1.data-miniprogram-appid 项，填写小程序appid，则表示该链接跳小程序；</para>
        /// <para>2.data-miniprogram-path项，填写小程序路径，路径与app.json中保持一致，可带参数；</para>
        /// <para>3.对于不支持data-miniprogram-appid 项的客户端版本，如果有herf项，则仍然保持跳href中的网页链接；</para>
        /// <para>4.data-miniprogram-appid对应的小程序必须与公众号有绑定关系。</para>
        /// </summary>
        public string content;
    }

    /// <summary>
    /// 单独个微信媒体消息
    /// </summary>
    public class Media_Msg
    {
        /// <summary>
        /// 发送的图片/语音/图文消息（点击跳转到图文消息页）的媒体ID
        /// </summary>
        public string media_id;
    }

    /// <summary>
    /// 图片客服消息
    /// </summary>
    public class PictureKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，图片为image
        /// </summary>
        [JsonProperty]
        public const string msgtype = "image";
        /// <summary>
        /// 封装个单独文本消息
        /// </summary>
        public Media_Msg image;
    }

    /// <summary>
    /// 语音客服消息
    /// </summary>
    public class VoiceKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，语音为voice
        /// </summary>
        [JsonProperty]
        public const string msgtype = "voice";
        /// <summary>
        /// 封装个单独文本消息
        /// </summary>
        public Media_Msg voice;
    }

    /// <summary>
    /// 单独个微信视频消息
    /// </summary>
    public class Video_Msg
    {
        /// <summary>
        /// 发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID
        /// </summary>
        public string media_id;
        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id;
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string description;
    }

    /// <summary>
    /// 视频客服消息
    /// </summary>
    public class VideoKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，视频消息为video
        /// </summary>
        [JsonProperty]
        public const string msgtype = "voice";
        /// <summary>
        /// 封装视频消息
        /// </summary>
        public Video_Msg video;
    }

    /// <summary>
    /// 单独个微信音乐消息
    /// </summary>
    public class Music_Msg
    {
        /// <summary>
        /// 音乐消息的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 音乐消息的描述
        /// </summary>
        public string description;
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl;
        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl;
        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id;
    }

    /// <summary>
    /// 音乐客服消息
    /// </summary>
    public class MusicKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，音乐消息为music
        /// </summary>
        [JsonProperty]
        public const string msgtype = "music";
        /// <summary>
        /// 封装音乐消息
        /// </summary>
        public Music_Msg music;
    }

    /// <summary>
    /// 单独个图文消息
    /// </summary>
    public class Article_Msg
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string title;
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string description;
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl;
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string url;
    }

    /// <summary>
    /// 封装多个图文消息
    /// </summary>
    public class News_Article
    {
        public Article_Msg articles;
    }

    /// <summary>
    /// 发送图文消息（点击跳转到外链） 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应
    /// </summary>
    public class NewsKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，图文消息（点击跳转到外链）为news
        /// </summary>
        [JsonProperty]
        public const string msgtype = "news";
        /// <summary>
        /// 封装图文消息
        /// </summary>
        public News_Article news;
    }

    /// <summary>
    /// 发送图文消息（点击跳转到图文消息页面） 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应
    /// </summary>
    public class MpNewsKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，图文消息（点击跳转到图文消息页面）为mpnews
        /// </summary>
        [JsonProperty]
        public const string msgtype = "mpnews";
        /// <summary>
        /// 封装图文消息
        /// </summary>
        public Media_Msg mpnews;
    }

    /// <summary>
    /// 单独个微信卡券消息
    /// </summary>
    public class Card_Msg
    {
        /// <summary>
        /// 微信卡券的ID
        /// </summary>
        public string card_id;
    }

    /// <summary>
    /// 卡券客服消息
    /// </summary>
    public class CardKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，卡券为wxcard
        /// </summary>
        [JsonProperty]
        public const string msgtype = "wxcard";
        /// <summary>
        /// 封装微信卡券消息
        /// </summary>
        public Card_Msg wxcard;
    }

    /// <summary>
    /// 单独个微信小程序消息
    /// </summary>
    public class MiniProgrampage_Msg
    {
        /// <summary>
        /// 小程序卡片的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 小程序的appid，要求小程序的appid需要与公众号有关联关系
        /// </summary>
        public string appid;
        /// <summary>
        /// 小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar
        /// </summary>
        public string pagepath;
        /// <summary>
        /// 小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416
        /// </summary>
        public string thumb_media_id;
    }

    /// <summary>
    /// 微信小程序客服消息（要求小程序与公众号已关联）
    /// </summary>
    public class MiniProgrampageKFMsg : CustomerServiceMsg
    {
        /// <summary>
        /// 消息类型，小程序为miniprogrampage
        /// </summary>
        [JsonProperty]
        public const string msgtype = "miniprogrampage";
        /// <summary>
        /// 封装小程序卡片消息
        /// </summary>
        public MiniProgrampage_Msg miniprogrampage;
    }

    /// <summary>
    /// 微信消息的命令类型
    /// </summary>
    public enum WxMsgCommand
    {
        /// <summary>
        /// 取消对用户的”正在输入"状态
        /// </summary>
        CancelTyping = 0,
        /// <summary>
        /// 对用户下发“正在输入"状态
        /// </summary>
        Typing = 1,
    }
}
