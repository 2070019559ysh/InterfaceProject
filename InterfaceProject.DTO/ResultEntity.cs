using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InterfaceProject.DTO
{
    /// <summary>
    /// 封装返回结果的实体类
    /// </summary>
    /// <typeparam name="T">最终数据所属类型</typeparam>
    public class ResultEntity<T>
    {
        /// <summary>
        /// 结果代号
        /// </summary>
        public ResultCode code;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg;
        /// <summary>
        /// 具体返回数据
        /// </summary>
        public T resultData;

        /// <summary>
        /// 实例化结果实体，指定结果代号
        /// </summary>
        /// <param name="code">结果代号</param>
        public ResultEntity(ResultCode code)
        {
            this.code = code;
            this.msg = ResultHandle.GetMsg(code);
            this.resultData = default(T);
        }
        /// <summary>
        /// 实例化结果实体，指定结果代号、消息内容
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="msg">消息内容</param>
        public ResultEntity(ResultCode code,string msg)
        {
            this.code = code;
            this.msg = string.IsNullOrWhiteSpace(msg) ? ResultHandle.GetMsg(code) : msg;
            this.resultData = default(T);
        }
        /// <summary>
        /// 实例化结果实体，指定结果代号、最终数据
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="resultData">最终数据</param>
        public ResultEntity(ResultCode code, T resultData)
        {
            this.code = code;
            this.msg = ResultHandle.GetMsg(code);
            this.resultData = resultData;
        }
        /// <summary>
        /// 实例化结果实体，指定结果代号、消息内容、最终数据
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="msg">消息内容</param>
        /// <param name="resultData">最终数据</param>
        public ResultEntity(ResultCode code, string msg,T resultData)
        {
            this.code = code;
            this.msg = string.IsNullOrWhiteSpace(msg) ? ResultHandle.GetMsg(code) : msg;
            this.resultData = resultData;
        }
    }

    /// <summary>
    /// 封装返回结果的实体类
    /// </summary>
    public class ResultEntity
    {
        /// <summary>
        /// 结果代号
        /// </summary>
        public ResultCode code;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg;

        /// <summary>
        /// 实例化结果实体，指定结果代号
        /// </summary>
        /// <param name="code">结果代号</param>
        public ResultEntity(ResultCode code)
        {
            this.code = code;
            this.msg = ResultHandle.GetMsg(code);
        }
        /// <summary>
        /// 实例化结果实体，指定结果代号、消息内容
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="msg">消息内容</param>
        public ResultEntity(ResultCode code, string msg)
        {
            this.code = code;
            this.msg = string.IsNullOrWhiteSpace(msg) ? ResultHandle.GetMsg(code) : msg;
        }
    }

    /// <summary>
    /// 微信接口结果消息内部实现单例模式
    /// </summary>
    internal class ResultHandle
    {
        /// <summary>
        /// 记录错误代码枚举的字段信息的集合
        /// </summary>
        private static List<FieldDesc<int>> fieldDescList;

        static ResultHandle()
        {
            Type wechatErrCodeType = typeof(ResultCode);
            foreach (var errCodeField in wechatErrCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var explainAttribute = (ExplainAttribute)(errCodeField.GetCustomAttributes(false).FirstOrDefault(x => x is ExplainAttribute));
                if (explainAttribute != null)
                {
                    if (fieldDescList == null) fieldDescList = new List<FieldDesc<int>>();
                    FieldDesc<int> fieldDesc = new FieldDesc<int>();
                    fieldDesc.FieldName = errCodeField.Name;
                    fieldDesc.FieldValue = Convert.ToInt32(errCodeField.GetValue(null));
                    fieldDesc.ExplainContent = explainAttribute.Content;
                    fieldDescList.Add(fieldDesc);//添加一个记录了说明内容的字段信息
                }
            }
        }

        /// <summary>
        /// 获取结果代号对应的中文消息
        /// </summary>
        /// <param name="errorCode">结果错误代号</param>
        /// <returns>结果代号对应的中文消息</returns>
        public static string GetMsg(ResultCode resultCode)
        {
            int errCode = (int)resultCode;
            FieldDesc<int> errCodeFieldDesc = fieldDescList.Where(fieldDesc => fieldDesc.FieldValue == errCode).FirstOrDefault();
            return errCodeFieldDesc.ExplainContent;
        }
    }

    /// <summary>
    /// 结果代号枚举
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 处理服务出错
        /// </summary>
        [Explain("处理服务出错")]
        ServerError = -2,
        /// <summary>
        /// 系统繁忙，此时请开发者稍候再试
        /// </summary>
        [Explain("系统繁忙，此时请开发者稍候再试")]
        SystemBusy = -1,
        /// <summary>
        /// 处理失败
        /// </summary>
        [Explain("处理失败")]
        Failure =0,
        /// <summary>
        /// 请求成功
        /// </summary>
        [Explain("请求成功")]
        SUCCESS = 1,
        /// <summary>
        /// 参数不能为空
        /// </summary>
        [Explain("参数不能为空")]
        ParamEmpty,
        /// <summary>
        /// 参数格式错误
        /// </summary>
        [Explain("参数格式错误")]
        ParamWrong,
        /// <summary>
        /// 远程请求失败
        /// </summary>
        [Explain("远程请求失败")]
        RemoteRequestFail,
        /// <summary>
        /// 微信公众号的AppId是必须的
        /// </summary>
        [Explain("微信公众号的AppId是必须的")]
        WeChatAppIdRequire,
        /// <summary>
        /// 微信公众号配置接入只能一个个进行
        /// </summary>
        [Explain("微信公众号配置接入只能一个个进行")]
        WeChatReadyOnlyOne,
        /// <summary>
        /// 微信签名验证不通过
        /// </summary>
        [Explain("微信签名验证不通过")]
        WeChatSignatureInValid,
    }
}
