using InterfaceProject.WxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.LinkUp
{
    /// <summary>
    /// 微信素材管理接入接口声明
    /// </summary>
    public interface IMaterialLinkUp
    {
        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        MaterialLinkUp Initialize(string idOrAppId);

        /// <summary>
        /// 新增临时素材，注意3天后media_id失效
        /// </summary>
        /// <param name="filePathName">菜单按钮数组</param>
        /// <returns>微信服务返回的创建结果</returns>
        WeChatResult<TempMaterialResult> AddTempMaterial(string filePathName);

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="mediaId">临时素材的媒体文件ID</param>
        /// <param name="filePathName">指定个临时包含路径的文件名</param>
        /// <returns>实际保存好的包含路径的文件名</returns>
        WeChatResult<string> GetTempMaterial(string mediaId, string filePathName);

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="articleNews">图文消息素材</param>
        /// <returns>图文素材MediaId</returns>
        WeChatResult<Media_Msg> AddNews(ArticleNews articleNews);

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="filePathName">包含完整访问路径的图片文件名</param>
        /// <returns>包含图片URL的微信响应结果</returns>
        WeChatResult<WeChatURL> GetUrlByUpdateImg(string filePathName);

        /// <summary>
        /// 新增其他类型永久素材
        /// </summary>
        /// <param name="filePathName">指定包含完整路径的文件名</param>
        /// <param name="videoDescription">上传视频素材时，需提供视频的描述信息</param>
        /// <returns>新增永久素材的结果</returns>
        WeChatResult<MaterialResult> AddMaterial(string filePathName, VideoDescription videoDescription = null);

        /// <summary>
        /// 获取永久素材，返回ArticleItemsInfo、VideoMaterialResult或fileName
        /// </summary>
        /// <param name="mediaId">永久素材的媒体文件ID</param>
        /// <param name="filePathName">指定个临时包含路径的文件名</param>
        /// <returns>图文素材请解析为ArticleItemsInfo，视频素材请解析为VideoMaterialResult，其他素材为实际保存好的包含路径的文件名</returns>
        WeChatResult<string> GetMaterial(string mediaId, string filePathName);

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="mediaId">要删除的永久素材Id</param>
        /// <returns>微信服务返回的删除结果</returns>
        WeChatResult DelMaterial(string mediaId);

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId">要修改的永久图文素材Id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <returns>微信服务返回的修改结果</returns>
        WeChatResult UpdateNews(string mediaId, int index, ArticleMaterial articleMaterial);

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <returns>微信服务返回的数量结果</returns>
        WeChatResult<MaterialCount> GetMaterialCount();

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）、图文（news）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns>微信服务返回的素材列表结果</returns>
        WeChatResult<MaterialListInfo> GetMaterialList(string type, int offset, int count);
    }
}
