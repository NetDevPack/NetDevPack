using NetDevPack.Specification;

namespace NetDevPack.Tests.Specs
{
    public class NewestGoodMoviesForKidsValidation : SpecValidator<Movie>
    {
        public NewestGoodMoviesForKidsValidation()
        {
            var less1YearSpec = new LessThanOneYearSpecification();
            var forKidsSpec = new MovieForKidsSpecification();
            var bestRatedSpec = new BestRatedFilmsSpecification();

            base.Add("less1YearSpec", new Rule<Movie>(less1YearSpec, "This movie was released over a year ago."));
            base.Add("forKidsSpec", new Rule<Movie>(forKidsSpec, "This film is not for children."));
            base.Add("bestRatedSpec", new Rule<Movie>(bestRatedSpec, "This film is not well rated."));
        }
    }
}