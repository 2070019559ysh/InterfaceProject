using InterfaceProject.WxSDK.HelpModel;
using InterfaceProject.WxSDK.LinkUp;
using InterfaceProject.WxSDKTest.HelpModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDKTest.LinkUp
{
    /// <summary>
    /// 客服消息单元测试
    /// </summary>
    [TestClass]
    public class CustomerServiceMsgLinkUpTest : ServiceInjectTestBase
    {
        private ICustomerServiceMsgLinkUp customerServiceMsgLinkUp;

        /// <summary>
        /// 实例化客服消息单元测试
        /// </summary>
        public CustomerServiceMsgLinkUpTest()
        {
            customerServiceMsgLinkUp = serviceProvider.GetService<ICustomerServiceMsgLinkUp>();
            customerServiceMsgLinkUp.Initialize(TestConst.WX_APPID);
        }

        /// <summary>
        /// 测试新增客服帐号
        /// </summary>
        [TestMethod]
        public void AddAccountTest()
        {
            string account = "hulian@yshdevelop.club";
            string nickName = "互联客服_01";
            string password = "123456";
            WeChatResult addResult = customerServiceMsgLinkUp.AddAccount(account, nickName, password);
            Assert.IsTrue(addResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试修改客服帐号
        /// </summary>
        [TestMethod]
        public void UpdateAccountTest()
        {
            string account = "hulian@yshdevelop.club";
            string nickName = "互联客服_02";
            string password = "123456";
            WeChatResult wechatResult = customerServiceMsgLinkUp.UpdateAccount(account, nickName, password);
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试删除客服帐号
        /// </summary>
        [TestMethod]
        public void DeleteAccountTest()
        {
            string account = "hulian@yshdevelop.club";
            string nickName = "互联客服_02";
            string password = "123456";
            WeChatResult wechatResult = customerServiceMsgLinkUp.DeleteAccount(account, nickName, password);
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试设置客服帐号的头像
        /// </summary>
        [TestMethod]
        public void UpdateHeadImgTest()
        {
            string account = "hulian@yshdevelop.club";
            string filePathName = "F:\\ps\\0ff.jpg";
            WeChatResult wechatResult = customerServiceMsgLinkUp.UpdateHeadImg(account, filePathName);
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试获取所有客服账号
        /// </summary>
        [TestMethod]
        public void SearchKFListTest()
        {
            WeChatResult<KFAccountInfo> wechatResult = customerServiceMsgLinkUp.SearchKFList();
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试发送客服消息给指定微信用户
        /// </summary>
        [TestMethod]
        public void SendMsgTest()
        {
            CustomerServiceMsg serviceMsg = new ContentKFMsg() {
                touser ="",
                text =new Text_Msg() {
                    content ="您好，互联01客服为您服务。"
                },
                customservice = new KFAccount()
                {
                    kf_account = "hulian@yshdevelop.club"
                }
            };
            WeChatResult wechatResult = customerServiceMsgLinkUp.SendMsg(serviceMsg);
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试发送客服输入状态
        /// </summary>
        [TestMethod]
        public void SendTypingTest()
        {
            string openId = "";
            WeChatResult wechatResult = customerServiceMsgLinkUp.SendTyping(openId, WxMsgCommand.Typing);
            Assert.IsTrue(wechatResult.errcode == WeChatErrorCode.SUCCESS);
        }
    }
}
