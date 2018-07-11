using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models.Base;
using System.Collections.Generic;

namespace InterrogateMe.Core.Data
{
    public interface IRepository
    {
        T Single<T>(ISpecification<T> specification) where T : BaseModel;
        ICollection<T> All<T>(ISpecification<T> specification) where T : BaseModel;
        T SingleInclude<T>(ISpecification<T> specification, IEnumerable<ISpecification<T>> includeSpecification) where T : BaseModel;
        void Remove<T>(T removeItem) where T : BaseModel;
        void Add<T>(T addItem) where T : BaseModel;
        void Update<T>(T updateItem) where T : BaseModel;
    }
}
