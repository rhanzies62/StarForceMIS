using StarForceMIS.BLL.Interface;
using StarForceMIS.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        public void MarkAsPresent(long guardID)
        {
            using (var db = new StarforceDBEntities())
            {
                var newAttendance = new Attendance()
                {
                    GuardID = guardID,
                    CreatedDate = DateTime.UtcNow
                };

                db.Attendances.Add(newAttendance);
                db.SaveChanges();
            }
        }
    }
}
