$("#btn-forgot-password").click(function (e) {
    e.preventDefault();
    $("#pxp-forgot-password-modal").modal("show");
})
$(document).ready(function () {
    //var checkstep = '@ViewBag.step';
    //if (checkstep == 0) {
    //    $("#btn-submit").click(function () {
    //        ForgotPassword();
    //    })
    //}
    $("#btn-submit").click(function () {
        ForgotPassword();
    })

});

function ForgotPassword() {
    var email = $("#Email").val();
    var obj = {
        "Email": email,
    }
    var regex = /^([\w-\.]+\u0040([\w-]+\.)+[\w-]{2,4})?$/;
    if (email.length < 1) {
        Swal.fire({
            icon: 'warning',
            title: 'Gửi Email thất bại.',
            text: 'Bạn chưa điền Email.'
        });
    }
    else if (!regex.test(email)) {
        Swal.fire({
            icon: 'warning',
            title: 'Gửi Email thất bại.',
            text: 'Email không hợp lệ.'
        });
    }
    else {
        $("#loading").addClass("show")
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: DefaultAPIURL + "api/employer/forgot-password",
            data: JSON.stringify(obj),
            success: function (res) {
                $("#loading").removeClass("show")
                if (res.isSucceeded) {

                    Swal.fire({
                        icon: 'success',
                        title: 'Gửi email thành công',
                        html: 'Vui lòng kiểm tra email để cập nhật mật khẩu tài khoản',
                        showCloseButton: false,
                        focusConfirm: true,
                        confirmButtonText: 'Ok',
                    }).then(() => {
                        window.location.href = 'dang-nhap';
                    })
                } else if (!res.isSucceeded) {
                    if (res.status == 400) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Gửi email thất bại',
                            html: res.errors != '' ? res.errors : 'Vui lòng kiểm tra lại thông tin của bạn',
                            showCloseButton: false,
                            focusConfirm: true,
                            confirmButtonText: 'Ok',
                        })
                    } else if (res.status == 420) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Gửi email thất bại',
                            html: res.errors != '' ? res.errors : 'Vui lòng kiểm tra lại thông tin của bạn',
                            showCloseButton: false,
                            focusConfirm: true,
                            confirmButtonText: 'Ok',
                        })
                    }
                }
                  
            },
            error: function (e) {
                $("#loading").removeClass("show")
                Swal.fire({
                    icon: 'warning',
                    title: 'Gửi email thất bại',
                    html: 'Có lỗi xảy ra, vui lòng thử lại sau',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                })

            },


        });
         }
}

$('#Email').keyup(function (e) {
    if (e.which == 13) {
        ForgotPassword();
    }
})
