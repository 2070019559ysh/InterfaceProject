using InterfaceProject.Tool.HelpModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// 模拟请求并获取响应出来对象
    /// </summary>
    public class SimulateRequest
    {
        /// <summary>
        /// 发送Post请求并返回响应字符串
        /// </summary>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/ </param>
        /// <param name="postDataStr">请求参数，如：key=value&key2=value2 或{key:value,key2:value2}</param>
        /// <param name="contentType">请求参数内容类型，可以设为application/json</param>
        /// <returns>响应字符串</returns>
        public static string HttpPost(string url, string postDataStr, string contentType = "application/x-www-form-urlencoded")
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            byte[] dataBytes = Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = dataBytes.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(dataBytes, 0, dataBytes.Length);
            string result = GetResponse(request);
            myRequestStream.Dispose();
            return result;
        }

        /// <summary>
        /// 发送Post请求并返回响应字符串
        /// </summary>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/ </param>
        /// <param name="postDataStr">请求参数对象</param>
        /// <param name="contentType">请求参数内容类型，可以设为application/json</param>
        /// <returns>响应字符串</returns>
        public static string HttpPost(string url, object postObj, string contentType = "application/x-www-form-urlencoded")
        {
            string postDataStr = JsonConvert.SerializeObject(postObj);
            string resultStr = HttpPost(url, postDataStr, contentType);
            return resultStr;
        }

        /// <summary>
        /// 发送Post请求并返回响应结果对象，T必须有无参数构造函数
        /// </summary>
        /// <typeparam name="T">必须有无参数构造函数</typeparam>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/ </param>
        /// <param name="postObj">请求参数对象</param>
        /// <param name="contentType">请求参数内容类型，可以设为application/json</param>
        /// <returns>响应结果对象</returns>
        public static T HttpPost<T>(string url, object postObj, string contentType = "application/x-www-form-urlencoded")
        {
            string postDataStr = JsonConvert.SerializeObject(postObj);
            string resultStr = HttpPost(url, postDataStr, contentType);
            T result = JsonConvert.DeserializeObject<T>(resultStr);
            return result;
        }

        /// <summary>
        /// 发送Get请求并返回响应字符串
        /// </summary>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/Home?lg=zh-cn </param>
        /// <param name="isDecompress">是否需要解压，默认false</param>
        /// <returns>响应字符串</returns>
        public static string HttpGet(string url, bool isDecompress = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=utf-8";
            return GetResponse(request, isDecompress);
        }

        /// <summary>
        /// 发送Get请求并返回响应结果对象
        /// </summary>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/Home?lg=zh-cn </param>
        /// <param name="isDecompress">是否需要解压，默认false</param>
        /// <returns>响应结果对象</returns>
        public static T HttpGet<T>(string url, bool isDecompress = false)
        {
            string resultStr = HttpGet(url, isDecompress);
            T result = JsonConvert.DeserializeObject<T>(resultStr);
            return result;
        }

        /// <summary>
        /// 根据HttpWebRequest请求对象获取最终响应信息
        /// </summary>
        /// <param name="request">HttpWebRequest请求对象</param>
        /// <param name="isDecompress">是否需要解压，默认false</param>
        /// <returns>响应字符串信息</returns>
        private static string GetResponse(HttpWebRequest request, bool isDecompress = false)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader;
            if (isDecompress)
            {
                GZipStream deZipStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                myStreamReader = new StreamReader(deZipStream, Encoding.UTF8);
            }
            else
            {
                myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            }
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }

        /// <summary>
        /// 利用HttpClient进行GET请求
        /// </summary>
        /// <param name="url">请求Url，如：http://yshweb.wicp.net/Home?lg=zh-cn </param>
        /// <param name="isDecompress">是否需要解压，默认false</param>
        /// <returns>响应字符串</returns>
        public static string HttpClientGet(string url, bool isDecompress = false)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                client.DefaultRequestHeaders.Add("Timeout", 30.ToString());
                client.DefaultRequestHeaders.Add("KeepAlive", "true");

                Task<HttpResponseMessage> responseMsgTask = client.GetAsync(url);
                responseMsgTask.Wait();
                string responseMsg;
                if (isDecompress)
                {
                    Task<Stream> responseStream = responseMsgTask.Result.Content.ReadAsStreamAsync();
                    GZipStream deZipStream = new GZipStream(responseStream.Result, CompressionMode.Decompress);
                    StreamReader myStreamReader = new StreamReader(deZipStream, Encoding.UTF8);
                    responseMsg = myStreamReader.ReadToEnd();
                }
                else
                {
                    Task<byte[]> responseContent = responseMsgTask.Result.Content.ReadAsByteArrayAsync();
                    responseMsg = Encoding.UTF8.GetString(responseContent.Result);
                }
                return responseMsg;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="parameter">包含文件二进制数据的上传参数</param>
        /// <returns>上传后的响应结果</returns>
        public static string UploadFile(UploadFileParam parameter)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // 1.分界线
                string boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x")),       // 分界线可以自定义参数
                    beginBoundary = string.Format("--{0}\r\n", boundary),
                    endBoundary = string.Format("\r\n--{0}--\r\n", boundary);
                byte[] beginBoundaryBytes = parameter.Encoding.GetBytes(beginBoundary),
                    endBoundaryBytes = parameter.Encoding.GetBytes(endBoundary);
                // 2.组装开始分界线数据体 到内存流中
                memoryStream.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);
                // 3.组装 上传文件附加携带的参数 到内存流中
                if (parameter.PostParameters != null && parameter.PostParameters.Count > 0)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in parameter.PostParameters)
                    {
                        string parameterHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}", keyValuePair.Key, keyValuePair.Value, beginBoundary);
                        byte[] parameterHeaderBytes = parameter.Encoding.GetBytes(parameterHeaderTemplate);

                        memoryStream.Write(parameterHeaderBytes, 0, parameterHeaderBytes.Length);
                    }
                }
                // 4.组装文件头数据体 到内存流中
                string fileHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", parameter.FileNameKey, parameter.FileNameValue);
                byte[] fileHeaderBytes = parameter.Encoding.GetBytes(fileHeaderTemplate);
                memoryStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);
                // 5.组装文件流 到内存流中
                byte[] buffer = new byte[1024 * 1024 * 1];
                int size = parameter.FileStream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    memoryStream.Write(buffer, 0, size);
                    size = parameter.FileStream.Read(buffer, 0, buffer.Length);
                }
                // 6.组装结束分界线数据体 到内存流中
                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                // 7.获取二进制数据
                byte[] postBytes = memoryStream.ToArray();
                // 8.HttpWebRequest 组装
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(parameter.Url, UriKind.RelativeOrAbsolute));
                webRequest.Method = "POST";
                webRequest.Timeout = int.MaxValue;
                webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                webRequest.ContentLength = postBytes.Length;
                if (Regex.IsMatch(parameter.Url, "^https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                }
                // 9.写入上传请求数据
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                }
                // 10.获取响应
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), parameter.Encoding))
                    {
                        string body = reader.ReadToEnd();
                        reader.Close();
                        return body;
                    }
                }
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求下载地址</param>
        /// <param name="filePathName">下载保存的包含路径的文件名，注意以最终返回文件名为准</param>
        /// <returns>保存的包含路径的文件名</returns>
        public static string DownloadFile(string url,string filePathName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.ContentType.Equals("text/plain"))
                {
                    //下载多媒体文件失败
                    string address = response.ResponseUri.ToString();
                    WebClient mywebclient = new WebClient();
                    byte[] dataContent=mywebclient.DownloadData(address);
                    MemoryStream stream = new MemoryStream(dataContent);
                    StreamReader streamReader = new StreamReader(stream);
                    string resultData = streamReader.ReadToEnd();
                    streamReader.Close();
                    stream.Close();
                    throw new Exception(resultData);
                }
                else
                {
                    //下载多媒体文件成功
                    string contentDisposition = response.Headers.Get("Content-disposition");
                    int firstIndex = contentDisposition.IndexOf("filename=") + 9;
                    string downloadFileName = contentDisposition.Substring(firstIndex);
                    downloadFileName = downloadFileName.Trim('"');
                    string fileExtension = Path.GetExtension(downloadFileName);
                    string fileName = Path.GetFileNameWithoutExtension(filePathName);
                    string filePath = Path.GetDirectoryName(filePathName);
                    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                    //最终确定的文件名
                    if (Path.GetFileNameWithoutExtension(downloadFileName).Length > 10)
                        filePathName = filePath + "\\" + downloadFileName.Substring(0, 10) + fileExtension;
                    else
                        filePathName = filePath + "\\" + downloadFileName;
                    //先以下载下来的文件名命名；如果已存在，则以前面命名；如果还存在，最后以Guid自动生成命名
                    if (File.Exists(filePathName))
                    {
                        filePathName = filePath + "\\" + fileName + fileExtension;
                    }
                    if (File.Exists(filePathName))
                    {
                        filePathName = filePath + "\\" + Guid.NewGuid().ToString("N") + fileExtension;
                    }
                    string address = response.ResponseUri.ToString();
                    WebClient mywebclient = new WebClient();
                    mywebclient.DownloadFile(address, filePathName);
                    return filePathName;
                }
            }
        }

        /// <summary>
        /// 已POST请求方式下载文件
        /// </summary>
        /// <param name="url">请求下载地址</param>
        /// <param name="filePathName">下载保存的包含路径的文件名，注意以最终返回文件名为准</param>
        /// <param name="postObj">需要Post的数据</param>
        /// <param name="contentType">请求参数内容类型，可以设为application/json</param>
        /// <returns>保存的包含路径的文件名</returns>
        public static string DownloadFilePost(string url, string filePathName, object postObj, string contentType = "application/x-www-form-urlencoded")
        {
            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            string postDataStr = JsonConvert.SerializeObject(postObj);
            byte[] dataBytes = Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = dataBytes.Length;
            using (Stream myRequestStream = request.GetRequestStream())
            {
                myRequestStream.Write(dataBytes, 0, dataBytes.Length);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.ContentType.Equals("text/plain"))
                    {
                        //下载多媒体文件失败
                        string address = response.ResponseUri.ToString();
                        WebClient mywebclient = new WebClient();
                        byte[] dataContent = mywebclient.DownloadData(address);
                        MemoryStream stream = new MemoryStream(dataContent);
                        StreamReader streamReader = new StreamReader(stream);
                        string resultData = streamReader.ReadToEnd();
                        streamReader.Close();
                        stream.Close();
                        throw new Exception(resultData);
                    }
                    else
                    {
                        //下载多媒体文件成功
                        string contentDisposition = response.Headers.Get("Content-disposition");
                        int firstIndex = contentDisposition.IndexOf("filename=") + 9;
                        string downloadFileName = contentDisposition.Substring(firstIndex);
                        downloadFileName = downloadFileName.Trim('"');
                        string fileExtension = Path.GetExtension(downloadFileName);
                        string fileName = Path.GetFileNameWithoutExtension(filePathName);
                        string filePath = Path.GetDirectoryName(filePathName);
                        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                        //最终确定的文件名
                        if (Path.GetFileNameWithoutExtension(downloadFileName).Length > 10)
                            filePathName = filePath + "\\" + downloadFileName.Substring(0, 10) + fileExtension;
                        else
                            filePathName = filePath + "\\" + downloadFileName;
                        //先以下载下来的文件名命名；如果已存在，则以前面命名；如果还存在，最后以Guid自动生成命名
                        if (File.Exists(filePathName))
                        {
                            filePathName = filePath + "\\" + fileName + fileExtension;
                        }
                        if (File.Exists(filePathName))
                        {
                            filePathName = filePath + "\\" + Guid.NewGuid().ToString("N") + fileExtension;
                        }
                        string address = response.ResponseUri.ToString();
                        WebClient mywebclient = new WebClient();
                        mywebclient.DownloadFile(address, filePathName);
                        return filePathName;
                    }
                }
            }
        }


    }
}