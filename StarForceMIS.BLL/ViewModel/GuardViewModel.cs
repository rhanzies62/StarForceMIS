using StarForceMIS.BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarForceMIS.Web.Models
{
    public class GuardViewModel
    {
        public GuardViewModel() { }
        public GuardViewModel(Guard entity) 
        {
            this.ID = entity.ID;
            this.FirstName = entity.FirstName;
            this.MiddleName = entity.MiddleName;
            this.LastName = entity.LastName;
            this.CallSign = entity.CallSign;
            this.LicensedNumber = entity.LicesnedNumber;
            this.ExpiredDate = entity.DateExpiry;
            this.Designation = entity.Position;
            this.IsReliever = entity.IsReliver;

        }

        public long ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Call Sign")]
        public string CallSign { get; set; }

        [Display(Name = "Licensed #")]
        public string LicensedNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpiredDate { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Is reliever")]
        public bool IsReliever { get; set; }
    }
}