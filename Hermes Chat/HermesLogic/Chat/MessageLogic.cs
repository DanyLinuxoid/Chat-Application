using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesLogic.Base;
using HermesLogic.Interfaces;
using HermesLogic.Mappers;
using HermesModels.Chat;
using HermesModels.Enums;
using HermesQueriesCommands.TBL_MESSAGES;
using HermesQueriesCommands.TBL_MESSAGES.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HermesLogic.Chat
{
    public class MessageLogic : BaseLogic, IMessageLogic
    {
        private readonly ISqlDb _database;

        public MessageLogic(
            ISqlDb database,
            ICommonDependencies commonDependencies,
            IUserManager userManager)
            : base(commonDependencies)
        {
            _database = database;
        }

        public async Task<bool?> SendMessageAsync(MessageModel message)
        {
            var user = _commonDependencies.UserManager.GetUser(new string[] { message.UserName }, UserRetrieveOption.GetByUsername);
            if (user == null)
            {
                return false;
            }

            var result = _database.CommandAsync(new MESSAGES_Commands(), CommandTypes.CreateAsync, new Dictionary<string, object>()
            {
                ["UserName"] = message.UserName,
                ["Text"] = message.Text,
                ["CreationTime"] = message.CreationTime,
                ["UserId"] = user.Id,
            });

            return result.Id != 0; 
        }

        public async Task<List<MessageModel>> GetAllMessagesAsync()
        {
            var result = await _database.QueryAsync(new GetAllMessagesQuery());
            return result.ToMessageList();
        }
    }
}