﻿"use strict";

const httpService = new HttpService()
const accessKey = "Authorization";

$("#loginForm").on("submit",function (e) {
    e.preventDefault();
    validationSignIn();
});
$(document).ready(function () {
    localStorage.removeItem("token");
})


function validationSignIn() {
    let isValidUserName = $("#username").val().match(/^[A-Za-z0-9!#$%&'*+/=?^_`{|}~\\,.@()<>[\]-]*$/);
    let isValidPassword = $("#password").val().match(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/);
    let errors = [];
    let swalSubTitle = "<p class='swal__admin__subtitle'>Đăng nhập không thành công!</p>";
    if ($("#username").val().length == 0) {
        errors.push("Tên đăng nhập không được để trống");
    }
    else if (!isValidUserName) {
        errors.push("Tên đăng nhập phải đúng định dạng");

    }
    if ($("#password").val().length == 0) {
        errors.push("Mật khẩu không được để trống");
    }
    else if (!isValidPassword) {
        errors.push("Mật khẩu có độ dài tối thiểu là 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và 1 ký tự đặc biệt")
    }
    if (errors.length > 0) {
        let contentError = "<ul>";
        errors.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        })
        contentError += "</ul>";
        Swal.fire({
            title: 'Đăng nhập' + swalSubTitle,
            html: contentError,
            icon: 'warning'
        })
    }
    else {
        signIn();

    }
}
async function signIn() {
    try {
        let data = {
            'userName': $("#username").val(),
            "password": $("#password").val()
        };
        let swalSubTitle = "<p class='swal__admin__subtitle'>Đăng nhập không thành công!</p>";
        let result = await httpService.postAsync("api/admin-account/sign-in", data);
        if (result.isSucceeded) {
            let token = result.resources.accessToken;
            localStorage.setItem("token", token);
            document.cookie = `${accessKey}=${token}`;
            Swal.fire("Đăng nhập thành công", "Chào mừng <b>" + data.userName + "</b> trở lại.", "success").then(function () {
                if (redirectURL.length == 0) {
                    location.href = "/";
                }
                else {
                    location.href = redirectURL;
                }
            });
        }
        else {
            if (result.errors.length > 1) {
                let contentError = "<ul>";
                result.errors.forEach(function (item, index) {
                    contentError += "<li class='text-start'>" + item + "</li>";
                })
                contentError += "</ul>";
                Swal.fire(
                    'Đăng nhập' + swalSubTitle,
                    contentError,
                    'warning'
                );
            }
            else {
                Swal.fire(
                    "Đăng nhập",
                    result.errors[0],
                    "error");
            }
        }
    } catch (e) {
        Swal.fire(
            "Đăng nhập",
            "Đã có lỗi xảy ra xin vui lòng thử lại sau!",
            "error");
        console.error(e);
    }
}
$("#btnLogin").on("click", function (e) {
    e.preventDefault();
    validationSignIn()
})

$("#loginForm").on("input change keypress keydown", "input", function (e) {
    let text = $(this).val().trim();
    $(this).val(text);
    if (e.which == 13) {
        e.preventDefault();
        validationSignIn()
    }
})
$(".none-space").on("change input blur", function () {
    let e = $(this);
    let text = e.val().trim();
    e.val(text);
})
$(".btn_show_pass").on("click", function (e) {
    var target = $($(this).attr("data-target"));
    if (target.attr("type") == "password") {
        target.attr("type", "text");
        $(this).html(`<i class="ki-duotone ki-eye-slash fs-3">
                                            <span class="path1 ki-uniEC07"></span>
                                            <span class="path2 ki-uniEC08"></span>
                                            <span class="path3 ki-uniEC09"></span>
                                            <span class="path4 ki-uniEC0A"></span>
                                        </i>`);
    }
    else {
        target.attr("type", "password");
        $(this).html(`<i class="ki-duotone ki-eye fs-3">
                                            <span class="path1 ki-uniEC0B"></span>
                                            <span class="path2 ki-uniEC0B"></span>
                                            <span class="path3 ki-uniEC0D"></span>
                                        </i>`);
    }
});