using Entities.DTOs.BasketDto;

namespace Services.Contracts
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketDto>> GetAllBasketsByUserAsync(string userId, bool? trackChanges);
        Task<BasketDto> GetBasketByIdAsync(int id, bool? trackChanges);
        Task<BasketDto> CreateBasketAsync(BasketDtoForInsertion basketDtoForInsertion);
        Task<BasketDto> UpdateBasketAsync(BasketDtoForUpdate basketDtoForUpdate);
        Task<BasketDto> DeleteBasketAsync(int id, bool? trackChanges);
    }
}
