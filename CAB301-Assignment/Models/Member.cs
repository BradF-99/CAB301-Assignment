using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace CAB301_Assignment.Models
{
    public enum State
    {
        ACT,
        NSW,
        NT,
        QLD,
        SA,
        TAS,
        VIC,
        WA
    }
    class Address
    {
        public int UnitNumber { get; set; }

        [Required(ErrorMessage = "Property number is required")]
        public string PropertyNumber { get; set; } // string due to property numbers such as 19-21

        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Suburb is required")]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "State is required")]
        public State State { get; set; }

        [Range(200, 9999, ErrorMessage = "Postcode must be between 0200 and 9999")]
        public int Postcode { get; set; }
    }

    class Member
    {

    }
}
