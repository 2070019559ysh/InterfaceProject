using InterfaceProject.Model.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.DAL.Service
{
    /// <summary>
    /// 笑话数据处理Service
    /// </summary>
    public class JokeInfoService: IJokeInfoService
    {
        private readonly InterfaceCoreDB db;

        public JokeInfoService(InterfaceCoreDB db)
        {
            this.db = db;
        }

        /// <summary>
        /// 按月份分页查找最新笑话集合
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="yearMonth">可查询指定年月份的数量，空代表查全部</param>
        /// <param name="pageIndex">当前页码，默认1开始</param>
        /// <param name="pageSize">指定每页最大记录数</param>
        /// <returns>笑话数据集合</returns>
        public List<JokeInfo> SearchJokeInfos(out int total, DateTime? yearMonth, int pageIndex = 1, int pageSize = 20)
        {
            total = 0;
            IQueryable<JokeInfo> jokeQuery;
            if (yearMonth.HasValue)
            {
                DateTime startTime = yearMonth.Value.Date;
                DateTime nextMonth = startTime.AddMonths(1);
                jokeQuery = db.JokeInfo.Where(joke => joke.IsDel == false && joke.CreateTime >= startTime
                && joke.CreateTime < nextMonth);
            }
            else
            {
                jokeQuery = db.JokeInfo.Where(joke => joke.IsDel == false);
            }
            total = jokeQuery.Count();
            List<JokeInfo> jokeInfos = jokeQuery.OrderByDescending(joke => joke.CreateTime)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return jokeInfos;
        }
    }
}
