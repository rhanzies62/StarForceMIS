using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Interface
{
    public interface IAttendanceService
    {
        void MarkAsPresent(long guardID);
    }
}
