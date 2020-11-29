using HermesDataAccess.Enums;
using HermesLogic.Features.Chat.Interfaces;
using HermesLogic.Mappers.DboToDto;
using HermesModels.Chat;
using HermesQueriesCommands.Commands;
using HermesQueriesCommands.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using HermesLogic.Mappers.DtoToDbo;
using HermesDataAccess.Interfaces;

namespace HermesLogic.Features.Chat
{
    /// <summary>
    /// Messaging system, for sending messages in chat/s.
    /// </summary>
    public class MessageLogic : IMessageLogic
    {
        /// <summary>
        /// Database access.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// Messaging system, for sending messages in chat/s.
        /// </summary>
        public MessageLogic(ISqlDb sqlDb) 
        {
            _sqlDb = sqlDb;
        }

        /// <summary>
        /// Send message to global chat.
        /// </summary>
        /// <param name="message">Message model to send.</param>
        public async Task SendMessageAsync(MessageModel message)
        {
            var user = await _sqlDb.CacheNQueryAsync(new GetUserDetailsByUserIdQuery(message.UserId), message.UserId);

            message.UserName = user.USERNAME;
            await _sqlDb.CommandAsync(new MESSAGES_Commands(), CommandTypesAsync.CreateAsync, message.ToDbMessage());
        }

        /// <summary>
        /// Returns all messages for global chat.
        /// </summary>
        public async Task<List<MessageModel>> GetAllMessagesAsync()
        {
            return (await _sqlDb.QueryAsync(new GetAllMessagesQuery())).ToMessageList();
        }
    }
}