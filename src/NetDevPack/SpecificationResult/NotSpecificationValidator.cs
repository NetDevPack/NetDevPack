using FluentValidation.Results;
using System;
using System.Linq.Expressions;

namespace NetDevPack.SpecificationResult
{
    internal sealed class NotSpecificationValidator<T> : SpecificationValidator<T>
    where T : class
    {
        private readonly SpecificationValidator<T> _specification;

        public NotSpecificationValidator(SpecificationValidator<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, ValidationResult, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, ValidationResult, bool>>(notExpression, expression.Parameters);
        }
    }
}
