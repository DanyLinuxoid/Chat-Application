import sigr = require("@microsoft/signalr");
import { Message } from "../Chat/Message";
import { MessageValidator } from "../Validators/MessageValidator";

const connection = new sigr.HubConnectionBuilder().withUrl("/Chat/Index").build(); 

// TODO write comments to functions !!!!

class Chat {
    private validator: MessageValidator

    public Initialize() { 
        var self = this;
        self.validator = new MessageValidator();
        $(document).ready(function () {
            self.AttachEvents();
        });
    }

    public SendMessage(e: Event): void {
        var self = this;
        if (!self.validator.Validate('#message-text')) {
            e.preventDefault();
            return;
        }

        let message: Message = new Message
        (
            $('input[name=UserName]').val(),
            $('#message-text').val(),
            new Date(),
        );
        let controllerObject = { Text: message.Text, Username: message.Username, CreationTime: message.CreationTime.toISOString() };
        $.post('/Chat/SendMessage', { message: controllerObject })
            .done(function (data) {
                if (data === true) {
                    self.AddMessageToChat(message);
                    self.PostMessageReceival();
                    connection.invoke('sendMessage', message); // TODO SignalR not working
                } else {
                    console.log('something went wrong'); // TODO  HANDLE LATER ERRORS WITH DISPLAY (Done in controller?)
                }
            })
    }

    public AddMessageToChat(message: Message) {
        this.DrawMessage(message);
    }

    private ClearInputField() {
        $('#message-text').val('');
    }

    private AttachEvents() {
        var self = this;
        $('#send-message').on('click', function (e: Event) {
            self.SendMessage(e);
        });

        connection.on('newMessageReceived', function (message: Message) {
            self.DrawMessage(message);
            self.PostMessageReceival();
        });

        connection.on("newUserJoined", function (user: string) {
            self.NewUserJoined(user);
        });
    }

    private PostMessageReceival() {
        // dafuq why scroll to bottom is not working...
        this.ClearInputField();
    }

    private DrawMessage(message: Message) {
        let date = message.CreationTime;
        let month = ('0' + (date.getMonth() + 1)).slice(-2);
        let customDate =
            `${date.getUTCDate()}.${month}.${date.getUTCFullYear()} at ${date.toTimeString().split(' ')[0]}`;
        let mesg = 
        `<div class="chat-row chat-message-box">
                  <p class="chat-row">${customDate}</p>
                  <p class="chat-row">${message.Username}</p>
                  <div class="message">
                      <p class="chat-row">${message.Text}</p>
                  </div>
             </div>`;
        $('#message-container').append(mesg);
    }

    /**
     * Notifies chat, that new user has joined
     * @param user - user that joined
     */
    private NewUserJoined(user: string) {
        alert('new user joined'); // todo change later
    }
}

new Chat().Initialize();