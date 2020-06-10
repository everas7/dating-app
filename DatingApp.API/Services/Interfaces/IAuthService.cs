using System.Threading.Tasks;
using DatingApp.API.Models;
using Domain.Auth.Responses;
using Domain.Auth.Requests;
using Domain.Users.Responses;

namespace DatingApp.API.Services.Interfaces
{
  public interface IAuthService
  {
      Task<UserDetailsResponse> Register(RegisterUserRequest registerUserRequest);
      Task<LoginUserResponse> Login(LoginUserRequest loginUserRequest);
      
  }
}