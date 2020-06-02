(function () {
    "use strict";

    /* constructor */
    let variations = [
        ["Loading", "Loading Will's", "Loading Will's Tools", "Loading", "Loading Will's", "Loading Will's Tools"],
        ["Loading Will's Tools", "Loading Will's Tools.", "Loading Will's Tools..", "Loading Will's Tools...", "Loading Will's Tools", "Loading Will's Tools.", "Loading Will's Tools..", "Loading Will's Tools..."],
        ["Loading Will's Tools", "Loading Will's", "Loading", "Loading Will's Tools", "Loading Will's", "Loading"],
        ["Loading Will's Tools...", "Loading Will's Tools.", "Loading Will's Tools.", "Loading Will's Tools", "Loading Will's Tools", "Loading Will's Tools...", "Loading Will's Tools.", "Loading Will's Tools.", "Loading Will's Tools", "Loading Will's Tools"],
        ["Is", "Is your", "Is your wifi", "Is your wifi down", "Is your wifi down?", "Is your wifi down?", "Is your wifi down?", "Is your wifi down??", "Is your wifi down???", "Is your wifi down????", "Is your wifi down?????"]
    ];
    const $image = document.querySelector("app a");
    let $loading = document.querySelector("#LOADING");
    $loading.textContent = "";
    let counter = 0;
    let variation = 0;

    /* main */
    const loadingInterval = setInterval(function () {
        if (!$loading.length) {
            // app loaded!
            clearInterval(loadingInterval);

            if (!localStorage.getItem("NO_ADS")) {
                document.querySelector("#AD_NATIVE_BANNER_CONTAINER").classList.remove("d-none");
                setTimeout(function () {
                    document.querySelector("#HIDE_ADS").classList.remove("d-none");
                }, 2000);
            }
        } else {
            let loadingText = variations[variation];

            if (counter === loadingText.length) {
                counter = 0;
                variation = ++variation % variations.length;

                if (variation === 0) {
                    document.querySelector("#AD_NATIVE_BANNER_CONTAINER").removeClass("d-none");
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