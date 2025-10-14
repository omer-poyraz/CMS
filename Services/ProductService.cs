using AutoMapper;
using Entities.DTOs.ProductDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Product;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDtoForInsertion productDtoForInsertion)
        {
            var product = _mapper.Map<Entities.Models.Product>(productDtoForInsertion);
            _manager.ProductRepository.CreateProduct(product);
            await _manager.SaveAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> DeleteProductAsync(int id, bool? trackChanges)
        {
            var product = await _manager.ProductRepository.GetProductByIdAsync(id, "tr", trackChanges);
            _manager.ProductRepository.DeleteProduct(product);
            await _manager.SaveAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters, bool? trackChanges)
        {
            var productPagedList = await _manager.ProductRepository.GetAllProductsAsync(productParameters, trackChanges);
            var products = productPagedList;
            // Contents listesini doldur
            foreach (var product in products)
            {
                if (product.ContentID != null && product.ContentID.Any())
                {
                    var contents = await _manager.ContentRepository.FindByIdsAsync(product.ContentID);
                    product.Contents = contents.ToList();
                }
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return (productDtos, productPagedList.MetaData);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, string lang, bool? trackChanges)
        {
            var product = await _manager.ProductRepository.GetProductByIdAsync(id, lang, trackChanges);
            if (product != null && product.ContentID != null && product.ContentID.Any())
            {
                var contents = await _manager.ContentRepository.FindByIdsAsync(product.ContentID);
                // lang parametresine göre filtrele
                if (!string.IsNullOrEmpty(lang))
                {
                    contents = contents.Where(c => c.Code == lang);
                }
                product.Contents = contents.ToList();
            }
            // ContentID response'a eklenmesin diye DTO mapping ayarını kontrol etmelisin.
            if (product != null)
                product.ContentID = null;
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductBySlugAsync(string slug, string lang, bool? trackChanges)
        {
            var product = await _manager.ProductRepository.GetProductBySlugAsync(slug, lang, trackChanges);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> SortProductAsync(int id, int sort, bool? trackChanges)
        {
            var product = await _manager.ProductRepository.SortProductAsync(id, sort, trackChanges);
            await _manager.SaveAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDtoForUpdate productDtoForUpdate)
        {
            var product = await _manager.ProductRepository.GetProductByIdAsync(productDtoForUpdate.ID, "tr", false);
            _mapper.Map(productDtoForUpdate, product);
            _manager.ProductRepository.UpdateProduct(product);
            await _manager.SaveAsync();
            return _mapper.Map<ProductDto>(product);
        }
    }
}
