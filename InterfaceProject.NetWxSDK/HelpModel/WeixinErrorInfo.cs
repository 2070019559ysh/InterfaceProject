using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetChatApp.LinkUp.HelpModel
{
    /// <summary>
    /// 公众号每次调用接口时，可能获得正确或错误的返回码，我们可以根据返回码信息调试接口，排查错误。
    /// </summary>
    public class WeixinErrorInfo
    {
        /// <summary>
        /// 实际微信错误代码
        /// </summary>
        private WeixinErrorCode _wechatErrorCode;
        /// <summary>
        /// 错误码对应的错误描述
        /// </summary>
        private string _errorMsg=String.Empty;

        /// <summary>
        /// 只读，获取错误码对应的错误描述
        /// </summary>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }

        /// <summary>
        /// 只读，获取实际微信错误代码
        /// </summary>
        public WeixinErrorCode WechatErrorCode
        {
            get { return _wechatErrorCode; }
        }

        /// <summary>
        /// 实例化处理微信错误码的对象，以整型错误码解析错误
        /// </summary>
        /// <param name="errorCode">整型错误码</param>
        public WeixinErrorInfo(int errorCode)
        {
            try
            {
                _wechatErrorCode = (WeixinErrorCode)errorCode;
            }
            catch (Exception ex)
            {
                _errorMsg = "暂时无法识别此【" + errorCode + "】错误信息，整型错误代码转错误枚举时出错。\n异常信息：" + ex.Message;
            }
            WeixinErrorCodeToStr();
        }

        /// <summary>
        /// 实例化处理微信错误码的对象，以整数字符串错误码或枚举字符串来解析错误
        /// </summary>
        /// <param name="errorCode">整数字符串错误码或枚举字符串</param>
        public WeixinErrorInfo(string errorCode)
        {
            try
            {
                int errorCodeInt;
                WeixinErrorCode errorCodeEnum;
                if (int.TryParse(errorCode, out errorCodeInt))
                {
                    _wechatErrorCode = (WeixinErrorCode)errorCodeInt;
                }
                else if (Enum.TryParse(errorCode, out errorCodeEnum))
                {
                    _wechatErrorCode = errorCodeEnum;
                }
                else
                {
                    _errorMsg = "暂时无法识别此错误信息，没有配置的错误信息：" + errorCode;
                }
            }
            catch (Exception ex)
            {
                _errorMsg = "暂时无法识别此【" + errorCode + "】错误信息，字符串转错误枚举时出错。\n异常信息：" + ex.Message;
            }
            WeixinErrorCodeToStr();
        }

        /// <summary>
        /// 实例化处理微信错误码的对象，以枚举错误码解析错误
        /// </summary>
        /// <param name="errorCode">枚举错误码</param>
        public WeixinErrorInfo(WeixinErrorCode errorCode)
        {
            _wechatErrorCode = errorCode;
            WeixinErrorCodeToStr();
        }

        /// <summary>
        /// 生成错误枚举值对应的错误描述信息
        /// </summary>
        private void WeixinErrorCodeToStr()
        {
            if (!string.IsNullOrEmpty(_errorMsg))
                return;
            switch (_wechatErrorCode)
            {
                case WeixinErrorCode.SystemBusy: _errorMsg = "系统繁忙，此时请开发者稍候再试"; break;
                case WeixinErrorCode.RequestSuccess: _errorMsg = "请求成功"; break;
                case WeixinErrorCode.AppSecretWrong: _errorMsg = "获取access_token时AppSecret错误，或者access_token无效。请开发者认真比对AppSecret的正确性，或查看是否正在为恰当的公众号调用接口"; break;
                case WeixinErrorCode.NotLegalVoucher: _errorMsg = "不合法的凭证类型"; break;
                case WeixinErrorCode.NotLegalOpenId: _errorMsg = "不合法的OpenID，请开发者确认OpenID（该用户）是否已关注公众号，或是否是其他公众号的OpenID"; break;
                case WeixinErrorCode.NotLegalMediaType: _errorMsg = "不合法的媒体文件类型"; break;
                case WeixinErrorCode.NotLegalFileType: _errorMsg = "不合法的文件类型"; break;
                case WeixinErrorCode.AccessTokenOvertime: _errorMsg = "access_token超时，请检查access_token的有效期，请参考基础支持-获取access_token中，对access_token的详细机制说明"; break;
                case WeixinErrorCode.AccountIllegal: _errorMsg = "帐号不合法"; break;
                case WeixinErrorCode.ApiNotAuth: _errorMsg = "api功能未授权，请确认公众号已获得该接口，可以在公众平台官网-开发者中心页中查看接口权限"; break;
                case WeixinErrorCode.AudioTimeOverLimit: _errorMsg = "语音播放时间超过限制"; break;
                case WeixinErrorCode.ButtonTypeError: _errorMsg = "button类型错误"; break;
                case WeixinErrorCode.ButtonTypeError2: _errorMsg = "button类型错误"; break;
                case WeixinErrorCode.CanNotAddEquipment: _errorMsg = "已有设备激活率低于50%，不能新增设备"; break;
                case WeixinErrorCode.CreateSelfMenuFirst:_errorMsg = "还没有默认菜单，请先创建默认菜单";break;
                case WeixinErrorCode.CommentTooLong: _errorMsg = "设备备注信息过长"; break;
                case WeixinErrorCode.DateTimeFormatError: _errorMsg = "日期格式错误"; break;
                case WeixinErrorCode.DateTimeRangeError: _errorMsg = "日期范围错误"; break;
                case WeixinErrorCode.DomainCountReachLimit:_errorMsg = "域名数量达到极限";break;
                case WeixinErrorCode.DateTimeRangeIllegal: _errorMsg = "时间区间不合法"; break;
                case WeixinErrorCode.DeletePageIdMore10: _errorMsg = "一次删除页面ID数量不能超过10"; break;
                case WeixinErrorCode.DiscriptionOverLimit: _errorMsg = "描述字段超过限制"; break;
                case WeixinErrorCode.EquipmentAndPageSaveError: _errorMsg = "保存设备与页面的绑定关系参数错误"; break;
                case WeixinErrorCode.EquipmentApplicationNumIllegal: _errorMsg = "设备申请数不合法，必须为大于0的数字"; break;
                case WeixinErrorCode.EquipmentIdIllegal: _errorMsg = "设备ID不合法"; break;
                case WeixinErrorCode.EquipmentParamIllegal: _errorMsg = "设备申请参数不合法"; break;
                case WeixinErrorCode.ExistsEquipmentApplication: _errorMsg = "已存在审核中的设备ID申请"; break;
                case WeixinErrorCode.FileUploadDefect: _errorMsg = "上传文件缺失"; break;
                case WeixinErrorCode.GetMerchantInfoFail: _errorMsg = "获取商户信息失败"; break;
                case WeixinErrorCode.GetOpenidFail: _errorMsg = "获取OpenID失败"; break;
                case WeixinErrorCode.GetShakePerimeterUserFail: _errorMsg = "获取摇周边用户信息失败"; break;
                case WeixinErrorCode.GroupNameTooLong: _errorMsg = "分组名字过长"; break;
                case WeixinErrorCode.GroupNumOverLimit: _errorMsg = "分组数量超过上限"; break;
                case WeixinErrorCode.InterfaceOvertLimit: _errorMsg = "接口调用超过限制"; break;
                case WeixinErrorCode.InvalidFileType: _errorMsg = "无效头像文件类型(invalid file type)"; break;
                case WeixinErrorCode.InvalidKfAccount: _errorMsg = "无效客服账号(invalid kf_account)"; break;
                case WeixinErrorCode.InvalidKfAccountLength: _errorMsg = "客服帐号名长度超过限制(仅允许10个英文字符，不包括@及@后的公众号的微信号)(invalid kf_acount length)"; break;
                case WeixinErrorCode.JsonOrXmlError: _errorMsg = "解析JSON/XML内容错误"; break;
                case WeixinErrorCode.KfAccountCountExceeded: _errorMsg = "客服帐号个数超过限制(10个客服账号)(kf_account count exceeded)"; break;
                case WeixinErrorCode.KfAccountExisted: _errorMsg = "客服帐号已存在(kf_account existed)"; break;
                case WeixinErrorCode.KfAccountIllegalChar: _errorMsg = "客服帐号名包含非法字符(仅允许英文+数字)(illegal character in kf_account)"; break;
                case WeixinErrorCode.LackAccessTokenParam: _errorMsg = "缺少access_token参数"; break;
                case WeixinErrorCode.LackAppIdParam: _errorMsg = "缺少appid参数"; break;
                case WeixinErrorCode.LackMediaFileData: _errorMsg = "缺少多媒体文件数据"; break;
                case WeixinErrorCode.LackMediaIdParam: _errorMsg = "缺少media_id参数"; break;
                case WeixinErrorCode.LackOAuthCode: _errorMsg = "缺少oauth code"; break;
                case WeixinErrorCode.LackOpenid: _errorMsg = "缺少openid"; break;
                case WeixinErrorCode.LackRefreshTokenParam: _errorMsg = "缺少refresh_token参数"; break;
                case WeixinErrorCode.LackSecretParam: _errorMsg = "缺少secret参数"; break;
                case WeixinErrorCode.LackSubMenuData: _errorMsg = "缺少子菜单数据"; break;
                case WeixinErrorCode.LinkFieldOverLimit: _errorMsg = "链接字段超过限制"; break;
                case WeixinErrorCode.MediaDataNotExists: _errorMsg = "不存在媒体数据"; break;
                case WeixinErrorCode.MediaFileSizeOverLimit: _errorMsg = "多媒体文件大小超过限制"; break;
                case WeixinErrorCode.MeidaFileIsEmpty: _errorMsg = "多媒体文件为空"; break;
                case WeixinErrorCode.MenuDataNotExists: _errorMsg = "不存在的菜单数据"; break;
                case WeixinErrorCode.MenuNumOverLimit: _errorMsg = "创建菜单个数超过限制"; break;
                case WeixinErrorCode.MenuVersionNotExists: _errorMsg = "不存在的菜单版本"; break;
                case WeixinErrorCode.MsgContentOverLimit: _errorMsg = "消息内容超过限制"; break;
                case WeixinErrorCode.NotLegalAccessToken: _errorMsg = "不合法的access_token，请开发者认真比对access_token的有效性（如是否过期），或查看是否正在为恰当的公众号调用接口"; break;
                case WeixinErrorCode.NotLegalAppid: _errorMsg = "不合法的AppID，请开发者检查AppID的正确性，避免异常字符，注意大小写"; break;
                case WeixinErrorCode.NotLegalAudioSize: _errorMsg = "不合法的语音文件大小"; break;
                case WeixinErrorCode.NotLegalBtnKeyLength: _errorMsg = "不合法的按钮KEY长度"; break;
                case WeixinErrorCode.NotLegalBtnNameLength: _errorMsg = "不合法的按钮名字长度"; break;
                case WeixinErrorCode.NotLegalBtnUrlLength: _errorMsg = "不合法的按钮URL长度"; break;
                case WeixinErrorCode.NotLegalButtonNum: _errorMsg = "不合法的按钮个数"; break;
                case WeixinErrorCode.NotLegalButtonNum2: _errorMsg = "不合法的按钮个数"; break;
                case WeixinErrorCode.NotLegalCustomMenu: _errorMsg = "不合法的自定义菜单使用用户"; break;
                case WeixinErrorCode.NotLegalFileSize: _errorMsg = "不合法的文件大小"; break;
                case WeixinErrorCode.NotLegalGroupid: _errorMsg = "不合法的分组id"; break;
                case WeixinErrorCode.NotLegalGroupName: _errorMsg = "分组名字不合法"; break;
                case WeixinErrorCode.NotLegalGroupName2: _errorMsg = "分组名字不合法"; break;
                case WeixinErrorCode.NotLegalMediaFileId: _errorMsg = "不合法的媒体文件id"; break;
                case WeixinErrorCode.NotLegalMediaIdSize: _errorMsg = "media_id大小不合法"; break;
                case WeixinErrorCode.NotLegalMediaIdType: _errorMsg = "不合法的media_id类型"; break;
                case WeixinErrorCode.NotLegalMenuType: _errorMsg = "不合法的菜单类型"; break;
                case WeixinErrorCode.NotLegalMenuVersionNum: _errorMsg = "不合法的菜单版本号"; break;
                case WeixinErrorCode.NotLegalMessageType: _errorMsg = "不合法的消息类型"; break;
                case WeixinErrorCode.NotLegalOauthcode: _errorMsg = "不合法的oauth_code"; break;
                case WeixinErrorCode.NotLegalOpenidList: _errorMsg = "不合法的openid列表"; break;
                case WeixinErrorCode.NotLegalOpenidListLength: _errorMsg = "不合法的openid列表长度"; break;
                case WeixinErrorCode.NotLegalParameter: _errorMsg = "不合法的参数"; break;
                case WeixinErrorCode.NotLegalPhotoSize: _errorMsg = "不合法的图片文件大小"; break;
                case WeixinErrorCode.NotLegalRefreshtoken: _errorMsg = "不合法的refresh_token"; break;
                case WeixinErrorCode.NotLegalRequestChar: _errorMsg = "不合法的请求字符，不能包含\\uxxxx格式的字符"; break;
                case WeixinErrorCode.NotLegalRequestMode: _errorMsg = "不合法的请求格式"; break;
                case WeixinErrorCode.NotLegalSearchInitDataBegin: _errorMsg = "查询起始值begin不合法"; break;
                case WeixinErrorCode.NotLegalSubMenuBtnKeyLength: _errorMsg = "不合法的子菜单按钮KEY长度"; break;
                case WeixinErrorCode.NotLegalSubMenuBtnNameLength: _errorMsg = "不合法的子菜单按钮名字长度"; break;
                case WeixinErrorCode.NotLegalSubMenuBtnNum: _errorMsg = "不合法的子菜单按钮个数"; break;
                case WeixinErrorCode.NotLegalSubMenuBtnType: _errorMsg = "不合法的子菜单按钮类型"; break;
                case WeixinErrorCode.NotLegalSubMenuBtnUrlLength: _errorMsg = "不合法的子菜单按钮URL长度"; break;
                case WeixinErrorCode.NotLegalSubMenuLevel: _errorMsg = "不合法的子菜单级数"; break;
                case WeixinErrorCode.NotLegalThumbnailSize: _errorMsg = "不合法的缩略图文件大小"; break;
                case WeixinErrorCode.NotLegalUrlLength: _errorMsg = "不合法的URL长度"; break;
                case WeixinErrorCode.NotLegalVideoSize: _errorMsg = "不合法的视频文件大小"; break;
                case WeixinErrorCode.NotLegalWeixin: _errorMsg = "微信号不合法"; break;
                case WeixinErrorCode.NotSupportPhotoFormat: _errorMsg = "不支持的图片格式"; break;
                case WeixinErrorCode.OAuthCodeOverTime: _errorMsg = "oauth_code超时"; break;
                case WeixinErrorCode.PageIdIllegal: _errorMsg = "页面ID不合法"; break;
                case WeixinErrorCode.PageParamIllegal: _errorMsg = "页面参数不合法"; break;
                case WeixinErrorCode.PageUseNotDelete: _errorMsg = "页面已应用在设备中，请先解除应用关系再删除"; break;
                case WeixinErrorCode.ParameterError: _errorMsg = "参数错误(invalid parameter)"; break;
                case WeixinErrorCode.PhotoLinkOverLimit: _errorMsg = "图片链接字段超过限制"; break;
                case WeixinErrorCode.PhotoTextMsgIsEmpty: _errorMsg = "图文消息内容为空"; break;
                case WeixinErrorCode.PhotoTextOverLimit: _errorMsg = "图文消息超过限制"; break;
                case WeixinErrorCode.PostDataIsEmpty: _errorMsg = "POST的数据包为空"; break;
                case WeixinErrorCode.PostParamIllegal: _errorMsg = "POST数据参数不合法"; break;
                case WeixinErrorCode.RefreshTokenOvertime: _errorMsg = "refresh_token超时"; break;
                case WeixinErrorCode.RemoteServiceNotAvailable: _errorMsg = "远端服务不可用"; break;
                case WeixinErrorCode.ReplyTimeOverLimit: _errorMsg = "回复时间超过限制"; break;
                case WeixinErrorCode.RequireGetRequest: _errorMsg = "需要GET请求"; break;
                case WeixinErrorCode.RequireGoodFriendRelation: _errorMsg = "需要好友关系"; break;
                case WeixinErrorCode.RequireHttpsRequest: _errorMsg = "需要HTTPS请求"; break;
                case WeixinErrorCode.RequirePostRequest: _errorMsg = "需要POST请求"; break;
                case WeixinErrorCode.RequireUserAttention: _errorMsg = "需要接收者关注"; break;
                case WeixinErrorCode.SearchEquipmentNumMore50: _errorMsg = "一次查询设备ID数量不能超过50"; break;
                case WeixinErrorCode.SearchPageIdMore50: _errorMsg = "一次查询页面ID数量不能超过50"; break;
                case WeixinErrorCode.StoreIdIllegal: _errorMsg = "门店ID不合法"; break;
                case WeixinErrorCode.SystemError: _errorMsg = "系统错误(system error)"; break;
                case WeixinErrorCode.SystemGroupNotUpdate: _errorMsg = "系统分组，不允许修改"; break;
                case WeixinErrorCode.TextMsgIsEmpty: _errorMsg = "文本消息内容为空"; break;
                case WeixinErrorCode.TicketIllegal: _errorMsg = "Ticket不合法"; break;
                case WeixinErrorCode.TitleFieldOverLimit: _errorMsg = "标题字段超过限制"; break;
                case WeixinErrorCode.UploadFail: _errorMsg = "上传失败"; break;
                case WeixinErrorCode.UploadFileSizeIllegal: _errorMsg = "上传素材的文件尺寸不合法"; break;
                case WeixinErrorCode.UploadFileTypeIllegal: _errorMsg = "上传素材的文件类型不合法"; break;
                case WeixinErrorCode.UserBeLimit: _errorMsg = "用户受限，可能是违规后接口被封禁"; break;
                case WeixinErrorCode.UserNotExists: _errorMsg = "不存在的用户"; break;
                case WeixinErrorCode.UserRefuseApiAuth: _errorMsg = "用户未授权该api"; break;
            }
        }

        /// <summary>
        /// 返回错误描述信息
        /// </summary>
        /// <returns>错误描述信息</returns>
        public override string ToString()
        {
            return _errorMsg;
        }
    }

    /// <summary>
    /// 微信返回的错误代码
    /// </summary>
    public enum WeixinErrorCode
    {
        /// <summary>
        /// 系统繁忙，此时请开发者稍候再试
        /// </summary>
        SystemBusy = -1,
        /// <summary>
        /// 请求成功
        /// </summary>
        RequestSuccess = 0,
        /// <summary>
        /// 获取access_token时AppSecret错误，或者AppId无效
        /// </summary>
        AppSecretWrong = 40001,
        /// <summary>
        /// 不合法的凭证类型
        /// </summary>
        NotLegalVoucher = 40002,
        /// <summary>
        /// 不合法的OpenID
        /// </summary>
        NotLegalOpenId = 40003,
        /// <summary>
        /// 不合法的媒体文件类型
        /// </summary>
        NotLegalMediaType = 40004,
        /// <summary>
        /// 不合法的文件类型
        /// </summary>
        NotLegalFileType = 40005,
        /// <summary>
        /// 不合法的文件大小
        /// </summary>
        NotLegalFileSize = 40006,
        /// <summary>
        /// 不合法的媒体文件id
        /// </summary>
        NotLegalMediaFileId = 40007,
        /// <summary>
        /// 不合法的消息类型
        /// </summary>
        NotLegalMessageType = 40008,
        /// <summary>
        /// 不合法的图片文件大小
        /// </summary>
        NotLegalPhotoSize = 40009,
        /// <summary>
        /// 不合法的语音文件大小
        /// </summary>
        NotLegalAudioSize = 40010,
        /// <summary>
        /// 不合法的视频文件大小
        /// </summary>
        NotLegalVideoSize = 40011,
        /// <summary>
        /// 不合法的缩略图文件大小
        /// </summary>
        NotLegalThumbnailSize = 40012,
        /// <summary>
        /// 不合法的AppID
        /// </summary>
        NotLegalAppid = 40013,
        /// <summary>
        /// 不合法的access_token
        /// </summary>
        NotLegalAccessToken = 40014,
        /// <summary>
        /// 不合法的菜单类型
        /// </summary>
        NotLegalMenuType = 40015,
        /// <summary>
        /// 不合法的按钮个数
        /// </summary>
        NotLegalButtonNum = 40016,
        /// <summary>
        /// 不合法的按钮个数
        /// </summary>
        NotLegalButtonNum2 = 40017,
        /// <summary>
        /// 不合法的按钮名字长度
        /// </summary>
        NotLegalBtnNameLength = 40018,
        /// <summary>
        /// 不合法的按钮KEY长度
        /// </summary>
        NotLegalBtnKeyLength = 40019,
        /// <summary>
        /// 不合法的按钮URL长度
        /// </summary>
        NotLegalBtnUrlLength = 40020,
        /// <summary>
        /// 不合法的菜单版本号
        /// </summary>
        NotLegalMenuVersionNum = 40021,
        /// <summary>
        /// 不合法的子菜单级数
        /// </summary>
        NotLegalSubMenuLevel = 40022,
        /// <summary>
        /// 不合法的子菜单按钮个数
        /// </summary>
        NotLegalSubMenuBtnNum = 40023,
        /// <summary>
        /// 不合法的子菜单按钮类型
        /// </summary>
        NotLegalSubMenuBtnType = 40024,
        /// <summary>
        /// 不合法的子菜单按钮名字长度
        /// </summary>
        NotLegalSubMenuBtnNameLength = 40025,
        /// <summary>
        /// 不合法的子菜单按钮KEY长度
        /// </summary>
        NotLegalSubMenuBtnKeyLength = 40026,
        /// <summary>
        /// 不合法的子菜单按钮URL长度
        /// </summary>
        NotLegalSubMenuBtnUrlLength = 40027,
        /// <summary>
        /// 不合法的自定义菜单使用用户
        /// </summary>
        NotLegalCustomMenu = 40028,
        /// <summary>
        /// 不合法的oauth_code
        /// </summary>
        NotLegalOauthcode = 40029,
        /// <summary>
        /// 不合法的refresh_token
        /// </summary>
        NotLegalRefreshtoken = 40030,
        /// <summary>
        /// 不合法的openid列表
        /// </summary>
        NotLegalOpenidList = 40031,
        /// <summary>
        /// 不合法的openid列表长度
        /// </summary>
        NotLegalOpenidListLength = 40032,
        /// <summary>
        /// 不合法的请求字符，不能包含\uxxxx格式的字符
        /// </summary>
        NotLegalRequestChar = 40033,
        /// <summary>
        /// 不合法的参数
        /// </summary>
        NotLegalParameter = 40035,
        /// <summary>
        /// 不合法的请求格式
        /// </summary>
        NotLegalRequestMode = 40038,
        /// <summary>
        /// 不合法的URL长度
        /// </summary>
        NotLegalUrlLength = 40039,
        /// <summary>
        /// 不合法的分组id
        /// </summary>
        NotLegalGroupid = 40050,
        /// <summary>
        /// 分组名字不合法
        /// </summary>
        NotLegalGroupName = 40051,
        /// <summary>
        /// 分组名字不合法
        /// </summary>
        NotLegalGroupName2 = 40117,
        /// <summary>
        /// media_id大小不合法
        /// </summary>
        NotLegalMediaIdSize = 40118,
        /// <summary>
        /// button类型错误
        /// </summary>
        ButtonTypeError = 40119,
        /// <summary>
        /// button类型错误
        /// </summary>
        ButtonTypeError2 = 40120,
        /// <summary>
        /// 不合法的media_id类型
        /// </summary>
        NotLegalMediaIdType = 40121,
        /// <summary>
        /// 微信号不合法
        /// </summary>
        NotLegalWeixin = 40132,
        /// <summary>
        /// 不支持的图片格式
        /// </summary>
        NotSupportPhotoFormat = 40137,
        /// <summary>
        /// 缺少access_token参数
        /// </summary>
        LackAccessTokenParam = 41001,
        /// <summary>
        /// 缺少appid参数
        /// </summary>
        LackAppIdParam = 41002,
        /// <summary>
        /// 缺少refresh_token参数
        /// </summary>
        LackRefreshTokenParam = 41003,
        /// <summary>
        /// 缺少secret参数
        /// </summary>
        LackSecretParam = 41004,
        /// <summary>
        /// 缺少多媒体文件数据
        /// </summary>
        LackMediaFileData = 41005,
        /// <summary>
        /// 缺少media_id参数
        /// </summary>
        LackMediaIdParam = 41006,
        /// <summary>
        /// 缺少子菜单数据
        /// </summary>
        LackSubMenuData = 41007,
        /// <summary>
        /// 缺少oauth code
        /// </summary>
        LackOAuthCode = 41008,
        /// <summary>
        /// 缺少openid
        /// </summary>
        LackOpenid = 41009,
        /// <summary>
        /// access_token超时
        /// </summary>
        AccessTokenOvertime = 42001,
        /// <summary>
        /// refresh_token超时
        /// </summary>
        RefreshTokenOvertime = 42002,
        /// <summary>
        /// oauth_code超时
        /// </summary>
        OAuthCodeOverTime = 42003,
        /// <summary>
        /// 需要GET请求
        /// </summary>
        RequireGetRequest = 43001,
        /// <summary>
        /// 需要POST请求
        /// </summary>
        RequirePostRequest = 43002,
        /// <summary>
        /// 需要HTTPS请求
        /// </summary>
        RequireHttpsRequest = 43003,
        /// <summary>
        /// 需要接收者关注
        /// </summary>
        RequireUserAttention = 43004,
        /// <summary>
        /// 需要好友关系
        /// </summary>
        RequireGoodFriendRelation = 43005,
        /// <summary>
        /// 多媒体文件为空
        /// </summary>
        MeidaFileIsEmpty = 44001,
        /// <summary>
        /// POST的数据包为空
        /// </summary>
        PostDataIsEmpty = 44002,
        /// <summary>
        /// 图文消息内容为空
        /// </summary>
        PhotoTextMsgIsEmpty = 44003,
        /// <summary>
        /// 文本消息内容为空
        /// </summary>
        TextMsgIsEmpty = 44004,
        /// <summary>
        /// 多媒体文件大小超过限制
        /// </summary>
        MediaFileSizeOverLimit = 45001,
        /// <summary>
        /// 消息内容超过限制
        /// </summary>
        MsgContentOverLimit = 45002,
        /// <summary>
        /// 标题字段超过限制
        /// </summary>
        TitleFieldOverLimit = 45003,
        /// <summary>
        /// 描述字段超过限制
        /// </summary>
        DiscriptionOverLimit = 45004,
        /// <summary>
        /// 链接字段超过限制
        /// </summary>
        LinkFieldOverLimit = 45005,
        /// <summary>
        /// 图片链接字段超过限制
        /// </summary>
        PhotoLinkOverLimit = 45006,
        /// <summary>
        /// 语音播放时间超过限制
        /// </summary>
        AudioTimeOverLimit = 45007,
        /// <summary>
        /// 图文消息超过限制
        /// </summary>
        PhotoTextOverLimit = 45008,
        /// <summary>
        /// 接口调用超过限制
        /// </summary>
        InterfaceOvertLimit = 45009,
        /// <summary>
        /// 创建菜单个数超过限制
        /// </summary>
        MenuNumOverLimit = 45010,
        /// <summary>
        /// 回复时间超过限制
        /// </summary>
        ReplyTimeOverLimit = 45015,
        /// <summary>
        /// 系统分组，不允许修改
        /// </summary>
        SystemGroupNotUpdate = 45016,
        /// <summary>
        /// 分组名字过长
        /// </summary>
        GroupNameTooLong = 45017,
        /// <summary>
        /// 分组数量超过上限
        /// </summary>
        GroupNumOverLimit = 45018,
        /// <summary>
        /// 不存在媒体数据
        /// </summary>
        MediaDataNotExists = 46001,
        /// <summary>
        /// 不存在的菜单版本
        /// </summary>
        MenuVersionNotExists = 46002,
        /// <summary>
        /// 不存在的菜单数据
        /// </summary>
        MenuDataNotExists = 46003,
        /// <summary>
        /// 不存在的用户
        /// </summary>
        UserNotExists = 46004,
        /// <summary>
        /// 解析JSON/XML内容错误
        /// </summary>
        JsonOrXmlError = 47001,
        /// <summary>
        /// api功能未授权
        /// </summary>
        ApiNotAuth = 48001,
        /// <summary>
        /// 用户未授权该api
        /// </summary>
        UserRefuseApiAuth = 50001,
        /// <summary>
        /// 用户受限
        /// </summary>
        UserBeLimit = 50002,
        /// <summary>
        /// 参数错误(invalid parameter)
        /// </summary>
        ParameterError = 61451,
        /// <summary>
        /// 无效客服账号(invalid kf_account)
        /// </summary>
        InvalidKfAccount = 61452,
        /// <summary>
        /// 客服帐号已存在(kf_account existed)
        /// </summary>
        KfAccountExisted = 61453,
        /// <summary>
        /// 客服帐号名长度超过限制
        /// </summary>
        InvalidKfAccountLength = 61454,
        /// <summary>
        /// 客服帐号名包含非法字符
        /// </summary>
        KfAccountIllegalChar = 61455,
        /// <summary>
        /// 客服帐号个数超过限制
        /// </summary>
        KfAccountCountExceeded = 61456,
        /// <summary>
        /// 无效头像文件类型
        /// </summary>
        InvalidFileType = 61457,
        /// <summary>
        /// 系统错误(system error)
        /// </summary>
        SystemError = 61450,
        /// <summary>
        /// 日期格式错误
        /// </summary>
        DateTimeFormatError = 61500,
        /// <summary>
        /// 日期范围错误
        /// </summary>
        DateTimeRangeError = 61501,
        /// <summary>
        /// 还没有默认菜单，请先创建默认菜单
        /// </summary>
        CreateSelfMenuFirst=65303,
        /// <summary>
        /// 域名数量达到极限
        /// </summary>
        DomainCountReachLimit = 65316,
        /// <summary>
        /// POST数据参数不合法
        /// </summary>
        PostParamIllegal = 9001001,
        /// <summary>
        /// 远端服务不可用
        /// </summary>
        RemoteServiceNotAvailable = 9001002,
        /// <summary>
        /// Ticket不合法
        /// </summary>
        TicketIllegal = 9001003,
        /// <summary>
        /// 获取摇周边用户信息失败
        /// </summary>
        GetShakePerimeterUserFail = 9001004,
        /// <summary>
        /// 获取商户信息失败
        /// </summary>
        GetMerchantInfoFail = 9001005,
        /// <summary>
        /// 获取OpenID失败
        /// </summary>
        GetOpenidFail = 9001006,
        /// <summary>
        /// 上传文件缺失
        /// </summary>
        FileUploadDefect = 9001007,
        /// <summary>
        /// 上传素材的文件类型不合法
        /// </summary>
        UploadFileTypeIllegal = 9001008,
        /// <summary>
        /// 上传素材的文件尺寸不合法
        /// </summary>
        UploadFileSizeIllegal = 9001009,
        /// <summary>
        /// 上传失败
        /// </summary>
        UploadFail = 9001010,
        /// <summary>
        /// 帐号不合法
        /// </summary>
        AccountIllegal = 9001020,
        /// <summary>
        /// 已有设备激活率低于50%，不能新增设备
        /// </summary>
        CanNotAddEquipment = 9001021,
        /// <summary>
        /// 设备申请数不合法，必须为大于0的数字
        /// </summary>
        EquipmentApplicationNumIllegal = 9001022,
        /// <summary>
        /// 已存在审核中的设备ID申请
        /// </summary>
        ExistsEquipmentApplication = 9001023,
        /// <summary>
        /// 一次查询设备ID数量不能超过50
        /// </summary>
        SearchEquipmentNumMore50 = 9001024,
        /// <summary>
        /// 设备ID不合法
        /// </summary>
        EquipmentIdIllegal = 9001025,
        /// <summary>
        /// 页面ID不合法
        /// </summary>
        PageIdIllegal = 9001026,
        /// <summary>
        /// 页面参数不合法
        /// </summary>
        PageParamIllegal = 9001027,
        /// <summary>
        /// 一次删除页面ID数量不能超过10
        /// </summary>
        DeletePageIdMore10 = 9001028,
        /// <summary>
        /// 页面已应用在设备中，请先解除应用关系再删除
        /// </summary>
        PageUseNotDelete = 9001029,
        /// <summary>
        /// 一次查询页面ID数量不能超过50
        /// </summary>
        SearchPageIdMore50 = 9001030,
        /// <summary>
        /// 时间区间不合法
        /// </summary>
        DateTimeRangeIllegal = 9001031,
        /// <summary>
        /// 保存设备与页面的绑定关系参数错误
        /// </summary>
        EquipmentAndPageSaveError = 9001032,
        /// <summary>
        /// 门店ID不合法
        /// </summary>
        StoreIdIllegal = 9001033,
        /// <summary>
        /// 设备备注信息过长
        /// </summary>
        CommentTooLong = 9001034,
        /// <summary>
        /// 设备申请参数不合法
        /// </summary>
        EquipmentParamIllegal = 9001035,
        /// <summary>
        /// 查询起始值begin不合法
        /// </summary>
        NotLegalSearchInitDataBegin = 9001036,
    }
}
