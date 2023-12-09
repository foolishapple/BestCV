var dataSourceCart = [];
async function ListCartByEmployerId() {
    try {
        let result = await httpService.getAsync("api/employer-cart/list-by-employer-id");
        if (result.isSucceeded) {
            dataSourceCart = result.resources;
            reGenTableCart();

        }
    } catch (e) {
        console.error(e);
    }
}
function reGenTableCart() {
    if (tableCart != undefined) {
        tableCart.destroy();
    }
    loadData();
    initTable();
    genDataBill();

    if (idCheckout != 0) {
        let itemId = dataSourceCart.find(x => x.employerServicePackageId == idCheckout);
        if (itemId != undefined) {
            $("#check-item-" + itemId.id).prop('checked', true).trigger('change');
            CheckBill("#check-item-" + itemId.id);
        }
    }
    if (listChecked.length > 0) {
        listChecked.forEach(function (item) {
            $('#check-item-' + item).prop('checked', true).trigger('change')
            CheckBill("#check-item-" + item);
        })
    }

}
var tableCart;
function initTable() {
    tableCart = $('#tableData').DataTable({
        language: systemConfig.languageDataTable,
        paging: false,
        info: false,
        ordering: false,
    });
}
function loadData() {
    $("#tableData tbody").html('');
    var content = "";
    dataSourceCart.forEach(function (item) {

        content += `<tr>
                        <td>
                            <input type="checkbox" class="form-check-input check-item" id='check-item-${item.id}' data-id="${item.id}"/>
                        </td>
                        <td>
                            <div>
                            <h4>${item.employerServicePackageName}</h4>
                            <p>${item.employerServicePackgeDescription != null ? item.employerServicePackgeDescription : ""}</p>
                            </div>
                        </td>`;
        content += `<td class="text-nowrap fw-bold">${formatNumber(item.price.toString())} đ</td>`;
        content += `<td class='text-center column-quantity'>
                        <div class="d-flex flex-end">
                        <button class="btn btn-icon btn-outline btn-active-color-primary minus-btn" data-id="${item.id}" type="button">
                            <i class="fa-solid fa-square-minus"></i>

                        </button>
                        <input type="text" class="form-control text-end input-quantity" id="input-quantity-${item.id}" data-id="${item.id}"  value="${item.quantity}" min="0" max="100"/>
                        <button class="btn btn-icon btn-outline btn-active-color-primary plus-btn" data-id="${item.id}" type="button">
                            <i class="fa-solid fa-square-plus"></i>
                        </button></div>
                    </td>`;
        content += `<td class="text-nowrap fw-bold text-info" id='total-price-${item.id}'>${formatNumber((item.price * item.quantity).toString())} đ</td>`;
        content += `<td class='text-center'><div class="d-flex justify-content-center"><button class="btn btn-icon btn-delete-cart" title="Xóa" data-id="${item.id}"><span class="fa fa-trash text-danger"></span></button></div></td></tr>`;

    });
    $("#tableData tbody").append(content);

}




var totalMoney = 0, finalMoney = 0;
function genDataBill() {
    totalMoney = 0, finalMoney = 0;
    listItemCheckout.forEach(function (item) {
        totalMoney += item.price * item.quantity;
    })
    finalMoney = totalMoney + (totalMoney * 8) / 100;
    $("#total-money").text(formatNumber(totalMoney.toString()) + ' đ')
    $("#final-money").text(formatNumber(finalMoney.toString()) + ' đ')
}


var listChecked = [];
async function updateQuantity(objCart) {
    try {
        let result = await httpService.putAsync("api/employer-cart/update", objCart);
        if (result.isSucceeded) {

            $('.check-item:checkbox:checked').each(function () {
                listChecked.push($(this).attr('data-id'))
            })
            ListCartByEmployerId();


            listItemCheckout.forEach(function (item) { 
                $("#tableData #check-item-" + item.id).prop("checked", true);
            })

        }
    } catch (e) {
        console.error(e);

    }
}
$(document).ready(async function () {
    await ListCartByEmployerId();
    $("#card-check-out").addClass('d-none');
    $("#btn-check-out").click(function () {
        if (!$("#accept-policy").is(":checked")) {
            swal.fire(
                "Lưu ý!",
                "Bạn cần đồng ý với điều khoản dịch vụ của chúng tôi trước khi thanh toán",
                "warning"
            )
        }
        else if (finalMoney == 0) {
            swal.fire(
                "Lưu ý!",
                "Chưa có thông tin đơn hàng",
                "warning"
            )
        }
        else if (listItemCheckout.length == 0) {
            swal.fire(
                "Lưu ý!",
                "Bạn cần chọn dịch vụ để tiến hành thanh toán",
                "warning"
            )
        }
        else {
            //$("#card-check-out").removeClass('d-none');
            //$("#img-check-out-qr").attr('src', "https://img.vietqr.io/image/" + BANKID + "-" + BANK_NUMBER + "-" + TEMPLATE + ".jpg?amount=" + finalMoney + "&accountName=" + ACCOUNT_NAME);
            //console.log("https://img.vietqr.io/image/" + BANKID + "-" + BANK_NUMBER + "-" + TEMPLATE + ".jpg?amount=" + finalMoney + "&accountName=" + ACCOUNT_NAME)

            let newOrder = {
                "orderStatusId": CHUA_THANH_TOAN,
                "paymentMethodId": CHUYEN_KHOAN,
                "price": finalMoney,
                "finalPrice": finalMoney,
                "listEmployerOrderDetail": listItemCheckout
            }
            addOrder(newOrder)
        }
    })

    $("#tableData").on('click', '.btn-delete-cart', function () {
        let id = $(this).attr('data-id');
        let objPackage = dataSourceCart.find(x => x.id == id);
        swal.fire({
            "title": "Lưu ý!",
            "html": "Bạn có chắc muốn xóa dịch vụ </br><b>" + objPackage.employerServicePackageName + "</b> trong giỏ hàng không",
            "icon": "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                try {
                    let result = await httpService.deleteAsync("api/employer-cart/delete?id=" + id);
                    if (result.isSucceeded) {
                        listItemCheckout = listItemCheckout.filter(x => x.id != id);
                        await ListCartByEmployerId();
                        countServicePackageInCart();
                    }
                } catch (e) {
                    console.error(e);
                }
            }
        })

    })
    $("#tableData").on('change patse', '.input-quantity', function () {
        var e = $(this);
        var text = e.val();
        e.val(text.replace(/\D/g, ""));
        if (text.length == 0) {
            e.val(0)
        }
        if (parseInt(text) >= 100) {
            e.val(100)
        }
        let id = $(this).attr('data-id');
        let index = dataSourceCart.findIndex(x => x.id == id);
        dataSourceCart[index].quantity = $(`#input-quantity-${id}`).val();
        let totalPrice = dataSourceCart[index].price * $(`#input-quantity-${id}`).val();
        $(`#total-price-${id}`).text(formatNumber(totalPrice.toString()) + ' đ')
        let newObj = {
            "id": id,
            "employerId": dataSourceCart[index].employerId,
            "employerServicePackageId": dataSourceCart[index].employerServicePackageId,
            "quantity": $(`#input-quantity-${id}`).val(),
            "description": "",

        }
        //genDataBill();
        updateQuantity(newObj);

    })

    $("#tableData").on('click', '.plus-btn', function () {
        let id = $(this).attr('data-id');
        let number = $(`#input-quantity-${id}`).val();
        let index = dataSourceCart.findIndex(x => x.id == id);


        if (number == 100) {
            return;
        }
        $(`#input-quantity-${id}`).val(parseInt(number) + 1);
        dataSourceCart[index].quantity = $(`#input-quantity-${id}`).val();
        let totalPrice = dataSourceCart[index].price * $(`#input-quantity-${id}`).val();
        $(`#total-price-${id}`).text(formatNumber(totalPrice.toString()) + ' đ')
        //let tempPrice = dataSourceCart[index].price;
        //dataSourceCart[index].price = tempPrice * $(`#input-quantity-${id}`).val();   
        let newObj = {
            "id": id,
            "employerId": dataSourceCart[index].employerId,
            "employerServicePackageId": dataSourceCart[index].employerServicePackageId,
            "quantity": $(`#input-quantity-${id}`).val(),
            "description": "",
        }
        //genDataBill();
        updateQuantity(newObj);
    })

    $("#tableData").on('click', '.minus-btn', function () {
        let id = $(this).attr('data-id');
        let number = $(`#input-quantity-${id}`).val();
        let index = dataSourceCart.findIndex(x => x.id == id);

        if (number == 0) {
            return;
        }
        $(`#input-quantity-${id}`).val(parseInt(number) - 1);
        dataSourceCart[index].quantity = $(`#input-quantity-${id}`).val();
        //let tempPrice = dataSourceCart[index].price;
        //dataSourceCart[index].price = tempPrice * number;
        let totalPrice = dataSourceCart[index].price * $(`#input-quantity-${id}`).val();
        $(`#total-price-${id}`).text(formatNumber(totalPrice.toString()) + ' đ')

        let newObj = {
            "id": id,
            "employerId": dataSourceCart[index].employerId,
            "employerServicePackageId": dataSourceCart[index].employerServicePackageId,
            "quantity": $(`#input-quantity-${id}`).val(),
            "description": "",

        }
        //genDataBill();
        updateQuantity(newObj);
    })
    $("#check-all").click(function () {
        if ($(this).is(":checked")) {
            listItemCheckout = []
            $("#tableData .check-item").prop("checked", true).trigger('change');
        }
        else {
            $("#tableData .check-item").prop("checked", false).trigger('change');

        }
    })
    $(document).on('change', ".check-item", function () {
        //console.log()
        CheckBill("#" + $(this).attr('id'))
    })
})
function CheckBill(element) {
    //console.log($(element).attr('data-id'))
    let newItemCheckout = dataSourceCart.find(x => x.id == $(element).attr('data-id'));
    if ($(element).is(":checked")) {
        let itemDmo = listItemCheckout.find(x => x.id == $(element).attr('data-id'))
        if (itemDmo == null) {
            listItemCheckout.push(newItemCheckout);
        }
        else {
            let index = listItemCheckout.findIndex(x => x.id == itemDmo.id)
            listItemCheckout[index] = newItemCheckout;
        }
    }
    else {
        listItemCheckout = listItemCheckout.filter(x => x.id != newItemCheckout.id);
    }
    genDataBill();
}

async function addOrder(newOrder) {
    try {
        let result = await httpService.postAsync('api/EmployerOrder/add-order', newOrder);
        if (result.isSucceeded) {
            miniToast.fire({
                icon: 'success',
                html: 'Đơn hàng được tạo  thành công, bạn sẽ được chuyển hướng sang trang thanh toán'
            }).then(function () {
                window.location.href = "/don-hang-cua-toi"
            })
        }
    }
    catch (e) {
        console.error(e)
    }
}
var listItemCheckout = [];

