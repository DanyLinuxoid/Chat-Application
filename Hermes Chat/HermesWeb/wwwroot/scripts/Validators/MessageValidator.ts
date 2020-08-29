export class MessageValidator {
    IsValid(messageHolderId: string): boolean {
        let elem = $(messageHolderId);
        return elem.length != 0 && elem.length! > 500;
    }

    Validate(messageHolderId: string): boolean {
        let elem = $(messageHolderId);
        let errorHolder;
        if ($(elem).next().is('span')) {
            errorHolder = $(elem).next(); 
        }
        else if ($(elem).next().next().is('span')) {
            errorHolder = $(elem).next().next();
        }

        if ($(elem).val().toString().length > 500 && errorHolder) {
            $(errorHolder).text('Message length cannot be more than 500 characters!').show();
            return false;
        }

        return true;
    }
}