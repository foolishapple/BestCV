
"use strict";
$(document).ready(function () {
    $("#adminAccountPhoto").on("change", async function (e) {
        let obj = currentUser;
        let filePath = $(this).attr("file-path");
        if (filePath != undefined && filePath.trim().length > 0) {
            filePath = filePath.replace(systemConfig.defaultStorageURL, "");
            obj.photo = filePath;
            $("#loading").addClass("show");
            try {
                let result = await httpService.putAsync("api/admin-account/update", currentUser);
                $("#loading").removeClass("show");
                if (result.isSucceeded) {
                    getInfo();
                }
                else {
                    Swal("Cập nhật ảnh", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                }
            } catch (e) {
                $("#loading").removeClass("show");
                console.error(e);
            }
        }
    });
    $("#btn_edit_profile").on("click", async function () {
        await getInfo();
        $("#adminAccountEmail").val(currentUser.email).trigger("change");
        $("#adminAccountFullName").val(currentUser.fullName).trigger("change");
        $("#adminAccountUserName").val(currentUser.userName).trigger("change");
        $("#adminAccountPhone").val(currentUser.phone).trigger("change");
        $("#adminAccountDescription").val(currentUser.description).trigger("change");
        $("#modal_admin_account .modal-title").text('Cập nhật thông tin cá nhân');
        $("#modal_admin_account").modal("show");
    })
    $("#btn_update_password").on("click", function () {
        let isValidOldPassword = $("#oldPass").val().match(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/);
        let isValidNewPassword = $("#newPass").val().match(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/);

        let errors = [];
        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật mật khẩu không thành công!</p>";
        if ($("#oldPass").val().length == 0) {
            errors.push("Mật khẩu cũ không được để trống");
        }
        else if (!isValidOldPassword) {
            errors.push("Mật khẩu có độ dài tối thiểu là 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và 1 ký tự đặc biệt")
        }

        if ($("#newPass").val().length == 0) {
            errors.push("Mật khẩu mới không được để trống");
        }
        else if (!isValidNewPassword) {
            errors.push("Mật khẩu có độ dài tối thiểu là 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và 1 ký tự đặc biệt")
        }

        if ($("#confirmNewPass").val().trim() != $("#newPass").val().trim()) {
            errors.push("Nhập lại mật khẩu không được khác mật khẩu mới");
        }


        if (errors.length > 0) {
            let contentError = "<ul>";
            errors.forEach(function (item, index) {
                contentError += "<li class='text-start'>" + item + "</li>";
            })
            contentError += "</ul>";
            Swal.fire({
                title: 'Thông tin cá nhân' + swalSubTitle,
                html: contentError,
                icon: 'warning'
            })
        }
        else {
            changePassword();
        }
    })

    $("#saveData").on("click", function () {
        let isValidEmail = $("#adminAccountEmail").val().match(/^([\w-\.]+\u0040([\w-]+\.)+[\w-]{2,4})?$/);
        let isValidPhone = $("#adminAccountPhone").val().match(/^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$/)
        let errors = [];
        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật thông tin không thành công!</p>";
        if ($("#adminAccountFullName").val().length == 0) {
            errors.push("Họ & tên không được để trống");
        }
        if ($("#adminAccountEmail").val().length == 0) {
            errors.push("Email không được để trống");

        }
        else if (!isValidEmail) {
            errors.push("Email phải đúng định dạng");
        }
        if ($("#adminAccountPhone").val().length == 0) {
            errors.push("Số điện thoại không được để trống");
        }
        else if (!isValidPhone) {
            errors.push("Số điện thoại phải đúng định dạng")
        }
        if (errors.length > 0) {
            let contentError = "<ul>";
            errors.forEach(function (item, index) {
                contentError += "<li class='text-start'>" + item + "</li>";
            })
            contentError += "</ul>";
            Swal.fire({
                title: 'Thông tin cá nhân' + swalSubTitle,
                html: contentError,
                icon: 'warning'
            })
        }
        else {
            submitData();
        }
    })


    function submitData() {
        let obj = currentUser;
        obj.email = $("#adminAccountEmail").val();
        obj.fullName = $("#adminAccountFullName").val();
        obj.phone = $("#adminAccountPhone").val();
        obj.description = $("#adminAccountDescription").val().escape();
        swal.fire({
            title: "Thông tin cá nhân",
            html: "Bạn có chắc chắn muốn cập nhật thông tin cá nhân?",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass("show");
                try {
                    let result = await httpService.putAsync("api/admin-account/update", obj);
                    $("#loading").removeClass("show");
                    if (result.isSucceeded) {
                        Swal.fire(
                            'Thông tin cá nhân',
                            'Thông tin cá nhân của bạn đã được cập nhật thành công.',
                            'success'
                        ).then((result) => {
                            $("#modal_admin_account").modal("hide");
                            getInfo();
                        });
                    }
                    else {
                        if (result.status == 400) {
                            let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật thông tin không thành công</p>";
                            if (result.errors != null) {
                                let contentError = "<ul>";
                                result.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                Swal.fire(
                                    'Thông tin cá nhân' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );

                            } else {
                                let contentError = `<ul>
                                                <li class='text-start'>`+ result.message + `</li>
                                </ul>`;
                                Swal.fire(
                                    'Thông tin cá nhân' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }
                        else {
                            Swal.fire(
                                'Thông tin cá nhân',
                                `Cập nhât thông tin cá nhân không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                    }
                    $("#loading").removeClass("show");
                }
                catch (e) {
                    console.error(e);
                    if (e.status === 401) {
                        Swal.fire(
                            'Thông tin cá nhân',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Thông tin cá nhân',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        swal.fire(
                            'Thông tin cá nhân',
                            `${actionName} tài khoản không thành công, <br> vui lòng thử lại sau!`,
                            'error'
                        );
                    }
                    $("#loading").removeClass("show");
                }
            }
        });
    }

    function changePassword() {
        let obj = {
            "oldPassword": $("#oldPass").val(),
            "newPassword": $("#newPass").val(),
            "reNewPassword": $("#confirmNewPass").val()
        }
        swal.fire({
            title: "Đổi mật khẩu",
            html: "Bạn có chắc chắn muốn cập nhật mật khẩu mới?",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#443',
            cancelButtonText: 'Hủy',
            confirmButtonText: 'Lưu'
        }).then(async (result) => {
            if (result.isConfirmed) {
                $("#loading").addClass("show");
                try {
                    let result = await httpService.postAsync("api/admin-account/update-password", obj);
                    $("#loading").removeClass("show");
                    if (result.isSucceeded) {
                        Swal.fire(
                            'Đổi mật khẩu',
                            'Mật khẩu của bạn đã được cập nhật thành công.',
                            'success'
                        ).then((result) => {
                            window.location.reload();
                        });
                    }
                    else {
                        if (result.status == 400) {
                            let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật mật khẩu không thành công</p>";
                            if (result.errors != null) {
                                let contentError = "<ul>";
                                result.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                Swal.fire(
                                    'Đổi mật khẩu' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );

                            } else {
                                let contentError = `<ul>
                                                <li class='text-start'>`+ result.message + `</li>
                                </ul>`;
                                Swal.fire(
                                    'Đổi mật khẩu' + swalSubTitle,
                                    contentError,
                                    'warning'
                                );
                            }
                        }
                        else {
                            Swal.fire(
                                'Đổi mật khẩu',
                                `Cập nhât mật khẩu không thành công, <br> vui lòng thử lại sau!`,
                                'error'
                            );
                        }
                    }
                    $("#loading").removeClass("show");
                }
                catch (e) {
                    console.error(e);
                    if (e.status === 401) {
                        Swal.fire(
                            'Đổi mật khẩu',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Đổi mật khẩu',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        swal.fire(
                            'Đổi mật khẩu',
                            `Cập nhật mật khẩu không thành công, <br> vui lòng thử lại sau!`,
                            'error'
                        );
                    }
                    $("#loading").removeClass("show");
                }
            }
        });
    }
})


$("#adminAccountPhone").on("keypress keyup change", function (e) {
    $(this).attr('type', 'text');
    if (e.which >= 37 && e.which <= 40) return;
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, '')
            .replace(/\B(?=(\d{3})+(?!\d))/g, '');
    });
})
