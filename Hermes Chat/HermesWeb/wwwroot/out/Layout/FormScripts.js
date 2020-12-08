"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.FormScripts = void 0;
require("jquery-validation-unobtrusive");
var FormScripts = /** @class */ (function () {
    function FormScripts() {
    }
    FormScripts.prototype.HandleFormSubmit = function (formSelector, callbackOnValidComplete, displayErrors) {
        return __awaiter(this, void 0, void 0, function () {
            var self, form, formData, input;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        self = this;
                        // Check if form is valid
                        if (!this.IsValidForm(formSelector)) {
                            return [2 /*return*/, false];
                        }
                        form = $(formSelector);
                        formData = new FormData();
                        input = document.getElementById('file-input');
                        if (input instanceof HTMLInputElement && input.files[0]) {
                            formData.append(input.files[0].name, input.files[0]);
                        }
                        // Setting form values
                        $("[class*='input']").each(function () {
                            var elem = $(this);
                            formData.append(elem.attr("name"), elem.val().toString());
                        });
                        return [4 /*yield*/, this.DoAjaxPost(form.attr('action'), form.attr('method'), formSelector, formData, displayErrors, callbackOnValidComplete)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    FormScripts.prototype.DoAjaxPost = function (url, method, formSelector, data, displayErrors, callbackOnValidComplete) {
        // Anti forgery
        var token = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers['RequestVerificationToken'] = token;
        var isValid = false;
        var self = this;
        // Do post
        $.ajax({
            type: 'post',
            headers: headers,
            method: method,
            url: url,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result) {
                    if (!result.hasOwnProperty('errors')) {
                        isValid = true;
                        self.HideErrorMessagesAfterValidSubmit(formSelector);
                    }
                    else {
                        isValid = false;
                        if (displayErrors != false) {
                            // Displaying errors from server, which jquery validate cannot handle (mostly those that require server side check).
                            $.each(result.errors, function (key, errors) { return self.DisplayErrorsInFormWithId(formSelector, key, errors); });
                        }
                    }
                }
            },
            error: function (result) {
                isValid = false;
                console.log(result);
                alert('Hoops, something went wrong'); // Change...
            },
            complete: function (result) {
                if (isValid && callbackOnValidComplete) {
                    callbackOnValidComplete(result);
                }
            }
        });
        return isValid;
    };
    FormScripts.prototype.PreviewImage = function (imageHolderSelector, image) {
        if (image) {
            var picHolder_1 = $(imageHolderSelector);
            picHolder_1.attr('src', URL.createObjectURL(image));
            picHolder_1.on('load', function () {
                URL.revokeObjectURL(picHolder_1.attr('src'));
            });
        }
    };
    /**
     * Sometimes JQuery is not hiding some errors automatically.
     * @param formId - form identifier.
     */
    FormScripts.prototype.HideErrorMessagesAfterValidSubmit = function (formId) {
        $(formId).find('.error-msg').hide();
    };
    FormScripts.prototype.IsValidForm = function (formId) {
        var form = $(formId);
        // Have to reparse validator, otherwise validate will not work on ajax loaded elements.
        form.removeData("validator");
        form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
        // Validate form => then check valid flag.
        form.validate();
        return form.valid();
    };
    FormScripts.prototype.DisplayErrorsInFormWithId = function (formId, key, value) {
        $(formId)
            .find($("span[data-valmsg-for=\"" + value.item1.toString() + "\"]"))
            .text('')
            .append(function () {
            // Splitting errors into array
            var errorArray = value.item2.join().split('!,');
            var errorStringToDisplay = "";
            for (var i = 0; i < errorArray.length; i++) {
                errorStringToDisplay += errorArray[i];
                // Checking if it is not last element, not to add one more '!' char or next line.
                if (errorArray[i + 1]) {
                    errorStringToDisplay += "!<br>";
                }
            }
            return errorStringToDisplay;
        })
            .show();
    };
    return FormScripts;
}());
exports.FormScripts = FormScripts;
//# sourceMappingURL=FormScripts.js.map