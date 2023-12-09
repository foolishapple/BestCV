




$(document).ready(function () {
    loadDataPosition();
})

/**
 * hàm validate các trường
 */
function validateInput() {
    let listErrors = [];
    let reEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    let reFullName = /^[0-9!@#$%^&*()_+,\-./:;<=>?@[\\\]^_`{|}~]*$/;
    let rePhoneNumber = /(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b/;
    let password = $('#pxp-signup-page-password').val();
    let rePassword = $('#pxp-signup-page-re-password').val();
    let email = $('#pxp-signup-page-email').val();
    if (email == null || email.trim().length == 0) {
        listErrors.push("Email không được để trống")
    }
    else if (!email.match(reEmail)) { 
        listErrors.push("Email không đúng định dạng")
    }
    // so sánh password
    if (password.trim().length == 0 || password == null) {
        listErrors.push("Mật khẩu không được để trống")
    }
    else if (password.trim().length <8) {
        listErrors.push("Mật khẩu có độ dài tối thiểu là 8 ký tự")
    }
    else {
        let characters = password.split("");
        let countUpper =0 , countLower = 0, countNumber = 0, countSymbol = 0;
        for (var i = 0; i < characters.length; i++) {
            // check upper case
            if (characters[i].match(/[A-Z]+/gm)) {
                countUpper++;
            }
            // check lower case
            if (characters[i].match(/[a-z]+/gm)) {
                countLower++;
            }
            // check number case
            if (characters[i].match(/[0-9]+/gm)) {
                countNumber++;
            }
            // check symbol case
            if (characters[i].match(/[^A-Za-z0-9\s]+/gm)) {
                countSymbol++;
            }
        }

        // add array
        if (countUpper < 1) {
            listErrors.push("Mật khẩu phải gồm ít nhất <span class='text-danger'>1 chữ viết hoa</span>")
        }
        if (countLower < 1) {
            listErrors.push("Mật khẩu phải gồm ít nhất <span class='text-danger'>1 chữ viết thường</span>")
        }
        if (countNumber < 1) {
            listErrors.push("Mật khẩu phải gồm ít nhất <span class='text-danger'>1 chữ số</span>")
        }
        if (countSymbol < 1) {
            listErrors.push("Mật khẩu phải gồm ít nhất <span class='text-danger'>1 ký tự đặc biệt</span>")
        }
    }
    // so sánh re-password
    if (rePassword.trim().length == 0 || rePassword == null) {
        listErrors.push("Mật khẩu nhập lại không được để trống")
    } else {
        if (rePassword != password) {
            listErrors.push("Mật khẩu nhập lại không giống với mật khẩu")
        }
    }
    let fullName = $('#pxp-signup-page-fullname').val();
    if (fullName.trim().length == 0 || fullName == null) {
        listErrors.push("Họ và tên không được để trống")
    }
    else if (fullName.match(reFullName)) {
        listErrors.push("Họ và tên không được chứa ký tự đặc biệt và chữ số");
    }
    let phoneNumber = $('#pxp-signup-page-phone').val();
    if (phoneNumber.trim().length == 0 || phoneNumber == null) {
        listErrors.push("Số điện thoại không được để trống")
    }
    else if (!rePhoneNumber.test(phoneNumber)) {
        listErrors.push("Số điện thoại chỉ gồm các chữ số & phải đúng số điện thoại Việt Nam");
    }
    if ($('#pxp-signup-page-gender input[name="Gender"]:checked').val() == null) {
        listErrors.push("Giới tính không được để trống")
    }
    if ($('#pxp-signup-page-position').val() == null) {
        listErrors.push("Vị trí công tác không được để trống")
    }
    const checked = $('#pxp-signup-page-confirm-checkbox').is(':checked');
    if (!checked) {
        listErrors.push("Bạn chưa đồng ý với điều khoản")
    }

    if (listErrors.length > 0) {
        let html = '<ul>';
        $.each(listErrors, function (index, item) {
            html += '<li class="text-start">' + item + '</li>';
        })
        html += '</ul>';
        Swal.fire({
            title: 'Đăng ký thất bại',
            icon: 'warning',
            html: html,
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        }).then(
            $('#pxp-signup-button').css('pointer-events', 'unset')
        )
    }
    else {
        let obj = {
            email: $('#pxp-signup-page-email').val(),
            password: $('#pxp-signup-page-password').val(),
            confirmPassword: $('#pxp-signup-page-re-password').val(),
            fullname: $('#pxp-signup-page-fullname').val(),
            phone: $('#pxp-signup-page-phone').val(),
            gender: $('#pxp-signup-page-gender input[name="Gender"]:checked').val(),
            positionId: $('#pxp-signup-page-position').val(),
            skypeAccount: $('#pxp-signup-page-skype').val()
        }
        SignUp(obj);
    }
}


/**
 * sự kiện nút đăng ký
 */
$('form').on('click', '#pxp-signup-button', function () {
    $('#pxp-signup-button').css('pointer-events', 'none')
    validateInput();
})

/**
 * gọi api 
 */
function SignUp(obj) {
    $.ajax({
        url: systemUrl + 'api/employer/sign-up',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (res) {
            if (res.isSucceeded) {
                Swal.fire({
                    icon: 'success',
                    title: 'Đăng ký thành công',
                    html: 'Vui lòng kiểm tra email để kích hoạt tài khoản',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                }).then(() => {
                    window.location.href = 'dang-nhap';
                })
            }
            else if (!res.isSucceeded) {
                if (res.status == 400) {

                }
                Swal.fire({
                    icon: 'warning',
                    title: 'Đăng ký thất bại',
                    html: res.message != '' ? res.message : 'Vui lòng kiểm tra lại thông tin của bạn',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                })
                $('#pxp-signup-button').css('pointer-events', 'unset')
            }
        },
        error: function (e) {
            Swal.fire({
                icon: 'warning',
                title: 'Đăng ký thất bại',
                html: 'Có lỗi xảy ra, vui lòng thử lại sau',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
            $('#pxp-signup-button').css('pointer-events', 'unset')
        }
    })
}



/**
 * check length password -> show icon eye
 */
$('form').on('keyup keydown input', '#pxp-signup-page-password, #pxp-signup-page-re-password', function () {
    if ($(this).val().length > 0) {
        // show
        $(this).val($(this).val().replaceAll(' ', ''));
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').show();
    }
    else {
        // hide
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').hide();
    }
})


/**
* Show/ hide password
*/
var seeP = false;
var seeRP = false;
$('#pxp-signup-page-password ~ span.fa[password]').on('click', function () {
    if (!seeP) {
        $(this).toggleClass('fa-eye fa-eye-slash');
        // đổi type của input
        $(this).siblings('input').prop('type', 'text');

        seeP = true;
    }
    else {
        $(this).toggleClass('fa-eye-slash fa-eye');
        // đổi type của input
        $(this).siblings('input').prop('type', 'password');

        seeP = false;
    }
})
$('#pxp-signup-page-re-password ~ span.fa[password]').on('click', function () {
    if (!seeRP) {
        $(this).toggleClass('fa-eye fa-eye-slash');
        // đổi type của input
        $(this).siblings('input').prop('type', 'text');

        seeRP = true;
    }
    else {
        $(this).toggleClass('fa-eye-slash fa-eye');
        // đổi type của input
        $(this).siblings('input').prop('type', 'password');

        seeRP = false;
    }
})



/**
 * load data vị trí công tác
 */
function loadDataPosition() {
    $.ajax({
        url: systemUrl + 'api/position/list',
        type: "GET",
        contentType: "application/json",
        success: function (res) {
            var data = res.resources;
            $.each(data, function (index, item) {
                $("#pxp-signup-page-position").append(new Option(item.name, item.id));
            })
            $("#pxp-signup-page-position").select2({
                language: "vi",
                width: "100%",
                placeholder: {
                    id: '-1', // the value of the option
                    text: 'Chọn vị trí công tác'
                }
            })
        }
    })
    // set null
    $("#pxp-signup-page-position").val(null).trigger('change');
}