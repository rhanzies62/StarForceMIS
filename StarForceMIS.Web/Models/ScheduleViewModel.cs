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
        public GuardViewModel GuardDetails { get; set; }
        public GuardScheduleViewModel Schedules { get; set; }
    }
}