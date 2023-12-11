function ValidatePassword() {
    var Password = $("#password").val();
    var ConfirmPassword = $("#confrim-password").val();
    var listErrors = [];
    if (Password.trim().length == 0 || Password == null) {
        listErrors.push("Mật khẩu không được để trống")
    }
    else if (Password.length < 8) {
        listErrors.push("Mật khẩu phải nhiều hơn 8 ký tự")
    }
    else
    {
        let characters = Password.split("");
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
            listErrors.push("Mật khẩu mới phải gồm ít nhất 1 chữ viết hoa")
        }
        if (countLower < 1) {
            listErrors.push("Mật khẩu mới phải gồm ít nhất 1 chữ viết thường")
        }
        if (countNumber < 1) {
            listErrors.push("Mật khẩu mới phải gồm ít nhất 1 chữ số")
        }
        if (countSymbol < 1) {
            listErrors.push("Mật khẩu mới phải gồm ít nhất 1 ký tự đặc biệt")
        }
    }
    if (ConfirmPassword.trim().length == 0 || ConfirmPassword == null) {
        listErrors.push("Mật khẩu xác nhận không được để trống")
    } else {
        if (ConfirmPassword != Password) {
            listErrors.push("Mật khẩu mới không trùng với mật khẩu xác nhận")
        }
    }

    if (listErrors.length > 0) {
        var html = '<ul class="list-error text-start">';
        $(listErrors).each(function (index, item) {
            html += '<li>' + item + '</li>'
        })
        html += '</ul>';
        Swal.fire({
            icon: 'warning',
            title: 'Mật khẩu<p class="swal-subtitle">Đặt lại mật khẩu thất bại</p> ',
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
     var obj = {
         code: code,
         hash: hash,
         password: $("#password").val(),
         confirmPassword: $("#confrim-password").val(),
     }        
     $("#loading").addClass("show")
     $.ajax({
         url: DefaultAPIURL + 'api/candidate/reset-password',
         type: "POST",
         contentType: "application/json",
         data: JSON.stringify(obj),
         success: function (result) {
             $("#loading").removeClass("show")
             if (result.isSucceeded) {
                 Swal.fire({
                     title: 'Đặt lại mật khẩu',
                     icon: 'success',
                     html: 'Đặt lại mật khẩu thành công',
                     showConfirmButton: true,
                 }).then(() => {
                     window.location.href = window.location.origin + '/dang-nhap';
                 })
             }
             else {
                 if (result.status == 404) {
                     Swal.fire({
                         icon: 'warning',
                         title: 'Đặt lại mật khẩu thất bại',
                         html: result.message != '' ? result.message : 'Vui lòng kiểm tra lại thông tin của bạn',
                         showCloseButton: false,
                         focusConfirm: true,
                         confirmButtonText: 'Ok',
                     })
                 }
                 else {
                     Swal.fire({
                         title: 'Đặt lại mật khẩu',
                         html: 'Đặt lại mật khẩu không thành công,' + '<br>' + 'vui lòng thử lại sau ',
                         icon: 'error'
                     });
                 }

             }
         },
         error: function (e) {
             $("#loading").removeClass("show")
             Swal.fire({
                 title: 'Đặt lại mật khẩu',
                 html: 'Đặt lại mật khẩu không thành công,' + '<br>' + 'vui lòng thử lại sau ',
                 icon: 'error'
             });
         },
     })
 
}
$(document).ready(function () {
    $("#btnResetPassword").on('click', function (e) {
        ValidatePassword();
    });
    $("#resetPassword input").on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            $("#btnUpdatePassword").click();
        }
    });
    $(".show-hide-password").click(function () {
        let targetInput = $(this).siblings('input');
        let currentType = targetInput.attr('type');

        if (currentType === "text") {
            targetInput.attr('type', 'password');
            $(this).removeClass("fa-eye-slash ").addClass("fa-eye")
        } else {
            targetInput.attr('type', 'text');
            $(this).removeClass("fa-eye").addClass("fa-eye-slash")
        }
    });
    $("#resetPassword").on("submit", function (e) {
        e.preventDefault();
    })
})
