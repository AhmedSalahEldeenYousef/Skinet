using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{


    public class ProductsController : BaseApiController
    {
        //  private readonly StoreContext _context;

        private readonly IProductRepository _repo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
                                IProductRepository repo,
                               IGenericRepository<Product> productsRepo,
                               IGenericRepository<ProductBrand> productBrandRepo,
                               IGenericRepository<ProductType> productTypeRepo,
                               IMapper mapper
                               )
        {
            //  _context = context;
            _repo = repo;
            _productRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProduts([FromQuery] ProductSpecParamsInput input)
        {

            var spec = new ProductsWithTypesAndBeandsSpecification(input);
            var countSpec = new ProductWithFilterForCountSpecification(input);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(input.PageIndex,
                input.PageSize, totalItems, data));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBeandsSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(400));
            // _mapper.Map<From,To>(Variable)
            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {

            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}