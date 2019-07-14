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
    /// 请求日志Service
    /// </summary>
    public class RequestLogService: ILogService
    {
        /// <summary>
        /// 批量把系统日志数据导入到库中保存
        /// </summary>
        /// <param name="systemLogTable">系统日志数据</param>
        /// <returns>成功保存记录数</returns>
        public int LogImportToDB(DataTable requestLogTable)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("INSERT INTO RequestLog");
            sqlBuilder.Append("SELECT LogTb.Id,LogTb.Name,LogTb.RequestUrl,LogTb.RequestMethod,");
            sqlBuilder.Append("LogTb.RequestMsg,LogTb.ResponseMsg,LogTb.ExceptionMsg,LogTb.ReferenceId,");
            sqlBuilder.AppendLine("LogTb.ReferenceTable,LogTb.Level,LogTb.Version,LogTb.CreateTime");
            sqlBuilder.AppendLine("FROM @RequestLogTable AS LogTb");
            string sql = sqlBuilder.ToString();
            SqlParameter param = new SqlParameter("@RequestLogTable", requestLogTable);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.RequestLogType";
            using (var db = new InterfaceMonitorDB())
            {
                int rows = db.Database.ExecuteSqlCommand(sql, param);
                return rows;
            }
        }
    }
}
