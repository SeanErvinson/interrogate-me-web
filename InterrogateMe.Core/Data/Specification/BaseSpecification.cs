using InterrogateMe.Core.Models.Base;
using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public Expression<Func<T, object>> Definition { get; }


        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecification(Expression<Func<T, object>> definition)
        {
            Definition = definition;
        }


        public static BaseSpecification<T> All()
        {
            return new BaseSpecification<T>(x => true);
        }

        public static BaseSpecification<T> ById(Guid id)
        {
            return new BaseSpecification<T>(x => x.Id == id);
        }
    }
}
