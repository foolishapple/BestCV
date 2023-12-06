$("#btnUpdatePassword").on('click', function (e) {
    ValidatePassword();
})

$("#form_pass input").on('keypress', function (e) {
    if (e.which === 13) {
        e.preventDefault();
        $("#btnUpdatePassword").click();
    }
})

function ValidatePassword() {
    var OldPassword = $("#old-password").val();
    var NewPassword = $("#new-password").val();
    var ConfirmNewPassword = $("#new-password-repeat").val();
    var regexUpperCase = /[A-Z]/;
    var regexSpecialChar = "/[!@#\$%^&*.]/";
    var listError = [];

    if (NewPassword != ConfirmNewPassword) {
        listError.push(
            'Mật khẩu mới không trùng với mật khẩu xác nhận.'
        )
    }
    if (OldPassword.length == 0) {
        listError.push(
            'Mật khẩu hiện tại không được để trống.'
        )
    }
    if (NewPassword.length == 0) {
        listError.push(
            'Mật khẩu mới không được để trống.'
        )

    }
    else if (NewPassword.length < 8) {
        listError.push(
            'Mật khẩu mới phải đạt 8 ký tự trở lên.'
        )
    }
    else if (!regexUpperCase.test(NewPassword) || !regexSpecialChar.test(NewPassword)) {
        listError.push(
            'Mật khẩu mới phải có ít nhất một chữ viết hoa và ít nhất một ký tự đặc biệt.'
        );
    }
    if (ConfirmNewPassword.length == 0) {
        listError.push(
            'Xác nhận mật khẩu không được để trống.'
        )
    }

    if (listError.length > 0) {
        var html = '<ul class="list-error text-start">';
        $(listError).each(function (index, item) {
            html += '<li>' + item + '</li>'
        })
        html += '</ul>';
        Swal.fire({
            icon: 'warning',
            title: 'Mật khẩu<p class="swal__admin__subtitle">Cập nhật mật khẩu thất bại ',
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
    var changePass = {
        oldPassword: $("#old-password").val(),
        newPassword: $("#new-password").val(),
        confirmPassword: $("#new-password-repeat").val()
    }

    $.ajax({
        url: systemConfig.defaultAPIURL + "api/Candidate/update-password-candidate",
        data: JSON.stringify(changePass),
        type: "PUT",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.currentUser) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
            }
        },
        success: function (res) {
            if (res.isSucceeded) {
                Swal.fire({
                    title: 'Tài khoản <p class="swal__admin__subtitle"> Cập nhật mật khẩu',
                    icon: 'success',
                    html: '<p class="swal__admin__subtitle">Cập nhật mật khẩu thành công!</p>',
                    showConfirmButton: true,

                }).then((result) => {
                    $("#old-password").val("");
                    $("#new-password").val("");
                    $("#new-password-repeat").val("");
                })
            }
            else {
                if (res.status == 400) {
                    if (res.errors != null) {
                        var contentError = "<ul>";
                        res.errors.forEach(function (item, index) {
                            contentError += "<li class='text-start pb-2'>" + item + "</li>";
                        })
                        contentError += "</ul>";
                        Swal.fire(
                            'Mật khẩu <p class="swal__admin__subtitle"> Cập nhật không thành công </p>',
                            contentError,
                            'warning'
                        );
                    } else {
                        Swal.fire(
                            'Lưu ý',
                            res.message,
                            'warning'
                        )
                    }
                }
                else {
                    Swal.fire(
                        'Lưu ý',
                        res.message,
                        'warning'
                    )
                }
            }
        },

        error: function (error) {
            if (error.status === 401) {
                Swal.fire({
                    title: 'Cập nhật mật khẩu',
                    html: 'Bạn không được phép truy cập vào trang này.' + '<br>' + 'Vui lòng đăng nhập lại !',
                    icon: 'error'
                });
            } else {
                Swal.fire({
                    title: 'Cập nhật mật khẩu',
                    html: 'Cập nhật mật khẩu không thành công,' + '<br>' + 'vui lòng thử lại sau !',
                    icon: 'error'
                });
            }
        },

    })
}
$(".show-hide-password").click(function () {
    let targetInput = $(this).prev('input');
    let currentType = targetInput.attr('type');
    if (currentType === "text") {
        targetInput.attr('type', 'password');
        $(this).removeClass("fa-eye-slash ").addClass("fa-eye")
    } else {
        targetInput.attr('type', 'text');
        $(this).removeClass("fa-eye").addClass("fa-eye-slash")
    }
})