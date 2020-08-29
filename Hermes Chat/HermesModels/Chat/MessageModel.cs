using HermesModels.Interfaces;
using HermesModels.MVC;
using System;

namespace HermesModels.Chat
{
    public class MessageModel : MvcBaseModel, IValidationObject
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime CreationTime { get; set; }

        public int UserId { get; set; }
    }
}