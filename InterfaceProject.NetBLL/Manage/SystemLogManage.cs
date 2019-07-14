using InterfaceProject.NetDAL.Service;
using InterfaceProject.NetModel.Monitor;
using InterfaceProject.NetTool.Cache;
using InterfaceProject.NetTool.HelpModel;
using InterfaceProject.NetTool.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 系统的日志存储处理Manage
    /// </summary>
    public class SystemLogManage:ILogManage
    {
        private RedisHelper redisHelper = new RedisHelper();
        private ILogService logService;

        /// <summary>
        /// 实例化日志存储Manage
        /// </summary>
        public SystemLogManage()
        {
            logService = new SystemLogService();
        }

        /// <summary>
        /// 把Redis的日志数据保存到数据库
        /// </summary>
        /// <returns>保存到数据库的日志数量</returns>
        public int LogForRedisToDB()
        {
            string systemLogRemedy = redisHelper.StringGet(RedisKeyPrefix.SYSTEM_LOG_REMEDY);
            DataTable systemLogTable = new DataTable();
            systemLogTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id",typeof(Guid)),
                new DataColumn("Module",typeof(string)),
                new DataColumn("Content",typeof(string)),
                new DataColumn("ThreadId",typeof(int)),
                new DataColumn("Level",typeof(string)),
                new DataColumn("Version",typeof(string)),
                new DataColumn("CreateTime",typeof(DateTime)),
            });
            List<SystemLog> systemLogList = null;//临时记录redis中被删除的
            if (!string.IsNullOrWhiteSpace(systemLogRemedy))
            {
                systemLogList = JsonConvert.DeserializeObject<List<SystemLog>>(systemLogRemedy);
                foreach (var systemLog in systemLogList)
                {
                    DataRow systemLogRow = systemLogTable.NewRow();
                    systemLogRow["Id"] = systemLog.Id == Guid.Empty ? Guid.NewGuid() : systemLog.Id;
                    systemLogRow["Module"] = systemLog.Module;
                    systemLogRow["Content"] = systemLog.Content;
                    systemLogRow["ThreadId"] = systemLog.ThreadId;
                    systemLogRow["Level"] = systemLog.Level;
                    systemLogRow["Version"] = systemLog.Version;
                    systemLogRow["CreateTime"] = systemLog.CreateTime;
                    systemLogTable.Rows.Add(systemLogRow);
                }
                redisHelper.StringSet(RedisKeyPrefix.SYSTEM_LOG_REMEDY, string.Empty, TimeSpan.FromSeconds(1));
            }
            else
            {
                int i = 0;
                systemLogList = new List<SystemLog>();
                long total = redisHelper.ListLength(RedisKeyPrefix.SYSTEM_LOG);
                long surplus = total;
                while (i < total && surplus > 0)
                {
                    SystemLog systemLog = redisHelper.ListLeftPop<SystemLog>(RedisKeyPrefix.SYSTEM_LOG);
                    systemLogList.Add(systemLog);
                    if (systemLog != null && !string.IsNullOrWhiteSpace(systemLog.Content))
                    {
                        DataRow systemLogRow = systemLogTable.NewRow();
                        systemLogRow["Id"] = systemLog.Id == Guid.Empty ? Guid.NewGuid() : systemLog.Id;
                        systemLogRow["Module"] = systemLog.Module;
                        systemLogRow["Content"] = systemLog.Content;
                        systemLogRow["ThreadId"] = systemLog.ThreadId;
                        systemLogRow["Level"] = systemLog.Level;
                        systemLogRow["Version"] = systemLog.Version;
                        systemLogRow["CreateTime"] = systemLog.CreateTime;
                        systemLogTable.Rows.Add(systemLogRow);
                    }
                    //redisHelper.ListRemove(RedisKeyPrefix.SYSTEM_LOG, systemLog);
                    surplus = redisHelper.ListLength(RedisKeyPrefix.SYSTEM_LOG);
                    i++;
                }
                if (i == 0) return 0;
            }
            try
            {
                int rows = logService.LogImportToDB(systemLogTable);
                return rows;
            }
            catch (Exception ex)
            {
                if (systemLogList != null && systemLogList.Count > 0)
                {
                    redisHelper.StringSet(RedisKeyPrefix.SYSTEM_LOG_REMEDY, systemLogList);
                }
                SystemLogHelper.Error(MethodBase.GetCurrentMethod(),"LogImportToDB_日志入库保存失败，需要补救已删除的日志数据", ex);
                throw ex;
            }
        }
    }
}
