using Vention.Models;

namespace Vention.Services
{
    public interface IUserService
    {
        public IQueryable<User> GellAll();
        public Task<User> GetByIdAsync(int id);
        public Task<bool> CreateAsync(User user);
        public Task<bool> UpdateAsync(int id, User user);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> SaveAsync();
        public Task<bool> IsExistAsync(int id);

    }
}
