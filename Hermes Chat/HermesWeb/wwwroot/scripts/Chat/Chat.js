"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Message = void 0;
var signalR = require("../../../node_modules/@microsoft/signalr");
var $ = require("jquery");
var connection = new signalR.HubConnectionBuilder().withUrl("/Home/Index").build(); // change with tokens, change default path
var Chat = /** @class */ (function () {
    function Chat() {
    }
    Chat.prototype.Initialize = function (jsonPreloadInformation) {
        this.AttachEvents();
        this.DisplayChatContent(jsonPreloadInformation);
    };
    Chat.prototype.DisplayChatContent = function (jsonContent) {
        var content = JSON.parse(jsonContent);
        for (var i = 0; i < content.counters.length; i++) {
            this.AddMessageToChat(new Message(content[i].Messages.UserName, content[i].Messages.Text, content[i].Messages.When));
        }
    };
    Chat.prototype.SendMessage = function (sender, text, when) {
        var _this = this;
        var message = new Message(sender, text, when);
        $.ajax({
            type: "post",
            url: '@Url.Action("SendMessage", "Chat")',
            contentType: "application/json; charset=utf-8",
            dataType: JSON.stringify(message),
            success: function (data) {
                if (!data.HasErrors) {
                    console.log('all fine');
                    _this.AddMessageToChat(message);
                }
            },
        });
        console.log('went wrong');
    };
    Chat.prototype.AddMessageToChat = function (message) {
        this.ClearInputField();
        this.Messages.push(message);
        this.DrawMessage(message);
        connection.invoke('sendMessage', message);
    };
    Chat.prototype.ClearInputField = function () {
        $('message-text').val('');
    };
    Chat.prototype.AttachEvents = function () {
        var _this = this;
        $('#submitButton').on('click', function () {
            _this.SendMessage(_this.MessageText, _this.SenderUsername, _this.When);
        });
        connection.on('newMessageReceived', function (message) {
            this.DrawMessage(message);
        });
        connection.on("newUserJoined", function (user) {
            this.NewUserJoined(user);
        });
    };
    Chat.prototype.DrawMessage = function (message) {
        var messageContainer = "<div>\n                  <p>" + message.When + "</p>\n                  <p>" + message.Username + "</p>\n                  <p>" + message.Text + "</p>\n             </div>";
        $('.chat').append(messageContainer);
    };
    Chat.prototype.NewUserJoined = function (user) {
        alert('new user joined'); // todo change later
    };
    return Chat;
}());
var Message = /** @class */ (function () {
    function Message(username, text, when) {
        this.Username = username;
        this.Text = text;
        this.When = when;
    }
    return Message;
}());
exports.Message = Message;
//# sourceMappingURL=Chat.js.map