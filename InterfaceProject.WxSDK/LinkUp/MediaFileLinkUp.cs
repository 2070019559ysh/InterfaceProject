using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatApp.Common;
using WeChatApp.Core.ILinkUp;
using WeChatApp.Model.HelpModel;
using WetChatApp.LinkUp.HelpModel;

namespace WeChatApp.LinkUp
{
    /// <summary>
    /// 微信上传下载多媒体文件
    /// </summary>
    public class MediaFileLinkUp:IMediaFileLinkUp
    {
        /// <summary>
        /// 记录日志对象
        /// </summary>
        private readonly LogHelper _logObj = new LogHelper(typeof(ConnectLinkUp));
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        [Inject]
        public IConnectLinkUp Connect { get; set; }

        /// <summary>
        /// 上传多媒体文件，返回上传成功的media_id，否则null
        /// </summary>
        /// <param name="filePathName">包括完整路径的文件名</param>
        /// <param name="fileData">文件数据</param>
        /// <param name="fileType">文件类型：图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）</param>
        /// <returns>上传成功的media_id，否则null</returns>
        public MediaFile UploadMediaFile(string filePathName,string fileType)
        {
            //上传的多媒体文件有格式和大小限制，如下：
            //图片（image）: 1M，支持JPG格式
            //语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式
            //视频（video）：10MB，支持MP4格式
            //缩略图（thumb）：64KB，支持JPG格式
            //媒体文件在后台保存时间为3天，即3天后media_id失效
            string url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            Stream fileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read);
            UploadParameter uploadParam = new UploadParameter()
            {
                FileNameValue = Path.GetFileName(filePathName),
                UploadStream = fileStream
            };
            url = string.Format(url, Connect.GetAccessToken(), fileType);
            uploadParam.Url = url; 
            string resultStr = SimulateRequest.UploadFile(uploadParam);
            var resultObj = JsonConvert.DeserializeObject<JObject>(resultStr);
            if (resultObj["errcode"] != null && resultObj["errcode"].ToString() != "0")
            {
                _logObj.Error("上传多媒体文件UploadMediaFile()，微信服务报错");
                var errorCode = new WeixinErrorInfo(resultObj["errcode"].ToString());
                _logObj.Warn(string.Format("{0}【errmsg：{1}】", errorCode.ToString(), resultObj["errmsg"].ToString()));
                return null;
            }
            else
            {
                _logObj.InfoFormat("上传多媒体文件UploadMediaFile()，微信处理成功");
                MediaFile mediaFile=JsonConvert.DeserializeObject<MediaFile>(resultStr);
                return mediaFile;
            }
        }

        /// <summary>
        /// 从微信服务器下载多媒体文件，返回实际存储在本地的文件名，null则下载失败
        /// </summary>
        /// <param name="filePathName">保存在本地的文件名，（由于文件扩展名未知）文件名以实际返回为准</param>
        /// <param name="media_id">媒体文件Id</param>
        /// <returns>实际存储在本地的文件名</returns>
        public string DownloadMediaFile(string filePathName,string media_id)
        {
            string url = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
            url = string.Format(url, Connect.GetAccessToken(), media_id);
            try
            {
                string fileName = SimulateRequest.DownloadFile(url, filePathName);
                _logObj.InfoFormat("下载多媒体文件DownloadMediaFile()，微信处理成功，文件存储为：" + fileName);
                return fileName;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("{") && ex.Message.EndsWith("}"))
                {
                    var resultObj = JsonConvert.DeserializeObject<JObject>(ex.Message);
                    if (resultObj["errcode"] != null && resultObj["errcode"].ToString() != "0")
                    {
                        _logObj.Error("下载多媒体文件DownloadMediaFile()，微信服务报错");
                        var errorCode = new WeixinErrorInfo(resultObj["errcode"].ToString());
                        _logObj.Warn(string.Format("{0}【errmsg：{1}】", errorCode.ToString(), resultObj["errmsg"].ToString()));
                    }
                }
                else
                {
                    _logObj.Error("下载多媒体文件DownloadMediaFile()，出错",ex);
                }
            }
            return null;
        }
    }
}
