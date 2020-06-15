using System.ComponentModel;
using System.Runtime.Serialization;
using DatingApp.API.Helpers;

namespace DatingApp.API.Domain.Users.Requests
{
    public class RequestForUserList : RequestParams<UserSortColumns>
    {
        public string Gender { get; set; }
        public int UserId { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }

    public enum UserSortColumns
    {
        [EnumMember(Value = "Created")]
        Created,
        [EnumMember(Value = "LastActive")]
        LastActive,
    }
}