import 'jquery-validation-unobtrusive';

export class FormScripts {
    public async HandleFormSubmit(formSelector: string, callbackOnValidComplete: any, displayErrors?: boolean): Promise<boolean> {
        let self = this;
        let isValid = false;

        // Check if form is valid
        if (!this.IsValidForm(formSelector)) {
            return isValid;
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

        $.ajax({
            type: 'post',
            method: form.attr('method'),
            url: form.attr('action'),
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result) {
                    if (!result.hasOwnProperty('errors')) {
                        isValid = true;
                    } else {
                        isValid = false;
                        if (displayErrors != false) {
                            // Displaying errors from server, which jquery validate cannot handle (mostly those that require server side check).
                            $.each(result.errors, (key, errors) => self.DisplayErrorsInFormWithId(formSelector, key, errors));
                        }
                    }
                }
            },
            error: function () {
                isValid = false;
                alert('Hoops, somethig went wrong'); // Change...
            },
            complete: function () {
                if (isValid && callbackOnValidComplete) {
                    callbackOnValidComplete();
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