using InterfaceProject.NetTool;
using InterfaceProject.NetTool.Log;
using InterfaceProject.NetWxSDK.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 对接微信自定义菜单
    /// </summary>
    public class CustomMenuLinkUp: ICustomMenuLinkUp
    {
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;

        /// <summary>
        /// 实例化微信自定义菜单处理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat"></param>
        public CustomMenuLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        public CustomMenuLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            return this;
        }

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="menuButton">菜单按钮数组</param>
        /// <returns>微信服务返回的创建结果</returns>
        public WeChatResult CreateMenu(List<MenuButton> menuButtonList)
        {
            if (menuButtonList == null) return null;
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + accessToken;//POST
            string menuBtnData = JsonConvert.SerializeObject(new { button = menuButtonList });
            string resultStr = SimulateRequest.HttpPost(url, menuBtnData);
            WeChatResult weChatResult = new WeChatResult(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"创建自定义菜单CreateMenu，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 创建个性化菜单按钮，menuButton公众号的所有个性化菜单，最多只能设置为跳转到3个域名下的链接；matchrule共六个字段，均可为空，但至少要有一个匹配信息是不为空的
        /// </summary>
        /// <param name="menuButton">菜单按钮数组，其中链接跳转不能多于3个</param>
        /// <param name="matchRule">matchrule共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的</param>
        /// <returns>微信服务返回的创建结果</returns>
        public WeChatResult<MenuCreateInfo> CreatePersonalMenu(List<MenuButton> menuButtonList, MatchRule matchRule)
        {
            if (menuButtonList == null) return null;
            if (matchRule == null)
                matchRule = new MatchRule()
                {
                    country = "中国",
                    province = "广东",
                    language = "zh_CN"
                };
            string menuBtnData = JsonConvert.SerializeObject(new { button = menuButtonList, matchrule = "{0}" });
            //开始生成菜单按钮的个性化匹配规则
            //country、province、city组成地区信息，将按照country、province、city的顺序进行验证，要符合地区信息表的内容。
            //地区信息从大到小验证
            StringBuilder ruleBuilder = new StringBuilder();
            ruleBuilder.Append("{");
            if (!string.IsNullOrEmpty(matchRule.tag_id))
            {
                ruleBuilder.Append($"\"tag_id\":\"{matchRule.tag_id}\",");
            }
            if (matchRule.sex.HasValue)
            {
                ruleBuilder.Append($"\"sex\":\"{matchRule.sex}\",");
            }
            if (!string.IsNullOrEmpty(matchRule.country))
            {
                ruleBuilder.Append($"\"country\":\"{matchRule.country}\",");
            }
            if (!string.IsNullOrEmpty(matchRule.province))
            {
                ruleBuilder.Append($"\"province\":\"{matchRule.province}\",");
            }
            if (!string.IsNullOrEmpty(matchRule.city))
            {
                ruleBuilder.Append($"\"city\":\"{matchRule.city}\",");
            }
            if (matchRule.client_platform_type.HasValue)
            {
                ruleBuilder.Append($"\"client_platform_type\":\"{matchRule.client_platform_type}\",");
            }
            if (!string.IsNullOrEmpty(matchRule.language))
            {
                ruleBuilder.Append($"\"language\":\"{matchRule.language}\",");
            }
            if (ruleBuilder.ToString().EndsWith(","))
            {
                ruleBuilder.Remove(ruleBuilder.Length - 1, 1);
            }
            ruleBuilder.Append("}");
            string[] menuBtnSplitAry = menuBtnData.Split(new string[] { "\"{0}\"" }, StringSplitOptions.RemoveEmptyEntries);//把"{0}"替换成matchRule对象
            menuBtnData = string.Join(ruleBuilder.ToString(), menuBtnSplitAry);
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token=" + accessToken;//POST
            string resultStr = SimulateRequest.HttpPost(url, menuBtnData);
            WeChatResult<MenuCreateInfo> weChatResult = new WeChatResult<MenuCreateInfo>(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"创建个性化菜单CreatePersonalMenu，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        //private static List<MenuButton> LimitMenuNum(List<MenuButton> menuButtonList)
        //{
        //    //开始生成微信菜单按钮的请求字符串
        //    int minLength = Math.Min(menuButtonList.Count, 3);
        //    if (menuButtonList.Count > 3)
        //        menuButtonList = menuButtonList.Take(3).ToList();//最多包括3个一级菜单
        //    menuButtonList.ForEach(menuBtn =>
        //    {
        //        if (menuBtn is ParentButton)
        //        {
        //            var parentBtn = menuBtn as ParentButton;
        //            if (parentBtn.sub_button != null && parentBtn.sub_button.Count > 5)
        //                parentBtn.sub_button = parentBtn.sub_button.Take(5).ToList();//每个一级菜单最多包含5个二级菜单
        //        }
        //    });
        //    return menuButtonList;
        //}

        /// <summary>
        /// 获取默认菜单和全部个性化菜单信息
        /// </summary>
        /// <returns>微信服务返回的菜单信息</returns>
        public WeChatResult<MenuQueryInfo> SearchMenu()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult<MenuQueryInfo> menuResult = new WeChatResult<MenuQueryInfo>(resultStr);
            if (menuResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取默认菜单和全部个性化菜单信息SearchMenu()，微信服务报错：{menuResult}");
                return menuResult;
            }
            JObject menuJobj = JObject.Parse(resultStr);
            JToken ordinaryMenu = menuJobj["menu"];//一个默认菜单
            if (ordinaryMenu == null) return menuResult;
            JArray menuButtonArray = ordinaryMenu["button"] as JArray;//默认菜单中的1~3个主菜单
            if (menuButtonArray == null) return menuResult;
            MenuQueryInfo menuQueryInfo = menuResult.resultData;
            AnalysisMenuButton(menuButtonArray, menuQueryInfo.menu.button);
            JArray personalMenuArray = menuJobj["conditionalmenu"] as JArray;//多个个性化菜单
            if (personalMenuArray == null) return menuResult;
            foreach (JToken personalMenu in personalMenuArray)
            {
                if (personalMenu == null) return menuResult;
                int index = personalMenuArray.IndexOf(personalMenu);
                JArray personalButtonArray = personalMenu["button"] as JArray;//默认菜单中的1~3个主菜单
                if (personalButtonArray == null) return menuResult;
                AnalysisMenuButton(personalMenu["button"] as JArray, menuQueryInfo.conditionalmenu[index].button);
            }
            return menuResult;
        }

        /// <summary>
        /// 分析JArray里包含的微信菜单信息，并保存到菜单数组中
        /// </summary>
        /// <param name="menuButtonArray">JArray微信菜单数据</param>
        /// <param name="menuButtons">保存解析好的菜单信息</param>
        private void AnalysisMenuButton(JArray menuButtonArray,List<MenuButton> menuButtons)
        {
            if (menuButtonArray == null) return;
            if (menuButtons == null)//先清空下现有的默认菜单信息
                menuButtons = new List<MenuButton>();
            else
                menuButtons.Clear();
            foreach (JToken menuButton in menuButtonArray)
            {
                JArray subButtonArray = menuButton["sub_button"] as JArray;
                MenuButton nowMenuButton;
                if (subButtonArray != null && subButtonArray.Count > 0)
                {
                    ParentButton parentButton = JsonConvert.DeserializeObject<ParentButton>(menuButton.ToString());//确认为父菜单
                    AnalysisMenuButton(menuButton["sub_button"] as JArray, parentButton.sub_button);
                    nowMenuButton = parentButton;
                }
                else
                {
                    string buttonType = menuButton["type"]?.ToString();//微信按钮类型
                    switch (buttonType)
                    {
                        case ClickButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<ClickButton>(menuButton.ToString());
                            break;
                        case ViewButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<ViewButton>(menuButton.ToString());
                            break;
                        case ScanCodePushButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<ScanCodePushButton>(menuButton.ToString());
                            break;
                        case ScanCodeWaitMsgButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<ScanCodeWaitMsgButton>(menuButton.ToString());
                            break;
                        case PicSysPhotoButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<PicSysPhotoButton>(menuButton.ToString());
                            break;
                        case PicPhotoOrAlbumButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<PicPhotoOrAlbumButton>(menuButton.ToString());
                            break;
                        case PicWeixinButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<PicWeixinButton>(menuButton.ToString());
                            break;
                        case LocationSelectButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<LocationSelectButton>(menuButton.ToString());
                            break;
                        case MediaIdButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<MediaIdButton>(menuButton.ToString());
                            break;
                        case ViewLimitedButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<ViewLimitedButton>(menuButton.ToString());
                            break;
                        case MiniProgramButton.type:
                            nowMenuButton = JsonConvert.DeserializeObject<MiniProgramButton>(menuButton.ToString());
                            break;
                        default:
                            nowMenuButton = JsonConvert.DeserializeObject<MenuButton>(menuButton.ToString());
                            break;
                    }
                }
                menuButtons.Add(nowMenuButton);//记录下一个解析好的按钮数据
            }
        }

        /// <summary>
        /// 删除默认菜单，同时删除全部个性化菜单
        /// </summary>
        /// <returns>微信服务返回的删除结果</returns>
        public WeChatResult DeleteMenu()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult deleteResult = new WeChatResult(resultStr);
            if (deleteResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"删除默认菜单DeleteMenu()，微信服务报错：{deleteResult}");
            }
            return deleteResult;
        }

        /// <summary>
        /// 删除指定的个性化菜单
        /// </summary>
        /// <param name="menuId">个性化菜单的Id</param>
        /// <returns>微信服务返回的删除结果</returns>
        public WeChatResult DeleteMenu(string menuId)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delconditional?access_token=" + accessToken;
            var delMenu = new { menuid = menuId };
            string resultStr = SimulateRequest.HttpPost(url, delMenu, "application/json");
            WeChatResult deleteResult = new WeChatResult(resultStr);
            if (deleteResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"删除指定的个性化菜单DeleteMenu()，微信服务报错：{deleteResult}");
            }
            return deleteResult;
        }

        /// <summary>
        /// 查找当前公众号使用的自定义菜单的配置，包括API设置的菜单和公众平台官网通过网站功能发布的菜单
        /// </summary>
        /// <returns>自定义菜单的配置JObject信息</returns>
        public WeChatResult<JObject> SearchCustomMenu()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult<JObject> customMenuResult = new WeChatResult<JObject>(resultStr);
            if (customMenuResult.errcode!=WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"查找当前公众号使用的自定义菜单的配置SearchCustomMenu()，微信服务报错：{customMenuResult}");
            }
            return customMenuResult;
        }

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="userId">user_id可以是粉丝的OpenID，也可以是粉丝的微信号</param>
        /// <returns>微信服务返回的匹配菜单</returns>
        public WeChatResult<OrdinaryMenu> TryMatchMenu(string userId)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/trymatch?access_token=" + accessToken;
            var matchParam = new { user_id = userId };
            string resultStr = SimulateRequest.HttpPost(url, matchParam, "application/json");
            WeChatResult<OrdinaryMenu> matchMenuResult = new WeChatResult<OrdinaryMenu>(resultStr);
            if (matchMenuResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"测试个性化菜单匹配结果TryMatchMenu()，微信服务报错：{matchMenuResult}");
            }
            return matchMenuResult;
        }
    }
}
