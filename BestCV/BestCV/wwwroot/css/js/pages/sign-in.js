/**
 * check đã đăng nhập hay chưa
 */
function checkSignedInUser() {
    if (localStorage.currentUser) {
        $('.pxp-user-nav a, .pxp-user-nav .inner').remove('d-none');
        return true;
    } else {
        $('.pxp-user-nav a, .pxp-user-nav .inner').addClass('d-none');
        return false;
    }
}

$("#form-signIn input").keyup(function (e) {
    e.preventDefault();
    if (e.keyCode === 13) {
        validate();
    }
});
/**
 * nút login
 */
$('#form-signIn').on('click', '#submit-signin', function () {
    validate();
})
/**
 * validate form
 */
function validate() {
    var listError = [];

    if ($('#signin-email').val().trim() == '' || $('#signin-email').val() == null) {
        listError.push('Email hoặc số điện thoại không được để trống.')
    }
    //else {
    //    var emailValid = IsValidEmail($('#signin-email').val().trim());
    //    if (!emailValid) {
    //        listError.push('Email không hợp lệ.')
    //    }
    //}
    if ($('#signin-password').val().trim() == '' || $('#signin-password').val() == null) {
        listError.push('Mật khẩu không được để trống.')
    }

    if (listError.length > 0) {
        var html = '<ul class="list-error text-start fs-6">';
        $(listError).each(function (index, item) {
            html += '<li>' + item + '</li>'
        })
        html += '</ul>';
        Swal.fire({
            icon: 'warning',
            title: 'Đăng nhập thất bại!',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
    else {
        var obj = {
            email: $('#signin-email').val(),
            //email: $('#signin-email').val(),
            password: $('#signin-password').val(),
            phone: "",
        }
        signIn(obj);
    }
}
/**
 * hàm sign in
 */
function signIn() {
    let obj = {
        emailorPhone: $("#signin-email").val().trim(),
        password: $("#signin-password").val()
    }
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/candidate/sign-in",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function (response) {
            //debugger;
            if (!response.isSucceeded) {
                Swal.fire({
                    icon: 'error',
                    title: 'Đăng nhập thất bại',
                    html: formatError(response.errors)
                });
            }
            // Nếu đăng nhập thành công
            else if (response.isSucceeded) {
                localStorage.setItem("currentUser", JSON.stringify(response.resources));
                localStorage.setItem("token", response.resources.token);
                Swal.fire({
                    icon: 'success',
                    title: 'Đăng nhập thành công',
                    html: `Chào mừng <b>${response.resources.fullname ? response.resources.fullname : ""}</b> quay trở lại.`
                }).then(() => {
                    /*window.location.href = window.location.origin;*/
                    window.location.href = '/';
                });
            }
        },
        error: function (e) {
            Swal.fire({
                icon: 'error',
                title: 'Đăng nhập thất bại',
                html: 'Có lỗi xảy ra trong quá trình đăng nhập.'
            });
        }
    })
}

function formatError(errors) {
    if (errors.length == 0) return "";
    else if (errors.length == 1) return errors[0];
    else {
        resultStr = "<ul>";
        for (let error of errors) {
            resultStr += `<li>${error}</li>`
        }
        resultStr += "</ul>";
        return resultStr;
    }
}

$('form').on('keyup', '#signin-password', function () {
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
$('#signin-password ~ span.fa-eye').on('click', function () {
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




