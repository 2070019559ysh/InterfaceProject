using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 天气服务的城市信息保存Service
    /// </summary>
    public class WeatherCityService : IWeatherCityService
    {
        /// <summary>
        /// 实例化天气服务城市信息保存Service
        /// </summary>
        public WeatherCityService()
        {
        }

        /// <summary>
        /// 新增一个天气服务城市信息
        /// </summary>
        /// <param name="weatherCity">天气服务城市信息</param>
        public void AddWeatherCity(WeatherCity weatherCity)
        {
            using (var db = new InterfaceCoreDB())
            {
                db.WeatherCity.Add(weatherCity);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// SqlBulkCopy批量保存天气服务城市信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="wCityTable">天气服务城市信息表格数据</param>
        public void SaveWeatherCity(string connectionString,DataTable wCityTable)
        {
            using (var db = new InterfaceCoreDB())
            {
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(connectionString))
                {
                    sqlBulk.DestinationTableName = nameof(WeatherCity);
                    sqlBulk.ColumnMappings.Add("Id", "Id");
                    sqlBulk.ColumnMappings.Add("ParentId", "ParentId");
                    sqlBulk.ColumnMappings.Add("Name", "Name");
                    sqlBulk.ColumnMappings.Add("WCityLevel", "WCityLevel");
                    sqlBulk.ColumnMappings.Add("WCityLevelName", "WCityLevelName");
                    sqlBulk.ColumnMappings.Add("CreateTime", "CreateTime");
                    sqlBulk.WriteToServer(wCityTable);
                }
            }
        }

        /// <summary>
        /// 利用表类型批量保存天气服务城市信息
        /// </summary>
        /// <param name="wCityTable">天气服务城市信息表格数据</param>
        /// <returns>保存成功记录数</returns>
        public int SaveWeatherCity(DataTable wCityTable)
        {
            using (var db = new InterfaceCoreDB())
            {
                string sql = $"INSERT INTO {nameof(WeatherCity)} SELECT city.Id,city.ParentId,city.Name,city.WCityLevel,city.WCityLevelName,city.CreateTime FROM @WeatherCityTable AS city";
                SqlParameter param = new SqlParameter("@WeatherCityTable", wCityTable);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.WeatherCityTable";
                int rows = db.Database.ExecuteSqlCommand(sql, param);
                return rows;
            }
        }
    }
}
