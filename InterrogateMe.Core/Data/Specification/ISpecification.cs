using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, object>> Criteria { get; }
    }
}
