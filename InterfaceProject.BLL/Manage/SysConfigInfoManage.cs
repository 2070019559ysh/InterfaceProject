using InterfaceProject.DAL.Service;
using InterfaceProject.Model.CoreDB;
using InterfaceProject.Tool.Cache;
using InterfaceProject.Tool.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.BLL.Manage
{
    /// <summary>
    /// 系统配置信息处理Manage
    /// </summary>
    public class SysConfigInfoManage: ISysConfigInfoManage
    {
        private readonly RedisHelper redisHelper;
        private readonly ISysConfigInfoService configInfoService;

        public SysConfigInfoManage(ISysConfigInfoService configInfoService)
        {
            redisHelper = new RedisHelper();
            this.configInfoService = configInfoService;
        }

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">指定Key</param>
        /// <param name="isReload">是否忽略缓存重新加载</param>
        /// <returns>指定Key的系统配置信息</returns>
        public SysConfigInfo GetConfigInfo(string key, bool isReload = false)
        {
            string redisKey = string.Format(RedisKeyPrefix.SYSTEM_CONFIG, key);
            SysConfigInfo configInfo = null;
            bool redisHas = redisHelper.KeyExists(redisKey);
            if (!isReload && redisHas)
            {
                
                configInfo = redisHelper.StringGet<SysConfigInfo>(redisKey);
            }
            if (configInfo == null)
            {
                configInfo = configInfoService.GetConfigInfo(key);
                if (configInfo != null)
                {
                    redisHelper.StringSet(redisKey, configInfo, TimeSpan.FromSeconds(CacheTime.Awhile_S));
                }
            }
            return configInfo;
        }
    }
}
