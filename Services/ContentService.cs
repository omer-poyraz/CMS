using AutoMapper;
using Entities.DTOs.ContentDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Content;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ContentService : IContentService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ContentService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<ContentDto> CreateContentAsync(ContentDtoForInsertion contentDtoForInsertion)
        {
            var content = _mapper.Map<Entities.Models.Content>(contentDtoForInsertion);
            _manager.ContentRepository.CreateContent(content);
            await _manager.SaveAsync();
            return _mapper.Map<ContentDto>(content);
        }

        public async Task<ContentDto> DeleteContentAsync(int id, bool? trackChanges)
        {
            var content = await _manager.ContentRepository.GetContentByIdAsync(id, trackChanges);
            _manager.ContentRepository.DeleteContent(content);
            await _manager.SaveAsync();
            return _mapper.Map<ContentDto>(content);
        }

        public async Task<(IEnumerable<ContentDto> contentDtos, MetaData metaData)> GetAllContentsAsync(ContentParameters contentParameters, bool? trackChanges)
        {
            var pagedList = await _manager.ContentRepository.GetAllContentsAsync(contentParameters, trackChanges);
            var contents = pagedList;
            // Her Content için Products listesini doldur
            var allProducts = await _manager.ProductRepository.GetAllProductsAsync(new Entities.RequestFeature.Product.ProductParameters { PageNumber = 1, PageSize = int.MaxValue }, false);
            foreach (var content in contents)
            {
                content.Products = allProducts.Where(p => p.ContentID != null && p.ContentID.Contains(content.ID)).ToList();
            }
            var contentsDto = _mapper.Map<IEnumerable<ContentDto>>(contents);
            return (contentsDto, pagedList.MetaData);
        }

        public async Task<ContentDto> GetContentByIdAsync(int id, bool? trackChanges)
        {
            var content = await _manager.ContentRepository.GetContentByIdAsync(id, trackChanges);
            if (content != null)
            {
                var allProducts = await _manager.ProductRepository.GetAllProductsAsync(new Entities.RequestFeature.Product.ProductParameters { PageNumber = 1, PageSize = int.MaxValue }, false);
                content.Products = allProducts.Where(p => p.ContentID != null && p.ContentID.Contains(content.ID)).ToList();
            }
            return _mapper.Map<ContentDto>(content);
        }

        public async Task<ContentDto> GetContentAsync(bool? trackChanges)
        {
            var content = await _manager.ContentRepository.GetContentAsync(trackChanges);
            return _mapper.Map<ContentDto>(content);
        }

        public async Task<ContentDto> UpdateContentAsync(ContentDtoForUpdate contentDtoForUpdate)
        {
            var content = await _manager.ContentRepository.GetContentByIdAsync(contentDtoForUpdate.ID, false);
            _mapper.Map(contentDtoForUpdate, content);
            _manager.ContentRepository.UpdateContent(content);
            await _manager.SaveAsync();
            return _mapper.Map<ContentDto>(content);
        }
    }
}
