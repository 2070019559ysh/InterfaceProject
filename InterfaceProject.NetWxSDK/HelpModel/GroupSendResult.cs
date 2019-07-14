using InterfaceProject.NetTool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 群发消息预览使用的账号类型
    /// </summary>
    public enum PreviewAccount
    {
        /// <summary>
        /// 微信账号
        /// </summary>
        WxAccount,
        /// <summary>
        /// 用户的OpenId
        /// </summary>
        OpenId
    }
    /// <summary>
    /// 微信群发后的返回结果，请注意：在返回成功时，意味着群发任务提交成功，并不意味着此时群发已经结束，仍有可能在后续的发送过程中出现异常情况导致用户未收到消息，如消息有时会进行审核、服务器不稳定等。
    /// </summary>
    public class GroupSendResult
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        public int msg_id;
        /// <summary>
        /// 消息的数据ID，该字段只有在群发图文消息时，才会出现。可以用于在图文分析数据接口中，获取到对应的图文消息的数据，是图文分析数据接口中的msgid字段中的前半部分
        /// </summary>
        public int msg_data_id;
    }

    /// <summary>
    /// 群发消息发送状态的查询结果
    /// </summary>
    public class GroupSendStatusResult
    {
        /// <summary>
        /// 群发消息后返回的消息id
        /// </summary>
        public int msg_id;
        /// <summary>
        /// 消息发送后的状态，SEND_SUCCESS表示发送成功，SENDING表示发送中，SEND_FAIL表示发送失败，DELETE表示已删除
        /// </summary>
        public string msg_status;
    }

    /// <summary>
    /// 微信响应的URL
    /// </summary>
    public class WeChatURL
    {
        public string url;
    }

    /// <summary>
    /// 上传图文消息素材的结果
    /// </summary>
    public class UploadNewsResult
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息（news）
        /// </summary>
        public string type;
        /// <summary>
        /// 媒体文件/图文消息上传后获取的唯一标识
        /// </summary>
        public string media_id;
        /// <summary>
        /// 媒体文件上传时间的时间戳
        /// </summary>
        public int create_at;
        /// <summary>
        /// 媒体文件上传时间
        /// </summary>
        public DateTime createAt
        {
            get
            {
                DateTime createDateTime =TimeHelper.GetDateTime(create_at);
                return createDateTime;
            }
        }
    }

    /// <summary>
    /// 封装带有图文Url的图文素材信息
    /// </summary>
    public class ArticleItemsInfo
    {
        /// <summary>
        /// 记录1至8条带有图文Url的图文素材信息
        /// </summary>
        public List<ArticleMaterialWithURL> news_item = new List<ArticleMaterialWithURL>();
    }

    /// <summary>
    /// 整体封装图文消息
    /// </summary>
    public class ArticleNews
    {
        /// <summary>
        /// 图文消息，一个图文消息支持1到8条图文
        /// </summary>
        public List<ArticleMaterial> articles = new List<ArticleMaterial>();
    }

    /// <summary>
    /// 图文消息的文章素材
    /// </summary>
    public class ArticleMaterial
    {
        /// <summary>
        /// 图文消息缩略图的media_id
        /// </summary>
        public string thumb_media_id;
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author;
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url;
        /// <summary>
        /// <para>图文消息页面的内容，支持HTML标签。具备微信支付权限的公众号，可以使用a标签，其他公众号不能使用</para>
        /// <para>在群发图文中插入小程序，则在调用上传图文消息素材接口时，需在content字段中添加小程序跳转链接</para>
        /// <para>小程序卡片跳转小程序：&lt;mp-miniprogram data-miniprogram-appid="wx123123123" data-miniprogram-path="pages/index/index" data-miniprogram-title="小程序示例" data-progarm-imageurl="http://mmbizqbic.cn/demo.jpg"&gt;&lt;/mp-miniprogram&gt;</para>
        /// <para>文字跳转小程序：&lt;a data-miniprogram-appid="wx123123123" data-miniprogram-path="pages/index" href=""&gt;点击文字跳转小程序&lt;/a&gt;</para>
        /// <para>图片跳转小程序：&lt;a data-miniprogram-appid="wx123123123" data-miniprogram-path="pages/index" href=""&gt;&lt;img src="http://mmbiz.qpic.cn/mmbiz_jpg/demo/0?wx_fmt=jpg" alt="" data-width="null" data-ratio="NaN"&gt;&lt;/a&gt;</para>
        /// </summary>
        public string content;
        /// <summary>
        /// 图文消息的描述，如本字段为空，则默认抓取正文前64个字
        /// </summary>
        public string digest;
        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public int show_cover_pic;
        /// <summary>
        /// 是否打开评论，0不打开，1打开
        /// </summary>
        public int need_open_comment;
        /// <summary>
        /// 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
        /// </summary>
        public int only_fans_can_comment;
    }

    /// <summary>
    ///  图文消息的文章素材带上Url参数
    /// </summary>
    public class ArticleMaterialWithURL : ArticleMaterial
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url;
    }

    /// <summary>
    /// 标签群发参数
    /// </summary>
    public abstract class TagSendParam
    {
        /// <summary>
        /// 标签群发的过滤筛选条件
        /// </summary>
        public TagSendFilter filter;
        /// <summary>
        /// 主动设置 clientmsgid 参数，避免重复推送，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid
        /// </summary>
        public string clientmsgid;
    }
    /// <summary>
    /// 标签群发的过滤条件
    /// </summary>
    public class TagSendFilter
    {
        /// <summary>
        /// 用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据tag_id发送给指定群组的用户
        /// </summary>
        public bool is_to_all;
        /// <summary>
        /// 群发到的标签的tag_id，参见用户管理中用户分组接口，若is_to_all值为true，可不填写tag_id
        /// </summary>
        public int tag_id;
    }
    /// <summary>
    /// 图文标签群发参数
    /// </summary>
    public class NewsTagSendParam:TagSendParam
    {
        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public Media_Msg mpnews;
        /// <summary>
        /// 群发的消息类型，图文消息为mpnews
        /// </summary>
        [JsonProperty]
        public const string msgtype= "mpnews";
        /// <summary>
        /// 图文消息被判定为转载时，是否继续群发。 1为继续群发（转载），0为停止群发。 该参数默认为0。
        /// </summary>
        public int send_ignore_reprint = 0;
    }
    /// <summary>
    /// 文本标签群发参数
    /// </summary>
    public class ContentTagSendParam : TagSendParam
    {
        /// <summary>
        /// 即将发送的文本消息
        /// </summary>
        public Text_Msg text;
        /// <summary>
        /// 群发的消息类型，文本消息为text
        /// </summary>
        [JsonProperty]
        public const string msgtype = "text";
    }
    /// <summary>
    /// 语音标签群发参数
    /// </summary>
    public class VoiceTagSendParam : TagSendParam
    {
        /// <summary>
        /// 即将群发的语音消息
        /// </summary>
        public Media_Msg voice;
        /// <summary>
        /// 群发的消息类型，语音为voice
        /// </summary>
        [JsonProperty]
        public const string msgtype = "voice";
    }
    /// <summary>
    /// 图片标签群发参数
    /// </summary>
    public class PictureTagSendParam : TagSendParam
    {
        /// <summary>
        /// 即将发送的图片消息
        /// </summary>
        public Media_Msg image;
        /// <summary>
        /// 群发的消息类型，图片为image
        /// </summary>
        [JsonProperty]
        public const string msgtype = "image";
    }
    /// <summary>
    /// 视频标签群发参数
    /// </summary>
    public class VideoTagSendParam : TagSendParam
    {
        /// <summary>
        /// 即将发送的视频消息
        /// </summary>
        public Media_Msg mpvideo;
        /// <summary>
        /// 群发的消息类型，视频为video
        /// </summary>
        [JsonProperty]
        public const string msgtype = "mpvideo";
    }
    /// <summary>
    /// 卡券标签群发参数
    /// </summary>
    public class WxCardTagSendParam : TagSendParam
    {
        /// <summary>
        /// 即将发送的卡券消息
        /// </summary>
        public Card_Msg wxcard;
        /// <summary>
        /// 群发的消息类型，卡券为wxcard
        /// </summary>
        [JsonProperty]
        public const string msgtype = "wxcard";
    }

    /// <summary>
    /// 用户OpenId列表群发参数
    /// </summary>
    public abstract class OpenIdSendParam
    {
        /// <summary>
        /// 消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个
        /// </summary>
        public List<string> touser = new List<string>();
        /// <summary>
        /// 主动设置 clientmsgid 参数，避免重复推送，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid
        /// </summary>
        public string clientmsgid;
    }
    /// <summary>
    /// 图文OpenId群发参数
    /// </summary>
    public class NewsOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public Media_Msg mpnews;
        /// <summary>
        /// 群发的消息类型，图文消息为mpnews
        /// </summary>
        [JsonProperty]
        public const string msgtype = "mpnews";
        /// <summary>
        /// 图文消息被判定为转载时，是否继续群发。 1为继续群发（转载），0为停止群发。 该参数默认为0。
        /// </summary>
        public int send_ignore_reprint = 0;
    }
    /// <summary>
    /// 文本OpenId群发参数
    /// </summary>
    public class ContentOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 即将发送的文本消息
        /// </summary>
        public Text_Msg text;
        /// <summary>
        /// 群发的消息类型，文本消息为text
        /// </summary>
        [JsonProperty]
        public const string msgtype = "text";
    }
    /// <summary>
    /// 语音OpenId群发参数
    /// </summary>
    public class VoiceOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 即将群发的语音消息
        /// </summary>
        public Media_Msg voice;
        /// <summary>
        /// 群发的消息类型，语音为voice
        /// </summary>
        [JsonProperty]
        public const string msgtype = "voice";
    }
    /// <summary>
    /// 图片OpenId群发参数
    /// </summary>
    public class PictureOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 即将发送的图片消息
        /// </summary>
        public Media_Msg image;
        /// <summary>
        /// 群发的消息类型，图片为image
        /// </summary>
        [JsonProperty]
        public const string msgtype = "image";
    }
    /// <summary>
    /// 视频OpenId群发参数
    /// </summary>
    public class VideoOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 即将发送的视频消息
        /// </summary>
        public Video_Msg mpvideo;
        /// <summary>
        /// 群发的消息类型，视频为video
        /// </summary>
        [JsonProperty]
        public const string msgtype = "mpvideo";
    }
    /// <summary>
    /// 卡券OpenId群发参数
    /// </summary>
    public class WxCardOpenIdSendParam : OpenIdSendParam
    {
        /// <summary>
        /// 即将发送的卡券消息
        /// </summary>
        public Card_Msg wxcard;
        /// <summary>
        /// 群发的消息类型，卡券为wxcard
        /// </summary>
        [JsonProperty]
        public const string msgtype = "wxcard";
    }

    /// <summary>
    /// 群发消息的速度
    /// </summary>
    public class GroupSendSpeed
    {
        /// <summary>
        /// 群发速度的级别：0、1、2、3、4
        /// </summary>
        public int speed;
        /// <summary>
        /// 群发速度的真实值 单位：万/分钟：80w/分钟、60w/分钟、45w/分钟、30w/分钟、10w/分钟
        /// </summary>
        public int realspeed;
    }
}
