using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Interface
{
    public interface IDayOffService
    {
        Callback<ScheduleDayOffViewModel> SetGuardDayOff(ScheduleDayOffViewModel model);
        ScheduleDayOffViewModel RetrieveDayOffData(DateTime scheduledDate);
        List<DayOffViewModel> RetrieveDayoffs();
    }
}
