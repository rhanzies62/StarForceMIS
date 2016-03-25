using StarForceMIS.BLL.Interface;
using StarForceMIS.BLL.Model;
using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Services
{
    public class MonthlyScheduleService : IMonthlyScheduleService
    {
        public List<MonthlySchedule> GetMonthlySchedule()
        {
            using (var db = new StarforceDBEntities())
            {
                var list = db.ScheduleLookUps.Select(i => new MonthlySchedule
                {
                    ID = i.ID,
                    From = i.DateFrom,
                    Title = i.Title,
                    To = i.DateTo
                }).ToList();

                return list;
            }
        }


        public long RetrieveCurrentSchedule()
        {
            var scheduleLookUp = GetMonthlySchedule();
            long todayTicks = DateTime.UtcNow.Ticks;
            long scheduleID = 1;
            foreach (var i in scheduleLookUp)
            {
                if (todayTicks >= i.From.Ticks && todayTicks <= i.To.Ticks)
                {
                    scheduleID = i.ID;
                }
            }
            return scheduleID;
        }
    }
}
