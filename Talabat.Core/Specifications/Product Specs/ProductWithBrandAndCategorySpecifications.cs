using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;


namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {

        public ProductWithBrandAndCategorySpecifications(ProductSpecPrams specParams)
            : base(P =>
                 (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))&&
                 (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId)
                 &&
                 (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId)

            )

        {
            AddIncludesToTheList();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);

                        break;
                }
            }

            else
                AddOrderBy(P => P.Name);

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);


        }
        public ProductWithBrandAndCategorySpecifications()
            : base()
        {
            AddIncludesToTheList();
        }

        public ProductWithBrandAndCategorySpecifications(int id)
            : base(p => p.Id == id)
        {
            AddIncludesToTheList();
        }
        private void AddIncludesToTheList()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }

    }
}
