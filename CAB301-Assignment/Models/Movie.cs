using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_Assignment.Models
{
    public enum Genre
    {
        Drama,
        Adventure,
        Family,
        Action,
        SciFi,
        Comedy,
        Animated,
        Thriller,
        Other
    }

    public enum Classification
    {
        G,
        PG,
        M15,
        MA15
    }

    class Movie
    {
        public string Title { get; set; }
        public string Starring { get; set; }
        public string Director { get; set; }
        public Genre Genre { get; set; }
        public Classification Classification { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Movie(string title, string starring, string director, Genre genre, Classification classification, DateTime releaseDate)
        {
            this.Title = title;
            this.Starring = starring;
            this.Director = director;
            this.Genre = genre;
            this.Classification = classification;
            this.ReleaseDate = releaseDate;
        }
    }
}
