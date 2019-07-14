using InterfaceProject.NetTool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 微信服务器推送的请求消息
    /// </summary>
    public class MsgRequest
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        private int createTime;
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime
        {
            get
            {
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
        /// 消息类型（text、image、voice、video、shortvideo、location、link、event）
        /// </summary>
        public string MsgType { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class ContentMsg:MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class PictureMsg : MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 图片链接（由系统生成）
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 各类型的消息媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMsg : MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMsg : MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 小视频消息
    /// </summary>
    public class ShortVideoMsg : VideoMsg
    {
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationMsg : MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public double Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class LinkMsg : MsgRequest
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 关注事件
    /// </summary>
    public class SubscribeEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)
        /// </summary>
        [JsonProperty]
        public const string Event = "subscribe";
    }

    /// <summary>
    /// 取消关注事件
    /// </summary>
    public class UnSubscribeEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，unsubscribe(取消订阅)
        /// </summary>
        [JsonProperty]
        public const string Event = "unsubscribe";
    }

    /// <summary>
    /// 扫描带参数二维码事件(用户未关注时，进行关注后的事件推送)
    /// </summary>
    public class ScanSubscribeEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)
        /// </summary>
        [JsonProperty]
        public const string Event = "subscribe";
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 扫描带参数二维码事件(用户已关注时的事件推送)
    /// </summary>
    public class ScanEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，SCAN（扫描【已关注时】）
        /// </summary>
        [JsonProperty]
        public const string Event = "SCAN";
        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class LocationEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，LOCATION（地理位置）
        /// </summary>
        [JsonProperty]
        public const string Event = "LOCATION";
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }
    }

    /// <summary>
    /// 点击菜单拉取消息时的事件
    /// </summary>
    public class ClickEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，CLICK（点击微信菜单）
        /// </summary>
        [JsonProperty]
        public const string Event = "CLICK";
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 点击菜单跳转链接时的事件
    /// </summary>
    public class ViewEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，VIEW（点击跳转URL）
        /// </summary>
        [JsonProperty]
        public const string Event = "CLICK";
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 封装单独个消息ID
    /// </summary>
    public class Msg_ID
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int msgid;
    }

    /// <summary>
    /// 批量发送群发消息处理完成后的推送请求事件
    /// </summary>
    public class MassSendJobFinishEvent : MsgRequest
    {
        /// <summary>
        /// 事件类型，MASSSENDJOBFINISH（批量发送群发消息处理完成）
        /// </summary>
        [JsonProperty]
        public const string Event = "MASSSENDJOBFINISH";
        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 群发处理结果状态
        /// </summary>
        public MassSendJobStatus Status;
        /// <summary>
        /// 群发处理结果状态的中文说明信息
        /// </summary>
        public string StatusChinese
        {
            get
            {
                string statusStr = SendJobStatusHandle.GetChineseMsg(Status);
                return statusStr;
            }
        }
        /// <summary>
        /// tag_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount;
        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount;
        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SentCount;
        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount;
        /// <summary>
        /// 权限检查总数量
        /// </summary>
        public int CopyrightCheckCount;
        /// <summary>
        /// 各个单图文校验结果
        /// </summary>
        public List<ArticleCheckResult> ResultList;
        /// <summary>
        /// 整体校验结果：1-未被判为转载，可以群发，2-被判为转载，可以群发，3-被判为转载，不能群发
        /// </summary>
        public int CheckState;

        /// <summary>
        /// 微信群发消息结果状态内部实现单例模式
        /// </summary>
        internal class SendJobStatusHandle
        {
            /// <summary>
            /// 记录错误代码枚举的字段信息的集合
            /// </summary>
            private static List<FieldDesc<int>> fieldDescList;

            static SendJobStatusHandle()
            {
                Type sendJobStatusType = typeof(MassSendJobStatus);
                foreach (var errCodeField in sendJobStatusType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var explainAttribute = (ExplainAttribute)(errCodeField.GetCustomAttributes(false).FirstOrDefault(x => x is ExplainAttribute));
                    if (explainAttribute != null)
                    {
                        if (fieldDescList == null) fieldDescList = new List<FieldDesc<int>>();
                        FieldDesc<int> fieldDesc = new FieldDesc<int>();
                        fieldDesc.FieldName = errCodeField.Name;
                        fieldDesc.FieldValue = Convert.ToInt32(errCodeField.GetValue(null));
                        fieldDesc.ExplainContent = explainAttribute.Content;
                        fieldDescList.Add(fieldDesc);//添加一个记录了说明内容的字段信息
                    }
                }
            }

            /// <summary>
            /// 获取微信群发消息结果状态对应的中文消息
            /// </summary>
            /// <param name="errorCode">微信错误代号</param>
            /// <returns>错误代号对应的中文消息</returns>
            public static string GetChineseMsg(MassSendJobStatus sendJobStatus)
            {
                int errCode = (int)sendJobStatus;
                FieldDesc<int> errCodeFieldDesc = fieldDescList.Where(fieldDesc => fieldDesc.FieldValue == errCode).FirstOrDefault();
                return errCodeFieldDesc?.ExplainContent;
            }
        }
    }

    /// <summary>
    /// 微信模板消息发送完成后的推送请求事件
    /// </summary>
    public class TemplateSendJobFinishEvent: MsgRequest
    {
        /// <summary>
        /// 事件类型，TEMPLATESENDJOBFINISH（微信模板消息处理完成）
        /// </summary>
        [JsonProperty]
        public const string Event = "TEMPLATESENDJOBFINISH";
        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 群发处理结果状态
        /// </summary>
        public string Status;
    }

    /// <summary>
    /// 群发消息中的文章原创检查结果
    /// </summary>
    public class ArticleCheckResult
    {
        /// <summary>
        /// 群发文章的序号，从1开始
        /// </summary>
        public int ArticleIdx;
        /// <summary>
        /// 用户声明文章的状态
        /// </summary>
        public int UserDeclareState;
        /// <summary>
        /// 系统校验的状态
        /// </summary>
        public int AuditState;
        /// <summary>
        /// 相似原创文的url
        /// </summary>
        public string OriginalArticleUrl;
        /// <summary>
        /// 相似原创文的类型
        /// </summary>
        public int OriginalArticleType;
        /// <summary>
        /// 是否能转载
        /// </summary>
        public bool CanReprint;
        /// <summary>
        /// 是否需要替换成原创文内容
        /// </summary>
        public bool NeedReplaceContent;
        /// <summary>
        /// 是否需要注明转载来源
        /// </summary>
        public bool NeedShowReprintSource;
    }

    /// <summary>
    /// 批量群发消息的处理结果状态
    /// </summary>
    public enum MassSendJobStatus
    {
        /// <summary>
        /// 群发失败
        /// </summary>
        [Explain("群发失败")]
        SendFail,
        /// <summary>
        /// 群发成功，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败
        /// </summary>
        [Explain("群发成功")]
        SendSuccess,
        /// <summary>
        /// 涉嫌广告
        /// </summary>
        [Explain("涉嫌广告")]
        SuspectedAdvertisement=10001,
        /// <summary>
        /// 涉嫌政治
        /// </summary>
        [Explain("涉嫌政治")]
        SuspectedPolitics =20001,
        /// <summary>
        /// 涉嫌色情
        /// </summary>
        [Explain("涉嫌色情")]
        SuspectedPornography =20002,
        /// <summary>
        /// 涉嫌社会
        /// </summary>
        [Explain("涉嫌社会")]
        SuspectedSociety =20004,
        /// <summary>
        /// 涉嫌违法犯罪
        /// </summary>
        [Explain("涉嫌违法犯罪")]
        SuspectedCrime = 20006,
        /// <summary>
        /// 涉嫌欺诈
        /// </summary>
        [Explain("涉嫌欺诈")]
        SuspectedFraud = 20008,
        /// <summary>
        /// 涉嫌版权
        /// </summary>
        [Explain("涉嫌版权")]
        SuspectedCopyright =20013,
        /// <summary>
        /// 涉嫌互推(互相宣传)
        /// </summary>
        [Explain("涉嫌互推(互相宣传)")]
        SuspectedPropagate = 22000,
        /// <summary>
        /// 涉嫌其他
        /// </summary>
        [Explain("涉嫌其他")]
        SuspectedOther = 21000,
        /// <summary>
        /// 原创校验出现系统错误且用户选择了被判为转载就不群发
        /// </summary>
        [Explain("原创校验出现系统错误且用户选择了被判为转载就不群发")]
        ErrorReprintNotSend = 30001,
        /// <summary>
        /// 原创校验被判定为不能群发
        /// </summary>
        [Explain("原创校验被判定为不能群发")]
        OriginalVerificationNoPass = 30002,
        /// <summary>
        /// 原创校验被判定为转载文且用户选择了被判为转载就不群发
        /// </summary>
        [Explain("原创校验被判定为转载文且用户选择了被判为转载就不群发")]
        OriginalVerifyReprintNotSend = 30003
    }
}
