using System;
using System.Linq.Expressions;
using FluentValidation;
using NetDevPack.Specification;
using NetDevPack.SpecificationResult;

namespace NetDevPack.Tests.Specs
{
    public sealed class AvailableOnStreamingSpecification : Specification<Movie>
    {
        private const int MonthsBeforeStreamingIsOut = 6;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeStreamingIsOut);
        }
    }

    public sealed class AvailableOnStreamingSpecificationValidator : SpecificationValidator<Movie>
    {
        private const int MonthsBeforeStreamingIsOut = 6;

        public AvailableOnStreamingSpecificationValidator()
        {
            Validator.RuleFor(movie => movie.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now.AddMonths(-MonthsBeforeStreamingIsOut))
                .WithMessage("Movie is not available on Stream");
        }
    }
}