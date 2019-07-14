using InterfaceProject.Tool;
using InterfaceProject.Tool.HelpModel;
using InterfaceProject.Tool.Log;
using InterfaceProject.WxSDK.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 微信素材管理接入实现
    /// </summary>
    public class MaterialLinkUp : IMaterialLinkUp
    {
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;

        /// <summary>
        /// 实例化微信素材管理处理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat">微信对接基础对象</param>
        public MaterialLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        public MaterialLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            return this;
        }

        /// <summary>
        /// 新增临时素材，注意3天后media_id失效
        /// </summary>
        /// <param name="filePathName">指定包含完整路径的文件名</param>
        /// <returns>微信服务返回的创建结果</returns>
        public WeChatResult<TempMaterialResult> AddTempMaterial(string filePathName)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            string accessToken = connect.GetAccessToken();
            string type = MimeMapping.GetMimeMapping(filePathName);//获取文件的Mime-Type
            type = type.Substring(0, type.IndexOf('/'));//提取Mime-Type的前部分
            string fileName = Path.GetFileName(filePathName);
            using (Stream fileStream = new FileStream(filePathName, FileMode.Open))
            {
                if ("image".Equals(type) && fileStream.Length < 64 * 1024)
                {
                    type = "thumb";//如果文件属于图片，而且小于64KB的，类型改成缩略图（thumb）
                }
                else if ("audio".Equals(type))
                {
                    type = "voice";
                }
                url = string.Format(url, accessToken, type);
                string resultStr = SimulateRequest.UploadFile(new UploadFileParam(url, fileName, fileStream));
                WeChatResult<TempMaterialResult> weChatResult = new WeChatResult<TempMaterialResult>(resultStr);
                if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
                {
                    SystemLogHelper.Warn(GetType().FullName, $"新增临时素材AddTempMaterial，微信服务报错：{weChatResult}");
                }
                return weChatResult;
            }
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="mediaId">临时素材的媒体文件ID</param>
        /// <param name="filePathName">指定个临时包含路径的文件名</param>
        /// <returns>实际保存好的包含路径的文件名</returns>
        public WeChatResult<string> GetTempMaterial(string mediaId, string filePathName)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/media/get?access_token={accessToken}&media_id={mediaId}";
            try
            {
                string fileName = SimulateRequest.DownloadFile(url, filePathName);
                WeChatResult<string> weChatResult = new WeChatResult<string>("{\"errcode\":0,\"errmsg\":\"SUCCESS\"}");
                weChatResult.resultData = fileName;
                return weChatResult;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Error(GetType().FullName, $"获取临时素材GetTempMaterial，报错", ex);
                WeChatResult<string> errorResult = new WeChatResult<string>(ex.Message);
                return errorResult;
            }
        }

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="articleNews">图文消息素材</param>
        /// <returns>图文素材MediaId</returns>
        public WeChatResult<Media_Msg> AddNews(ArticleNews articleNews)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={accessToken}";
            string resultStr = SimulateRequest.HttpPost(url, articleNews);
            WeChatResult<Media_Msg> weChatResult = new WeChatResult<Media_Msg>(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"新增永久图文素材AddNews，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="filePathName">包含完整访问路径的图片文件名</param>
        /// <returns>包含图片URL的微信响应结果</returns>
        public WeChatResult<WeChatURL> GetUrlByUpdateImg(string filePathName)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={accessToken}";
            string fileName = Path.GetFileName(filePathName);
            using (Stream fileStream = new FileStream(filePathName, FileMode.Open))
            {
                string resultStr = SimulateRequest.UploadFile(new UploadFileParam(url, fileName, fileStream));
                WeChatResult<WeChatURL> weChatResult = new WeChatResult<WeChatURL>(resultStr);
                if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
                {
                    SystemLogHelper.Warn(GetType().FullName, $"上传图文消息内的图片获取URLGetUrlByUpdateImg，微信服务报错：{weChatResult}");
                }
                return weChatResult;
            }
        }

        /// <summary>
        /// 新增其他类型永久素材
        /// </summary>
        /// <param name="filePathName">指定包含完整路径的文件名</param>
        /// <param name="videoDescription">上传视频素材时，需提供视频的描述信息</param>
        /// <returns>新增永久素材的结果</returns>
        public WeChatResult<MaterialResult> AddMaterial(string filePathName, VideoDescription videoDescription = null)
        {
            string accessToken = connect.GetAccessToken();
            string type = MimeMapping.GetMimeMapping(filePathName);//获取文件的Mime-Type
            type = type.Substring(0, type.IndexOf('/'));//提取Mime-Type的前部分
            string fileName = Path.GetFileName(filePathName);
            using (Stream fileStream = new FileStream(filePathName, FileMode.Open))
            {
                if ("image".Equals(type) && fileStream.Length < 64 * 1024)
                {
                    type = "thumb";//如果文件属于图片，而且小于64KB的，类型改成缩略图（thumb）
                }
                else if ("audio".Equals(type))
                {
                    type = "voice";
                }
                string url = $"https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={accessToken}&type={type}";
                UploadFileParam fileParam = new UploadFileParam(url, fileName, fileStream);
                if(videoDescription != null)
                {
                    fileParam.PostParameters.Add("title", videoDescription.title);
                    fileParam.PostParameters.Add("introduction", videoDescription.introduction);
                }
                string resultStr = SimulateRequest.UploadFile(fileParam);
                WeChatResult<MaterialResult> weChatResult = new WeChatResult<MaterialResult>(resultStr);
                if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
                {
                    SystemLogHelper.Warn(GetType().FullName, $"上传图文消息内的图片获取URLGetUrlByUpdateImg，微信服务报错：{weChatResult}");
                }
                return weChatResult;
            }
        }

        /// <summary>
        /// 获取永久素材，返回ArticleItemsInfo、VideoMaterialResult或fileName
        /// </summary>
        /// <param name="mediaId">永久素材的媒体文件ID</param>
        /// <param name="filePathName">指定个临时包含路径的文件名</param>
        /// <returns>图文素材请解析为ArticleItemsInfo，视频素材请解析为VideoMaterialResult，其他素材为实际保存好的包含路径的文件名</returns>
        public WeChatResult<string> GetMaterial(string mediaId, string filePathName)
        {
            string accessToken = connect.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={accessToken}";
            try
            {
                string fileName = SimulateRequest.DownloadFilePost(url, filePathName, new
                {
                    media_id = mediaId
                });
                WeChatResult<string> weChatResult = new WeChatResult<string>("{\"errcode\":0,\"errmsg\":\"SUCCESS\"}");
                weChatResult.resultData = fileName;
                return weChatResult;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Error(GetType().FullName, $"获取永久素材GetMaterial，报错", ex);
                WeChatResult<string> errorResult = new WeChatResult<string>(ex.Message);
                return errorResult;
            }
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="mediaId">要删除的永久素材Id</param>
        /// <returns>微信服务返回的删除结果</returns>
        public WeChatResult DelMaterial(string mediaId)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpPost(url, new { media_id = mediaId });
            WeChatResult weChatResult = new WeChatResult(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"删除永久素材DelMaterial，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId">要修改的永久图文素材Id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <returns>微信服务返回的修改结果</returns>
        public WeChatResult UpdateNews(string mediaId, int index, ArticleMaterial articleMaterial)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/material/update_news?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpPost(url, new
            {
                media_id = mediaId,
                index,
                articles = articleMaterial
            });
            WeChatResult weChatResult = new WeChatResult(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"修改永久图文素材UpdateNews，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <returns>微信服务返回的数量结果</returns>
        public WeChatResult<MaterialCount> GetMaterialCount()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpGet(url);
            WeChatResult<MaterialCount> weChatResult = new WeChatResult<MaterialCount>(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"获取素材总数GetMaterialCount，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）、图文（news）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns>微信服务返回的素材列表结果</returns>
        public WeChatResult<MaterialListInfo> GetMaterialList(string type,int offset,int count)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=" + accessToken;
            string resultStr = SimulateRequest.HttpPost(url, new { type, offset, count });
            WeChatResult<MaterialListInfo> weChatResult = new WeChatResult<MaterialListInfo>(resultStr);
            weChatResult.resultData.item.Clear();
            JObject wechatResultObj = JsonConvert.DeserializeObject<JObject>(resultStr);
            if ("news".Equals(type)) //图文消息素材
            {
                weChatResult.resultData.item.AddRange(JsonConvert.DeserializeObject<List<NewsMaterialItem>>(wechatResultObj["item"].ToString()));
            }
            else //其他多媒体素材
            {
                weChatResult.resultData.item.AddRange(JsonConvert.DeserializeObject<List<MediaMaterialItem>>(wechatResultObj["item"].ToString()));
            }
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(GetType().FullName, $"获取素材列表GetMaterialList，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }
    }
}
