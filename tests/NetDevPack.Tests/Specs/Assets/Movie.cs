using System;
using NetDevPack.Domain;

namespace NetDevPack.Tests.Specs
{
    public class Movie : Entity
    {
        public string Name { get; }
        public DateTime ReleaseDate { get; }
        public MpaaRating MpaaRating { get; }
        public string Genre { get; }
        public double Rating { get; }
        public Director Director { get; }

        public Movie(string name, DateTime releaseDate, MpaaRating mpaaRating, string genre, double rating, Director director)
        {
            Name = name;
            ReleaseDate = releaseDate;
            MpaaRating = mpaaRating;
            Genre = genre;
            Rating = rating;
            Director = director;
        }

        protected Movie()
        {
        }
    }
}