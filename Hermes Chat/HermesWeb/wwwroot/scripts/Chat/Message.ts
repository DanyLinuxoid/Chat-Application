// User message model
export class Message {
    public Text: string;
    public Username: string;
    public CreationTime: Date;

    constructor(username, text, creationTime) {
        this.Username = username;
        this.Text = text;
        this.CreationTime = creationTime;
    }
}
