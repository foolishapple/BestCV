
$(document).ready(function (){
    verifyEmail();
})


async function verifyEmail() {
    
    try {
        let result = await httpService.putAsync('api/employer/verify-email-code', data);
        
        if (result.isSucceeded) {
            if (result.status == 200) {
                $('.pxp-section-h2').text("Xác thực tài khoản thành công");
                $('.pxp-text').text(result.message);
            }
        }
        else {
            if (result.status == 400) {
                $('#pxp-sign-in').hide();
                $('#pxp-re-send-email').show();
                $('#img-failed').show();
                $('#img-success').hide();
                $('.pxp-section-h2').text("Xác thực tài khoản không thành công");
                $('.pxp-text').text(result.message);
            }
            else if (result.status == 404) {
                $('#pxp-sign-in').hide();
                $('#pxp-re-send-email').show();
                $('#img-failed').show();
                $('#img-success').hide();
                $('.pxp-section-h2').text("Xác thực tài khoản không thành công");
                $('.pxp-text').text(result.message);
            } 
        }

    } catch (e) {
        $('#pxp-sign-in').hide();
        $('#pxp-re-send-email').show();
        $('#img-failed').show();
        $('#img-success').hide();
        $('.pxp-section-h2').text("Xác thực tài khoản không thành công");
        $('.pxp-text').text('Có lỗi xảy ra, vui lòng thử lại sau');

        
    }

    // turn off loading
    $('body').removeClass('no-scroll');
    $('.pxp-prepareVerifying').css('display', 'none');
}



/**
 * Bấm nút gửi lại
 */
$('#pxp-re-send-email').on('click', function () {
    var urlElements = window.location.href.split("/");

    var obj = {
        value: urlElements[urlElements.length - 2],
        hash: urlElements[urlElements.length - 1]
    }

    $.ajax({
        url: systemConfig.defaultAPIURL + 'api/employer/re-send-email',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (res) {
            if (res.isSucceeded) {
                Swal.fire({
                    icon: 'success',
                    title: 'Gửi email xác nhận lại thành công',
                    html: 'Vui lòng kiểm tra email để kích hoạt tài khoản',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                    allowOutsideClick: false
                }).then(() => {
                    window.location.href = window.location.origin + '/dang-nhap';
                })
            }
            else if (!res.isSucceeded && res.status == 400) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Gửi email không thành công',
                    html: res.message,
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                })
            }
            else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Gửi email không thành công',
                    html: 'Có lỗi xảy ra khi gửi yêu cầu, vui lòng liên hệ hotline.',
                    showCloseButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Ok',
                })
            }
        },
        error: function (e) {
            Swal.fire({
                icon: 'warning',
                title: 'Gửi email không thành công',
                html: 'Có lỗi xảy ra khi gửi yêu cầu, vui lòng liên hệ hotline.',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
        }
    })
})