using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDTO.CacheDTO
{
    /// <summary>
    /// 统一记录系统的缓存Key前缀
    /// </summary>
    public class RedisKeyPrefix
    {
        /// <summary>
        /// 保存笑话防止并发_{第一个笑话的Id}
        /// </summary>
        public const string SAVE_JOKE_CONCURRENCY = "SaveJokeConcurrency_{0}";
        /// <summary>
        /// 读取系统配置信息_{配置Key}
        /// </summary>
        public const string SYS_CONFIGINFO = "SysConfigInfo_{0}";
    }
}
