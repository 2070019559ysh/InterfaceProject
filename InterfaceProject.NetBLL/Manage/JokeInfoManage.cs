using InterfaceProject.NetDAL.Service;
using InterfaceProject.NetDTO;
using InterfaceProject.NetDTO.ManageDTO;
using InterfaceProject.NetModel.CoreDB;
using InterfaceProject.NetTool.Cache;
using InterfaceProject.NetTool.HelpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceProject.NetBLL.Manage
{
    /// <summary>
    /// 笑话数据处理Manage
    /// </summary>
    public class JokeInfoManage: IJokeInfoManage
    {
        private readonly RedisHelper redisHelper;
        private readonly IJokeInfoService jokeInfoService;

        public JokeInfoManage()
        {
            redisHelper = new RedisHelper();
            jokeInfoService = new JokeInfoService();
        }

        /// <summary>
        /// 保存笑话数据集合
        /// </summary>
        /// <param name="jokeInfoDtos">多个笑话数据的集合</param>
        /// <returns>保存成功的记录数</returns>
        public int SaveRangeJokes(List<JokeInfoDto> jokeInfoDtos)
        {
            if (jokeInfoDtos == null || jokeInfoDtos.Count == 0) return 0;
            Guid firstId = jokeInfoDtos[0].Id;
            string concurrencyKey = string.Format(RedisKeyPrefix.SAVE_JOKE_CONCURRENCY, firstId);
            int saveResult = ConcurrentControl.SingleUserFunc(concurrencyKey, 0, () =>
              {
                  List<JokeInfo> jokeInfoList = AutoMapperHelper.MapToList<JokeInfo>(jokeInfoDtos);
                  jokeInfoList.ForEach(joke=>
                  {
                      joke.CreateBy = nameof(SaveRangeJokes) + Thread.CurrentThread.ManagedThreadId;
                      joke.Version = SysConfigReader.Version;
                  });
                  int row = jokeInfoService.AddNeedJokeInfo(jokeInfoList);
                  return row;
              });
            return saveResult;
        }
    }
}
