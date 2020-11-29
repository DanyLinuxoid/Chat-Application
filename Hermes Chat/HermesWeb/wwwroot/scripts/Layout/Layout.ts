export class Layout {
    public Initialize() {
        var self = this;
        $(document).ready(function () {
            self.AttachEvents();
        });
    }

    private AttachEvents(): void {
        var self = this;
        $("body").on('click', function (e: JQueryEventObject) { self.HandleGlobalClicks(e) })

        // Navigation bar
        $("#acc-manage").on('click', function (e: JQueryEventObject) { self.ManageAccount(e) })
        $("#acc-logout").on('click', function (e: JQueryEventObject) { self.Logout(e) })
    }

    private HandleGlobalClicks(e: Event): any {
        this.ShowHideUserOptionsDropdown(e);
    }

    private ShowHideUserOptionsDropdown(e: Event): void {
        let dropDown = $('.dropdown-content');
        $(e.target).hasClass('navbar-account-img')
            ? dropDown.is(':visible')
                ? dropDown.hide() : dropDown.show()
            : dropDown.hide();
    }

    private ManageAccount(e: Event): void {
        e.preventDefault();
        window.location.href = '/AccountManagement/Index';
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