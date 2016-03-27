using StarForceMIS.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarForceMIS.Web.Models;
using StarForceMIS.BLL.Model;

namespace StarForceMIS.BLL.Services
{
    public class GuardService : IGuardService
    {
        public Callback<GuardViewModel> AddNewGuard(GuardViewModel model)
        {
            using (var db = new StarforceDBEntities())
            {
                var result = new Callback<GuardViewModel>();
                try
                {
                    var entity = new Guard(model);
                    db.Guards.Add(entity);
                    db.SaveChanges();
                    model.ID = entity.ID;
                    result.Data = model;
                    result.SuccessResult(string.Empty);
                    return result;
                }
                catch (Exception e)
                {
                    result.FailResult(e.Message);
                    return result;
                }
            }
        }

        public List<GuardViewModel> RetrieveGuards()
        {
            using (var db = new StarforceDBEntities())
            {
                var entities = db.Guards.Select(entity => new GuardViewModel
                {
                    ID = entity.ID
                    ,
                    FirstName = entity.FirstName
                    ,
                    MiddleName = entity.MiddleName
                    ,
                    LastName = entity.LastName
                    ,
                    CallSign = entity.CallSign
                    ,
                    LicensedNumber = entity.LicesnedNumber
                    ,
                    ExpiredDate = entity.DateExpiry
                    ,
                    Designation = entity.Position
                    ,
                    IsReliever = entity.IsReliver
                }).ToList();
                return entities;
            }
        }

        public List<GuardViewModel> SearchGuard(string queryString)
        {
            using (var db = new StarforceDBEntities())
            {
                var entities = db.Guards
                        .Where(i => i.FirstName.Contains(queryString) || i.LastName.Contains(queryString))
                        .Select(entity => new GuardViewModel
                        {
                            ID = entity.ID,
                            FirstName = entity.FirstName,
                            MiddleName = entity.MiddleName,
                            LastName = entity.LastName,
                            CallSign = entity.CallSign,
                            LicensedNumber = entity.LicesnedNumber,
                            ExpiredDate = entity.DateExpiry,
                            Designation = entity.Position,
                            IsReliever = entity.IsReliver
                        }).ToList();

                return entities;
            }
        }

        public Callback<GuardViewModel> UpdateGuard(GuardViewModel model)
        {
            using (var db = new StarforceDBEntities())
            {
                var result = new Callback<GuardViewModel>();
                try
                {
                    var entity = db.Guards.Where(i => i.ID.Equals(model.ID)).FirstOrDefault();
                    entity.FirstName = model.FirstName;
                    entity.MiddleName = model.MiddleName;
                    entity.LastName = model.LastName;
                    entity.CallSign = model.CallSign;
                    entity.LicesnedNumber = model.LicensedNumber;
                    entity.DateExpiry = model.ExpiredDate;
                    entity.Position = model.Designation;
                    entity.IsReliver = model.IsReliever;
                    entity.UpdatedBy = 1;
                    entity.UpdatedDate = DateTime.UtcNow;
                    db.SaveChanges();
                    result.Data = model;
                    result.SuccessResult(string.Empty);
                }
                catch (Exception e)
                {
                    result.FailResult(e.Message);
                }

                return result;

            }
        }

        public Callback<GuardViewModel> RemoveGuard(int id)
        {
            throw new NotImplementedException();
        }

        public GuardViewModel GetGuardByID(long id)
        {
            using (var db = new StarforceDBEntities())
            {
                GuardViewModel model = new GuardViewModel();
                var entity = db.Guards.Where(i => i.ID.Equals(id)).FirstOrDefault();
                if (entity != null)
                {
                    model = new GuardViewModel(entity);
                    model.ScheduleLookUp = db.ScheduleLookUps.Select(i => new MonthlySchedule
                    {
                        ID = i.ID,
                        From = i.DateFrom,
                        Title = i.Title,
                        To = i.DateTo
                    }).ToList();
                    model.TourOfDutyLookUp = db.TourOfDutyLookUps.Select(i => new StarForceMIS.Web.Models.TourOfDutyLookUp
                    {
                        ID = i.ID,
                        TourOfDuty = i.TourOfDuty
                    }).ToList();
                    model.PositionLookUp = db.Positions.Select(i => new PositionLookUp
                    {
                        ID = i.ID,
                        Title = i.Title
                    }).ToList();
                }
                return model;

            }
        }

        public Callback<GuardViewModel> ScheduleGuard(GuardViewModel model)
        {
            var result = new Callback<GuardViewModel>();
            using (var db = new StarforceDBEntities())
            {
                var verifySched = db.Schedules.Where(i => i.PositionID.Equals(model.PositionID)
                                && i.ScheduleID.Equals(model.ScheduleID)
                                && i.TourOfDutyID.Equals(model.TourOfDutyID));
                try
                {
                    var getUserSched = db.Schedules.Where(i => i.GuardID.Equals(model.ID) && i.ScheduleID.Equals(model.ScheduleID));
                    if (!getUserSched.Any())
                    {
                        if (!verifySched.Any())
                        {
                            var scheduleEntity = new Schedule()
                            {
                                GuardID = model.ID,
                                PositionID = model.PositionID,
                                ScheduleID = model.ScheduleID,
                                TourOfDutyID = model.TourOfDutyID
                            };

                            db.Schedules.Add(scheduleEntity);
                            db.SaveChanges();
                            result.SuccessResult(string.Empty);
                        }
                        else
                        {
                            var guard = verifySched.FirstOrDefault();
                            result.FailResult(string.Format("{0} is already assigned in this sched", guard.Guard.CallSign));
                        }
                    }
                    else
                    {
                        result.FailResult("This guard is already schedule in this date but in other shift");
                    }

                }
                catch (Exception e)
                {
                    result.FailResult(string.Format("{0} is already assigned in this sched", model.CallSign));
                }
            }

            return result;

        }

        public List<ScheduleDetailViewModel> RetrieveScheduleGuard(long tourOfDuty, long scheduleID)
        {
            using (var db = new StarforceDBEntities())
            {
                var schedules = db.Schedules
                    .Where(i => i.ScheduleID.Equals(scheduleID) && i.TourOfDutyID.Equals(tourOfDuty))
                    .Select(i => new ScheduleDetailViewModel
                    {
                        CallSign = i.Guard.CallSign,
                        GuardID = i.GuardID,
                        Name = i.Guard.FirstName + " " + i.Guard.LastName,
                        Position = i.Position.Title
                    }).ToList();

                return schedules;
            }
        }

        public List<GuardViewModel> RetrieveGuardAttendance(long scheduleID)
        {
            using (var db = new StarforceDBEntities())
            {
                var attendanceList = new List<GuardViewModel>();
                IQueryable<Guard> unCheckGuards, notDayOffGuards;


                //retrieve todays schedule
                var todaysSchedule = (from i in db.Schedules
                                      where i.ScheduleID.Equals(scheduleID)
                                      select i);

                var ts = todaysSchedule.Count();

                //retrieve todays attendance
                var todaysAttendnace = (from attendance in db.Attendances
                                        where attendance.CreatedDate.Month.Equals(DateTime.UtcNow.Month) &&
                                        attendance.CreatedDate.Day.Equals(DateTime.UtcNow.Day) &&
                                        attendance.CreatedDate.Year.Equals(DateTime.UtcNow.Year)
                                        select attendance);


                //retrieve todays day off
                var todaysDayOff = (from dayoff in db.DayOffSchedules
                                    where dayoff.DayOffDate.Month.Equals(DateTime.UtcNow.Month) &&
                                    dayoff.DayOffDate.Day.Equals(DateTime.UtcNow.Day) &&
                                    dayoff.DayOffDate.Year.Equals(DateTime.UtcNow.Year)
                                    select dayoff);

                //retrieve reliever
                var todaysRelievers = todaysDayOff.Select(i => new GuardViewModel()
                                    {
                                        ID = i.RelieverID,
                                        FirstName = i.Guard1.FirstName,
                                        LastName = i.Guard1.LastName,
                                        IsReliever = true
                                    }).ToList();


                //filter out schdules and remove guards thats already scanned
                unCheckGuards = (from schedule in todaysSchedule
                                 join attendace in todaysAttendnace on schedule.GuardID equals attendace.GuardID into g
                                 where !g.Any()
                                 select schedule.Guard);

                //ilter out schedules and remove off duty guards
                notDayOffGuards = (from schedule in todaysSchedule
                                   join dayoff in todaysDayOff on schedule.GuardID equals dayoff.GuardID into g
                                   where !g.Any()
                                   select schedule.Guard);


                if (unCheckGuards.Count() != 0 || notDayOffGuards.Count() != 0)
                {
                    //combine two list to retrieve the none scanned guards
                    attendanceList = (from i in unCheckGuards
                                      join ii in notDayOffGuards on i.ID equals ii.ID into g
                                      where g.Any()
                                      select new GuardViewModel
                                      {
                                          ID = i.ID,
                                          FirstName = i.FirstName,
                                          LastName = i.LastName
                                      }).ToList();
                    //insert all reliever after the final query
                    foreach(var reliever in todaysRelievers)
                    {
                        var exist = attendanceList.Where(i => i.ID.Equals(reliever.ID)).FirstOrDefault();
                        if (exist != null)
                            attendanceList.Remove(exist);
                    }
                    
                    attendanceList.AddRange(todaysRelievers);
                    
                }
                else
                {
                    //just retrieve all of the records scheduled today
                    attendanceList = (from i in todaysSchedule
                                      select new GuardViewModel
                                      {
                                          ID = i.GuardID,
                                          FirstName = i.Guard.FirstName,
                                          LastName = i.Guard.LastName
                                      }).ToList();
                }

                return attendanceList;
            }
        }
    }
}
