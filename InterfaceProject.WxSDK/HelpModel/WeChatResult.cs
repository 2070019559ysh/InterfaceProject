using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 微信服务器接口处理结果，指定数据的泛型T
    /// </summary>
    public class WeChatResult<T>
    {
        /// <summary>
        /// 错误代号
        /// </summary>
        public WeChatErrorCode errcode;
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg;
        /// <summary>
        /// 中文错误消息
        /// </summary>
        public string errchinese;
        /// <summary>
        /// 具体返回数据
        /// </summary>
        public T resultData;

        /// <summary>
        /// 根据微信返回的字符串结果，解析成微信结果对象
        /// </summary>
        /// <param name="result">微信返回的字符串结果</param>
        public WeChatResult(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                this.errcode = WeChatErrorCode.NullOrWhiteSpace;
                this.errmsg = "NullOrWhiteSpace";
                this.errchinese = ResultHandle.GetChineseMsg(this.errcode);
                return;
            }
            try
            {
                JObject resultObj = JsonConvert.DeserializeObject<JObject>(result);
                if (resultObj != null)
                {
                    string errcodeStr = resultObj["errcode"]?.ToString();
                    this.errmsg = resultObj["errmsg"]?.ToString();
                    if (string.IsNullOrWhiteSpace(errcodeStr))
                    {
                        this.errcode = WeChatErrorCode.SUCCESS;
                    }
                    else
                    {
                        WeChatErrorCode wechatErrorCode;
                        if (!Enum.TryParse(errcodeStr, out wechatErrorCode))
                        {
                            wechatErrorCode = WeChatErrorCode.UnknownCode;
                        }
                        this.errcode = wechatErrorCode;
                    }
                    try
                    {
                        //如果结果字符串符合json
                        this.resultData = JsonConvert.DeserializeObject<T>(result);
                    }
                    catch (Exception)
                    {
                        //结果字符串符合不符合json，需要转具体的值类型，如：string, bool, int, decimal
                        this.resultData = (T)Convert.ChangeType(result, typeof(T));
                    }
                }
            }
            catch (Exception ex)
            {
                this.errcode = WeChatErrorCode.UnableToParse;
                this.errmsg = ex.Message;
            }
            this.errchinese = ResultHandle.GetChineseMsg(this.errcode);
        }

        /// <summary>
        /// 返回errcode,errmsg,errchinese组成的字符串
        /// </summary>
        /// <returns>结果消息字符串</returns>
        public override string ToString()
        {
            return $"errcode={this.errcode},errmsg={this.errmsg},errchinese={this.errchinese}";
        }
    }

    /// <summary>
    /// 微信服务器接口处理结果
    /// </summary>
    public class WeChatResult
    {
        /// <summary>
        /// 错误代号
        /// </summary>
        public WeChatErrorCode errcode;
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg;
        /// <summary>
        /// 中文错误消息
        /// </summary>
        public string errchinese;

        /// <summary>
        /// 根据微信返回的字符串结果，解析成微信结果对象
        /// </summary>
        /// <param name="result">微信返回的字符串结果</param>
        public WeChatResult(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                this.errcode = WeChatErrorCode.NullOrWhiteSpace;
                this.errmsg = "NullOrWhiteSpace";
                this.errchinese = ResultHandle.GetChineseMsg(this.errcode);
                return;
            }
            try
            {
                JObject resultObj = JsonConvert.DeserializeObject<JObject>(result);
                if (resultObj != null)
                {
                    string errcodeStr = resultObj["errcode"]?.ToString();
                    this.errmsg = resultObj["errmsg"]?.ToString();
                    if (string.IsNullOrWhiteSpace(errcodeStr))
                    {
                        this.errcode = WeChatErrorCode.SUCCESS;
                    }
                    else
                    {
                        WeChatErrorCode wechatErrorCode;
                        if (!Enum.TryParse(errcodeStr, out wechatErrorCode))
                        {
                            wechatErrorCode = WeChatErrorCode.UnknownCode;
                        }
                        this.errcode = wechatErrorCode;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errcode = WeChatErrorCode.UnableToParse;
                this.errmsg = ex.Message;
            }
            this.errchinese = ResultHandle.GetChineseMsg(this.errcode);
        }

        /// <summary>
        /// 返回errcode,errmsg,errchinese组成的字符串
        /// </summary>
        /// <returns>结果消息字符串</returns>
        public override string ToString()
        {
            return $"errcode={this.errcode},errmsg={this.errmsg},errchinese={this.errchinese}";
        }
    }

    /// <summary>
    /// 微信接口结果消息内部实现单例模式
    /// </summary>
    internal class ResultHandle
    {
        /// <summary>
        /// 记录错误代码枚举的字段信息的集合
        /// </summary>
        private static List<FieldDesc<int>> fieldDescList;

        static ResultHandle()
        {
            Type wechatErrCodeType = typeof(WeChatErrorCode);
            foreach (var errCodeField in wechatErrCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var explainAttribute = (ExplainAttribute)(errCodeField.GetCustomAttributes(false).FirstOrDefault(x => x is ExplainAttribute));
                if (explainAttribute != null)
                {
                    if (fieldDescList == null) fieldDescList = new List<FieldDesc<int>>();
                    FieldDesc<int> fieldDesc = new FieldDesc<int>();
                    fieldDesc.FieldName = errCodeField.Name;
                    fieldDesc.FieldValue = Convert.ToInt32(errCodeField.GetValue(null));
                    fieldDesc.ExplainContent = explainAttribute.Content;
                    fieldDescList.Add(fieldDesc);//添加一个记录了说明内容的字段信息
                }
            }
        }

        /// <summary>
        /// 获取微信错误代号对应的中文消息
        /// </summary>
        /// <param name="errorCode">微信错误代号</param>
        /// <returns>错误代号对应的中文消息</returns>
        public static string GetChineseMsg(WeChatErrorCode errorCode)
        {
            int errCode = (int)errorCode;
            FieldDesc<int> errCodeFieldDesc = fieldDescList.Where(fieldDesc => fieldDesc.FieldValue == errCode).FirstOrDefault();
            return errCodeFieldDesc?.ExplainContent;
        }
    }

    /// <summary>
    /// 微信的错误代号枚举
    /// </summary>
    public enum WeChatErrorCode
    {
        /// <summary>
        /// 无法解析的微信结果
        /// </summary>
        [Explain("无法解析的微信结果")]
        UnableToParse = -9999,
        /// <summary>
        /// 未知错误代号
        /// </summary>
        [Explain("未知错误代号")]
        UnknownCode = -9998,
        /// <summary>
        /// 结果为NULL或空白字符
        /// </summary>
        [Explain("结果为NULL或空白字符")]
        NullOrWhiteSpace = -9997,
        /// <summary>
        /// 系统繁忙，此时请开发者稍候再试
        /// </summary>
        [Explain("系统繁忙，此时请开发者稍候再试")]
        SystemBusy = -1,
        /// <summary>
        /// 请求成功
        /// </summary>
        [Explain("请求成功")]
        SUCCESS = 0,
        /// <summary>
        /// 获取 access_token 时 AppSecret 错误，或者 access_token 无效。请开发者认真比对 AppSecret 的正确性，或查看是否正在为恰当的公众号调用接口
        /// </summary>
        [Explain("获取 access_token 时 AppSecret 错误，或者 access_token 无效。请开发者认真比对 AppSecret 的正确性，或查看是否正在为恰当的公众号调用接口")]
        AppSecretWrong = 40001,
        /// <summary>
        /// 不合法的凭证类型，如请确保grant_type字段值为client_credential
        /// </summary>
        [Explain("不合法的凭证类型，如请确保grant_type字段值为client_credential")]
        NotLegalVoucher = 40002,
        /// <summary>
        /// 不合法的OpenID，请开发者确认 OpenID （该用户）是否已关注公众号，或是否是其他公众号的 OpenID
        /// </summary>
        [Explain("不合法的OpenID，请开发者确认 OpenID （该用户）是否已关注公众号，或是否是其他公众号的 OpenID")]
        NotLegalOpenId = 40003,
        /// <summary>
        /// 不合法的媒体文件类型
        /// </summary>
        [Explain("不合法的媒体文件类型")]
        NotLegalMediaType = 40004,
        /// <summary>
        /// 不合法的文件类型
        /// </summary>
        [Explain("不合法的文件类型")]
        NotLegalFileType = 40005,
        /// <summary>
        /// 不合法的文件大小
        /// </summary>
        [Explain("不合法的文件大小")]
        NotLegalFileSize = 40006,
        /// <summary>
        /// 不合法的媒体文件id
        /// </summary>
        [Explain("不合法的媒体文件id")]
        NotLegalMediaFileId = 40007,
        /// <summary>
        /// 不合法的消息类型
        /// </summary>
        [Explain("不合法的消息类型")]
        NotLegalMessageType = 40008,
        /// <summary>
        /// 不合法的图片文件大小
        /// </summary>
        [Explain("不合法的图片文件大小")]
        NotLegalPhotoSize = 40009,
        /// <summary>
        /// 不合法的语音文件大小
        /// </summary>
        [Explain("不合法的语音文件大小")]
        NotLegalAudioSize = 40010,
        /// <summary>
        /// 不合法的视频文件大小
        /// </summary>
        [Explain("不合法的视频文件大小")]
        NotLegalVideoSize = 40011,
        /// <summary>
        /// 不合法的缩略图文件大小
        /// </summary>
        [Explain("不合法的缩略图文件大小")]
        NotLegalThumbnailSize = 40012,
        /// <summary>
        /// 不合法的 AppID ，请开发者检查 AppID 的正确性，避免异常字符，注意大小写
        /// </summary>
        [Explain("不合法的 AppID ，请开发者检查 AppID 的正确性，避免异常字符，注意大小写")]
        NotLegalAppid = 40013,
        /// <summary>
        /// 不合法的 access_token ，请开发者认真比对 access_token 的有效性（如是否过期），或查看是否正在为恰当的公众号调用接口
        /// </summary>
        [Explain("不合法的 access_token ，请开发者认真比对 access_token 的有效性（如是否过期），或查看是否正在为恰当的公众号调用接口")]
        NotLegalAccessToken = 40014,
        /// <summary>
        /// 不合法的菜单类型
        /// </summary>
        [Explain("不合法的菜单类型")]
        NotLegalMenuType = 40015,
        /// <summary>
        /// 不合法的按钮个数
        /// </summary>
        [Explain("不合法的按钮个数")]
        NotLegalButtonNum = 40016,
        /// <summary>
        /// 不合法的按钮个数
        /// </summary>
        [Explain("不合法的按钮个数")]
        NotLegalButtonNum2 = 40017,
        /// <summary>
        /// 不合法的按钮名字长度
        /// </summary>
        [Explain("不合法的按钮名字长度")]
        NotLegalBtnNameLength = 40018,
        /// <summary>
        /// 不合法的按钮 KEY 长度
        /// </summary>
        [Explain("不合法的按钮 KEY 长度")]
        NotLegalBtnKeyLength = 40019,
        /// <summary>
        /// 不合法的按钮 URL 长度
        /// </summary>
        [Explain("不合法的按钮 URL长度")]
        NotLegalBtnUrlLength = 40020,
        /// <summary>
        /// 不合法的菜单版本号
        /// </summary>
        [Explain("不合法的菜单版本号")]
        NotLegalMenuVersionNum = 40021,
        /// <summary>
        /// 不合法的子菜单级数
        /// </summary>
        [Explain("不合法的子菜单级数")]
        NotLegalSubMenuLevel = 40022,
        /// <summary>
        /// 不合法的子菜单按钮个数
        /// </summary>
        [Explain("不合法的子菜单按钮个数")]
        NotLegalSubMenuBtnNum = 40023,
        /// <summary>
        /// 不合法的子菜单按钮类型
        /// </summary>
        [Explain("不合法的子菜单按钮类型")]
        NotLegalSubMenuBtnType = 40024,
        /// <summary>
        /// 不合法的子菜单按钮名字长度
        /// </summary>
        [Explain("不合法的子菜单按钮名字长度")]
        NotLegalSubMenuBtnNameLength = 40025,
        /// <summary>
        /// 不合法的子菜单按钮 KEY 长度
        /// </summary>
        [Explain("不合法的子菜单按钮 KEY 长度")]
        NotLegalSubMenuBtnKeyLength = 40026,
        /// <summary>
        /// 不合法的子菜单按钮 URL 长度
        /// </summary>
        [Explain("不合法的子菜单按钮 URL 长度")]
        NotLegalSubMenuBtnUrlLength = 40027,
        /// <summary>
        /// 不合法的自定义菜单使用用户
        /// </summary>
        [Explain("不合法的自定义菜单使用用户")]
        NotLegalCustomMenu = 40028,
        /// <summary>
        /// 不合法的oauth_code
        /// </summary>
        [Explain("不合法的oauth_code")]
        NotLegalOauthcode = 40029,
        /// <summary>
        /// 不合法的refresh_token
        /// </summary>
        [Explain("不合法的refresh_token")]
        NotLegalRefreshtoken = 40030,
        /// <summary>
        /// 不合法的openid列表
        /// </summary>
        [Explain("不合法的openid列表")]
        NotLegalOpenidList = 40031,
        /// <summary>
        /// 不合法的openid列表长度
        /// </summary>
        [Explain("不合法的openid列表长度")]
        NotLegalOpenidListLength = 40032,
        /// <summary>
        /// 不合法的请求字符，不能包含\uxxxx格式的字符
        /// </summary>
        [Explain("不合法的请求字符，不能包含\\uxxxx格式的字符")]
        NotLegalRequestChar = 40033,
        /// <summary>
        /// 不合法的参数
        /// </summary>
        [Explain("不合法的参数")]
        NotLegalParameter = 40035,
        /// <summary>
        /// 不合法的请求格式
        /// </summary>
        [Explain("不合法的请求格式")]
        NotLegalRequestMode = 40038,
        /// <summary>
        /// 不合法的URL长度
        /// </summary>
        [Explain("不合法的URL长度")]
        NotLegalUrlLength = 40039,
        /// <summary>
        /// 不合法的分组id
        /// </summary>
        [Explain("不合法的分组id")]
        NotLegalGroupid = 40050,
        /// <summary>
        /// 分组名字不合法
        /// </summary>
        [Explain("分组名字不合法")]
        NotLegalGroupName = 40051,
        /// <summary>
        /// 删除单篇图文时，指定的 article_idx 不合法
        /// </summary>
        [Explain("删除单篇图文时，指定的 article_idx 不合法")]
        NotLegalArticleId = 40060,
        /// <summary>
        /// 分组名字不合法
        /// </summary>
        [Explain("分组名字不合法")]
        NotLegalGroupName2 = 40117,
        /// <summary>
        /// media_id大小不合法
        /// </summary>
        [Explain("media_id大小不合法")]
        NotLegalMediaIdSize = 40118,
        /// <summary>
        /// button类型错误
        /// </summary>
        [Explain("button类型错误")]
        ButtonTypeError = 40119,
        /// <summary>
        /// button类型错误
        /// </summary>
        [Explain("button类型错误")]
        ButtonTypeError2 = 40120,
        /// <summary>
        /// 不合法的media_id类型
        /// </summary>
        [Explain("不合法的media_id类型")]
        NotLegalMediaIdType = 40121,
        /// <summary>
        /// 微信号不合法
        /// </summary>
        [Explain("微信号不合法")]
        NotLegalWeixin = 40132,
        /// <summary>
        /// 不支持的图片格式
        /// </summary>
        [Explain("不支持的图片格式")]
        NotSupportPhotoFormat = 40137,
        /// <summary>
        /// 请勿添加其他公众号的主页链接
        /// </summary>
        [Explain("请勿添加其他公众号的主页链接")]
        NotAddOtherWeChatUrl = 40155,
        /// <summary>
        /// 调用接口的IP地址不在白名单中，请在接口IP白名单中进行设置
        /// </summary>
        [Explain("调用接口的IP地址不在白名单中，请在接口IP白名单中进行设置")]
        NotInIpWhiteList = 40164,
        /// <summary>
        /// 缺少access_token参数
        /// </summary>
        [Explain("缺少access_token参数")]
        LackAccessTokenParam = 41001,
        /// <summary>
        /// 缺少appid参数
        /// </summary>
        [Explain("缺少appid参数")]
        LackAppIdParam = 41002,
        /// <summary>
        /// 缺少refresh_token参数
        /// </summary>
        [Explain("缺少refresh_token参数")]
        LackRefreshTokenParam = 41003,
        /// <summary>
        /// 缺少secret参数
        /// </summary>
        [Explain("缺少secret参数")]
        LackSecretParam = 41004,
        /// <summary>
        /// 缺少多媒体文件数据
        /// </summary>
        [Explain("缺少多媒体文件数据")]
        LackMediaFileData = 41005,
        /// <summary>
        /// 缺少media_id参数
        /// </summary>
        [Explain("缺少media_id参数")]
        LackMediaIdParam = 41006,
        /// <summary>
        /// 缺少子菜单数据
        /// </summary>
        [Explain("缺少子菜单数据")]
        LackSubMenuData = 41007,
        /// <summary>
        /// 缺少oauth code
        /// </summary>
        [Explain("缺少oauth code")]
        LackOAuthCode = 41008,
        /// <summary>
        /// 缺少openid
        /// </summary>
        [Explain("缺少openid")]
        LackOpenid = 41009,
        /// <summary>
        /// access_token 超时，请检查 access_token 的有效期
        /// </summary>
        [Explain("access_token 超时，请检查 access_token 的有效期")]
        AccessTokenOvertime = 42001,
        /// <summary>
        /// refresh_token超时
        /// </summary>
        [Explain("refresh_token超时")]
        RefreshTokenOvertime = 42002,
        /// <summary>
        /// oauth_code超时
        /// </summary>
        [Explain("oauth_code超时")]
        OAuthCodeOverTime = 42003,
        /// <summary>
        /// 用户修改微信密码， accesstoken 和 refreshtoken 失效，需要重新授权
        /// </summary>
        [Explain("用户修改微信密码， accesstoken 和 refreshtoken 失效，需要重新授权")]
        UserAccessTokenUpdate = 42007,
        /// <summary>
        /// 需要GET请求
        /// </summary>
        [Explain("需要GET请求")]
        RequireGetRequest = 43001,
        /// <summary>
        /// 需要POST请求
        /// </summary>
        [Explain("需要POST请求")]
        RequirePostRequest = 43002,
        /// <summary>
        /// 需要HTTPS请求
        /// </summary>
        [Explain("需要HTTPS请求")]
        RequireHttpsRequest = 43003,
        /// <summary>
        /// 需要接收者关注
        /// </summary>
        [Explain("需要接收者关注")]
        RequireUserAttention = 43004,
        /// <summary>
        /// 需要好友关系
        /// </summary>
        [Explain("需要好友关系")]
        RequireGoodFriendRelation = 43005,
        /// <summary>
        /// 需要将接收者从黑名单中移除
        /// </summary>
        [Explain("需要将接收者从黑名单中移除")]
        RequireRemoveBacklist = 43019,
        /// <summary>
        /// 多媒体文件为空
        /// </summary>
        [Explain("多媒体文件为空")]
        MeidaFileIsEmpty = 44001,
        /// <summary>
        /// POST的数据包为空
        /// </summary>
        [Explain("POST的数据包为空")]
        PostDataIsEmpty = 44002,
        /// <summary>
        /// 图文消息内容为空
        /// </summary>
        [Explain("图文消息内容为空")]
        PhotoTextMsgIsEmpty = 44003,
        /// <summary>
        /// 文本消息内容为空
        /// </summary>
        [Explain("文本消息内容为空")]
        TextMsgIsEmpty = 44004,
        /// <summary>
        /// 多媒体文件大小超过限制
        /// </summary>
        [Explain("多媒体文件大小超过限制")]
        MediaFileSizeOverLimit = 45001,
        /// <summary>
        /// 消息内容超过限制
        /// </summary>
        [Explain("消息内容超过限制")]
        MsgContentOverLimit = 45002,
        /// <summary>
        /// 标题字段超过限制
        /// </summary>
        [Explain("标题字段超过限制")]
        TitleFieldOverLimit = 45003,
        /// <summary>
        /// 描述字段超过限制
        /// </summary>
        [Explain("描述字段超过限制")]
        DiscriptionOverLimit = 45004,
        /// <summary>
        /// 链接字段超过限制
        /// </summary>
        [Explain("链接字段超过限制")]
        LinkFieldOverLimit = 45005,
        /// <summary>
        /// 图片链接字段超过限制
        /// </summary>
        [Explain("图片链接字段超过限制")]
        PhotoLinkOverLimit = 45006,
        /// <summary>
        /// 语音播放时间超过限制
        /// </summary>
        [Explain("语音播放时间超过限制")]
        AudioTimeOverLimit = 45007,
        /// <summary>
        /// 图文消息超过限制
        /// </summary>
        [Explain("图文消息超过限制")]
        PhotoTextOverLimit = 45008,
        /// <summary>
        /// 接口调用超过限制
        /// </summary>
        [Explain("接口调用超过限制")]
        InterfaceOvertLimit = 45009,
        /// <summary>
        /// 创建菜单个数超过限制
        /// </summary>
        [Explain("创建菜单个数超过限制")]
        MenuNumOverLimit = 45010,
        /// <summary>
        /// API 调用太频繁，请稍候再试
        /// </summary>
        [Explain("API 调用太频繁，请稍候再试")]
        ApiCallFrequently = 45011,
        /// <summary>
        /// 回复时间超过限制
        /// </summary>
        [Explain("回复时间超过限制")]
        ReplyTimeOverLimit = 45015,
        /// <summary>
        /// 系统分组，不允许修改
        /// </summary>
        [Explain("系统分组，不允许修改")]
        SystemGroupNotUpdate = 45016,
        /// <summary>
        /// 分组名字过长
        /// </summary>
        [Explain("分组名字过长")]
        GroupNameTooLong = 45017,
        /// <summary>
        /// 分组数量超过上限
        /// </summary>
        [Explain("分组数量超过上限")]
        GroupNumOverLimit = 45018,
        /// <summary>
        /// 客服接口下行条数超过上限
        /// </summary>
        [Explain("客服接口下行条数超过上限")]
        CustomerServiceLimit = 45047,
        /// <summary>
        /// 相同 clientmsgid 已存在群发记录，返回数据中带有已存在的群发任务的 msgid
        /// </summary>
        [Explain("相同 clientmsgid 已存在群发记录，返回数据中带有已存在的群发任务的 msgid")]
        GroupSendClientMsgIdRepeat = 45065,
        /// <summary>
        /// 相同 clientmsgid 重试速度过快，请间隔1分钟重试
        /// </summary>
        [Explain("相同 clientmsgid 重试速度过快，请间隔1分钟重试")]
        GroupSendClientMsgIdQuickly = 45066,
        /// <summary>
        /// clientmsgid 长度超过限制
        /// </summary>
        [Explain("clientmsgid 长度超过限制")]
        GroupSendClientMsgIdTooLong= 45067,
        /// <summary>
        /// 客服接口command字段取值不对
        /// </summary>
        [Explain("command字段取值不对")]
        CustomerServiceCommandInValid = 45072,
        /// <summary>
        /// 下发输入状态，需要之前30秒内跟用户有过消息交互
        /// </summary>
        [Explain("下发输入状态，需要之前30秒内跟用户有过消息交互")]
        CustomerServiceLess30s = 45080,
        /// <summary>
        /// 已经在输入状态，不可重复下发
        /// </summary>
        [Explain("已经在输入状态，不可重复下发")]
        CustomerServiceInTyping = 45081,
        /// <summary>
        /// 设置的 speed 参数不在0到4的范围内
        /// </summary>
        [Explain("设置的 speed 参数不在0到4的范围内")]
        GroupSendSpeedInValid= 45083,
        /// <summary>
        /// 没有设置 speed 参数
        /// </summary>
        [Explain("没有设置 speed 参数")]
        GroupSendSpeedRequired= 45084,
        /// <summary>
        /// 不存在媒体数据
        /// </summary>
        [Explain("不存在媒体数据")]
        MediaDataNotExists = 46001,
        /// <summary>
        /// 不存在的菜单版本
        /// </summary>
        [Explain("不存在的菜单版本")]
        MenuVersionNotExists = 46002,
        /// <summary>
        /// 不存在的菜单数据
        /// </summary>
        [Explain("不存在的菜单数据")]
        MenuDataNotExists = 46003,
        /// <summary>
        /// 不存在的用户
        /// </summary>
        [Explain("不存在的用户")]
        UserNotExists = 46004,
        /// <summary>
        /// 解析JSON/XML内容错误
        /// </summary>
        [Explain("解析JSON/XML内容错误")]
        JsonOrXmlError = 47001,
        /// <summary>
        /// api 功能未授权，请确认公众号已获得该接口
        /// </summary>
        [Explain("api 功能未授权，请确认公众号已获得该接口")]
        ApiNotAuth = 48001,
        /// <summary>
        /// 粉丝拒收消息（粉丝在公众号选项中，关闭了“接收消息”）
        /// </summary>
        [Explain("粉丝拒收消息（粉丝在公众号选项中，关闭了“接收消息”）")]
        UserRefuseMessage = 48002,
        /// <summary>
        /// api 接口被封禁，请登录 mp.weixin.qq.com 查看详情
        /// </summary>
        [Explain("api 接口被封禁，请登录 mp.weixin.qq.com 查看详情")]
        ApiCallClosure = 48004,
        /// <summary>
        /// api 禁止删除被自动回复和自定义菜单引用的素材
        /// </summary>
        [Explain("api 禁止删除被自动回复和自定义菜单引用的素材")]
        ApiRefuseDeleteMaterial = 48005,
        /// <summary>
        /// api 禁止清零调用次数，因为清零次数达到上限
        /// </summary>
        [Explain("api 禁止清零调用次数，因为清零次数达到上限")]
        ApiRefuseClearCount = 48006,
        /// <summary>
        /// 没有该类型消息的发送权限
        /// </summary>
        [Explain("没有该类型消息的发送权限")]
        MessageTypeNoAuth = 48008,
        /// <summary>
        /// 用户未授权该api
        /// </summary>
        [Explain("用户未授权该api")]
        UserRefuseApiAuth = 50001,
        /// <summary>
        /// 用户受限，可能是违规后接口被封禁
        /// </summary>
        [Explain("用户受限，可能是违规后接口被封禁")]
        UserBeLimit = 50002,
        /// <summary>
        /// 用户未关注公众号
        /// </summary>
        [Explain("用户未关注公众号")]
        UserNoSubscribe = 50005,
        /// <summary>
        /// 参数错误(invalid parameter)
        /// </summary>
        [Explain("参数错误(invalid parameter)")]
        ParameterError = 61451,
        /// <summary>
        /// 无效客服账号(invalid kf_account)
        /// </summary>
        [Explain("无效客服账号(invalid kf_account)")]
        InvalidKfAccount = 61452,
        /// <summary>
        /// 客服帐号已存在(kf_account existed)
        /// </summary>
        [Explain("客服帐号已存在(kf_account existed)")]
        KfAccountExisted = 61453,
        /// <summary>
        /// 客服帐号名长度超过限制(仅允许10个英文字符，不包括@及@后的公众号的微信号)
        /// </summary>
        [Explain("客服帐号名长度超过限制(仅允许10个英文字符，不包括@及@后的公众号的微信号)")]
        InvalidKfAccountLength = 61454,
        /// <summary>
        /// 客服帐号名包含非法字符(仅允许英文+数字)
        /// </summary>
        [Explain("客服帐号名包含非法字符(仅允许英文+数字)")]
        KfAccountIllegalChar = 61455,
        /// <summary>
        /// 客服帐号个数超过限制(10个客服账号)
        /// </summary>
        [Explain("客服帐号个数超过限制(10个客服账号)")]
        KfAccountCountExceeded = 61456,
        /// <summary>
        /// 无效头像文件类型
        /// </summary>
        [Explain("无效头像文件类型")]
        InvalidFileType = 61457,
        /// <summary>
        /// 系统错误(system error)
        /// </summary>
        [Explain("系统错误(system error)")]
        SystemError = 61450,
        /// <summary>
        /// 日期格式错误
        /// </summary>
        [Explain("日期格式错误")]
        DateTimeFormatError = 61500,
        /// <summary>
        /// 日期范围错误
        /// </summary>
        [Explain("日期范围错误")]
        DateTimeRangeError = 61501,
        /// <summary>
        /// 不存在此 menuid 对应的个性化菜单
        /// </summary>
        [Explain("不存在此 menuid 对应的个性化菜单")]
        NotMenuIdPersonalizedMenu = 65301,
        /// <summary>
        /// 没有相应的用户
        /// </summary>
        [Explain("没有相应的用户")]
        NotCorrespondingUser = 65302,
        /// <summary>
        /// 还没有默认菜单，请先创建默认菜单
        /// </summary>
        [Explain("还没有默认菜单，请先创建默认菜单")]
        CreateSelfMenuFirst = 65303,
        /// <summary>
        /// MatchRule 信息为空
        /// </summary>
        [Explain("MatchRule 信息为空")]
        MatchRuleIsNull = 65304,
        /// <summary>
        /// 个性化菜单数量受限
        /// </summary>
        [Explain("个性化菜单数量受限")]
        PersonalizedMenuLimit = 65305,
        /// <summary>
        /// 个性化菜单数量受限
        /// </summary>
        [Explain("不支持个性化菜单的帐号")]
        PersonalizedMenuNotSupport = 65306,
        /// <summary>
        /// 个性化菜单信息为空
        /// </summary>
        [Explain("个性化菜单信息为空")]
        PersonalizedMenuIsNull = 65307,
        /// <summary>
        /// 包含没有响应类型的 button
        /// </summary>
        [Explain("包含没有响应类型的 button")]
        ButtonNotResponseType = 65308,
        /// <summary>
        /// 个性化菜单开关处于关闭状态
        /// </summary>
        [Explain("个性化菜单开关处于关闭状态")]
        PersonalizedMenuIsClosed = 65309,
        /// <summary>
        /// 填写了省份或城市信息，国家信息不能为空
        /// </summary>
        [Explain("填写了省份或城市信息，国家信息不能为空")]
        CountryIsNotNull = 65310,
        /// <summary>
        /// 填写了城市信息，省份信息不能为空
        /// </summary>
        [Explain("填写了城市信息，省份信息不能为空")]
        ProvinceIsNotNull = 65311,
        /// <summary>
        /// 不合法的国家信息
        /// </summary>
        [Explain("不合法的国家信息")]
        NotLegalCountryInfo = 65312,
        /// <summary>
        /// 不合法的省份信息
        /// </summary>
        [Explain("不合法的省份信息")]
        NotLegalProvinceInfo = 65313,
        /// <summary>
        /// 不合法的城市信息
        /// </summary>
        [Explain("不合法的城市信息")]
        NotLegalCityInfo = 65314,
        /// <summary>
        /// 该公众号的菜单设置了过多的域名外跳（最多跳转到 3 个域名的链接）
        /// </summary>
        [Explain("该公众号的菜单设置了过多的域名外跳（最多跳转到 3 个域名的链接）")]
        DomainCountReachLimit = 65316,
        /// <summary>
        /// 不合法的 URL
        /// </summary>
        [Explain("不合法的 URL")]
        NotLegalUrl = 65317,
        /// <summary>
        /// 请启用新的自定义服务，或如果启用，请稍候
        /// </summary>
        [Explain("请启用新的自定义服务，或如果启用，请稍候")]
        NeedEnableCustomService = 65400,
        /// <summary>
        /// POST数据参数不合法
        /// </summary>
        [Explain("POST数据参数不合法")]
        PostParamIllegal = 9001001,
        /// <summary>
        /// 远端服务不可用
        /// </summary>
        [Explain("远端服务不可用")]
        RemoteServiceNotAvailable = 9001002,
        /// <summary>
        /// Ticket不合法
        /// </summary>
        [Explain("Ticket不合法")]
        TicketIllegal = 9001003,
        /// <summary>
        /// 获取摇周边用户信息失败
        /// </summary>
        [Explain("获取摇周边用户信息失败")]
        GetShakePerimeterUserFail = 9001004,
        /// <summary>
        /// 获取商户信息失败
        /// </summary>
        [Explain("获取商户信息失败")]
        GetMerchantInfoFail = 9001005,
        /// <summary>
        /// 获取OpenID失败
        /// </summary>
        [Explain("获取OpenID失败")]
        GetOpenidFail = 9001006,
        /// <summary>
        /// 上传文件缺失
        /// </summary>
        [Explain("上传文件缺失")]
        FileUploadDefect = 9001007,
        /// <summary>
        /// 上传素材的文件类型不合法
        /// </summary>
        [Explain("上传素材的文件类型不合法")]
        UploadFileTypeIllegal = 9001008,
        /// <summary>
        /// 上传素材的文件尺寸不合法
        /// </summary>
        [Explain("上传素材的文件尺寸不合法")]
        UploadFileSizeIllegal = 9001009,
        /// <summary>
        /// 上传失败
        /// </summary>
        [Explain("上传失败")]
        UploadFail = 9001010,
        /// <summary>
        /// 帐号不合法
        /// </summary>
        [Explain("帐号不合法")]
        AccountIllegal = 9001020,
        /// <summary>
        /// 已有设备激活率低于50%，不能新增设备
        /// </summary>
        [Explain("已有设备激活率低于50%，不能新增设备")]
        CanNotAddEquipment = 9001021,
        /// <summary>
        /// 设备申请数不合法，必须为大于0的数字
        /// </summary>
        [Explain("设备申请数不合法，必须为大于0的数字")]
        EquipmentApplicationNumIllegal = 9001022,
        /// <summary>
        /// 已存在审核中的设备ID申请
        /// </summary>
        [Explain("已存在审核中的设备ID申请")]
        ExistsEquipmentApplication = 9001023,
        /// <summary>
        /// 一次查询设备ID数量不能超过50
        /// </summary>
        [Explain("一次查询设备ID数量不能超过50")]
        SearchEquipmentNumMore50 = 9001024,
        /// <summary>
        /// 设备ID不合法
        /// </summary>
        [Explain("设备ID不合法")]
        EquipmentIdIllegal = 9001025,
        /// <summary>
        /// 页面ID不合法
        /// </summary>
        [Explain("页面ID不合法")]
        PageIdIllegal = 9001026,
        /// <summary>
        /// 页面参数不合法
        /// </summary>
        [Explain("页面参数不合法")]
        PageParamIllegal = 9001027,
        /// <summary>
        /// 一次删除页面ID数量不能超过10
        /// </summary>
        [Explain("一次删除页面ID数量不能超过10")]
        DeletePageIdMore10 = 9001028,
        /// <summary>
        /// 页面已应用在设备中，请先解除应用关系再删除
        /// </summary>
        [Explain("页面已应用在设备中，请先解除应用关系再删除")]
        PageUseNotDelete = 9001029,
        /// <summary>
        /// 一次查询页面ID数量不能超过50
        /// </summary>
        [Explain("一次查询页面ID数量不能超过50")]
        SearchPageIdMore50 = 9001030,
        /// <summary>
        /// 时间区间不合法
        /// </summary>
        [Explain("时间区间不合法")]
        DateTimeRangeIllegal = 9001031,
        /// <summary>
        /// 保存设备与页面的绑定关系参数错误
        /// </summary>
        [Explain("保存设备与页面的绑定关系参数错误")]
        EquipmentAndPageSaveError = 9001032,
        /// <summary>
        /// 门店ID不合法
        /// </summary>
        [Explain("门店ID不合法")]
        StoreIdIllegal = 9001033,
        /// <summary>
        /// 设备备注信息过长
        /// </summary>
        [Explain("设备备注信息过长")]
        CommentTooLong = 9001034,
        /// <summary>
        /// 设备申请参数不合法
        /// </summary>
        [Explain("设备申请参数不合法")]
        EquipmentParamIllegal = 9001035,
        /// <summary>
        /// 查询起始值begin不合法
        /// </summary>
        [Explain("查询起始值begin不合法")]
        NotLegalSearchInitDataBegin = 9001036,
    }
}
