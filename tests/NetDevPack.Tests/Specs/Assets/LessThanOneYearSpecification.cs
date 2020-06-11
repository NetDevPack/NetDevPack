using System;
using System.Linq.Expressions;
using NetDevPack.Specification;

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
}