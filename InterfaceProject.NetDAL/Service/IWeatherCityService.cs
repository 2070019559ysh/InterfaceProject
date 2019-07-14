using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 天气服务的城市信息保存IService接口声明
    /// </summary>
    public interface IWeatherCityService
    {
        /// <summary>
        /// 新增一个天气服务城市信息
        /// </summary>
        /// <param name="weatherCity">天气服务城市信息</param>
        void AddWeatherCity(WeatherCity weatherCity);

        /// <summary>
        /// SqlBulkCopy批量保存天气服务城市信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="wCityTable">天气服务城市信息表格数据</param>
        void SaveWeatherCity(string connectionString, DataTable wCityTable);

        /// <summary>
        /// 利用表类型批量保存天气服务城市信息
        /// </summary>
        /// <param name="wCityTable">天气服务城市信息表格数据</param>
        /// <returns>保存成功记录数</returns>
        int SaveWeatherCity(DataTable wCityTable);
    }
}
