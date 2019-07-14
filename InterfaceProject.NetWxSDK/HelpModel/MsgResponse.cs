using InterfaceProject.NetTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 向微信服务器响应的消息
    /// </summary>
    public class MsgResponse
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }

        private int createTime;
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime {
            get
            {
                if (createTime.ToString().Length < 10)
                    createTime = Convert.ToInt32(TimeHelper.GetTime(DateTime.Now));
                return createTime;
            }
            set
            {
                if (value.ToString().Length < 10)
                    createTime = Convert.ToInt32(TimeHelper.GetTime(DateTime.Now));
                else
                    createTime = value;
            }
        }
        /// <summary>
        /// 消息创建时间 （DateTime类型）
        /// </summary>
        public DateTime CreateTime2 {
            get
            {
                if (createTime.ToString().Length < 10)
                    createTime = Convert.ToInt32(TimeHelper.GetTime(DateTime.Now));
                return TimeHelper.GetDateTime(createTime);
            }
            set
            {
                if (value < new DateTime(1970, 1, 1))
                    createTime = Convert.ToInt32(TimeHelper.GetTime(DateTime.Now));
                else
                    createTime = Convert.ToInt32(TimeHelper.GetTime(value));
            }
        }
        /// <summary>
        /// 消息类型（text、image、voice、video、music、news）
        /// </summary>
        public string MsgType { get; set; }
    }

    /// <summary>
    /// 回复文本消息
    /// </summary>
    public class ContentReplyMsg : MsgResponse
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class PictureReplyMsg : MsgResponse
    {
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的媒体id
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class VoiceReplyMsg : PictureReplyMsg
    {
    }

    /// <summary>
    /// 回复视频消息
    /// </summary>
    public class VideoReplyMsg : PictureReplyMsg
    {
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class MusicReplyMsg : MsgResponse
    {
        /// <summary>
        /// 音乐消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐消息的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐消息的链接
        /// </summary>
        public string MusicUrl { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 回复图文消息
    /// </summary>
    public class NewsReplyMsg : MsgResponse
    {
        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>
        public int ArticleCount { get { return this.Articles.Count; } }
        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过8，则将会无响应
        /// </summary>
        public List<WxArticle> Articles { get; set; }
    }
    /// <summary>
    /// 图文消息的文章
    /// </summary>
    public class WxArticle
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }
}
