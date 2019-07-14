using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace InterfaceProject.Tool
{
    /// <summary>
    /// Xml的类型转换器
    /// </summary>
    public class XmlConvert
    {
        /// <summary>
        /// 序列化对象为Xml
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="value">对象的值</param>
        /// <returns>对象对应的Xml</returns>
        public static string SerializeObject<T>(object value)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            //序列化对象  
            xml.Serialize(Stream, value);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        /// <summary>
        /// 反序列化Xml为具体对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="xml">对象对应的Xml</param>
        /// <returns>对象的值</returns>
        public static T DeserializeObject<T>(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                object deserializeObj = xmldes.Deserialize(sr);
                string objStr = JsonConvert.SerializeObject(deserializeObj);
                T obj = JsonConvert.DeserializeObject<T>(objStr);
                return obj;
            }
        }

        /// <summary>
        /// 反序列化Xml为具体对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="xml">对象对应的Xml</param>
        /// <returns>对象的值</returns>
        public static T DeserializeObject<T>(Stream xmlStream)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            object deserializeObj = xmldes.Deserialize(xmlStream);
            string objStr = JsonConvert.SerializeObject(deserializeObj);
            T obj = JsonConvert.DeserializeObject<T>(objStr);
            return obj;
        }
    }
}
