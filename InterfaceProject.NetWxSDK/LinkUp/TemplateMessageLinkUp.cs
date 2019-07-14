using InterfaceProject.NetTool;
using InterfaceProject.NetTool.Log;
using InterfaceProject.NetWxSDK.HelpModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace InterfaceProject.NetWxSDK.LinkUp
{
    /// <summary>
    /// 对接微信模板消息
    /// </summary>
    public class TemplateMessageLinkUp
    {
        /// <summary>
        /// 依赖注入对接微信对象
        /// </summary>
        private IConnectLinkUp connect;

        /// <summary>
        /// 实例化微信模板消息处理实例，后期需Initialize初始化
        /// </summary>
        /// <param name="connectWeChat"></param>
        public TemplateMessageLinkUp(IConnectLinkUp connectWeChat)
        {
            connect = connectWeChat;
        }

        /// <summary>
        /// 初始化并设置微信公众号
        /// </summary>
        /// <param name="idOrAppId">公众号的Id(itfc...)或微信的AppId(wx...)</param>
        /// <returns>返回微信接入实例本身</returns>
        public TemplateMessageLinkUp Initialize(string idOrAppId)
        {
            connect.Initialize(idOrAppId);
            return this;
        }

        /// <summary>
        /// 设置所属行业，每月可修改行业1次
        /// </summary>
        /// <param name="industry1">公众号模板消息所属行业编号</param>
        /// <param name="industry2">公众号模板消息所属行业编号</param>
        /// <returns></returns>
        public WeChatResult SetIndustry(WeChatIndustry industry1, WeChatIndustry industry2)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token=" + accessToken;//POST
            string postData = JsonConvert.SerializeObject(new { industry_id1 = industry1, industry_id2 = industry2 });
            string resultStr = SimulateRequest.HttpPost(url, postData);
            WeChatResult weChatResult = new WeChatResult(resultStr);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"设置所属行业SetIndustry，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <returns>公众号设置的行业信息</returns>
        public WeChatResult<IndustryQuery> GetIndustry()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/template/get_industry?access_token=" + accessToken;
            WeChatResult<IndustryQuery> weChatResult = SimulateRequest.HttpGet<WeChatResult<IndustryQuery>>(url);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取设置的行业信息GetIndustry，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 给公众号添加消息模板，获得模板ID
        /// </summary>
        /// <param name="templateShortId">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <returns>公众号获得的模板ID</returns>
        public WeChatResult<TemplateID> AddTemplate(string templateShortId)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + accessToken;
            string resultData = SimulateRequest.HttpPost(url, new
            {
                template_id_short = templateShortId
            });
            WeChatResult<TemplateID> weChatResult = new WeChatResult<TemplateID>(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取设置的行业信息GetIndustry，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 获取公众号已添加的模板列表
        /// </summary>
        /// <returns>公众号获得的模板列表</returns>
        public WeChatResult<TemplateListInfo> SearchTemplate()
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token=" + accessToken;
            WeChatResult<TemplateListInfo> weChatResult = SimulateRequest.HttpGet<WeChatResult<TemplateListInfo>>(url);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"获取公众号已添加的模板列表SearchTemplate，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 删除公众号指定的模板
        /// </summary>
        /// <param name="templateId">公众帐号下模板消息ID</param>
        /// <returns>删除模板结果</returns>
        public WeChatResult DeleteTemplate(string templateId)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + accessToken;
            string resultData = SimulateRequest.HttpPost(url, new
            {
                template_id = templateId
            });
            WeChatResult weChatResult = new WeChatResult(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"删除公众号指定的模板DeleteTemplate，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="templateMessage">发送模板消息的参数</param>
        /// <returns>发送模板消息获得的消息ID</returns>
        public WeChatResult<Msg_ID> SendTemplate(TemplateMessageParam templateMessage)
        {
            string accessToken = connect.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
             string resultData = SimulateRequest.HttpPost(url, templateMessage);
            WeChatResult<Msg_ID> weChatResult = new WeChatResult<Msg_ID>(resultData);
            if (weChatResult.errcode != WeChatErrorCode.SUCCESS)
            {
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), $"发送模板消息SendTemplate，微信服务报错：{weChatResult}");
            }
            return weChatResult;
        }
    }
}
