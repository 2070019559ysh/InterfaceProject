using InterfaceProject.NetTool.Log;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;

namespace InterfaceProject.NetTool
{
    /// <summary>
    /// 发送Email类
    /// </summary>
    public class SendEmail
    {
        /// <summary>
        /// 实例化发送Email类,使用默认值
        /// </summary>
        public SendEmail()
        {
            Server = "smtp.163.com";
            FromAddress = "tow070019559@163.com";
            Password = "503104plkj";
            ToAddressList = new List<string>();
            FileList = new List<string>();
            AlternateViews = new List<string>();
        }

        /// <summary>
        /// 实例化发送Email类，要指定stmp服务地址如：smtp.sina.com.cn，发件人地址及密码
        /// </summary>
        /// <param name="server">smtp服务地址</param>
        /// <param name="from">发件人地址</param>
        /// <param name="password">发件人密码</param>
        public SendEmail(string server, string from,string password)
        {
            Server = server;
            FromAddress = from;
            Password = password;
            ToAddressList = new List<string>();
            FileList = new List<string>();
            AlternateViews = new List<string>();
        }

        /// <summary>
        /// smtp服务地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 发件人地址
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 接收人地址集合，可以发给一个或多个人
        /// </summary>
        public List<string> ToAddressList { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件文件名列表
        /// </summary>
        public List<string> FileList { get; set; }

        /// <summary>
        /// 指定电子邮件不同格式的副本。 例如，如果您发送 HTML 格式的邮件，您可能希望同时提供邮件的纯文本格式，以防一些收件人使用的电子邮件阅读程序无法显示 HTML 内容。
        /// </summary>
        public List<string> AlternateViews { get; set; }

        /// <summary>
        /// 发送邮件并可发送附件
        /// </summary>
        /// <param name="isBodyHtml">发送的内容是否为html格式</param>
        /// <returns>是否发送成功</returns>
        public bool SendEmailWithAttachment(bool isBodyHtml=false)
        {
            try
            {
                // SmtpClient要发送的邮件实例
                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromAddress);
                message.Subject = Subject;
                message.SubjectEncoding = Encoding.UTF8; //标题编码
                message.Body = Body;
                message.BodyEncoding = Encoding.UTF8; //邮件内容编码
                message.IsBodyHtml = isBodyHtml;
                foreach (var to in ToAddressList)
                {
                    //添加接收人地址
                    message.To.Add(new MailAddress(to));
                }
                //添加邮件附件
                foreach (var file in FileList)
                {
                    //添加附件
                    // 为邮件创建文件附件对象
                    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    //为文件添加时间戳信息。
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    // Add the file attachment to this e-mail message.
                    //将文件附件添加到该电子邮件。
                    message.Attachments.Add(data);
                    //data.Dispose();
                }
                //添加纯文本格式的替代邮件内容
                foreach (var body in AlternateViews)
                {
                    ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    // Add the alternate body to the message.
                    AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                    message.AlternateViews.Add(alternate);
                }
                //创建基于密码的身份验证方案
                NetworkCredential nc = new NetworkCredential(FromAddress, Password);
                SmtpClient client = new SmtpClient(Server);
                //表示以当前登录用户的默认凭据进行身份验证
                client.UseDefaultCredentials = true;
                client.Credentials = nc;//设置验证发件人的身份凭证
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//待发的电子邮件通过网络发送到smtp服务器
                //Send the message.
                //正式发送信息
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Error(MethodBase.GetCurrentMethod(), "发送邮件失败_SendEmailWithAttachment", ex);
                return false;
            }
        }

        /// <summary>
        /// 发送邮件并可发送附件
        /// </summary>
        /// <param name="isBodyHtml">发送的内容是否为html格式</param>
        /// <returns>是否发送成功</returns>
        public void SendEmailWithAttachmentAsync(bool isBodyHtml = false)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                SendEmailWithAttachment(isBodyHtml);
            });
        }
    }
}
