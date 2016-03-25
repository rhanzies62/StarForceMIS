using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarForceMIS.Web.Models
{
    public class ScheduleViewModel
    {
        [Display(Name = "Search Guard Here:")]
        public string QueryString { get; set; }
        public List<GuardViewModel> GuardDetails { get; set; }
        public GuardScheduleViewModel Schedules { get; set; }
        [Display(Name = "Schedule")]
        public long ScheduleID { get; set; }
        public List<MonthlySchedule> ScheduleLookUp { get; set; }
    }
}