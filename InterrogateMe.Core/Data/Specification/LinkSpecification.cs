using InterrogateMe.Core.Models;
using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public class LinkSpecification : BaseSpecification<Link>
    {
        public LinkSpecification(Expression<Func<Link, bool>> criteria) : base(criteria)
        {
        }

        public static LinkSpecification ByUrl(string url)
        {
            return new LinkSpecification(x => x.Url == url);
        }
    }
}
