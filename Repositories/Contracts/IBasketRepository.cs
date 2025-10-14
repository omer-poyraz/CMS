using Entities.Models;

namespace Repositories.Contracts
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Task<IEnumerable<Basket>> GetAllBasketsByUserAsync(string userId, bool? trackChanges);
        Task<Basket> GetBasketByIdAsync(int id, bool? trackChanges);
        Basket CreateBasket(Basket basket);
        Basket UpdateBasket(Basket basket);
        Basket DeleteBasket(Basket basket);
    }
}
