using InterfaceProject.Tool;
using InterfaceProject.Tool.HelpModel;
using InterfaceProject.Tool.Log;
using InterfaceProject.WxSDK.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 对接微信群发接口
    /// </summary>
    public class GroupSendLinkUp: IGroupSendLinkUp
    {
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;

        /// <summary>
        /// 实例化微信群发接口处理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat"></param>
        public GroupSendLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信群发实例本身</returns>
        public GroupSendLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            return this;
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="filePathName">包含完整访问路径的图片文件名</param>
        /// <returns>包含图片URL的微信响应结果</returns>
        public WeChatResult<WeChatURL> GetUrlByUpdateImg(string filePathName)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={accessToken}";
            string fileName = Path.GetFileName(filePathName);
            using (Stream fileStream = new FileStream(filePathName, FileMode.Open))
            {
                string resultStr = SimulateRequest.UploadFile(new UploadFileParam(url, fileName, fileStream));
                WeChatResult<WeChatURL> weChatResult = new WeChatResult<WeChatURL>(resultStr);
                if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
                {
                    SystemLogHelper.Warn(GetType().FullName, $"上传图文消息内的图片获取URLGetUrlByUpdateImg，微信服务报错：{weChatResult}");
                }
                return weChatResult;
            }
        }

        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="articleNews">图文消息素材</param>
        /// <returns>上传图文消息素材结果</returns>
        public WeChatResult<UploadNewsResult> UpdateNews(ArticleNews articleNews)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={accessToken}";
            string resultStr = SimulateRequest.HttpPost(url, articleNews);
            WeChatResult<UploadNewsResult> weChatResult = new WeChatResult<UploadNewsResult>(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"上传图文消息素材UpdateNews，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 根据标签进行群发
        /// </summary>
        /// <param name="tagSendParam">标签群发参数</param>
        /// <returns>标签进行群发结果</returns>
        public WeChatResult<GroupSendResult> SendAll(TagSendParam tagSendParam)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={accessToken}";
            string resultData = SimulateRequest.HttpPost(url, tagSendParam);
            WeChatResult<GroupSendResult> weChatResult = new WeChatResult<GroupSendResult>(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"根据标签进行群发SendAll，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="tagSendParam">OpenID列表群发参数</param>
        /// <returns>OpenID列表群发结果</returns>
        public WeChatResult<GroupSendResult> SendAll(OpenIdSendParam openIdSendParam)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={accessToken}";
            string resultData = SimulateRequest.HttpPost(url, openIdSendParam);
            WeChatResult<GroupSendResult> weChatResult = new WeChatResult<GroupSendResult>(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"根据OpenID列表群发SendAll，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="articleIndex">要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <returns>删除结果</returns>
        public WeChatResult DeleteSendInfo(int msgId, int articleIndex = 0)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={accessToken}";
            string resultData = SimulateRequest.HttpPost(url,
                new
                {
                    msg_id = msgId,
                    article_idx = articleIndex
                });
            WeChatResult weChatResult = new WeChatResult(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"上传图文消息素材UpdateNews，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 群发消息预览，微信支持多种预览账号，但每次只能一个，所以OpenIdSendParam.touser参数只第一个值有效并转成微信需要的参数键名
        /// </summary>
        /// <param name="previewAccount">touser字段都可以改为towxname，这样就可以针对微信号进行预览（而非openID），towxname和touser同时赋值时，以towxname优先</param>
        /// <param name="openIdSendParam">与实际群发参数一致</param>
        /// <returns>群发消息预览发送结果</returns>
        public WeChatResult<GroupSendResult> PreviewSendAll(PreviewAccount previewAccount ,OpenIdSendParam openIdSendParam)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={accessToken}";
            if (openIdSendParam.touser == null || openIdSendParam.touser.Count == 0)
            {
                throw new ArgumentException("OpenIdSendParam.touser必须指定最少一个预览账号");
            }
            string previewWx = openIdSendParam.touser[0];
            string originJson =JsonConvert.SerializeObject(openIdSendParam);
            JObject jobject = JObject.Parse(originJson);
            jobject.Remove("touser");
            if (previewAccount == PreviewAccount.OpenId)
            {
                jobject.Add($"\"touser\":\"{previewWx}\"");
            }
            else
            {
                jobject.Add($"\"towxname\":\"{previewWx}\"");
            }
            string resultStr = SimulateRequest.HttpPost(url,jobject.ToString());
            WeChatResult<GroupSendResult> weChatResult = JsonConvert.DeserializeObject<WeChatResult<GroupSendResult>>(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"群发消息预览PreviewSendAll，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns>发送状态查询结果</returns>
        public WeChatResult<GroupSendStatusResult> GetSendStatus(int msgId)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/get?access_token={accessToken}";
            string resultData= SimulateRequest.HttpPost(url, new { msg_id = msgId });
            WeChatResult<GroupSendStatusResult> weChatResult = new WeChatResult<GroupSendStatusResult>(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"查询群发消息发送状态GetSendStatus，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 设置群发速度
        /// </summary>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns>发送状态查询结果</returns>
        public WeChatResult SetSendSpeed(int speed)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/speed/set?access_token={accessToken}";
            string resultData = SimulateRequest.HttpPost(url, new { speed });
            WeChatResult weChatResult = new WeChatResult(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"设置群发速度SetSendSpeed，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 获取群发速度
        /// </summary>
        /// <returns>发送状态查询结果</returns>
        public WeChatResult<GroupSendSpeed> GetSendSpeed()
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/message/mass/speed/get?access_token={accessToken}";
            WeChatResult<GroupSendSpeed> weChatResult = SimulateRequest
                .HttpGet<WeChatResult<GroupSendSpeed>>(url);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"获取群发速度GetSendSpeed，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }
    }
}
