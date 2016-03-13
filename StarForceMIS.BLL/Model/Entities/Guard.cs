using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarForceMIS.BLL.Model
{
    public partial class Guard
    {
        public Guard(GuardViewModel model)
        {
            this.FirstName = model.FirstName;
            this.MiddleName = model.MiddleName;
            this.LastName = model.LastName;
            this.CallSign = model.CallSign;
            this.LicesnedNumber = model.LicensedNumber;
            this.DateExpiry = model.ExpiredDate;
            this.Position = model.Designation;
            this.CreatedBy = 1;
            this.CreatedDate = DateTime.UtcNow;
            this.IsActive = true;
            this.IsReliver = model.IsReliever;
        }
    }
}
