"use strict";
window.Utility = {
    scrollToBottom: function () {
        window.scrollTo(0, document.body.scrollHeight);
    },
    addClass: function (targetSelector, cssClass) {
        document.querySelectorAll(targetSelector).forEach(node => node.classList.add(cssClass));
        document.querySelectorAll(targetSelector).forEach(node => node.classList.add(cssClass));
    },
    removeClass: function (targetSelector, cssClass) {
        document.querySelectorAll(targetSelector).forEach(node => node.classList.remove(cssClass));
    },
    copyText: function (text) {
        return navigator.clipboard.writeText(text).catch(_ => false);
    }
};