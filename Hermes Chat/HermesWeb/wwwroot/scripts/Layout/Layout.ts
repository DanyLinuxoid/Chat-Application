export class Layout {
    public Initialize() {
        var self = this;
        $(document).ready(function () {
            self.AttachEvents();
        });
    }

    private AttachEvents(): void {
        var self = this;
        $("form :input").each(function () {
            let input = $(this);
            $(input).on('change', function (e: JQueryEventObject) { self.ShowHideErrorForEmptyField(e.target) })
        });
        $("form").each(function () {
            let form = $(this);
            let submitButton = $(form).find(':submit');
            $(submitButton).on('click', function (e: JQueryEventObject) { self.IfFormHasErrorsThenCancelSubmit(e, form) })
        });
        $("body").on('click', function (e: JQueryEventObject) { self.HandleGlobalClicks(e) })
        $("#acc-logout").on('click', function (e: JQueryEventObject) { self.Logout(e) })
    }

    private ShowHideErrorForEmptyField(field: Element): void { 
        let errorPlaceHolder;
        if ($(field).next().is('span')) {
            errorPlaceHolder = $(field).next();
        } else if ($(field).next().next().is('span')) {
            errorPlaceHolder = $(field).next().next(); 
        }

        if (errorPlaceHolder) {
            if (!$(field).val() && $(errorPlaceHolder).text()) {
                let errorMsg = $(errorPlaceHolder).text().toString().toLowerCase();
                if (errorMsg.indexOf("mandatory") ||
                    errorMsg.indexOf("cannot be empty")) {
                    $(errorPlaceHolder).show();
                }
            } else {
                $(errorPlaceHolder).hide();
            }
        }
    }

    private IfFormHasErrorsThenCancelSubmit(e: Event, form: JQuery<HTMLElement>): void {
        form.find("span").each(function () {
            let potentialErrorHolder = $(this);
            if (potentialErrorHolder.hasClass("field-validation-error") &&
                potentialErrorHolder.is(":visible")) {
                e.preventDefault();
            }
        })
    }

    private HandleGlobalClicks(e: Event) {
        let dropDown = $('.dropdown-content');

        if ($(e.target).hasClass('navbar-account-img')) {
            dropDown.is(':visible') ? dropDown.hide() : dropDown.show();
        }
        else {
            dropDown.hide();
        }
    }

    private Logout(e: Event): void {
        e.preventDefault();
        $.post('/Home/Logout')
            .done(function (response) {
                window.location = response.url;
            });
    }
}

new Layout().Initialize();