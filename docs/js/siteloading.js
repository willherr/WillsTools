(function () {
    "use strict";

    /* constructor */
    let variations = [
        ["Loading", "Loading Cassandra's", "Loading Cassandra's Cookbook", "Loading", "Loading Cassandra's", "Loading Cassandra's Cookbook"],
        ["Loading Cassandra's Cookbook", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook..", "Loading Cassandra's Cookbook...", "Loading Cassandra's Cookbook", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook..", "Loading Cassandra's Cookbook..."],
        ["Loading Cassandra's Cookbook", "Loading Cassandra's", "Loading", "Loading Cassandra's Cookbook", "Loading Cassandra's", "Loading"],
        ["Loading Cassandra's Cookbook...", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook", "Loading Cassandra's Cookbook", "Loading Cassandra's Cookbook...", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook.", "Loading Cassandra's Cookbook", "Loading Cassandra's Cookbook"],
        ["Is", "Is your", "Is your wifi", "Is your wifi down", "Is your wifi down?", "Is your wifi down?", "Is your wifi down?", "Is your wifi down??", "Is your wifi down???", "Is your wifi down????", "Is your wifi down?????"]
    ];
    const $image = document.querySelector("app a");
    let $loading = document.querySelector("#LOADING");
    $loading.textContent = "";
    let counter = 0;
    let variation = 0;
    const $container = document.querySelector("#AD_NATIVE_BANNER_CONTAINER");

    /* main */
    const loadingInterval = setInterval(function () {
        if (!$loading.length) {
            // app loaded!
            clearInterval(loadingInterval);

            if (!localStorage.getItem("NO_ADS")) {
                if ($container) {
                    $container.classList.remove("d-none");
                    setTimeout(function () {
                        document.querySelector("#HIDE_ADS").classList.remove("d-none");
                    }, 2000);
                }
            }
        } else {
            let loadingText = variations[variation];

            if (counter === loadingText.length) {
                counter = 0;
                variation = ++variation % variations.length;

                if (variation === 0) {
                    if ($container) {
                        $container.removeClass("d-none");
                    }
                    clearInterval(loadingInterval);
                    $loading.textContent = "";
                    $image.classList.remove("d-none");
                    $loading.appendChild($image);
                }
            }
            else {
                // animate
                $loading.textContent = loadingText[counter++];
            }
        }

        $loading = document.querySelector("#LOADING");
    }, 500);
})();