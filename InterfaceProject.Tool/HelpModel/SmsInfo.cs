using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.Tool.HelpModel
{
    /// <summary>
    /// SMS短信平台用户信息类
    /// </summary>
    public class SmsInfo
    {
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="uid">SMS短信平台账号</param>
        /// <param name="key">SMS短信平台密钥</param>
        public SmsInfo(string uid, string key) 
        {
            this.Uid = uid;
            this.Key = key;
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="uid">SMS短信平台账号</param>
        /// <param name="key">SMS短信平台密钥</param>
        /// <param name="smsMob">发送目的号码，多个手机号请用半角逗号隔开</param>
        /// <param name="smsText">发送内容</param>
        public SmsInfo(string uid,string key,string smsMob,string smsText)
        {
            this.Uid = uid;
            this.Key = key;
            this.SmsMob = smsMob;
            this.SmsText = smsText;
        }
        /// <summary>
        /// SMS短信平台账号
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// SMS短信平台密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 发送目的号码
        /// </summary>
        public string SmsMob { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string SmsText { get; set; }

        /// <summary>
        /// 剩余短信条数
        /// </summary>
        public int Remainder { get; set; }
    }
}
