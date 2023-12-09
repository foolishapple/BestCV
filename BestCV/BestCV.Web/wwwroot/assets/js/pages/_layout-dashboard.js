loadDataMenuDashboardCandidate();
function loadDataMenuDashboardCandidate() {
    var currenthref = window.location.pathname.toLowerCase();
    $("#nav-bar-pc").html('');
    var newDiv = `<div class="pxp-dashboard-side-label">Công cụ quản trị</div>`;
       
    $(newDiv).appendTo($("#nav-bar-pc"));

    let ul = `<ul class="list-unstyled menu-dashboard">`
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/menu/list-all-candidate",
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {
            let data = responseData;

            dataSource = data.Resources;
            dataSource.forEach(function (item, index) {
                if (item.ParentId == null || item.ParentId == 0) {
                    ul += `
                <li class="${currenthref === item.Link ? "pxp-active" : ""}"><a href="${item.Link != null ? item.Link : "#!"}"><span class="${item.Icon}"></span>${item.Name}</a></li>
            `;
                };
            });
            ul += `</ul>`;
            $(ul).appendTo($("#nav-bar-pc"));
        }
    })
}

$(document).ready(function () {
    $("[data-control=select2]").select2();
})