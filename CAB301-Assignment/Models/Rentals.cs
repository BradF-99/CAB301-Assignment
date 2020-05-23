using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_Assignment.Models
{
    class Rentals
    {
        public Movie Movie { get; set; }
        public Member Member { get; set; }
        public DateTime RentalDate { get; set; }

        public Rentals (Movie movie, Member member)
        {

        }
    }
}
