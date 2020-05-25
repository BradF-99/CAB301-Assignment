using System;
using System.Collections.Generic;

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
        public List<string> Starring { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public Genre Genre { get; set; }
        public Classification Classification { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AmountBorrowed { get; set; }
        public int Copies { get; set; }
        public int CopiesAvailable { get; set; }

        public Movie(string title, List<string> starring, string director, int duration, Genre genre, Classification classification, DateTime releaseDate, int copies)
        {
            this.Title = title;
            this.Starring = starring;
            this.Director = director;
            this.Duration = duration;
            this.Genre = genre;
            this.Classification = classification;
            this.ReleaseDate = releaseDate;
            this.Copies = copies;
            this.CopiesAvailable = copies;
            this.AmountBorrowed = 0;
        }
    }
}
