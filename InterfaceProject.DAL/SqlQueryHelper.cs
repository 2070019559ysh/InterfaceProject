using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceProject.DAL
{
    /// <summary>
    /// 执行sql查询的帮助类
    /// </summary>
    public static class SqlQueryHelper
    {
        /// <summary>
        /// 扩展db.Database的查询功能
        /// </summary>
        /// <typeparam name="T">查询的数据类</typeparam>
        /// <param name="database">扩展DatabaseFacade</param>
        /// <param name="sql">查询Sql</param>
        /// <param name="parameters">查询需要的参数</param>
        /// <returns>返回数据集合</returns>
        public static IList<T> SqlQuery<T>(this DatabaseFacade database, string sql, params object[] parameters)
           where T : new()
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常
            var conn = database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    object firstParam = parameters?.FirstOrDefault();
                    if (firstParam != null)
                        command.Parameters.AddRange(parameters);
                    var propts = typeof(T).GetProperties();
                    var rtnList = new List<T>();
                    T model;
                    object val;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new T();
                            foreach (var l in propts)
                            {
                                val = reader[l.Name];
                                if (val == DBNull.Value)
                                {
                                    l.SetValue(model, null);
                                }
                                else
                                {
                                    l.SetValue(model, val);
                                }
                            }
                            rtnList.Add(model);
                        }
                    }
                    return rtnList;
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
