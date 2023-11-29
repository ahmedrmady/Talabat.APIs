using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
       

        public async Task<IReadOnlyList<Product>> GetAllProductsWithSpecAsync(ProductSpecPrams specParams)
        {
            var ProductSpecifacitions = new ProductWithBrandAndCategorySpecifications(specParams);
            return await _unitOfWork.Repositry<Product>().GetAllWithSpecAsync(ProductSpecifacitions);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        => await _unitOfWork.Repositry<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        => await _unitOfWork.Repositry<ProductCategory>().GetAllAsync();

       

        public  async Task<int> GetCountAsync(ProductSpecPrams spec)
        {
            var countSpecs = new ProductsWithFilterationForCountSpecifications(spec);
            return await _unitOfWork.Repositry<Product>().CountAsync(countSpecs);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var productSpecifacitions = new ProductWithBrandAndCategorySpecifications(id);
            return await _unitOfWork.Repositry<Product>().GetWithSpecAsync(productSpecifacitions);
        }

        
    }
}
