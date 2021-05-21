using System;
using System.Linq;
using Xunit;

namespace NetDevPack.Tests.Specs
{
    public class SpecValidatonResultTests
    {
        [Fact(DisplayName = "SingleSpecification ReturnTrue")]
        [Trait("Category", "Specification Tests")]
        public void Specification_SingleSpecification_ShouldReturnTrue()
        {
            // Arrange
            var movie = MovieFactory.GetForKids();

            var kidSpec = new MovieForKidsSpecificationValidator();

            // Act
            var result = kidSpec.Validate(movie);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "SingleSpecification ReturnFalse")]
        [Trait("Category", "Specification Tests")]
        public void Specification_SingleSpecification_ShouldReturnFalse()
        {
            // Arrange
            var movie = MovieFactory.GetRatedR();

            var kidSpec = new MovieForKidsSpecificationValidator();

            // Act
            var result = kidSpec.Validate(movie);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This film is not for children.");
        }

        [Fact(DisplayName = "AndSpecification")]
        [Trait("Category", "Specification Tests")]
        public void Specification_AndSpecification_ShouldReturnTrue()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.PG &&            // For kids
                m.ReleaseDate.Year < DateTime.Now.Year);    // Last year or older

            var director = movie.Director.Name;

            var kidSpec = new MovieForKidsSpecificationValidator();
            var dirSpec = new MovieDirectedBySpecificationValidator(director);
            var dvdSpec = new AvailableOnStreamingSpecificationValidator();

            var movieSpec = kidSpec.And(dirSpec).And(dvdSpec);

            // Act
            var result = movieSpec.Validate(movie);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "AndSpecification Different Director Name ReturnFalse")]
        [Trait("Category", "Specification Tests")]
        public void Specification_AndSpecification_WithDifferentDirectorName_ShouldReturnFalse()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.PG &&            // For kids
                m.ReleaseDate.Year < DateTime.Now.Year);    // Last year or older

            var director = movie.Director.Name + " LastName";

            var kidSpec = new MovieForKidsSpecificationValidator();
            var dirSpec = new MovieDirectedBySpecificationValidator(director);

            var movieSpec = kidSpec.And(dirSpec);

            // Act
            var result = movieSpec.Validate(movie);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "The director name is different.");
        }


        [Fact(DisplayName = "OrSpecification")]
        [Trait("Category", "Specification Tests")]
        public void Specification_OrSpecification_ShouldFilterMovies()
        {
            // Arrange
            var movieCount = MovieFactory.GetMixedMovies().Count(m =>
                m.MpaaRating <= MpaaRating.PG ||    // For kids OR
                m.Rating >= 4);                     // Best Rates

            var kidSpec = new MovieForKidsSpecificationValidator();
            var dirBest = new BestRatedFilmsSpecificationValidator();

            var movieSpec = kidSpec.Or(dirBest);

            // Act
            var result = MovieFactory.GetMixedMovies().Where(movieSpec.IsSatisfiedBy);

            // Assert
            Assert.Equal(movieCount, result.Count());
        }

        [Fact(DisplayName = "OrSpecification All Invalid ReturnFalse")]
        [Trait("Category", "Specification Tests")]
        public void Specification_OrSpecification_AllInvalid_ShouldReturnFalse()
        {
            // Arrange
            var movie = MovieFactory.GetRatedR();

            var director = movie.Director.Name + " LastName";

            var kidSpec = new MovieForKidsSpecificationValidator();
            var dirSpec = new MovieDirectedBySpecificationValidator(director);

            var movieSpec = kidSpec.Or(dirSpec);

            // Act
            var result = movieSpec.Validate(movie);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This film is not for children.");
            Assert.Contains(result.Errors, e => e.ErrorMessage == "The director name is different.");
        }

        [Fact(DisplayName = "NotSpecification")]
        [Trait("Category", "Specification Tests")]
        public void Specification_NotSpecification_ShouldFilterMovies()
        {
            // Arrange
            var movieCount = MovieFactory.GetMixedMovies().Count(m =>
                m.MpaaRating > MpaaRating.PG &&     // Not for kids
                m.Rating >= 4);                     // Best ratigs

            var kidSpec = new MovieForKidsSpecificationValidator();
            var dirBest = new BestRatedFilmsSpecificationValidator();

            var movieSpec = dirBest.And(kidSpec.Not());

            // Act
            var result = MovieFactory.GetMixedMovies().Where(movieSpec.IsSatisfiedBy);

            // Assert
            Assert.Equal(movieCount, result.Count());
        }

        [Fact(DisplayName = "Validate should return True")]
        [Trait("Category", "Specification Tests")]
        public void SpecValidation_Validate_ShouldReturnTrue()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.PG &&            // For kids
                m.ReleaseDate.Year == DateTime.Now.Year &&  // New Release
                m.Rating >= 4);                             // Best ratings

            var less1YearSpec = new LessThanOneYearSpecificationValidator();
            var forKidsSpec = new MovieForKidsSpecificationValidator();
            var bestRatedSpec = new BestRatedFilmsSpecificationValidator();

            var newestGoodMoviesForKidsValidation = less1YearSpec.And(forKidsSpec).And(bestRatedSpec);

            // Act
            var result = newestGoodMoviesForKidsValidation.Validate(movie);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validate should return False with error messages")]
        [Trait("Category", "Specification Tests")]
        public void SpecValidation_Validate_ShouldReturnFalseWithErrorMessages()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.R &&             // Not For kids
                m.ReleaseDate.Year < DateTime.Now.Year &&   // Old(?) Release
                m.Rating < 4);                              // Bad ratings

            var less1YearSpec = new LessThanOneYearSpecificationValidator();
            var forKidsSpec = new MovieForKidsSpecificationValidator();
            var bestRatedSpec = new BestRatedFilmsSpecificationValidator();

            var newestGoodMoviesForKidsValidation = less1YearSpec.Or(forKidsSpec.Or(bestRatedSpec));

            // Act
            var result = newestGoodMoviesForKidsValidation.Validate(movie);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This movie was released over a year ago.");
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This film is not for children.");
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This film is not well rated.");
        }
    }
}
