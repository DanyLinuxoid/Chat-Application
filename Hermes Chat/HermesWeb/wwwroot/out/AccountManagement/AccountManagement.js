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
var FormScripts_1 = require("../Layout/FormScripts");
var AccountManagement = /** @class */ (function () {
    function AccountManagement() {
    }
    AccountManagement.prototype.Initialize = function () {
        var self = this;
        $(document).ready(function () {
            self._formScripts = new FormScripts_1.FormScripts();
            self.AttachEvents();
        });
    };
    AccountManagement.prototype.AttachEvents = function () {
        var self = this;
        $('#profile-option').on('click', function () {
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
    };
    AccountManagement.prototype.LoadAccountDetails = function () {
        var elem = $('#profile-details');
        if (!elem.is(':hidden')) {
            return;
        }
        elem.load('/AccountManagement/GetCurrentAccountDetails').show();
        this.HideAllOtherWindowsExcept(elem.attr('id'));
    };
    AccountManagement.prototype.SetAccountDetailsEditable = function () {
        $('.profile-pic').addClass('uploadable-hover');
        $('#upload-pic').show();
        $('#acc-form-submit *').removeClass('disable');
        $('#edit-account').hide();
        $('#save-account').show();
    };
    AccountManagement.prototype.SetAccountDetailsNonEditable = function () {
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
    };
    AccountManagement.prototype.UpdateAccountInfo = function () {
        return __awaiter(this, void 0, void 0, function () {
            var formId;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        formId = '#acc-form-submit';
                        return [4 /*yield*/, this._formScripts.HandleFormSubmit(formId, function () {
                                $('#info-updated').fadeIn("fast", function () { $(this).delay(500).fadeOut("fast"); }); // to notification logic (?)
                                _this.SetAccountDetailsNonEditable();
                            })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    AccountManagement.prototype.HideAllOtherWindowsExcept = function (id) {
        var profileElem = $('#profile-details');
        var securityElem = $('#security-details');
        profileElem.attr('id') != id ? profileElem.hide() : null;
        securityElem.attr('id') != id ? securityElem.hide() : null;
    };
    return AccountManagement;
}());
new AccountManagement().Initialize();
//# sourceMappingURL=AccountManagement.js.map