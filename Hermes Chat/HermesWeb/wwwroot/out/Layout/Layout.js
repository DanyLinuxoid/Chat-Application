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
        $("body").on('click', function (e) { self.HandleGlobalClicks(e); });
        // Navigation bar
        $("#acc-manage").on('click', function (e) { self.ManageAccount(e); });
        $("#acc-logout").on('click', function (e) { self.Logout(e); });
    };
    Layout.prototype.HandleGlobalClicks = function (e) {
        this.ShowHideUserOptionsDropdown(e);
    };
    Layout.prototype.ShowHideUserOptionsDropdown = function (e) {
        var dropDown = $('.dropdown-content');
        $(e.target).hasClass('navbar-account-img')
            ? dropDown.is(':visible')
                ? dropDown.hide() : dropDown.show()
            : dropDown.hide();
    };
    Layout.prototype.ManageAccount = function (e) {
        e.preventDefault();
        window.location.href = '/AccountManagement/Index';
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