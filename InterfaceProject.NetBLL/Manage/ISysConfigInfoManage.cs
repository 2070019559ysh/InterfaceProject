using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 系统配置信息处理IManage接口
    /// </summary>
    public interface ISysConfigInfoManage
    {
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">指定Key</param>
        /// <param name="isReload">是否忽略缓存重新加载</param>
        /// <returns>指定Key的系统配置信息</returns>
        SysConfigInfo GetConfigInfo(string key, bool isReload = false);
    }
}
