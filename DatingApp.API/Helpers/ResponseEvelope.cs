using System.Collections.Generic;

namespace DatingApp.API.Helpers
{
  public class PaginatedResponseEnvelope<T>
  {
      public List<T> Response { get; set; }
      public PaginationHeaders PaginationHeaders { get; set; }
  }
}
