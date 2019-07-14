using InterfaceProject.WxSDK.LinkUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using InterfaceProject.WxSDKTest.HelpModel;
using InterfaceProject.WxSDK.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InterfaceProject.Tool;

namespace InterfaceProject.WxSDKTest.LinkUp
{
    /// <summary>
    /// 微信素材管理单元测试类
    /// </summary>
    [TestClass]
    public class MaterialLinkUpTest : ServiceInjectTestBase
    {
        private readonly IMaterialLinkUp materialLinkUp;

        public MaterialLinkUpTest()
        {
            materialLinkUp = serviceProvider.GetService<IMaterialLinkUp>();
            materialLinkUp.Initialize(TestConst.WX_APPID);
        }

        [TestMethod]
        public void AddTempMaterialTest()
        {
            WeChatResult<TempMaterialResult> weChatResult = materialLinkUp.AddTempMaterial(
                "G:\\FFOutput\\wxtest.amr");
            WeChatResult<string> fileResult;
            if (string.IsNullOrWhiteSpace(weChatResult.resultData.media_id))
                fileResult = materialLinkUp.GetTempMaterial(weChatResult.resultData.thumb_media_id,
                "F:\\ps\\FileImg\\file.txt"); 
            else
                fileResult = materialLinkUp.GetTempMaterial(weChatResult.resultData.media_id,
                    "F:\\ps\\FileImg\\file.txt");
            try
            {
                JObject videoResult = JsonConvert.DeserializeObject<JObject>(fileResult.resultData);
                string fileName = SimulateRequest.DownloadFile(videoResult["video_url"].ToString(), "F:\\ps\\FileImg\\bigBreast.txt");
                Assert.IsNotNull(fileName);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
            Assert.IsNotNull(fileResult.resultData);
        }
    }
}
