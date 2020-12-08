import 'jquery-validation-unobtrusive';
import { url } from 'inspector';
import { log } from 'console';

export class FormScripts {
    public async HandleFormSubmit(formSelector: string, callbackOnValidComplete: any, displayErrors?: boolean): Promise<boolean> {
        let self = this;

        // Check if form is valid
        if (!this.IsValidForm(formSelector)) {
            return false;
        }

        // Submitting form
        let form = $(formSelector);
        let formData = new FormData();

        // Setting files (Other logic?)
        let input = document.getElementById('file-input');
        if (input instanceof HTMLInputElement && input.files[0]) {
            formData.append(input.files[0].name, input.files[0]);
        }

        // Setting form values
        $("[class*='input']").each(function () {
            let elem = $(this);
            formData.append(elem.attr("name"), elem.val().toString());
        });

        return await this.DoAjaxPost(form.attr('action'), form.attr('method'), formSelector, formData, displayErrors, callbackOnValidComplete);
    }

    public DoAjaxPost(
        url: string,
        method?: string,
        formSelector?: string,
        data?: any,
        displayErrors?: boolean,
        callbackOnValidComplete?: any): boolean {

        // Anti forgery
        var token = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers['RequestVerificationToken'] = token;

        let isValid = false;
        const self = this;

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
                    } else {
                        isValid = false;
                        if (displayErrors != false) {
                            // Displaying errors from server, which jquery validate cannot handle (mostly those that require server side check).
                            $.each(result.errors, (key, errors) => self.DisplayErrorsInFormWithId(formSelector, key, errors));
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
    }

    public PreviewImage(imageHolderSelector: string, image: any) {
        if (image) {
            let picHolder = $(imageHolderSelector);
            picHolder.attr('src', URL.createObjectURL(image));
            picHolder.on('load', function () {
                URL.revokeObjectURL(picHolder.attr('src'))
            });
        }
    }

    /**
     * Sometimes JQuery is not hiding some errors automatically.
     * @param formId - form identifier.
     */
    private HideErrorMessagesAfterValidSubmit(formId: string) {
        $(formId).find('.error-msg').hide();
    }
    private IsValidForm(formId: string): boolean {
        let form = $(formId);

        // Have to reparse validator, otherwise validate will not work on ajax loaded elements.
        form.removeData("validator");
        form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
        // Validate form => then check valid flag.
        form.validate();
        return form.valid();
    }

    private DisplayErrorsInFormWithId(formId, key, value) {
        $(formId)
            .find($(`span[data-valmsg-for="${value.item1.toString()}"]`))
            .text('')
            .append(() => {
                // Splitting errors into array
                let errorArray = value.item2.join().split('!,');
                let errorStringToDisplay: string = "";
                for (let i = 0; i < errorArray.length; i++) {
                    errorStringToDisplay += errorArray[i];
                    // Checking if it is not last element, not to add one more '!' char or next line.
                    if (errorArray[i + 1]) {
                        errorStringToDisplay += "!<br>";
                    }
                }
                return errorStringToDisplay;
            })
            .show();
    }
}