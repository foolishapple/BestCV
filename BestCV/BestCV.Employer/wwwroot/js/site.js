/*
Author: DucNN
CreatedDate: 31/07/2023
*/

const httpService = new HttpService();

//check token available
function IsSignIn() {
    if (localStorage.currentLoggedInUserJobi) {
        return true
    }
    return false;
}

//load employer profile
function loadEmployerDetail() {
    if (IsSignIn()) {
        let employer = JSON.parse(localStorage.currentLoggedInUserJobi);
        $(".employer-name").text(employer.fullname);
        let photo = systemConfig.defaultStorage_URL + (employer.photo.startsWith("/")?"":"/")+ employer.photo;
        $(".employer-photo").css("background-image", `url('${photo}')`);
    }
    else {
        window.location.href = systemConfig.defaultURL + "/dang-nhap";
    }
}
$(".sign-out").click(function (e) {
    e.preventDefault();
    localStorage.clear();
    window.location.href = systemConfig.defaultURL + "/dang-nhap";
})
loadEmployerDetail();