"use strict";
window.MainLayout = {
    initialize: function () {
        $('.dropdown').on('hide.bs.dropdown', function (e) {
            if (e.clickEvent && e.clickEvent.target) {
                if ($(e.clickEvent.target).is("button") && $(e.clickEvent.target).parent(".dropdown-menu").length) {
                    return false;
                }
            }
            return true;
        });
    }
};