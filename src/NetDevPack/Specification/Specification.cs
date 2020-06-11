using System;
using System.Linq.Expressions;

namespace NetDevPack.Specification
{
    public abstract class Specification<T>
    {
        private static readonly Specification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}