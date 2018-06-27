using InterrogateMe.Core.Data;
using InterrogateMe.Core.Data.Specification;
using InterrogateMe.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InterrogateMe.Data
{
    /// <summary>
    /// .Set<T>() is another way of saying
    /// _context.(the class).method
    /// </summary>
    public class Repository : IRepository
    {
        private readonly TodoDbContext _context;

        public Repository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T addItem) where T : BaseModel
        {
            _context.Set<T>().Add(addItem);
            _context.SaveChanges();
        }

        public IEnumerable<T> All<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            return specification != null ? dataSet.Where(specification.Criteria).ToList() : dataSet.ToList();
        }

        public T Single<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();
            return dataSet.SingleOrDefault(specification.Criteria);
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


        public IList<T> Include<T>(ISpecification<T> specification) where T : BaseModel
        {
            var dataSet = _context.Set<T>();

            IQueryable<T> query = null;
            query = dataSet.Include(specification.Criteria);

            return query.ToList();
        }
    }
}
