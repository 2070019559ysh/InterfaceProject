using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 对接微信群发接口的声明
    /// </summary>
    public interface IGroupSendLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        GroupSendLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="filePathName">包含完整访问路径的图片文件名</param>
        /// <returns>包含图片URL的微信响应结果</returns>
        WeChatResult<WeChatURL> GetUrlByUpdateImg(string filePathName);

        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="articleNews">图文消息素材</param>
        /// <returns>上传图文消息素材结果</returns>
        WeChatResult<UploadNewsResult> UpdateNews(ArticleNews articleNews);

        /// <summary>
        /// 根据标签进行群发
        /// </summary>
        /// <param name="tagSendParam">标签群发参数</param>
        /// <returns>标签进行群发结果</returns>
        WeChatResult<GroupSendResult> SendAll(TagSendParam tagSendParam);

        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        /// <param name="tagSendParam">OpenID列表群发参数</param>
        /// <returns>OpenID列表群发结果</returns>
        WeChatResult<GroupSendResult> SendAll(OpenIdSendParam openIdSendParam);

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="articleIndex">要删除的文章在图文消息中的位置，第一篇编号为1，该字段不填或填0会删除全部文章</param>
        /// <returns>删除结果</returns>
        WeChatResult DeleteSendInfo(int msgId, int articleIndex = 0);

        /// <summary>
        /// 群发消息预览，微信支持多种预览账号，但每次只能一个，所以OpenIdSendParam.touser参数只第一个值有效并转成微信需要的参数键名
        /// </summary>
        /// <param name="previewAccount">touser字段都可以改为towxname，这样就可以针对微信号进行预览（而非openID），towxname和touser同时赋值时，以towxname优先</param>
        /// <param name="openIdSendParam">与实际群发参数一致</param>
        /// <returns>群发消息预览发送结果</returns>
        WeChatResult<GroupSendResult> PreviewSendAll(PreviewAccount previewAccount, OpenIdSendParam openIdSendParam);

        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns>发送状态查询结果</returns>
        WeChatResult<GroupSendStatusResult> GetSendStatus(int msgId);

        /// <summary>
        /// 设置群发速度
        /// </summary>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns>发送状态查询结果</returns>
        WeChatResult SetSendSpeed(int speed);

        /// <summary>
        /// 获取群发速度
        /// </summary>
        /// <returns>发送状态查询结果</returns>
        WeChatResult<GroupSendSpeed> GetSendSpeed();
    }
}
