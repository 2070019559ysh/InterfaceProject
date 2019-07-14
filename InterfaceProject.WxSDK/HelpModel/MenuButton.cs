using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 微信菜单按钮
    /// </summary>
    public class MenuButton
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string name;
    }

    /// <summary>
    /// 父菜单按钮
    /// </summary>
    public class ParentButton:MenuButton
    {
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        public List<MenuButton> sub_button;
    }

    /// <summary>
    /// 点击推事件的菜单按钮
    /// </summary>
    public class ClickButton : MenuButton
    {
        /// <summary>
        /// 按钮类型click
        /// </summary>
        [JsonProperty]
        public const string type = "click";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 跳转URL的菜单按钮
    /// </summary>
    public class ViewButton : MenuButton
    {
        /// <summary>
        /// 按钮类型view
        /// </summary>
        [JsonProperty]
        public const string type = "view";
        /// <summary>
        /// 微信客户端将会打开的网页URL
        /// </summary>
        public string url;
    }

    /// <summary>
    /// 扫码推事件的菜单按钮
    /// </summary>
    public class ScanCodePushButton : MenuButton
    {
        /// <summary>
        /// 按钮类型scancode_push
        /// </summary>
        [JsonProperty]
        public const string type = "scancode_push";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框的菜单按钮
    /// </summary>
    public class ScanCodeWaitMsgButton : MenuButton
    {
        /// <summary>
        /// 按钮类型scancode_waitmsg
        /// </summary>
        [JsonProperty]
        public const string type = "scancode_waitmsg";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 弹出系统拍照发图的菜单按钮
    /// </summary>
    public class PicSysPhotoButton : MenuButton
    {
        /// <summary>
        /// 按钮类型pic_sysphoto
        /// </summary>
        [JsonProperty]
        public const string type = "pic_sysphoto";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 弹出拍照或者相册发图的菜单按钮
    /// </summary>
    public class PicPhotoOrAlbumButton : MenuButton
    {
        /// <summary>
        /// 按钮类型pic_photo_or_album
        /// </summary>
        [JsonProperty]
        public const string type = "pic_photo_or_album";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 弹出微信相册发图器的菜单按钮
    /// </summary>
    public class PicWeixinButton : MenuButton
    {
        /// <summary>
        /// 按钮类型pic_weixin
        /// </summary>
        [JsonProperty]
        public const string type = "pic_weixin";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 弹出地理位置选择器的菜单按钮
    /// </summary>
    public class LocationSelectButton : MenuButton
    {
        /// <summary>
        /// 按钮类型location_select
        /// </summary>
        [JsonProperty]
        public const string type = "location_select";
        /// <summary>
        /// 消息接口推送消息类型为event时，附带的key值
        /// </summary>
        public string key;
    }

    /// <summary>
    /// 下发永久素材的菜单按钮
    /// </summary>
    public class MediaIdButton : MenuButton
    {
        /// <summary>
        /// 按钮类型media_id
        /// </summary>
        [JsonProperty]
        public const string type = "media_id";
        /// <summary>
        /// 永久素材Id，永久素材类型可以是图片、音频、视频、图文消息
        /// </summary>
        public string media_id;
    }

    /// <summary>
    /// 跳转图文消息URL的菜单按钮
    /// </summary>
    public class ViewLimitedButton : MenuButton
    {
        /// <summary>
        /// 按钮类型view_limited
        /// </summary>
        [JsonProperty]
        public const string type = "view_limited";
        /// <summary>
        /// 永久图文消息素材Id，微信客户端将打开永久素材id对应的图文消息URL，永久素材类型只能是图文消息
        /// </summary>
        public string media_id;
    }

    /// <summary>
    /// 微信小程序的菜单按钮
    /// </summary>
    public class MiniProgramButton : MenuButton
    {
        /// <summary>
        /// 按钮类型miniprogram
        /// </summary>
        [JsonProperty]
        public const string type = "miniprogram";
        /// <summary>
        /// 不支持小程序的老版本客户端将会打开的网页URL
        /// </summary>
        public string url;
        /// <summary>
        /// 小程序的appid（仅认证公众号可配置）
        /// </summary>
        public string appid;
        /// <summary>
        /// 小程序的页面路径
        /// </summary>
        public string pagepath;
    }

    /// <summary>
    /// 普通的默认微信菜单
    /// </summary>
    public class OrdinaryMenu
    {
        /// <summary>
        /// 微信菜单按钮，数量为1~3个
        /// </summary>
        public List<MenuButton> button;
        /// <summary>
        /// 微信菜单Id
        /// </summary>
        public string menuid;
    }

    /// <summary>
    /// 区分用户显示的个性化菜单
    /// </summary>
    public class PersonalMenu : OrdinaryMenu
    {
        /// <summary>
        /// 个性化菜单的匹配规则
        /// </summary>
        public MatchRule matchrule;
    }

    /// <summary>
    /// 微信菜单查询结果信息
    /// </summary>
    public class MenuQueryInfo
    {
        /// <summary>
        /// 普通微信菜单
        /// </summary>
        public OrdinaryMenu menu;
        /// <summary>
        /// 多个个性化菜单
        /// </summary>
        public List<PersonalMenu> conditionalmenu;
    }

    /// <summary>
    /// 微信菜单创建成功的结果信息
    /// </summary>
    public class MenuCreateInfo
    {
        /// <summary>
        /// 创建好的微信菜单Id
        /// </summary>
        public string menuid;
    }
}
