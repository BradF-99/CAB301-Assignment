using System;
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
        public string PropertyNumber { get; set; } // string due to property numbers such as 19-21
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public State State { get; set; }

        public int Postcode { get; set; }

        public Address (int unitNumber, string propertyNumber, string streetName, string suburb, State state, int postcode)
        {
            this.UnitNumber = unitNumber;
            this.PropertyNumber = propertyNumber;
            this.StreetName = streetName;
            this.Suburb = suburb;
            this.State = state;
            this.Postcode = postcode;
        }

        public Address (string propertyNumber, string streetName, string suburb, State state, int postcode) // overload for no unit number
        {
            this.PropertyNumber = propertyNumber;
            this.StreetName = streetName;
            this.Suburb = suburb;
            this.State = state;
            this.Postcode = postcode;
        }
    }

    class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; } // so as to not remove the 0 at the start of area code
        public List<Movie> RentedMovies { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; } // maybe hash if i can be bothered

        public Member (string firstName, string lastName, Address address, string phoneNumber, int password)
        {
            this.FirstName = firstName;
            this.Address = address;
            this.PhoneNumber = phoneNumber;

            this.UserName = lastName + firstName;
            this.Password = password;
        }
    }
}
