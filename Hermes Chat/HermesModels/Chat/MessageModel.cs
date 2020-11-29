using HermesModels.Interfaces;
using System;

namespace HermesModels.Chat
{
    public class MessageModel : IValidationObject
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime CreationTime { get; set; }

        public long UserId { get; set; }
    }
}