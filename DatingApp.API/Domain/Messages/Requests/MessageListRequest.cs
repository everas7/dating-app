using System.Runtime.Serialization;
using DatingApp.API.Helpers;

namespace DatingApp.API.Domain.Messages.Requests
{
    public class MessageListRequest : RequestParams<MessageListSortColumns>
    {
        public int UserId { get; set; }
        public MessageContainer MessageContainer { get; set; } = MessageContainer.Unread;
    }

    public enum MessageListSortColumns
    {
        [EnumMember(Value = "MessageSent")]
        MessageSent,
    }

    public enum MessageContainer
    {
        Unread,
        Outbox,
        Inbox
    }
}