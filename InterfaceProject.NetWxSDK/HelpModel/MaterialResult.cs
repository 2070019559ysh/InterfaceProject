using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.HelpModel
{
    /// <summary>
    /// 素材上传结果
    /// </summary>
    public class MaterialResult
    {
        /// <summary>
        /// 新增的永久素材的media_id
        /// </summary>
        public string media_id;
        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url;
    }

    /// <summary>
    /// 素材总数封装
    /// </summary>
    public class MaterialCount
    {
        /// <summary>
        /// 语音总数量
        /// </summary>
        public int voice_count;
        /// <summary>
        /// 视频总数量
        /// </summary>
        public int video_count;
        /// <summary>
        /// 图片总数量
        /// </summary>
        public int image_count;
        /// <summary>
        /// 图文总数量
        /// </summary>
        public int news_count;
    }

    /// <summary>
    /// 素材列表信息封装
    /// </summary>
    public class MaterialListInfo
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count;
        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count;
        /// <summary>
        /// 包含多个素材项的内容
        /// </summary>
        public List<MaterialItemInfo> item;
    }

    /// <summary>
    /// 素材单项信息
    /// </summary>
    public class MaterialItemInfo
    {
        /// <summary>
        /// 永久素材的media_id
        /// </summary>
        public string media_id;
        
    }

    /// <summary>
    /// 图文消息的素材单项信息
    /// </summary>
    public class NewsMaterialItem : MaterialItemInfo
    {
        /// <summary>
        /// 图文消息的具体内容
        /// </summary>
        public ArticleItemsInfo content;
    }

    /// <summary>
    /// 其他类型（图片、语音、视频）的素材单项信息
    /// </summary>
    public class MediaMaterialItem : MaterialItemInfo
    {
        /// <summary>
        /// 素材名称
        /// </summary>
        public string name;
        /// <summary>
        /// 素材的最后更新时间
        /// </summary>
        public string update_time;
        /// <summary>
        /// 素材的URL
        /// </summary>
        public string url;
    }
}
