"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Layout = void 0;
var Layout = /** @class */ (function () {
    function Layout() {
    }
    Layout.prototype.Initialize = function () {
        var self = this;
        $(document).ready(function () {
            self.AttachEvents();
        });
    };
    Layout.prototype.AttachEvents = function () {
        var self = this;
        $("form :input").each(function () {
            var input = $(this);
            $(input).on('change', function (e) { self.ShowHideErrorForEmptyField(e.target); });
        });
        $("form").each(function () {
            var form = $(this);
            var submitButton = $(form).find(':submit');
            $(submitButton).on('click', function (e) { self.IfFormHasErrorsThenCancelSubmit(e, form); });
        });
        $("body").on('click', function (e) { self.HandleGlobalClicks(e); });
        $("#acc-logout").on('click', function (e) { self.Logout(e); });
    };
    Layout.prototype.ShowHideErrorForEmptyField = function (field) {
        var errorPlaceHolder;
        if ($(field).next().is('span')) {
            errorPlaceHolder = $(field).next();
        }
        else if ($(field).next().next().is('span')) {
            errorPlaceHolder = $(field).next().next();
        }
        if (errorPlaceHolder) {
            if (!$(field).val() && $(errorPlaceHolder).text()) {
                var errorMsg = $(errorPlaceHolder).text().toString().toLowerCase();
                if (errorMsg.indexOf("mandatory") ||
                    errorMsg.indexOf("cannot be empty")) {
                    $(errorPlaceHolder).show();
                }
            }
            else {
                $(errorPlaceHolder).hide();
            }
        }
    };
    Layout.prototype.IfFormHasErrorsThenCancelSubmit = function (e, form) {
        form.find("span").each(function () {
            var potentialErrorHolder = $(this);
            if (potentialErrorHolder.hasClass("field-validation-error") &&
                potentialErrorHolder.is(":visible")) {
                e.preventDefault();
            }
        });
    };
    Layout.prototype.HandleGlobalClicks = function (e) {
        var dropDown = $('.dropdown-content');
        if ($(e.target).hasClass('navbar-account-img')) {
            dropDown.is(':visible') ? dropDown.hide() : dropDown.show();
        }
        else {
            dropDown.hide();
        }
    };
    Layout.prototype.Logout = function (e) {
        e.preventDefault();
        $.post('/Home/Logout')
            .done(function (response) {
            window.location = response.url;
        });
    };
    return Layout;
}());
exports.Layout = Layout;
new Layout().Initialize();
//# sourceMappingURL=Layout.js.map