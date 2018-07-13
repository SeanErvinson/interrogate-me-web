using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InterrogateMe.Data
{
    /// <summary>
    /// .Set<T>() is another way of saying
    /// _context.(the class).method
    /// </summary>
    public class Repository : IRepository
    {
        private readonly InterrogateDbContext _context;

        public Repository(InterrogateDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T addItem) where T : BaseModel
        {
            _context.Set<T>().Add(addItem);
            _context.SaveChanges();
        }

        public ICollection<T> All<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            return specification != null ? dataSet.Where(specification.Criteria).ToList() : dataSet.ToList();
        }

        public T Single<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            return dataSet.SingleOrDefault(specification.Criteria);
        }


        public T SingleInclude<T>(ISpecification<T> specification, IEnumerable<ISpecification<T>> includeSpecification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            var dataSetInclude = Include<T>(includeSpecification);
            return dataSetInclude.SingleOrDefault(specification.Criteria);
        }

        public T SingleInclude<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            return dataSet.SingleOrDefault(specification.Criteria);
        }


        public void Remove<T>(T removeItem) where T : BaseModel
        {
            _context.Set<T>().Remove(removeItem);
            _context.SaveChanges();
        }

        public void Update<T>(T updateItem) where T : BaseModel
        {
            _context.Set<T>().Update(updateItem);
            _context.SaveChanges();
        }

        /// <summary>
        /// Include the data specified on specification and stores into a queryable structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specifications">Collection of specifications</param>
        /// <returns></returns>
        private IQueryable<T> Include<T>(IEnumerable<ISpecification<T>> specifications) where T : BaseModel
        {
            var dataSet = _context.Set<T>();

            IQueryable<T> query = null;
            foreach (var specification in specifications)
            {
                query = dataSet.Include(specification.Definition);
            }

            return query ?? dataSet;
        }
    }
}
