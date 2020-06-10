namespace Domain.Auth.Responses
{
  public class LoginUserResponse
  {
      public string Username { get; set; }
      public int UserId { get; set; }
      public string AccessToken { get; set; }
  }
}