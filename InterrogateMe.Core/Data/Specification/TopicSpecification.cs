using InterrogateMe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InterrogateMe.Core.Data.Specification
{
    public class TopicSpecification : BaseSpecification<Topic>
    {
        public TopicSpecification(Expression<Func<Topic, object>> criteria) : base(criteria)
        {
        }

        public static TopicSpecification AllQuestion()
        {
            return new TopicSpecification(x => x.Questions);
        }
    }
}
