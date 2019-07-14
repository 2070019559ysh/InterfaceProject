using InterfaceProject.DAL.Service;
using InterfaceProject.DTO.ViewModel;
using InterfaceProject.Model.CoreDB;
using InterfaceProject.Tool.Cache;
using InterfaceProject.Tool.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceProject.BLL.Manage
{
    /// <summary>
    /// 笑话数据处理Manage
    /// </summary>
    public class JokeInfoManage: IJokeInfoManage
    {
        private readonly RedisHelper redisHelper;
        private readonly IJokeInfoService jokeInfoService;

        public JokeInfoManage(IJokeInfoService jokeInfoService)
        {
            redisHelper = new RedisHelper();
            this.jokeInfoService = jokeInfoService;
        }

        /// <summary>
        /// 按月份分页查找最新笑话集合
        /// </summary>
        /// <param name="yearMonth">可查询指定年月份的数量，空代表查全部</param>
        /// <param name="pageIndex">当前页码，默认1开始</param>
        /// <param name="pageSize">指定每页最大记录数</param>
        /// <returns>笑话总记录数和数据集合</returns>
        public DataPage<List<JokeInfo>> SearchJokeInfos(DateTime? yearMonth, int pageIndex = 1, int pageSize = 20)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 1;
            string paramStr = yearMonth?.ToShortDateString() + "_" + pageIndex + "_" + pageSize;
            string jokeTotalKey = string.Format(RedisKeyPrefix.JOKE_COUNT, paramStr);
            string jokeListKey = string.Format(RedisKeyPrefix.JOKE_LIST, paramStr);
            string totalStr = redisHelper.StringGet(jokeTotalKey);
            List<JokeInfo> jokeInfoList = redisHelper.StringGet<List<JokeInfo>>(jokeListKey);
            int total;
            int.TryParse(totalStr, out total);
            if (jokeInfoList == null)
            {
                jokeInfoList = jokeInfoService.SearchJokeInfos(out total, yearMonth, pageIndex, pageSize);
                if (jokeInfoList != null && jokeInfoList.Count != 0)
                {
                    redisHelper.StringSet(jokeListKey, jokeInfoList, TimeSpan.FromSeconds(CacheTime.Base_S));
                    redisHelper.StringSet(jokeTotalKey, total, TimeSpan.FromSeconds(CacheTime.Base_S));
                }
            }
            return new DataPage<List<JokeInfo>>()
            {
                aaData = jokeInfoList,
                iTotalDisplayRecords = total,
                iTotalRecords = pageSize
            };
        }
    }
}
