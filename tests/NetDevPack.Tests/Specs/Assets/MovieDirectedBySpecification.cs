using System;
using System.Linq.Expressions;
using FluentValidation;
using NetDevPack.Specification;
using NetDevPack.SpecificationResult;

namespace NetDevPack.Tests.Specs
{
    public sealed class MovieDirectedBySpecification : Specification<Movie>
    {
        private readonly string _director;

        public MovieDirectedBySpecification(string director)
        {
            _director = director;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Director.Name == _director;
        }
    }

    public sealed class MovieDirectedBySpecificationValidator : SpecificationValidator<Movie>
    {
        public MovieDirectedBySpecificationValidator(string director)
        {
            Validator.RuleFor(movie => movie.Director).Must(d => d.Name.Equals(director))
                .WithMessage("The director name is different.");
        }
    }
}