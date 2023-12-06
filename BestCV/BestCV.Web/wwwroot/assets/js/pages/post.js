$(document).ready(async function () {
    //debugger;
    var postCategoryIdList = postCategoryId;
    $('input.post-category-list-check-box[value=' + postCategoryIdList + ']').prop('checked', true);
    $.when(await loadDataPostCategory()).done(function () {
        $("#search-all-post").val(searchKeyword);
        $("#search-category").val(searchCategory).trigger('change');
        loadDataPost();
        loadDataFilter();
        loadPostTag();
    })
    var isLoading = false;
    $("#search-all-post").on("keypress", function (e) {
        let key = e.which;
        if (key == 13) {
            pageIndex = 1;
            loadDataPost();
        }
    })
    $("#search-category").on('change', function () {
        searchCategory = $(this).val();
    })
    $("#selectOrder").on('change', function () {
        order = $(this).val();
        loadDataPost();
    })

    if (postCategoryId != 0) {
        $("#filterPostCategoryTitle").addClass('d-none');
        $("#filterPostCategory").addClass('d-none');
    }
    else {
        $("#filterPostCategoryTitle").removeClass('d-none');
        $("#filterPostCategory").removeClass('d-none');
    }

    
})

var order;
var searchCategory = 0;
var dataFilter = [];
var pageIndex = 1;
function loadDataFilter() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/post/filter-post",
        type: "GET",
        success: function (responseData) {
            //debugger;
            //console.log(responseData.resources)
            if (responseData.isSucceeded) {
                dataFilter = responseData.resources;
                $("#filterPostCategory").html('');
                var contentPostCategory = "";
                dataFilter.postCategoryData.forEach(function (item) {
                    //console.log(item)
                    contentPostCategory += `
                        <label class="d-flex align-items-center mb-lg-2">
                            <span class="d-flex">
                                <input class="form-check-input me-2 post-category-list-check-box" type="checkbox" value="${item.id}">
                                ${item.name} 
                            </span>
                            <span class="badge rounded-pill ">${item.countPost}</span>
                        </label>
                    `

                })
                $("#filterPostCategory").html(contentPostCategory)
                var postCategoryIdList = postCategoryId;
                $('input.post-category-list-check-box[value=' + postCategoryIdList + ']').prop('checked', true).trigger("change");

            }
            //< li > <a href="blog-list-1.html">Jobs</a></li >
            //                <li><a href="blog-list-1.html">Resume</a></li>
            //                <li><a href="blog-list-1.html">Future of Work</a></li>
            //                <li><a href="blog-list-1.html">Interview</a></li>
            //< span class="badge rounded-pill" > ${ item.countPost }</span >

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}

//Load Tag
async function loadPostTag() {
    let res = await httpService.getAsync("api/tag/list-post-tag");
    var id = parseInt($(this).attr("data-listTag"))
    if (res && res.resources) {
        let tagHtml = "";
        for (let tag of res.resources) {
            tagHtml += `<a class="tagSelect ${searchtagId == tag.id ? "choosed" : ""}" data-tagId="${tag.id}" href="#!">${tag.name}</a>`
        }
        $(".pxp-blogs-side-tags").html(tagHtml);
    }
}


//Load Data Post Category
async function loadDataPostCategory() {
    $("#search-category").empty();
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/post-category/list",
        type: "GET",
        success: function (responseData) {
            if (responseData.isSucceeded) {
                if (responseData.resources.length > 0) {
                    $('#search-category').append(new Option("Tất cả danh mục", 0, false, false)).trigger('change');

                    responseData.resources.forEach(function (item) {
                        $('#search-category').append(new Option(item.name, item.id, false, false)).trigger('change');

                    })
                    $("#search-category").select2();
                }
            }
        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}
var dataSource = [];
var dataSourcePost = [];
function loadDataPost() {
    $("#loading").addClass('show');
    //debugger;
    var obj = {
        keywords: $("#search-all-post").val(),
        orderCriteria: $("#selectOrder").val(),
        pageIndex: pageIndex,
        pageSize: 5,
        postCategory: arrPostCategory.toString(),
        listTag: arrTagCategory.toString(),
        postCategoryId: $("#search-category").val(),
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/post/list-post",
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
            //debugger;
            $("#loading").removeClass('show');
            dataSources = responseData.resources;
            dataSourcePost = responseData.resources.dataSource;
            $("#pageInfo").text(responseData.resources.pageInfo)
            var contentPost = "";
            $("#section-post").html("");
            if (dataSourcePost.length > 0) {
                dataSourcePost.forEach(function (item) {
                    var slug = convertToSlug(item.name) + "-" + item.id
                    ///console.log(item)
                    var slugCategory = convertToSlug(item.postCategoryName) + "-" + item.postCategoryId
                    contentPost += `
                            <div class="pxp-posts-card-2 pxp-has-border  jobi-post-card mb-32px">
                                <div class="pxp-posts-card-2-fig">
                                    <div class="pxp-cover" style="background-image: url(${item.photo != null ? systemConfig.defaultStorageURL + item.photo : ""});"></div>
                                </div>
                                <div class="pxp-posts-card-2-content">
                                    <div class="row justify-content-between align-items-center">
                                        <div class="col-auto">
                                            <a href="/danh-muc/${slugCategory}" class="pxp-posts-card-2-category">${item.postCategoryName} </a>
                                        </div>
                                        <div class="col-auto">
                                            <div class="pxp-posts-card-2-date">${moment(item.publishedTime).format("DD/MM/YYYY HH:mm:ss")}</div>
                                        </div>
                                    </div>
                                    <div class="pxp-posts-card-2-title mt-4">
                                        <a href="/bai-viet/${slug}">${item.name}</a>
                                    </div>
                                    <div class="pxp-posts-card-2-summary pxp-text-light">${item.overview}</div>
                                    <div class="pxp-posts-card-2-cta">
                                        <a href="/bai-viet/${slug}">Đọc Thêm<span class="fa fa-angle-right"></span></a>
                                    </div>
                                </div>
                            </div>`
                })
            }
            else {
                contentPost += `<div class='text-center mt-3'>Không có dữ liệu để hiển thị</div>`
            }
            $("#section-post").html(contentPost);
            //$("#countPost").html("Tìm kiếm cơ hội nghề nghiệp của bạn thông qua <strong>" + dataSources.total + "</strong> việc làm")
            //pageIndex = dataSources.currentPage;
            initPagination(dataSources.totalPages, '.pxp-pagination');

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
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

//checkbox postCategory
var arrPostCategory = [];
//tag select
var arrTagCategory = [];
$(document).on("change", ".post-category-list-check-box", function () {
    if ($(this).is(":checked")) {
        arrPostCategory.push($(this).attr("value"));
        $(this).parent().addClass('pxp-checked')
    }
    else {
        arrPostCategory = arrPostCategory.filter(x => x != $(this).attr("value"));
        $(this).parent().removeClass('pxp-checked')
    }
    pageIndex = 1;
    loadDataPost();
})

$(document).on("click", ".tagSelect", function () {
    //debugger;
    var id = parseInt($(this).attr("data-tagId"));
    if (!arrTagCategory.includes(id)) {
        arrTagCategory.push(id);
        $(this).addClass("choosed");
    } else {
        arrTagCategory = arrTagCategory.filter(item => item !== id)
        $(this).removeClass("choosed");
    }

    if (searchObj.tagId.length > 0) {
        $("#resetTagFilter").removeClass("d-none");
    } else {
        $("#resetTagFilter").addClass("d-none");
    }
    
    pageIndex = 1;
    loadDataPost();
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

$("#pagination-post").on("click", ".page-item", function (e) {
    e.preventDefault();
    window.scrollTo(0, 200);

    if ($(this).hasClass('paging-first-item')) {
        pageIndex = 1;
        loadDataPost();
    }
    else if ($(this).hasClass('paging-last-item')) {
        pageIndex = dataSources.totalPages;
        loadDataPost();
    }
    else if ($(this).hasClass('paging-next')) {
        pageIndex = pageIndex + 1;
        loadDataPost();
    }
    else if ($(this).hasClass('paging-previous')) {
        pageIndex = pageIndex - 1;
        loadDataPost();
    }
    else {
        if (!($(this).attr('class').includes('active'))) {
            $(".page-item").removeClass("active");
            $(this).addClass("active");
            pageIndex = parseInt($(this).text());
            loadDataPost();
        }
    }
});

function convertToSlug(input) {

    var slug = input.normalize("NFD").replace(/[\u0300-\u036f]/g, "").replace(/[^a-zA-Z0-9\s]/g, '');

    slug = slug.replace(/\s+/g, '-').toLowerCase();

    return slug;
}