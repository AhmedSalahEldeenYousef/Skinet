using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }  //  .Where(x => x.ProductTypeId == TypeId)
        List<Expression<Func<T, object>>> Includes { get; }

    }
}