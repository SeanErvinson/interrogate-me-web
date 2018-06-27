using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace InterrogateMe.Core.Data
{
    public interface IRepository
    {
        T Single<T>(ISpecification<T> specification) where T : BaseModel;
        IEnumerable<T> All<T>(ISpecification<T> specification) where T : BaseModel;
        IList<T> Include<T>(ISpecification<T> specification) where T : BaseModel;
        void Remove<T>(T removeItem) where T : BaseModel;
        void Add<T>(T addItem) where T : BaseModel;
        void Update<T>(T updateItem) where T : BaseModel;
    }
}
