using System;
using System.Linq.Expressions;
using FluentValidation;
using NetDevPack.Specification;
using NetDevPack.SpecificationResult;

namespace NetDevPack.Tests.Specs
{
    public sealed class BestRatedFilmsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Rating >= 4;
        }
    }

    public sealed class BestRatedFilmsSpecificationValidator : SpecificationValidator<Movie>
    {
        public BestRatedFilmsSpecificationValidator()
        {
            Validator.RuleFor(movie => movie.Rating).GreaterThanOrEqualTo(4)
                .WithMessage("This film is not well rated.");
        }
    }
}