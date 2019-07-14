using InterfaceProject.NetBLL.Manage;
using InterfaceProject.NetDTO.ManageDTO;
using InterfaceProject.NetOtherSDK.JuHe;
using InterfaceProject.NetOtherSDK.JuHe.HelpModel;
using InterfaceProject.NetTool;
using InterfaceProject.WinForm.InnerTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.WinForm.TimerTask
{
    /// <summary>
    /// 处理笑话数据定时器
    /// </summary>
    public class JokeInfoTimer:AbstractTimerTask
    {
        private readonly IJokeProvider jokeProvider;
        private readonly IJokeInfoManage jokeInfoManage;
        /// <summary>
        /// 当前查找页码
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 统计连续多少次没有保存到记录
        /// </summary>
        private int nonDataIndex = 0;
        /// <summary>
        /// 总共查找次数
        /// </summary>
        private int totalSearch = 0;

        public JokeInfoTimer()
        {
            jokeProvider = new JokeProvider();
            jokeInfoManage = new JokeInfoManage();
        }

        /// <summary>
        /// 定时任务执行的方法
        /// </summary>
        /// <param name="console">控制台输出</param>
        public override void ExecMethod(RichTextConsole console)
        {
            console.Info($"执行{TimerName}定时任务开始...", isWriteLog: false);
            totalSearch++;
            if (totalSearch > 50)//总共查找次数达到50重新从第一页开始
            {
                totalSearch = 1;
                pageIndex = 1;
            }
            JuHeResponse<JokesData> jokeResult = jokeProvider.SearchNewestJokes(pageIndex);
            if (jokeResult.error_code == 0)
            {
                List<JokeInfo> jokeList = jokeResult.result.data;
                List<JokeInfoDto> jokeDtoList = jokeList.Select(joke => new JokeInfoDto
                {
                    Id = joke.hashId,
                    Content = joke.content,
                    CreateTime = TimeHelper.GetDateTime(joke.unixtime),
                    UpdateTime = joke.updatetime
                }).ToList();
                int saveRow = jokeInfoManage.SaveRangeJokes(jokeDtoList);
                if (saveRow > 0)
                {
                    console.Info($"当前页码：{pageIndex}，保存最新笑话数据成功，共保存{saveRow}条记录", isWriteLog: false);
                    if (pageIndex > 1) pageIndex++;
                }
                else
                {
                    console.Warn($"当前页码：{pageIndex}，保存最新笑话数据0条，可能远程没有更新笑话", isWriteLog: false);
                    nonDataIndex++;
                    if (nonDataIndex > 2)
                    {
                        pageIndex = pageIndex * 2;
                        nonDataIndex = 0;//快速双倍翻页后，累计没有保存到数据的次数清0
                    }
                    else
                    {
                        pageIndex++;
                    }
                }
            }
            else
            {
                console.Error($"定时获取最新笑话数据储存：error_code ={jokeResult.error_code}，reason ={jokeResult.reason}");
            }
            console.Info($"执行{TimerName}定时任务结束", isWriteLog: false);
        }
    }
}
