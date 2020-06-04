namespace Domain.Auth.DTOs
{
  public class LoginUserDTO
  {
      public string Username { get; set; }
      public int UserId { get; set; }
      public string AccessToken { get; set; }
  }
}