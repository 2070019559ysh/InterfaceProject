using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetTool.HelpModel
{
    /// <summary>
    /// 记录缓存时间常量
    /// </summary>
    public class CacheTime
    {
        /// <summary>
        /// 一会儿（单位：s）
        /// </summary>
        public const int Awhile_S = 60;
        /// <summary>
        /// 基本简短时间（单位：s）
        /// </summary>
        public const int Base_S = 300;
        /// <summary>
        /// 一时段（单位：s）
        /// </summary>
        public const int Term_S = 7200;
        /// <summary>
        /// 长久（单位：s）
        /// </summary>
        public const int Max_S = int.MaxValue;
    }
}
