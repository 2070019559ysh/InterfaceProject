using InterfaceProject.NetOtherSDK.JuHe.HelpModel;
using System;
using System.Collections.Generic;
using System.Text;
using InterfaceProject.NetTool;

namespace InterfaceProject.NetOtherSDK.JuHe
{
    /// <summary>
    /// 通过聚合数据网提供笑话数据的提供器
    /// </summary>
    public class JokeProvider:IJokeProvider
    {
        /// <summary>
        /// 查找最新笑话数据
        /// </summary>
        /// <param name="pageIndex">当前页数，默认1</param>
        /// <param name="pageSize">每次返回条数，默认1，最大20</param>
        /// <returns>最新笑话数据集合</returns>
        public JuHeResponse<JokesData> SearchNewestJokes(int pageIndex=1,int pageSize=20)
        {
            string url = string.Format("http://v.juhe.cn/joke/content/text.php?key={0}&page={1}&pagesize={2}",
                JuHeOptions.AppKey, pageIndex, pageSize);
            JuHeResponse<JokesData> jokesDataResult = SimulateRequest.HttpGet<JuHeResponse<JokesData>>(url);
            return jokesDataResult;
        }

        /// <summary>
        /// 查找指定时间前发布的笑话数据
        /// </summary>
        /// <param name="splitTime">指定分隔时间</param>
        /// <param name="timeFilter">数据排列方式，默认指定时间之后</param>
        /// <param name="pageIndex">当前页数，默认1</param>
        /// <param name="pageSize">每次返回条数，默认1，最大20</param>
        /// <returns>指定时间前发布的笑话数据集合</returns>
        public JuHeResponse<JokesData> SearchTimeJokes(DateTime splitTime, JuHeTimeFilter timeFilter = JuHeTimeFilter.After, int pageIndex = 1, int pageSize = 20)
        {
            string sort = "asc";
            if (timeFilter == JuHeTimeFilter.Before)//聚合的排序与我们的习惯相反
                sort = "desc";
            long time = TimeHelper.GetTime(splitTime);
            string url = string.Format("http://v.juhe.cn/joke/content/list.php?key={0}&page={1}&pagesize={2}&sort={3}&time={4}",
                JuHeOptions.AppKey, pageIndex, pageSize, sort, time);
            JuHeResponse<JokesData> jokesDataResult = SimulateRequest.HttpGet<JuHeResponse<JokesData>>(url);
            return jokesDataResult;
        }

        /// <summary>
        /// 随机获取笑话
        /// </summary>
        /// <returns>随机笑话数据集合</returns>
        public JuHeResponse<JokesData> RandomJokes()
        {
            string url = string.Format("http://v.juhe.cn/joke/randJoke.php?key={0}", JuHeOptions.AppKey);
            JuHeResponse<JokesData> jokesDataResult = SimulateRequest.HttpGet<JuHeResponse<JokesData>>(url);
            return jokesDataResult;
        }
    }
}
