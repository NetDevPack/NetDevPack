using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDevPack.Domain
{
    /// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Needs to implement using a yield return statement to return each element one at a time
        /// </summary>
        /// <example>
        /// yield return Foo;
        /// yield return Bar;
        /// </example>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            return EqualOperator(a, b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return NotEqualOperator(a, b);
        }

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;
            
            return left is null || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }
    }
}
