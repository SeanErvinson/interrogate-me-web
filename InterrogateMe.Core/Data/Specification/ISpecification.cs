using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        Expression<Func<T, object>> Definition { get; }
    }
}
