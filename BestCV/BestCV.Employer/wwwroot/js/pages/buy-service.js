async function LoadDataServicePackage() {
    try {
        let result = await httpService.getAsync("api/employer-service-package/list");
        if (result.isSucceeded) {
            dataServicePackage = result.resources;
            genDataServicePackage();
        }
    } catch (e) {
        console.error(e);
    }
}
var dataServicePackage = [];
LoadDataServicePackage();
function genDataServicePackage() {
    if (dataServicePackage.length > 0) {
        let listItemPostJob = dataServicePackage.filter(x => x.servicePackageTypeId == SPT_REQUIREMENT);
        let listItemPremium = dataServicePackage.filter(x => x.servicePackageTypeId == SPT_PREMIUM);
        let listItemAddOn = dataServicePackage.filter(x => x.servicePackageTypeId == SPT_ADD_ON);
        let listItemCredit = dataServicePackage.filter(x => x.servicePackageTypeId == SPT_CREDIT);
        let listItemAdvanced = dataServicePackage.filter(x => x.servicePackageTypeId == SPT_ADVANCED);

        //load data post job
        let contentPostJob = "";
        listItemPostJob.forEach(function (item) {
            contentPostJob += `<div class="col-md-4 col-lg-4 col-sm-6 mb-4">
                    <div class="card card-service-style" data-id="${item.id}" title="Nháy đúp để xem thông tin chi tiết dịch vụ">
                        <div class="card-body d-flex flex-column justify-content-between">
                            <div>
                                <h4 class='service-name' title="${item.name}">${item.name}</h4>
                                <h4 class="service-package-price">${formatNumber(item.price.toString())} đ</h4>
                                <p class="service-package-description">${item.description != null ? item.description : ""}</p>
                            </div>
                            <div class="d-flex justify-content-between">
                                <button class="btn btn-outline-warning btn-md btn-buy btn-add-to-cart col-lg-6 col-md-12 col-sm-12" data-id="${item.id}"><span class="fa-regular fa-cart-plus"></span> Thêm vào giỏ</button>
                                <button class="btn btn-warning btn-md btn-buy btn-check-out  col-lg-6 col-md-12 col-sm-12" data-id="${item.id}">Mua ngay</button>
                            </div>
                        </div>
                    </div>
                </div>`;
        })
        $("#content-post-job").html(contentPostJob);

        //load data premium post job
        let contentPremiumJob = "";
        listItemPremium.forEach(function (item) {
            contentPremiumJob += `<div class="col-md-4 col-lg-4 col-sm-6 mb-4">
                    <div class="card card-service-style" data-id="${item.id}" title="Nháy đúp để xem thông tin chi tiết dịch vụ">
                        <div class="card-body d-flex flex-column justify-content-between">
                            <div>
                                <h4 class='service-name' title="${item.name}">${item.name}</h4>
                                <h4 class="service-package-price">${formatNumber(item.price.toString())} đ</h4>
                                <p class="service-package-description">${item.description != null ? item.description : ""}</p>
                            </div>
                            <div class="d-flex justify-content-between">
                                <button class="btn btn-outline-warning btn-md btn-buy btn-add-to-cart" data-id="${item.id}"><span class="fa-regular fa-cart-plus"></span> Thêm vào giỏ</button>
                                <button class="btn btn-warning btn-md btn-buy btn-check-out" data-id="${item.id}">Mua ngay</button>
                            </div>
                        </div>
                    </div>
                </div>`;
        })
        $("#content-premium").html(contentPremiumJob);

        //load data add-on
        let contentAddOn = "";
        listItemAddOn.forEach(function (item) {
            contentAddOn += `<div class="col-md-4 col-lg-4 col-sm-6 mb-4">
                    <div class="card card-service-style" data-id="${item.id}" title="Nháy đúp để xem thông tin chi tiết dịch vụ">
                        <div class="card-body d-flex flex-column justify-content-between">
                            <div>
                                <h4 class='service-name' title="${item.name}">${item.name}</h4>
                                <h4 class="service-package-price">${formatNumber(item.price.toString())} đ</h4>
                                <p class="service-package-description">${item.description != null ? item.description : ""}</p>
                            </div>
                            <div class="d-flex justify-content-between">
                                <button class="btn btn-outline-warning btn-md btn-buy btn-add-to-cart" data-id="${item.id}"><span class="fa-regular fa-cart-plus"></span> Thêm vào giỏ</button>
                                <button class="btn btn-warning btn-md btn-buy btn-check-out" data-id="${item.id}">Mua ngay</button>
                            </div>
                        </div>
                    </div>
                </div>`;
        })
        $("#content-add-on").html(contentAddOn);


        //load data credit
        let contentCredit = "";
        listItemCredit.forEach(function (item) {
            contentCredit += `<div class="col-md-4 col-lg-4 col-sm-6 mb-4">
                    <div class="card card-service-style" data-id="${item.id}" title="Nháy đúp để xem thông tin chi tiết dịch vụ">
                        <div class="card-body d-flex flex-column justify-content-between">
                            <div>
                                <h4 class='service-name' title="${item.name}">${item.name}</h4>
                                <h4 class="service-package-price">${formatNumber(item.price.toString())} đ</h4>
                                <p class="service-package-description">${item.description != null ? item.description : ""}</p>
                            </div>
                            <div class="d-flex justify-content-between">
                                <button class="btn btn-outline-warning btn-md btn-buy btn-add-to-cart" data-id="${item.id}"><span class="fa-regular fa-cart-plus"></span> Thêm vào giỏ</button>
                                <button class="btn btn-warning btn-md btn-buy btn-check-out" data-id="${item.id}">Mua ngay</button>
                            </div>
                        </div>
                    </div>
                </div>`;
        })
        $("#content-credit").html(contentCredit);

    }
}

$(document).ready(function () {
    $(document).on('dblclick', ".card-service-style", async function () {
        /*console.log($(this).attr('data-id'))*/
        let id = $(this).attr('data-id');
        await getItemById(id);
        await getBenefitById(id)
        $("#other-benefit-div").html('');
        if (editingObj != null) {
            $("#modal-label").text(editingObj.name)
            $("#modal-sub-label").html(editingObj.description);
            $("#service-package-price").text(formatNumber(editingObj.price.toString()) + " đ")
            $("#service-package-name").text(editingObj.name)
            $("#service-package-name").attr("title",editingObj.name)
        }
        $("#display-time-div").addClass('d-none');
        $("#credit-gift-div").addClass('d-none');
        $("#push-top-div").addClass('d-none');
        if (listBenefit.length > 0) {
            listBenefit.forEach(function (item) {
                //thời gian hiển thị tin
                if (item.benefitId == 1003) {
                    $("#display-time").text("Thời gian hiển thị tin: " + item.value + " ngày");
                    $("#display-time-div").removeClass('d-none');

                }
                //Quà tặng credit
                else if (item.benefitId == 1002) {
                    $("#credit-gift").text("Quà tặng: " + item.value + " credit")
                    $("#credit-gift-div").removeClass('d-none');

                }
                //Đẩy top khung giờ vàng
                else if (item.benefitId == 1008) {
                    $("#push-top").text(item.value + " lần đẩy top Khung giờ vàng")
                    $("#push-top-div").removeClass('d-none');

                }
                else {
                    $("#other-benefit-div").append(`<div><span class="fa-solid fa-circle-check text-success"></span><span> ${item.benefitName}</span></div>`)
                }

            })

        }

        $("#modal-btn-quick-check-out").attr('data-id', editingObj.id);
        $("#modal-btn-add-to-cart").attr('data-id', editingObj.id);
        $("#editModal").modal('show')
    })

    $(document).on('click', '.btn-add-to-cart', function (e) {
        e.preventDefault();
        let id = $(this).attr('data-id')
        addToCart(id);
    })

    $(document).on('click', '.btn-check-out', function (e) {
        e.preventDefault();
        let id = $(this).attr('data-id')
        quickCheckout(id);
    })

    $("#modal-btn-quick-check-out").click(function (e) {
        e.preventDefault();
        let id = $(this).attr('data-id')
        quickCheckout(id);
    })

    $("#modal-btn-add-to-cart").click(function (e) {
        e.preventDefault();
        let id = $(this).attr('data-id')
        addToCart(id);
    })
})
var editingObj = [];
async function getItemById(id) {
    try {
        let result = await httpService.getAsync("api/employer-service-package/detail/" + id);
        if (result.isSucceeded) {
            editingObj = result.resources;
        }
    } catch (e) {
        console.error(e);
    }
}

var listBenefit = [];
async function getBenefitById(id) {
    try {
        let result = await httpService.getAsync("api/service-package-benefit/list-by-service-package-id/" + id);
        if (result.isSucceeded) {
            listBenefit = result.resources;
        }
    } catch (e) {
        console.error(e);
    }
}

async function addToCart(id) {
    try {
        let objService = dataServicePackage.find(x => x.id == id);
        let result = await httpService.postAsync("api/employer-cart/add-to-cart/" + id);
        if (result.isSucceeded) {

            miniToast.fire({
                icon: 'success',
                html: 'Bạn đã thêm dịch vụ </br><b>' + objService.name + '</b> vào giỏ hàng'
            })
            countServicePackageInCart();
        }
    } catch (e) {
        console.error(e);
    }
}

async function quickCheckout(id) {
    try {
        let result = await httpService.postAsync("api/employer-cart/add-to-cart/" + id);
        if (result.isSucceeded) {
            location.href = '/gio-hang?dv=' + id;
        }
    } catch (e) {
        console.error(e);
    }
}