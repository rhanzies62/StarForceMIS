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
    
    public partial class Schedule
    {
        public long GuardID { get; set; }
        public long PositionID { get; set; }
        public System.DateTime DesignationDate { get; set; }
        public string TourOfDuty { get; set; }
    
        public virtual Guard Guard { get; set; }
    }
}