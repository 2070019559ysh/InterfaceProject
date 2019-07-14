using InterfaceProject.WxSDK.HelpModel;
using InterfaceProject.WxSDK.LinkUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterfaceProject.BLL.Handle
{
    /// <summary>
    /// 处理微信对接相关业务
    /// </summary>
    public class WeChatHandle : IWeChatHandle
    {
        private readonly IMessageLinkUp messageLinkUp;

        /// <summary>
        /// 实例化，处理微信对接相关业务
        /// </summary>
        /// <param name="messageLinkUp">依赖微信消息处理</param>
        public WeChatHandle(IMessageLinkUp messageLinkUp)
        {
            this.messageLinkUp = messageLinkUp;
        }

        /// <summary>
        /// 初始化处理的公众号参数
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>初始化处理后的对象</returns>
        public WeChatHandle Initialize(string idOrAppId)
        {
            messageLinkUp.Initialize(idOrAppId);
            return this;
        }

        /// <summary>
        /// 执行微信消息处理
        /// </summary>
        /// <param name="signature">微信消息签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="body">请求参数主体</param>
        /// <returns>响应结果的Xml</returns>
        public string Execute(string signature,string timestamp,string nonce,Stream body)
        {
            WeChatEncryptMsg wechatMsg = new WeChatEncryptMsg()
            {
                MsgSignature = signature,
                Timestamp = timestamp,
                Nonce = nonce,
                Body = body
            };
            MsgRequest msgRequest = messageLinkUp.GetMsgRequestData(wechatMsg);
            string responseXml = "success";
            if (msgRequest is SubscribeEvent)
            {
                responseXml = DealWithMsg(msgRequest as SubscribeEvent);
            }
            else if (msgRequest is ContentMsg)
            {
                responseXml = DealWithMsg(msgRequest as ContentMsg);
            }
            return responseXml;
        }

        private string DealWithMsg(ContentMsg contentMsg)
        {
            MsgResponse msgResponse = new ContentReplyMsg()
            {
                FromUserName = contentMsg.ToUserName,
                ToUserName = contentMsg.FromUserName,
                CreateTime2 = DateTime.Now,
                Content = "感谢给公众号发消息",
                MsgType = "text"
            };
            return messageLinkUp.GetResponseText(msgResponse);
        }

        private string DealWithMsg(PictureMsg pictureMsg)
        {
            return "";
        }

        private string DealWithMsg(VoiceMsg voiceMsg)
        {
            return "";
        }

        private string DealWithMsg(VideoMsg videoMsg)
        {
            return "";
        }

        private string DealWithMsg(ShortVideoMsg shortVideoMsg)
        {
            return "";
        }

        private string DealWithMsg(LocationMsg locationMsg)
        {
            return "";
        }

        private string DealWithMsg(LinkMsg linkMsg)
        {
            return "";
        }

        private string DealWithMsg(SubscribeEvent subscribeEvent)
        {
            MsgResponse msgResponse = new ContentReplyMsg()
            {
                FromUserName = subscribeEvent.ToUserName,
                ToUserName = subscribeEvent.FromUserName,
                CreateTime2 = DateTime.Now,
                Content = "欢迎关注公众号",
                MsgType = "text"
            };
            return messageLinkUp.GetResponseText(msgResponse);
        }

        private string DealWithMsg(UnSubscribeEvent unSubscribeEvent)
        {
            return "";
        }

        private string DealWithMsg(ScanSubscribeEvent scanSubscribeEvent)
        {
            return "";
        }

        private string DealWithMsg(ScanEvent scanEvent)
        {
            return "";
        }

        private string DealWithMsg(LocationEvent locationEvent)
        {
            return "";
        }

        private string DealWithMsg(ClickEvent clickEvent)
        {
            return "";
        }

        private string DealWithMsg(ViewEvent viewEvent)
        {
            return "";
        }

        private string DealWithMsg(MassSendJobFinishEvent massSendJobFinishEvent)
        {
            return "";
        }

        private string DealWithMsg(TemplateSendJobFinishEvent templateSendJobFinishEvent)
        {
            return "";
        }
    }
}
