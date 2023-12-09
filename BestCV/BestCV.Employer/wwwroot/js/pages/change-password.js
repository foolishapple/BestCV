
function submit() {
    var changePass = {
        oldPassword: $("#old-password").val(),
        newPassword: $("#new-password").val(),
        newPasswordRepeat: $("#new-password-repeat").val()
    }
    $("#loading").addClass("show");
    $.ajax({
        url: DefaultAPIURL + "api/employer/change-password",
        data: JSON.stringify(changePass),
        type: "PUT",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
            }
        },
        success: function (res) {
            $("#loading").removeClass("show");
            if (res.isSucceeded) {
                Swal.fire({
                    title: 'Cập nhật mật khẩu',
                    icon: 'success',
                    html: 'Cập nhật mật khẩu thành công',
                    showConfirmButton: true,

                }).then((result) => {
                    $("#old-password").val("");
                    $("#new-password").val("");
                    $("#new-password-repeat").val("");
                    $("#changePassword input").attr("password");
                    $(".show-hide-password").removeClass("fa-eye-slash ").addClass("fa-eye")

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
                            'Cập nhật mật khẩu <p class="swal-subtitle"> Cập nhật không thành công </p>',
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
            $("#loading").removeClass("show");
            if (error.status === 401) {
                Swal.fire({
                    title: 'Cập nhật mật khẩu',
                    html: 'Phiên đang nhập của bạn đã hết hạn.' + '<br>' + 'Vui lòng đăng nhập lại ',
                    icon: 'error'
                }).then(() => {
                    window.location.href = window.origin + "/dang-nhap";
                });
            } else {
                Swal.fire({
                    title: 'Cập nhật mật khẩu',
                    html: 'Cập nhật mật khẩu không thành công,' + '<br>' + 'vui lòng thử lại sau ',
                    icon: 'error'
                });
            }
        },

    })
}
function ValidatePassword() {
    var OldPassword = $("#old-password").val();
    var NewPassword = $("#new-password").val();
    var ConfirmNewPassword = $("#new-password-repeat").val();
    var regexUpperCase = /[A-Z]/;
    var regexSpecialChar = /[!@#\$%^&*.]/;
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
    if (NewPassword.trim().length == 0 || NewPassword == null) {
        listError.push("Mật khẩu không được để trống")
    } else {
        let characters = NewPassword.split("");
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
            title: 'Mật khẩu<p class="swal-subtitle">Cập nhật mật khẩu thất bại ',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
    else {
        submit();
    }
}
$(document).ready(function () {
    $("#btnUpdatePassword").on('click', function (e) {
        ValidatePassword();
    });
    $("#changePassword input").on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            $("#btnUpdatePassword").click();
        }
    });
    $(".show-hide-password").click(function () {
        debugger
        let targetInput = $(this).prev('input');
        let currentType = targetInput.attr('type');
        if (currentType === "text") {
            targetInput.attr('type', 'password');
            $(this).removeClass("fa-eye-slash ").addClass("fa-eye")
        }
        else {
            targetInput.attr('type', 'text');
            $(this).removeClass("fa-eye").addClass("fa-eye-slash")
        }
    });
    $("#changePassword").on("submit", function (e) {
        e.preventDefault();
    })

});