var dataSource = [];
$(document).ready(function () {

    if (localStorage.currentUser) {
        loadDataCandidateFollowCompany();
    }
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })
    $(".btn-add-candidate").on('click', function () {
        var btn = $(this);
        if (localStorage.currentUser) {
            //let candidateId = $(this).attr("data-candidate-id");
            let companyId = $(this).attr("data-company-id");
            let companyName = $(this).attr("data-company-name");
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/candidate-follow-company/add-candidate-follow-company",
                data: JSON.stringify({ companyId: companyId, companyName: companyName }),
                type: "Post",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    if (localStorage.currentUser) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
                    }
                },
                success: function (responseData) {
                    if (responseData.isSucceeded) {
                        if (responseData.status == 200 && responseData.message == "Success") {
                            Toast.fire({
                                icon: 'success',
                                title: '<div><label class="fs-14 fw-bold">Cảm ơn bạn đã theo dõi công ty</label><br/><strong class="color-success">' + companyName + '</strong><br/></div>'
                            })

                            let btnText = btn.find('.btn-text')
                            btnText.text('Bỏ theo dõi');
                            btn.addClass('unfollow');
                        } else if (responseData.status == 200 && responseData.message == "Delete") {
                            Toast.fire({
                                icon: 'success',
                                title: '<div><label class="fs-14 fw-bold">Đã bỏ theo dõi công ty</label><br/><strong class="color-success">' + companyName + '</strong><br/></div>'
                            })

                            let btnText = btn.find('.btn-text')
                            btnText.text('Theo dõi');
                            btn.removeClass('unfollow');
                        }
                    }
                },
                error: function (error) {
                    if (responseData.status == 401) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Lưu ý',
                            html: 'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại!',
                            focusConfirm: true,
                            allowEnterKey: true
                        });
                    }
                },

            })
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'Lưu ý',
                html: 'Bạn chưa đăng nhập, vui lòng đăng nhập để sử dụng tính năng này!',
                focusConfirm: true,
                allowEnterKey: true
            });
        }
    });
});

async function loadDataCandidateFollowCompany() {
    try {
        let res = await httpService.postAsync(`api/candidate-follow-company/get-list-candidate-follow-company`);
        if (res.isSucceeded) {
            if (res.status == 200) {
                dataSource = res.resources;
                dataSource.forEach(item => {
                    $(`.btn-add-candidate[data-company-id=${item.companyId}]`).addClass('unfollow');
                    $(`.btn-add-candidate[data-company-id=${item.companyId}] span.btn-text`).text("Bỏ theo dõi");
                })
            }
        }
    }
    catch (e) {
        dataSource = [];
        Swal.fire({
            icon: 'warning',
            title: 'Lấy dữ liệu thất bại',
            html: 'Có lỗi xảy ra khi lấy danh sách công việc đã lưu, vui lòng thử lại',
            showCloseButton: false,
            focusConfirm: true,
            confirmButtonText: 'Ok',
        })
    }
}