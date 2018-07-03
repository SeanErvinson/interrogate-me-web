using InterrogateMe.Core.Models;
using System;
using System.Linq.Expressions;

namespace InterrogateMe.Core.Data.Specification
{
    public class ProfaneWordSpecification : BaseSpecification<ProfaneWord>
    {
        public ProfaneWordSpecification(Expression<Func<ProfaneWord, bool>> criteria) : base(criteria)
        {
        }

        public static ProfaneWordSpecification ByWord(string word)
        {
            return new ProfaneWordSpecification(x => x.Word == word);
        }
    }
}
