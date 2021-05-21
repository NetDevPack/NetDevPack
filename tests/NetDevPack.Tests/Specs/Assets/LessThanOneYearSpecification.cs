using System;
using System.Linq.Expressions;
using FluentValidation;
using NetDevPack.Specification;
using NetDevPack.SpecificationResult;

namespace NetDevPack.Tests.Specs
{
    public sealed class LessThanOneYearSpecification : Specification<Movie>
    {
        private const int Months = 12;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate >= DateTime.Now.AddMonths(-Months);
        }
    }

    public sealed class LessThanOneYearSpecificationValidator : SpecificationValidator<Movie>
    {
        private const int Months = 12;

        public LessThanOneYearSpecificationValidator()
        {
            Validator.RuleFor(movie => movie.ReleaseDate)
                .GreaterThanOrEqualTo(DateTime.Now.AddMonths(-Months))
                .WithMessage("This movie was released over a year ago.");
        }
    }
}