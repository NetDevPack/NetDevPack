using System;
using System.Linq.Expressions;
using FluentValidation;
using NetDevPack.Specification;
using NetDevPack.SpecificationResult;

namespace NetDevPack.Tests.Specs
{
    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }

    public sealed class MovieForKidsSpecificationValidator : SpecificationValidator<Movie>
    {
        public MovieForKidsSpecificationValidator()
        {
            Validator.RuleFor(movie => (int)movie.MpaaRating).LessThanOrEqualTo((int)MpaaRating.PG)
                .WithMessage("This film is not for children.");
        }
    }
}