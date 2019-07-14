using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 日志处理接口IService
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 批量把日志数据导入到库中保存
        /// </summary>
        /// <param name="logTable">日志数据</param>
        /// <returns>成功保存记录数</returns>
        int LogImportToDB(DataTable logTable);
    }
}
