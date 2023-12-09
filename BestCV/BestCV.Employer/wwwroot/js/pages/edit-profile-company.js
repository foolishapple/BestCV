
var workPlaceId = 0;
var provinceId = 0;
var updatingId = 0;
var pathCover = "";
var pathLogo = "";
var pathLicense = "";
var dataSourse = [];
var showItem = ["licenseTypeName", "path", "isApproved", "approvalDate", "createdTime"];
var listActivityOfCompany = [];

setTimeout(function () {
    $(document).ready(function () {
        //sét giá trị khi chưa chọn tỉnh/thành phố, quận/huyện
        var newOption = new Option("Chọn tỉnh/ thành phố", -1, false, false);
        $("#city").append(newOption).trigger('change');
        var newOption1 = new Option("Chọn quận/ huyện", -1, false, false);
        $("#province").append(newOption1).trigger('change');

        $.when(loadCompanySize()).then(GetDetailCompany());
        loadCity();
        loadlicenseType();
        loadActivity();


    });
}, 1000)

//set sự kiện chọn ảnh
function setupCKUploadFile() {
    var inputElement = $(".cke_dialog_image_url .cke_dialog_ui_hbox_first input");
    var buttonFileElement = $(".cke_dialog_image_url .cke_dialog_ui_hbox_last a");
    buttonFileElement.addClass("choseFile");
    buttonFileElement.attr("data-fm-target", "#" + inputElement.attr("id"));
    buttonFileElement.attr("control-type", "ckeditor4");
    //$(".cke_dialog_body .cke_dialog_tabs a:nth-child(4)").remove();khi xóa sẽ bị bug
    //$(".cke_dialog_contents .cke_dialog_contents_body div:nth-child(4)").remove();
    buttonFileElement.click(function () {
        $("#fileInput").click();
    });

    $("#fileInput").change(function (event) {
        var selectedFile = event.target.files[0];
        console.log("Selected file:", selectedFile);
        uploadFileImgDescription(selectedFile);
    });
}
//upload ảnh trong description vào fileServer
async function uploadFileImgDescription(file) {
    try {
        let type = "cover-photo";
        var url = "api/file-explorer/upload/companys/" + type;
        var formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            let pathDescription = response.resources[0].path;
            let url = systemConfig.defaultStorage_URL + pathDescription;
            $(".cke_dialog_image_url .cke_dialog_ui_hbox_first input").val(url);
        }
    } catch (res) {

    }
}




/*
 * Lầy thông tin chi tiết của công ty
 */
async function GetDetailCompany() {
    try {
        let response = await httpService.getAsync("api/company/detailByEmployerId");

        if (response.isSucceeded) {
            $("#companyName").attr('disabled', 'disabled')
            var infoProfileCompany = response.resources;
            updatingId = infoProfileCompany.id;
            //set ảnh cho cover
            pathCover = infoProfileCompany.coverPhoto;
            let urlCover = systemConfig.defaultStorage_URL + infoProfileCompany.coverPhoto;
            $('#pxp-company-cover-choose-file').next('label').css({
                'background-image': `url('${urlCover}')`,
                'border': '0 none'
            }).find('span').hide();
            //set ảnh cho logo
            pathLogo = infoProfileCompany.logo;
            let urlLogo = systemConfig.defaultStorage_URL + infoProfileCompany.logo;
            $('#pxp-company-logo-choose-file').next('label').css({
                'background-image': `url('${urlLogo}')`,
                'border': '0 none'
            }).find('span').hide();

            $("#companyName").val(infoProfileCompany.name);
            $("#email").val(infoProfileCompany.emailAddress);
            $("#website").val(infoProfileCompany.website);
            $("#phone").val(infoProfileCompany.phone);
            //$("#description").val(infoProfileCompany.description);
            //hiển thị description qua ckeditor
            // Lấy nội dung từ cơ sở dữ liệu
            if (CKEDITOR.instances["description"].getData() == "" && infoProfileCompany.description != "") {
                CKEDITOR.instances["description"].setData(infoProfileCompany.description);
            }
            $("#overview").val(infoProfileCompany.overview)
            $("#taxCode").val(infoProfileCompany.taxCode);
            $("#foundedIn").val(infoProfileCompany.foundedIn);

            $("#companySize").val(infoProfileCompany.companySizeId);
            $("#companySize").append().trigger('change');

            $("#addressDetail").val(infoProfileCompany.addressDetail);
            $("#facebook").val(infoProfileCompany.facebookLink);
            $("#twitter").val(infoProfileCompany.twitterLink);
            $("#instagram").val(infoProfileCompany.instagramLink);
            $("#linkedin").val(infoProfileCompany.linkedinLink);
            //$("#videoIntro").val(infoProfileCompany.VideoIntro);

            //load vị trí của công ty theo tính thành phố, quận huyện
            if (infoProfileCompany.workPlaceId != null) {
                loadDetailWorkPlace(infoProfileCompany.workPlaceId);
            }
            //Lấy danh sách giấy giờ của công ty
            getListLicense();
            //Lấy danh sách lĩnh vực hoạt động của công ty
            loadActivityOfCompany(infoProfileCompany.id);
        }
    } catch (response) {

    }
}


/*
 * Lấy thông tin chi tiết địa điểm
 */
function loadDetailWorkPlace(id) {
    return $.ajax({
        url: DefaultAPIURL + "api/workplace/detail/" + id,
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            if (responseData.isSucceeded) {
                let data = responseData.resources;
                if (data.parentId == null) {
                    //hiển thị thành phố (vị trí của công ty)
                    $("#city").val(data.id);
                    $("#city").append().trigger('change');
                    //lấy danh sách quận huyện của thành phố 
                    loadProvince(data.id);
                } else {
                    provinceId = data.id;
                    //lấy danh sách quận huyện của thành phố 
                    loadProvince(data.parentId, provinceId);

                    ////hiển thị quận huyện (vị trí của công ty)
                    //$("#province").val(data.id);
                    //$("#province").append().trigger('change');

                    //hiển thị thành phố (vị trí của công ty)
                    $("#city").val(data.parentId);
                    $("#city").append().trigger('change');
                }
            }
        },
        error: function (e) {
        }
    });
}

/*
 * Lầy danh sách toàn bộ tỉnh thành
 */
function loadCity() {
    return $.ajax({
        url: DefaultAPIURL + "api/workplace/list-city",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData.resources;

            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                $("#city").append(newOption).trigger('change');

            });
            $("#city").select2({
                width: "100%"
            });
            $("#province").select2({
                width: "100%"
            });
        },
        error: function (e) {
        }
    });
}

/*
 * Lầy danh sách toàn bộ quận huyện của một thành phố
 */
function loadProvince(cityId, provinceId) {
    return $.ajax({
        url: DefaultAPIURL + "api/workplace/list-district-by-cityid/" + cityId,
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            if (responseData.isSucceeded) {
                var data = responseData.resources;

                data.forEach(function (item, index) {
                    var data = {
                        id: item.id,
                        text: item.name
                    };
                    var newOption = new Option(data.text, data.id, false, false);
                    $("#province").append(newOption).trigger('change');

                });
                //$("#province").select2();
                if (provinceId > 0) {
                    //hiển thị quận huyện (vị trí của công ty)
                    $("#province").val(provinceId);
                    $("#province").append().trigger('change');
                }
            }
        },
        error: function (e) {
        }
    });
}

/*
 * Lấy danh sách lĩnh vữ hoạt động của công ty
 */
function loadActivityOfCompany() {
    return $.ajax({
        url: DefaultAPIURL + "api/company-field-of-activity/list-by-companyId/" + updatingId,
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (response) {
            if (response.isSucceeded) {
                var data = response.resources;
                data.forEach(function (item) {
                    listActivityOfCompany.push(item.fieldOfActivityId);
                });
                $('#activity').val(listActivityOfCompany).trigger('change');
            }
        },
        error: function (e) {
        }
    });
}

/*
 * Lấy danh sách lĩnh vữ hoạt động
 */
function loadActivity() {
    return $.ajax({
        url: DefaultAPIURL + "api/field-of-activity/list",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (response) {
            if (response.isSucceeded) {
                var data = response.resources;
                data.forEach(function (item, index) {
                    var data = {
                        id: item.id,
                        text: item.name
                    };
                    var newOption = new Option(data.text, data.id, false, false);
                    $("#activity").append(newOption).trigger('change');
                });
                //$("#activity").select2();
                $("#activity").select2({
                    //dropdownParent: $("#defaultModal"),
                    allowClear: true,
                    data: data,
                    //templateResult: formatResult,
                    placeholder: "Lĩnh vực hoạt động",
                    width: "100%"
                });
            }
        },
        error: function (e) {
        }
    });
}

/*
 * Lầy danh sách loại giấy tờ
 */
function loadlicenseType() {
    return $.ajax({
        url: DefaultAPIURL + "api/license-type/list",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData.resources;

            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                $("#licenseType").append(newOption).trigger('change');

            });
            $("#licenseType").select2({
                width: "100%"
            });
        },
        error: function (e) {
        }
    });
}

/*
 * Lầy danh sách quy mô công ty
 */
function loadCompanySize() {
    return $.ajax({
        url: DefaultAPIURL + "api/company-size/list",
        type: 'GET',
        async: 'true',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData.resources;

            data.forEach(function (item, index) {
                var data = {
                    id: item.id,
                    text: item.name
                };
                var newOption = new Option(data.text, data.id, false, false);
                $("#companySize").append(newOption).trigger('change');

            });
            $("#companySize").select2({
                width: "100%"
            });
        },
        error: function (e) {
        }
    });
}

/*
 * Validate các trường
 */
function validate() {
    var errorList = [];
    var regexCompanyName = /^[\p{L}\s]+$/u;
    var regexPhone = /^[0-9]{10}$/;
    var regexFoundedIn = /^[1-9][0-9]*$/;
    var regexEmail = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
    var regexURL = /^(https?:\/\/)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/[^\s]*)?$/;
    var regexOnlyAlphanumeric = /^[a-zA-Z0-9]+$/;
    var currentYear = new Date().getFullYear();
    var regexFacebookURL = /^(?:https?:\/\/)?(?:www\.)?facebook\.com\/(?:groups\/[^/]+|pages\/[^/]+|[^/]+)\/?$/i;
    var regexTwitterURL = /^(?:https?:\/\/)?(?:www\.)?twitter\.com\/(?:[A-Za-z0-9_]{1,15}\/status\/\d+|[^/]+)\/?$/i;
    var regexTikTokURL = /^(?:https?:\/\/)?(?:www\.)?tiktok\.com\/(@[A-Za-z0-9._-]+|v\/\d+|music\/[A-Za-z0-9._-]+|hashtag\/[A-Za-z0-9._-]+|[^/]+)\/?$/i;
    var regexLinkedInURL = /^(?:https?:\/\/)?(?:www\.)?linkedin\.com\/(?:company\/[A-Za-z0-9_-]+|in\/[A-Za-z0-9_-]+|[^/]+)\/?$/i;
    var regexYouTubeURL = /^(?:https?:\/\/)?(?:www\.)?youtube\.com\/(?:watch\?v=[A-Za-z0-9_-]+|channel\/[A-Za-z0-9_-]+|playlist\?list=[A-Za-z0-9_-]+|user\/[A-Za-z0-9_-]+|[^/]+)\/?$/i;


    if ($("#companyName").val().length == 0 || $("#companyName").val().length > 255) {
        errorList.push("Tên công ty không được để trống và vượt quá 255 ký tự.");
    }
    if (!regexCompanyName.test($("#companyName").val())) {
        errorList.push("Tên công ty không được có ký tự đặc biệt.");
    }
    if (!regexPhone.test($("#phone").val())) {
        errorList.push("Số điện thoại có 10 ký tự và là chữ số.");
    }
    if (!regexEmail.test($("#email").val())) {
        errorList.push("Địa chỉ email không đúng định dạng.");
    }
    if ($("#website").val().trim().length > 0 && !regexURL.test($("#website").val())) {
        errorList.push("Đường dẫn website không đúng định dạng.");
    }
    if ($("#overview").val().length > 255) {
        errorList.push("Giới thiệu công ty không được dài quá 255 ký tự");
    }
    //if ($("#description").val().length > 500) {
    //    errorList.push("Mô tả không được vượt quá 500 ký tự.");
    //}
    if ($("#taxCode").val().length == 0 || $("#taxCode").val().length > 50 || !regexOnlyAlphanumeric.test($("#taxCode").val())) {
        errorList.push("Mã số thuế không được để trống hoặc không có ký tự đặc biệt và không vượt quá 50 ký tự.");
    }
    if (!regexFoundedIn.test($("#foundedIn").val()) || $("#foundedIn").val() > currentYear) {
        errorList.push("Năm thành lập không được để trống, phải lớn hơn 0 và nhỏ hơn hoặc bằng năm hiện tại.");
    }
    //if ($("#companySize").val().length == 0) {
    //    errorList.push("Quy mô công ty không được để trống.");
    //}
    if ($("#addressDetail").val().length == 0 || $("#addressDetail").val().length > 500) {
        errorList.push("Địa chỉ công ty không được để trống và vượt quá 50 ký tự.");
    }

    if ($("#facebook").val().length > 500 || ($("#facebook").val().length > 0 && !regexFacebookURL.test($("#facebook").val()))) {
        errorList.push("Đường dẫn facebook không đúng định dạng hoặc vượt quá 500 ký tự.");
    }
    if ($("#twitter").val().length > 500 || ($("#twitter").val().length > 0 && !regexTwitterURL.test($("#twitter").val()))) {
        errorList.push("Đường dẫn twitter không đúng định dạng hoặc vượt quá 500 ký tự.");
    }
    if ($("#tiktok").val().length > 500 || ($("#tiktok").val().length > 0 && !regexTikTokURL.test($("#tiktok").val()))) {
        errorList.push("Đường dẫn tiktok không đúng định dạng hoặc vượt quá 500 ký tự.");
    }
    if ($("#linkedin").val().length > 500 || ($("#linkedin").val().length > 0 && !regexLinkedInURL.test($("#linkedin").val()))) {
        errorList.push("Đường dẫn linkedin không đúng định dạng hoặc vượt quá 500 ký tự.");
    }
    if ($("#youtube").val().length > 500 || ($("#youtube").val().length > 0 && !regexYouTubeURL.test($("#youtube").val()))) {
        errorList.push("Đường dẫn youtube không đúng định dạng hoặc vượt quá 500 ký tự.");
    }
    //if ($("#videoIntro").val().length > 500) {
    //    errorList.push("Đường dẫn videoIntro không được vượt quá 50 ký tự.");
    //}
    if ($("#city").val() == -1) {
        errorList.push("Bạn chưa chọn tỉnh/thành phố.");
    } else {
        if ($("#province").val() == -1) {
            workPlaceId = $("#city").val();
        } else {
            workPlaceId = $("#province").val();
        }
    }


    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin công ty không thành công</p>";

        Swal.fire(
            'Thông tin công ty' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        updateProfileCompany();
    }
}

$("#submitButtonCompany").click(function () {
    updateActivityOfCompany();
})

/*
 * cập nhật thông tin của company
 */
function updateProfileCompany() {
    // Lấy tham chiếu đến CKEditor
    var editor = CKEDITOR.instances.description;

    // Lấy nội dung HTML từ CKEditor
    var content = editor.getData();

    // Thông tin công ty cần update
    var obj = {
        name: $("#companyName").val().trim(),
        employerId: 0,
        emailAddress: $("#email").val(),
        website: $("#website").val(),
        phone: $("#phone").val(),
        logo: pathLogo,
        coverPhoto: pathCover,
        description: content,
        taxCode: $("#taxCode").val(),
        foundedIn: $("#foundedIn").val(),
        companySizeId: $("#companySize").val(),
        addressDetail: $("#addressDetail").val(),
        location: "",
        facebookLink: $("#facebook").val(),
        twitterLink: $("#twitter").val(),
        tiktokLink: $("#tiktok").val(),
        linkedinLink: $("#linkedin").val(),
        videoIntro: "",
        workPlaceId: workPlaceId,
        overview: $("#overview").val()
    }


    var actionName = "Cập nhật thông tin";
    swal.fire({
        title: actionName,
        html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " cho công ty <b>" + $("#companyName").val() + '</b>?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then((result) => {
        if (result.isConfirmed) {
            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $("#loading").addClass("show");
                //update thông tin công ty trừ lĩnh vữ hoạt động
                delete obj.name;
                $.ajax({
                    url: DefaultAPIURL + "api/company/update-profile-company",
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    beforeSend: function (xhr) {
                        if (localStorage.token) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                        }
                    },
                    success: function (responseData) {
                        $("#loading").removeClass("show");
                        if (responseData.isSucceeded) {
                            let infoProfile = responseData.resources;
                            var swaltitlesuccess = "<p class='swal-subtitle'>" + actionName + " thành công</p>";
                            Swal.fire(
                                'Thông tin công ty' + swaltitlesuccess,
                                'Thông tin công ty <b>' + $("#companyName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            );
                        }
                        if (!responseData.isSucceeded) {
                            var listError = responseData.errors;
                            if (listError != null) {
                                var contenterror = "<ul>";
                                listError.forEach(function (item) {
                                    contenterror += "<li class='text-start'>" + item + "</li>";
                                })
                                contenterror += "</ul>";
                                var swalsubtitle = "<p class='swal-subtitle'>" + actionName + " không thành công</p>";
                                swal.fire(
                                    'Thông tin công ty' + swalsubtitle,
                                    contenterror,
                                    'warning'
                                );
                            } else {
                                swal.fire(
                                    'Thông tin công ty' + swalsubtitle,
                                    responseData.title,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        swal.fire(
                            'Lỗi khi cập nhật !',
                            'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                            'error'
                        );
                    }
                });


            };
            //CALL AJAX TO CREATE
            if (updatingId == 0) {
                $("#loading").addClass("show");
                $.ajax({
                    url: DefaultAPIURL + "api/company/add-profile-company",
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    beforeSend: function (xhr) {
                        if (localStorage.token) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                        }
                    },
                    success: function (responseData) {
                        $("#loading").removeClass("show");
                        if (responseData.isSucceeded) {
                            let infoProfile = responseData.resources;
                            var swaltitlesuccess = "<p class='swal-subtitle'>" + actionName + " thành công</p>";
                            Swal.fire(
                                'Thông tin công ty' + swaltitlesuccess,
                                'Thông tin công ty <b>' + $("#companyName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            );
                        }
                        if (!responseData.isSucceeded) {
                            var listError = responseData.errors;
                            if (listError != null) {
                                var contenterror = "<ul>";
                                listError.forEach(function (item) {
                                    contenterror += "<li class='text-start'>" + item + "</li>";
                                })
                                contenterror += "</ul>";
                                var swalsubtitle = "<p class='swal-subtitle'>" + actionName + " không thành công</p>";
                                swal.fire(
                                    'Thông tin công ty' + swalsubtitle,
                                    contenterror,
                                    'warning'
                                );
                            } else {
                                swal.fire(
                                    'Thông tin công ty' + swalsubtitle,
                                    responseData.errors,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        swal.fire(
                            'Lỗi khi cập nhật !',
                            'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                            'error'
                        );
                    }
                });
            }
        }
    });
}

/*
 * cập nhật thông tin lĩnh vực hoạt động của công ty
 */
async function updateActivityOfCompany() {
    //Danh sách cần update hiện tại
    var listActivityCurrent = [];
    var activityCurrent = $("#activity").val();
    activityCurrent.forEach(function (item) {
        var dataActivity = {
            companyId: updatingId,
            fieldOfActivityId: parseInt(item)
        };
        listActivityCurrent.push(dataActivity);
    });

    //Xóa tất cả các lĩnh vực mà công ty đang có 
    if (updatingId != 0) {
        await $.ajax({
            url: DefaultAPIURL + "api/company-field-of-activity/deleteByCompanyId/" + updatingId,
            type: 'DELETE',
            async: 'true',
            contentType: 'application/json',
            success: function (responseData) {
                if (responseData.isSucceeded) {
                    $.ajax({
                        url: DefaultAPIURL + "api/company-field-of-activity/add",
                        type: "POST",
                        async: "true",
                        contentType: "application/json",
                        data: JSON.stringify(listActivityCurrent),
                        beforeSend: function (xhr) {
                            if (localStorage.token) {
                                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                            }
                        },
                        success: function (response) {
                            $("#loading").removeClass("show");
                            if (response.isSucceeded) {
                                validate();
                            }
                        },
                        error: function (e) {
                            $("#loading").removeClass("show");
                            swal.fire(
                                'Lỗi khi cập nhật !',
                                'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                                'error'
                            );
                        }
                    });
                }
            },
            error: function (e) {
                $("#loading").removeClass("show");
                swal.fire(
                    'Lỗi khi cập nhật !',
                    'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                    'error'
                );
            }
        });
    }
    else {
        $.ajax({
            url: DefaultAPIURL + "api/company-field-of-activity/add",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(listActivityCurrent),
            beforeSend: function (xhr) {
                if (localStorage.token) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                }
            },
            success: function (responseData) {
                $("#loading").removeClass("show");
                if (responseData.isSucceeded) {

                }
            },
            error: function (e) {
                $("#loading").removeClass("show");
                swal.fire(
                    'Lỗi khi cập nhật !',
                    'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                    'error'
                );
            }
        });
    }

}


/*
 * chọn ảnh cho cover
 */
$("#pxp-company-cover-choose-file").change(function (e) {
    var file = (e.target.files)[0];
    var nameType = file.type.split('/');
    var typeFile = nameType[0];
    if (typeFile == "image") {
        if (file.size > (5 * 1024 * 1024)) {

            html = '<div style="text-align:left;">Tệp: </div>' + file.name + 'mà bạn đã chọn quá lớn. Kích thước tối đa là <strong>5</strong> MB.';

            Swal.fire({
                title: 'Lưu ý',
                icon: 'warning',
                html: html,
                focusConfirm: true,
                allowEnterKey: true

            })
        }
        else {
            var type = "cover-photo";
            uploadFile(file, type);
        }
    }
    else {
        let html = '<li>Ảnh phải có định dạng của file image ví dụ như ".jpg", ".jpeg", ".png".</li>';

        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
})

/*
 * chọn ảnh cho logo
 */
$("#pxp-company-logo-choose-file").change(function (e) {
    var file = (e.target.files)[0];
    var typeFile = file.type.split('/')[0];
    if (typeFile == "image") {
        if (file.size > (5 * 1024 * 1024)) {

            html = '<div style="text-align:left;">Tệp: </div>' + file.name + 'mà bạn đã chọn quá lớn. Kích thước tối đa là <strong>5</strong> MB.';

            Swal.fire({
                title: 'Lưu ý',
                icon: 'warning',
                html: html,
                focusConfirm: true,
                allowEnterKey: true

            })
        }
        else {
            var type = "avatars";
            uploadFile(file, type);
        }
    }
    else {
        let html = '<li>Ảnh phải có định dạng của file image ví dụ như ".jpg", ".jpeg", ".png".</li>';

        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
})

/*
 * gọi API để upload ảnh 
 */
async function uploadFile(file, type) {
    try {
        var url = "api/file-explorer/upload/companys/" + type;
        var formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            if (type == "cover-photo") {
                pathCover = response.resources[0].path;
                let url = systemConfig.defaultStorage_URL + pathCover;
                $('#pxp-company-cover-choose-file').next('label').css({
                    'background-image': `url('${url}')`,
                    'border': '0 none'
                }).find('span').hide();
            }
            if (type == "avatars") {
                pathLogo = response.resources[0].path;
                let url = systemConfig.defaultStorage_URL + pathLogo;
                $('#pxp-company-logo-choose-file').next('label').css({
                    'background-image': `url('${url}')`,
                    'border': '0 none'
                }).find('span').hide();
            }
        }
    } catch (res) {

    }
}



/*
 * hàm lấy ra danh sách các giấy tờ của một công ty 
 */
async function getListLicense() {
    try {
        let response = await httpService.getAsync("api/license/listByCompanyId/" + updatingId);

        if (response.isSucceeded) {
            dataSource = response.resources;
            loadTable();
        }
    } catch (response) {

    }
}

/*
 * load danh sách giấy tờ vào bảng
 */
function loadTable() {
    var index = 0;
    $("#licenseTable tbody").html("");
    dataSource.forEach(function (item, index) {
        var rowContent = "<tr>";
        rowContent += "<td style='text-align: center;'>" + (index + 1) + "</td>";
        showItem.forEach(function (key) {
            if (key == "path") {
                var linkFile = item[key].split('/');
                var nameFile = linkFile[linkFile.length - 1];
                rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + nameFile + "</td>";
            }
            else if (key == "createdTime") {
                rowContent += "<td class=' row-" + item.id + "-column column-" + key + "' property='" + key + "'>" + moment(item[key], "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss") + "</td>";
            }
            else if (key == "approvalDate") {
                if (item[key] != null) {
                    rowContent += "<td class=' row-" + item.id + "-column column-" + key + "' property='" + key + "'>" + moment(item[key], "YYYY-MM-DD hh:mm:ss").format("DD/MM/YYYY hh:mm:ss") + "</td>";
                }
                else {
                    rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + "" + "</td>";
                }
            }
            else if (key == "isApproved") {
                if (item[key] == true) {
                    rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + "Đã duyệt" + "</td>";
                } else {
                    rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + "Chưa duyệt" + "</td>";
                }
            }
            else {
                rowContent += "<td class='row" + item.id + "-column column-" + key + "' property='" + key + "'>" + item[key] + "</td>";
            }
        })

        rowContent += "</div></td ></tr>";
        $(rowContent).appendTo($("#licenseTable tbody"));
        $("#licenseTable tbody").show();
    })
}

/*
 * load danh sách giấy tờ vào bảng
 */
$("#addNewLicense").click(function () {
    $("#add").slideToggle();
});

/*
 * chọn thêm giấy tờ của công ty
 */
$("#myfile").change(function (e) {
    var file = (e.target.files)[0];
    var nameType = file.type.split('/');
    var typeFile = nameType[0];
    if (typeFile == "image" || file.type == "application/pdf") {
        if (file.size > (5 * 1024 * 1024)) {

            html = '<div style="text-align:left;">Tệp: </div>' + file.name + 'mà bạn đã chọn quá lớn. Kích thước tối đa là <strong>5</strong> MB.';

            Swal.fire({
                title: 'Lưu ý',
                icon: 'warning',
                html: html,
                focusConfirm: true,
                allowEnterKey: true

            })
        }
        else {
            //chèn thông tin mới up vào
            var type = "avatars";
            uploadFileLicense(file, type);
        }
    }
    else {
        let html = '<li>Giấy tờ tải lên phải là ảnh hoặc có đuôi tệp tin là .pdf</li>';

        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
        pathLicense = null;
    }
});

/*
 * gọi API để upload giấy tờ
 */
async function uploadFileLicense(file, type) {
    try {
        var url = "api/file-explorer/upload/companys/" + type;
        var formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            pathLicense = response.resources[0].path;
        }
    } catch (res) {

    }
}

/*
 * gọi API thêm mới giấy tờ
 */
async function addLicense() {
    var obj = {
        companyId: updatingId,
        licenseTypeId: $("#licenseType").val(),
        path: pathLicense,
        isApproved: false,
        approvalDate: null
    }
    try {
        $("#loading").addClass("show");
        let response = await httpService.postAsync("api/license/add", obj);

        $("#loading").removeClass("show");
        if (response.isSucceeded) {
            Swal.fire({
                title: "Thông tin công ty",
                text: "Giấy tờ đã được thêm mới thàng công!",
                icon: "success"
            }).then((e) => {
                reGenTable();
            })
            
        }
    } catch (response) {
        $("#loading").removeClass("show");
    }
}

$("#submitButtonSave").click(function () {
    if (pathLicense != null && pathLicense.length != 0) {
        addLicense();
    }
    else if (pathLicense == null) {
        let html = '<li>Vui lòng chọn tệp tải lên đúng với yêu cầu định dạng</li>';

        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
});

/*
 * bắt sự kiện khi chọn thành phố lấy danh sách quận huyện
 */
$("#chooseCity").click(function () {
    console.log("đã ấn");
    $("#city").on("change", function () {
        console.log("đã chọn");
        $("#province").empty().select2({
            width: "100%"
        });
        var newOption1 = new Option("Chọn quận/ huyện", -1, false, false);
        $("#province").append(newOption1).trigger('change');
        loadProvince($("#city").val());
    });
});


