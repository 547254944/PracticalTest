using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalTest.Models
{
    public class Stu
    {
        public int ID{ get; set; }

        [Required]
        [RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Please input correct NRIC number")]
        public string NRIC { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AvailableDate { get; set; }

        public bool English { get; set; }
        public bool Math { get; set; }
        public bool Science { get; set; }

        public int Age { get; set; }
        public int Number { get; set; }
    }

    public enum Gender {
        Female,
        Male
    }

    public enum Option
    {
        NRIC,
        Name
    }
}