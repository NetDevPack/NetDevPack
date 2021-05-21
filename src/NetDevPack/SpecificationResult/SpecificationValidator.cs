using FluentValidation.Results;
using NetDevPack.SpecificationResult.Result;
using NetDevPack.SpecificationResult.Validation;
using System;
using System.Linq.Expressions;

namespace NetDevPack.SpecificationResult
{
    public abstract class SpecificationValidator<T> : ValidatorObject<T>
         where T : class
    {
        private readonly ValidationResult _results = new ValidationResult();

        public SpecificationValidationResult Validate(T entity)
        {
            var predicate = ToExpression().Compile();
            var evaluationResult = predicate(entity, _results);
            return new SpecificationValidationResult(evaluationResult, _results.Errors);
        }

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            var evaluationResult = predicate(entity, _results);
            return evaluationResult;
        }

        public virtual Expression<Func<T, ValidationResult, bool>> ToExpression() => (e, results) => Validate(e, results);

        public SpecificationValidator<T> And(SpecificationValidator<T> specification)
        {
            return new AndSpecificationValidator<T>(this, specification);
        }

        public SpecificationValidator<T> Or(SpecificationValidator<T> specification)
        {
            return new OrSpecificationValidator<T>(this, specification);
        }

        public SpecificationValidator<T> Not()
        {
            return new NotSpecificationValidator<T>(this);
        }
    }
}
