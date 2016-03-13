using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarForceMIS.Web.Models
{
    public class ScheduleDetailViewModel
    {
        public int GuardID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
}