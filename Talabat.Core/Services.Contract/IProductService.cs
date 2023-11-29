using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService
    {

        public Task<Product> GetProductAsync(int id);

        public Task<IReadOnlyList<Product>> GetAllProductsWithSpecAsync(ProductSpecPrams specParams);


        public Task<int> GetCountAsync(ProductSpecPrams spec);

        public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();

        public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
    }
}
