using InterfaceProject.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.DTO.Support
{
    /// <summary>
    /// Api请求接口的请求封装
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// 设备Mac地址
        /// </summary>
        public string mac;
        /// <summary>
        /// 请求参数
        /// </summary>
        public object request;
        /// <summary>
        /// 响应参数
        /// </summary>
        public object response;
        /// <summary>
        /// 接口所属类型（对应文档的序号）
        /// </summary>
        public string type;
        /// <summary>
        /// 请求发生的时间
        /// </summary>
        public DateTime createTime;

        /// <summary>
        /// 加解密密钥
        /// </summary>
        private string secretKey = "pznj#3hRmPG@qdMR2$qQZrrN";

        /// <summary>
        /// 获取符合指定类型的请求参数
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <returns>指定类型的请求参数</returns>
        public T GetRequest<T>()
        {
            if (string.IsNullOrWhiteSpace(request?.ToString())) return default(T);
            string requestData = string.Empty;
            try
            {
                requestData = EncryptHelper.TripleDESDecrypt(request.ToString(), secretKey);
            }
            catch (FormatException)
            {
                requestData = request.ToString();
            }
            try
            {
                //如果结果字符串符合json
                T dataResult = JsonConvert.DeserializeObject<T>(requestData);
                return dataResult;
            }
            catch (Exception)
            {
                //结果字符串符合不符合json，需要转具体的值类型，如：string, bool, int, decimal
                T dataResult = (T)Convert.ChangeType(requestData, typeof(T));
                return dataResult;
            }
        }

        /// <summary>
        /// 获取符合指定类型的响应参数
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <returns>指定类型的响应参数</returns>
        public T GetResponse<T>()
        {
            if (string.IsNullOrWhiteSpace(response?.ToString())) return default(T);
            string responseData = string.Empty;
            try
            {
                responseData = EncryptHelper.TripleDESDecrypt(response.ToString(), secretKey);
            }
            catch (FormatException)
            {
                responseData = response.ToString();
            }
            try
            {
                //如果结果字符串符合json
                T dataResult = JsonConvert.DeserializeObject<T>(responseData);
                return dataResult;
            }
            catch (Exception)
            {
                //结果字符串符合不符合json，需要转具体的值类型，如：string, bool, int, decimal
                T dataResult = (T)Convert.ChangeType(responseData, typeof(T));
                return dataResult;
            }
        }
    }
}
