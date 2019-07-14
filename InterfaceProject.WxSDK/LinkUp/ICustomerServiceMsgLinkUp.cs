using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 对接微信的客服消息定义接口
    /// </summary>
    public interface ICustomerServiceMsgLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        CustomerServiceMsgLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 新增客服帐号，本接口为公众号添加客服账号，每个公众号最多添加10个客服账号
        /// </summary>
        /// <param name="account">账号，如：kf@test.com</param>
        /// <param name="nickName">昵称，如：客服1</param>
        /// <param name="password">客服登录密码，内部会转成md5</param>
        /// <returns>新增的微信结果</returns>
        WeChatResult AddAccount(string account, string nickName, string password);

        /// <summary>
        /// 修改客服帐号
        /// </summary>
        /// <param name="account">账号，如：kf@test.com</param>
        /// <param name="nickName">昵称，如：客服1</param>
        /// <param name="password">客服登录密码，内部会转成md5</param>
        /// <returns>修改的微信结果</returns>
        WeChatResult UpdateAccount(string account, string nickName, string password);

        /// <summary>
        /// 删除客服帐号
        /// </summary>
        /// <param name="account">账号，如：kf@test.com</param>
        /// <param name="nickName">昵称，如：客服1</param>
        /// <param name="password">客服登录密码，内部会转成md5</param>
        /// <returns>删除的微信结果</returns>
        WeChatResult DeleteAccount(string account, string nickName, string password);

        /// <summary>
        /// 设置客服帐号的头像，头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果
        /// </summary>
        /// <param name="account">账号，如：kf@test.com</param>
        /// <param name="filePathName">包含完整访问路径的文件名，文件必须是jpg格式，推荐使用640*640大小的图片</param>
        /// <returns>设置头像的微信结果</returns>
        WeChatResult UpdateHeadImg(string account, string filePathName);

        /// <summary>
        /// 获取所有客服账号，获取公众号中所设置的客服基本信息，包括客服工号、客服昵称、客服登录账号
        /// </summary>
        /// <param name="account">账号，如：kf@test.com</param>
        /// <param name="nickName">昵称，如：客服1</param>
        /// <param name="password">客服登录密码，内部会转成md5</param>
        /// <returns>删除的微信结果</returns>
        WeChatResult<KFAccountInfo> SearchKFList();

        /// <summary>
        /// 发送客服消息给指定微信用户，可指定客服账号
        /// </summary>
        /// <param name="serviceMsg">需发送的客服消息，可指定客服账号</param>
        /// <returns>发送客服消息的微信结果</returns>
        WeChatResult SendMsg(CustomerServiceMsg serviceMsg);

        /// <summary>
        /// 发送客服输入状态
        /// <para>下发输入状态，需要客服之前30秒内跟用户有过消息交互</para>
        /// <para>在输入状态中（持续15s），不可重复下发输入态</para>
        /// <para>在输入状态中，如果向用户下发消息，会同时取消输入状态</para>
        /// </summary>
        /// <param name="serviceMsg">需发送的客服消息，可指定客服账号</param>
        /// <returns>发送客服消息的微信结果</returns>
        WeChatResult SendTyping(string openId, WxMsgCommand msgCommand);
    }
}
