﻿
$(document).ready(function () {
    loadData();
    $("#btnAddNew").on("click", function () {
        editItem(0);
    })

    $("#tableData").on("click", ".btn-admin-edit", function () {
        var id = parseInt($(this).attr("data-idItem"));
        editItem(id);
    })

    $("#tableData").on("click", ".btn-admin-delete", function () {
        var id = parseInt($(this).attr("data-idItem"));
        deleteItem(id);
    })

    $("#submitButton").on("click", function () {
        validateSkillLevel();
    })

    $("#form-submit-skill-level").on("submit", function (e) {
        e.preventDefault();
        validateskillLevel();
    })


    $("#table_search_all").on('keyup', function (e) {
        if (e.keyCode == 13) {
            table.search($(this).val()).draw();
        }
    });
    $("#btnTableSearch").click(function () {
        tableSearch();
    });


    $("#tableData thead:nth-child(2)").find("input").keypress(function (e) {
        let key = e.which;
        if (key == 13) {
            $("#btnTableSearch").click();
        }
    })


    $("#btnTableResetSearch").click(function () {
        $("#searchPostStatusName").val("");
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        //linked1.clear();
        //linked2.clear();
        tableSearch();
    });
})

async function editItem(id) {
    updatingId = id;

    autosize.destroy($('#skillLevelModalDescription'));
    autosize($('#skillLevelModalDescription'));

    $("#loading").addClass("show");

    if (id > 0) {
        updatingObj = getItemById(updatingId);

        if (updatingObj != null && updatingObj != undefined) {
            //set value obj to form
            //console.log(updatingObj);
            $("#skillLevelName").val(updatingObj.name);
            $("#skillLevelDescription").text(updatingObj.description);
            $("#createdTime").val(moment(updatingObj.createdTime).format("DD/MM/YYYY HH:mm:ss"));
        }
        else {
            swal.fire(
                'Mức độ kỹ năng',
                'Không thể cập nhật kỹ năng, hãy kiểm tra lại thông tin',
                'Error'
            );
        }
        //var strLengthDes = $("#skillLevelModalDescription").val().split('\n').length;
        //$("#skillLevelModalDescription").attr('rows', strLengthDes < 2 ? 2 : strLengthDes);
    } else {
        $("#skillLevelName").val("").trigger("change");
        $("#skillLevelModalDescription").val("").trigger("change");
        /*$("#skillLevelColor").val("").trigger("change");*/

        $("#createdTime").val(moment().format("DD/MM/YYYY HH:mm:ss"));
    }

    $("#skillLevelModalTitle").text(id > 0 ? "Cập nhật kỹ năng" : "Thêm mới kỹ năng");
    $("#loading").removeClass("show");
    $("#skillLevelModal").modal("show");
}

function getItemById(id) {
    const result = dataSource.find(item => parseInt(item.id) === parseInt(id));
    return result;
}

function loadData() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/skill-level/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            dataSource = response.resources;
            loadTable();
            if (tableUpdating === 0) {
                initTable();
            }
        },
        error: function (e) {
            initTable();
        }
    });
}
$("#skillLevelName").on("keypress", function (e) {
    if (e.keyCode == 13) {

        e.preventDefault();
        validateSkillLevel();

    }
});
function initTable() {
    table = $('#tableData').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0, -1], orderable: false },
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

        'order': [
            [3, 'desc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableData tfoot').html("");
            $("#tableData thead:nth-child(1) tr").clone(true).appendTo("#tableData tfoot");
            $("#rowSearch").removeClass("d-none");
        }


    });

    table.on('order.dt search.dt', function () {
        table.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function loadTable() {
    var index = 0;
    $("#tableData tbody").html("");
    dataSource.forEach(function (item, index) {
        var rowContent = "<tr>";
        rowContent += "<td style='text-align: center;'>" + (index + 1) + "</td>";
        showItem.forEach(function (key) {
            if (item[key]) {
                if (key == "createdTime") {
                    rowContent += "<td class=' row-" + item.id + "-column column-" + key + "' property='" + key + "'data-sort= '" + moment(item[key]).format("YYYYMMDDHHmmss") +"'>" + moment(item[key], "YYYY-MM-DD HH:mm:ss").format("DD/MM/YYYY HH:mm:ss") + "</td>"
                }
                else {
                    rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + item[key] + "</td>";
                }
            }
            else {
                rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + "</td>";
            }
        })
        rowContent += "<td class='row" + item.id + "-column column-' property=''>"
            + "<div class='d-flex justify-content-center'>";
        rowContent += "<button  type='button' class='btn btn-icon btn-admin-edit' data-idItem='" + item.id + "'><span class='svg-icon-primary svg-icon  svg-icon-1'> " + systemConfig.editIcon + " </span></button>"

        rowContent += "<button type='button' class='btn btn-icon btn-admin-delete' data-idItem='" + item.id + "' ><span class='svg-icon-danger svg-icon  svg-icon-1'>" + systemConfig.deleteIcon + "</span></button></div>";

        rowContent += "</div></td ></tr>";
        $(rowContent).appendTo($("#tableData tbody"));
    })
}

function deleteItem(id) {
    updatingObj = getItemById(id);
    swal.fire({
        title: 'Xóa kỹ năng',
        html: 'Bạn có chắc chắn muốn xóa kỹ năng <b>' + updatingObj.name + '</b>',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/skill-level/delete/" + id,
                type: "DELETE",
                success: function (response) {
                    $("#loading").removeClass("show");
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Xóa mức độ kỹ năng',
                            'Mức độ kỹ năng <b>' + updatingObj.name + '</b> đã được xóa thành công.',
                            'success'
                        );
                        reGenTable();
                    } else {
                        Swal.fire(
                            'Xóa kỹ năng',
                            'Xóa kỹ năng không thành công, <br> vui lòng thử lại sau!',
                            'Error'
                        )
                    }
                },
                error: function (e) {
                    $("#loading").removeClass("show");
                    if (e.status === 401) {
                        Swal.fire(
                            'Mức độ kỹ năng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'Error'
                        ).then(function () {
                            window.localtion.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Mức độ kỹ năng',
                            'Bạn không có quyền sử dụng tính năng này',
                            'Error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Xóa kỹ năng',
                            'Xóa kỹ năng không thành công, <br> vui lòng thử lại sau!',
                            'Error'
                        );
                    }
                }
            })
        }
    })
}

function validateSkillLevel() {
    var errorList = [];
    if ($("#skillLevelName").val().length == 0) {
        errorList.push("Tên không được để trống.");
    } else if ($("#skillLevelName").val().length > 255) {
        errorList.push("Tên không được dài quá 255 ký tự.");
    }
    if ($("#skillLevelDescription").val().length > 500) {
        errorList.push("Mô tả không được dài quá 500 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");
        var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName + " không thành công</p>";
        Swal.fire(
            'Mức độ kỹ năng' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        submit();
    }
}

function submit() {
    var obj = {
        name: $("#skillLevelName").val().trim(),
        description: $("#skillLevelDescription").val()
    }

    if (updatingId > 0) {
        obj.id = updatingId;
    }
    var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");

    Swal.fire({
        title: actionName + "trình độ kỹ năng",
        html: "Bạn có chắc chắn muốn" + actionName.toLowerCase() + "kỹ năng <b>" + $("#skillLevelName").val() + '</b>?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Lưu',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");

            //CALL AJAX TO CREATE
            if (updatingId == 0) {
                $.ajax({
                    type: 'POST',
                    url: systemConfig.defaultAPIURL + "api/skill-level/add",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");
                        if (response.isSucceeded) {
                            Swal.fire(
                                'Thêm mới kỹ năng',
                                'Mức độ kỹ năng <b>' + $("#skillLevelName").val() + '</b> đã được thêm mới thành công.',
                                'success'
                            ).then(function () {
                                $("#skillLevelModal").modal("hide");
                                //window.location.reload();
                                reGenTable();
                            });
                        } else {
                            Swal.fire(
                                'Thêm mới kỹ năng',
                                'Thêm mới kỹ năng không thành công, <br> vui lòng thử lại sau!',
                                'Error'
                            )
                        }

                        if (response.status == 400) {
                            var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + "không thành công</p>";
                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";

                                Swal.fire(
                                    'Mức độ kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                var contentError = `<ul><li class='text-start'>` + response.message + `</li></ul>`;
                                Swal.fire(
                                    'Mức độ kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }

                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        if (e.status === 401) {
                            Swal.fire(
                                'Quản lý kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Quản lý kỹ năng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            Swal.fire(
                                'Quản lý kỹ năng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                    }
                });
            };

            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $.ajax({
                    type: 'PUT',
                    url: systemConfig.defaultAPIURL + "api/skill-level/update",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");

                        if (response.isSucceeded) {
                            Swal.fire(
                                'Cập nhật kỹ năng',
                                'Mức độ kỹ năng <b>' + $("#skillLevelName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            ).then((result) => {
                                $("#skillLevelModal").modal("hide");
                                reGenTable();
                            });
                        } else {
                            Swal.fire(
                                'Cập nhật kỹ năng',
                                'Cập nhật kỹ năng không thành công, <br> vui lòng thử lại sau!',
                                'Error'
                            )
                        }

                        if (response.status == 400) {
                            var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";

                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";

                                Swal.fire(
                                    'Mức độ kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                var contentError = `<ul><li class='text-start'>` + response.message + `</li></ul>`;

                                Swal.fire(
                                    'Mức độ kỹ năng' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");

                        if (e.status === 401) {
                            Swal.fire(
                                'Mức độ kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Mức độ kỹ năng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            );
                        }
                    }
                });
            };
        }
    })
}

function tableSearch() {
    table.column(1).search($("#searchskillLevelName").val());
    table.draw();
}

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var date = new Date(moment(data[3], "DD/MM/YYYY HH:mm:ss"));
        var startDate = $("#fillter_startDate_value").val();
        var endDate = $("#fillter_endDate_value").val();
        var min = startDate != "" ? new Date(moment(startDate, "DD/MM/YYYY ").format("YYYY-MM-DD 00:00:00")) : null;
        var max = endDate != "" ? new Date(moment(endDate, "DD/MM/YYYY").format("YYYY-MM-DD 23:59:59")) : null;
        if (
            (min === null && max === null) ||
            (min === null && date <= max) ||
            (min <= date && max === null) ||
            (min <= date && date <= max)
        ) {
            return true;
        }
        return false;
    }
);