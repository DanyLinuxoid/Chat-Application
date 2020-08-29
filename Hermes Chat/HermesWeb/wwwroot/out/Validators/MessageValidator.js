"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MessageValidator = void 0;
var MessageValidator = /** @class */ (function () {
    function MessageValidator() {
    }
    MessageValidator.prototype.IsValid = function (messageHolderId) {
        var elem = $(messageHolderId);
        return elem.length != 0 && elem.length > 500;
    };
    MessageValidator.prototype.Validate = function (messageHolderId) {
        var elem = $(messageHolderId);
        var errorHolder;
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
    };
    return MessageValidator;
}());
exports.MessageValidator = MessageValidator;
//# sourceMappingURL=MessageValidator.js.map