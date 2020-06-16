using System;

namespace DatingApp.API.Domain.Messages.Requests
{
    public class MessageCreationRequest
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }
        public MessageCreationRequest()
        {
            MessageSent = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }
    }
}