//$("#btnSignUp").on('click', function (e) {
//    e.preventDefault();
//    ValidatePassword();

//});
$('form').on('click', '#btnSignUp', function () {
    /*$('#btnSignUp').css('pointer-events', 'none')*/
    ValidatePassword();
})
function ValidatePassword() {
    var account = JSON.parse(localStorage.getItem("currentLoggedInUser"));
    var fullName = $("#pxp-signup-page-fullname").val();
    var email = $("#pxp-signup-page-email").val();
    const checked = $('#pxp-signup-page-confirmCheckbox').is(':checked');
    var password = $("#pxp-signup-page-password").val();
    var regexUpperCase = /[A-Z]/;
    var regexSpecialChar = "/[!@#\$%^&*]/";
    var confirmPassword = $("#pxp-signup-page-confirmpassword").val();
    var phone = $("#pxp-signup-page-phone").val();
    var regexPhone = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    var regexEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var listError = [];

    if (fullName.trim().length == 0) {
        listError.push(
            'Họ và tên không được để trống.'
        )
    }

    if (fullName.length > 255) {
        listError.push(
            'Họ và tên không được quá 255 ký tự.'
        )
    }

    // Check Email
    if (email.length == 0) {
        listError.push(
            'Email không được để trống.'
        )
    }

    if (regexEmail.test(email) == false) {
        listError.push(
            'Email không hợp lệ.'
        )
    }

    if (email.length > 255) {
        listError.push(
            'Email không được quá 255 ký tự.'
        )
    }

    //Check phone
    if (phone.length == 0) {
        listError.push(
            'Số điện thoại không được để trống.'
        )
    } else if (phone.length < 10) {
        listError.push(
            'Số điện thoại phải đủ 10 số.'
        )
    } else if (regexPhone.test(phone) == false) {
        listError.push(
            'Số điện thoại của bạn không đúng định dạng'
        )
    }

    if (phone.length > 10) {
        listError.push(
            'Số điện thoại không được quá 10 số.'
        )
    }

    if (password.trim().length == 0 || password == null) {
        listError.push("Mật khẩu không được để trống")
    } else if (password.trim().length < 8 || password.trim().length > 255) {
        listError.push("Mật khẩu nhập phải bắt đầu từ 8 đến 255 kí tự.")
    }
    else {
        let characters = password.split("");
        let countUpper = 0, countLower = 0, countNumber = 0, countSymbol = 0;
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
            listError.push("Mật khẩu phải gồm ít nhất 1 chữ viết hoa")
        }
        if (countLower < 1) {
            listError.push("Mật khẩu phải gồm ít nhất 1 chữ viết thường")
        }
        if (countNumber < 1) {
            listError.push("Mật khẩu phải gồm ít nhất 1 chữ số")
        }
        if (countSymbol < 1) {
            listError.push("Mật khẩu phải gồm ít nhất 1 ký tự đặc biệt")
        }

    }

    // so sánh re-password
    if (confirmPassword.trim().length == 0 || confirmPassword == null) {
        listError.push("Mật khẩu nhập lại không được để trống")
    } else if (confirmPassword.trim().length < 8 || confirmPassword.trim().length > 255) {
        listError.push("Mật khẩu nhập lại phải bắt đầu từ 8 đến 255 kí tự.")
    } else {
        if (confirmPassword != password) {
            listError.push("Mật khẩu nhập lại không giống với mật khẩu")
        }
    }

    //Checked :
    if (!checked) {
        listError.push("Bạn chưa đồng ý với điều khoản.")
    }

    if (listError.length > 0) {
        var html = '<ul class="list-error text-start fs-6">';
        $(listError).each(function (index, item) {
            html += '<li >' + item + '</li>'
        })
        html += '</ul>';
        Swal.fire({
            icon: 'warning',
            title: 'Đăng ký tài khoản<p class="swal__admin__subtitle">Đăng kí tài khoản thất bại.',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
    else {
        submit();
    }
}



function submit() {
    var signUp = {
        fullName: $("#pxp-signup-page-fullname").val(),
        email: $("#pxp-signup-page-email").val(),
        phone: $("#pxp-signup-page-phone").val(),
        password: $("#pxp-signup-page-password").val(),
        confirmPassword: $("#pxp-signup-page-confirmpassword").val(),
    }

    $.ajax({
        url: systemConfig.defaultAPIURL + "api/candidate/sign-up",
        data: JSON.stringify(signUp),
        type: "Post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.currentLoggedInUserELearning) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentLoggedInUserELearning).token);
            }
        },
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
                Swal.fire({
                    icon: 'warning',
                    title: 'Đăng ký thất bại',
                    html: res.message != '' ? res.message : 'Vui lòng kiểm tra lại thông tin của bạn',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                })
            }
        },
        error: function (error) {
            Swal.fire(
                {
                    title: 'Đăng ký tài khoản',
                    html: 'Đăng ký tài khoản không thành công,' + '<br>' + 'vui lòng thử lại sau',
                    icon: 'error'
                }
            );
        },

    })
}

/**
* check length password -> show icon eye
*/
$('form').on('keyup', '#pxp-signup-page-password, #pxp-signup-page-password', function () {
    if ($(this).val().length > 0) {
        // show
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').show();
    }
    else {
        // hide
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').hide();
    }
})

$('form').on('keyup', '#pxp-signup-page-confirmpassword, #pxp-signup-page-confirmpassword', function () {
    if ($(this).val().length > 0) {
        // show
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').show();
    }
    else {
        // hide
        $(this).siblings('.fa.fa-eye, .fa.fa-eye-slash').hide();
    }
})


//Show and hiden pass word
var seeP = false;
var seeRP = false;
$('#pxp-signup-page-password ~ span.fa-eye').on('click', function () {
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
$('#pxp-signup-page-confirmpassword ~ span.fa-eye').on('click', function () {
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