using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 日志数据处理接口IManage
    /// </summary>
    public interface ILogManage
    {
        /// <summary>
        /// 把Redis中的日志数据保存到数据库去
        /// </summary>
        /// <returns>成功保存记录数</returns>
        int LogForRedisToDB();
    }
}
