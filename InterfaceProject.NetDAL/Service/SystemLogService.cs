using InterfaceProject.NetModel.Monitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 系统日志Service
    /// </summary>
    public class SystemLogService:ILogService
    {
        /// <summary>
        /// 批量把系统日志数据导入到库中保存
        /// </summary>
        /// <param name="systemLogTable">系统日志数据</param>
        /// <returns>成功保存记录数</returns>
        public int LogImportToDB(DataTable systemLogTable)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("INSERT INTO SystemLog");
            sqlBuilder.Append("SELECT LogTb.Id,LogTb.Module,LogTb.Content,LogTb.ThreadId,");
            sqlBuilder.AppendLine("LogTb.Level,LogTb.Version,LogTb.CreateTime");
            sqlBuilder.AppendLine("FROM @SystemLogTable AS LogTb");
            string sql = sqlBuilder.ToString();
            SqlParameter param = new SqlParameter("@SystemLogTable", systemLogTable);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.SystemLogType";
            using (var db = new InterfaceMonitorDB())
            {
                int rows = db.Database.ExecuteSqlCommand(sql, param);
                return rows;
            }
        }
    }
}
