using Data.Models.Entities;

namespace Application.Interfaces.Repositories;
public interface IUserRepository {
    Task<User> GetByEmail(string email);

    Task<bool> Create(User user);
}
