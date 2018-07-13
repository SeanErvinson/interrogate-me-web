using InterrogateMe.Core.Models;
using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public class IpAddressSpecification : BaseSpecification<IpAddress>
    {
        public IpAddressSpecification(Expression<Func<IpAddress, bool>> criteria) : base(criteria)
        {
        }

        public IpAddressSpecification(Expression<Func<IpAddress, object>> definition) : base(definition)
        {
        }

        public static IpAddressSpecification ByIpAddressAndTopicId(string ipAddress, Guid topicId)
        {
            return new IpAddressSpecification(x => x.Address == ipAddress && x.TopicId == topicId);
        }
    }
}
