"use strict";
var table;
var recruitmentCampaginStatus = [];
var updatingObj = {};
var parramApproved = [];
$(document).ready(async () => {
    init();
})
$(document).on("change", "[data-bs-toggle=tooltip]", function () {
    let tooltip = new bootstrap.Tooltip(this, {});
})



var dataSource = [];




async function editItem(id) {
    if (id != 0) {
        await getDetail(id);
    }
    else {
        updatingObj = {};
    }
    $("#campaign-name").val(id > 0 ? updatingObj.name : "");
    $("#campaign-status").val(id > 0 ? updatingObj.recruitmentCampaignStatusId : $("#campaign-status option:first-child").attr("value")).trigger("change");
    $("#campaign-description").val(id > 0 ? updatingObj.description : "");
    $("#editModal .modal-title").text(id > 0 ? "Cập nhật chiến dịch" : 'Thêm mới chiến dịch');
    $("#editModal").modal("show");
}
async function init() {
    $.when(await loadSelectRecruitCampaginStatus()).done(() => {
        $(document).on("click", '.bnt_edit', function (e) {
            let itemId = $(this).attr("data-id");
            editItem(itemId);
        });
        $(document).on("click", ".btn_add", function () {
            editItem(0);
        })
        $(document).on("click", ".btn_submit", function (e) {
            submit();
        })

        initTable();
    })
}

function initTable() {
    table = $("#tbl_recruitment_campaign").DataTable({
        language: systemConfig.languageDataTable,
        serverSide: true,
        processing: true,
        paging: true,
        searching: { regex: true },
        order: false,
        autoWidth: false,
        ordering: false,
        ajax: {
            url: `${systemConfig.defaultAPI_URL}api/recruitment-campaign/list-dt-paging`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#search_all").val().trim() || "";
                d.reccruitmentCampaginStatusIds = $("#tbl_fillter_status").val() != null ? [$("#tbl_fillter_status").val()] : [];
                d.isApproveds = parramApproved || [];
                return JSON.stringify(d);
            }
        },
        columns: [
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return ` <div class="form-check form-switch">
                                <input class="form-check-input change_approve" data-id="${row.id}" type="checkbox" role="checkbox" ${row.isApproved ? `checked` : ``}>
                            </div>`;
                }
            },
            {
                data: "title",
                render: function (data, type, row, meta) {
                    let candidates = row.candidates;
                    let rowContent = ``;
                    if (candidates.length > 0) {
                        candidates.forEach(function (item, index) {
                            let photo = item.photo == null || item.photo == "" ? "" : item.photo;
                            if (row.totalCandidate > MAXIMUM_CANDIDATE_SHOW && index == MAXIMUM_CANDIDATE_SHOW - 1) {
                                rowContent += `<div class="candidate_avatar bg-secondary me-1" data-bs-toggle="tooltip" data-bs-placement="top"
                                   data-bs-custom-class="custom-tooltip"
                                   data-bs-title="${item.name}" >
								   <div class="overlay avatar flex-cm" style="height: 30px;"><div class="number text-center" style="height: 30px;
											font-size: 11px;
											line-height: 30px;
										}">+${row.totalCandidate + 1 - MAXIMUM_CANDIDATE_SHOW}</div></div>
								   </div>`;
                            }
                            else {
                                rowContent += `<div class="candidate_avatar bg-secondary me-1 d-flex justify-content-center" data-bs-toggle="tooltip" data-bs-placement="top"
                                   data-bs-custom-class="custom-tooltip"
                                   data-bs-title="${item.name}" style="background-image: url(${systemConfig.defaultStorage_URL + photo})">
								   </div>`;
                            }
                        })
                    }
                    else {
                        rowContent = `<span class="pxp_status text-nowrap">Chưa có CV nào</span>`;
                    }
                    return ` <div class="d-flex flex-column pxp_name">
                                <span><a href="/recruitment-campaign/dashboard/${row.id}"  class="pxp_title" title="${row.name}">${row.name}</a></span>
                                <div class="d-flex flex-row mt-1">
                                    ${rowContent}
                                </div>
                                <div class="mt-1">
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.id} </span>
                                </div>
                            </div>`;
                }
            },
            {
                data: "jobs",
                render: function (data, type, row, meta) {
                    let jobs = row.jobs;
                    let content = ``;
                    jobs.forEach(function (item, index) {
                        content += `<div class="d-flex flex-column pxp_name mb-1">
                                <a href="/tin-tuyen-dung/dashboard/${item.id}" class="pxp_title text_link" title="${item.name}">${item.name}</a>
                                <div class="">
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${item.id}</span><span class="pxp_status ms-2 text-nowrap" ${item.isApproved ? `style="color:${item.statusColor}"` : ``}>${item.isApproved ? item.statusName : `Tin chưa được duyệt`}</span>
                                </div>
                            </div>`;
                    })
                    if (row.totalJob > MAXIMUM_JOB_SHOW) {
                        content += `<div>và ${row.totalJob - MAXIMUM_JOB_SHOW} tin tuyển dụng khác</div>`
                    }
                    if (jobs.length == 0) {
                        content = "Chưa có tin tuyển dụng";
                    }
                    return `<div>${content}<div>`;
                }
            },
            {
                data: "status",
                render: function (data, type, row, meta) {
                    let color = row.statusColor;
                    return `<span  class="badge px-2 rounded-pill" style="color: ${color};background-color: ${customBagdeColor(color)};">${row.statusName}</span>`;
                }
            },
            {
                data: "action",
                render: function (data, type, row, meta) {
                    return `<div class="pxp-dashboard-table-options">
                                <ul class="list-unstyled justify-content-center">
                                    <li><button class="bnt_edit" data-id="${row.id}" title="Chỉnh sửa"><span class="fa fa-pencil"></span></button></li>
                                    <li><button class="btn_postJob" data-id="${row.id}" title="Đăng tin"><span class="fa fa-newspaper text-dark"></span></button></li>
                                </ul>
                            </div>`;
                }
            }
        ],
        columnDefs: [
            {
                targets: [0, 3, -1],
                className: "text-center"
            }
        ],
        drawCallback: function () {
            $('#tbl_recruitment_campaign tfoot').html("");
            $(".candidate_recruitment_campaign").trigger("change");
            innitTblPaging("#candidate_paging");
            initInfo("#tbl_info");
            $("[data-bs-toggle=tooltip]").trigger('change');
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
    $("#tbl_recruitment_campaign").on("keypress", ".fillter_tbl", function (e) {
        if (e.which == 13) {
            table.draw();
        }
    })
    $("#tbl_recruitment_campaign").on("click", ".btn_search", function (e) {
        table.draw();
    })
    $(".btn_search_all").on("click", function () {
        table.draw();
    })
    $(document).on("click", ".btn_postJob", function () {
        let dataId = $(this).attr("data-id");
        window.location.href = `/dang-tin-tuyen-dung?recruitmentCampaginId=${dataId}`;
    })
    $("#tbl_recruitment_campaign").on("click", ".change_approve", function (e) {
        let element = $(this);
        let itemId = element.attr("data-id");
        let checked = element.is(":checked");
        let actionName = checked ? "Bật" : "Tắt"
        if (itemId) {
            let obj = table.ajax.json().data.find(c => c.id == itemId);
            if (obj) {
                obj.isApproved = checked;
                swal.fire({
                    title: "Quản lý chiến dịch",
                    html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " chiến dịch <b>" + obj.name + '</b>?',
                    icon: 'info',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#443',
                    cancelButtonText: 'Hủy',
                    confirmButtonText: 'Lưu'
                }).then(async (result) => {
                    if (result.isConfirmed) {
                        $("#loading").addClass("show");
                        //CALL AJAX TO UPDATE

                        try {
                            let result = await httpService.putAsync("api/recruitment-campaign/change-approved-to-employer", obj);
                            $("#loading").removeClass("show");
                            if (result.isSucceeded) {
                                Swal.fire(
                                    'Quản lý chiến dịch',
                                    'Chiến dịch <b>' + obj.name + `</b> đã được ${actionName.toLowerCase()} thành công.`,
                                    'success'
                                )
                            }
                            else {
                                $(this).prop("checked", !checked);
                                if (result.status == 400) {
                                    let swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";
                                    try {
                                        let contentError = "<ul>";
                                        result.errors.forEach(function (item, index) {
                                            contentError += "<li class='text-start'>" + item + "</li>";
                                        })
                                        contentError += "</ul>";
                                        Swal.fire(
                                            'Quản lý chiến dịch' + swalSubTitle,
                                            contentError,
                                            'warning'
                                        );
                                    } catch (e) {
                                        Swal.fire(
                                            'Quản lý chiến dịch' + swalSubTitle,
                                            result.errors,
                                            'warning'
                                        );
                                    }
                                }
                                else {
                                    Swal.fire(
                                        'Quản lý chiến dịch',
                                        `${actionName} chiến dịch không thành công, <br> vui lòng thử lại sau!`,
                                        'error'
                                    );
                                }
                            }
                        }
                        catch (e) {
                            $(this).prop("checked", !checked);
                            if (e.status === 401) {
                                Swal.fire(
                                    'Quản lý chiến dịch',
                                    'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                    'error'
                                ).then(function () {
                                    window.location.href = "/home/login";
                                });
                            }
                            else if (e.status == 403) {
                                Swal.fire(
                                    'Quản lý chiến dịch',
                                    'Bạn không có quyền sử dụng tính năng này.',
                                    'error'
                                );
                            }
                            else {
                                swal.fire(
                                    'Quản lý chiến dịch',
                                    `${actionName} chiến dịch không thành công, <br> vui lòng thử lại sau!`,
                                    'error'
                                );
                            }
                            $("#loading").removeClass("show");
                            console.error(e);

                        }
                    }
                    else {
                        element.prop("checked", !checked);
                    }
                })
            }
            else {
                element.prop("checked", !checked);
            }
        }
        else {
            element.prop("checked", !checked);
        }
    })
    $("#tbl_fillter_status").on("change", function (e) {
        table.draw();
    })
    $("#tbl_fillter_main").on("change", function (e) {
        let data = ["all", "isOpen"];
        let value = $(this).val();
        switch (value) {
            case 'all':
                parramApproved = []
                break;
            case 'isOpen':
                parramApproved = [true];
                break;
            default:
                parramApproved = [];
                break;
        }
        table.draw();
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
    $(e).text(`${info.recordsDisplay} chiến dịch`);
}

async function getRecruitCampaginStatus() {
    try {
        let result = await httpService.getAsync("api/recruitment-campaign-status/list");
        if (result.isSucceeded) {
            recruitmentCampaginStatus = result.resources;
            recruitmentCampaginStatus.forEach(function (item) {
                item.text = item.name;
            });
        }
        else {
            recruitmentCampaginStatus = [];
        }
    } catch (e) {
        recruitmentCampaginStatus = [];
        console.error(e);
    }
}

async function loadSelectRecruitCampaginStatus() {
    await getRecruitCampaginStatus();
    $("#campaign-status,#tbl_fillter_status").html("");
    recruitmentCampaginStatus.forEach(function (item, index) {
        $("#campaign-status").append(new Option(item.name, item.id, index == 0, index == 0));
    })
    $("#tbl_fillter_status").select2({
        placeholder: "Lọc trạng thái",
        allowClear: true,
        data: recruitmentCampaginStatus,
        templateResult: formatStatusData,
        templateSelection: formatStatusSelection,
        width: "155px"
        //matcher: matchCustom
    })
    $("#tbl_fillter_status").val(null).trigger("change");
}
//matchCustom
function formatStatusData(status) {
    if (status.loading) {
        return status.text;
    }
    let color = status.color;
    var $container = $(
        `<span  class="badge px-2 rounded-pill" style="color: ${color};background-color: ${customBagdeColor(color)};">${status.name}</span>`
    );
    return $container;
}

function formatStatusSelection(status) {
    if (status.id === '') { // adjust for custom placeholder values
        return 'Lọc trạng thái';
    }
    return status.name;
}

async function getDetail(id) {
    try {
        let result = await httpService.getAsync(`api/recruitment-campaign/detail/${id}`);
        if (result.isSucceeded) {
            updatingObj = result.resources;
        }
        else {
            updatingObj = {};
        }
    } catch (e) {
        updatingObj = {};
    }
}
function submit() {
    let obj = updatingObj;
    obj.name = $("#campaign-name").val();
    obj.recruitmentCampaignStatusId = $("#campaign-status").val();
    obj.description = $("#campaign-description").val();
    if (validate(obj)) {
        let actionName = obj.id != undefined ? "Cập nhật" : "Thêm mới";
        swal.fire({
            title: "Quản lý chiến dịch",
            html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " chiến dịch <b>" + obj.name + '</b>?',
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass("show");
                //CALL AJAX TO UPDATE
                try {
                    let result;
                    if (obj.id > 0) {
                        result = await httpService.putAsync("api/recruitment-campaign/update-to-employer", obj);
                    }
                    else {
                        result = await httpService.postAsync("api/recruitment-campaign/add-to-employer", obj);
                    }
                    $("#loading").removeClass("show");
                    if (result.isSucceeded) {
                        Swal.fire(
                            'Quản lý chiến dịch',
                            'Chiến dịch <b>' + obj.name + `</b> đã được ${actionName.toLowerCase()} thành công.`,
                            'success'
                        ).then((result) => {
                            $("#editModal").modal("hide");
                            refreshTable();
                        });
                    }
                    else {
                        if (result.status == 400) {
                            let swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";
                            try {
                                let contentError = "<ul>";
                                result.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                Swal.fire(
                                    'Quản lý chiến dịch' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } catch (e) {
                                Swal.fire(
                                    'Quản lý chiến dịch' + swalSubTitle,
                                    result.errors,
                                    'warning'
                                );
                            }
                        }
                        else {
                            Swal.fire(
                                'Quản lý chiến dịch',
                                `${actionName} chiến dịch không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                    }
                }
                catch (e) {
                    if (e.status === 401) {
                        Swal.fire(
                            'Quản lý chiến dịch',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Quản lý chiến dịch',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        swal.fire(
                            'Quản lý chiến dịch',
                            `${actionName} chiến dịch không thành công, <br> vui lòng thử lại sau!`,
                            'error'
                        );
                    }
                    $("#loading").removeClass("show");
                    console.error(e);
                }
            }
        });
    }
}
function refreshTable() {
    $("#search_all").val("");
    table.page(0).draw();
};
function validate(obj) {
    let errorList = [];
    if (obj.name.trim().length == 0) {
        errorList.push("Tên chiến dịch không được bỏ trống.");
    }

    if (errorList.length > 0) {
        let contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        let actionName = (obj.id > 0 ? "Cập nhật" : "Thêm mới");
        let swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName + "chiến dịch không thành công</p>";
        Swal.fire(
            'Quản lý chiến dịch' + swalSubTitle,
            contentError,
            'warning'
        );
        return false;
    } else {
        return true;
    }
}