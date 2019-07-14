using InterfaceProject.Tool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterfaceProject.ToolTest
{
    [TestClass]
    public class TimeHelperTest
    {
        /// <summary>
        /// ����ʱ��תʱ���
        /// </summary>
        [TestMethod]
        public void GetTimeTest()
        {
            long timeStamp1 =TimeHelper.GetTime(DateTime.Now);
            long timeStamp2 = TimeHelper.GetTime(DateTime.Now,TimeStampType.Milliseconds);
            Assert.IsTrue(timeStamp1.ToString().Length >= 10);
            Assert.IsTrue(timeStamp2.ToString().Length >= 13);
        }

        /// <summary>
        /// ����ʱ���תʱ��
        /// </summary>
        [TestMethod]
        public void GetDateTimeTest()
        {
            DateTime dateTime1 = TimeHelper.GetDateTime(1528901251);
            DateTime dateTime2 = TimeHelper.GetDateTime(1528901251588, TimeStampType.Milliseconds);
            Assert.IsTrue(dateTime1 < dateTime2);
        }
    }
}
