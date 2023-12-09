

$(document).ready(function () {


    if (localStorage.currentLoggedInUserJobi) {
        initnoti();
        loadDataAndCountNotifications();

    }
    else {
        window.location.href = "";
    }
    $('#table_employer_notification tbody tr').each(function () {
        var notificationStatusId = $(this).find('.notification-clickable').data('notification-status-id');
        if (notificationStatusId == read) {
            $(this).addClass('read');
        } else if (notificationStatusId == unRead) {
            $(this).addClass('unread');
        }
    });
    
})

var table;
var dataSource = [];
var dataSourceDashBoard = [];
var showItem = ["name", "createdTime"];
var updatingObj = {};
var table;
var linked1, linked2;
var totalPage;
var pageIndex = 1;

async function initnoti() {
    initTablenoti();
}

function initTablenoti() {
    table = $("#table_employer_notification").DataTable({
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
            url: `${systemConfig.defaultAPI_URL}api/employer-notification/list-employer-notification`,
            type: "POST",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            },
            dataType: "json",
            data: function (d) {
                d.search.value = $("#search_all").val().trim() || "";
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
                data: "name",
                render: function (data, type, row, meta) {
                    return `
        <span class="notification-clickable ${row.notificationStatusId === read ? 'read' : 'unread'}" 
              data-notification-id="${row.id}" 
              data-notification-status-id="${row.notificationStatusId}">
            <div class="pxp-dashboard-notifications-item-left">
                <div class="pxp-dashboard-notifications-item-type"><span class="fa fa-briefcase"></span></div>
                <div class="pxp-dashboard-notifications-item-message">
                    ${data}
                </div>
            </div>
        </span>`;
                }
            },

            {
                data: "createdTime",
                render: function (data, type, row, meta) {
                    return `<div >` + moment(data).format("DD/MM/YYYY HH:mm:ss") + `</div>`;
                }
            },
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return `<div class='d-flex justify-content-center'>`
                        /*+ `<button class="btn-admin-edit btn btn-icon" title='Cập nhật' data-idItem='` + data + `'><span class='svg-icon-primary svg-icon  svg-icon-1'> ` + systemConfig.editIcon + ` </span></button>`;*/
                        + `<button class="btn-admin-delete btn btn-icon" onclick='deleteNoti(${data})'  title='Xóa' data-id='` + data + `' ><span class="fa fa-trash"></span></button></div>`;
                   
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
            $('#table_employer_notification tfoot').html("");
            innitTblPaging("#candidate_paging");
            //$("[data-bs-toggle=tooltip]").trigger('change');
            
        }
    });
}
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

//async function loadData() {
//    try {
//        let data = {
//            'PageIndex': pageIndex,
//            'PageSize': 10,
//        };
//        let res = await httpService.postAsync(`api/employer-notification/list-employer-notification`, data);
//        if (res.isSucceeded) {
//            if (res.status == 200) {
//                dataSource = res.resources.dataSource;
//                totalPage = res.resources.totalPages;
//                loadTableNotification();
//                //initTableNotification();
//                initPagination(totalPage, "#pagination-job");

//            } 
//        }
//    }
//    catch (e) {
//        dataSource = [];
//        loadTableNotification();
//        //initTableNotification();
//        initPagination(totalPage, "#pagination-job");
//        Swal.fire({
//            icon: 'warning',
//            title: 'Lấy dữ liệu thất bại',
//            html: 'Có lỗi xảy ra khi lấy danh sách thông báo, vui lòng thử lại',
//            showCloseButton: false,
//            focusConfirm: true,
//            confirmButtonText: 'Ok',
//        })
//    }
//}




//function loadTableNotification() {
//    $("#table_employer_notification tbody").html("");
//    if (dataSource.length === 0) {
//        $("#table_employer_notification tbody").html(`
//        <tr>
//            <td colspan="5" class="text-center p-3">Bạn chưa có thông báo</td>
//        </tr>
//    `);
//    }
//    else {

//        dataSource.forEach(function (item, index) {
//            var rowContent = `<tr class="  ${item.notificationStatusId == read ? "read" : "unread"}">`;

//            showItem.forEach(function (key) {
//                if (item[key]) {
//                    if (key == "createdTime") {
//                        rowContent += `<td style="width: 20%;" class="row-${item.id}-column column-${key}">
//                                        <div class="pxp-dashboard-notifications-item-right">${moment(item[key], "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss")}</div>
//                                    </td>`
//                    }
//                    else {
//                        /*rowContent += `<td class="row-${item.id}-column column-${key}" property="${key}">${item[key]}</td>`;*/
//                        rowContent += `<td style="width: 75%;" class="row-${item.id}-column column-${key}" property="${key}">
//                    <span class="notification-clickable" data-notification-id="${item.id}">
//                                        <div class="pxp-dashboard-notifications-item-left">
//                                            <div class="pxp-dashboard-notifications-item-type"><span class="fa fa-briefcase"></span></div>
//                                            <div class="pxp-dashboard-notifications-item-message">
//                                                ${item[key]}
//                                            </div>
//                                        </div></span>
//                                    </td>`
//                    }
//                }
//                else {
//                    rowContent += `<td class="row-${item.id}-column column-${key}" property="${key}" + key + "'>" + "</td>`;
//                }
//            })

//            rowContent += `<td class="row-${item.id}-column" property="">
//                            <div class="pxp-dashboard-table-options">
//                                <ul class="list-unstyled">
//                                    <li><button title="Delete" onclick='deleteNoti(${item.id})' class="btn_delete" data-id="${item.id}"><span class="fa fa-trash"></span></button></li>
//                                </ul>
//                            </div>
//                        </td>`;
//            rowContent += `</tr>`;
//            $(rowContent).appendTo($("#table_employer_notification tbody")).trigger("change");
//        });
//    }
//}
$("#table_employer_notification").on("click", ".notification-clickable", makeAsRead);

async function makeAsRead(event) {
    event.preventDefault();

    var notificationId = $(this).data("notification-id");

    // Mã hóa ID
    var encodedId = btoa(notificationId);

    let res = await httpService.putAsync(`api/employer-notification/MakeAsRead/${notificationId}`);
    if (res.isSucceeded) {
        if (res.status == 200) {
            // Thực hiện chuyển hướng đến trang chi tiết thông báo với ID đã mã hóa
            var targetUrl = "chi-tiet-thong-bao/" + encodedId;
            window.location.href = targetUrl;
        }
    }
}



////function initTableNotification() {
////    table = $('#table_employer_notification').DataTable({
////        language: systemConfig.languageDataTable,
////        searching: {
////            regex: true
////        },
////        'order': [
////            [1, 'desc']
////        ],
////        columnDefs: [
////            { targets: "no-sort", orderable: false },
////            { targets: [0, -1], orderable: false },
////            { targets: "no-search", searchable: false },
////            {
////                targets: "trim",
////                render: function (data, type, full, meta) {
////                    if (type === "display") {
////                        data = strtrunc(data, 10);
////                    }
////                    return data;
////                }
////            },
////            { targets: "date-type", type: "date-eu" },
////        ],

////        aLengthMenu: [
////            [10, 25, 50, 100],
////            [10, 25, 50, 100]
////        ],
////        drawCallback: function () {
           
////            //$('#table_employer_notification tfoot').html("");
////            //$("#table_employer_notification thead:nth-child(1) tr").clone(true).appendTo("#table_employer_notification tfoot");
////        }

////    });

////    //table.on('order.dt search.dt', function () {
////    //    table.column(0, {
////    //        search: 'applied',
////    //        order: 'applied'
////    //    }).nodes().each(function (cell, i) {
////    //        cell.innerHTML = i + 1;
////    //    });
////    //}).draw();
////}


//function viewDetails(id) {
//    // Tìm thông báo có id tương ứng trong dataSource
//    var notification = dataSource.find(item => item.id === id);

//    // Điền thông tin vào các phần tử HTML
//    $(".notification-name").text(notification.name);
//    $(".notification-createdtime").text(moment(notification.createdTime, "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss"));
//    $(".notification-description").text(notification.description);
//    $(".notification-link").text("Xem chi tiết");
//    $(".notification-link").attr("href", notification.link);


//}
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
async function getItemById(id) {
    await $.ajax({
        url: systemConfig.defaultAPI_URL + "api/employer-notification/detail/" + id,
        type: "GET",
        success: function (responseData) {
            console.log(responseData)
            updatingObj = responseData.resources;
        },
        error: function (e) {
            $("#loading").removeClass("show");

            swal.fire(
                'Ứng viên',
                'Đã có lỗi xảy ra, hãy kiểm tra lại thông tin.',
                'error'
            );
        },
    })
}
//$(".employer-notification").on("click", ".page-item", function (e) {
//    e.preventDefault();
//    if ($(this).hasClass('paging-first-item')) {
//        pageIndex = 1;
//        loadData();
//    }
//    else if ($(this).hasClass('paging-last-item')) {
//        pageIndex = totalPage;
//        loadData();
//    }
//    else if ($(this).hasClass('paging-next')) {
//        pageIndex += 1;
//        loadData();
//    }
//    else if ($(this).hasClass('paging-previous')) {
//        pageIndex = pageIndex - 1;
//        loadData();

//    }
//    else {
//        if (!($(this).attr('class').includes('active'))) {
//            $(".page-item").removeClass("active");
//            $(this).addClass("active");
//            pageIndex = parseInt($(this).text());

//            loadData();
//        }
//    }
//});
//function refreshTable() {
//    loadData();
//    loadDataAndCountNotifications();
//}



////function tableSearch() {
////    table.column(1).search($("#table_employer_notification thead:nth-child(2) tr th:nth-child(2) input").val());
////    table.column(3).search($("#table_employer_notification thead:nth-child(2) tr th:nth-child(4) input").val());
////    table.column(4).search($("#table_employer_notification thead:nth-child(2) tr th:nth-child(5) input").val());
////    table.draw();
////}




////$.fn.dataTa ble.ext.search.push(
////    function (settings, data, dataIndex) {
////        var date = new Date(moment(data[5], "DD/MM/YYYY HH:mm:ss"));
////        var startDate = $("#fillter_startDate").val();
////        var endDate = $("#fillter_endDate").val();
////        var min = startDate != "" ? new Date(moment(startDate, "DD/MM/YYYY ").format("YYYY-MM-DD 00:00:00")) : null;
////        var max = endDate != "" ? new Date(moment(endDate, "DD/MM/YYYY").format("YYYY-MM-DD 23:59:59")) : null;
////        if (
////            (min === null && max === null) ||
////            (min === null && date <= max) ||
////            (min <= date && max === null) ||
////            (min <= date && date <= max)
////        ) {
////            return true;
////        }
////        return false;
////    }
////);
function refreshTable() {
    $("#search_all").val("");
    table.page(0).draw();
        loadDataAndCountNotifications();

};
async function deleteNoti(id) {
    await getItemById(id);

    swal.fire({
        title: 'Xóa thông báo',
        html: 'Bạn có chắc chắn muốn xóa thông báo <br><b>' + updatingObj.name + '</b>?',
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
                let deleteResult = await httpService.deleteAsync(`api/employer-notification/delete/${id}`);
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
async function loadDataAndCountNotifications() {
    $(".noti_body").html("");
    try {
        let res = await httpService.getAsync(`api/employer-notification/listRecent`);
        if (res.isSucceeded && res.status === 200) {
            dataSourceDashBoard = res.resources;
            LoadListNotification();
            await loadUnreadTotal();
        }
    } catch (e) {

        dataSourceDashBoard = [];
        LoadListNotification();
        await loadUnreadTotal();
        Swal.fire({
            icon: 'warning',
            title: 'Lấy dữ liệu thất bại',
            html: 'Có lỗi xảy ra khi lấy danh sách thông báo, vui lòng thử lại',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        });
    }
}

function LoadListNotification() {
    $("#noti_side_menu .noti_body").html("");
    if (dataSourceDashBoard.length == 0) {
        $(".dropdown-menu.noti_body").html(`
        <span style="white-space: nowrap;"> Bạn chưa có thông báo </span>
        <li><hr class="dropdown-divider"></li>
        <li><a class="dropdown-item pxp-link" href="/thong-bao">Xem tất cả</a></li>
        `);
    } else {
        var listContent = "";

        dataSourceDashBoard.forEach(function (item) {
            listContent += `
            <li class="${item.notificationStatusId == unRead ? "unRead" : "read"}">
                <a class="dropdown-item " href="/thong-bao">
                    <span>${item.name}</span> <span class="pxp-is-time">${moment(item.createdTime).fromNow()}</span>
                </a>
            </li>
        `;
        });

        listContent += `
        <li><hr class="dropdown-divider"></li>
        <li><a class="dropdown-item pxp-link" href="/thong-bao">Xem tất cả</a></li>
    `;

        $(".dropdown-menu.noti_body").append(listContent);
    }
}

async function loadUnreadTotal() {
    $(".rbt-notification-count").html("");
    try {
        let res = await httpService.getAsync(`api/employer-notification/GetUnreadTotal`);
        if (res.isSucceeded && res.status === 200) {
            LoadCountNotification(res.resources); // Gọi hàm LoadCountNotification để đặt giá trị total vào thẻ div
        }
    } catch (e) {
        LoadCountNotification(0);
        Swal.fire({
            icon: 'warning',
            title: 'Lấy dữ liệu thất bại',
            html: 'Có lỗi xảy ra khi lấy danh sách thông báo, vui lòng thử lại',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        });
    }
}

function LoadCountNotification(total) {
    if (total == 0) {
        $('span.rbt-notification-count, div.rbt-notification-count').removeClass();
    }
    else {
        $(".rbt-notification-count").text(total);

    }
}