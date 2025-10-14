using Entities.DTOs.ProductDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Product;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters, bool? trackChanges);
        Task<ProductDto> GetProductByIdAsync(int id, string lang, bool? trackChanges);
        Task<ProductDto> GetProductBySlugAsync(string slug, string lang, bool? trackChanges);
        Task<ProductDto> SortProductAsync(int id, int sort, bool? trackChanges);
        Task<ProductDto> CreateProductAsync(ProductDtoForInsertion productDtoForInsertion);
        Task<ProductDto> UpdateProductAsync(ProductDtoForUpdate productDtoForUpdate);
        Task<ProductDto> DeleteProductAsync(int id, bool? trackChanges);
    }
}
