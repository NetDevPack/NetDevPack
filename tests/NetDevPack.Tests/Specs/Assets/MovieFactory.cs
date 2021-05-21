using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDevPack.Tests.Specs
{
    public static class MovieFactory
    {
        public static Movie GetForKids()
        {
            return GetMixedMovies().FirstOrDefault(m => m.MpaaRating <= MpaaRating.PG);
        }

        public static Movie GetRatedR()
        {
            return GetMixedMovies().FirstOrDefault(m => m.MpaaRating == MpaaRating.R);
        }

        public static List<Movie> GetMixedMovies()
        {
            return new List<Movie>
            {
                new Movie("Finding Nemo", new DateTime(2003, 07, 04), MpaaRating.G, "Animation", 5, new Director("Andrew Stanton")),
                new Movie("The Godfather", new DateTime(1972, 01, 01), MpaaRating.R, "Drama", 5, new Director("Francis Ford Coppola")),
                new Movie("Scarface", new DateTime(1983, 07, 04), MpaaRating.R, "Action", 5, new Director("Brian De Palma")),
                new Movie("The Hobbit: The Battle of the Five Armies ", new DateTime(2014, 07, 04), MpaaRating.PG13, "Adventure", 4, new Director("Peter Jackson")),
                new Movie("Alone in the Dark", new DateTime(2005, 07, 04), MpaaRating.R, "Horror", 1, new Director("Uwe Boll")),
                new Movie("Scoob!", new DateTime(DateTime.Now.Year, 05, 15), MpaaRating.G, "Animation", 5, new Director("Matt Lieberman"))
            };
        }
    }
}