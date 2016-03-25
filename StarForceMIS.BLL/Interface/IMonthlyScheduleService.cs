using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Interface
{
    public interface IMonthlyScheduleService
    {
        List<MonthlySchedule> GetMonthlySchedule();
        long RetrieveCurrentSchedule();
    }
}
