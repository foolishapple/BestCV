$("#btnUpdate").on('click', function (e) {
    update();
})
document.getElementById('toggleSearch').addEventListener('change', function (e) {
    var statusElement = document.getElementById('searchStatus');
    if (this.checked) {
        statusElement.textContent = "Trạng thái tìm kiếm việc làm đang bật";
        statusElement.style.color = "green";
        // Thực hiện các hành động cần thiết khi bắt đầu tìm kiếm
    } else {
        statusElement.textContent = "Trạng thái tìm kiếm việc làm đang tắt";
        statusElement.style.color = "red";
        // Thực hiện các hành động cần thiết khi dừng tìm kiếm
    }
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job-category/list",
        type: "GET",
        success: function (data) {
            var select = $('#jobCategoryId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job-position/list",
        type: "GET",
        success: function (data) {
            var select = $('#jobPositionId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job-skill/list",
        type: "GET",
        success: function (data) {
            var select = $('#jobSkillId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/experience-range/list",
        type: "GET",
        success: function (data) {
            var select = $('#workExperienceId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/salary-range/list",
        type: "GET",
        success: function (data) {
            var select = $('#salaryId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/workplace/list-city",
        type: "GET",
        success: function (data) {
            var select = $('#workPlaceId');
            select.empty();
            // Make sure to access the 'resources' property of the response data
            $.each(data.resources, function (index, item) {
                select.append($('<option></option>').attr('value', item.id).text(item.name));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //console.log(textStatus, errorThrown);
        }
    });
});


function update() {
    var selectedValues = {
        Id: $('#id').val(),
        Gender: $("input[name='gender']:checked").val(),
        JobPositionId: $('#jobPositionId').val(),
        JobCategoryId: $('#jobCategoryId').val(),
        JobSkillId: $('#jobSkillId').val(),
        JobId: $('#jobId').val(),
        experienceRangeId: $('#workExperienceId').val(),
        salaryRangeId: $('#salaryId').val(),
        workPlaceId: $('#workPlaceId').val(),
        jobStatusSearch: $('#toggleSearch').is(':checked')
    };
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/Candidate/job-suggestion-setting",
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
                    title: 'Tài khoản <p class="swal__admin__subtitle"> Cập nhật gợi ý việc làm',
                    icon: 'success',
                    html: '<p class="swal__admin__subtitle">Cập nhật gợi ý việc làm thành công!</p>',
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
                            'Gợi ý việc làm <p class="swal__admin__subtitle"> Cập nhật không thành công </p>',
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
                    title: 'Cập nhật gợi ý việc làm',
                    html: 'Bạn không được phép truy cập vào trang này.' + '<br>' + 'Vui lòng đăng nhập lại !',
                    icon: 'error'
                });
            } else {
                Swal.fire({
                    title: 'Cập nhật gợi ý việc làm',
                    html: 'Cập nhật gợi ý việc làm không thành công,' + '<br>' + 'vui lòng thử lại sau !',
                    icon: 'error'
                });
            }
        },
    });
}          
