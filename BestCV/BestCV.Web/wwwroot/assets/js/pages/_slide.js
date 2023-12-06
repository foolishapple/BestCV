/**
 * Author: TUNGTD
 * Created: 14/08/2023
 * Description: Page slide java script
 */
"use strict";
var slideSource = [];
$(document).ready(async function (e) {
    await showSlide();
});

/**
 * Author: TUNGTD
 * Created: 14/08/2023
 * Description: Get slide show data
 */
async function getSlide() {
    try {
        let result = await httpService.getAsync("api/slide/list-on-homepage");
        if (result.isSucceeded) {
            slideSource = result.resources;
        }
        else {
            slideSource = [];
        }
    } catch (e) {
        slideSource = [];
        console.error(e);
    }
}

async function showSlide() {
    await getSlide();
    if (slideSource.length > 0) {
        slideSource.forEach(function (item) {
            $('.pxp_slide .slide_content').append(`<div class="item"><img class="rounded device-25-9" src="${systemConfig.defaultStorageURL+item.image}"/></div>`);
        });
        $('.pxp_slide .slide_content').owlCarousel({
            loop: true,

            items: 1,
            autoplayTimeout: 5000,
            autoplayHoverPause: true,
        });
        setTimeout(function () {
            $('.pxp_slide .d-none').removeClass("d-none");
        }, 1000);
        
    }
}