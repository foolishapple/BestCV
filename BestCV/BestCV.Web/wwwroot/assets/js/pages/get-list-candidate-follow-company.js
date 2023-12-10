$(document).ready(function () {


    if (localStorage.currentUser) {
        initCandidateFollowCompany();
    }
    else {
        window.location.href = "/";
    }
})

var showItem = ["companyName", "companySizeName", "countJob", "logo", "createdTime"];
var table;
var dataSource = [];
var dataSourceDashBoard = [];
var updatingObj = {};
var table;
var linked1, linked2;
var totalPage;
var pageIndex = 1;

async function initCandidateFollowCompany() {
    initTableCandidateFollowCompany();
}

function initTableCandidateFollowCompany() {
    table = $("#table_candidate_follow_company").DataTable({
        language: systemConfig.languageDataTable,
        serverSide: true,
        processing: true,
        paging: true,
        searching: { regex: true },
        order: false,
        autoWidth: false,
        info: false,
        ordering: false,
        ajax: {
            url: `${systemConfig.defaultAPIURL}api/candidate-follow-company/list-candidate-follow-company`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#table_search_all").val().trim() || "";
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
                data: "companyName",
                render: function (data, type, row, meta) {
                    return data;
                }
            },
            {
                data: "companySizeName",
                render: function (data, type, row, meta) {
                    return data;
                }
            },
            {
                data: "countJob",
                render: function (data, type, row, meta) {
                    return data;
                }
            },
            {
                data: "logo",
                render: function (data, type, row, meta) {
                    let pathLogo = systemConfig.defaultStorage_URL + data
                    return `<div>
                        <img src="${pathLogo}" alt="Logo" style="max-width: 150px; height: auto;">
                    </div>`;
                }
            },
            {
                data: "createdTime",
                render: function (data, type, row, meta) {
                    return `<span class='text-nowrap'>${moment(data).format("DD/MM/YYYY HH:mm:ss")}</span>`;
                }
            },
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return `<div class='d-flex justify-content-center'>`
                        + `<button class="btn-admin-delete btn btn-icon" onclick='deleteFollowCompany(${data})'  title='Xóa' data-id='` + data + `' ><span class="fa fa-trash"></span></button></div>`;

                }
            }
           
        ],
        columnDefs: [
            {
                targets: [0],
                className: "text-center"
            }
        ],
        drawCallback: function () {
            $('#table_candidate_follow_company tfoot').html("");
            innitTblPaging("#table_candidate_follow_company_paging");
            initInfo(".pxp-candidate-dashboard-jobs-search-results")
        }
    });
}

function innitTblPagingApplyJob(element) {
    let info = tableApplyJob.page.info();
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
            html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-apply-job page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-apply-job page-link"><i class="fa fa-angle-left"></i></a></li>`;
        }
        for (var i = startPage; i <= endPage; i++) {
            if (i > 0) {
                html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-apply-job ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
            }
        }
        if (pageIndex < totalPage - 1) {
            html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-apply-job page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-apply-job page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
        }
        $(element).html(html);
    }
    else {
        $(element).html("");
    }
}

function initInfo(e) {
    let info = table.page.info();
    $(e).text(`${info.recordsDisplay} công ty`);
}

function getItemById(id) {
    updatingObj = table.ajax.json().data.find(c=>c.id==id);
}

function refreshTable() {
    reGenDataTable(table, function () {
        $("#table_search_all").val(null).trigger("change");
        initTableCompanyFollow();
    })
}
$("#table_search_all").on('keyup', function (e) {
    if (e.keyCode == 13) {
        table.search($(this).val()).draw();
    }
});
function tableSearch() {
    table.column(1).search($("#table_candidate_follow_company thead:nth-child(1) tr th:nth-child(1) input").val());
    table.column(2).search($("#table_candidate_follow_company thead:nth-child(2) tr th:nth-child(2) input").val());
    table.column(3).search($("#table_candidate_follow_company thead:nth-child(3) tr th:nth-child(3) input").val());
    table.column(4).search($("#table_candidate_follow_company thead:nth-child(4) tr th:nth-child(4) input").val());
    table.draw();
}

async function deleteFollowCompany(id) {
    getItemById(id);

    swal.fire({
        title: 'Xóa thông báo',
        html: 'Bạn có chắc chắn muốn xóa thông báo <br><b>' + updatingObj.companyName + '</b>?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then(async (swalResult) => {
        if (swalResult.isConfirmed) {
            // bật loading
            $("#loading").addClass("show");

            try {
                let deleteResult = await httpService.deleteAsync(`api/candidate-follow-company/delete/${id}`);
                if (deleteResult.isSucceeded) {
                    if (deleteResult.status == 200) {
                        Swal.fire(
                            'Xóa thông báo',
                            'Thông báo đã được xóa thành công',
                            'success'
                        ).then((swalResult) => {
                            if (dataSource.length === 1 && pageIndex > 1) {
                                pageIndex--; // Lùi về trang trước

                            }

                            refreshTable();

                        });
                    }
                }
                else {
                    Swal.fire(
                        'Xóa thông báo',
                        'Xóa thông báo không thành công, vui lòng thử lại!',
                        'error'
                    );
                }
                $("#loading").removeClass("show");
            }
            catch (e) {
                Swal.fire(
                    'Xóa thông báo',
                    'Có lỗi xảy ra, vui lòng thử lại sau!',
                    'error'
                );
                $("#loading").removeClass("show");
            }
        }
    })
}