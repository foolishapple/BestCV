﻿
var colorSource = [];
$(document).ready(async function () {

    await loadDataOrderStatusSelect();
    loadData();
    loadDataPaymentMethodSelect();

    $("#tableData").on("click", ".btn-admin-edit", function () {
        var id = parseInt($(this).attr("data-idItem"));
        editItem(id);
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

    $("#searchPrice").on('keyup', function (e) {
        $(this).val(function (index, value) {
            return customFormatValue(value
                .replace(/\D/g, ''), 3, '.');
        })
        if (e.keyCode == 13) {
            tableSearch();
        }
    })

    $("#searchDiscountPrice").on('keyup', function (e) {
        $(this).val(function (index, value) {
            return customFormatValue(value
                .replace(/\D/g, ''), 3, '.');
        })
        if (e.keyCode == 13) {
            tableSearch();
        }
    })

    $("#searchFinalPrice").on('keyup', function (e) {
        $(this).val(function (index, value) {
            return customFormatValue(value
                .replace(/\D/g, ''), 3, '.');
        })
        if (e.keyCode == 13) {
            tableSearch();
        }
    })

    $("#searchDiscountVoucher").on('keyup', function (e) {
        $(this).val(function (index, value) {
            return customFormatValue(value
                .replace(/\D/g, ''), 3, '.');
        })
        if (e.keyCode == 13) {
            tableSearch();
        }
    })

    $("#searchIsApproved").val("").trigger('change')
    $("#searchIsApproved").select2({
        placeholder: ""
    });
    $("#tableData tbody").on("click", ".checkIsApproved", function () {
        quickIsApproved($(this).is(":checked"), $(this).attr("data-id"));
    })
    $("#btnTableResetSearch").click(function () {
        $("#searchEmployerName").val("");
        $("#searchPrice").val("");
        $("#searchDiscountPrice").val("");
        $("#searchFinalPrice").val("");
        $("#searchDiscountVoucher").val("");
        $("#searchSelectOrderPayment").val("").trigger('change');
        $("#searchSelectOrderStatus").val("").trigger('change');
        $("#searchIsApproved").val("").trigger('change');
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        linked1.clear();
        linked2.clear();
        //tableSearch();
        reGenTable();
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

async function editItem(id) {
    autosize.destroy($('#info'));
    autosize($('#info'));
    updatingId = id;
    $("#loading").addClass("show");
    if (id > 0) {
        await getItemById(updatingId);
        //console.log(updatingObj);
        if (updatingObj != null && updatingObj != undefined) {
            $("#employer").val(updatingObj.listEmployer[0].fullname); // Đặt giá trị cho trường nhập liệu
            $("#selectOrderStatus").prop('disabled', true);
            $("#selectOrderStatus").val(updatingObj.orderStatusId).trigger('change');
            $("#selectPayment").prop('disabled', true);
            $("#selectPayment").val(updatingObj.paymentMethodId).trigger('change');
            $("#transactionCode").val(updatingObj.transactionCode == null ? "" : updatingObj.transactionCode);
            //$("#requestId").val(updatingObj.requestId == null ? "" : updatingObj.requestId);
            $("#requestId").val(updatingObj.requestId);
            $("#info").val(updatingObj.info == null ? "" : updatingObj.info);
            $("#createdTime").val(moment(updatingObj.createdTime, "YYYY-MM-DDTHH:mm:ss").format("DD/MM/YYYY HH:mm:ss"))
        }
        else {
            swal.fire(
                'Đơn hàng',
                'Không thể xem chi tiết đơn hàng, hãy kiểm tra lại thông tin.',
                'error'
            );
        }
    }

    //load table list order detail
    //$.when(loadDataOrderDetail(id)).done(function () {
    //    var totalPrice = `<tr>
    //                                       <td>
    //                                           <div class="d-flex align-items-center gap-3">
    //                                           </div>
    //                                       </td>
    //                                       <td class="text-end">
    //                                                   <div class="text-gray-800 fs-12">Tổng giá gốc</div>
    //                                                   <div class="text-muted fs-14 fw-bold" data-kt-table-widget-4="template_cost">`+ customFormatValue(updatingObj.price, 3, ".") + `</div>
    //                                       </td>
    //                                       <td class="text-end">
    //                                           <div class="text-gray-800 fs-12">Giá đã giảm (KM)</div>
    //                                                   <div class="text-muted fs-14 fw-bold" data-kt-table-widget-4="template_cost">`+ customFormatValue(updatingObj.discountPrice, 3, ".") + `</div>
    //                                       </td>
    //                                               <td class="text-end">
    //                                                   <div class="text-gray-800 fs-12">Tổng giảm giá (Voucher)</div>
    //                                                           <div class="text-muted fs-14 fw-bold" data-kt-table-widget-4="template_cost">`+ customFormatValue(dataSourceOrderDetail.voucherDiscount, 3, ".") + `</div>
    //                                               </td>
    //                                       <td class="text-end">
    //                                           <div class="text-gray-800 fs-12">Tổng giá cuối</div>
    //                                                   <div class="text-muted fs-14 fw-bold" data-kt-table-widget-4="template_cost">`+ customFormatValue(updatingObj.finalPrice, 3, ".") + `</div>
    //                                       </td>
    //                                      </tr>`;
    //    $(totalPrice).appendTo($("#tableDataOrderDetail tbody"));
    //});
    loadDataOrderDetail(id);


    $("#orderModalTitle").text(id > 0 ? "Chi tiết đơn hàng" : "Thêm mới đơn hàng");
    $("#loading").removeClass("show");
    $("#orderModal").modal("show");
}

$("#orderModal").on('shown.bs.modal', function () {
    $(this).find('.nav-tabs a:first').tab('show');
    autosize.destroy($('#info'));
    autosize($('#info'));

})

$("#orderModal").on('hiden.bs.modal', function () {
    $(this).find('.nav-tabs a:first').tab('show');
    autosize.destroy($('#info'));
})

function loadTableOrderDetail() {
    var index = 0;
    var totalPrice = 0, totalFinalPrice = 0;
    $("#tableDataOrderDetail tbody").html("");
    var rowContent = "";
    // Kiểm tra xem dataSourceOrderDetail.listOrderDetail có giá trị và có thể lặp qua
    if (Array.isArray(dataSourceOrderDetail.listOrderDetail)) {
        dataSourceOrderDetail.listOrderDetail.forEach(function (item, index) {
            rowContent += "<tr>";
            rowContent += `<td class="text-muted">${item.employerServicePackageName}</td>`;
            rowContent += `<td class="text-muted text-end">${customFormatValue(item.price,3,'.')}</td>`;
            rowContent += `<td class="text-muted text-end">${customFormatValue(item.quantity, 3, '.') }</td>`;
            rowContent += `<td class="text-muted text-end">${customFormatValue(item.finalPrice, 3, '.') }</td>`;
            rowContent += "</tr>";
            // Thêm mã lệnh xử lý dữ liệu vào đây
            totalPrice += item.discountPrice;
            totalFinalPrice += item.finalPrice;
        });
        // Tạo rowContent và hiển thị dữ liệu
        $("#tableDataOrderDetail tbody").html(rowContent);
    }

    //load thông tin tiền
    var moneyContent = 
    `<div class="d-flex justify-content-between">     
        <span>TỔNG</span>
        <span>${customFormatValue(totalFinalPrice , 3, '.')}</span>
    </div>
    <div class="d-flex justify-content-between">
        <span>VAT</span>
        <span>8 %</span>
    </div>
    <div class="d-flex justify-content-between">
        <span>THÀNH TIỀN (VNĐ)</span>
        <span>${customFormatValue(totalFinalPrice + ((totalFinalPrice * 8) / 100), 3, '.')}</span>
    </div>
    `;

    $("#price-info").html(moneyContent);
}

function initTableOrderDetail() {
    tableOrderDetail = $('#tableDataOrderDetail').DataTable({
        language: systemConfig.languageDataTable,
        ordering: false,
        paging: false,
        info: false,
    });

}
function reGenTableOrderDetail() {
    if (tableOrderDetail) {
        tableOrderDetail.destroy();
    }
    loadTableOrderDetail();
    initTableOrderDetail();
}

function loadDataOrderDetail(orderId) {
    return $.ajax({
        url: systemConfig.defaultAPIURL + "api/employerorder/list-order-detail/" + orderId,
        type: "GET",
        success: function (responseData) {
            //console.log(responseData);
            dataSourceOrderDetail = responseData.resources;
            //loadTableOrderDetail();
            //initTableOrderDetail();
            reGenTableOrderDetail()
        },
        error: function (e) {
            //loadTableOrderDetail();
            //initTableOrderDetail();
            reGenTableOrderDetail();
        }
    });
}
var tableOrderDetail;
async function getItemById(id) {
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/employerorder/detail-admin/" + id,
        type: "GET",
        success: function (responseData) {
            updatingObj = responseData.resources;
        },
        error: function (e) {
            swal.fire(
                'Đơn hàng',
                'Đã có lỗi xảy ra, hãy kiểm tra lại thông tin.',
                'error'
            );
        },
    })
}

function loadData() {
    table = $("#tableData").DataTable({
        language: systemConfig.languageDataTable,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ajax: {
            url: systemConfig.defaultAPIURL + "api/employerorder/list-employer-order-aggregates",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            },
        },
        columns: [
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return "#" + data;
                }
            },
            {
                data: "employerName",
                render: function (data, type, row, meta) {
                    var companyName = row.companyName;
                    var html = '<div style="margin-bottom: 5px;">' + data + '</div><div class="text-primary">' + companyName + '</div>';
                    return html;
                }
            },
            {
                data: "price",
                render: function (data, type, row, meta) {
                    console.log(row)
                    if (type === 'display') {
                        return '<div style="text-align: right;">' + customFormatValue(data, 3, '.') + '</div>';
                    }
                    return data;
                }
            },
            {
                data: "discountPrice",
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<div style="text-align: right;">' + customFormatValue(data, 3, '.') + '</div>';
                    }
                    return data;
                }
            },
            {
                data: "finalPrice",
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return '<div style="text-align: right;">' + customFormatValue(data, 3, '.') + '</div>';
                    }
                    return data;
                }
            },
            //{
            //    data: "discountVoucher",
            //    render: function (data, type, row, meta) {
            //        if (type === 'display') {
            //            return '<div style="text-align: right;">' + customFormatValue(data, 3, '.') + '</div>';
            //        }
            //        return data;
            //    }
            //},
            {
                data: "paymentMethodName",
                render: function (data, type, row, meta) {
                    return data;
                }
            },
            {
                data: "orderStatusName",
                render: function (data, type, row, meta) {
                    let content = ``;
                    colorSource.forEach(function (item) {
                        if (item.value != row.orderStatusId) {
                            content += `  <li><a class="dropdown-item drop_status" href="#!" type="button" data-order-id="${row.id}" data-status-id = "${item.value}">${item.text} </a></li>`
                        }
                    })
                    return `<div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle w-100" type="button" data-bs-toggle="dropdown" aria-expanded="false" style="color: ${row.orderStatusColor}" >
   ${data}
  </button>
  <ul class="dropdown-menu">
    ${content}
  </ul>
</div>`;
                }
            },
            {
                data: "createdTime",
                render: function (data, type, row, meta) {
                    return `<div class='text-center'>` + moment(data).format("DD/MM/YYYY HH:mm:ss") + `</div>`;
                }
            },
            {
                data: "isApproved",
                render: function (data, type, row, meta) {
                    return `<span id='row` + row.id + `-column-activated'><div class="form-check form-switch form-check-custom form-check-solid justify-content-center column-approve"><input class="form-check-input checkIsApproved" data-id="${row.id}" type="checkbox" value="" ${data ? 'checked=""' : ''}></div></span>`

                        ;
                }
            },
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return `<div class='d-flex justify-content-center'>`
                        + `<button class="btn-admin-edit btn btn-icon" title='Cập nhật' data-idItem='` + data + `'><span class='svg-icon-primary svg-icon  svg-icon-1'> ` + systemConfig.editIcon + ` </span></button></div>`;

                }
            }
        ],
        columnDefs: [

            { targets: [0, -1], orderable: false },

        ],
        'order': [
            [7, 'asc']
        ],
        drawCallback: function () {
            $('#tableData tfoot').html("");
            $("#tableData thead:nth-child(1) tr").clone(true).appendTo("#tableData tfoot");
            $("#rowSearch").removeClass("d-none");
        }

    });
    table.on('draw', function (e) {
        dataExport = table.ajax.json().allData;
    });
    $(document).on("click", ".drop_status", function (e) {
        e.preventDefault();
        let orderId = $(this).attr("data-order-id");
        let statusId = $(this).attr("data-status-id");
        let text = $(this).text();
        let params = {
            'orderId': orderId,
            'orderStatusId': statusId
        };
        Swal.fire({
            title: "Quản lý đơn hàng nhà tuyển dụng",
            html: "Bạn có chắc chắn muốn đổi trạng thái thành <b>" + text + "</b> đơn hàng <strong>#" + orderId + "</strong> không?",
            icon: 'info',
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu',
            showCancelButton: true
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass('show');
                //CALL AJAX TO UPDATE
                try {
                    let response = await httpService.putAsync('api/employerorder/update-order-status', params);
                    $("#loading").removeClass('show');
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Quản lý đơn hàng nhà tuyển dụng',
                            'Đơn hàng <b>#' + orderId + ' </b> đã được cập nhật trạng thái thành công.',
                            'success'
                        );
                        table.draw();
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
                                    'Đơn hàng <p class="swal__admin__subtitle">Đổi trạng thái đơn hàng không thành công.</p>',
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
                } catch (e) {
                    debugger;
                    $("#loading").removeClass('show');
                    if (e.status === 401) {
                        Swal.fire(
                            'Đơn hàng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Đơn hàng',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Đơn hàng',
                            'Đổi trạng thái đơn hàng nhà tuyển dụng không thành công, <br> vui lòng thử lại sau!',
                            'error'
                        );
                    }
                    console.error(e);
                }
            }
        })
    })
}
function tableSearch() {
    table.column(1).search($("#searchEmployerName").val());
    table.column(2).search($("#searchPrice").val().replaceAll(".", ""));
    table.column(3).search($("#searchDiscountPrice").val().replaceAll(".", ""));
    table.column(4).search($("#searchFinalPrice").val().replaceAll(".", ""));
    table.column(5).search($("#searchDiscountVoucher").val().replaceAll(".", ""));
    const selectOrderPayment = $("#searchSelectOrderPayment").val();
    const selectOrderStatus = $("#searchSelectOrderStatus").val();
    table.column(6).search(selectOrderPayment ? selectOrderPayment.toString() : "");
    table.column(7).search(selectOrderStatus ? selectOrderStatus.toString() : "");
    table.search($("#table_search_all").val().trim()).draw();
    $("#table_search_all").val($("#table_search_all").val().replaceAll(".", ""));


    if ($("#fillter_startDate_value").val().length > 0 || $("#fillter_endDate_value").val().length > 0) {
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
        table.column(8).search(searchDateArrs.toString());
    }
    else {
        table.column(8).search("")
    }
    table.column(9).search($("#searchIsApproved").val());

    table.draw();
}

async function loadDataOrderStatusSelect() {
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/employerorder/list-order-status-selected",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            if (response.isSucceeded) {
                colorSource = response.resources
                //dataSource = response.resources;
                response.resources.forEach(function (item, index) {
                    //console.log(item);
                    $('#searchSelectOrderStatus').append(new Option(item.text, item.value, false, false)).trigger('change');
                    $('#selectOrderStatus').append(new Option(item.text, item.value, false, false)).trigger('change');
                })
                $("#searchSelectOrderStatus").val("").trigger('change')
                $("#selectOrderStatus").select2();
                $("#searchSelectOrderStatus").select2({
                    allowClear: true,
                    placeholder: ""
                });
            }
        },
        error: function (e) {
        }
    });
}

function loadDataPaymentMethodSelect() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/employerorder/list-payment-method-selected",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            //dataSource = response.resources;
            response.resources.forEach(function (item, index) {
                //console.log(item);
                $('#searchSelectOrderPayment').append(new Option(item.text, item.value, false, false)).trigger('change');
                $('#selectPayment').append(new Option(item.text, item.value, false, false)).trigger('change');
            })
            $("#searchSelectOrderPayment").val("").trigger('change')
            $("#selectPayment").select2();
            $("#searchSelectOrderPayment").select2({
                allowClear: true,
                placeholder: ""
            });

        },
        error: function (e) {
        }
    });
}

$("#submitButton").click(function () {
    var obj = {
        id: updatingObj.id,
        info: $("#info").val(),
    }
    Swal.fire({
        title: "Cập nhật đơn hàng",
        html: "Bạn có chắc chắn muốn cập nhật đơn hàng không?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu',
        focusConfirm: true,
    }).then((result) => {
        if (result.value) {
            $("#loading").addClass("show");

            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/employerorder/update-info-order",
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    success: function (response) {
                        $("#loading").removeClass("show");
                        //console.log(response)
                        //debugger;
                        if (response.isSucceeded) {
                            Swal.fire(
                                'Cập nhật đơn hàng',
                                'Đơn hàng <b>' + '</b> đã được cập nhật thành công.',
                                'success'
                            ).then((result) => {
                                $("#orderModal").modal("hide");
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
                                        'Đơn hàng <p class="swal__admin__subtitle"> Cập nhật không thành công </p>',
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
                                'Đơn hàng',
                                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                                'error'
                            ).then(function () {
                                window.location.href = "/home/login";
                            });
                        }
                        else if (e.status == 403) {
                            Swal.fire(
                                'Đơn hàng',
                                'Bạn không có quyền sử dụng tính năng này.',
                                'error'
                            );
                        }
                        else {
                            swal.fire(
                                'Đơn hàng',
                                'không thể cập nhật đơn hàng, hãy kiểm tra lại thông tin.',
                                'error'
                            );
                        }

                    }
                });
            };
        }
    })
})

async function quickIsApproved(isApproved, id) {
    var titleName = "Quản lý đơn hàng";
    var actionName = isApproved ? "duyệt" : "bỏ duyệt";
    await getItemById(id);
    var obj = { "id": id, "IsApproved": isApproved }
    Swal.fire({
        title: titleName,
        html: "Bạn có chắc chắn muốn " + actionName + " đơn hàng <strong>" + updatingObj.listEmployer[0].fullname + "</strong> không?",
        icon: 'info',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu',
        showCancelButton: true
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass('show');

            //CALL AJAX TO UPDATE
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/employerorder/quick-isapproved?id=" + id,
                type: "PUT",
                contentType: "application/json",
                success: function (response) {
                    $("#loading").removeClass('show');
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Duyệt đơn hàng',
                            'Đơn hàng <b>' + updatingObj.listEmployer[0].fullname + ' </b> đã được ' + actionName + ' thành công.',
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
                                    'Đơn hàng <p class="swal__admin__subtitle"> Duyệt không thành công </p>',
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
                    //console.log(e)
                    $("#loading").removeClass('show');
                    if (e.status === 401) {
                        Swal.fire(
                            'Đơn hàng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Đơn hàng',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Đơn hàng',
                            'Kích hoạt nhanh nhà tuyển dụng không thành công, <br> vui lòng thử lại sau!',
                            'error'
                        );
                    }


                }
            });
        }
        else {
            $(".checkIsApproved[data-id=" + id + "]").prop("checked", !isApproved);
        }
    })
}

$("#form-order").on('submit', function (e) {
    e.preventDefault();
})
