(function () {
    "use strict";
    $("#LOADING").text("Loading Will's Tools");
    let loadingInterval = setInterval(function () {
        const $loading = $("#LOADING");
        if ($loading.length) {
            $("#LOADING").text($("#LOADING").text() + ".");
        } else {
            clearInterval(loadingInterval);
        }
    }, 250);

    setTimeout(function () {
        const $loading = $("#LOADING");
        if ($loading.length) {
            clearInterval(loadingInterval);
            $loading.text("This is taking too long. Ask Will why.");
            if (/iPad|iPhone|iPod/.test(navigator.platform) || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1)) {
                $loading.parent().addClass("p-3").append("Note: IOS is not supported yet. I'm developing this site with pre-released Microsoft software called Blazor Web Assembly. The official release date is in May, so you may have to wait until then. I know, the anticipation is probably killing you.");
            }
        }
    }, 10000)
})()