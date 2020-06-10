using System;
using System.Linq.Expressions;
using NetDevPack.Specification;

namespace NetDevPack.Tests.Specs
{
    public sealed class BestRatedFilmsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Rating >= 4;
        }
    }
}