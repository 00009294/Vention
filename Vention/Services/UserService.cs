using Microsoft.EntityFrameworkCore;
using Vention.Data;
using Vention.Models;

namespace Vention.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext appDbContext;
       
        public UserService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<User> GellAll()
        {
            return this.appDbContext.Users.ToList().AsQueryable();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var selectedUser =  await this.appDbContext.Users.Where(u=>u.Id == id).FirstOrDefaultAsync();
            return selectedUser;
        }

        public async Task<bool> CreateAsync(User user)
        {
            if(user is null)
            {
                return false;
            }
            int userId = user.Id;
            var existedUser = await IsExistAsync(userId);
            if(existedUser is false)
            {
                this.appDbContext.Users.Add(user);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<bool> UpdateAsync(int id, User user)
        {
            if(user is null)
            {
                return false;
            }
            await IsExistAsync(id);
            var existingUser = await this.appDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false; 
            }
            existingUser.Id = user.Id;
            existingUser.Name = user.Name;
            existingUser.Age = user.Age;
            existingUser.Email = user.Email;
            existingUser.City = user.City;
            existingUser.PhoneNumber = user.PhoneNumber;

            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existedUser = await IsExistAsync(id);
            if (existedUser)
            {
                var selectedUserId = this.appDbContext.Users.FirstOrDefault(u => u.Id == id);
                this.appDbContext.Users.Remove(selectedUserId);
                return await SaveAsync();
            }
            return false;

        }
        public async Task<bool> IsExistAsync(int id)
        {
            var user = await this.appDbContext.Users.Where(u => u.Id == id).AnyAsync();
            return user;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await appDbContext.SaveChangesAsync();
            return saved > 0;
        }

    }
}
