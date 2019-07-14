using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using InterfaceProject.DTO.Support;

namespace InterfaceProject.DTO.Support
{
    /// <summary>
    /// Api请求接口的响应封装
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 平台返回响应码
        /// </summary>
        public ResultCode code;
        /// <summary>
        /// 平台返回响应说明
        /// </summary>
        public string desc;

        /// <summary>
        /// 创建响应结果
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        /// <returns>响应结果</returns>
        public ApiResponse(ResultCode code, string desc = null)
        {
            this.code = code;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = ResultHandle.GetChineseMsg(code);
            }
            this.desc = desc;
        }
    }

    /// <summary>
    /// Api请求接口的响应封装
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 平台返回响应码
        /// </summary>
        public ResultCode code;
        /// <summary>
        /// 平台返回响应说明
        /// </summary>
        public string desc;
        /// <summary>
        /// 业务数据
        /// </summary>
        public T data;

        /// <summary>
        /// 创建响应结果
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        /// <returns>响应结果</returns>
        public ApiResponse(ResultCode code, string desc = null)
        {
            this.code = code;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = ResultHandle.GetChineseMsg(code);
            }
            this.desc = desc;
        }

        /// <summary>
        /// 创建响应结果，并返回业务数据
        /// </summary>
        /// <param name="code">结果代号</param>
        /// <param name="desc">结果说明，可选</param>
        /// <param name="data">指定返回业务数据</param>
        /// <returns>响应结果</returns>
        public ApiResponse(ResultCode code, string desc = null, T data=default(T))
        {
            this.code = code;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = ResultHandle.GetChineseMsg(code);
            }
            this.desc = desc;
            this.data = data;
        }
    }

    /// <summary>
    /// 结果消息内部实现单例模式
    /// </summary>
    internal class ResultHandle
    {
        /// <summary>
        /// 记录错误代码枚举的字段信息的集合
        /// </summary>
        private static List<FieldDesc<int>> fieldDescList;

        static ResultHandle()
        {
            Type resultCodeType = typeof(ResultCode);
            foreach (var errCodeField in resultCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
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
        /// <param name="errorCode">微信错误代号</param>
        /// <returns>错误代号对应的中文消息</returns>
        public static string GetChineseMsg(ResultCode errorCode)
        {
            int errCode = (int)errorCode;
            FieldDesc<int> errCodeFieldDesc = fieldDescList.Where(fieldDesc => fieldDesc.FieldValue == errCode).FirstOrDefault();
            return errCodeFieldDesc?.ExplainContent;
        }
    }
}
