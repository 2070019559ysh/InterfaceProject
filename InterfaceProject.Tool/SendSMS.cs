using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InterfaceProject.Tool.HelpModel;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// 短信发送结果
    /// </summary>
    public enum SendResult
    {
        没有此短信账户=-1,
        接口密钥不正确=-2,
        MD5接口密钥加密不正确=-21,
        短信数量不足=-3,
        此用户被禁用=-11,
        短信内容出现非法字符=-14,
        手机号格式不正确=-4,
        手机号码为空=-41,
        短信内容为空=-42,
        短信签名格式不正确=-51,
        IP限制=-6,
        成功=1
    }
    /// <summary>
    /// 发送短信类
    /// </summary>
    public class SendSMS
    {
        private const string url = "http://utf8.sms.webchinese.cn/?";
        private const string strUid = "Uid=";
        private const string strKey = "&key=";
        private const string strKeyMD5 = "&keyMD5=";
        private const string strMob = "&smsMob=";
        private const string strContent = "&smsText=";

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">接收消息的手机号</param>
        /// <param name="content">短信内容</param>
        /// <param name="resultNum">输出参数，成功发送的短信数量</param>
        /// <param name="isMD5">可选，是否对密钥进行MD5加密,默认加密</param>
        /// <returns>短信发送结果枚举</returns>
        public static SendResult SendMessage(string mobile,string content, out int resultNum, bool isMD5 = true)
        {
            SmsInfo smsInfo = new SmsInfo(ToolOptions.SMSUId, ToolOptions.SMSPassword,
                mobile, content);
            SendResult sendResult = SendMessage(smsInfo, out resultNum);
            return sendResult;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsInfo">sms短信平台信息</param>
        /// <param name="resultNum">输出参数，成功发送的短信数量</param>
        /// <param name="isMD5">可选，是否对密钥进行MD5加密,默认加密</param>
        /// <returns>短信发送结果枚举</returns>
        public static SendResult SendMessage(SmsInfo smsInfo,out int resultNum,bool isMD5=true)
        {
            ///UTF-8编码发送接口地址：
            ///http://utf8.sms.webchinese.cn/?Uid=本站用户名&Key=接口安全秘钥&smsMob=手机号码&smsText=验证码:8888
            resultNum = 0;
            //字符串拼接出请求地址
            StringBuilder requestUri = new StringBuilder();
            requestUri.Append(url);
            requestUri.Append(strUid);
            requestUri.Append(smsInfo.Uid);
            if (isMD5)
            {   //密钥加密
                requestUri.Append(strKeyMD5);
                requestUri.Append(EncryptHelper.HashMD532(smsInfo.Key));
            }
            else
            {
                requestUri.Append(strKey);
                requestUri.Append(smsInfo.Key);
            }
            requestUri.Append(strMob);
            requestUri.Append(smsInfo.SmsMob);
            requestUri.Append(strContent);
            requestUri.Append(smsInfo.SmsText);
            string requestUriString = requestUri.ToString();
            //创建请求并获取响应
            string result = GetResponseByUri(requestUriString);//读取响应结果
            int intResult=0;//响应结果尝试转成整型
            int.TryParse(result, out intResult);
            if (intResult > 0)
            {   //发送短信成功
                resultNum = intResult;
                return (SendResult)Enum.ToObject(typeof(SendResult),1);
            }
            return (SendResult)intResult;
        }

        /// <summary>
        /// 获取剩余短信数量
        /// </summary>
        /// <param name="smsInfo">sms短信平台信息</param>
        /// <returns>剩余短信数量</returns>
        public static SendResult GetSMSNum(SmsInfo smsInfo,out int smsNum,bool isMD5=true)
        {
            ///获取短信数量接口地址(UTF8)：
            ///http://sms.webchinese.cn/web_api/SMS/?Action=SMS_Num&Uid=本站用户名&Key=接口安全秘钥
            string requestUriString;//请求地址
            smsNum = 0;//最终剩余短信数量
            if (isMD5)
            {   //密钥加密
                requestUriString = String.Format("http://sms.webchinese.cn/web_api/SMS/?Action=SMS_Num&Uid={0}&KeyMD5={1}",
                    smsInfo.Uid, EncryptHelper.HashMD532(smsInfo.Key));
            }
            else
            {
                requestUriString = String.Format("http://sms.webchinese.cn/web_api/SMS/?Action=SMS_Num&Uid={0}&Key={1}", smsInfo.Uid, smsInfo.Key);
            }
            //创建请求并获取响应
            string result = GetResponseByUri(requestUriString);//读取响应结果
            int intResult = 0;//响应结果尝试转成整型
            int.TryParse(result, out intResult);
            if (intResult >= 0)
            {   //获取短信数量成功
                smsNum = intResult;
                return (SendResult)Enum.ToObject(typeof(SendResult), 1);
            }
            return (SendResult)intResult;
        }

        /// <summary>
        /// 通过Internet地址获取响应的资源
        /// </summary>
        /// <param name="requestUriString">请求uri地址</param>
        /// <returns>资源的字符串格式内容</returns>
        private static string GetResponseByUri(string requestUriString)
        {
            if(String.IsNullOrWhiteSpace(requestUriString))
            {
                return "";
            }
            //创建请求并获取响应
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 30 * 60 * 1000;
            WebResponse webResponse = httpWebRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            string result = streamReader.ReadToEnd();//读取响应结果
            streamReader.Close();
            return result;
        }
    }
}
