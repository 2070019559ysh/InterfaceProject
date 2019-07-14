using InterfaceProject.NetModel.CoreDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.NetDAL.Service
{
    /// <summary>
    /// 笑话数据处理IService接口
    /// </summary>
    public interface IJokeInfoService
    {
        /// <summary>
        /// 添加一批笑话数据入库
        /// </summary>
        /// <param name="jokeInfos">笑话数据集合</param>
        void AddRangeJokeInfo(List<JokeInfo> jokeInfos);

        /// <summary>
        /// 添加一批需要保存的笑话数据入库
        /// </summary>
        /// <param name="jokeInfos">笑话数据集合</param>
        int AddNeedJokeInfo(List<JokeInfo> jokeInfos);

        /// <summary>
        /// 按月份分页查找最新笑话集合
        /// </summary>
        /// <param name="total"></param>
        /// <param name="yearMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<JokeInfo> SearchJokeInfos(out int total, DateTime yearMonth, int pageIndex = 1, int pageSize = 20);
    }
}
