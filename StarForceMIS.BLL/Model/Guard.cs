//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarForceMIS.BLL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Guard
    {
        public Guard()
        {
            this.Attendances = new HashSet<Attendance>();
            this.DayOffSchedules = new HashSet<DayOffSchedule>();
            this.GuardPositions = new HashSet<GuardPosition>();
            this.Schedules = new HashSet<Schedule>();
        }
    
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CallSign { get; set; }
        public string LicesnedNumber { get; set; }
        public Nullable<System.DateTime> DateExpiry { get; set; }
        public string Position { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsReliver { get; set; }
    
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<DayOffSchedule> DayOffSchedules { get; set; }
        public virtual ICollection<GuardPosition> GuardPositions { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
