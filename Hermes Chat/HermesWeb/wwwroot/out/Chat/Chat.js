"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Chat = void 0;
var SignalR = require("@microsoft/signalr");
var Message_1 = require("../Chat/Message");
require("jquery-validation-unobtrusive");
var FormScripts_1 = require("../Layout/FormScripts");
var connection = new SignalR.HubConnectionBuilder().withUrl("/Chat/Index").build();
var Chat = /** @class */ (function () {
    function Chat() {
    }
    Chat.prototype.Initialize = function () {
        var self = this;
        $(document).ready(function () {
            connection.start();
            self._formScripts = new FormScripts_1.FormScripts();
            self.AttachEvents();
            self.AnimateScrollToMessageHistoryEnd();
            self.currentUserUsername = $('input[name=UserName]').val().toString();
        });
    };
    Chat.prototype.SendMessage = function (formId) {
        var self = this;
        var message = new Message_1.Message();
        // Values are handled on server, but it is needed for signalR
        message.CreationTime = new Date();
        message.Username = this.currentUserUsername;
        message.Text = $('#message-text').val().toString();
        var callbackOnSuccess = function () {
            connection.invoke('SendMessageAsync', message);
            self.PostMessageReceival();
        };
        this._formScripts.HandleFormSubmit(formId, callbackOnSuccess);
    };
    Chat.prototype.ClearInputField = function () {
        $('#message-text').val('');
    };
    Chat.prototype.AttachEvents = function () {
        var self = this;
        $('#send-message').on('click', function (e) {
            self.SendMessage('.chat-form');
        });
        connection.on("receiveMessage", function (message) {
            self.DrawMessage(message);
            self.AnimateScrollToMessageHistoryEnd();
        });
    };
    Chat.prototype.PostMessageReceival = function () {
        this.ClearInputField();
        this.AnimateScrollToMessageHistoryEnd();
    };
    Chat.prototype.DrawMessage = function (message) {
        var isMirrored = message.username == this.currentUserUsername;
        var messageHtml = "<ul class=\"" + (isMirrored ? "mirror" : null) + " max-height list-row\">\n                    <li class=\"left-pad-top\">\n                        <img class='person-avatar-M' src='data:image;base64," + message.accountImageData + "' />\n                    </li>\n                    <li class=\"photo-message-delimeter\"></li>\n                    <li class=\"chat-row chat-message-box\">\n                        <p class=\"chat-row " + (isMirrored ? "non-mirror" : null) + "\">" + this.GetFormattedSendingTimeForMessage() + "</p>\n                        <p class=\"chat-row " + (isMirrored ? "non-mirror" : null) + "\">" + message.username + "</p>\n                        <p class=\"chat-row  " + (isMirrored ? "non-mirror" : null) + " message\">" + message.text + "</p>\n                    </li>\n             </ul>";
        var messageContainer = $('#message-rows');
        messageContainer.append(messageHtml);
    };
    Chat.prototype.AnimateScrollToMessageHistoryEnd = function () {
        var messages = document.getElementById('message-rows');
        $(messages).stop().animate({
            scrollTop: "+=" + messages.scrollHeight // Potentially can cause strange scrolling behavior if too many messages
        }, 400);
    };
    Chat.prototype.GetFormattedSendingTimeForMessage = function () {
        var creationTime = new Date();
        var dayWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getDate());
        var monthWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getMonth() + 1);
        var hourWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getHours());
        var minutesWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getMinutes());
        var customTime = hourWithLeadingZeros + ":" + minutesWithLeadingZeros;
        var customDate = dayWithLeadingZeros + "." + monthWithLeadingZeros + "." + creationTime.getUTCFullYear();
        return customDate + " at " + customTime;
    };
    Chat.prototype.GetFormattedDateTimeWithLeadingZeros = function (time) {
        return ('0' + time).slice(-2);
    };
    return Chat;
}());
exports.Chat = Chat;
new Chat().Initialize();
//# sourceMappingURL=Chat.js.map