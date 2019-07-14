using InterfaceProject.NetTool;
using InterfaceProject.NetTool.Log;
using InterfaceProject.NetWxSDK.HelpModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 微信消息管理
    /// </summary>
    public class MessageLinkUp: IMessageLinkUp
    {
        /// <summary>
        /// 加/解密操作对象
        /// </summary>
        private Tencent.WXBizMsgCrypt wxcpt;

        /// <summary>
        /// 当前处理的微信公众号信息
        /// </summary>
        private WeChatConfig weChatConfig;

        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;

        /// <summary>
        /// 实例化微信消息管理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat">微信接入基本操作对象</param>
        public MessageLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 实例化微信消息管理实例，并指明操作的公众号
        /// </summary>
        /// <param name="connectWeChat">微信接入基本操作对象</param>
        /// <param name="idOrAppId">公众号的AppId</param>
        public MessageLinkUp(IConnectLinkUp connectWeChat, string idOrAppId)
        {
            connect = connectWeChat;
            connect.Initialize(idOrAppId);
            weChatConfig = connect.WeChatConfig;
            wxcpt = new Tencent.WXBizMsgCrypt(weChatConfig.Token, weChatConfig.EncodingAESKey, weChatConfig.AppID);
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        public MessageLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            weChatConfig = connect.WeChatConfig;
            wxcpt = new Tencent.WXBizMsgCrypt(weChatConfig.Token, weChatConfig.EncodingAESKey, weChatConfig.AppID);
            return this;
        }

        /// <summary>
        /// 从请求中提取微信消息推送的请求数据
        /// </summary>
        /// <param name="request">推送过来的请求</param>
        /// <returns>消息推送的请求数据</returns>
        public MsgRequest GetMsgRequestData(WeChatEncryptMsg requestEncryptMsg)
        {
            //微信服务器在五秒内收不到响应会断掉连接，并且重新发起请求，总共重试三次
            //消息排重，推荐使用msgid排重;事件类型消息推荐使用FromUserName + CreateTime 排重
            //服务器无法保证在五秒内处理并回复,直接回复success;直接回复空串,微信不再重试
            //开发者在5秒内未回复任何内容，开发者回复了异常数据，微信系统提示“该公众号暂时无法提供服务，请稍后再试”
            if (weChatConfig == null)
                throw new NullReferenceException("请使用构造MessageLinkUp(idOrAppId)，或初始化Initialize(idOrAppId)");
            string xmlText;//消息
            if (weChatConfig.EnCrypt)
            {
                //消息是密文，要解密后处理
                xmlText = MessageDecrypt(requestEncryptMsg);
            }
            else
            {
                var stream = requestEncryptMsg.Body;//具体消息数据在请求流里面
                StreamReader reader = new StreamReader(stream);
                xmlText = reader.ReadToEnd();//消息
                reader.Close();
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlText);
            XmlNode rootNode = xmlDoc.SelectSingleNode("xml");
            MsgRequest msgRequest = null;
            if (rootNode["MsgType"] == null)
            {
                msgRequest = new MsgRequest();
            }
            else
            {
                string msgType =rootNode["MsgType"].InnerText;
                switch (msgType)
                {
                    case "text":
                        ContentMsg contentMsg = new ContentMsg();
                        if (rootNode["MsgId"] != null)
                            contentMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["Content"] != null)
                            contentMsg.Content = rootNode["Content"].InnerText;
                        msgRequest = contentMsg;
                        break;
                    case "image":
                        PictureMsg pictureMsg = new PictureMsg();
                        if (rootNode["MsgId"] != null)
                            pictureMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["PicUrl"] != null)
                            pictureMsg.PicUrl = rootNode["PicUrl"].InnerText;
                        if (rootNode["MediaId"] != null)
                            pictureMsg.MediaId = rootNode["MediaId"].InnerText;
                        msgRequest = pictureMsg;
                        break;
                    case "voice":
                        VoiceMsg voiceMsg = new VoiceMsg();
                        if (rootNode["MsgId"] != null)
                            voiceMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["MediaId"] != null)
                            voiceMsg.MediaId = rootNode["MediaId"].InnerText;
                        if (rootNode["Format"] != null)
                            voiceMsg.Format = rootNode["Format"].InnerText;
                        if (rootNode["Recognition"] != null)
                            voiceMsg.Recognition = rootNode["Recognition"].InnerText;
                        msgRequest = voiceMsg;
                        break;
                    case "video":
                        VideoMsg videoMsg = new VideoMsg();
                        if (rootNode["MsgId"] != null)
                            videoMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["MediaId"] != null)
                            videoMsg.MediaId = rootNode["MediaId"].InnerText;
                        if (rootNode["ThumbMediaId"] != null)
                            videoMsg.ThumbMediaId = rootNode["ThumbMediaId"].InnerText;
                        msgRequest = videoMsg;
                        break;
                    case "shortvideo":
                        ShortVideoMsg shortVideoMsg = new ShortVideoMsg();
                        if (rootNode["MsgId"] != null)
                            shortVideoMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["MediaId"] != null)
                            shortVideoMsg.MediaId = rootNode["MediaId"].InnerText;
                        if (rootNode["ThumbMediaId"] != null)
                            shortVideoMsg.ThumbMediaId = rootNode["ThumbMediaId"].InnerText;
                        msgRequest = shortVideoMsg;
                        break;
                    case "location":
                        LocationMsg locationMsg = new LocationMsg();
                        if (rootNode["MsgId"] != null)
                            locationMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["Location_X"] != null)
                            locationMsg.Location_X = Convert.ToDouble(rootNode["Location_X"].InnerText);
                        if (rootNode["Location_Y"] != null)
                            locationMsg.Location_Y = Convert.ToDouble(rootNode["Location_Y"].InnerText);
                        if (rootNode["Scale"] != null)
                            locationMsg.Scale = int.Parse(rootNode["Scale"].InnerText);
                        if (rootNode["Label"] != null)
                            locationMsg.Label = rootNode["Label"].InnerText;
                        msgRequest = locationMsg;
                        break;
                    case "link":
                        LinkMsg linkMsg = new LinkMsg();
                        if (rootNode["MsgId"] != null)
                            linkMsg.MsgId = Convert.ToInt64(rootNode["MsgId"].InnerText);
                        if (rootNode["Title"] != null)
                            linkMsg.Title = rootNode["Title"].InnerText;
                        if (rootNode["Description"] != null)
                            linkMsg.Description = rootNode["Description"].InnerText;
                        if (rootNode["Url"] != null)
                            linkMsg.Url = rootNode["Url"].InnerText;
                        msgRequest = linkMsg;
                        break;
                    case "event":
                        if (rootNode["Event"] != null)
                        {
                            string eventStr = rootNode["Event"].InnerText.ToLower();
                            switch (eventStr)
                            {
                                case "subscribe":
                                    if (rootNode["EventKey"] != null)
                                    {
                                        string eventKey =rootNode["EventKey"].InnerText;
                                        if (eventKey.StartsWith("qrscene_"))
                                        {
                                            ScanSubscribeEvent scanSubscribeEvent = new ScanSubscribeEvent();
                                            scanSubscribeEvent.EventKey = eventKey;
                                            if (rootNode["Ticket"] != null)
                                                scanSubscribeEvent.Ticket = rootNode["Ticket"].InnerText;
                                            msgRequest = scanSubscribeEvent;
                                        }
                                    }
                                    if(msgRequest == null)
                                    {
                                        msgRequest = new SubscribeEvent();
                                    }
                                    break;
                                case "unsubscribe":msgRequest = new UnSubscribeEvent();break;
                                case "scan":
                                    ScanEvent scanEvent = new ScanEvent();
                                    if (rootNode["EventKey"] != null)
                                        scanEvent.EventKey = rootNode["EventKey"].InnerText;
                                    if (rootNode["Ticket"] != null)
                                        scanEvent.Ticket = rootNode["Ticket"].InnerText;
                                    msgRequest = scanEvent;
                                    break;
                                case "location":
                                    LocationEvent locationEvent = new LocationEvent();
                                    if (rootNode["Latitude"] != null)
                                        locationEvent.Latitude = double.Parse(rootNode["Latitude"].InnerText);
                                    if (rootNode["Longitude"] != null)
                                        locationEvent.Longitude = double.Parse(rootNode["Longitude"].InnerText);
                                    if (rootNode["Precision"] != null)
                                        locationEvent.Precision = double.Parse(rootNode["Precision"].InnerText);
                                    msgRequest = locationEvent;
                                    break;
                                case "click":
                                    ClickEvent clickEvent = new ClickEvent();
                                    if (rootNode["EventKey"] != null)
                                        clickEvent.EventKey = rootNode["EventKey"].InnerText;
                                    msgRequest = clickEvent;
                                    break;
                                case "view":
                                    ViewEvent viewEvent = new ViewEvent();
                                    if (rootNode["EventKey"] != null)
                                        viewEvent.EventKey = rootNode["EventKey"].InnerText;
                                    msgRequest = viewEvent;
                                    break;
                                case "masssendjobfinish"://批量发送群发消息处理完成的消息通知
                                    MassSendJobFinishEvent sendJobFinishEvent = new MassSendJobFinishEvent();
                                    if (rootNode["MsgID"] != null)
                                        sendJobFinishEvent.MsgID = int.Parse(rootNode["MsgID"].InnerText);
                                    if (rootNode["Status"] != null)
                                        sendJobFinishEvent.Status = (MassSendJobStatus)Enum.Parse(typeof(MassSendJobStatus), rootNode["Status"].InnerText);
                                    if (rootNode["TotalCount"] != null)
                                        sendJobFinishEvent.TotalCount = int.Parse(rootNode["TotalCount"].InnerText);
                                    if (rootNode["FilterCount"] != null)
                                        sendJobFinishEvent.FilterCount = int.Parse(rootNode["FilterCount"].InnerText);
                                    if (rootNode["SentCount"] != null)
                                        sendJobFinishEvent.SentCount = int.Parse(rootNode["SentCount"].InnerText);
                                    if (rootNode["ErrorCount"] != null)
                                        sendJobFinishEvent.ErrorCount = int.Parse(rootNode["ErrorCount"].InnerText);
                                    XmlNodeList checkResultList = rootNode["CopyrightCheckResult"]?.ChildNodes;
                                    if (checkResultList != null&&checkResultList.Count>0)
                                    {
                                        XmlNode countNode = rootNode["CopyrightCheckResult"]["Count"];
                                        XmlNode checkStateNode = rootNode["CopyrightCheckResult"]["CheckState"];
                                        XmlNode resultListNode = rootNode["CopyrightCheckResult"]["ResultList"];
                                        if (countNode!=null)
                                            sendJobFinishEvent.CopyrightCheckCount = int.Parse(countNode.InnerText);
                                        else if (checkStateNode!=null)
                                            sendJobFinishEvent.CheckState = int.Parse(checkStateNode.InnerText);
                                        else if (resultListNode!=null)
                                        {
                                            sendJobFinishEvent.ResultList = new List<ArticleCheckResult>();
                                            foreach (XmlNode itemNode in resultListNode)
                                            {
                                                sendJobFinishEvent.ResultList.Add(new ArticleCheckResult()
                                                {
                                                    ArticleIdx = int.Parse(itemNode["ArticleIdx"].InnerText),
                                                    UserDeclareState = int.Parse(itemNode["UserDeclareState"].InnerText),
                                                    AuditState = int.Parse(itemNode["AuditState"].InnerText),
                                                    OriginalArticleUrl = itemNode["OriginalArticleUrl"].InnerText,
                                                    OriginalArticleType = int.Parse(itemNode["OriginalArticleType"].InnerText),
                                                    CanReprint = bool.Parse(itemNode["CanReprint"].InnerText),
                                                    NeedReplaceContent = bool.Parse(itemNode["NeedReplaceContent"].InnerText),
                                                    NeedShowReprintSource = bool.Parse(itemNode["NeedShowReprintSource"].InnerText),
                                                });
                                            }
                                        }
                                    }
                                    msgRequest = sendJobFinishEvent;
                                    break;
                                case "templatesendjobfinish":
                                    TemplateSendJobFinishEvent templateSendFinishEvent = new TemplateSendJobFinishEvent();
                                    if (rootNode["MsgID"] != null)
                                        templateSendFinishEvent.MsgID = int.Parse(rootNode["MsgID"].InnerText);
                                    if (rootNode["Status"] != null)
                                        templateSendFinishEvent.Status = rootNode["Status"].InnerText;
                                    break;
                                default: msgRequest = new MsgRequest(); break;
                            }
                        }
                        break;
                    default: msgRequest = new MsgRequest(); break;
                }
                msgRequest.MsgType = msgType;
            }
            msgRequest.ToUserName = rootNode["ToUserName"].InnerText;
            msgRequest.FromUserName = rootNode["FromUserName"].InnerText;
            msgRequest.CreateTime = int.Parse(rootNode["CreateTime"].InnerText);

            return msgRequest;
        }

        /// <summary>
        /// 根据请求确认是否要加密，处理出消息响应文本
        /// </summary>
        /// <param name="request">推送过来的请求</param>
        /// <param name="msgResponse">消息响应对象</param>
        /// <returns>消息响应文本</returns>
        public string GetResponseText(MsgResponse msgResponse)
        {
            if (weChatConfig == null)
                throw new NullReferenceException("请使用构造MessageLinkUp(idOrAppId)，或初始化Initialize(idOrAppId)");
            //根据MsgResponse生成响应的xml
            StringBuilder responseXml = new StringBuilder();
            responseXml.AppendLine("<xml>");
            responseXml.AppendLine($"<ToUserName><![CDATA[{msgResponse.ToUserName}]]></ToUserName>");
            responseXml.AppendLine($"<FromUserName><![CDATA[{msgResponse.FromUserName}]]></FromUserName>");
            responseXml.AppendLine($"<CreateTime><![CDATA[{msgResponse.CreateTime}]]></CreateTime>");
            responseXml.AppendLine($"<MsgType><![CDATA[{msgResponse.MsgType}]]></MsgType>");
            msgResponse.MsgType = msgResponse.MsgType?.ToLower();
            if ("text".Equals(msgResponse.MsgType) && msgResponse is ContentReplyMsg)
                responseXml.AppendLine($"<Content><![CDATA[{((ContentReplyMsg)msgResponse).Content}]]></Content>");

            if ("image".Equals(msgResponse.MsgType) && msgResponse is PictureReplyMsg)
            {
                PictureReplyMsg pictureMsg = (PictureReplyMsg)msgResponse;
                responseXml.AppendLine("<Image>");
                if (!string.IsNullOrWhiteSpace(pictureMsg.MediaId))
                    responseXml.AppendLine($"<MediaId><![CDATA[{pictureMsg.MediaId}]]></MediaId>");
                else
                    throw new Exception("图片媒体Id不能为空");
                responseXml.AppendLine("</Image>");
            }
            else if ("voice".Equals(msgResponse.MsgType) && msgResponse is VoiceReplyMsg)
            {
                VoiceReplyMsg voiceMsg = (VoiceReplyMsg)msgResponse;
                responseXml.AppendLine("<Voice>");
                if (!string.IsNullOrWhiteSpace(voiceMsg.MediaId))
                    responseXml.AppendLine($"<MediaId><![CDATA[{voiceMsg.MediaId}]]></MediaId>");
                else
                    throw new Exception("语音媒体Id不能为空");
                responseXml.AppendLine("</Voice>");
            }
            else if ("video".Equals(msgResponse.MsgType) && msgResponse is VideoReplyMsg)
            {
                VideoReplyMsg videoMsg = (VideoReplyMsg)msgResponse;
                responseXml.AppendLine("<Video>");
                if (!string.IsNullOrWhiteSpace(videoMsg.MediaId))
                    responseXml.AppendLine($"<MediaId><![CDATA[{videoMsg.MediaId}]]></MediaId>");
                else
                    throw new Exception("视频媒体Id不能为空");
                responseXml.AppendLine($"<Title><![CDATA[{videoMsg.Title}]]></Title>");
                responseXml.AppendLine($"<Description><![CDATA[{videoMsg.Description}]]></Description>");
                responseXml.AppendLine("</Video>");
            }
            else if ("music".Equals(msgResponse.MsgType) && msgResponse is MusicReplyMsg)
            {
                MusicReplyMsg musicMsg = (MusicReplyMsg)msgResponse;
                responseXml.AppendLine("<Music>");
                responseXml.AppendLine($"<Title><![CDATA[{musicMsg.Title}]]></Title>");
                responseXml.AppendLine($"<Description><![CDATA[{musicMsg.Description}]]></Description>");
                responseXml.AppendLine($"<MusicUrl><![CDATA[{musicMsg.MusicUrl}]]></MusicUrl>");
                responseXml.AppendLine($"<HQMusicUrl><![CDATA[{musicMsg.HQMusicUrl}]]></HQMusicUrl>");
                if (!string.IsNullOrWhiteSpace(musicMsg.ThumbMediaId))
                    responseXml.AppendLine($"<ThumbMediaId><![CDATA[{musicMsg.ThumbMediaId}]]></ThumbMediaId>");
                else
                    throw new Exception("音乐缩略图的媒体id不能为空");
                responseXml.AppendLine("</Music>");
            }
            else if ("news".Equals(msgResponse.MsgType) && msgResponse is NewsReplyMsg)
            {
                NewsReplyMsg newsMsg = (NewsReplyMsg)msgResponse;
                responseXml.AppendLine($"<ArticleCount>{newsMsg.ArticleCount}</ArticleCount>");
                responseXml.AppendLine("<Articles>");
                int maxCount = Math.Min(newsMsg.ArticleCount, 8);
                for(int i = 0; i < maxCount; i++)
                {
                    var msgArticle = newsMsg.Articles[0];
                    responseXml.AppendLine("<item>");
                    responseXml.AppendLine($"<Title><![CDATA[{msgArticle.Title}]]></Title>");
                    responseXml.AppendLine($"<Description><![CDATA[{msgArticle.Description}]]></Description>");
                    responseXml.AppendLine($"<PicUrl><![CDATA[{msgArticle.PicUrl}]]></PicUrl>");
                    responseXml.AppendLine($"<PicUrl><![CDATA[{msgArticle.PicUrl}]]></PicUrl>");
                    responseXml.AppendLine("</item>");
                }
                responseXml.AppendLine("</Articles>");
            }
            responseXml.AppendLine("</xml>");
            string xmlText = responseXml.ToString();//消息
            if (weChatConfig.EnCrypt)
            {
                //消息进行加密处理
                xmlText = MessageEncrypt(xmlText);
            }
            return xmlText;
        }

        /// <summary>
        /// 获取公众号的自动回复规则
        /// </summary>
        /// <returns>自动回复规则</returns>
        public WeChatResult<JObject> GetAutoReplyRule()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token={0}";
            string accessToken = connect.GetAccessToken();
            url = String.Format(url, accessToken);
            WeChatResult<JObject> weChatResult = SimulateRequest.HttpGet<WeChatResult<JObject>>(url);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取微信服务器IP地址GetWeChatServerIP()，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 对微信推送过来的消息进行解密
        /// </summary>
        /// <param name="request">推送过来的请求</param>
        /// <returns>解密后的xml明文</returns>
        private string MessageDecrypt(WeChatEncryptMsg requestEncryptMsg)
        {
            //string signature = request.QueryString["signature"];
            //string timestamp = request.QueryString["timestamp"];
            //string nonce = request.QueryString["nonce"];
            //string openid = request.QueryString["openid"];
            //string encrypt_type = request.QueryString["encrypt_type"];//aes
            //string msg_signature = request.QueryString["msg_signature"];
            var stream = requestEncryptMsg.Body;//具体消息数据在请求流里面
            StreamReader reader = new StreamReader(stream);
            string ciphertext = reader.ReadToEnd();//密文消息
            reader.Close();
            string xmlMsgTxt = null;  //解析之后的明文
            if (ciphertext.Contains("<Encrypt>") && ciphertext.Contains("</Encrypt>"))
            {
                int result = wxcpt.DecryptMsg(requestEncryptMsg.MsgSignature,
                    requestEncryptMsg.Timestamp, requestEncryptMsg.Nonce, ciphertext, ref xmlMsgTxt);
                SystemLogHelper.Info(MethodBase.GetCurrentMethod(), "消息解密处理结果：" + CryptResult(result));
                weChatConfig.EnCrypt = true;//确实启用了加密方式
            }
            else
            {
                xmlMsgTxt = ciphertext;
                weChatConfig.EnCrypt = false;//并没有启用加密方式
            }
            return xmlMsgTxt;
        }

        /// <summary>
        /// 以推送过来请求中的随机字符串、时间戳为基础进行响应消息加密
        /// </summary>
        /// <param name="xmlMsgTxt">要加密的明文</param>
        /// <returns>加密后的密文</returns>
        private string MessageEncrypt(string xmlMsgTxt)
        {
            string timestamp = TimeHelper.GetTime(DateTime.Now).ToString();
            string nonce = StringHelper.RandomStr(StrType.NumAndLowercase,16);
            string sEncryptMsg = null; //xml格式的密文
            int result = wxcpt.EncryptMsg(xmlMsgTxt, timestamp, nonce, ref sEncryptMsg);
            SystemLogHelper.Info(MethodBase.GetCurrentMethod(), "消息加密处理结果：" + CryptResult(result));
            return sEncryptMsg;
        }

        /// <summary>
        /// 根据结果代码转成可读的加/解密处理结果
        /// </summary>
        /// <param name="resultCode">加/解密结果代码</param>
        /// <return>加/解密处理结果</return>
        private string CryptResult(int resultCode)
        {
            string resultTxt = string.Empty;
            switch (resultCode)
            {
                case 0: resultTxt="加/解密成功"; break;
                case -40001: resultTxt = "签名验证错误";break;
                case -40002: resultTxt = "xml解析失败"; break;
                case -40003: resultTxt = "sha加密生成签名失败"; break;
                case -40004: resultTxt = "AESKey 非法"; break;
                case -40005: resultTxt = "appid 校验错误"; break;
                case -40006: resultTxt = "AES 加密失败"; break;
                case -40007: resultTxt = "AES 解密失败"; break;
                case -40008: resultTxt = "解密后得到的buffer非法"; break;
                case -40009: resultTxt = "base64加密异常"; break;
                case -40010: resultTxt = "base64解密异常"; break;
                default: resultTxt = "其他未知异常";break;
            }
            return resultTxt;
        }
    }
}
