using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 封装微信的Access_Token信息类
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 微信公众号的Access_Token凭证
        /// </summary>
        public string access_token;
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in;
    }
}
