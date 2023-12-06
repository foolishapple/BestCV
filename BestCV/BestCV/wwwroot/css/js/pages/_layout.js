"use strict";
var systemConfigSource = [];

$(document).ready(async function () {
    await getSystemConfig();
    await showSocialVideo();
    var isSignedIn = checkSignedInUser();
    if (isSignedIn) {
        var user = JSON.parse(localStorage.currentUser);
        $('.user-photo').attr('src', (user.photo != null && user.photo.trim().length > 0) ? systemConfig.defaultStorage_URL + user.photo : "/assets/images/avatar-1.jpg");
        $('#full-name').text(user.fullname);
        $('.btn-signin').remove();
    }

    $('.btnSignOut').on('click', function () {
        localStorage.clear();
        var url = window.location.href = '/';

        window.location.href = url;
    });
    $(document).trigger("tooltip");//show tool tip
})
function checkSignedInUser() {
    if (localStorage.currentUser) {
        $('.btn-signin').hide();
        $('.sign-in-profile').removeClass('d-none');
        return true;
    }
    else {
        $('.btn-signin').show();
        return false;
    }

}
async function getSystemConfig() {
    try {
        let result = await httpService.getAsync("api/system-config/list");
        if (result.isSucceeded) {
            systemConfigSource = result.resources;
        }
        else {
            systemConfigSource = [];
        }

    } catch (e) {
        systemConfigSource = [];
        console.error(e);
    }
}

function showSocialVideo() {
    let video = systemConfigSource.find(c => c.key == "CANDIDATE_HEADER_YOUTUBE_URL");
    if (video) {
        let linkytb = video.value.replace("watch?v=", "embed/");
        $("#social_ytb_video").attr("src", linkytb);
    }
}
//const Toast = Swal.mixin({
//    toast: true,
//    position: 'top-end',
//    showConfirmButton: false,
//    timer: 3000,
//    timerProgressBar: true,
//    didOpen: (toast) => {
//        toast.addEventListener('mouseenter', Swal.stopTimer)
//        toast.addEventListener('mouseleave', Swal.resumeTimer)
//    }
//})
function stringToSlug(text) {
    text = text.toLowerCase().trim();

    // Define a map of special characters and their replacements
    const specialChars = {
        'à': 'a',
        'á': 'a',
        'ả': 'a',
        'ã': 'a',
        'ạ': 'a',
        'ă': 'a',
        'ắ': 'a',
        'ằ': 'a',
        'ẳ': 'a',
        'ẵ': 'a',
        'ặ': 'a',
        'â': 'a',
        'ấ': 'a',
        'ầ': 'a',
        'ẩ': 'a',
        'ẫ': 'a',
        'ậ': 'a',
        'đ': 'd',
        'è': 'e',
        'é': 'e',
        'ẻ': 'e',
        'ẽ': 'e',
        'ẹ': 'e',
        'ê': 'e',
        'ế': 'e',
        'ề': 'e',
        'ể': 'e',
        'ễ': 'e',
        'ệ': 'e',
        'ì': 'i',
        'í': 'i',
        'ỉ': 'i',
        'ĩ': 'i',
        'ị': 'i',
        'ò': 'o',
        'ó': 'o',
        'ỏ': 'o',
        'õ': 'o',
        'ọ': 'o',
        'ô': 'o',
        'ố': 'o',
        'ồ': 'o',
        'ổ': 'o',
        'ỗ': 'o',
        'ộ': 'o',
        'ơ': 'o',
        'ớ': 'o',
        'ờ': 'o',
        'ở': 'o',
        'ỡ': 'o',
        'ợ': 'o',
        'ù': 'u',
        'ú': 'u',
        'ủ': 'u',
        'ũ': 'u',
        'ụ': 'u',
        'ư': 'u',
        'ứ': 'u',
        'ừ': 'u',
        'ử': 'u',
        'ữ': 'u',
        'ự': 'u',
        'ỳ': 'y',
        'ý': 'y',
        'ỷ': 'y',
        'ỹ': 'y',
        'ỵ': 'y',
        'ñ': 'n',
        'ç': 'c',
        'ß': 'ss',
        ' ': '-',
        '_': '-',
        '+': '-',
    };

    // Replace special characters with their counterparts
    text = text.replace(/[^a-z0-9-]/g, (char) => specialChars[char] || '');

    // Replace multiple consecutive hyphens with a single hyphen
    text = text.replace(/-+/g, '-');

    return text;
}
$(document).on("tooltip", function (e) {
    $("[data-bs-toggle=tooltip]").tooltip();
})