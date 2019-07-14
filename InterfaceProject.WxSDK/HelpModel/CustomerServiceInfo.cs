using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 客服账号信息
    /// </summary>
    public class CustomerServiceInfo
    {
        /// <summary>
        /// 客服帐号
        /// </summary>
        public string kf_account;
        /// <summary>
        /// 客服昵称
        /// </summary>
        public string kf_nick;
        /// <summary>
        /// 客服工号
        /// </summary>
        public string kf_id;
        /// <summary>
        /// 客服头像完整域名地址
        /// </summary>
        public string kf_headimgurl;
    }

    /// <summary>
    /// 客服账号信息查询结果类
    /// </summary>
    public class KFAccountInfo
    {
        /// <summary>
        /// 记录10个以内的客服账号信息
        /// </summary>
        public CustomerServiceInfo[] kf_list;
    }
}
