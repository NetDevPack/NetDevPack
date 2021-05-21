using System;
using System.Linq;
using Xunit;

namespace NetDevPack.Tests.Specs
{
    public class SpecValidationTests
    {
        [Fact(DisplayName = "Validate should return True")]
        [Trait("Category", "SpecValidation Tests")]
        public void SpecValidation_Validate_ShouldReturnTrue()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.PG &&            // For kids
                m.ReleaseDate.Year == DateTime.Now.Year &&  // New Release
                m.Rating >= 4);                             // Best ratings

            var newestGoodMoviesForKidsValidation = new NewestGoodMoviesForKidsValidation();

            // Act
            var result = newestGoodMoviesForKidsValidation.Validate(movie);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validate should return False with error messages")]
        [Trait("Category", "SpecValidation Tests")]
        public void SpecValidation_Validate_ShouldReturnFalseWithErrorMessages()
        {
            // Arrange
            var movie = MovieFactory.GetMixedMovies().FirstOrDefault(m =>
                m.MpaaRating <= MpaaRating.R &&             // Not For kids
                m.ReleaseDate.Year < DateTime.Now.Year &&   // Old(?) Release
                m.Rating < 4);                              // Bad ratings

            var newestGoodMoviesForKidsValidation = new NewestGoodMoviesForKidsValidation();

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