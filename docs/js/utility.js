"use strict";
window.Utility = {
    // utility functions
    scrollToBottom: function () {
        window.scrollTo(0, document.body.scrollHeight);
    },

    // jquery wrappers
    addClass: function (targetSelector, cssClass) {
        $(targetSelector).addClass(cssClass);
    },
    removeClass: function (targetSelector, cssClass) {
        $(targetSelector).removeClass(cssClass);
    }
};