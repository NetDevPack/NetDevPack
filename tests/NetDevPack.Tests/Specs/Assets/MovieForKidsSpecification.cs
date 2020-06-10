using System;
using System.Linq.Expressions;
using NetDevPack.Specification;

namespace NetDevPack.Tests.Specs
{
    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }
}