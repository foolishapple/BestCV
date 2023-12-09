
"use strict";
var table;
var recruitmentCampaginStatus = [];
async function loadRecruitmentCampagin() {
    let dataSource = [];
    try {
        let result = await httpService.getAsync("api/recruitment-campaign/list-to-employer");
        if (result.isSucceeded) {
            dataSource = result.resources;
        }
    } catch (e) {
        console.error(e);
    }
    dataSource.forEach(function (item) {
        $("#table_recruitment_campagin tbody").append(
            `<tr>
        <td>
        <div class="d-flex flex-column pxp_name">
                                <span>${item.name}</span>
                                <div class="mt-1">
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${item.id}</span>  <span class="pxp_status">Được tạo vào lúc ${moment(item.createdTime).format("DD/MM/YYYY HH:mm")} </span>
                                </div>
                            </div>
        </td>
        <td class="text-end"><a href="/dang-tin-tuyen-dung?recruitmentCampaginId=${item.id}" class="btn btn-warning btn-md btn-buy" >Đăng tin</a></td>
        </tr>`)
    });
    table = $("#table_recruitment_campagin").DataTable({
        language: systemConfig.languageDataTable,
        ordering: false,
        paging: false,
        info: false
    });
    $("#search_all_recruitment_campagin").on("keypress", function (e) {
        if (e.which == 13) {
            let text = $(this).val().trim();
            table.search(text).draw();
        }
    });
    $("#btn_search_all_recruitment_campagin").on("click", function (e) {
        let text = $("#search_all_recruitment_campagin").val().trim();
        table.search(text).draw();
    });
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
    $("#campaign-status").html("");
    recruitmentCampaginStatus.forEach(function (item, index) {
        $("#campaign-status").append(new Option(item.name, item.id, index == 0, index == 0));
    })
}
function submit() {
    let obj = {};
    obj.name = $("#campaign-name").val();
    obj.recruitmentCampaignStatusId = $("#campaign-status").val();
    obj.description = $("#campaign-description").val();
    if (validate(obj)) {
        let actionName = obj.id != undefined ? "Cập nhật" : "Thêm mới";
        swal.fire({
            title: "Đăng tin tuyển dụng",
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
                    let result = await httpService.postAsync("api/recruitment-campaign/add-to-employer", obj);
                    $("#loading").removeClass("show");
                    if (result.isSucceeded) {
                        Swal.fire(
                            'Đăng tin tuyển dụng',
                            'Chiến dịch <b>' + obj.name + `</b> đã được ${actionName.toLowerCase()} thành công.`,
                            'success'
                        ).then((res) => {
                            let item = result.resources;
                            window.location.href = `/dang-tin-tuyen-dung?recruitmentCampaginId=${item.id}`;
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
                                    'Đăng tin tuyển dụng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } catch (e) {
                                Swal.fire(
                                    'Đăng tin tuyển dụng' + swalSubTitle,
                                    result.errors,
                                    'warning'
                                );
                            }
                        }
                        else {
                            Swal.fire(
                                'Đăng tin tuyển dụng',
                                `${actionName} chiến dịch không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                    }
                }
                catch (e) {
                    if (e.status === 401) {
                        Swal.fire(
                            'Đăng tin tuyển dụng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Đăng tin tuyển dụng',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        swal.fire(
                            'Đăng tin tuyển dụng',
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
$(document).ready(async function () {
    loadRecruitmentCampagin();
    $.when(await loadSelectRecruitCampaginStatus()).done(function() {
        $("#btn_add_recruitment_campagin").on("click", function () {
            $("#campaign-name").val(null);
            $("#campaign-description").val(null);
            $('#editModal').modal("show");
        })
        $(document).on("click", ".btn_submit", function (e) {
            submit();
        })
    });
});