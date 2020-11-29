import { FormScripts } from "../Layout/FormScripts"

class AccountManagement {
    private _formScripts: FormScripts;

    public Initialize() {
        var self = this;
        $(document).ready(function () {
            self._formScripts = new FormScripts();
            self.AttachEvents();
        });
    }

    private AttachEvents(): void {
        var self = this;
        $('#profile-option').on('click', function() {
            self.LoadAccountDetails();
        });

        // Attaching events for dynamically loaded elements
        $(document).on('click', '#edit-account', function () {
            self.SetAccountDetailsEditable();
        });
        $(document).on('click', '#save-account', function (e) {
            e.preventDefault();
            self.UpdateAccountInfo();
        });
        $(document).on('change', '#file-input', function () {
            if (this && this.files && this.files[0]) {
                self._formScripts.PreviewImage('.profile-pic', this.files[0]);
            }
        });
    }

    private LoadAccountDetails(): void { 
        let elem = $('#profile-details');
        if (!elem.is(':hidden')) {
            return;
        }

        elem.load('/AccountManagement/GetCurrentAccountDetails').show();
        this.HideAllOtherWindowsExcept(elem.attr('id'));
    }

    private SetAccountDetailsEditable(): void { // Ugly...
        $('.profile-pic').addClass('uploadable-hover');
        $('#upload-pic').show();
        $('#acc-form-submit *').removeClass('disable');
        $('#edit-account').hide();
        $('#save-account').show();
    }

    private SetAccountDetailsNonEditable(): void { // Ugly...
        $('.profile-pic').removeClass('uploadable-hover');
        $('#upload-pic').hide();
        $('#nickname').addClass('disable');
        $('#phone-number').addClass('disable');
        $('#email').addClass('disable');
        $('#about-me').addClass('disable')
            .removeAttr('placeholder')
            .removeAttr('style');
        $('#edit-account').show();
        $('#save-account').hide();
    }

    private async UpdateAccountInfo() {
        let formId = '#acc-form-submit';
        await this._formScripts.HandleFormSubmit(formId, () =>
        {
            $('#info-updated').fadeIn("fast", function () { $(this).delay(500).fadeOut("fast"); }); // to notification logic (?)
            this.SetAccountDetailsNonEditable();
        });
    }

    private HideAllOtherWindowsExcept(id: string): void { // Ugly...
        let profileElem = $('#profile-details');
        let securityElem = $('#security-details');

        profileElem.attr('id') != id ? profileElem.hide() : null;
        securityElem.attr('id') != id ? securityElem.hide() : null; 
    }
}

new AccountManagement().Initialize();