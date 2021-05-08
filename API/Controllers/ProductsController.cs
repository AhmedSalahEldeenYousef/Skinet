using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //  private readonly StoreContext _context;

        private readonly IProductRepository _repo;
        public ProductsController(StoreContext context, IProductRepository repo)
        {
            //  _context = context;
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduts()
        {


            var products = await _repo.GetAllProductsAsync();
            return Ok(products);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _repo.GetAllProductBrandsAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {

            return Ok(await _repo.GetAllProductTypesAsync());
        }
    }
}