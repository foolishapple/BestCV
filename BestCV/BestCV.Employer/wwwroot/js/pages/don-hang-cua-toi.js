
initTableMyOrder();
async function initTableMyOrder() {
	tableOrder = $("#tableDataOrder").DataTable({
		language: systemConfig.languageDataTable,
		processing: true,
		serverSide: true,
		paging: true,
		searching: { regex: true },
		autoWidth: false,
		ordering: false,
		info: false,
		ajax: {
			url: systemConfig.defaultAPI_URL + `api/employerOrder/list-by-employer`,
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
					return `<span class='badge-order text-dark'>#${data}<span>`;
				}
			},
			{
				data: "employerName",
				render: function (data, type, row, meta) {
					return data;
				}
			},
			{
				data: "finalPrice",
				render: function (data, type, row, meta) {
					return formatNumber(data.toString()) + ' đ';
				}
			},
			{
				data: "orderStatusName",
				render: function (data, type, row, meta) {
					return `<span style="background-color: ${customBagdeColor(row.orderStatusColor)}; padding: 4px 6px; color: ${row.orderStatusColor};border-radius: 5px;" class='text-nowrap'>${data}</span>`;
				}
			},
			{
				data: "isApproved",
				render: function (data, type, row, meta) {
					return (data ? `<span style="background-color: ${customBagdeColor("#32a24d")}; padding: 4px 6px; color: #32a24d;border-radius: 5px;" class='text-nowrap'>Đã duyệt</span>` : `<span style="background-color: ${customBagdeColor("#e5b500")}; padding: 4px 6px; color: #e5b500;border-radius: 5px;" class='text-nowrap'>Chưa duyệt</span>`);
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
					return `<div class="candidate_action dropdown text-center">
                                <button class="dropdown-toggle btn_icon" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><span class="fa fa-ellipsis-h"></span></button>
                                <ul class="dropdown-menu list-unstyled shadow ">
                                    <li><a class="dropdown-item btn_detail_order" data-id=${data} href="#!" title="Chi tiết đơn hàng">Chi tiết đơn hàng</a></li>
                                    <li><a class="dropdown-item btn_cancel_order" href="#!" data-id=${data} title="Hủy đơn hàng">Hủy đơn hàng</a></li>
                                </ul>
                            </div>`;
				}
			}

		],
		columnDefs: [
			{
				targets: [0 ,4,3,-1, -2],
				className: "text-center text-nowrap"
			},
			{
				targets: [2],
				className: "text-end"
			}
		],
		drawCallback: function () {
			$('#tableDataOrder tfoot').html("");
			innitTblPagingMyOrder("#my-order-paging");
			initInfo("#tbl_info")
		}
	});
	$(document).on("click", ".page-link-my-order", function (e) {
		if ($(this).hasClass("active")) {
			return;
		}
		let pageIndex = $(this).attr("data-index");
		if (pageIndex) {
			tableOrder.page(parseInt(pageIndex)).draw('page');
		}
	})
	$("#search_all").on("keypress", function (e) {
		if (e.which == 13) {
			tableOrder.draw();
		}
	})
}
function innitTblPagingMyOrder(element) {
	let info = tableOrder.page.info();
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
			html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-my-order page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-my-order page-link"><i class="fa fa-angle-left"></i></a></li>`;
		}
		for (var i = startPage; i <= endPage; i++) {
			if (i > 0) {
				html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-my-order ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
			}
		}
		if (pageIndex < totalPage - 1) {
			html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-my-order page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-my-order page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
		}
		$(element).html(html);
	}
	else {
		$(element).html("");
	}
}
$(document).ready(function () {
	$(document).on('click', '.btn_detail_order', async function () {
		//console.log($(this).attr('data-id'))
		let orderId = $(this).attr('data-id');
		await LoadDetailOrderById(orderId);
		
		$("#detailOrderModal").modal('show')
	})

	$(document).on('click', '.btn_cancel_order', async function () {
		let orderId = $(this).attr('data-id');
		await CancelOrder(orderId);
	})
})
var dataDetailOrder = [];
async function LoadDetailOrderById(id) {
	try {
		let result = await httpService.getAsync("api/employerorder/detail-by-order-id/" + id);
		if (result.isSucceeded) {
			dataDetailOrder = result.resources;
			reGenTableOrderDetail();

		}
	} catch (e) {
		console.error(e);
	}
}
async function CancelOrder(id) {
	try {
		let result = await httpService.putAsync("api/employerorder/cancel-order/" + id);
		if (result.isSucceeded) {
			miniToast.fire({
				icon: 'success',
				html: 'Hủy đơn hàng thành công'
			})
			reGenTableOrder();
		}
	} catch (e) {
		console.error(e);
	}
}
function reGenTableOrder() {
	if (tableOrder != undefined) {
		tableOrder.destroy();
	}
	initTableMyOrder();
}

function reGenTableOrderDetail() {
	if (tableOrderDetail != undefined) {
		tableOrderDetail.destroy();
	}
	loadDataOrderDetail();
	initTableOrderDetail();
	genDataBillOrder();
}

var tableOrderDetail;
function initTableOrderDetail() {
	tableOrderDetail = $('#tableDataOrderDetail').DataTable({
		language: systemConfig.languageDataTable,
		paging: false,
		info: false,
		ordering: false,
		columnDefs: [
			{
				targets: [1,2,3],
				className: "text-end text-nowrap"
			},
			

		],
	});
}
var totalPriceOrder = 0;
function loadDataOrderDetail() {
	totalPriceOrder = 0;
	$("#tableDataOrderDetail tbody").html('');
	var content = "";
	dataDetailOrder.listOrderDetail.forEach(function (item) {
		totalPriceOrder += item.price * item.quantity;
		content += `<tr>`;
		content += `<td>${item.employerServicePackageName}</td>`;
		content += `<td>${formatNumber(item.quantity.toString())}</td>`;
		content += `<td>${formatNumber(item.price.toString())} đ</td>`;
		content += `<td>${formatNumber((item.quantity * item.price).toString())} đ</td>`;
		content += `</tr>`;
	})
	$("#tableDataOrderDetail tbody").append(content);
}
function genDataBillOrder() {
	let vatTax = (totalPriceOrder * 8) / 100;
	$("#table-price-total-money").text(formatNumber(totalPriceOrder.toString()) + ' đ')
	$("#table-price-final-money").text(formatNumber(dataDetailOrder.finalPrice.toString()) + ' đ')
	$("#table-price-vat").text(formatNumber(vatTax.toString()) + ' đ')
	$("#detail-order-img-check-out-qr").attr('src', "https://img.vietqr.io/image/" + BANKID + "-" + BANK_NUMBER + "-" + TEMPLATE + ".jpg?amount=" + dataDetailOrder.finalPrice + "&accountName=" + ACCOUNT_NAME);
	$("#modal-order-id").text("#" + dataDetailOrder.id);
	$("#modal-order-created-time").text(moment(dataDetailOrder.createdTime).format("DD/MM/YYYY"));
	$("#modal-order-employer").text(dataDetailOrder.employerName);
	$("#modal-order-is-approved").html(dataDetailOrder.isApproved ? "<span class='' style='color: #32a24d'>Đã duyệt</spam>" : "<span class='' style='color:#e5b500'>Chưa duyệt</spam>");
	$("#modal-order-status").html(`<span style='color: ${dataDetailOrder.orderStatusColor}'>${dataDetailOrder.orderStatusName}</span>`)
}

function initInfo(e) {
	let info = tableOrder.page.info();
	$(e).text(`${info.recordsDisplay} đơn hàng`);
}