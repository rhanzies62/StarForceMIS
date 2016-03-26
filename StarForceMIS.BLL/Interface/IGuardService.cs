using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarForceMIS.Web.Models;

namespace StarForceMIS.BLL.Interface
{
    public interface IGuardService
    {
        Callback<GuardViewModel> AddNewGuard(GuardViewModel model);
        GuardViewModel GetGuardByID(long id);
        List<GuardViewModel> RetrieveGuards();
        List<GuardViewModel> RetrieveGuardAttendance(long scheduleID);
        List<GuardViewModel> SearchGuard(string queryString);
        Callback<GuardViewModel> UpdateGuard(GuardViewModel model);
        Callback<GuardViewModel> RemoveGuard(int id);
        Callback<GuardViewModel> ScheduleGuard(GuardViewModel model);
        List<ScheduleDetailViewModel> RetrieveScheduleGuard(long tourOfDuty, long scheduleID);
    }
}
