using InterfaceProject.NetWxSDK.HelpModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 对接微信自定义菜单接口
    /// </summary>
    public interface ICustomMenuLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        CustomMenuLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="menuButton">菜单按钮数组</param>
        /// <returns>微信服务返回的创建结果</returns>
        WeChatResult CreateMenu(List<MenuButton> menuButtonList);

        /// <summary>
        /// 创建个性化菜单按钮，menuButton公众号的所有个性化菜单，最多只能设置为跳转到3个域名下的链接；matchrule共六个字段，均可为空，但至少要有一个匹配信息是不为空的
        /// </summary>
        /// <param name="menuButton">菜单按钮数组，其中链接跳转不能多于3个</param>
        /// <param name="matchRule">matchrule共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的</param>
        /// <returns>微信服务返回的创建结果</returns>
        WeChatResult<MenuCreateInfo> CreatePersonalMenu(List<MenuButton> menuButtonList, MatchRule matchRule);

        /// <summary>
        /// 获取默认菜单和全部个性化菜单信息
        /// </summary>
        /// <returns>微信服务返回的菜单信息</returns>
        WeChatResult<MenuQueryInfo> SearchMenu();

        /// <summary>
        /// 删除默认菜单，同时删除全部个性化菜单
        /// </summary>
        /// <returns>微信服务返回的删除结果</returns>
        WeChatResult DeleteMenu();

        /// <summary>
        /// 删除指定的个性化菜单
        /// </summary>
        /// <param name="menuId">个性化菜单的Id</param>
        /// <returns>微信服务返回的删除结果</returns>
        WeChatResult DeleteMenu(string menuId);

        /// <summary>
        /// 查找当前公众号使用的自定义菜单的配置，包括API设置的菜单和公众平台官网通过网站功能发布的菜单
        /// </summary>
        /// <returns>自定义菜单的配置JObject信息</returns>
        WeChatResult<JObject> SearchCustomMenu();

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="userId">user_id可以是粉丝的OpenID，也可以是粉丝的微信号</param>
        /// <returns>微信服务返回的匹配菜单</returns>
        WeChatResult<OrdinaryMenu> TryMatchMenu(string userId);
    }
}
