using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductsWithFilterationForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpecifications(ProductSpecPrams specParams)
            : base(
               P =>
                 (String.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&

               (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId) &&
               (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId)
                )
        {

        }


    }
}
