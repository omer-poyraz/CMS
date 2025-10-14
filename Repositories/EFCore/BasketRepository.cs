using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(RepositoryContext context) : base(context) { }

        public Basket CreateBasket(Basket basket)
        {
            Create(basket);
            return basket;
        }

        public Basket DeleteBasket(Basket basket)
        {
            Delete(basket);
            return basket;
        }

        public async Task<IEnumerable<Basket>> GetAllBasketsByUserAsync(string userId, bool? trackChanges) =>
            await FindAll(trackChanges)
                .Where(s => s.UserId!.Equals(userId))
                .OrderBy(s => s.CreatedAt)
                .Include(s => s.Product)
                .ToListAsync();

        public async Task<Basket> GetBasketByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public Basket UpdateBasket(Basket basket)
        {
            Update(basket);
            return basket;
        }
    }
}
