using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using InterfaceProject.BLL.Handle;
using InterfaceProject.DTO;
using InterfaceProject.Tool.Cache;
using InterfaceProject.Tool.HelpModel;
using InterfaceProject.Tool.Log;
using InterfaceProject.WxSDK.HelpModel;
using InterfaceProject.WxSDK.LinkUp;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceProject.Web.Controllers
{
    /// <summary>
    /// 对接微信服务器{controller=we}_{action=chat}/{id?}
    /// </summary>
    public class WeController : Controller
    {
        //private RedisHelper redisHelper = new RedisHelper();
        private readonly IConnectLinkUp connectLinkUp;
        private readonly IWeChatHandle weChatHandle;

        public WeController(IConnectLinkUp connectLinkUp, IWeChatHandle weChatHandle)
        {
            this.connectLinkUp = connectLinkUp;
            this.weChatHandle = weChatHandle;
        }

        /// <summary>
        /// 如果验证是匹配的微信公众号信息返回nonce原始字符串
        /// </summary>
        /// <param name="id">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>nonce原始字符串</returns>
        [HttpGet]
        [ActionName("Chat")]
        public IActionResult Index(string id)
        {
            string signature = Request.Query["signature"];
            string timestamp = Request.Query["timestamp"];
            string nonce = Request.Query["nonce"];
            SystemLogHelper.Info(GetType().FullName, $"微信公众号接入参数：signature={signature},timestamp={timestamp},nonce={nonce}");
            //RedisHelper redisHelper = new RedisHelper();
            //string readyAppId = redisHelper.StringGet(RedisKeyPrefix.WECHAT_CONNECT_READY);
            connectLinkUp.Initialize(id);//确定公众号
            if (connectLinkUp.CheckSignature(signature, timestamp, nonce))
            {
                SystemLogHelper.Info(GetType().FullName, $"匹配微信公众号AppId：{id}");
                return Content(Request.Query["echostr"]);
            }
            return Json(new ResultEntity<object>(ResultCode.SUCCESS, new { signature, timestamp, nonce }));
        }

        /// <summary>
        /// 接收处理微信服务器推送过来的消息
        /// </summary>
        /// <param name="id">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>响应微信服务器</returns>
        [HttpPost]
        public IActionResult Chat(string id)
        {
            string signature = Request.Query["signature"];
            string timestamp = Request.Query["timestamp"];
            string nonce = Request.Query["nonce"];
            connectLinkUp.Initialize(id);//确定公众号
            if (connectLinkUp.CheckSignature(signature, timestamp, nonce))
            {
                weChatHandle.Initialize(id);
                string responseXml = weChatHandle.Execute(signature, timestamp, nonce, Request.Body);
                return Content(responseXml);
            }
            return Json(new ResultEntity<object>(ResultCode.SUCCESS));
        }

        /// <summary>
        /// 请求告知系统某公众号就绪接入
        /// </summary>
        /// <param name="id">公众号AppId</param>
        /// <returns>就绪接入结果</returns>
        //public IActionResult WeChatReady(string id)
        //{
        //    if(string.IsNullOrWhiteSpace(id)) return Json(new ResultEntity<string>(ResultCode.WeChatAppIdRequire));
        //    string readyAppId = redisHelper.StringGet(RedisKeyPrefix.WECHAT_CONNECT_READY);
        //    if (string.IsNullOrWhiteSpace(readyAppId))
        //    {
        //        ResultEntity<string> concurrentResult = new ResultEntity<string>(
        //            ResultCode.SystemBusy, "只允许单个公众号做接入就绪，请重试");
        //        ResultEntity<string> finalResult = ConcurrentControl.SingleUserFunc(RedisKeyPrefix.WECHAT_READY_CONCURRENT, concurrentResult, () =>
        //         {
        //             redisHelper.StringSet(RedisKeyPrefix.WECHAT_CONNECT_READY, id, TimeSpan.FromMinutes(1));
        //             return new ResultEntity<string>(
        //                 ResultCode.SUCCESS, "公众号接入就绪，请在1分钟内完成配置接入", $"微信公众号AppId：{id}");
        //         });
        //        return Json(finalResult);
        //    }
        //    else
        //    {
        //        return Json(new ResultEntity<string>(ResultCode.WeChatReadyOnlyOne));
        //    }
        //}
    }
}