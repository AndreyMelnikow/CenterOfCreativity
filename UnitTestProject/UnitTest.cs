using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethodPositive()
        {
            int idUser = 1;
            int idGroup = 1;
            int idEvent = 1;
            int idAuditory = 1;
            string date = "11.04.2022";
            string startEndTime = "09:00 - 10:00";

            bool result = CheckData.ValidateData(
                idUser, idGroup, idEvent, idAuditory, date, startEndTime);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestMethodNegative()
        {
            int idUser = 0;
            int idGroup = -1;
            int idEvent = 0;
            int idAuditory = -5;
            string date = "sdfs";
            string startEndTime = null;

            bool result = CheckData.ValidateData(
                idUser, idGroup, idEvent, idAuditory, date, startEndTime);

            Assert.AreEqual(result, false);
        }
    }
}
