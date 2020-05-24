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
    const $image = $("app a");
    const $loading = $("#LOADING").text('');
    let counter = 0;
    let variation = 0;

    /* main */
    let loadingInterval = setInterval(async function () {

        if (!$loading.length) {
            clearInterval(loadingInterval);
        } else {
            let loadingText = variations[variation];

            if (counter === loadingText.length) {
                counter = 0;
                variation = ++variation % variations.length;

                if (variation === 0) {
                    clearInterval(loadingInterval);
                    $loading.text('').append($image.removeClass("d-none"));
                }
            }
            else {
                // animate
                $loading.text(loadingText[counter++]);
            }
        }
    }, 500);

/* private functions */
    function sleep(time) {
        return new Promise(resolve => setTimeout(resolve, time));
    }
})();