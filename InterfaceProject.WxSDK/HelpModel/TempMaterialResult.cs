using InterfaceProject.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 临时素材结果封装类
    /// </summary>
    public class TempMaterialResult
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息（news）
        /// </summary>
        public string type;
        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
        public string media_id;
        /// <summary>
        /// 缩略图上传后获取的唯一标识
        /// </summary>
        public string thumb_media_id;
        /// <summary>
        /// 媒体文件上传时间的时间戳
        /// </summary>
        public int created_at;
        /// <summary>
        /// 媒体文件上传时间
        /// </summary>
        public DateTime createAt
        {
            get
            {
                DateTime createDateTime = TimeHelper.GetDateTime(created_at);
                return createDateTime;
            }
        }
    }

    /// <summary>
    /// 视频素材的描述
    /// </summary>
    public class VideoDescription
    {
        /// <summary>
        /// 视频素材的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 视频素材的描述
        /// </summary>
        public string introduction;
    }

    /// <summary>
    /// 视频素材的描述结果
    /// </summary>
    public class VideoMaterialResult
    {
        /// <summary>
        /// 视频素材的标题
        /// </summary>
        public string title;
        /// <summary>
        /// 视频素材的描述
        /// </summary>
        public string introduction;
        /// <summary>
        /// 视频下载请求地址
        /// </summary>
        public string down_url;
    }
}
