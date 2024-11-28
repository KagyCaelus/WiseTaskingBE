using Application.Interfaces.Repositories;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Repositories;
public class UserRepository : IUserRepository {

    private readonly WiseTaskingDbContext _dbContext;

    public UserRepository(WiseTaskingDbContext context) 
    {
        _dbContext = context;
    }
    public async Task<bool> Create(User user)
    {
        try
        {
            await _dbContext.Users.AddAsync(user);

            _dbContext.SaveChanges();

            return true;
        } 
        catch(Exception ex) 
        { 
            return false;
        }
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
