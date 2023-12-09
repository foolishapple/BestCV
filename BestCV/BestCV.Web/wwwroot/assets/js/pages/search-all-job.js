$(document).ready(async function () {
    //console.log('a')

    $.when(await loadDataJobCategory(), await loadDataCity()).done(function () {
        $("#search-all-job").val(searchKeyword);
        $("#search-location").val(searchLocation).trigger('change');
        $("#search-category").val(searchCategory).trigger('change');
        loadDataJob();
        loadDataFilter();
    })
    $("#btn-search-job").click(function (e) {
        e.preventDefault();
        pageIndex = 1;
        $("#loading").addClass('show');
        loadDataJob();
    })

    $("#search-category").on('change', function () {
        searchCategory = $(this).val();
    })

    $("#selectOrder").on('change', function () {
        order = $(this).val();
        pageIndex = 1;
        $("#loading").addClass('show');
        loadDataJob();
    })

    $("#selectOrder").select2({
        minimumResultsForSearch: Infinity,
    });
    $("#search-location").select2({
        width: "resolve"
    });
    $("#search-category").select2({
        width: "resolve"
    });
})
var order;
var dataFilter = [];
var pageIndex = 1;
function loadDataFilter() {
    
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job/filter-job",
        type: "GET",
        success: function (responseData) {
            //console.log(responseData.resources)
            dataFilter = responseData.resources;
            $("#filterJobType,#filterJobExperience,#filterJobSalary").html('');
            var contentJobType = "", contentJobExperience = "", contentJobSalary = "", contentJobPosition = "";
            dataFilter.jobTypeData.forEach(function (item) {
                contentJobType += `<label class="list-group-item d-flex justify-content-between align-items-center mb-lg-2">
                                        <span class="d-flex">
                                            <input class="form-check-input me-2 job-type-list-check-box" type="checkbox" value="${item.id}">
                                            ${item.name}
                                        </span>
                                        <span class="badge rounded-pill">${item.countJob}</span>
                                    </label>`
            })

            dataFilter.jobExperience.forEach(function (item) {
                contentJobExperience += `<label class="list-group-item d-flex justify-content-between align-items-center mb-lg-2">
                                                    <span class="d-flex">
                                                        <input class="form-check-input me-2 job-experience-list-check-box" type="checkbox" value="${item.id}">
                                                        ${item.name}
                                                    </span>
                                                    <span class="badge rounded-pill">${item.countJob}</span>
                                                </label>`
            })

            dataFilter.jobSalary.forEach(function (item) {
                contentJobSalary += `<label class="list-group-item d-flex justify-content-between align-items-center mb-lg-2">
                                                <span class="d-flex">
                                                    <input class="form-check-input me-2 job-salary-list-check-box" type="checkbox" value="${item.id}">
                                                    ${item.name}
                                                </span>
                                                
                                            </label>`
            })

            dataFilter.jobPrimaryPosition.forEach(function (item) {
                contentJobPosition += `<label class="list-group-item d-flex justify-content-between align-items-center mb-lg-2">
                                                <span class="d-flex">
                                                    <input class="form-check-input me-2 job-position-list-check-box" type="checkbox" value="${item.id}">
                                                    ${item.name}
                                                </span>
                                                <span class="badge rounded-pill">${item.countJob}</span>
                                            </label>`
            })
            //< span class="badge rounded-pill" > ${ item.countJob }</span >
            $("#filterJobType").html(contentJobType)
            $("#filterJobExperience").html(contentJobExperience)
            $("#filterJobSalary").html(contentJobSalary)
            $("#filterJobPosition").html(contentJobPosition)

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}
var dataSources = [];
var dataSourceJob = [];
function loadDataJob() {
    
    var obj = {
        keywords: $("#search-all-job").val(),
        location: $("#search-location").val(),
        orderCriteria: $("#selectOrder").val(),
        jobCategoryId: $("#search-category").val(),
        jobType: arrJobType.toString(),
        jobExperience: arrJobExperience.toString(),
        salaryRange: arrJobSalary.toString(),
        pageIndex: pageIndex,
        pageSize: 20,
        jobPosition: arrJobPosition.toString(),
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job/searching-job",
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
            //window.scrollTo(0, 0);
            window.history.pushState(null, null, "/tim-kiem-viec-lam?tukhoa=" + $("#search-all-job").val() + "&diadiem=" + $("#search-location").val() + "&danhmuc=" + $("#search-category").val())
            $("#loading").removeClass('show');
            dataSources = responseData.resources;
            dataSourceJob = responseData.resources.dataSource;
            $("#pageInfo").text(responseData.resources.totalFiltered + " việc làm phù hợp");
            var contentJob = "";
            $("#section-job").html("");
            if (dataSourceJob.length > 0) {
                dataSourceJob.forEach(function (item) {
                    var contentCity = "";
                    let slugJob = stringToSlug(item.name);
                    if (item.jobRequireCity.length > 0) {
                        let itemCity = item.jobRequireCity[0];
                        contentCity += `<a class='hash-tag' data-bs-toggle="tooltip" data-bs-placement="top" title="${itemCity.cityName}" value-id='${itemCity.id}'>${itemCity.cityName.replace("Thành phố ", "") + (item.jobRequireCity.length > 1 ? ` và ${item.jobRequireCity.length-1} nơi khác`:``)}</a>`
                    }
                    let dataNew = item.listBenefit.find(x => x == ListBenefit.GAN_TAG_NEW);
                    let dataRedTitle = item.listBenefit.find(x => x == ListBenefit.BOLD_AND_RED);
                    let dataJobManagement = item.listBenefit.find(x => x == ListBenefit.GAN_TAG_TOP_MANAGEMENT);
                    let dataJobTop = item.listBenefit.find(x => x == ListBenefit.GAN_TAG_TOP);
                    let dataJobUrgent = item.listBenefit.find(x => x == ListBenefit.GAN_TAG_URGENT)

                    contentJob += `<div class="pxp-jobs-card-3 jbi-card-2">
                        <div class="row align-items-center justify-content-between">
                            <div class="col-2 d-flex align-items-center justify-content-center img-div">
                                <a href="/job-company-best/${item.companyId}" class="pxp-jobs-card-3-company-logo" style="background-image: url(${item.companyLogo != null ? systemConfig.defaultStorageURL + item.companyLogo : ""});"></a>
                            </div>
                            <div class="col-9">
                                <a href="/chi-tiet-cong-viec/${slugJob + "-" + item.id}" class="pxp-jobs-card-3-title mt-3 mt-sm-0 ${dataRedTitle != undefined ? "text-danger" : ""}" data-bs-toggle="tooltip" data-bs-placement="top" title="${item.name}" ><div class="${dataNew != undefined && (moment() - moment(item.createdTime)) < dayToMilisecond ? "mb-1" : ""}">${dataNew != undefined && (moment() - moment(item.refreshDate == null ? item.createdTime : item.refreshDate)) < dayToMilisecond ? newTag : ""}${item.name}</div></a>
                                <div>
                                    ${dataJobManagement != undefined ? managementTag : ""}
                                    ${dataJobTop != undefined ? topTag : ""}
                                    ${dataJobUrgent != undefined ? urgentTag : ""}
                                </div>
                                <div class="pxp-jobs-card-3-details d-flex flex-column mt-0">
                                    <a href="/job-company-best/${item.companyId}" class="">${item.companyName}</a>
                                    <div class='mt-2 mb-2'>
                                    <span class='text-color-money fw-bold'>${item.salaryTypeId == salaryFrom ? (item.salaryFrom != null ? "Từ " + formatNumber(item.salaryFrom.toString()) : 0) : (item.salaryTypeId == salaryTo ? (item.salaryTo != null ? "Đến " + formatNumber(item.salaryTo.toString()) : 0) : (item.salaryTypeId == salaryBetween ? formatNumber(item.salaryFrom.toString()) + " - " + formatNumber(item.salaryTo.toString()) : item.salaryTypeName))}</span> | <span class="pxp-jobs-card-3-date pxp-text-light">${timeAgo(item.refreshDate == null ? item.createdTime : item.refreshDate)}</span></div>
                                </div>
                                    <div>
                                        <span class='hash-tag ${item.applyEndDate == null ? "d-none" : ""} ${countRemainDay(item.applyEndDate) == 0 ? "hash-tag-expired" : " "}'>${item.applyEndDate != null ? (countRemainDay(item.applyEndDate) > 0 ? "Còn <b>" + countRemainDay(item.applyEndDate) + "</b> ngày để ứng tuyển" : "<span class='text-danger'>Đã hết hạn ứng tuyển</span>") : ""} </span>${contentCity}<a class='hash-tag'>${item.primaryJobPositionName}</a>
                                    </div>

                            </div>
                            <div class="col-1">
                                <a class="rbt-round-btn btn-save-job d-flex justify-content-center" data-bs-toggle="tooltip" data-bs-placement="top" title="${item.isSaveJob ? "Bỏ theo dõi" : "Theo dõi"}"  href="#!" data-job-id='${item.id}' data-job-name='${item.name}'>
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
            $(document).trigger("tooltip");
        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

//checkbox jobtype
var arrJobType = [];
$(".pxp-list-side-filter-panel").on("change", ".job-type-list-check-box", function () {
    $("#loading").addClass('show');
    if ($(this).is(":checked")) {
        arrJobType.push($(this).attr("value"));
        $(this).parent().parent().addClass('pxp-checked')
    }
    else {
        arrJobType = arrJobType.filter(x => x != $(this).attr("value"));
        $(this).parent().parent().removeClass('pxp-checked')
    }
    pageIndex = 1;
    loadDataJob();
})

//checkbox job experience
var arrJobExperience = [];
$(".pxp-list-side-filter-panel").on("change", ".job-experience-list-check-box", function () {
    $("#loading").addClass('show');
    if ($(this).is(":checked")) {
        arrJobExperience.push($(this).attr("value"));
        $(this).parent().parent().addClass('pxp-checked')
    }
    else {
        arrJobExperience = arrJobExperience.filter(x => x != $(this).attr("value"));
        $(this).parent().parent().removeClass('pxp-checked')
    }
    pageIndex = 1;

    loadDataJob();
})

//checkbox jobSalary
var arrJobSalary = [];
$(".pxp-list-side-filter-panel").on("change", ".job-salary-list-check-box", function () {
    $("#loading").addClass('show');
    if ($(this).is(":checked")) {
        arrJobSalary.push($(this).attr("value"));
        $(this).parent().parent().addClass('pxp-checked')
    }
    else {
        arrJobSalary = arrJobSalary.filter(x => x != $(this).attr("value"));
        $(this).parent().parent().removeClass('pxp-checked')
    }
    pageIndex = 1;

    loadDataJob();
})

//checkbox job prosition
var arrJobPosition = [];
$(".pxp-list-side-filter-panel").on("change", ".job-position-list-check-box", function () {
    $("#loading").addClass('show');
    if ($(this).is(":checked")) {
        arrJobPosition.push($(this).attr("value"));
        $(this).parent().parent().addClass('pxp-checked')
    }
    else {
        arrJobPosition = arrJobPosition.filter(x => x != $(this).attr("value"));
        $(this).parent().parent().removeClass('pxp-checked')
    }
    pageIndex = 1;

    loadDataJob();
})

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
async function loadDataJobCategory() {
    $("#search-category").empty();
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/job-category/list",
        type: "GET",
        success: function (responseData) {
            if (responseData.resources.length > 0) {
                $('#search-category').append(new Option("Tất cả danh mục", 0, false, false)).trigger('change');

                responseData.resources.forEach(function (item) {
                    $('#search-category').append(new Option(item.name, item.id, false, false)).trigger('change');

                })

            }

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

$("#pagination-job").on("click", ".page-item", function (e) {
    e.preventDefault();
    window.scrollTo(0, 300);

    if ($(this).hasClass('paging-first-item')) {
        pageIndex = 1;
        loadDataJob();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSources.totalPages;
        loadDataJob();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        loadDataJob();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        loadDataJob();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass("active");
            $(this).addClass("active");
            pageIndex = parseInt($(this).text());
            loadDataJob();
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
                        //$("[data-job-id=" + jobId + "] i").removeClass('text-primary');
                        //$("[data-job-id=" + jobId + "] i").addClass('text-secondary');
                        //$("[data-job-id=" + jobId + "] i").removeClass('fa-heart');
                        //$("[data-job-id=" + jobId + "] i").addClass('fa-heart-o');
                        //$(this).toggleClass('icon-wishlist');
                        $('[data-job-id=' + jobId + '] div').toggleClass('icon-wishlist');
                        $('[data-job-id=' + jobId + '] div').addClass('icon-wishlist');
                        $('[data-job-id=' + jobId + '] div').removeClass('in-wishlist-2');
                        $('[data-job-id=' + jobId + '] div').removeClass('in-wishlist');


                        //Swal.fire({
                        //    position: 'top-end',
                        //    icon: 'success',
                        //    html: '<div><label class="fs-14 fw-bold">Tin tuyển dụng</label><br/><strong class="color-success">"' +jobName + '"</strong><br/>đã được xóa khỏi danh sách đã lưu!</div>',
                        //    showConfirmButton: false,
                        //    timer: 1800
                        //}).then(function () {
                        //    //$(".rbt-review").click();
                        //    //$("[data-course-id="+courseId+"] i").addClass('text-primary');
                        //    //$('[data-course-id="+courseId+"] i').unbind();
                        //});
                    }
                    else {
                        //$("[data-job-id=" + jobId + "] i").removeClass('text-secondary');
                        //$("[data-job-id=" + jobId + "] i").addClass('text-primary');
                        //$("[data-job-id=" + jobId + "] i").removeClass('fa-heart-o');
                        //$("[data-job-id=" + jobId + "] i").addClass('fa-heart');
                        $('[data-job-id=' + jobId + '] div').toggleClass('in-wishlist');
                        $('[data-job-id=' + jobId + '] div').addClass('in-wishlist-2');
                        $('[data-job-id=' + jobId + '] div').addClass('in-wishlist');

                        //Swal.fire({
                        //    position: 'top-end',
                        //    icon: 'success',
                        //    html: '<div><label class="fs-14 fw-bold">Tin tuyển dụng</label><br/><strong class="color-success">"' + jobName + '"</strong><br/>đã được lưu thành công!</div>',
                        //    showConfirmButton: false,
                        //    timer: 1800
                        //}).then(function () {
                        //    //$(".rbt-review").click();
                        //    //$("[data-course-id="+courseId+"] i").addClass('text-primary');
                        //    //$('[data-course-id="+courseId+"] i').unbind();
                        //});
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
                
            }

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}