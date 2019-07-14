using InterfaceProject.WxSDK.LinkUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InterfaceProject.WxSDKTest.HelpModel;
using InterfaceProject.WxSDK.HelpModel;

namespace InterfaceProject.WxSDKTest.LinkUp
{
    /// <summary>
    /// 微信接入类单元测试
    /// </summary>
    [TestClass]
    public class ConnectLinkUpTest:ServiceInjectTestBase
    {
        private readonly IConnectLinkUp connectLinkUp;

        /// <summary>
        /// 实例化微信接入类的测试
        /// </summary>
        public ConnectLinkUpTest()
        {
            connectLinkUp = serviceProvider.GetService<IConnectLinkUp>();
            connectLinkUp.Initialize(TestConst.WX_APPID);
        }

        /// <summary>
        /// 测试获取公众号的AccessToken的并发下的情况
        /// </summary>
        [TestMethod]
        public void GetAccessTokenTest()
        {
            Func<string> runFunc = () =>
               {
                   string accessToken = connectLinkUp.GetAccessToken();
                   return accessToken;
               };
            Task<string> task1 = Task.Run(runFunc);
            Task<string> task2 = Task.Run(runFunc);
            Task<string> task3 = Task.Run(runFunc);
            Task<string> task4 = Task.Run(runFunc);
            Task<string> task5 = Task.Run(runFunc);
            Assert.IsFalse(string.IsNullOrWhiteSpace(task1.Result));
            Assert.IsFalse(string.IsNullOrWhiteSpace(task2.Result));
            Assert.IsFalse(string.IsNullOrWhiteSpace(task3.Result));
            Assert.IsFalse(string.IsNullOrWhiteSpace(task4.Result));
            Assert.IsFalse(string.IsNullOrWhiteSpace(task5.Result));
        }

        /// <summary>
        /// 测试对公众号的所有api调用次数进行清零
        /// </summary>
        [TestMethod]
        public void ApiClearQuotaTest()
        {
            WeChatResult result = connectLinkUp.ApiClearQuota();
            Assert.IsTrue(result.errcode == WeChatErrorCode.SUCCESS);
        }
    }
}
