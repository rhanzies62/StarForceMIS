using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarForceMIS.Web.Models
{
    public class GuardScheduleViewModel
    {
        public List<ScheduleDetailViewModel> DayShift { get; set; }
        public List<ScheduleDetailViewModel> NightShift { get; set; }
    }
}