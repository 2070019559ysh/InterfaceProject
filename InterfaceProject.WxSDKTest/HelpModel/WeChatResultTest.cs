using InterfaceProject.WxSDK.HelpModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterfaceProject.WxSDKTest.HelpModel
{
    [TestClass]
    public class WeChatResultTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            WeChatResult<AccessTokenTest> wechatResult1 = new WeChatResult<AccessTokenTest>(null);
            WeChatResult<AccessTokenTest> wechatResult2 = new WeChatResult<AccessTokenTest>(string.Empty);
            WeChatResult<AccessTokenTest> wechatResult3 = new WeChatResult<AccessTokenTest>("{\"access_token\":\"ACCESS_TOKEN\",\"expires_in\":7200}");
            WeChatResult<object> wechatResult4 = new WeChatResult<object>("{\"errcode\":40013,\"errmsg\":\"invalid appid\"}");
            WeChatResult<string> wechatResult5 = new WeChatResult<string>("{\"errcode\":40013,\"errmsg\":\"invalid appid\"}");
            Assert.IsNotNull(wechatResult4);
        }

        public class AccessTokenTest
        {
            public string access_token;

            public int expires_in;
        }
    }
}
