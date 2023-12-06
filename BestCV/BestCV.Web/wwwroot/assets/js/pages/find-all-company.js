

$(document).ready(async function () {
    $.when(await loadDataCity(), await loadDataFieldOfActivity(), await loadDataCompanySize()).done(async function () {
        $("#search-all-company").val(searchKeyword);
        $("#search-location").val(searchLocation).trigger('change');
        //$("#search-category").val(searchCategory).trigger('change');
        loadDataCompany();

        $(document).on('click', ".btn-add-candidate", function () {
            var btn = $(this);
            if (localStorage.currentUser) {
                //let candidateId = $(this).attr("data-candidate-id");
                let companyId = $(this).attr("data-company-id");
                let companyName = $(this).attr("data-company-name");
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/candidate-follow-company/add-candidate-follow-company",
                    data: JSON.stringify({ companyId: companyId, companyName: companyName }),
                    type: "Post",
                    contentType: "application/json",
                    beforeSend: function (xhr) {
                        if (localStorage.currentUser) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
                        }
                    },
                    success: function (responseData) {
                        if (responseData.isSucceeded) {
                            if (responseData.status == 200 && responseData.message == "Success") {
                                Toast.fire({
                                    icon: 'success',
                                    title: '<div><label class="fs-14 fw-bold">Cảm ơn bạn đã theo dõi công ty</label><br/><span class="color-success">' + companyName + '</span><br/></div>'
                                })

                                let btnText = btn.find('.btn-text')
                                btnText.text('Bỏ theo dõi');
                                btn.addClass('unfollow');
                            } else if (responseData.status == 200 && responseData.message == "Delete") {
                                Toast.fire({
                                    icon: 'success',
                                    title: '<div><label class="fs-14 fw-bold">Đã bỏ theo dõi công ty</label><br/><span class="color-success">' + companyName + '</span><br/></div>'
                                })

                                let btnText = btn.find('.btn-text')
                                btnText.text('Theo dõi');
                                btn.removeClass('unfollow');
                            }
                        }
                    },
                    error: function (error) {
                        if (responseData.status == 401) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Lưu ý',
                                html: 'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại!',
                                focusConfirm: true,
                                allowEnterKey: true
                            });
                        }
                    },

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
        });
    });

    

    $("#search-all-company").on('keyup', function (e) {
        let key = e.which;
        if (key == 13) {
            pageIndex = 1;
            loadDataCompany();
        }
    });

    $("#search-location").on('change', function () {
        pageIndex = 1;
        loadDataCompany();
    })

    $("#search-activity").on('change', function () {
        pageIndex = 1;
        loadDataCompany();
    })

    //$("#btn-search-company").click(function (e) {
    //    e.preventDefault();
    //    loadDataJob();
    //})

    //$("#search-activity").on('change', function () {
    //    search = $(this).val();
    //})

    $("#selectOrder").select2({
        width: "100%",
    });

    $("#selectOrder").on('change', function () {
        order = $(this).val();
        pageIndex = 1;
        loadDataCompany();
    })


    if (localStorage.currentUser) {
        loadDataCandidateFollowCompany();
    }
    const Toast = Swal.mixin({
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

    
})

async function loadDataCandidateFollowCompany() {
    try {
        let res = await httpService.postAsync(`api/candidate-follow-company/get-list-candidate-follow-company`);
        if (res.isSucceeded) {
            if (res.status == 200) {
                dataSource = res.resources;
                dataSource.forEach(item => {
                    $(`.btn-add-candidate[data-company-id=${item.companyId}]`).addClass('unfollow');
                    $(`.btn-add-candidate[data-company-id=${item.companyId}] span.btn-text`).text("Bỏ theo dõi");
                })
            }
        }
    }
    catch (e) {
        dataSource = [];
        Swal.fire({
            icon: 'warning',
            title: 'Lấy dữ liệu thất bại',
            html: 'Có lỗi xảy ra khi lấy danh sách công việc đã lưu, vui lòng thử lại',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        })
    }
}
// hiển thị tỉnh thành
async function loadDataCity() {
    $("#search-location").empty();
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/workplace/list-city",
        type: "GET",
        success: function (responseData) {
            if (responseData.isSucceeded) {
                $('#search-location').append(new Option("Tất cả tỉnh thành", 0, false, false)).trigger('change');

                responseData.resources.forEach(function (item) {
                    $('#search-location').append(new Option(item.name, item.id, false, false)).trigger('change');

                })
                $("#search-location").select2({
                    width: "100%",
                });
            }

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

// hiển thị lĩnh vực hoạt động
async function loadDataFieldOfActivity() {
    $("#search-activity").empty();
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/field-of-activity/list",
        type: "GET",
        success: function (responseData) {
            if (responseData.isSucceeded) {
                if (responseData.resources.length > 0) {
                    $('#search-activity').append(new Option("Tất cả lĩnh vực hoạt động", 0, false, false)).trigger('change');

                    responseData.resources.forEach(function (item) {
                        $('#search-activity').append(new Option(item.name, item.id, false, false)).trigger('change');

                    })
                    $("#search-activity").select2({
                        width: "100%",
                    });
                }
            }
        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

// hiển thị quy mô công ty
async function loadDataCompanySize() {
    $("#loading").addClass("show");
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/company-size/filter-company-size",
        type: "GET",
        success: function (responseData) {
            if (responseData.isSucceeded) {
                //console.log(responseData.resources)
                dataFilter = responseData.resources;
                $("#company-size").html('');
                var contentCompanySize = "";
                dataFilter.forEach(function (item) {
                    contentCompanySize += `<label class="list-group-item d-flex justify-content-between align-items-center mb-lg-2">
                                        <span class="d-flex">
                                            <input class="form-check-input me-2 company-size-list-check-box" type="checkbox" value="${item.id}">
                                            ${item.name}
                                        </span>
                                        <span class="badge rounded-pill">${item.count}</span>
                                    </label>`
                })
                $("#company-size").html(contentCompanySize)
            }
        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

var pageIndex = 1;
var dataSources = [];
var dataSourceCompany = [];
function loadDataCompany() {
    $("#loading").addClass('show');
    var obj = {
        keywords: $("#search-all-company").val(),
        workPlace: $("#search-location").val(),
        orderCriteria: $("#selectOrder").val(),
        fieldOfActivity: $("#search-activity").val(),
        companySize: arrCompanySize.toString(),
        pageIndex: pageIndex,
        pageSize: 6,
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/company/searching-company",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(obj),
        async: false,
        beforeSend: function (xhr) {
            if (localStorage.currentUser) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
            }
        },
        success: function (responseData) {
            if (responseData.isSucceeded) {
                //window.scrollTo(0, 0);
                window.history.pushState(null, null, "/tim-kiem-cong-ty?keyword=" + $("#search-all-company").val() + "&location=" + $("#search-location").val() + "&activity=" + $("#search-activity").val())
                $("#loading").removeClass('show');
                dataSources = responseData.resources;
                dataSourceCompany = responseData.resources.dataSource;
                var pageInforCompany = "Hiển thị từ " + dataSources.firstRowOnPage
                    + " đến " + dataSources.lastRowOnPage
                    + " trong tổng số " + dataSources.totalFiltered + " công ty";
                $("#pageInfo").text(pageInforCompany);
                var contentCompany = "";
                $("#section-company").html("");
                if (dataSourceCompany.length > 0) {
                    dataSourceCompany.forEach(function (item) {
                        let link = systemConfig.defaultURL + '/thong-tin-cong-ty/' + item.id;
                        //let description = item.description;
                        //let htmlDescription = description;
                        //let images = htmlDescription.find("img");
                        //images.remove();
                        //hiển thị thông tin các lĩnh vực hoạt động của công ty 
                        var contentFieldOfActivity = "";
                        var titleActivity = "Lĩnh vực hoạt động của công ty\n";
                        var listActivity = item.companyFieldOfActivityAggregates;
                        let index = 0;
                        listActivity.forEach(function (item, index) {
                            titleActivity += (index + 1) + ": " + item.fieldOfActivityName + "\n";
                        });

                        if (item.companyFieldOfActivityAggregates.length > 0 && item.companyFieldOfActivityAggregates.length < 2) {
                            item.companyFieldOfActivityAggregates.forEach(function (itemActivity) {
                                contentFieldOfActivity += `<a class='hash-tag' value-id='${itemActivity.fieldOfActivityId}'>${itemActivity.fieldOfActivityName}</a>`
                            })
                        }
                        if (item.companyFieldOfActivityAggregates.length > 2) {

                            contentFieldOfActivity += `<a class='hash-tag' value-id='${listActivity[0].fieldOfActivityId}'>
                                                            ${listActivity[0].fieldOfActivityName} & ${listActivity.length} lĩnh vực khác
                                                   </a>`
                        }
                        contentCompany += `<div class="col-md-6 col-lg-12 col-xl-6 col-xxl-4 pxp-companies-card-1-container" >
                        <div class="pxp-companies-card-1 hover-card jobi-card-company">
                             
                            <div class="pxp-companies-card-1-top">
                                <a href="${link}" class="jbi-company-logo-2 bg-white" style="background-image: url(${item.logo != null ? systemConfig.defaultStorageURL + item.logo : ""});"></a>

                                <a href="${link}" class="pxp-companies-card-1-company-name mt-32px">${item.name}</a>
                            </div>
                            
                            <div class="pxp-companies-card-1-bottom d-flex flex-column">
                                <span class="overview-title mb-4 line-clamp-5">${item.overview}</span>

                            <div class=" d-flex justify-content-between align-items-center">
                                <a href="${systemConfig.defaultURL + '/tim-kiem-viec-lam?tukhoa=' + item.name}" class="pxp-companies-card-1-company-jobs">${item.countJob} việc làm</a>
                                <button href="#!" class="btn btn-add-candidate ${item.isFollowedCompany ? "unfollow" : ""}" data-company-id="${item.id}" data-company-name="${item.name}">
                                     <span class="btn-text"> ${item.isFollowedCompany ? "Bỏ theo dõi" : "Theo dõi"}</span>
                                </button>
                                </div>
                            </div>
                        </div>
                    </div>`;
                    })
                }
                else {
                    contentCompany += `<div class='text-center mt-3'>Không có dữ liệu để hiển thị</div>`
                }
                $("#section-company").html(contentCompany);
                //$("#countJob").html("Tìm kiếm cơ hội nghề nghiệp của bạn thông qua <strong>" + dataSources.total + "</strong> việc làm")
                pageIndex = dataSources.currentPage;
                initPagination(dataSources.totalPages, '.pxp-pagination');
            }
        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}



//checkbox companysize
var arrCompanySize = [];
$("#company-size").on("change", ".company-size-list-check-box", function () {
    if ($(this).is(":checked")) {
        arrCompanySize.push($(this).attr("value"));
        $(this).parent().parent().addClass('pxp-checked')
    }
    else {
        arrCompanySize = arrCompanySize.filter(x => x != $(this).attr("value"));
        $(this).parent().parent().removeClass('pxp-checked')
    }
    loadDataCompany();
})


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

$("#pagination-company").on("click", ".page-item", function (e) {
    e.preventDefault();
    window.scrollTo(0, 300);

    if ($(this).hasClass('paging-first-item')) {
        pageIndex = 1;
        loadDataCompany();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSources.totalPages;
        loadDataCompany();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        loadDataCompany();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        loadDataCompany();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass("active");
            $(this).addClass("active");
            pageIndex = parseInt($(this).text());
            loadDataCompany();
        }
    }
});