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
    }
}
