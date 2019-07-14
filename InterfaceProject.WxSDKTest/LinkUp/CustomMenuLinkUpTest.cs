using InterfaceProject.WxSDK.LinkUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using InterfaceProject.WxSDK.HelpModel;
using Newtonsoft.Json.Linq;
using InterfaceProject.WxSDKTest.HelpModel;

namespace InterfaceProject.WxSDKTest.LinkUp
{
    /// <summary>
    /// 对接微信自定义菜单的测试
    /// </summary>
    [TestClass]
    public class CustomMenuLinkUpTest: ServiceInjectTestBase
    {
        private ICustomMenuLinkUp customMenuLinkUp;
        private static string menuId;

        /// <summary>
        /// 实例化自定义菜单的测试
        /// </summary>
        public CustomMenuLinkUpTest()
        {
            customMenuLinkUp = serviceProvider.GetService<ICustomMenuLinkUp>();
            customMenuLinkUp.Initialize(TestConst.WX_APPID);
        }

        /// <summary>
        /// 测试创建自定义菜单
        /// </summary>
        [TestMethod]
        public void CreateMenuTest()
        {
            List<MenuButton> menuButtons = new List<MenuButton>()
            {
                new ParentButton()
                {
                    name="我的网站",
                    sub_button=new List<MenuButton>()
                    {
                        new ViewButton(){name="网盘",url="http://www.yshdevelop.club"},
                        new ViewButton(){name="GitLib",url="http://www.yshdevelop.club:10101"}
                    }
                },
                new ParentButton()
                {
                    name="微信功能",
                    sub_button=new List<MenuButton>()
                    {
                        new PicWeixinButton(){name="微信相册",key="WeixinPicture"},
                        new PicSysPhotoButton(){name="系统拍照发图",key="SysPhoto"},
                        new PicPhotoOrAlbumButton(){name="拍照或相册",key="PhotoOrAlbum"}
                    }
                },
                new ViewButton(){name="百度一下",url="http://m.baidu.com"}
            };
            WeChatResult createResult = customMenuLinkUp.CreateMenu(menuButtons);
            Assert.IsTrue(createResult != null && createResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试创建个性化菜单
        /// </summary>
        [TestMethod]
        public void CreatePersonalMenuTest()
        {
            List<MenuButton> menuButtons = new List<MenuButton>()
            {
                new ParentButton()
                {
                    name="MyWeb",
                    sub_button=new List<MenuButton>()
                    {
                        new ViewButton(){name="SkyDrive",url="http://www.yshdevelop.club"},
                        new ViewButton(){name="GitBLib",url="http://www.yshdevelop.club:10101"}
                    }
                },
                new ParentButton()
                {
                    name="WeChatFunction",
                    sub_button=new List<MenuButton>()
                    {
                        new PicWeixinButton(){name="Picture",key="WeixinPicture"},
                        new PicSysPhotoButton(){name="SysPhoto",key="SysPhoto"},
                        new PicPhotoOrAlbumButton(){name="PhotoOrAlbum",key="PhotoOrAlbum"}
                    }
                },
                new ViewButton(){name="BaiDu",url="http://m.baidu.com"}
            };
            MatchRule matchRule = new MatchRule()
            {
                language="en"
            };
            WeChatResult<MenuCreateInfo> createResult = customMenuLinkUp.CreatePersonalMenu(menuButtons, matchRule);
            menuId = createResult.resultData.menuid;
            Assert.IsTrue(createResult != null && createResult.errcode == WeChatErrorCode.SUCCESS);
            
        }

        /// <summary>
        /// 测试删除微信菜单
        /// </summary>
        [TestMethod]
        public void DeleteMenuTest()
        {
            WeChatResult deleteResult;
            if (string.IsNullOrWhiteSpace(menuId))
                deleteResult = customMenuLinkUp.DeleteMenu();
            else
                deleteResult = customMenuLinkUp.DeleteMenu(menuId);
            Assert.IsTrue(deleteResult != null && deleteResult.errcode == WeChatErrorCode.SUCCESS);
        }

        /// <summary>
        /// 测试查询自定义菜单
        /// </summary>
        [TestMethod]
        public void SearchMenuTest()
        {
            WeChatResult<MenuQueryInfo> menuQueryResult = customMenuLinkUp.SearchMenu();
            Assert.IsTrue(menuQueryResult != null && menuQueryResult.resultData != null);
            WeChatResult<JObject> menuJobjResult = customMenuLinkUp.SearchCustomMenu();
            Assert.IsTrue(menuJobjResult != null && menuJobjResult.resultData != null);
        }
    }
}
