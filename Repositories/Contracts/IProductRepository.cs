using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Product;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool? trackChanges);
        Task<Product> GetProductByIdAsync(int id, string lang, bool? trackChanges);
        Task<Product> GetProductBySlugAsync(string slug, string lang, bool? trackChanges);
        Task<Product> SortProductAsync(int id, int sort, bool? trackChanges);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(Product product);
    }
}
