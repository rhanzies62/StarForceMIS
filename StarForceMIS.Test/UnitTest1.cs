using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarForceMIS.BLL.Services;

namespace StarForceMIS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckAttendance()
        {
            var guardService = new GuardService();
            var attendance = guardService.RetrieveGuardAttendance(1);
            Assert.AreNotEqual(0, attendance.Count);
        }

        [TestMethod]
        public void CheckDayoffList()
        {
            var dayOffService = new DayOffScheduleService();
            var model = dayOffService.RetrieveDayOffData();
            Assert.AreNotEqual(0, model.RelieverList);
            Assert.AreNotEqual(0, model.GuardList);
        }
    }
}
