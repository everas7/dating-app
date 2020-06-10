using System.Threading.Tasks;
using DatingApp.API.Models;
using Domain.Auth.Responses;
using Domain.Auth.Requests;

namespace DatingApp.API.Services.Interfaces
{
  public interface IAuthService
  {
      Task<User> Register(RegisterUserRequest registerUserRequest);
      Task<LoginUserResponse> Login(LoginUserRequest loginUserRequest);
      
  }
}