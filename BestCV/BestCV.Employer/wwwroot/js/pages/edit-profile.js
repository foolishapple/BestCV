

var pathLogo = "";

$(document).ready(function () {
    //GetDetail();
    //loadPosition();
    $.when(loadPosition()).then(GetDetail());
    $("#gender").select2({
        "width": "100%"
    });
});

// lấy thông tin cá nhân của employer
function GetDetail() {
    $.ajax({
        url: DefaultAPIURL + "api/employer/detail",
        type: "GET",
        contentType: 'application/json',
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },

        success: function (response) {
            if (response.isSucceeded) {
                var infoProfile = response.resources;
                updatingId = infoProfile.id;

                //set ảnh cho logo
                pathLogo = (infoProfile.photo == null || infoProfile.photo.trim().length == 0) ? systemConfig.defaultAvatar : infoProfile.photo;
                let urlLogo = systemConfig.defaultStorage_URL + pathLogo;
                $('#pxp-company-logo-choose-file').next('label').css({
                    'background-image': `url('${urlLogo}')`,
                    'border': '0 none'
                }).find('span').hide();

                $("#fullName").val(infoProfile.fullname);
                $("#email").val(infoProfile.email);
                $("#gender").val(infoProfile.gender);
                $("#gender").append().trigger('change');

                $("#position").val(infoProfile.positionId);
                $("#phone").val(infoProfile.phone);
                $("#skypeAccount").val(infoProfile.skypeAccount);
            }
        },
        error: function (ex) {
            
        }
    });
};



// lấy danh sách chức vụ
function loadPosition() {
    return $.ajax({
        url: DefaultAPIURL + "api/position/list",
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
                $("#position").append(newOption).trigger('change');
            });

            $("#position").select2({
                "width":"100%"
            });

        },
        error: function (e) {
        }
    });
}

function validate() {
    var errorList = [];
    var regexFullName = /^[\p{L}\s]+$/u;

    if ($("#fullName").val().length == 0 || $("#fullName").val().length > 255) {
        errorList.push("Họ và tên không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#skypeAccount").val().length > 500) {
        errorList.push("Tài khoản skype không được vượt quá 500 ký tự.");
    }
    if (!regexFullName.test($("#fullName").val())) {
        errorList.push("Họ và tên không được có số hoặc ký tự đặc biệt.");
    }
    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin cá nhân không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        UpdateProfile();
    }
}

$("#submitButton").click(function () {
    validate();
})

// cập nhật thông tin cá nhân của employer
function UpdateProfile() {
    var obj = {
        "Id": 0,
        "Fullname": $("#fullName").val(),
        "Gender": $("#gender").val(),
        "PositionId": $("#position").val(),
        "Photo": pathLogo,
        "SkypeAccount": $("#skypeAccount").val()
    }

    var actionName = "Cập nhật thông tin";
    swal.fire({
        title: actionName,
        html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " cho tài khoản <b>" + $("#fullName").val() + '</b>?',
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
                $.ajax({
                    url: DefaultAPIURL + "api/employer/update-profile",
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
                                'Tài khoản' + swaltitlesuccess,
                                'Tài khoản <b>' + $("#fullName").val() + '</b> đã được cập nhật thành công.',
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
                                    'Tài khoản' + swalsubtitle,
                                    contenterror,
                                    'warning'
                                );
                            } else {
                                swal.fire(
                                    'Tài khoản' + swalsubtitle,
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
        }
    });
}

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
            pathLogo = response.resources[0].path;
            let url = systemConfig.defaultStorage_URL + pathLogo;
            $('#pxp-company-logo-choose-file').next('label').css({
                'background-image': `url('${url}')`,
                'border': '0 none'
            }).find('span').hide();
        }
    } catch (res) {

    }
}