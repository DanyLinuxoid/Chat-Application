"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SignalR = require("@microsoft/signalr");
var Message_1 = require("../Chat/Message");
require("jquery-validation-unobtrusive");
var FormScripts_1 = require("../Layout/FormScripts");
var connection = new SignalR.HubConnectionBuilder().withUrl("/Chat/Index").build(); /// SIGNALR TO OTHER LOGIC!!!!!!!!!!!!!!!!!!
// --------------- TODO -------------------
//1. Scroll on new message adding(+/- bug) -- DONE
//2. Dodelat6 sessiju(is db field needed ?) -- DONE
//3. Signalr not working - DONE
//3.5. dissalow user to get on login/registration page -- DONE
//4. Some fancy message animations on message adding -- DONE
//5. Next to message - account image -- DONE
//5.6 ajax form submit works fine with update values on acc, but not working for message, also it sends data multiple times if .submit() is triggered manually -- DONE
//7. Backend refactoring + 6.5 minimize IhttpContexAccessor + 6. retrieve values from cache, not from session, in session - store only id _ minimize ISessionLogic
//8. Final tests
//9. Unit tests (???)
//10. ready for prod :)
// validation remove error message on empty text
// return to chat if user is authorized and requests login page or registration page
var Chat = /** @class */ (function () {
    function Chat() {
        this.messageHeight = 115;
    }
    Chat.prototype.Initialize = function () {
        var self = this;
        $(document).ready(function () {
            self._formScripts = new FormScripts_1.FormScripts();
            self.AttachEvents();
            connection.start();
            self.ScrollToBottom();
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
            self.FormatAndDrawMessage(message);
        });
    };
    Chat.prototype.PostMessageReceival = function () {
        this.ClearInputField();
        this.AnimateScrollToNewMessage();
    };
    Chat.prototype.FormatAndDrawMessage = function (message) {
        var isMirrored = message.username == this.currentUserUsername;
        this.DrawMessage(message, isMirrored);
    };
    Chat.prototype.AnimateScrollToNewMessage = function () {
        var messages = document.getElementById('message-container');
        $(messages).stop().animate({
            scrollTop: "+=" + this.messageHeight
        }, 100);
    };
    Chat.prototype.ScrollToBottom = function () {
        var messages = document.getElementById('message-container');
        messages.scrollTop = messages.scrollHeight;
    };
    Chat.prototype.DrawMessage = function (message, mirrored) {
        var messageHtml = "<ul class=\"" + (mirrored ? "mirror" : null) + " list-row\">\n                    <li class=\"left-pad-top\">\n                        <img class='person-avatar-M' src='data:image;base64," + message.accountImageData + "' />\n                    </li>\n                    <li class=\"photo-message-delimeter\"></li>\n                    <li class=\"chat-row chat-message-box\">\n                        <p class=\"chat-row " + (mirrored ? "non-mirror" : null) + "\">" + this.GetFormattedSendingTimeForMessage() + "</p>\n                        <p class=\"chat-row " + (mirrored ? "non-mirror" : null) + "\">" + message.username + "</p>\n                        <p class=\"message chat-row " + (mirrored ? "non-mirror" : null) + "\">" + message.text + "</p>\n                    </li>\n             </ul>";
        var messageContainer = $('#message-rows');
        messageContainer.append(messageHtml);
        this.AnimateScrollToNewMessage();
    };
    Chat.prototype.GetFormattedSendingTimeForMessage = function () {
        var creationTime = new Date();
        var month = ('0' + (creationTime.getMonth() + 1)).slice(-2);
        var currentHour = creationTime.getHours();
        // getHours() returns hours in format 1.11 if one digit, have to check and add 0 manually if this is the case.
        var customTime = (currentHour.toString().length == 1 ? "0" + currentHour : currentHour) + ":" + creationTime.getMinutes();
        var customDate = creationTime.getUTCDate() + "." + month + "." + creationTime.getUTCFullYear();
        return customDate + " at " + customTime;
    };
    return Chat;
}());
new Chat().Initialize();
//# sourceMappingURL=Chat.js.map