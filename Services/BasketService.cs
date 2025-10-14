using AutoMapper;
using Entities.DTOs.BasketDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public BasketService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<BasketDto> CreateBasketAsync(BasketDtoForInsertion basketDtoForInsertion)
        {
            var basket = _mapper.Map<Entities.Models.Basket>(basketDtoForInsertion);
            var alreadyExists = await _manager.BasketRepository.GetBasketByIdAsync(basket.ID, false);
            if (alreadyExists != null)
            {
                var piece = alreadyExists.Piece + basket.Piece;
                basket.Piece = piece;
                basket.UpdatedAt = DateTime.UtcNow;
                _manager.BasketRepository.UpdateBasket(basket);
            }
            else
            {
                _manager.BasketRepository.CreateBasket(basket);
            }
            await _manager.SaveAsync();
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> DeleteBasketAsync(int id, bool? trackChanges)
        {
            var basket = await _manager.BasketRepository.GetBasketByIdAsync(id, trackChanges);
            _manager.BasketRepository.DeleteBasket(basket);
            await _manager.SaveAsync();
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<IEnumerable<BasketDto>> GetAllBasketsByUserAsync(string userId, bool? trackChanges)
        {
            var baskets = await _manager.BasketRepository.GetAllBasketsByUserAsync(userId, trackChanges);
            return _mapper.Map<IEnumerable<BasketDto>>(baskets);
        }

        public async Task<BasketDto> GetBasketByIdAsync(int id, bool? trackChanges)
        {
            var basket = await _manager.BasketRepository.GetBasketByIdAsync(id, trackChanges);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDtoForUpdate basketDtoForUpdate)
        {
            var basket = await _manager.BasketRepository.GetBasketByIdAsync(basketDtoForUpdate.ID, false);
            _mapper.Map(basketDtoForUpdate, basket);
            _manager.BasketRepository.UpdateBasket(basket);
            await _manager.SaveAsync();
            return _mapper.Map<BasketDto>(basket);
        }
    }
}
