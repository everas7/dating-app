using System;

namespace DatingApp.API.Domain.Messages.Responses
{
    public class MessageDetailsResponse
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }
    }
}