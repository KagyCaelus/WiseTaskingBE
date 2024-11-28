﻿using Application.Interfaces;
using Application.Interfaces.Repositories;
using Data.Models;
using Data.Models.DTOs;
using Data.Models.Entities;
using Data.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;
public class AuthService : IAuthService {

    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly AuthConfiguration _authConfiguration;
    public AuthService(IPasswordHasher passwordHasher, AuthConfiguration authConfiguration, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _authConfiguration = authConfiguration;
        _userRepository = userRepository;
    }

    public async Task<AuthUserResponse> Login(User user)
    {
        
        var accessToken = GenerateToken(user); 
        return new AuthUserResponse
        {
            AccessToken = accessToken,
            UserId = user.UserId,
            Success = true,
            Message = "Login success"
        };
    }

    public async Task<bool> Register(UserRegisterDto userRegister)
    {
        string passwordHashed = _passwordHasher.HashPassword(userRegister.Password);

        User user = new User
        {
            Email = userRegister.Email,
            PasswordHash = passwordHashed,
            UserName = userRegister.UserName,
            Biography = String.Empty,
            IsEmailConfirmed = false
        };

        await _userRepository.Create(user);

        return true;
    }

    public bool IsPasswordCorrect(string password, string passwordHash) 
    { 
        return _passwordHasher.VerifyPassword(password, passwordHash);
    }

    private string GenerateToken(User user)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Key));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>()
        {
            new Claim("id", user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        JwtSecurityToken token = new JwtSecurityToken(
            _authConfiguration.Issuer,
            _authConfiguration.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_authConfiguration.ExpirationMinutes),
            credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}