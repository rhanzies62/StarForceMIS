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
    
    public partial class ScheduleLookUp
    {
        public ScheduleLookUp()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public long ID { get; set; }
        public string Title { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
    
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
