using Application.Interfaces;
using Application.Interfaces.Repositories;
using Data.Models.DTOs;
using Data.Models.Entities;
using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WiseTaskingBE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {

    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AuthController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        if (userRegister.Password != userRegister.RepeatPassword)
        {
            return Conflict(new Response { Success = false, Message = "The password are not the same" });
        }

        User userByEmail = await _userRepository.GetByEmail(userRegister.Email);

        if (userByEmail != null)
        {
            return Conflict(new Response { Success = false, Message = "This email is already registered" });
        }
        var succes = await _authService.Register(userRegister);
        if(succes) return Ok(new Response { Success = true, Message = "Register Succes"});
        else return BadRequest(new Response { Success = false, Message = "Register Failed. Try Again." });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        User userByEmail = await _userRepository.GetByEmail(userLogin.Email);

        if (userByEmail == null)
            return Ok(new Response { Success = false, Message = "This user email doesn't exist" });

        if(!_authService.IsPasswordCorrect(userLogin.Password, userByEmail.PasswordHash))
            return Ok(new Response { Success = false, Message = "The email or password is wrong" });

        var authResponse = await _authService.Login(userByEmail);
        return Ok(authResponse);
    }

    private IActionResult BadRequestModelState() 
    {
        IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        return BadRequest(new Response { Success = false, Message = errorMessages.FirstOrDefault() });
    }
}

