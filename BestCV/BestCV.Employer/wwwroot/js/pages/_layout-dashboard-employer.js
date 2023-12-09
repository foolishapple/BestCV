loadDataMenuDashboardEmployer();
function loadDataMenuDashboardEmployer() {
    var currenthref = window.location.pathname.toLowerCase();
    $("#nav-bar-pc").html('');
    var newDiv = `<div class="pxp-dashboard-side-label">Quản trị</div>`;

    $(newDiv).appendTo($("#nav-bar-pc"));

    let ul = `<ul class="list-unstyled menu-dashboard">`

    $.ajax({
        url: systemConfig.defaultAPI_URL + "api/menu/list-all-employer",
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
countServicePackageInCart();
//count service package in cart
async function countServicePackageInCart() {
    try {
        let result = await httpService.getAsync("api/employer-cart/count");
        //console.log(result)
        $("#count-service-package").removeClass('d-none')

        $("#count-service-package").text(result);
        if (result == 0) {
            $("#count-service-package").addClass('d-none')
        }
    } catch (e) {
        console.error(e);
    }
}

const miniToast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})



