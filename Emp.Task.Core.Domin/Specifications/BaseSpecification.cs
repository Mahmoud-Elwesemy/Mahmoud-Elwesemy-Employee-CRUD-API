using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Domin.Specifications;
public class BaseSpecification<T>:ISpecification<T>
{
    public BaseSpecification(Expression<Func<T,bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T,bool>> Criteria { get; }
}
