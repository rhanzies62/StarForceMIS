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
    public class DayOffScheduleService : IDayOffService
    {
        public Callback<ScheduleDayOffViewModel> SetGuardDayOff(ScheduleDayOffViewModel model)
        {
            using (var db = new StarforceDBEntities())
            {
                var result = new Callback<ScheduleDayOffViewModel>();


                var span = model.DayOffDate - DateTime.UtcNow;

                if (span.Days < 0)
                {
                    result.FailResult("Date is invalid");
                    return result;
                }

                //check guard id should not equal to reliever id
                if (model.RelieverID.Equals(model.GuardID))
                {
                    result.FailResult("Off duty guard cannot be schedule with the same guard");
                    return result;
                }

                //check if guard already set day off schedule on this day
                var checkGuard = db.DayOffSchedules.Where(i => i.GuardID.Equals(model.GuardID) &&
                     i.DayOffDate.Month.Equals(DateTime.UtcNow.Month) &&
                     i.DayOffDate.Day.Equals(DateTime.UtcNow.Day) &&
                     i.DayOffDate.Year.Equals(DateTime.UtcNow.Month));

                if (checkGuard.Any())
                {
                    result.FailResult("This guard is already schedule in the said date");
                    return result;
                }

                //check if reliever is already set on this date
                var checkReliver = db.DayOffSchedules.Where(i => i.RelieverID.Equals(model.RelieverID) &&
                     i.DayOffDate.Month.Equals(DateTime.UtcNow.Month) &&
                     i.DayOffDate.Day.Equals(DateTime.UtcNow.Day) &&
                     i.DayOffDate.Year.Equals(DateTime.UtcNow.Month));

                if (checkReliver.Any())
                {
                    result.FailResult("This reliever is already set on the said date");
                    return result;
                }

                if (!result.Success)
                {
                    var dayoffentity = new DayOffSchedule();
                    dayoffentity.GuardID = model.GuardID;
                    dayoffentity.RelieverID = model.RelieverID;
                    dayoffentity.DayOffDate = model.DayOffDate;
                    db.DayOffSchedules.Add(dayoffentity);
                    db.SaveChanges();
                    result.SuccessResult("Done");
                }

                return result;
            }
        }

        public ScheduleDayOffViewModel RetrieveDayOffData(DateTime scheduledDate)
        {
            using (var db = new StarforceDBEntities())
            {
                var model = new ScheduleDayOffViewModel();

                //retrieve list of guards scheduled today
                var dayOffs = (from item in db.DayOffSchedules
                               where item.DayOffDate.Month.Equals(scheduledDate.Month) &&
                               item.DayOffDate.Day.Equals(scheduledDate.Day) &&
                               item.DayOffDate.Year.Equals(scheduledDate.Year)
                               select item);

                var guards = (from item in db.Guards
                              select item);
                //retrieve list of guards thats not yet scheduled for day off for this day
                model.GuardList = (from guard in guards
                                   join dayoff in dayOffs on guard.ID equals dayoff.GuardID into g
                                   join reliever in dayOffs on guard.ID equals reliever.RelieverID into p
                                   where !g.Any() && !p.Any()
                                   select new GuardViewModel
                                   {
                                       ID = guard.ID,
                                       FirstName = guard.FirstName,
                                       LastName = guard.LastName
                                   }).ToList();

                model.RelieverList = (from guard in guards
                                      join dayoff in dayOffs on guard.ID equals dayoff.RelieverID into g
                                      join reliever in dayOffs on guard.ID equals reliever.GuardID into p
                                      where !g.Any() && !p.Any()
                                      select new GuardViewModel
                                      {
                                          ID = guard.ID,
                                          FirstName = guard.FirstName,
                                          LastName = guard.LastName
                                      }).ToList();
                return model;
            }
        }

        public List<DayOffViewModel> RetrieveDayoffs()
        {
            using (var db = new StarforceDBEntities())
            {
                var list = (from item in db.DayOffSchedules
                            where item.DayOffDate.Month.Equals(DateTime.UtcNow.Month) &&
                            item.DayOffDate.Year.Equals(DateTime.UtcNow.Year)
                            select item).ToList();

                var _list = (from item in list
                             select new DayOffViewModel
                                              {
                                                  DayOffGuard = new GuardViewModel
                                                  {
                                                      ID = item.Guard.ID,
                                                      FirstName = item.Guard.FirstName,
                                                      LastName = item.Guard.LastName
                                                  },
                                                  RelieverGuard = new GuardViewModel
                                                  {
                                                      ID = item.Guard1.ID,
                                                      LastName = item.Guard1.LastName,
                                                      FirstName = item.Guard1.FirstName
                                                  },
                                                  ScheduledDayOff = item.DayOffDate
                                              }).ToList();

                return _list;
            }
        }
    }
}
