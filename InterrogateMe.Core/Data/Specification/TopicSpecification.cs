using InterrogateMe.Core.Models;
using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public class TopicSpecification : BaseSpecification<Topic>
    {
        public TopicSpecification(Expression<Func<Topic, object>> criteria) : base(criteria)
        {
        }

        public static TopicSpecification IncludeQuestions()
        {
            return new TopicSpecification(x => x.Questions);
        }
    }
}
