using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarForceMIS.Web.Models
{
    public class GuardViewModel
    {
        public int ID { get; set; }

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

        [Required]
        [Display(Name = "Licensed Number")]
        public string LicensedNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime ExpiredDate { get; set; }
    }
}