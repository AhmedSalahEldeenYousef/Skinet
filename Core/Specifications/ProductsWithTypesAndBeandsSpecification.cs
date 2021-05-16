using System;
using System.Linq.Expressions;
using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBeandsSpecification : BaseSpecification<Product>
    {

        public ProductsWithTypesAndBeandsSpecification(ProductSpecParamsInput input)
        : base(x =>
                 //   [||] => Else Expression 
                 // [&&] => And Also
                 //Where( x.ProductBrandId == brandId)
                 (string.IsNullOrEmpty(input.Search) || x.Name.ToLower().Contains(input.Search)) &&  //For Searching
                 (!input.BrandId.HasValue || x.ProductBrandId == input.BrandId) &&
                 (!input.TypeId.HasValue || x.ProductTypeId == input.TypeId)
              )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(input.PageSize * (input.PageIndex - 1), input.PageSize);
            if (!string.IsNullOrEmpty(input.Sort))
            {
                switch (input.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            }
        }

        public ProductsWithTypesAndBeandsSpecification(int id)
        : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}