using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repositry;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,
            IMapper mapper)
        {
           
            this._productService = productService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Paganiation<IReadOnlyList<PeoductToReturnDto>>>> GetAllProducts([FromQuery] ProductSpecPrams specParams)
        {
            var products = await _productService.GetAllProductsWithSpecAsync(specParams);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<PeoductToReturnDto>>(products);

            var count = await _productService.GetCountAsync(specParams);
            return Ok(new Paganiation<PeoductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PeoductToReturnDto>> GetProduct(int id)
        {
            

            var product = await _productService.GetProductAsync(id);
            return Ok(_mapper.Map<Product, PeoductToReturnDto>(product));
        }


        [HttpGet("brands")] // GET : /api/Product/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        => Ok(await _productService.GetBrandsAsync());


        [HttpGet("categories")] // GET : /api/producut/categoris
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        => Ok(await _productService.GetCategoriesAsync());


    }
}
