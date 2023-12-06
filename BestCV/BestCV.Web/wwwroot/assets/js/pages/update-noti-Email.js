$("#btnUpdatePassword").on('click', function (e) {
    submitNoti();
})

function submitNoti() {
    var selectedValues = {
        IsSubcribeEmailImportantSystemUpdate: $("#check_ImportantSystemUpdate").is(":checked"),
        IsSubcribeEmailEmployerViewCV: $("#check_EmployerViewCV").is(":checked"),
        IsSubcribeEmailNewFeatureUpdate: $("#check_NewFeatureUpdate").is(":checked"),
        IsSubcribeEmailOtherSystemNotification: $("#check_OtherSystemNotification").is(":checked"),
        IsSubcribeEmailJobSuggestion: $("#check_JobSuggestion").is(":checked"),
        IsSubcribeEmailEmployerInviteJob: $("#check_EmployerInviteJob").is(":checked"),
        IsSubcribeEmailServiceIntro: $("#check_ServiceIntro").is(":checked"),
        IsSubcribeEmailProgramEventIntro: $("#check_ProgramEventIntro").is(":checked"),
        IsSubcribeEmailGiftCoupon: $("#check_GiftCoupon").is(":checked"),
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/Candidate/update-noti-email",
        data: JSON.stringify(selectedValues),
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
                    title: 'Tài khoản <p class="swal__admin__subtitle"> Cập nhật thông báo email ',
                    icon: 'success',
                    html: '<p class="swal__admin__subtitle">Cập nhật thông báo email thành công!</p>',
                    showConfirmButton: true,

                });
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
                            'Thông báo Email <p class="swal__admin__subtitle"> Cập nhật không thành công </p>',
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
                    title: 'Cập nhật thông báo Email',
                    html: 'Bạn không được phép truy cập vào trang này.' + '<br>' + 'Vui lòng đăng nhập lại !',
                    icon: 'error'
                });
            } else {
                Swal.fire({
                    title: 'Cập nhật thông báo Email',
                    html: 'Cập nhật thông báo Email không thành công,' + '<br>' + 'vui lòng thử lại sau !',
                    icon: 'error'
                });
            }
        },
    });
}          
