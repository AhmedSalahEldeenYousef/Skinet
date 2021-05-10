using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        /*
            var TypeId = 1;  // var Query = inputQuery; 

            var productsss = _context.Products
            .Where(x => x.ProductTypeId == TypeId)

            .Include(x => x.ProductType).ToListAsync(); => 
            Query = spec.Includes.Aggregate(Query, (current, includes) => current.Include(includes));
        
        */
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
                                                       ISpecification<TEntity> spec)
        {
            var Query = inputQuery;   //var TpyeId = 1
            if (spec.Criteria != null)
            {
                Query = Query.Where(spec.Criteria);  //Where(p=>p.ProductId ==Id)
            }

            //Currnet => Whicj Represents The Entity
            //includes => Which Is Gonna Be The Expression Of Our Includes

            //Include Statment Method
            Query = spec.Includes.Aggregate(Query, (current, includes) => current.Include(includes));

            return Query;
        }
    }
}