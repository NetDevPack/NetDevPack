using System;
using System.Linq.Expressions;
using NetDevPack.Specification;

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
}