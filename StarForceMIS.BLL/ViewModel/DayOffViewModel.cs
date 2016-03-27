using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.Web.Models
{
    public class DayOffViewModel
    {
        public GuardViewModel DayOffGuard { get; set; }
        public GuardViewModel RelieverGuard { get; set; }
        public DateTime ScheduledDayOff { get; set; }
    }

    public class ScheduleDayOffViewModel
    {
        [Display(Name = "Guard")]
        public long GuardID { get; set; }
        public List<GuardViewModel> GuardList { get; set; }

        [Display(Name = "Relievers")]
        public long RelieverID { get; set; }
        public List<GuardViewModel> RelieverList { get; set; }
        

        public DateTime DayOffDate { get; set; }
        
    }
}
