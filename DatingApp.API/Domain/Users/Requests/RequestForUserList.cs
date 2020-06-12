using DatingApp.API.Helpers;

namespace DatingApp.API.Domain.Users.Requests
{
  public class RequestForUserList : PaginationParams
  {
      public string Gender { get; set; }
      public int UserId { get; set; }
      public int MinAge { get; set; }
      public int MaxAge { get; set; }
  }
}