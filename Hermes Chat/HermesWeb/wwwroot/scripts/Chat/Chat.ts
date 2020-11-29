import SignalR = require("@microsoft/signalr");
import { Message } from "../Chat/Message";
import 'jquery-validation-unobtrusive';
import { FormScripts } from "../Layout/FormScripts"

const connection = new SignalR.HubConnectionBuilder().withUrl("/Chat/Index").build();  /// SIGNALR TO OTHER LOGIC!!!!!!!!!!!!!!!!!!

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


class Chat {
    private messageHeight: number = 115;
    private _formScripts: FormScripts;
    private currentUserUsername: string;

    public Initialize() { 
        var self = this;
        $(document).ready(function () {
            self._formScripts = new FormScripts();
            self.AttachEvents();
            connection.start();
            self.ScrollToBottom();
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
            self.FormatAndDrawMessage(message);
        });
    }

    private PostMessageReceival() {
        this.ClearInputField();
        this.AnimateScrollToNewMessage();
    }

    private FormatAndDrawMessage(message: any): void {
        const isMirrored = message.username == this.currentUserUsername;
        this.DrawMessage(message, isMirrored);
    }

    private AnimateScrollToNewMessage() {
        const messages = document.getElementById('message-container');
        $(messages).stop().animate({
            scrollTop: `+=${this.messageHeight}`
        }, 100);
    }

    private ScrollToBottom() {
        const messages = document.getElementById('message-container');
        messages.scrollTop = messages.scrollHeight;
    }

    private DrawMessage(message: any, mirrored?: boolean) { // to message logic
        let messageHtml = 
            `<ul class="${mirrored ? "mirror" : null} list-row">
                    <li class="left-pad-top">
                        <img class='person-avatar-M' src='data:image;base64,${message.accountImageData}' />
                    </li>
                    <li class="photo-message-delimeter"></li>
                    <li class="chat-row chat-message-box">
                        <p class="chat-row ${mirrored ? "non-mirror" : null}">${this.GetFormattedSendingTimeForMessage()}</p>
                        <p class="chat-row ${mirrored ? "non-mirror" : null}">${message.username}</p>
                        <p class="message chat-row ${mirrored ? "non-mirror" : null}">${message.text}</p>
                    </li>
             </ul>`;
        let messageContainer = $('#message-rows');
        messageContainer.append(messageHtml);
        this.AnimateScrollToNewMessage();
    }

    private GetFormattedSendingTimeForMessage() {
        let creationTime = new Date();
        let month = ('0' + (creationTime.getMonth() + 1)).slice(-2);
        let currentHour = creationTime.getHours();

        // getHours() returns hours in format 1.11 if one digit, have to check and add 0 manually if this is the case.
        let customTime = `${currentHour.toString().length == 1 ? "0" + currentHour : currentHour}:${creationTime.getMinutes()}`;
        let customDate = `${creationTime.getUTCDate()}.${month}.${creationTime.getUTCFullYear()}`;
        return `${customDate} at ${customTime}`
    }
}

new Chat().Initialize();