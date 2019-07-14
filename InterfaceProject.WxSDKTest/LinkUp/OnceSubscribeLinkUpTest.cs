using InterfaceProject.WxSDK.LinkUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using InterfaceProject.WxSDKTest.HelpModel;
using InterfaceProject.WxSDK.HelpModel;

namespace InterfaceProject.WxSDKTest.LinkUp
{
    /// <summary>
    /// 微信一次性订阅测试类
    /// </summary>
    [TestClass]
    public class OnceSubscribeLinkUpTest:ServiceInjectTestBase
    {
        private readonly IOnceSubscribeLinkUp onceSubscribe;
        private readonly WeChatConfig weChatConfig;

        /// <summary>
        /// 实例化一次性订阅测试类
        /// </summary>
        public OnceSubscribeLinkUpTest()
        {
            onceSubscribe = serviceProvider.GetService<IOnceSubscribeLinkUp>();
            onceSubscribe.Initialize(TestConst.WX_APPID);
            weChatConfig = WeChatConfig.Init(TestConst.WX_APPID);
        }

        /// <summary>
        /// 获取需要用户同意授权的访问地址--测试
        /// </summary>
        [TestMethod]
        public void GetAuthorizedUrlTest()
        {
            string authUrl = onceSubscribe.GetAuthorizedUrl(1000, "1uDxHNXwYQfBmXOfPJcjAS3FynHArD8aWMEFNRGSbCc", weChatConfig.Id);
            Assert.IsNotNull(authUrl);
        }
    }
}
