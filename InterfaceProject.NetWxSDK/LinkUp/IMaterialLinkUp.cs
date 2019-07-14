using InterfaceProject.NetWxSDK.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetWxSDK.LinkUp
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
    }
}
