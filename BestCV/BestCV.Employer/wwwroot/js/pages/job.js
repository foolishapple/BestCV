
"use strict";
var table;
var jobStatusSource = [];
var jobStatus;

async function init() {
    await getJobStatusSource();
    initTable();
}
function initTable() {
    table = $("#tbl_job").DataTable({
        language: systemConfig.languageDataTable,
        serverSide: true,
        processing: true,
        paging: true,
        searching: { regex: true },
        order: false,
        autoWidth: false,
        ordering: false,
        ajax: {
            url: `${systemConfig.defaultAPI_URL}api/job/dt-paging-to-employer`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#search_all").val().trim() || "";
                d.jobStatusId = jobStatus;
                return JSON.stringify(d);
            }
        },
        columns: [
            {
                data: "job",
                render: function (data, type, row, meta) {
                    return `<div class="d-flex flex-column pxp_name">
                                <span><a href="/tin-tuyen-dung/dashboard/${row.id}" class="pxp_title text_link" title="${row.name}">${row.name}</a></span>
                                <div class="mt-1">
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.id}</span><span class="pxp_status ms-2 text-nowrap" ${row.isApproved ? `style="color:${row.statusColor}"` : ``}>${row.isApproved ? row.statusName : `Tin chưa được duyệt`}</span>
                                </div>
                            </div>`;
                }
            },
            {
                data: "recruitmentCampagin",
                render: function (data, type, row, meta) {
                    let rowContent = ``;
                    return `<div class="d-flex flex-column pxp_name">
                                <span><a href="/recruitment-campaign/dashboard/${row.recruitmentCampaginId}"  class="pxp_title text_link" title="${row.recruitmentCampaginName}">${row.recruitmentCampaginName}</a></span>
                                <div class="mt-1">
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.recruitmentCampaginId}</span>
                                </div>
                            </div>`;
                }
            },
            {
                data: "activity",
                render: function (data, type, row, meta) {
                    let rowContent = ``;
                    return `<div class="job_activity">
                                <div class="item mb-2" title="Số lượng ứng tuyển"><i class="text-muted far fa-file-user me-2"></i>Lượt ứng tuyển: ${row.totalCandidateApply}</div>
                                <div class="item" title="Thời gian hoạt động"><i class="far fa-calendar me-2"></i>${moment(row.createdTime).format("DD/MM/YYYY")} - ${row.applyEndDate == null ? `Hiện tại` : moment(row.applyEndDate).format("DD/MM/YYYY") }</div>
                            </div>`;
                }
            },
            {
                data: "action",
                render: function (data, type, row, meta) {
                    return `<div class="pxp-dashboard-table-options">
                                <ul class="list-unstyled justify-content-center">
                                    <li><a href="/tin-tuyen-dung/${row.id}"><button class="bnt_edit" data-id="${row.id}" title="Chỉnh sửa"><span class="fa fa-pencil"></span></button></a></li>
                                     <li><a href="${systemConfig.defaultWebURL}chi-tiet-cong-viec/${stringToSlug(row.name)+"-"+row.id}" target="_blank"><button class="bnt_preview" data-id="${row.id}" title="Xem trước"><span class="fa fa-eye"></span></button></a></li>
                                </ul>
                            </div>`;
                }
            }
        ],
        columnDefs: [
            {
                targets: [-1],
                className: "text-center"
            }
        ],
        drawCallback: function () {
            $('#tbl_job tfoot').html("");
            innitTblPaging("#job_paging");
            initInfo("#tbl_info");
        }
    });
    $(document).on("click", ".page-link", function (e) {
        if ($(this).hasClass("active")) {
            return;
        }
        let pageIndex = $(this).attr("data-index");
        if (pageIndex) {
            table.page(parseInt(pageIndex)).draw('page');
        }
    })
    $("#search_all").on("keypress", function (e) {
        if (e.which == 13) {
            table.draw();
        }
    })
    $("#tbl_job").on("click", ".btn_search", function (e) {
        table.draw();
    })
    $(".btn_search_all").on("click", function () {
        table.draw();
    })
    $("#pills-status").on("click", ".nav-link", function (e) {
        e.preventDefault();
        let element = $(this);
        if (element.hasClass("active")) {
            return;
        }
        else {
            $("#pills-status .nav-link.active").removeClass("active");
            element.addClass("active");
            $("#search_all").val("");
            jobStatus = element.attr("data-id");
            table.page("first").draw();
        }
    })
}
function innitTblPaging(element) {
    let info = table.page.info();
    let totalPage = info.pages;
    let pageIndex = info.page;
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
                startPage = pageIndex == 0 ? 1 : pageIndex;
            }

        }
        let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
        if (pageIndex >= 1) {
            html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link"><i class="fa fa-angle-left"></i></a></li>`;
        }
        for (var i = startPage; i <= endPage; i++) {
            if (i > 0) {
                html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
            }
        }
        if (pageIndex < totalPage - 1) {
            html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
        }
        $(element).html(html);
    }
    else {
        $(element).html("");
    }
}
function initInfo(e) {
    let info = table.page.info();
    $(e).text(`${info.recordsDisplay} tin tuyển dụng`);
}

async function getJobStatusSource() {
    try {
        let result = await httpService.getAsync("api/job-status/list");
        if (result.isSucceeded) {
            jobStatusSource = result.resources;
            jobStatusSource.forEach(function (item) {
                $("#pills-status").append(`<li class="nav-item" role="presentation">
                        <button class="btn nav-link  me-2 mb-1"  data-id="${item.id}"  type="button">${item.name} <span class="badge  rounded-pill bg-primary" id="total_job_${item.id}">0</span></button>
                    </li>`)
                let statusIds = [item.id];
                getCount(statusIds, `#total_job_${item.id}`);
            })
        }
        else {
            jobStatusSource = [];
        }
    } catch (e) {
        jobStatusSource = [];
        console.error(e);
    }
}

async function getCount(statusIds, target) {
    try {
        let params = {
            jobStatusIds: statusIds
        }
        let result = await httpService.postAsync("api/job/count-to-employer", params);
        if (result.isSucceeded) {
            if (target) {
                let total = result.resources;
                $(target).text(total);
            }
        }
    } catch (e) {
        console.error(e);
    }
}
$(document).ready(function () {
    init();
})
