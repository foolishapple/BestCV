var dataSource = [];
var showItem = ["jobName", "companyName", "jobCategoryName", "jobTypeName", "createdTime"];
var table;
var linked1, linked2;

$(document).ready(function () {
    $.when(
        loadData()
    ).done(function () {
        $("#loading").removeClass("show");

        $("#table_candidate_save_job").on("click", ".btn_delete", function () {
            let itemid = $(this).attr("data-id");
            deleteNoti(itemid);
        });
    })
})

async function loadData() {
    try {
        let res = await httpService.postAsync(`api/candidate-viewed-job/get-list-viewed-job`);
        if (res.isSucceeded) {
            if (res.status == 200) {
                dataSource = res.resources;
                console.log(dataSource);
                loadTableNotification();
                initTableNotification();
            }
        }
    }
    catch (e) {
        dataSource = [];
        loadTableNotification();
        initTableNotification();
        Swal.fire({
            icon: 'warning',
            title: 'Lấy dữ liệu thất bại',
            html: 'Có lỗi xảy ra khi lấy danh sách công việc đã xem, vui lòng thử lại',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        })
    }
}

function loadTableNotification() {
    //var index = 0;
    $("#table_candidate_save_job tbody").html("");
    dataSource.forEach(function (item, index) {
        var rowContent = "<tr>";
        //debugger;
        showItem.forEach(function (key) {
            if (item[key]) {
                if (key == "createdTime") {
                    rowContent += `<td style="width: 20%;" class="row-${item.id}-column column-${key}">
                                        <div class="pxp-candidate-dashboard-job-date">${moment(item[key], "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss")}</div>
                                    </td>`
                }
                else {
                    rowContent += `<td>
                        
                            <div class="row-${item.id}-column column-${key}">${item[key]}</div>
                        
                    </td>
                    `
                }
            }
            else {
                rowContent += `<td class="row-${item.id}-column column-${key}" property="${key}" '></td>`;
            }
        })

        rowContent += `<td class="row-${item.id}-column" property="">
                            <div class="pxp-dashboard-table-options">
                                <ul class="list-unstyled">
                                    <li><button title="Delete" class="btn_delete" data-id="${item.id}"><span class="fa fa-trash-o"></span></button></li>
                                </ul>
                            </div>
                        </td>`;
        rowContent += `</tr>`;

        $(rowContent).appendTo($("#table_candidate_save_job tbody")).trigger("change");
    })
}

function initTableNotification() {
    table = $('#table_candidate_save_job').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        'order': [
            [4, 'desc']
        ],
        columnDefs: [
            //{ targets: "no-sort", orderable: false },
            { targets: [-1], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }
                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#table_candidate_save_job tfoot').html("");
            $("#table_candidate_save_job thead:nth-child(1) tr").clone(true).appendTo("#table_candidate_save_job tfoot");
            var data = $('#table_candidate_save_job').DataTable();
            $('.pxp-candidate-dashboard-jobs-search-results').html(
                data.page.info().recordsDisplay + ' công việc'
            );
        }
    });
}

function refreshTable() {
    reGenDataTable(table, function () {
        $("#table_search_all").val(null).trigger("change");
        loadData();
    })
}

$("#table_search_all").on('keyup', function (e) {
    if (e.keyCode == 13) {
        table.search($(this).val()).draw();
    }
});

function tableSearch() {
    table.column(1).search($("#table_candidate_save_job thead:nth-child(1) tr th:nth-child(1) input").val());
    table.column(2).search($("#table_candidate_save_job thead:nth-child(2) tr th:nth-child(2) input").val());
    table.column(3).search($("#table_candidate_save_job thead:nth-child(3) tr th:nth-child(3) input").val());
    table.column(4).search($("#table_candidate_save_job thead:nth-child(4) tr th:nth-child(4) input").val());
    table.draw();
}

async function deleteNoti(id) {
    let item = dataSource.find(c => c.id == id);
    swal.fire({
        title: 'Xóa công việc đã xem',
        html: 'Bạn có chắc chắn muốn xóa công việc đã xem <br><b>' + item.jobName + '</b>?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then(async (result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");

            try {
                let result = await httpService.deleteAsync(`api/candidate-viewed-job/delete/${id}`);
                if (result.isSucceeded) {
                    if (result.status == 200) {
                        Swal.fire(
                            'Xóa công việc đã xem',
                            'Công việc đã xem đã được xóa thành công',
                            'success'
                        );
                        refreshTable();
                    }
                }
                else {
                    Swal.fire(
                        'Xóa công việc đã xem',
                        'Xóa công việc đã xem không thành công, vui lòng thử lại!',
                        'error'
                    );
                }
                $("#loading").removeClass("show");
            }
            catch (e) {
                Swal.fire(
                    'Xóa công việc đã xem',
                    'Có lỗi xảy ra, vui lòng thử lại sau!',
                    'error'
                );
                $("#loading").removeClass("show");
            }
        }
    })
}