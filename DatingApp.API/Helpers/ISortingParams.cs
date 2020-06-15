using System.Runtime.Serialization;

namespace DatingApp.API.Helpers
{
    public interface ISortingParams<T>
    {
        T SortBy { get; set; }
        SortOrder SortOrder { get; set; }

    }

    public enum SortOrder
    {
        [EnumMember(Value = "asc")]
        Asc,
        [EnumMember(Value = "desc")]
        Desc,
    }
}