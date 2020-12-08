import SignalR = require("@microsoft/signalr");
import { Message } from "../Chat/Message";
import 'jquery-validation-unobtrusive';
import { FormScripts } from "../Layout/FormScripts"

const connection = new SignalR.HubConnectionBuilder().withUrl("/Chat/Index").build();

export class Chat {
    private currentUserUsername: string;
    private _formScripts: FormScripts;

    public Initialize() { 
        var self = this;
        $(document).ready(function () {
            connection.start();
            self._formScripts = new FormScripts();
            self.AttachEvents();
            self.AnimateScrollToMessageHistoryEnd();
            self.currentUserUsername = $('input[name=UserName]').val().toString();
        });
    }

    public SendMessage(formId: string): void {
        var self = this;
        let message: Message = new Message();

        // Values are handled on server, but it is needed for signalR
        message.CreationTime = new Date();
        message.Username = this.currentUserUsername;
        message.Text = $('#message-text').val().toString();
        let callbackOnSuccess = () => {
            connection.invoke('SendMessageAsync', message);
            self.PostMessageReceival();
        }
        this._formScripts.HandleFormSubmit(formId, callbackOnSuccess);
    }

    private ClearInputField() {
        $('#message-text').val('');
    }

    private AttachEvents() {
        var self = this;
        $('#send-message').on('click', function (e) {
            self.SendMessage('.chat-form');
        });

        connection.on("receiveMessage", function (message: any) {
            self.DrawMessage(message);
            self.AnimateScrollToMessageHistoryEnd();
        });
    }

    private PostMessageReceival() {
        this.ClearInputField();
        this.AnimateScrollToMessageHistoryEnd();
    }

    public DrawMessage(message: any) {
        const isMirrored = message.username == this.currentUserUsername;
        let messageHtml =
            `<ul class="${isMirrored ? "mirror" : null} max-height list-row">
                    <li class="left-pad-top">
                        <img class='person-avatar-M' src='data:image;base64,${message.accountImageData}' />
                    </li>
                    <li class="photo-message-delimeter"></li>
                    <li class="chat-row chat-message-box">
                        <p class="chat-row ${isMirrored ? "non-mirror" : null}">${this.GetFormattedSendingTimeForMessage()}</p>
                        <p class="chat-row ${isMirrored ? "non-mirror" : null}">${message.username}</p>
                        <p class="chat-row  ${isMirrored ? "non-mirror" : null} message">${message.text}</p>
                    </li>
             </ul>`;
        let messageContainer = $('#message-rows');
        messageContainer.append(messageHtml);
    }

    private AnimateScrollToMessageHistoryEnd() {
        const messages = document.getElementById('message-rows');
        $(messages).stop().animate({
            scrollTop: `+=${messages.scrollHeight}` // Potentially can cause strange scrolling behavior if too many messages
        }, 400);
    }

    private GetFormattedSendingTimeForMessage() {
        let creationTime = new Date();
        let dayWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getDate());
        let monthWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getMonth() + 1);

        let hourWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getHours());
        let minutesWithLeadingZeros = this.GetFormattedDateTimeWithLeadingZeros(creationTime.getMinutes());

        let customTime = `${hourWithLeadingZeros}:${minutesWithLeadingZeros}`;
        let customDate = `${dayWithLeadingZeros}.${monthWithLeadingZeros}.${creationTime.getUTCFullYear()}`;

        return `${customDate} at ${customTime}`
    }

    private GetFormattedDateTimeWithLeadingZeros(time: number) {
        return ('0' + time).slice(-2);
    }
}

new Chat().Initialize();