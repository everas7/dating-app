using System.Threading.Tasks;
using DatingApp.API.Models;
using Domain.Auth.DTOs;
using Domain.Auth.Payloads;

namespace DatingApp.API.Services.Interfaces
{
  public interface IAuthService
  {
      Task<User> Register(RegisterUserPayload registerUserPayload);
      Task<LoginUserDTO> Login(LoginUserPayload loginUserPaylaod);
      
  }
}