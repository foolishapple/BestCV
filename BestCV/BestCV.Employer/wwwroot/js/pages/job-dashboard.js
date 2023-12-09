
"use strict";
var jobDetail = {};

$(document).on("change", "[data-bs-toggle=tooltip]", function () {
    let tooltip = new bootstrap.Tooltip(this, {});
})
var table, tableCanidateViewed, tableServiceHistory, tableAddNewService;
var updatingObj = {};
var candidateApplyJobStatusSource = [];
var jobSource = [];
var recruimentCampagin = {};
var candidateApplyJobSourceId = candidateApplyJobSource.CANDIDATE_APPLY;
var serviceSource = [];
$(document).ready(function () {
    init();
})
async function detail() {
    try {
        let response = await httpService.getAsync(`api/job/job-detail/${jobId}`);
        if (response.isSucceeded) {
            jobDetail = response.resources;
            $("#card_title").text(jobDetail.name);
            $("#JobViewCount").text(jobDetail.viewCount);
        }
        else {
            jobDetail = {};
        }
    } catch (e) {
        console.error(e);
    }
}
detail();
async function initTable() {
    table = $("#table_candidate").DataTable({
        language: systemConfig.languageDataTable,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ordering: false,
        autoWidth: false,
        ajax: {
            url: `${systemConfig.defaultAPI_URL}api/candidate-apply-job/list-to-employer`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#search_all").val().trim() || "";
                d.columns[2].search.value = $("#fillter_title").val().trim() || "";
                d.columns[3].search.value = $("#fillter_contact").val().trim() || "";
                d.columns[4].search.value = $("#fillter_other").val().trim() || "";
                d.isViewUnread = $("#fillter_unread").val() == "unread" || false;
                d.candidateApplyJobStatusIds = $("#fillterStatus").val() || [];
                d.candidateApplyJobSourceId = candidateApplyJobSourceId;
                d.jobId = jobId;
                return JSON.stringify(d);
            }
        },
        columns: [
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "candidateAvatar",
                render: function (data, type, row, meta) {
                    return `<div class="pxp-company-dashboard-candidate-avatar pxp-cover bg-secondary" style="background-image: url(${systemConfig.defaultStorage_URL + row.candidateAvatar});"></div>`;
                }
            },
            {
                data: "title",
                render: function (data, type, row, meta) {
                    let viewed = row.isEmployerViewed;
                    return `<a class="employerCV" data-target="${row.id}" href="${systemConfig.defaultStorage_URL + row.candidateCVPDFUrl}" target="_blank">
                                <div class="pxp-company-dashboard-job-title">${row.candidateName}</div>
                                <div class="pxp-company-dashboard-job-status">
                                    <span class=" badge  rounded-pill ${viewed ? `bg-secondary text-dark` : `bg-primary`} ">${viewed ? `Đã đọc` : `Chưa đọc`}</span>
                                </div>
                            </a>`;
                }
            },
            {
                data: "contact",
                render: function (data, type, row, meta) {
                    return `<div class="candidate_contact">
                                <div class="item ${row.candidateEmail == null || row.candidateEmail == `` || row.candidateEmail == undefined ? `d-none` : ``} mb-1" title="Email"><i class="fa text-primary fa-envelope me-2"></i>${row.candidateEmail}</div>
                                <div class="item ${row.candidatePhone == null || row.candidatePhone == `` || row.candidatePhone == undefined ? `d-none` : ``}" title="Số điện thoại"><i class="fa text-primary fa-circle-phone me-2"></i>${row.candidatePhone}</div>
                            </div>`;
                }
            },
            {
                data: "other",
                render: function (data, type, row, meta) {
                    return ` <div class="candidate_more">
                                <div class="item mb-1" title="Nguồn ứng tuyển"><i class="fa-regular fa-briefcase-arrow-right me-2"></i>${row.candidateApplyJobSourceName}</div>
                                <div class="item mb-1" title="Ngày ứng tuyển"><i class="fa-regular fa-clock me-2"></i>${moment(row.createdTime).format(`DD/MM/YYYY HH:mm`)}</div>
                                <div class="item" title="Tên công việc"><i class="fa-regular fa-briefcase me-2"></i>${row.jobName}</div>
                            </div>`;
                }
            },
            {
                data: "status",
                render: function (data, type, row, meta) {
                    let color = row.candidateApplyJobStatusColor;
                    return `<span type="button"  class="badge px-2 rounded-pill btn_change_status" data-id=${row.id} style="color: ${color};background-color: ${customBagdeColor(color)}; opacity: 0.7;">${row.candidateApplyJobStatusName}</span>`;
                }
            },
            {
                data: "action",
                render: function (data, type, row, meta) {
                    return ` <div class="candidate_action dropdown text-center">
                                <button class="dropdown-toggle btn_icon" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><span class="fa fa-ellipsis-h"></span></button>
                                <ul class="dropdown-menu list-unstyled shadow ">
                                    <li><a class="dropdown-item btn_change_status" data-id=${row.id} href="#!" title="Đổi trạng thái CV">Đổi trạng thái CV</a></li>
                                    <li><a class="dropdown-item btn_add_note" href="#!" data-id=${row.id} title="Cập nhật ghi chú">Ghi chú</a></li>
                                    <li><a class="dropdown-item btn_add_interview" href="#!" data-id=${row.id} title="Tạo lịch phỏng vấn">Tạo lịch phỏng vấn</a></li>
                                    <li><a class="dropdown-item d-none" href="#!" title="Approve">Tải CV</a></li>
                                    <li><a class="dropdown-item d-none" href="#!" title="Reject">Sao chép mã ứng viên</a></li>
                                    <li><a class="dropdown-item btn_compare_candidate ${countCV < 2 ? "d-none" : ""}" href="#!" title="So sánh ứng viên" data-id="${row.id}">So sánh ứng viên</a></li>
                                </ul>
                            </div>`;
                }
            }
        ],
        columnDefs: [
            {
                targets: [0, 1, 2, 3, 4, 5, 6, -1],
                orderable: false
            },
            {
                targets: [0, -1],
                className: "text-center"
            },
            {
                targets: [0],
                className: "pt-4"
            },
            {
                targets: [1],
                className: "column-avatar"
            }
        ],
        drawCallback: function () {
            $('#table_candidate tfoot').html("");
            $("#pills-cv .candidate_recruitment_campaign").trigger("change");
            innitTblPaging("#candidate_paging");
            initInfo("#tbl_info");

        }
    });
    $(document).on("click", "#pills-cv .page-link", function (e) {
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
    $("#table_candidate").on("keypress", ".fillter_tbl", function (e) {
        if (e.which == 13) {
            table.draw();
        }
    })
    $("#table_candidate").on("click", ".btn_search", function (e) {
        table.draw();
    })
    $("#table_candidate").on("click", ".btn_reset", function (e) {
        $("#table_candidate .fillter_tbl").val(null).trigger("change");
        table.draw();
    })
    $("#fillter_unread").on("change", function () {
        table.draw();
    })
    $("#pills-cv .btn_search_all").on("click", function () {
        table.draw();
    })
    $(document).on("click", "#pills-cv .employerCV", function (e) {
        let id = $(this).attr("data-target");
        if (id) {
            let item = table.ajax.json().data.find(c => c.id == id);
            if (item) {
                if (!item.isEmployerViewed) {
                    EmployerViewedCV(item)
                }
            }
        }
    })
    $(document).on("click", "#pills-cv .nav-cv", function () {
        let dataId = $(this).attr("data-source-id");
        if (dataId) {
            candidateApplyJobSourceId = dataId;
        };
        $("#fillter_unread").val("all");
        $("#search_all").val(null).trigger("change");
        table.page("first").draw();
    })

    //So sánh ứng viên
    $(document).on('click', '.btn_compare_candidate', function () {
        let dataId = $(this).attr("data-id");
        //console.log(dataId);
        window.open("/tin-tuyen-dung/so-sanh-cv/" + dataId + "/" + jobId);
    })
}
async function initTableCandidateViewed() {
    tableCanidateViewed = $("#table_candidate_viewed").DataTable({
        language: systemConfig.languageDataTable,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ordering: false,
        autoWidth: false,
        ajax: {
            url: `${systemConfig.defaultAPI_URL}api/candidate-viewed-job/dt-paging`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#search_all_candidate_viewed").val().trim() || "";
                d.jobId = jobId;
                return JSON.stringify(d);
            }
        },
        columns: [
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "candidatePhoto",
                render: function (data, type, row, meta) {
                    return `<div class="pxp-company-dashboard-candidate-avatar pxp-cover bg-secondary me-0" style="background-image: url(${systemConfig.defaultStorage_URL + data});"></div>`;
                }
            },
            {
                data: "title",
                render: function (data, type, row, meta) {
                    let viewed = row.isEmployerViewed;
                    return `<a  href="#!" >
                                <div class="pxp-company-dashboard-job-title">${row.candidateName}</div>
                            </a>`;
                }
            },
            {
                data: "contact",
                render: function (data, type, row, meta) {
                    return `<div class="candidate_contact">
                                <div class="item ${row.candidateEmail == null || row.candidateEmail == `` || row.candidateEmail == undefined ? `d-none` : ``} mb-1" title="Email"><i class="fa text-primary fa-envelope me-2"></i>${row.candidateEmail}</div>
                                <div class="item ${row.candidatePhone == null || row.candidatePhone == `` || row.candidatePhone == undefined ? `d-none` : ``}" title="Số điện thoại"><i class="fa text-primary fa-circle-phone me-2"></i>${row.candidatePhone}</div>
                            </div>`;
                }
            },
            {
                data: "createdTime",
                render: function (data, type, row, meta) {
                    return moment(data).format("DD/MM/YYYY HH:mm");
                }
            },
            {
                data: "action",
                render: function (data, type, row, meta) {
                    return ` <div class="candidate_action dropdown text-center">
                                <button class="dropdown-toggle btn_icon" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><span class="fa fa-ellipsis-h"></span></button>
                                <ul class="dropdown-menu list-unstyled shadow ">
                                    <li><a class="dropdown-item "  href="${systemConfig.defaultWebURL}thong-tin-ca-nhan/${row.candidateId}" target="_blank" title="Xem hồ sơ">Xem hồ sơ</a></li>
                                </ul>
                            </div>`;
                }
            }
        ],
        columnDefs: [
            {
                targets: [0, -1],
                className: "text-center"
            },
            {
                targets: [0],
                className: "pt-3"
            },
            {
                targets: [1],
                className: "text-start"
            }
        ],
        drawCallback: function () {
            let element = "#candidate_viewed_paging";
            let info = tableCanidateViewed.page.info();
            $("#tbl_candidate_viewed_info").text(`${info.recordsDisplay} ứng viên`);
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
    });
    $("#search_all_candidate_viewed").on("keypress", function (e) {
        if (e.which == 13) {
            tableCanidateViewed.draw();
        }
    })
    $(document).on("click", ".btn_search_all_viewed", function (e) {
        tableCanidateViewed.draw();
    })
    $(document).on("click", "#pills-candidate-viewed-tab", function () {
        $("#search_all").val(null).trigger("change");
        tableCanidateViewed.page("first").draw();
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
    $(e).text(`${info.recordsDisplay} ứng viên`);
}


$(document).on("click", ".btn_add_note", function (e) {
    let id = $(this).attr("data-id");
    if (id) {
        updatingObj = table.ajax.json().data.find(c => c.id == id);
        if (updatingObj) {
            $("#modal_addNote #candidateApplyJobDescription").val(updatingObj.description);
            $("#modal_addNote").modal("show");
        }
    }
})
$("#modal_addNote .btn_submit").on("click", async function (e) {
    swal.fire({
        title: "Ứng viên",
        html: "Bạn có chắc chắn muốn cập nhật ghi chú ?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then(async (result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            try {
                let obj = updatingObj;
                obj.description = $("#candidateApplyJobDescription").val();
                let result = await httpService.putAsync("api/candidate-apply-job/add-description", obj);
                $("#loading").removeClass("show");
                if (result.isSucceeded) {
                    Swal.fire("Ứng viên", "Ghi chú đã được cập nhật thành công!", "success");
                    table.draw();
                    $("#modal_addNote").modal("hide");
                }
                else {
                    if (result.status == 400) {
                        let contentError = "<ul>";
                        let errorList = result.errors;
                        errorList.forEach(function (item, index) {
                            contentError += "<li class='text-start'>" + item + "</li>";
                        });
                        contentError += "</ul>";
                        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật ghi chú không thành công</p>";
                        Swal.fire(
                            'Ứng viên' + swalSubTitle,
                            contentError,
                            'warning'
                        );
                    }
                    else {
                        Swal.fire("Cập nhật ghi chú không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                    }
                }
            } catch (e) {
                Swal.fire("Cập nhật ghi chú không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                console.error("e");
                $("#loading").removeClass("show");
            }
        }
    })

})

$(document).on("click", ".btn_change_status", function (e) {
    let id = $(this).attr("data-id");
    if (id) {
        updatingObj = table.ajax.json().data.find(c => c.id == id);
        if (updatingObj) {
            $("#ApplyJobDescription").val(updatingObj.description).trigger("change");
            $("#pills-tab-status .active").removeClass("active");
            $(`#pills-tab-status .nav-link[data-id=${updatingObj.candidateApplyJobStatusId}]`).addClass("active");
            $("#modal_change_status").modal("show");
        }
    }
})
$("#modal_change_status .btn_submit").on("click", async function (e) {
    swal.fire({
        title: "Ứng viên",
        html: "Bạn có chắc chắn muốn cập nhật trạng thái CV ứng viên ?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then(async (result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            try {
                let obj = updatingObj;
                obj.description = $("#ApplyJobDescription").val();
                obj.candidateApplyJobStatusId = $("#pills-tab-status .active").attr("data-id");
                let result = await httpService.putAsync("api/candidate-apply-job/change-status", obj);
                $("#loading").removeClass("show");
                if (result.isSucceeded) {
                    Swal.fire("Ứng viên", "Trạng thái CV đã được cập nhật thành công!", "success");
                    table.draw();
                    $("#modal_change_status").modal("hide");
                }
                else {
                    if (result.status == 400) {
                        let contentError = "<ul>";
                        let errorList = result.errors;
                        errorList.forEach(function (item, index) {
                            contentError += "<li class='text-start'>" + item + "</li>";
                        });
                        contentError += "</ul>";
                        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật trạng thái CVkhông thành công</p>";
                        Swal.fire(
                            'Ứng viên' + swalSubTitle,
                            contentError,
                            'warning'
                        );
                    }
                    else {
                        Swal.fire("Cập nhật trạng thái CV ứng viên thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                    }
                }
            } catch (e) {
                Swal.fire("Cập nhật trạng thái CV ứng viên thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                console.error("e");
                $("#loading").removeClass("show");
            }
        }
    })

})
async function getCandidateApplyJobStatus() {
    try {
        let result = await httpService.getAsync("api/candidate-apply-job-status/list-all");
        if (result.isSucceeded) {
            candidateApplyJobStatusSource = result.resources;
        }
        else {
            candidateApplyJobStatusSource = []
        }
    } catch (e) {
        candidateApplyJobStatusSource = [];
        console.error(e);
    }
    if (candidateApplyJobStatusSource.length > 0) {
        candidateApplyJobStatusSource.forEach(function (item) {
            $('#pills-tab-status').append(`<li class="nav-item" role="presentation">
                            <button class="btn active nav-link me-1 mb-1" data-id="${item.id}" type="button" data-bs-toggle="pill" role="tab" aria-controls="pills-status" aria-selected="false">${item.name}</button>
                        </li>`);
            $("#fillterStatus").append(new Option(item.name, item.id, false, false)).trigger("change");
        })
    }
    $("#fillterStatus").select2({
        placeholder: "Lọc theo trạng thái...",
        allowClear: true
    })
}

function formatRepo(row) {
    if (row.loading) {
        return row.text;
    }
    var $container = $(`<a href="#!" class="candidate_recruitment_campaign" >${row.name}</a>
                                <div>
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.id}</span>
                                </div>`)
    return $container;
}

function formatRepoSelection(row) {
    return row.name;
}

async function init() {
    $.when(await getCandidateApplyJobStatus()).done(function () {
        initTable();
        initTableCandidateViewed();
        initTableServiceHistory();
        initTableAddService();
        getTotalCV();
        getTotalCVCandidateApply();
        $("#history-service-tab").on("click", function () {
            initTableServiceHistory();
        })
        $("#add-service-tab").on("click", function () {
            initTableAddService();
        })
    })
}
var countCV = 0;
async function getTotalCV() {
    try {
        let result = await httpService.getAsync(`api/candidate-apply-job/get-total-cv-to-job/${jobId}`)
        if (result.isSucceeded) {
            countCV = result.resources;
            let total = result.resources;
            $("#total_cv").text(total);
        }
    } catch (e) {
        console.error(e);
    }
}
async function getTotalCVCandidateApply() {
    try {
        let result = await httpService.getAsync(`api/candidate-apply-job/get-total-cv-candidate-apply-to-job/${jobId}`)
        if (result.isSucceeded) {
            let total = result.resources;
            $("#total_cv_candidate_apply").text(total);
        }
    } catch (e) {
        console.error(e);
    }
}

$(document).on("click", ".btn_add_interview", function (e) {
    let id = $(this).attr("data-id");
    if (id) {
        updatingObj = table.ajax.json().data.find(c => c.id == id);
        if (updatingObj) {
            $("#modal_create_interview").modal("show");
            $("#modal_create_interview input, #modal_create_interview textarea").val(null).trigger("change");
        }
    }
})

$("#modal_create_interview .btn_submit").on("click", async function (e) {
    swal.fire({
        title: "Ứng viên",
        html: "Bạn có chắc chắn muốn tạo lịch hẹn phỏng vấn với ứng viên này ứng viên ?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                let obj = {
                    name: $("#interview_name").val(),
                    description: $("#interview_schedule_description").val(),
                    interviewscheduleTypeId: $("#interview_type").val(),
                    interviewscheduleStatusId: $("#interview_status").val(),
                    candidateApplyJobId: updatingObj.id,
                    link: $("#interview_link").val(),
                    stateDate: moment($("#startDate_value").val(), "DD-MM-YYYY HH:mm").format("YYYY-MM-DD HH:mm"),
                    endDate: moment($("#endDate_value").val(), "DD-MM-YYYY HH:mm").format("YYYY-MM-DD HH:mm"),
                    location: $("#interview_location").val(),
                    search: ""
                };
                //validate
                var errors = [];
                if (obj.name == '' || obj.name.length == 0) {
                    errors.push('Tên lịch hẹn phỏng vấn không được để trống');
                }

                if (obj.location == '' || obj.location.length == 0) {
                    errors.push('Địa điểm lịch hẹn phỏng vấn không được để trống');
                }

                if (obj.link == '' || obj.link.length == 0) {
                    errors.push('Link lịch hẹn phỏng vấn không được để trống');
                }

                if (obj.stateDate == '' || obj.stateDate.length == 0) {
                    errors.push('Thời gian bắt đầu lịch hẹn phỏng vấn không được để trống');
                }

                if (obj.endDate == '' || obj.endDate.length == 0) {
                    errors.push('Thời gian kết thúc lịch hẹn phỏng vấn không được để trống');
                }

                if (obj.link.length > 500) {
                    errors.push('Link lịch hẹn phỏng vấn không được vượt quá 500 ký tự');
                }
                if (obj.description.length > 500) {
                    errors.push('Ghi chú lịch hẹn phỏng vấn không được vượt quá 500 ký tự');
                }
                if (obj.location.length > 500) {
                    errors.push('Địa điểm lịch hẹn phỏng vấn không được vượt quá 500 ký tự');
                }

                let startDateTimestamp = moment($("#startDate_value").val(), "YYYY-MM-DD HH:mm").unix();
                let endDateTimestamp = moment($("#endDate_value").val(), "YYYY-MM-DD HH:mm").unix();

                if (startDateTimestamp === endDateTimestamp) {
                    errors.push('Thời gian bắt đầu không được trùng với thời gian kết thúc');
                }
                if (startDateTimestamp > endDateTimestamp) {
                    errors.push('Thời gian bắt đầu không được sau thời gian kết thúc');
                }

                if (errors.length > 0) {
                    var errorHtml = `<ul style="text-align:left;">`;
                    $.each(errors, function (index, item) {
                        errorHtml += `<li>${item}</li>`;
                    })
                    errorHtml += `</ul>`;

                    Swal.fire({
                        icon: 'warning',
                        title: 'Tạo lịch hẹn phỏng vấn',
                        html: errorHtml,
                        showCloseButton: false,
                        showConfirmButton: true,
                        focusConfirm: true,
                    })
                }
                else {
                    $("#loading").addClass("show");
                    let result = await httpService.postAsync("api/interview-schedule/add", obj);

                    if (result.isSucceeded) {
                        $("#loading").removeClass("show");
                        Swal.fire("Ứng viên", "Tạo lịch hẹn phỏng vấn thành công!", "success");

                        let objCandidateApplyJob = updatingObj;
                        objCandidateApplyJob.candidateApplyJobStatusId = CandidateApplyStatusId_Interview;
                        let result = await httpService.putAsync("api/candidate-apply-job/change-status", objCandidateApplyJob);
                        $("#loading").removeClass("show");
                        if (result.isSucceeded) {
                            table.draw();
                        }

                        $("#modal_create_interview").modal("hide");
                    }
                    else {
                        $("#loading").removeClass("show");
                        if (result.status == 400) {
                            let contentError = "<ul>";
                            let errorList = result.errors;
                            errorList.forEach(function (item, index) {
                                contentError += "<li class='text-start'>" + item + "</li>";
                            });
                            contentError += "</ul>";
                            let swalSubTitle = "<p class='swal__admin__subtitle'>Tạo lịch hẹn phỏng vấn không thành công</p>";
                            Swal.fire(
                                'Ứng viên' + swalSubTitle,
                                contentError,
                                'warning'
                            );
                        }
                        else {
                            Swal.fire("Tạo lịch hẹn phỏng vấn không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                        }
                    }
                }
            } catch (e) {
                Swal.fire("Tạo lịch hẹn phỏng vấn không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                console.log(e);
                $("#loading").removeClass("show");
            }
        }
    })
});

async function getListInterviewStatus() {
    try {
        let result = await httpService.getAsync(`api/interview-status/list`)
        if (result.isSucceeded) {
            let data = result.resources;
            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                $("#interview_status").append(newOption).trigger('change');
            });
            /*$("#interview_status").select2();*/
        }
    } catch (e) {
        console.error(e);
    }
}

async function getListInterviewType() {
    try {
        let result = await httpService.getAsync(`api/interview-type/list`)
        if (result.isSucceeded) {
            let data = result.resources;
            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                $("#interview_type").append(newOption).trigger('change');

            });
            /*$("#interview_status").select2();*/
        }
    } catch (e) {
        console.error(e);
    }
}

async function initCreateInterview() {
    $("#interview_status #interviee_type").select2();
    getListInterviewStatus();
    getListInterviewType();
    // date picker
    flatpickr("#startDate_value", {
        "locale": "vn", // locale for this instance only
        enableTime: true, // Bật chức năng chọn giờ
        dateFormat: "d-m-Y H:i",
        parseDate: (datestr, format) => {
            return moment(datestr, format, true).toDate();
        },
    });
    flatpickr("#endDate_value", {
        "locale": "vn", // locale for this instance only
        enableTime: true, // Bật chức năng chọn giờ
        dateFormat: "d-m-Y H:i",
        parseDate: (datestr, format) => {
            return moment(datestr, format, true).toDate();
        },
    });
    /**
 * bấm vào icon lịch
 */
    $('#startDate > i.fa').click(() => {
        $('#startDate_value').click();
    })
    $('#endDate > i.fa').click(() => {
        $('#endDate_value').click();
    })
}

initCreateInterview();

async function EmployerViewedCV(item) {
    try {
        let result = await httpService.putAsync(`api/candidate-apply-job/employer-viewed/${item.id}`);
        if (result.isSucceeded) {
            $(`.employerCV[data-target=${item.id}]`).find(".pxp-company-dashboard-job-status").html(`<span class="badge  rounded-pill bg-secondary text-dark">Đã đọc</span>`);
        }
        else {
            console.error(e);
        }
    } catch (e) {
        console.error(e);
    }
}

async function EmployerViewedCV(item) {
    try {
        let result = await httpService.putAsync(`api/candidate-apply-job/employer-viewed/${item.id}`);
        if (result.isSucceeded) {
            $(`.employerCV[data-target=${item.id}]`).find(".pxp-company-dashboard-job-status").html(`<span class="badge  rounded-pill bg-secondary text-dark">Đã đọc</span>`);
        }
        else {
            console.error(e);
        }
    } catch (e) {
        console.error(e);
    }
}

async function initTableServiceHistory() {
    let jobServiceSource = [];
    try {
        let res = await httpService.getAsync(`api/job-service-package/list-all-add-on-of-job/${jobId}`);
        if (res.isSucceeded) {
            jobServiceSource = res.resources;
        }
    } catch (e) {
        console.error(e);
    }
    if (tableServiceHistory) {
        tableServiceHistory.destroy();
    }
    $("#table_history_service tbody").html("").trigger("change");
    jobServiceSource.forEach(function (item) {
        $("#table_history_service tbody").append(
            `
            <tr>
                <td>${item.servicePackageName}</td>
                <td>${item.quantity}</td>
                <td class="text-center">${moment(item.createdTime).format("DD/MM/YYYY HH:mm")}</td>
                <td class="text-center">${moment(item.applyEndDate).format("DD/MM/YYYY HH:mm") }</td>
                <td></td>
            </tr>
        `)
    })
    tableServiceHistory = $("#table_history_service").DataTable({
        language: systemConfig.languageDataTable,
        ordering: false,
        paging: false,
        info: false,
        autoWidth: false
    });
}
async function initTableAddService() {
    serviceSource = [];
    try {
        let res = await httpService.getAsync("api/employer-service-package-employer/list-service-of-employer-add-on");
        if (res.isSucceeded) {
            serviceSource = res.resources;
        }
    } catch (e) {
        console.error(e);
    }
    if (tableAddNewService) {
        tableAddNewService.destroy();
    }
    $("#table_add_service tbody").html("").trigger("change");
    serviceSource.forEach(function (item) {
        $("#table_add_service tbody").append(
            `
            <tr>
                <td class="column-index">#${item.orderId}</td>
                <td>${item.servicePackageName}</td>
                <td>${item.quantity}</td>
                <td class="column-add-service"><div class="d-flex flex-end">
                        <button class="btn btn-icon btn-outline btn-active-color-primary minus-btn" data-order-id="${item.orderId}" data-id="${item.servicePackageId}" type="button">
                            <i class="fa-solid fa-square-minus"></i>

                        </button>
                        <input type="number" class="form-control  text-end input-quantity" readonly data-order-id="${item.orderId}" id="input-quantity-${item.servicePackageId}" data-id="${item.servicePackageId}" value="1" min="1" max="${item.quantity}">
                        <button class="btn btn-icon btn-outline btn-active-color-primary plus-btn" data-order-id="${item.orderId}"  data-id="${item.servicePackageId}" type="button">
                            <i class="fa-solid fa-square-plus"></i>
                        </button></div></td>
                <td class="column-action">
                    <button data-id="${item.servicePackageId}" data-order-id="${item.orderId}" class="btn btn-outline-warning text-nowrap btn_add_service">
                        Kích hoạt
                    </button>
                </td>
            </tr>
        `)
    })

    tableAddNewService = $("#table_add_service").DataTable({
        language: systemConfig.languageDataTable,
        ordering: false,
        paging: false,
        info: false,
        autoWidth: false
    });
    //trigger
    $("#table_add_service .plus-btn").on("click", function (e) {
        e.preventDefault();
        let dataId = $(this).attr("data-id");
        let orderId = $(this).attr("data-order-id");
        if (dataId) {
            let element = $(`#input-quantity-${dataId}[data-order-id=${orderId}]`);
            let value = parseInt(element.val())+1;
            let max = parseInt(element.attr("max"));
            if (max) {
                if (value > max) {
                    value = max;
                }
            }
            element.val(value);
        }
    });
    $("#table_add_service .minus-btn").on("click", function (e) {
        e.preventDefault();
        let dataId = $(this).attr("data-id");
        let orderId = $(this).attr("data-order-id");
        if (dataId) {
            let element = $(`#input-quantity-${dataId}[data-order-id=${orderId}]`);
            let value = parseInt(element.val())-1;
            let min = parseInt(element.attr("min"));
            if (min) {
                if (value < min) {
                    value = min;
                }
            }
            element.val(value);
        }
    });

    $(".btn_add_service").on("click", function (e) {
        e.preventDefault();
        let serviceId = $(this).attr("data-id");
        let orderId = $(this).attr("data-order-id");
        let service = serviceSource.find(c => serviceId == c.servicePackageId && orderId == c.orderId);
        if (service) {
            let quantity = $(`#input-quantity-${serviceId}[data-order-id=${orderId}]`).val();
            let obj = {
                'jobId': jobId,
                'quantity': quantity,
                'orderId': orderId,
                'employerServicePackageId': serviceId
            };
            swal.fire({
                title: "Quản lý tin tuyển dụng",
                html: `Bạn có chắc chắn muốn kích hoạt <b>${quantity}</b> dịch vụ <b>${service.servicePackageName}</b>?`,
                icon: 'info',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#443',
                cancelButtonText: 'Hủy',
                confirmButtonText: 'Lưu'
            }).then(async (result) => {
                if (result.isConfirmed) {
                    $("#loading").addClass("show");
                    try {
                        let result = await httpService.postAsync(`api/job-service-package/add-service-pack-to-job`,obj)
                        if (result.isSucceeded) {
                            if (result.status == 200) {
                                Swal.fire(
                                    'Quản lý tin tuyển dụng',
                                    'Dịch vụ <b>' + service.servicePackageName + ' </b> đã được kích hoạt thành công thành công.',
                                    'success'
                                );
                                initTableAddService();
                            }
                        }
                        else {
                            Swal.fire(
                                'Quản lý tin tuyển dụng',
                                'Kích hoạt dịch vụ không thành công, <br> vui lòng thử lại sau!',
                                'error'
                            );
                        }
                        $("#loading").removeClass("show");
                    }
                    catch (e) {
                        console.error(e);
                        if (e.status === 401) {
                            Swal.fire(
                                'Quản lý tin tuyển dụng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Quản lý tin tuyển dụng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            Swal.fire(
                                'Quản lý tin tuyển dụng',
                                'Kích hoạt dịch vụ không thành công, <br> vui lòng thử lại sau!',
                                'error'
                            );
                        }
                        $("#loading").removeClass("show");
                    }
                }
            })
        }
       
    })
}