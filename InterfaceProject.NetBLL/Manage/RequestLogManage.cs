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
    /// 请求日志的存储处理Manage
    /// </summary>
    public class RequestLogManage:ILogManage
    {
        private RedisHelper redisHelper = new RedisHelper();
        private ILogService logService;

        /// <summary>
        /// 实例化请求日志存储Manage
        /// </summary>
        public RequestLogManage()
        {
            logService = new RequestLogService();
        }

        /// <summary>
        /// 把Redis的日志数据保存到数据库
        /// </summary>
        /// <returns>保存到数据库的日志数量</returns>
        public int LogForRedisToDB()
        {
            string requestLogRemedy = redisHelper.StringGet(RedisKeyPrefix.REQUEST_LOG_REMEDY);
            DataTable requestLogTable = new DataTable();
            requestLogTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id",typeof(Guid)),
                new DataColumn("Name",typeof(string)),
                new DataColumn("RequestUrl",typeof(string)),
                new DataColumn("RequestMethod",typeof(string)),
                new DataColumn("RequestMsg",typeof(string)),
                new DataColumn("ResponseMsg",typeof(string)),
                new DataColumn("ExceptionMsg",typeof(string)),
                new DataColumn("ReferenceId",typeof(Guid)),
                new DataColumn("ReferenceTable",typeof(string)),
                new DataColumn("Level",typeof(string)),
                new DataColumn("Version",typeof(string)),
                new DataColumn("CreateTime",typeof(DateTime)),
            });
            List<RequestLog> requestLogList = null;//临时记录redis中被删除的
            if (!string.IsNullOrWhiteSpace(requestLogRemedy))
            {
                requestLogList = JsonConvert.DeserializeObject<List<RequestLog>>(requestLogRemedy);
                foreach (var requestLog in requestLogList)
                {
                    DataRow requestLogRow = requestLogTable.NewRow();
                    requestLogRow["Id"] = requestLog.Id == Guid.Empty ? Guid.NewGuid() : requestLog.Id;
                    requestLogRow["Name"] = requestLog.Name;
                    requestLogRow["RequestUrl"] = requestLog.RequestUrl;
                    requestLogRow["RequestMethod"] = requestLog.RequestMethod;
                    requestLogRow["RequestMsg"] = requestLog.RequestMsg;
                    requestLogRow["ResponseMsg"] = requestLog.ResponseMsg;
                    requestLogRow["ExceptionMsg"] = requestLog.ExceptionMsg;
                    requestLogRow["ReferenceId"] = requestLog.ReferenceId;
                    requestLogRow["ReferenceTable"] = requestLog.ReferenceTable;
                    requestLogRow["Level"] = requestLog.Level;
                    requestLogRow["Version"] = requestLog.Version;
                    requestLogRow["CreateTime"] = requestLog.CreateTime;
                    requestLogTable.Rows.Add(requestLogRow);
                }
                redisHelper.StringSet(RedisKeyPrefix.REQUEST_LOG_REMEDY, string.Empty, TimeSpan.FromTicks(100));
            }
            else
            {
                int i = 0;
                requestLogList = new List<RequestLog>();
                long total = redisHelper.ListLength(RedisKeyPrefix.REQUEST_LOG);
                long surplus = total;
                while (i < total && surplus > 0)
                {
                    RequestLog requestLog = redisHelper.ListLeftPop<RequestLog>(RedisKeyPrefix.REQUEST_LOG);
                    requestLogList.Add(requestLog);
                    if (requestLog != null && !string.IsNullOrWhiteSpace(requestLog.RequestMsg))
                    {
                        DataRow requestLogRow = requestLogTable.NewRow();
                        requestLogRow["Id"] = requestLog.Id == Guid.Empty ? Guid.NewGuid() : requestLog.Id;
                        requestLogRow["Name"] = requestLog.Name;
                        requestLogRow["RequestUrl"] = requestLog.RequestUrl;
                        requestLogRow["RequestMethod"] = requestLog.RequestMethod;
                        requestLogRow["RequestMsg"] = requestLog.RequestMsg;
                        requestLogRow["ResponseMsg"] = requestLog.ResponseMsg;
                        requestLogRow["ExceptionMsg"] = requestLog.ExceptionMsg;
                        requestLogRow["ReferenceId"] = requestLog.ReferenceId;
                        requestLogRow["ReferenceTable"] = requestLog.ReferenceTable;
                        requestLogRow["Level"] = requestLog.Level;
                        requestLogRow["Version"] = requestLog.Version;
                        requestLogRow["CreateTime"] = requestLog.CreateTime;
                        requestLogTable.Rows.Add(requestLogRow);
                    }
                    //redisHelper.ListRemove(RedisKeyPrefix.REQUEST_LOG, requestLog);
                    surplus = redisHelper.ListLength(RedisKeyPrefix.REQUEST_LOG);
                    i++;
                }
                if (i == 0) return 0;
            }
            try
            {
                int rows = logService.LogImportToDB(requestLogTable);
                return rows;
            }
            catch (Exception ex)
            {
                if (requestLogList != null && requestLogList.Count > 0)
                {
                    redisHelper.StringSet(RedisKeyPrefix.REQUEST_LOG_REMEDY, requestLogList);
                }
                SystemLogHelper.Error(MethodBase.GetCurrentMethod(), "LogImportToDB_日志入库保存失败，需要补救已删除的日志数据", ex);
                throw ex;
            }
        }
    }
}
