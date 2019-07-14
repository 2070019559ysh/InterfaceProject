using InterfaceProject.OtherSDK.JuHe.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.OtherSDK.JuHe
{
    /// <summary>
    /// 聚合数据网提供笑话数据的提供器接口
    /// </summary>
    public interface IJokeProvider
    {
        /// <summary>
        /// 查找最新笑话数据
        /// </summary>
        /// <param name="pageIndex">当前页数，默认1</param>
        /// <param name="pageSize">每次返回条数，默认1，最大20</param>
        /// <returns>最新笑话数据集合</returns>
        JuHeResponse<JokesData> SearchNewestJokes(int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 查找指定时间前发布的笑话数据
        /// </summary>
        /// <param name="beforeTime">指定时间之前</param>
        /// <param name="juHeSort">数据排列方式，默认降序</param>
        /// <param name="pageIndex">当前页数，默认1</param>
        /// <param name="pageSize">每次返回条数，默认1，最大20</param>
        /// <returns>指定时间前发布的笑话数据集合</returns>
        JuHeResponse<JokesData> SearchTimeJokes(DateTime beforeTime, JuHeTimeFilter timeFilter = JuHeTimeFilter.After, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 随机获取笑话
        /// </summary>
        /// <returns>随机笑话数据集合</returns>
        JuHeResponse<JokesData> RandomJokes();
    }
}
