using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }  //  .Where(x => x.ProductTypeId == TypeId)
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        //[Filtring Data]
        //Take => take A Certain Amount Of Records Or Certain Amount Of Products. 
        int Take { get; }

        //Skip => skip A Certain Amount Of Products as Well. 
        int Skip { get; }
        bool IsPagingEnabled { get; }



    }
}