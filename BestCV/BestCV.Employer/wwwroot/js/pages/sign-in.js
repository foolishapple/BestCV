    $(document).ready(function () {
    // Click nút đăng nhập
    $("#btnSignIn").click(function () {
        validateData();
    });

    // Gõ enter khi đang ở trường Email hoặc số điện thoại
    $("#pxp-signin-page-email").keypress(function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            $("#btnSignIn").click();
        }
    });

    // Gõ enter khi đang ở trường password
    $("#pxp-signin-page-password").keypress(function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            $("#btnSignIn").click();
        }
    });

    // Thay đổi trường password
    $("#pxp-signin-page-password").keyup(function () {
        if ($(this).val().length != 0) {
            if ($(this).attr("type") == "password") {
                $("span.fa-eye").removeClass("d-none");
            } else if ($(this).attr("type") == "text") {
                $("span.fa-eye-slash").removeClass("d-none");
            }
        } else {
            $("span.fa-eye").addClass("d-none");
            $("span.fa-eye-slash").addClass("d-none");
        }
    });

    // Click nút hiện mật khẩu
    $("span.fa-eye").click(function () {
        $("#pxp-signin-page-password").attr("type", "text");
        $("span.fa-eye").addClass("d-none");
        $("span.fa-eye-slash").removeClass("d-none");
    });

    // Click nút ẩn mật khẩu
    $("span.fa-eye-slash").click(function () {
        $("#pxp-signin-page-password").attr("type", "password");
        $("span.fa-eye").removeClass("d-none");
        $("span.fa-eye-slash").addClass("d-none");
    });
});

// Hàm validate dữ liệu nhập
function validateData() {
    let errors = [];
    if ($("#pxp-signin-page-email").val().trim().length == 0) {
        errors.push("Email hoặc số điện thoại không được để trống.");
    }
    if ($("#pxp-signin-page-password").val().length == 0) {
        errors.push("Mật khẩu không được để trống.");
    }
    if (errors.length != 0) {
        Swal.fire({
            icon: 'error',
            title: 'Đăng nhập thất bại',
            html: formatError(errors)
        });
    } else {
        signIn();
    }
}

// Hàm đăng nhập
function signIn() {
    let obj = {
        emailOrPhone: $("#pxp-signin-page-email").val().trim(),
        passWord: $("#pxp-signin-page-password").val()
    };
    $.ajax({
        url: DefaultAPIURL + `api/employer/sign-in`,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (response) {
            // Nếu có lỗi gì đó trong quá trình đăng nhập
            if (response.status === 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Đăng nhập thất bại',
                    html: formatError(response.errors)
                });
            }
            // Nếu đăng nhập thành công
            else if (response.status === 200) {
                localStorage.setItem("currentLoggedInUserJobi", JSON.stringify(response.resources));
                localStorage.setItem("token", response.resources.token);
                Swal.fire({
                    icon: 'success',
                    title: 'Đăng nhập thành công',
                    html: `Chào mừng <b>${response.resources.fullname ? response.resources.fullname : ""}</b> quay trở lại.`
                }).then(() => {
                    window.location.href = window.location.origin;
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

// Hàm format lỗi khi đăng nhập
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