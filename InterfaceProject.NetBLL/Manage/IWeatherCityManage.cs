using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 天气服务的城市信息保存IManage接口声明
    /// </summary>
    public interface IWeatherCityManage
    {
        /// <summary>
        /// 批量保存天气服务的城市信息
        /// </summary>
        /// <param name="connectionString">基本数据库连接字符串</param>
        /// <param name="weatherCities">天气服务的城市信息</param>
        void SaveWeatherCity(string connectionString, List<WeatherCity> weatherCities);

        /// <summary>
        /// 批量保存天气服务的城市信息
        /// </summary>
        /// <param name="weatherCities">天气服务的城市信息</param>
        /// <returns>成功保存记录数</returns>
        int SaveWeatherCity(List<WeatherCity> weatherCities);
    }
}
