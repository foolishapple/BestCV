var updatingObj = [];
$(document).ready(function () {
    loadTagType();
    LoadDataServerSide();
    //debugger;
    ////$('#postIsApprovedTable').val("").trigger("change");
    //$("#postIsApprovedTable").select2({
    //    language: "vi",
    //    allowClear: true,
    //    placeholder: "",
    //    language: "vi"
    //});
    $("#btnAddNew").on("click", function () {
        editItem(0);
        //window.open("/tag/create", '_blank').focus();
    })

    $("#tableData").on("click", ".btn-admin-edit", function () {
        var id = parseInt($(this).attr("data-idItem"));
        editItem(id);

    })

    $("#tableData").on("click", ".btn-admin-delete", function () {
        var id = parseInt($(this).attr("data-idItem"));
        deleteItem(id);
    })

    $("#form-submit-tag").on("submit", function (e) {
        e.preventDefault();
        validatetag();
    })

    $("#submitButton").on("click", function () {
        validatetag();
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
            tableSearch();
        }
    })

    $("#searchTagType").val("").trigger('change')
    $("#searchTagType").select2({
        placeholder: ""
    });

    $("#btnTableResetSearch").click(function () {
        $("#searchTagName").val("");
        $("#searchTagType").val("").trigger('change');
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        $(".tableHeaderFilter").val("").trigger("change");
        //linked1.clear();
        //linked2.clear();
        tableSearch();
    });


    const linkedPicker1Element = document.getElementById("fillter_startDate");
    linked1 = new tempusDominus.TempusDominus(linkedPicker1Element, datePickerOption);
    linked2 = new tempusDominus.TempusDominus(document.getElementById("fillter_endDate"), datePickerOption);
    // updateOption
    linked2.updateOptions({
        useCurrent: false,
    });
    //using event listeners
    linkedPicker1Element.addEventListener(tempusDominus.Namespace.events.change, (e) => {
        var minDate = $("#fillter_startDate_value").val() == "" ? undefined : new Date(moment(e.detail.date).add(-1, "d"));
        linked2.updateOptions({
            restrictions: {
                minDate: minDate
            },
        });
    });
    //using subscribe method
    const subscription = linked2.subscribe(tempusDominus.Namespace.events.change, (e) => {
        var maxdate = $("#fillter_endDate_value").val() == "" ? undefined : new Date(moment(e.date).add(1, "d"));
        linked1.updateOptions({
            restrictions: {
                maxDate: maxdate
            },
        });
    });
})
$("#tagName").on("keypress", function (e) {
    if (e.keyCode == 13) {

        e.preventDefault();
        validatetag();

    }
});
async function editItem(id) {
    updatingId = id;

    //autosize.destroy($('#tagType'));
    //autosize($('#tagType'));

    $("#loading").addClass("show");

    if (id > 0) {
        //debugger;
        await getItemById(updatingId);

        if (updatingObj != null && updatingObj != undefined) {
            //set value obj to form
            //console.log(updatingObj);
            $("#tagName").val(updatingObj.name);
            $("#selectTagType").val(updatingObj.tagTypeId).trigger('change');
            $("#createdTime").val(moment(updatingObj.createdTime).format("DD/MM/YYYY HH:mm:ss"));
        }
        else {
            swal.fire(
                'Loại thẻ',
                'Không thể cập nhật thẻ, hãy kiểm tra lại thông tin',
                'Error'
            );
        }
        //var strLengthDes = $("#occupationModalDescription").val().split('\n').length;
        //$("#occupationModalDescription").attr('rows', strLengthDes < 2 ? 2 : strLengthDes);
    } else {
        $("#tagName").val("")
        $("#selectTagType").val("").trigger("change");
        /*$("#occupationColor").val("").trigger("change");*/

        $("#createdTime").val(moment().format("DD/MM/YYYY HH:mm:ss"));
    }

    $("#tagModalTitle").text(id > 0 ? "Cập nhật thẻ" : "Thêm mới thẻ");
    $("#loading").removeClass("show");
    $("#tagModal").modal("show");
}

//function getItemById(id) {
//    const result = dataSource.find(item => parseInt(item.id) === parseInt(id));
//    return result;
//}
async function getItemById(id) {
    try {
        let result = await httpService.getAsync('api/tag/detail/' + id);
        if (result.isSucceeded) {
            updatingObj = result.resources;
        }
    } catch (e) {
        console.error(e)
    }
}

async function deleteItem(id) {
    //debugger;
    await getItemById(id);
    swal.fire({
        title: 'Xóa thẻ',
        html: 'Bạn có chắc muốn xóa thẻ <b>' + updatingObj.name + '</b>',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/tag/delete?id=" + id,
                type: "DELETE",
                success: function (response) {
                    $("#loading").removeClass("show");
                    if (response.isSucceeded) {
                        if (response.status == 200) {
                            Swal.fire(
                                'Xóa thẻ',
                                'thẻ đã được xóa thành công.',
                                'success'
                            ).then((result) => {
                                /*window.location.reload();*/
                                regenTableServerSide();
                            });
                        }
                    };
                    //if (response.status == 400) {
                    //    if (response.errors != null) {
                    //        var contentError = "<ul>";
                    //        response.errors.forEach(function (item, index) {
                    //            contentError += "<li class='text-start pb-2'>" + item + "</li>";
                    //        })
                    //        contentError += "</ul>";
                    //        Swal.fire(
                    //            'Thẻ <p class="swal__admin__subtitle"> Xóa không thành công </p>',
                    //            contentError,
                    //            'warning'
                    //        );
                    //    } else {
                    //        Swal.fire(
                    //            'Lưu ý',
                    //            response.message,
                    //            'warning'
                    //        )
                    //    }
                    //}
                    //else {
                    //    Swal.fire(
                    //        'Lưu ý',
                    //        response.message,
                    //        'warning'
                    //    )
                    //}

                },
                error: function (e) {
                    $("#loading").removeClass("show");
                    if (e.status === 401) {
                        Swal.fire(
                            'Quản lý thẻ ',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Quản lý thẻ ',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Xóa thẻ',
                            'Xóa thẻ không thành công, <br> vui lòng thử lại sau!',
                            'error'
                        );
                    }

                }
            })
        }
    })
}

function loadTagType() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/tag-Type/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            console.log(response);
            var data = response.resources;
            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                var newOption2 = new Option(data.text, data.id, false, false);
                var newOption3 = new Option(data.text, data.id, false, false);
                //$('#selectTagType').append(newOption);
                $('#searchTagType').append(newOption2);

                $('#selectTagType').append(newOption3);


                //$("#loading").removeClass("show");

            });
            $("#selectTagType").select2();
            //$('#searchSelectTagType').val("").trigger("change");
            $('#searchTagType').select2({
                allowClear: true,
                placeholder: ""
            });

        },
        error: function (e) {
            if (e.status === 401) {
                Swal.fire(
                    'Quản lý tag',
                    'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                    'error'
                ).then(function () {
                    window.location.href = "/home/login";
                });
            }
            else if (e.status == 403) {
                Swal.fire(
                    'Quản lý tag',
                    'Bạn không có quyền sử dụng tính năng này.',
                    'error'
                );
            }
            else {
                Swal.fire(
                    'Quản lý tag',
                    'Không thể hiển thị loại tag, hãy kiểm tra lại thông tin.',
                    'error'
                );
            }
        }
    });
}

function LoadDataServerSide() {
    table = $("#tableData").DataTable({
        // Design Assets
        autoWidth: true,
        // ServerSide Setups
        processing: true,
        serverSide: true,
        // Paging Setups
        paging: true,
        // Searching Setups
        searching: { regex: true },
        // Ajax Filter
        ajax: {
            url: systemConfig.defaultAPIURL + "api/tag/list-tag-aggregates-async",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        // Columns Setups
        columns: [
            {
                data: 'id',
                render: function (data, type, row, meta) {
                    var info = table.page.info();
                    return meta.row + 1 + info.page * info.length;
                }
            },

            {
                data: "name",
                render: function (data, type, row, meta) {
                    return `<span id='row` + row.id + `-column-name'>` + data + `</span>`;
                }
            },

            {
                data: "tagTypeName",
                render: function (data, type, row, meta) {
                    return `<span id='row` + row.id + `-column-tagTypeName'>` + data + `</span>`;
                }
            },

            {
                data: "createdTime",
                render: function (data, type, row, meta) {
                    return `<span id='row` + row.id + `-column-createdTime' class='font-datetime'>` + moment(data).format("DD/MM/YYYY HH:MM:ss") + `</span>`;
                }
            },
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return `<div class='d-flex justify-content-center'>`
                        + `<button class="btn btn-icon me-2 btn-admin-edit" title='Cập nhật' data-idItem='` + data + `'><span class='svg-icon-primary svg-icon  svg-icon-1'> ` + systemConfig.editIcon + ` </span></button>`
                        + `<button class="btn btn-icon ms-2 btn-admin-delete"  title='Xóa' data-idItem='` + data + `' ><span class='svg-icon-danger svg-icon  svg-icon-1'>` + systemConfig.deleteIcon + `</span></button></div>`;
                }
            },
        ],
        // Column Definitions
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
            { targets: "date-type", type: "date-eu" }
        ],

        lengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        language: systemConfig.languageDataTable,
        order: [
            [3, 'asc']
        ],
        drawCallback: function () {
            // Clone phần order
            $("#tableData tfoot").html("");
            $("#tableData thead:first-child tr").clone(true).appendTo("#tableData tfoot");
            //hiển thị search cột
            $("#rowSearch").removeClass("d-none");
            //loadSVG();


        }
    });
    //$("#loading").removeClass("show");
}

function validatetag() {
    var errorList = [];
    if ($("#tagName").val().length == 0) {
        errorList.push("Tên không được bỏ trống.");
    } else if ($("#tagName").val().length > 255) {
        errorList.push("Tên không được dài quá 255 ký tự.");
    }
    //if ($("#tagType").val() == null) {
    //    errorList.push("Loại tag không được bỏ trống.")
    //}

    var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");

    if (errorList.length > 0) {
        var contentError = "<ul>";
        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName + " không thành công</p>";
        Swal.fire(
            'Thẻ' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        submit();
    }
}

function submit() {
    var obj = {
        name: ($("#tagName").val() != '' ? $("#tagName").val().trim() : ""),
        tagTypeId: ($("#selectTagType").val() != '' ? $("#selectTagType").val() : ""),
    }

    if (updatingId > 0) {
        obj.id = updatingId;
    }

    var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");

    swal.fire({
        title: actionName + " tag",
        html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " tag <b>" + $("#tagName").val() + '</b>?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");

            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/tag/update",
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (responseData) {
                        $("#loading").removeClass("show");

                        // debugger;
                        if (responseData.isSucceeded) {
                            Swal.fire(
                                'Cập nhật tag',
                                'Tag <b>' + $("#tagName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            ).then((result) => {
                                $("#tagModal").modal("hide");
                                //window.location.reload();
                                regenTableServerSide();
                            });
                        }

                        if (responseData.status == 403) {
                            if (responseData.errors != null) {
                                var contentError = "<ul>";
                                responseData.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";

                                var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";

                                Swal.fire(
                                    'Thẻ' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                Swal.fire(
                                    'Thẻ' + swalSubTitle,
                                    responseData.title,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");

                        if (e.status === 401) {
                            Swal.fire(
                                'Quản lý thẻ',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Quản lý thẻ',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Quản lý thẻ',
                                'không thể cập nhật tag, hãy kiểm tra lại thông tin.',
                                'error'
                            );
                        }

                    }
                });
            };

            //CALL AJAX TO CREATE
            if (updatingId == 0) {
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/tag/add",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (responseData) {
                        $("#loading").removeClass("show");

                        if (responseData.isSucceeded) {
                            Swal.fire(
                                'Thêm mới thẻ',
                                'Thẻ <b>' + $("#tagName").val() + '</b> đã được thêm mới thành công.',
                                'success'
                            ).then(function () {
                                $("#tagModal").modal("hide");
                                //window.location.reload();
                                regenTableServerSide();
                            });
                        }

                        if (responseData.status == 403) {
                            //console.log(responseData);
                            if (responseData.errors != null) {
                                var contentError = "<ul>";
                                responseData.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                var swalSubTitle = "<p class='swal__admin__subtitle'>" + actionName.toLowerCase() + " không thành công</p>";
                                Swal.fire(
                                    'Thẻ' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            } else {
                                Swal.fire(
                                    'Lưu ý',
                                    responseData.title,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        if (e.status === 401) {
                            Swal.fire(
                                'Quản lý thẻ',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Quản lý thẻ',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {

                            Swal.fire(
                                'Quản lý thẻ',
                                'Không thể thêm mới tag, hãy kiểm tra lại thông tin.',
                                'error'
                            );
                        }
                    }
                });
            }
        }
    });

}

function tableSearch() {
    //debugger;
    table.column(1).search($("#searchTagName").val().trim());
    table.column(2).search($("#searchTagType").val().toString());
    if ($("#fillter_startDate_value").val().length > 0 || $("#fillter_endDate_value").val().length > 0) {
        //debugger;
        var minDate = $("#fillter_startDate_value").val();
        var maxDate = $("#fillter_endDate_value").val();
        let searchDateArrs = [];

        if (minDate.length > 0) {
            searchDateArrs.push(moment(minDate, "DD/MM/YYYY").format("YYYY-MM-DD 00:00:00"))

        }
        else {
            searchDateArrs.push("")
        }
        if (maxDate.length > 0) {
            searchDateArrs.push(moment(maxDate, "DD/MM/YYYY").format("YYYY-MM-DD 23:59:59"))
        }
        else {
            searchDateArrs.push("")
        }
        table.column(3).search(searchDateArrs.toString());
    }
    else {
        table.column(3).search("")
    }


    table.draw();
}





function regenTableServerSide() {
    table.destroy();
    $("#tableData tbody").html('');
    $("#searchSelectTagType").val("").trigger("change");
    $("#searchTagName").val("");
    $("#tableDataSearchInput").val("");
    //fromDate.clear();
    //toDate.clear();
    LoadDataServerSide();
}