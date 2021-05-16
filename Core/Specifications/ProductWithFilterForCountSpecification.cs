using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParamsInput input)
         : base(x =>
                 //   [||] => Else Expression 
                 // [&&] => And Also
                 //Where( x.ProductBrandId == brandId)
                 (string.IsNullOrEmpty(input.Search) || x.Name.ToLower().Contains(input.Search)) &&  //For Searching
                 (!input.BrandId.HasValue || x.ProductBrandId == input.BrandId) &&
                 (!input.TypeId.HasValue || x.ProductTypeId == input.TypeId)
              )
        {
        }
    }
}