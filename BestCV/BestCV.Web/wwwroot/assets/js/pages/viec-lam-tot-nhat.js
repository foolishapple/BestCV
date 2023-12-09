var pageIndex = 1;
$(document).ready(async function () {
    //console.log('a')

    $.when(await loadDataCity()).done(function () {
        loadDataJobExtra();
    })
    $("#btn-search-job").click(function (e) {
        e.preventDefault();
        window.location.href = "/tim-kiem-viec-lam?tukhoa=" + $("#search-all-job").val().trim() + "&diadiem=" + $("#search-location").val() + "&danhmuc=" + $("#search-category").val()
    })
    $("#selectOrder").on('change', function () {
        order = $(this).val();
        pageIndex = 1;
        loadDataJobExtra();
    })

    $("#selectOrder").select2({
        minimumResultsForSearch: Infinity,
    });

})




async function loadDataCity() {
    $("#search-location").empty();
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/workplace/list-city",
        type: "GET",
        success: function (responseData) {
            if (responseData.resources.length > 0) {
                $('#search-location').append(new Option("Tất cả tỉnh thành", 0, false, false)).trigger('change');

                responseData.resources.forEach(function (item) {
                    $('#search-location').append(new Option(item.name, item.id, false, false)).trigger('change');

                })
                $("#search-location").select2();
            }

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

function loadDataJobExtra() {
    var obj = {
        orderCriteria: $("#selectOrder").val(),
        pageSize: 15,
        pageIndex: pageIndex,
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/top-job-extra/searching-top-extra",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        beforeSend: function (xhr) {
            if (localStorage.currentUser) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
            }
        },
        success: function (responseData) {
            $("#loading").removeClass('show');
            dataSources = responseData.resources;
            dataSourceJob = responseData.resources.dataSource;
            var contentJob = "";
            $("#section-job").html("");
            if (dataSourceJob.length > 0) {
                dataSourceJob.forEach(function (item) {
                    //convert ve dang slug
                    let slugJob = stringToSlug(item.jobName);

                    //hiển thị tag city
                    let dataCity = "";
                    if (item.jobRequireCity.length > 1) {
                        for (let x = 0; x < 1; x++) {
                            dataCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                        }
                        dataCity += `<a class='badge-city'>+${item.jobRequireCity.length - 1}</a>`
                    }
                    else {
                        for (let x = 0; x < item.jobRequireCity.length; x++) {
                            dataCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                        }
                    }

                    //tim kiem trong list benefit
                    let dataNew = item.listBenefit.find(x => x.id == ListBenefit.GAN_TAG_NEW);
                    let dataRedTitle = item.listBenefit.find(x => x.id == ListBenefit.BOLD_AND_RED);
                    let dataJobManagement = item.listBenefit.find(x => x.id == ListBenefit.GAN_TAG_TOP_MANAGEMENT);
                    let dataJobTop = item.listBenefit.find(x => x.id == ListBenefit.GAN_TAG_TOP);
                    let dataJobUrgent = item.listBenefit.find(x => x.id == ListBenefit.GAN_TAG_URGENT)

                    contentJob += `<div class="pxp-jobs-card-3 pxp-has-border">
                        <div class="row align-items-center justify-content-between">
                            <div class="col-2 d-flex align-items-center justify-content-center">
                                <a href="/thong-tin-cong-ty/${item.companyId}" class="pxp-jobs-card-3-company-logo img-cover-fit" style="background-image: url(${item.companyLogo != null ? systemConfig.defaultStorageURL + item.companyLogo : ""});"></a>
                            </div>
                            <div class="col-9">
                                <div class="d-flex flex-column">
                                    <a href="/chi-tiet-cong-viec/${slugJob + "-" + item.jobId}" class="pxp-jobs-card-3-title mt-3 mt-sm-0 ${dataRedTitle != undefined ? "text-danger" : ""}">${dataNew != undefined && (moment() - moment(item.jobCreatedTime)) < dayToMilisecond ? newTag : ""}${item.jobName}</a>
                                    <div>
                                        ${dataJobManagement != undefined ? managementTag : ""}
                                        ${dataJobTop != undefined ? topTag : ""}
                                        ${dataJobUrgent != undefined ? urgentTag : ""}
                                    </div>
                                </div>

                                <div class="pxp-jobs-card-3-details d-flex flex-column">
                                    <a href="/thong-tin-cong-ty/${item.companyId}" class="">${item.companyName}</a>
                                    <div class='mt-2 mb-2'>
                                    <span class='text-color-money fw-bold'>${item.salaryTypeId == salaryFrom ? (item.salaryFrom != null ? "Từ " + formatNumber(item.salaryFrom.toString()) : 0) : (item.salaryTypeId == salaryTo ? (item.salaryTo != null ? "Đến " + formatNumber(item.salaryTo.toString()) : 0) : (item.salaryTypeId == salaryBetween ? formatNumber(item.salaryFrom.toString()) + " - " + formatNumber(item.salaryTo.toString()) : "Thỏa thuận"))}</span> | <span class="pxp-jobs-card-3-date pxp-text-light">${timeAgo(item.jobCreatedTime)}</span></div>
                                </div>
                                    <div>
                                        <span class='hash-tag ${item.applyEndDate == null ? "d-none" : ""} ${countRemainDay(item.applyEndDate) == 0 ? "hash-tag-expired" : " "}'>${item.applyEndDate != null ? (countRemainDay(item.applyEndDate) > 0 ? "Còn <b>" + countRemainDay(item.applyEndDate) + "</b> ngày để ứng tuyển" : "<span class='text-danger'>Đã hết hạn ứng tuyển</span>") : ""} </span>${dataCity}
                                    </div>

                            </div>
                            <div class="col-1">
                                <a class="rbt-round-btn btn-save-job" title="Bookmark" href="#!" data-job-id='${item.jobId}' data-job-name='${item.jobName}'>
                                    <div class="${item.isSaveJob ? "in-wishlist-2" : "icon-wishlist"}"></div>
                                </a>
                            </div>
                    
                        </div>
                    </div>`
                })
            }
            else {
                contentJob += `<div class='text-center mt-3'>Không có dữ liệu để hiển thị</div>`
            }
            $("#section-job").html(contentJob);
            $("#countJob").html("Tìm kiếm cơ hội nghề nghiệp của bạn thông qua <strong>" + dataSources.total + "</strong> việc làm")
            pageIndex = dataSources.currentPage;
            initPagination(dataSources.totalPages, '.pxp-pagination');

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}
function countRemainDay(input) {
    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    var firstDate = new Date();
    var secondDate = new Date(input);

    var diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
    return diffDays;
}
function timeAgo(input) {
    const date = (input instanceof Date) ? input : new Date(input);
    const formatter = new Intl.RelativeTimeFormat('vi');
    const ranges = {
        years: 3600 * 24 * 365,
        months: 3600 * 24 * 30,
        weeks: 3600 * 24 * 7,
        days: 3600 * 24,
        hours: 3600,
        minutes: 60,
        seconds: 1
    };
    const secondsElapsed = (date.getTime() - Date.now()) / 1000;
    for (let key in ranges) {
        if (ranges[key] < Math.abs(secondsElapsed)) {
            const delta = secondsElapsed / ranges[key];
            return formatter.format(Math.round(delta), key);
        }
    }
}
function initPagination(totalPage, element) {
    if (totalPage > 0) {
        let html = "";
        let startPage;
        if (totalPage <= 3) {
            startPage = 1;
        }
        else {
            if (totalPage == pageIndex) {
                startPage = totalPage - 2;
            }
            else {
                startPage = pageIndex == 1 ? 1 : pageIndex - 1;
            }

        }
        let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
        if (pageIndex > 1) {
            html += `<li class="page-item paging-first-item"><a href="#!" aria-label="Previous"  class="page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a href="#!" aria-label="Previous"  class="page-link"><i class="fa fa-angle-left"></i></a></li>`;
        }
        for (var i = startPage; i <= endPage; i++) {
            if (i > 0) {
                html += `<li class="page-item ${i == pageIndex ? 'active' : ''}" aria-current="page">
                                    <a class="page-link">${i}</a>
                                </li>`
            }
        }
        if (pageIndex < totalPage) {
            html += `<li class="page-item paging-next"><a href="#!" aria-label="Next"  class="page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a href="#!" aria-label="Next"  class="page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
        }
        $(element).html(html);
    }
    else {
        $(element).html("");
    }

}

$("#pagination-job").on("click", ".page-item", function (e) {
    e.preventDefault();
    window.scrollTo(0, 300);

    if ($(this).hasClass('paging-first-item')) {
        pageIndex = 1;
        loadDataJobExtra();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSources.totalPages;
        loadDataJobExtra();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        loadDataJobExtra();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        loadDataJobExtra();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass("active");
            $(this).addClass("active");
            pageIndex = parseInt($(this).text());
            loadDataJobExtra();
        }
    }
});

$("body").on('click', '.btn-save-job', function () {
    if (localStorage.currentUser) {
        let jobId = $(this).attr("data-job-id");
        let jobName = $(this).attr("data-job-name");
        $.ajax({
            url: systemConfig.defaultAPIURL + "api/candidate-save-job/save-job/" + jobId,
            type: "Post",
            contentType: 'application/json',
            beforeSend: function (xhr) {
                if (localStorage.currentUser) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
                }
            },
            success: function (responseData) {
                //$("[data-job-id=" + jobId + "] i").unbind('mouseover');
                if (responseData.isSucceeded) {
                    if (responseData.message === "Deleted") {

                        $('[data-job-id=' + jobId + '] div').toggleClass('icon-wishlist');
                        $('[data-job-id=' + jobId + '] div').addClass('icon-wishlist');
                        $('[data-job-id=' + jobId + '] div').removeClass('in-wishlist-2');
                        $('[data-job-id=' + jobId + '] div').removeClass('in-wishlist');

                    }
                    else {

                        $('[data-job-id=' + jobId + '] div').toggleClass('in-wishlist');
                        $('[data-job-id=' + jobId + '] div').addClass('in-wishlist-2');
                        $('[data-job-id=' + jobId + '] div').addClass('in-wishlist');

                    }
                }
            },
            error: function (res) {
                if (res.status == 401) {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Lưu ý',
                        html: 'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại!',
                        focusConfirm: true,
                        allowEnterKey: true
                    });
                }
            }
        })
    } else {
        Swal.fire({
            icon: 'warning',
            title: 'Lưu ý',
            html: 'Bạn chưa đăng nhập, vui lòng đăng nhập để sử dụng tính năng này!',
            focusConfirm: true,
            allowEnterKey: true
        });
    }
})