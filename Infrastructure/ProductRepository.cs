using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;

        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            var TypeId = 1;
            var productsss = _context.Products
            .Where(x => x.ProductTypeId == TypeId)
            .Include(x => x.ProductType).ToListAsync();

            var Products = await _context.Products
                //Eager loading of navigation properties
                .Include(b => b.ProductBrand)
                .Include(t => t.ProductType)
                .ToListAsync();
            return Products;
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(b => b.ProductBrand)
                .Include(t => t.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}