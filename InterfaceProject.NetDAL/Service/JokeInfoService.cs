using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 笑话数据处理Service
    /// </summary>
    public class JokeInfoService: IJokeInfoService
    {
        /// <summary>
        /// 添加一批笑话数据入库
        /// </summary>
        /// <param name="jokeInfos">笑话数据集合</param>
        public void AddRangeJokeInfo(List<JokeInfo> jokeInfos)
        {
            using(var db = new InterfaceCoreDB())
            {
                db.JokeInfo.AddRange(jokeInfos);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加一批需要保存的笑话数据入库
        /// </summary>
        /// <param name="jokeInfos">笑话数据集合</param>
        public int AddNeedJokeInfo(List<JokeInfo> jokeInfos)
        {
            using (var db = new InterfaceCoreDB())
            {
                foreach(JokeInfo jokeInfo in jokeInfos)
                {
                    if (db.JokeInfo.Any(joke => joke.Id == jokeInfo.Id))
                        continue;
                    else
                    {
                        db.JokeInfo.Add(jokeInfo);
                    }
                }
                int row = db.SaveChanges();
                return row;
            }
        }

        /// <summary>
        /// 按月份分页查找最新笑话集合
        /// </summary>
        /// <param name="total"></param>
        /// <param name="yearMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<JokeInfo> SearchJokeInfos(out int total, DateTime yearMonth, int pageIndex = 1, int pageSize = 20)
        {
            total = 0;
            using (var db = new InterfaceCoreDB())
            {
                var jokeQuery = db.JokeInfo.Where(joke => joke.CreateTime.Year == yearMonth.Year
                && joke.CreateTime.Month == yearMonth.Month);
                total = jokeQuery.Count();
                List<JokeInfo> jokeInfos = jokeQuery.OrderByDescending(joke => joke.CreateTime)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return jokeInfos;
            }
        }
    }
}
