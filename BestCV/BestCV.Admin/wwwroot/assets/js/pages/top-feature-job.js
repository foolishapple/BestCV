
$(document).ready(function () {
    loadData();
    loadJobData();
    //loadSelectJob();
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
        validate();
    })

    $("#form-submit-top-feature-job").on("submit", function (e) {
        e.preventDefault();
        validate();
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
        $("#searchTopFeatureJobName").val("").trigger('change');
        $("#searchTopFeatureJobOrderSort").val("");
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        linked1.clear();
        linked2.clear();
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
$("#topFeatureJobOrderSort").on("keypress", function (e) {
    if (e.keyCode == 13) {

        e.preventDefault();
        validate();

    }
});
async function editItem(id) {
    updatingId = id;

    $("#loading").addClass("show");

    if (id > 0) {
        $("#topFeatureJobName").prop("disabled", true);
        updatingObj = await getItemById(updatingId);
        //debugger;

        if (updatingObj != null && updatingObj != undefined) {
            //set value obj to form
            //console.log(updatingObj.jobName);
            //debugger;
            $("#topFeatureJobName").val(updatingObj.jobName).trigger("change");
            $("#topFeatureJobName").select2("trigger", "select", {
                data: {
                    id: updatingObj.jobId,
                    text: updatingObj.jobName
                }
            });
            $("#topFeatureJobOrderSort").val(updatingObj.orderSort).trigger("change");
            $("#createdTime").val(moment(updatingObj.createdTime).format("DD/MM/YYYY HH:mm:ss"));
        }
        else {
            swal.fire(
                'Top việc làm nổi bật',
                'Không thể cập nhật top việc làm nổi bật, hãy kiểm tra lại thông tin.',
                'error'
            );
        }

    } else {
        $("#topFeatureJobName").prop("disabled", false);
        $("#topFeatureJobName").val("").trigger("change");
        $("#topFeatureJobOrderSort").val("").trigger("change");
        $("#createdTime").val(moment().format("DD/MM/YYYY HH:mm:ss"));
    }

    $("#topFeatureJobModalTitle").text(id > 0 ? "Cập nhật top việc làm nổi bật" : "Thêm mới top việc làm nổi bật");
    $("#loading").removeClass("show");
    $("#topFeatureJobModal").modal("show");
}

async function getItemById(id) {
    try {
        const responseData = await $.ajax({
            url: systemConfig.defaultAPIURL + "api/top-feature-job/detail/" + id,
            type: "GET"
        });
        //console.log(responseData);
        return responseData.resources;
    } catch (e) {
        swal.fire(
            'Top việc làm nổi bật',
            'Đã có lỗi xảy ra, hãy kiểm tra lại thông tin.',
            'error'
        );
        return null;
    }
}

function loadData() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/top-feature-job/list-by-orderSort",
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

function initTable() {
    table = $('#tableData').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0, -1, 3, 4], orderable: false },
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
            [2, 'asc']
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
        rowContent += "<td>" + "<span style='display : none;'>" + (item.topFeatureJobId != null ? item.topFeatureJobId : "") + "</span>" + (item.topFeatureJobName != null ? item.topFeatureJobName : "") + "</td>";
        rowContent += "<td>" + item.orderSort + "." + item.subOrderSort + "</td>";
        rowContent += '<td class="text-center order-section-change-up" data-id-row="' + item.id + '" data-event="upper-order-' + item.id + '"><span class="svg-icon svg-icon-success svg-icon-2hx">' + upperIcon + '</span></td>';
        rowContent += '<td class="text-center order-section-change-down" data-id-row="' + item.id + '" data-event="lower-order-' + item.id + '"><span class="svg-icon svg-icon-danger svg-icon-2hx">' + lowerIcon + '</span></td>';
        rowContent += "<td data-sort= '" + moment(item.createdTime).format("YYYYMMDDHHmmss") +"'>" + moment(item.createdTime, "YYYY-MM-DD HH:mm:ss").format("DD/MM/YYYY HH:mm:ss") + "</td>";

        rowContent += "<td class='row" + item.id + "-column column-' property=''>"
            + "<div class='d-flex justify-content-center'>";

        rowContent += "<button  type='button' class='btn btn-icon btn-admin-edit' data-idItem='" + item.id + "'><span class='svg-icon-primary svg-icon  svg-icon-1'> " + systemConfig.editIcon + " </span></button>"

        rowContent += "<button type='button' class='btn btn-icon btn-admin-delete' data-idItem='" + item.id + "' ><span class='svg-icon-danger svg-icon  svg-icon-1'>" + systemConfig.deleteIcon + "</span></button></div>";

        rowContent += "</div></td ></tr>";
        $(rowContent).appendTo($("#tableData tbody"));
    })
}

async function deleteItem(id) {
    var jobName = dataSource.find(item => item.id === id).topFeatureJobName;
    updatingObj = await getItemById(id);
    swal.fire({
        title: 'Xóa top việc làm nổi bật',
        html: 'Bạn có chắc chắn muốn xóa top việc làm nổi bật <b>' + jobName + '</b>?',
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
                url: systemConfig.defaultAPIURL + "api/top-feature-job/delete?id=" + id,
                type: "DELETE",
                success: function (response) {
                    $("#loading").removeClass("show");
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Xóa top việc làm nổi bật',
                            'Top việc làm nổi bật <b>' + $("#topFeatureJobName option:selected").text() + ' </b> đã được xóa thành công.',
                            'success'
                        );
                        reGenTable();
                    }
                    else {
                        if (response.status == 400) {
                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start pb-2'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                Swal.fire(
                                    'Top việc làm nổi bật <p class="swal__admin__subtitle"> Xóa không thành công </p>',
                                    contentError,
                                    'warning'
                                );
                            } else {
                                Swal.fire(
                                    'Lưu ý',
                                    response.message,
                                    'warning'
                                )
                            }
                        }
                        else {
                            Swal.fire(
                                'Lưu ý',
                                response.message,
                                'warning'
                            )
                        }
                    }
                },
                error: function (e) {
                    $("#loading").removeClass("show");
                    if (e.status === 401) {
                        Swal.fire(
                            'Top việc làm nổi bật',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Top việc làm nổi bật',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Top việc làm nổi bật',
                            'Top việc làm nổi bật không thành công, <br> vui lòng thử lại sau!',
                            'error'
                        );
                    }

                }
            })
        }
    })
}

//function loadSelectJob() {
//    $.ajax({
//        url: systemConfig.defaultAPIURL + "api/top-feature-job/List-Job-Selected",
//        type: "GET",
//        contentType: "application/json",
//        success: function (response) {
//            //console.log(response);
//            $('#searchTopFeatureJobName, #topFeatureJobName').empty().trigger('change');
//            response.resources.forEach(function (item) {
//                var option = new Option(item.text, item.value, false, false);
//                $('#searchTopFeatureJobName, #topFeatureJobName').append(option);
//            });

//            $("#topFeatureJobName, #searchTopFeatureJobName").select2({
//                allowClear: true,
//                placeholder: ""
//            });

//            $("#searchTopFeatureJobName").val("").trigger('change');
//        },
//        error: function (e) {
//            console.error('An error occurred:', e);
//        }
//    });
//}

function loadJobData() {
    $('#topFeatureJobName, #searchTopFeatureJobName').select2({
        dropdownCssClass: 'bigdrop',
        minimumInputLength: 3,
        ajax: {
            url: systemConfig.defaultAPIURL + "api/top-feature-job/search-jobs",
            type: "POST",
            contentType: "application/json",
            data: function (params) {
                return JSON.stringify({
                    searchString: params.term,
                    pageLimit: 10
                });
            },
            processResults: function (res) {
                var data = res.resources || []; // Thêm kiểm tra trong trường hợp resources không tồn tại
                var results = data.map(function (item) {
                    return {
                        id: item.topFeatureJobId, // Sử dụng topFeatureJobId làm id
                        text: item.topFeatureJobName // Sử dụng topFeatureJobName làm text
                    };
                });
                return {
                    results: results
                };
            }
        },
        escapeMarkup: function (markup) { return markup; },
        templateSelection: formatJobSelection
    });
}

function formatJobSelection(data) {
    return `\
            <div class="">\
                <span><strong>` + data.text + `</strong></span>\
            </div>\
        `;
}

function validate() {
    var errorList = [];
    if ($("#topFeatureJobName").val() == null) {
        errorList.push("Tên công ty không được bỏ trống.");
    }
    //if ($("#topFeatureJobName").val().trim() === "") {
    //    errorList.push("Tên công ty đã tồn tại.");
    //}

    if ($("#topFeatureJobOrderSort").val().trim() === "") {
        errorList.push("Thứ tự sắp xếp không được để trống.");
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
            'Top việc làm nổi bật' + swalSubTitle,
            contentError,
            'warning'
        );
    } else {
        submit();
    }
}
$("#tableData").on('click', '.order-section-change-up', function () {
    var clickedElementId = $(this).attr('data-id-row');

    // Sử dụng findIndex để tìm index của phần tử trong mảng dataSource dựa trên giá trị id
    var index = dataSource.findIndex(x => x.id == clickedElementId);

    if (index != 0) {
        objSectionOrderUp = dataSource[index];
        objSectionOrderDown = dataSource[index - 1];
        var obj = {
            slideUp: objSectionOrderUp,
            slideDown: objSectionOrderDown,
        }

        swal.fire({
            title: "Thay đổi thứ tự",
            html: "Bạn có chắc chắn muốn thay đổi thứ tự này",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass("show");
                //CALL AJAX TO CREATE
                if (obj) {
                    try {
                        let result;
                        result = await httpService.postAsync("api/top-feature-job/change-order-slide", obj);

                        if (result.isSucceeded) {
                            Swal.fire(
                                'Thay đổi thứ tự ',
                                'Thay đổi thứ tự thành công.',
                                'success'
                            ).then((result) => {
                                reGenTable();
                            });
                        } else {
                            if (result.status == 400) {
                                Swal.fire(
                                    'Thay đổi thứ tự',
                                    `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                    'error'
                                );
                            }
                            else {
                                Swal.fire(
                                    'Thay đổi thứ tự ',
                                    `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                    'error'
                                );
                            }
                        }
                        $("#loading").removeClass("show");
                    } catch (e) {
                        console.error(e);
                        if (e.status === 401) {
                            Swal.fire(
                                'Thay đổi thứ tự',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Thay đổi thứ tự',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Thay đổi thứ tự',
                                `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                        $("#loading").removeClass("show");
                    }
                }
                $("#loading").removeClass("show");
            }
        });
    }
    else {
        swal.fire({
            title: "Thay đổi thứ tự",
            html: "Việc làm tốt nhất đã ở vị trí đầu tiên, không thể thay đổi lên trên được",
            icon: 'info',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Đồng ý'
        })
    }
})
$("#tableData").on('click', '.order-section-change-down', function () {
    var clickedElementId = $(this).attr('data-id-row');
    // Sử dụng findIndex để tìm index của phần tử trong mảng dataSource dựa trên giá trị id
    var index = dataSource.findIndex(x => x.id == clickedElementId);
    if (index != dataSource.length - 1) {
        objSectionOrderUp = dataSource[index];
        objSectionOrderDown = dataSource[index + 1];
        var obj = {
            slideUp: objSectionOrderUp,
            slideDown: objSectionOrderDown,
        }

        swal.fire({
            title: "Thay đổi thứ tự",
            html: "Bạn có chắc chắn muốn thay đổi thứ tự này",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass("show");
                //CALL AJAX TO CREATE
                if (obj) {
                    try {
                        let result;
                        result = await httpService.postAsync("api/top-feature-job/change-order-slide", obj);
                        if (result.isSucceeded) {
                            Swal.fire(
                                'Thay đổi thứ tự ',
                                'Thay đổi thứ tự thành công.',
                                'success'
                            ).then((result) => {
                                reGenTable();
                            });
                        } else {
                            if (result.status == 400) {
                                Swal.fire(
                                    'Thay đổi thứ tự ',
                                    `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                    'error'
                                );
                            }
                            else {
                                Swal.fire(
                                    'Thay đổi thứ tự',
                                    `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                    'error'
                                );
                            }
                        }
                        $("#loading").removeClass("show");
                    } catch (e) {
                        console.error(e);
                        if (e.status === 401) {
                            Swal.fire(
                                'Thay đổi thứ tự',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Thay đổi thứ tự',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Thay đổi thứ tự ',
                                `Thay đổi thứ tự không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                        $("#loading").removeClass("show");
                    }
                }
                $("#loading").removeClass("show");
            }
        });
    }
    else {
        swal.fire({
            title: "Thay đổi thứ tự",
            html: "Việc làm tốt nhất đã ở vị trí cuối cùng, không thể thay đổi xuống dưới được",
            icon: 'info',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Đồng ý'
        })
    }
})
function submit() {
    var jobId = $("#topFeatureJobName").val(); // Khai báo và lấy giá trị jobId mới
    if (updatingId > 0 && updatingObj && updatingObj.id !== undefined) {
        var obj = {
            id: updatingObj.id,
            active: updatingObj.active,
            jobId: jobId, // Sử dụng jobId mới ở đây
            orderSort: ($("#topFeatureJobOrderSort").val() != '' ? $("#topFeatureJobOrderSort").val() : ""),
            createdTime: updatingObj.createdTime,
        };
    } else {
        obj = {
            jobId: jobId, // Sử dụng jobId mới ở đây
            orderSort: ($("#topFeatureJobOrderSort").val() != '' ? $("#topFeatureJobOrderSort").val() : ""),
            createdTime: moment($("#createdTime").val(), "DD/MM/YYYY HH:mm:ss").format("YYYY-MM-DDTHH:mm:ss"),
        };
    }

    var actionName = (updatingId > 0 ? "Cập nhật" : "Thêm mới");

    swal.fire({
        title: actionName + " top việc làm nổi bật",
        html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " top việc làm nổi bật <b>" + $("#topFeatureJobName option:selected").text() + '</b>?',
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
                    url: systemConfig.defaultAPIURL + "api/top-feature-job/update",
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");
                        //debugger;
                        if (response.isSucceeded) {
                            Swal.fire(
                                'Cập nhật top việc làm nổi bật',
                                'Top việc làm nổi bật <b>' + $("#topFeatureJobName option:selected").text() + '</b> đã được cập nhật thành công.',
                                'success'
                            ).then((result) => {
                                $("#topFeatureJobModal").modal("hide");
                                reGenTable();

                            });
                        }
                        else {
                            if (response.status == 400) {
                                if (response.errors) {
                                    Swal.fire(
                                        'Top việc làm nổi bật <p class="swal__admin__subtitle"> Cập nhật không thành công </p>',
                                        response.errors,
                                        'warning'
                                    );
                                } else {
                                    Swal.fire(
                                        'Lưu ý',
                                        response.message,
                                        'warning'
                                    )
                                }
                            }
                            else {
                                Swal.fire(
                                    'Lưu ý',
                                    response.message,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");

                        if (e.status === 401) {
                            Swal.fire(
                                'Top việc làm nổi bật',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Top việc làm nổi bật',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Top việc làm nổi bật',
                                'Không thể cập nhật top việc làm nổi bật, hãy kiểm tra lại thông tin.',
                                'error'
                            );
                        }

                    }
                });
            };

            //CALL AJAX TO CREATE
            if (updatingId == 0) {
                //console.log(obj);
                obj.topFeatureJobId = parseInt(obj.topFeatureJobId); // Chuyển đổi jobId thành số nguyên
                obj.orderSort = parseInt(obj.orderSort);
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/top-feature-job/add",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");
                        //debugger;

                        if (response.isSucceeded) {
                            Swal.fire(
                                'Thêm mới top việc làm nổi bật',
                                'Top việc làm nổi bật <b>' + $("#topFeatureJobName option:selected").text() + '</b> đã được thêm mới thành công.',
                                'success'
                            ).then(function () {
                                $("#topFeatureJobModal").modal("hide");
                                //window.location.reload();
                                reGenTable();

                            });
                        }
                        else {
                            if (response.status == 400) {
                                if (response.errors != null) {
                                    var contentError = "<ul>";
                                    response.errors.forEach(function (item, index) {
                                        contentError += "<li class='text-start pb-2'>" + item + "</li>";
                                    })
                                    contentError += "</ul>";
                                    Swal.fire(
                                        'Top việc làm nổi bật <p class="swal__admin__subtitle"> Thêm mới không thành công </p>',
                                        contentError,
                                        'warning'
                                    );
                                } else {
                                    Swal.fire(
                                        'Lưu ý',
                                        response.message,
                                        'warning'
                                    )
                                }
                            }
                            else {
                                Swal.fire(
                                    'Lưu ý',
                                    response.message,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        if (e.status === 401) {
                            Swal.fire(
                                'Top việc làm nổi bật',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Top việc làm nổi bật',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {

                            Swal.fire(
                                'Top việc làm nổi bật',
                                'Không thể thêm mới vị trí công việc, hãy kiểm tra lại thông tin.',
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
    //table.column(1).search($("#searchTopFeatureJobName").val());
    table.column(2).search($("#searchTopFeatureJobOrderSort").val());
    table.draw();
}

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var date = new Date(moment(data[5], "DD/MM/YYYY HH:mm:ss"));
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
$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var columnSearch = data[1];
        var dataSearch = $("#searchTopFeatureJobName").val() || "";
        //console.log(dataSearch);
        if (dataSearch == "") {
            return true;
        }
        if (columnSearch.includes(dataSearch.toString())) {
            return true;
        }
        return false;
    }
);