/*
    created by: HuyDQ
    createTime: 08/08/2023
 */

$(document).ready(function () {
    GetDetailCompany();
    loadActivityOfCompany();
    GetListJobOfCompany();
});


var urlCover;
var urlLogo;
var dataSourse = [];
var dataActivity = [];
async function GetDetailCompany() {
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/company/detailAggregate/" + idCompany,
        type: "GET",
        success: function (data) {
            if (data.isSucceeded) {
                //console.log(data);
                dataSourse = data.resources;
                //set ảnh cho cover
                urlCover = systemConfig.defaultStorageURL + dataSourse.coverPhoto;
                $('#company-cover').css({
                    'background-image': `url('${urlCover}')`
                });
                //set ảnh cho logo
                urlLogo = systemConfig.defaultStorageURL + dataSourse.logo;
                $('#company-logo').css({
                    'background-image': `url('${urlLogo}')`
                });
                //set tên công ty
                $("#company-name").html(dataSourse.name);
                $("#company-listJob").html("Danh sách làm việc được đăng bởi " + dataSourse.name);
                //set mô tả về công ty
                $("#company-about").html("Về " + dataSourse.name);

                // Lấy nội dung từ cơ sở dữ liệu
                var contentFromDatabase = dataSourse.description;
                $('#company-about-desription').html(contentFromDatabase);



                //set địa chỉ chi tiết công ty
                $("#address-detail").html(dataSourse.addressDetail);
                $("#company-address-detail").html(dataSourse.addressDetail);
                //set quy mô công ty
                $("#company-size").html(dataSourse.companySizeName);
                //set năm thành lập
                $("#company-foundedIn").html(dataSourse.foundedIn);

                //set số điện thoại
                //Sử dụng hàm formatPhoneNumber để định dạng số điện thoại
                var formattedPhoneNumber = formatPhoneNumber(dataSourse.phone);
                $("#company-phone").text(formattedPhoneNumber);

                //set email
                $("#company-email").html(dataSourse.emailAddress);

                //set website công ty
                $("#company-website").html(dataSourse.website);
                $("#company-website").attr("href", dataSourse.website);

                //set social
                if (dataSourse.facebookLink != null && dataSourse.facebookLink != "") {
                    $("#facebook .fa-facebook").removeClass("d-none");
                    $("#facebook").attr("href", dataSourse.facebookLink);
                }
                if (dataSourse.twitterLink != null && dataSourse.twitterLink != "") {
                    $("#twitter .fa-twitter").removeClass("d-none");
                    $("#twitter").attr("href", dataSourse.twitterLink);
                }
                if (dataSourse.linkedinLink != null && dataSourse.linkedinLink != "") {
                    $("#linkedin .fa-linkedin").removeClass("d-none");
                    $("#linkedin").attr("href", dataSourse.linkedinLink);
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
}

// Hàm để định dạng số điện thoại vào định dạng chuẩn
function formatPhoneNumber(phoneNumber) {
    // Loại bỏ tất cả các ký tự không phải số
    var cleaned = phoneNumber.replace(/\D/g, '');

    // Kiểm tra độ dài của số điện thoại
    var match = cleaned.match(/^(\d{3})(\d{3})(\d{4})$/);

    // Nếu match, định dạng lại số điện thoại
    if (match) {
        return '(' + match[1] + ') ' + match[2] + '-' + match[3];
    }

    // Nếu không match, trả về số điện thoại nguyên gốc
    return phoneNumber;
}

/*
 * Lấy danh sách lĩnh vữ hoạt động của công ty
 */

function loadActivityOfCompany() {
    return $.ajax({
        url: systemConfig.defaultAPIURL + "api/company-field-of-activity/list-by-companyId/" + idCompany,
        type: 'GET',
        contentType: 'application/json',
        success: function (response) {
            if (response.isSucceeded) {
                var content = "";
                if (response.resources.length < 1) {
                    $("#company-activity").text("...");
                }
                if (response.resources.length == 1) {
                    $("#company-activity").text(response.resources[0].fieldOfActivityName);
                }
                if (response.resources.length > 1) {
                    response.resources.forEach(function (item) {
                        //console.log(item);
                        content = content + item.fieldOfActivityName + ", ";
                    });
                    content = content.slice(0, -2);
                    $("#company-activity").text(content);
                }
                //$("#company-activity").text(formattedPhoneNumber);
            }
        },
        error: function (e) {
        }
    });
}

//Hiển thị việc làm được đăng tải bởi công ty
var dataJob = []
async function GetListJobOfCompany() {
    try {
        let response = await httpService.getAsync("api/job/job-on-detail-company-page/" + idCompany);
        if (response.isSucceeded) {
            dataJob = response.resources;
            var content = "";
            dataJob.forEach(function (item) {
                let logo = systemConfig.defaultStorageURL + item.companyLogo;

                var placeWork = "";
                if (item.jobRequireCity.length == 0) {
                    placeWork = "Địa điểm làm việc linh hoạt";
                }
                if (item.jobRequireCity.length == 1) {
                    placeWork = item.jobRequireCity[0].cityName;
                }
                if (item.jobRequireCity.length > 1) {
                    placeWork = item.jobRequireCity[0].cityName + ", ...";
                }
                console.log(item.jobRequireCity);
                content += `<div class="col-xl-6 pxp-jobs-card-2-container">
                    <div class="pxp-jobs-card-2 pxp-has-border">
                        <div class="pxp-jobs-card-2-top">
                            <a href="/chi-tiet-cong-viec?jobId=${item.id}" class="pxp-jobs-card-2-company-logo" style="background-image: url(${logo});"></a>
                            <div class="pxp-jobs-card-2-info">
                                <a href="/chi-tiet-cong-viec?jobId=${item.id}" class="pxp-jobs-card-2-title">${item.name}</a>
                                <div class="pxp-jobs-card-2-details">
                                    <a href="/chi-tiet-cong-viec?jobId=${item.id}" class="pxp-jobs-card-2-location">
                                        <span class="fa fa-map-marker"></span>${placeWork}
                                    </a>
                                    <div class="pxp-jobs-card-2-type">${item.jobTypeName}</div>
                                </div>
                            </div>
                        </div>
                        <div class="pxp-jobs-card-2-bottom">
                            <a href="/chi-tiet-cong-viec?jobId=${item.id}" class="pxp-jobs-card-2-category">
                                <div class="pxp-jobs-card-2-category-label">${item.companyName}</div>
                            </a>
                            <div class="pxp-jobs-card-2-bottom-right">
                                <span class="pxp-jobs-card-2-date pxp-text-light ${item.applyEndDate == null ? "d-none" : ""}">${item.applyEndDate != null ? "Còn <b>" + countRemainDay(item.applyEndDate) + "</b> ngày để ứng tuyển" : ""} </span>
                                <a href="single-company-1.html" class="pxp-jobs-card-2-company"></a>
                            </div>
                        </div>
                    </div>
                </div>`
            });
            $("#list-job").html();
            $("#list-job").html(content);
        }
    } catch (ex) {

    }
}

function countRemainDay(input) {
    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    var firstDate = new Date();
    var secondDate = new Date(input);

    var diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
    return diffDays;
}