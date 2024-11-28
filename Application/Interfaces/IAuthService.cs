using Data.Models.DTOs;
using Data.Models.Entities;
using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces;
public interface IAuthService {
    Task<bool> Register(UserRegisterDto userRegisterDto);

    Task<AuthUserResponse> Login(User user);

    bool IsPasswordCorrect(string password, string passwordHash);
}