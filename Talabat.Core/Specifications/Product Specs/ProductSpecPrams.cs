namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecPrams
    {
        #region Attributes
        private int _pageSize =5;
        private string? search;
        private const int _maxPageSize = 5;
        #endregion

        public int PageSize { get => _pageSize; set => _pageSize = value > 10 ? _maxPageSize : value; } 

        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public string? Search { get => search; set => search = value.ToLower(); }


    }
}
