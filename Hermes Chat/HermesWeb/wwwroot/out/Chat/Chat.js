"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var sigr = require("@microsoft/signalr");
var Message_1 = require("../Chat/Message");
var MessageValidator_1 = require("../Validators/MessageValidator");
var connection = new sigr.HubConnectionBuilder().withUrl("/Chat/Index").build();
// TODO write comments to functions !!!!
var Chat = /** @class */ (function () {
    function Chat() {
    }
    Chat.prototype.Initialize = function () {
        var self = this;
        self.validator = new MessageValidator_1.MessageValidator();
        $(document).ready(function () {
            self.AttachEvents();
        });
    };
    Chat.prototype.SendMessage = function (e) {
        var self = this;
        if (!self.validator.Validate('#message-text')) {
            e.preventDefault();
            return;
        }
        var message = new Message_1.Message($('input[name=UserName]').val(), $('#message-text').val(), new Date());
        var controllerObject = { Text: message.Text, Username: message.Username, CreationTime: message.CreationTime.toISOString() };
        $.post('/Chat/SendMessage', { message: controllerObject })
            .done(function (data) {
            if (data === true) {
                self.AddMessageToChat(message);
                self.PostMessageReceival();
                connection.invoke('sendMessage', message); // TODO SignalR not working
            }
            else {
                console.log('something went wrong'); // TODO  HANDLE LATER ERRORS WITH DISPLAY (Done in controller?)
            }
        });
    };
    Chat.prototype.AddMessageToChat = function (message) {
        this.DrawMessage(message);
    };
    Chat.prototype.ClearInputField = function () {
        $('#message-text').val('');
    };
    Chat.prototype.AttachEvents = function () {
        var self = this;
        $('#send-message').on('click', function (e) {
            self.SendMessage(e);
        });
        connection.on('newMessageReceived', function (message) {
            self.DrawMessage(message);
            self.PostMessageReceival();
        });
        connection.on("newUserJoined", function (user) {
            self.NewUserJoined(user);
        });
    };
    Chat.prototype.PostMessageReceival = function () {
        // dafuq why scroll to bottom is not working...
        this.ClearInputField();
    };
    Chat.prototype.DrawMessage = function (message) {
        var date = message.CreationTime;
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        var customDate = date.getUTCDate() + "." + month + "." + date.getUTCFullYear() + " at " + date.toTimeString().split(' ')[0];
        var mesg = "<div class=\"chat-row chat-message-box\">\n                  <p class=\"chat-row\">" + customDate + "</p>\n                  <p class=\"chat-row\">" + message.Username + "</p>\n                  <div class=\"message\">\n                      <p class=\"chat-row\">" + message.Text + "</p>\n                  </div>\n             </div>";
        $('#message-container').append(mesg);
    };
    /**
     * Notifies chat, that new user has joined
     * @param user - user that joined
     */
    Chat.prototype.NewUserJoined = function (user) {
        alert('new user joined'); // todo change later
    };
    return Chat;
}());
new Chat().Initialize();
//# sourceMappingURL=Chat.js.map