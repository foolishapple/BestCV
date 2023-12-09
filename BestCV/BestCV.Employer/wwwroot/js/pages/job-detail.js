var dataCity = [];
var dataDistrict = [];
var dataJobType = [];
var dataExperienceRange = [];
var dataJobPosition = [];
var dataJobCategory = [];
var dataSalaryType = [];
var dataRequireSkill = [];
var dataTag = [];
var listRowWorkplace = []; // danh sách các dòng html workplace 
var listWorkplaceObject = [];
var listReason = [];
var listTagId = [];
var listSkillId = [];
var listSecondaryPositionId = [];
var dataJob;



$(document).ready(() => {
    $.when(
        getListCity(),
        getListJobType(),
        getListExperienceRange(),
        getListJobPosition(),
        getListPosition(),
        getListSalaryType(),
        getListJobSkill(),
        getListTag(),
        //$('#loading').addClass('show')
    ).done(() => {
        // load thông tin chi tiết của job
        setTimeout(function () {
            getData(jobId)
            //$('#loading').removeClass('show')
        }, 500)

        // date picker
        flatpickr("#pxp-company-job-applyDate", {
            "locale": "vn", // locale for this instance only
            dateFormat: "d-m-Y",
            parseDate: (datestr, format) => {
                return moment(datestr, format, true).toDate();
            },
        });
    })
})



$('.save-button').click(() => {
    listReason = [];
    //listWorkplaceObject = [];

    // gán giá trị cho vùng quyền lợi nổi bật
    var reasons = $('#reason-zone .row-reason');
    $.each(reasons, function (index, item) {
        let obj = {
            name: $(item).find('input').val().trim(),
            id: $(item).find('input').attr('data-id'),
        }
        listReason.push(obj);
    })

    // gán giá trị cho phần khu vực
    var cityObj;
    $.each(listRowWorkplace, function (index, item) {
        // workplace cũ đã có trong mảng này, ko thêm vào
        // đối với workplace mói tạo, thay đổi đánh dấu tạo mới
        if ($(`div[data-row=workplace-${item}]`).has(".assigned").length < 1) {
            cityObj = {
                jobRequireCityId: 0,
                jobRequireDistrictId: 0,
                cityId: $(`div[data-row=workplace-${item}] .pxp-company-job-city`).val(),
                districtId: $(`div[data-row=workplace-${item}] .pxp-company-job-district`).val(),
                addressDetail: $(`div[data-row=workplace-${item}] .pxp-company-job-address`).val(),
                isAdded: true,
            }
        }
        listWorkplaceObject.push(cityObj);
    })

    var data = {
        id: dataJob.id,
        name: $('#pxp-company-job-title').val().trim(),
        recruimentCampaignId: dataJob.recruimentCampaignId,
        jobStatusId: $('.save-button').attr('id') == 'save-job' ? 1 : 0,
        jobCategoryId: $('#pxp-company-job-category').val(),
        totalRecruitment: $('#pxp-company-job-total-recruitment').val().trim(),
        genderRequirement: $('#pxp-company-job-gender-requirement').val(),
        jobTypeId: $('#pxp-company-job-type').val(),
        primaryJobPositionId: $('#pxp-company-job-primary-position').val(),
        experienceRangeId: $('#pxp-company-job-experience').val(),
        currency: $('#pxp-company-job-currency').val(),
        salaryTypeId: $('#pxp-company-job-salaryType').val(),
        salaryFrom: $('#pxp-company-job-salary-from').val().trim().replaceAll('.', ''),
        salaryTo: $('#pxp-company-job-salary-to').val().trim().replaceAll('.', ''),
        overview: CKEDITOR.instances["pxp-company-job-overview"].getData(),
        requirement: CKEDITOR.instances["pxp-company-job-benefit"].getData(),
        benefit: CKEDITOR.instances["pxp-company-job-benefit"].getData(),
        receiverName: $('#pxp-company-job-receiverName').val().trim(),
        receiverPhone: $('#pxp-company-job-receiverPhone').val().trim(),
        receiverEmail: $('#pxp-company-job-receiverEmail').val().trim(),
        applyEndDate: $('#pxp-company-job-applyDate').val() == '' ? null : moment($('#pxp-company-job-applyDate').val(), "DD-MM-YYYY").format("YYYY-MM-DD"),
        createdTime: moment(moment()).format("YYYY-MM-DD HH:mm:ss"),
        listJobSecondaryJobPosition: $('#pxp-company-job-secondary-position').val(),
        listJobRequireSkill: $('#pxp-company-job-requirementSkill').val(),
        listJobReasonApply: listReason,
        listJobRequireWorkplace: listWorkplaceObject,
        listTag: $('#pxp-company-job-tag').val(),
    }

    validateInput(data);
})
async function editJob(data) {
    try {
        let res = await httpService.putAsync("api/job/update", data);
        if (res.isSucceeded && res.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Cập nhật tin tuyển dụng',
                html: 'Chúc mừng, tin tuyển dụng của bạn đã được cập nhật thành công',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            }).then(() => {
                location.replace("");
            })
        }
        else {
            var errorHtml = `<ul style="text-align: left;">`;
            $.each(res.errors, function (index, item) {
                errorHtml += `<li>${item}</li>`;
            })
            errorHtml += `</ul>`;

            Swal.fire({
                icon: 'warning',
                title: 'Cập nhật tin tuyển dụng',
                html: errorHtml,
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
        }
    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: 'Cập nhật tin tuyển dụng',
            html: 'Có lỗi xảy ra khi cập nhật tin tuyển dụng, vui lòng thử lại sau',
            showCloseButton: false,
            showConfirmButton: false,
        })
    }
}


function validateInput(data) {
    var errors = [];
    if (data.name == '' || data.name.length == 0) {
        errors.push('Tên tuyển dụng không được để trống');
    }
    if (data.primaryJobPositionId == null) {
        errors.push('Ngành nghề chính không được để trống');
    }
    if (data.totalRecruitment == '' || data.totalRecruitment.length == 0) {
        errors.push('Số lượng tuyển không được để trống');
    }
    if (data.genderRequirement == null) {
        errors.push('Giới tính không được để trống');
    }
    if (data.experienceRangeId == null) {
        errors.push('Kinh nghiệm không được để trống');
    }
    if (data.jobCategoryId == null) {
        errors.push('Cấp bậc không được để trống');
    }
    if (data.jobTypeId == null) {
        errors.push('Loại công việc không được để trống');
    }
    if (data.currency == null) {
        errors.push('Loại tiền tệ không được để trống');
    }
    if (data.salaryTypeId == null) {
        errors.push('Loại lương không được để trống');
    }
    if (data.listJobRequireSkill.length == 0) {
        errors.push('Kỹ năng không được để trống');
    }
    if (data.listJobRequireWorkplace.length == 0) {
        errors.push('Nơi làm việc phải có ít nhất 1 địa điểm');
    }
    if (data.listJobSecondaryJobPosition.length > 2) {
        errors.push('Ngành nghề phụ không được quá 2 lựa chọn');
    }
    if (data.receiverPhone != '') {
        if (!data.receiverPhone.match(/([\\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})/gm)) {
            errors.push('Số điện thoại người nhận không đúng định dạng');
        }
    }
    if (data.receiverEmail != '') {
        if (!data.receiverEmail.match(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/gm)) {
            errors.push('Email người nhận không đúng định dạng');
        }
        if (data.receiverEmail.length > 255) {
            errors.push('Email người nhận không được quá 255 ký tự');
        }
    }

    var salaryType = $('#pxp-company-job-salaryType').val();
    var salaryFrom = $('#pxp-company-job-salary-from').val().trim().replaceAll('.', '');
    var salaryTo = $('#pxp-company-job-salary-to').val().trim().replaceAll('.', '');
    if (salaryFrom != '' && salaryTo != '') {
        if ((salaryFrom - salaryTo) > 0) {
            errors.push('Mức lương đến phải lớn hơn mức lương từ');
        }
    }
    if (salaryFrom == '' && salaryType == SALARY_TYPE_FROM) {
        $('#pxp-company-job-salary-to').val(null)
        errors.push('Mức lương đến không được để trống');
    }
    if (salaryTo == '' && salaryType == SALARY_TYPE_TO) {
        $('#pxp-company-job-salary-from').val(null)
        errors.push('Mức lương từ không được để trống');
    }
    if (salaryTo == '' && salaryFrom == '' && salaryType == SALARY_TYPE_BETWEEN) {
        errors.push('Mức lương từ không được để trống');
    }
    if (salaryType == SALARY_TYPE_AGREEMENT) {
        $('#pxp-company-job-salary-from').val(null)
        $('#pxp-company-job-salary-to').val(null)
    }

    if (errors.length > 0) {
        var errorHtml = `<ul style="text-align:left;">`;
        $.each(errors, function (index, item) {
            errorHtml += `<li>${item}</li>`;
        })
        errorHtml += `</ul>`;

        Swal.fire({
            icon: 'warning',
            title: 'Tạo tin tuyển dụng',
            html: errorHtml,
            showCloseButton: false,
            showConfirmButton: true,
            focusConfirm: true,
        })
    }
    else {
        editJob(data);
    }
}


$('.btn-add-workplace').click(() => {
    const rand = randomNumber();
    var row = `<div class="row align-items-center mb-2 row-workplace" data-row="workplace-${rand}">
                    <div class="w-10">
                        <label for="pxp-company-job-location" class="form-label">Khu vực <span>${listRowWorkplace.length + 1}</span></label>
                    </div>
                    <div class="w-20">
                        <select class="form-select pxp-company-job-city"></select>
                    </div>
                    <div class="w-20">
                        <select class="form-select pxp-company-job-district"></select>
                    </div>
                    <div class="w-35">
                        <input type="text" class="form-control pxp-company-job-address" placeholder="Nhập địa chỉ">
                    </div>
                    <div class="text-end w-15">
                        <button type="button" class="btn rounded-pill btn-secondary btn-delete" data-id="${rand}">
                            Xóa khu vực <span class="fa fa-minus"></span>
                        </button>
                    </div>
                </div>`;
    $('#workplace-zone > div').append(row);
    // thêm vào mảng
    listRowWorkplace.push(`${rand}`);
    // khởi tạo sự kiện xóa
    $(`div[data-row="workplace-${rand}"]`).on('click', '.btn-delete', function () {
        var id = $(this).attr('data-id');
        checkNumberWorkplace(id);
    })

    reInitialSelect2();
})

$('#workplace-zone .row-workplace').on('click', '.btn-delete', function () {
    var id = $(this).attr('data-id');
    checkNumberWorkplace(id);
})
function checkNumberWorkplace(id) {
    // nếu có nhiều hơn 1 dòng mới cho xóa
    if (listRowWorkplace.length > 1) {
        // tìm index của element để xóa khỏi mảng
        let i = listRowWorkplace.indexOf(id);

        // kiểm tra xem nếu là workplace được lấy ra từ DB - ko phải được gen mới
        if ($(`div[data-row=workplace-${id}]`).has(".assigned").length > 0) {
            Swal.fire({
                icon: 'warning',
                title: 'Xóa khu vực làm việc',
                html: 'Nếu xóa sẽ không khôi phục lại được <br>Bạn có chắc chắn muốn xóa?',
                showCloseButton: false,
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Có',
                cancelButtonText: 'Hủy',
                focusConfirm: true,
            }).then(function (result) {
                if (result.isConfirmed) {
                    // xóa phần tử khỏi mảng
                    listRowWorkplace.splice(i, 1)
                    // xóa phần tử khỏi html
                    $(`div[data-row=workplace-${id}]`).remove();
                    // thay đổi đánh dấu bị xóa (chỉ đối với workplace cũ)
                    listWorkplaceObject.find(x => x.jobRequireDistrictId == id).isDeleted = true;
                }
            })
        }
        else {
            // xóa phần tử khỏi mảng
            listRowWorkplace.splice(i, 1)
            // xóa phần tử khỏi html
            $(`div[data-row=workplace-${id}]`).remove();
        }

        // đặt lại stt các khu vực
        $.each(listRowWorkplace, function (index, item) {
            $(`div[data-row=workplace-${item}]`).find('label[for=pxp-company-job-location] span').text(index + 1);
        })
    }
}


/**
 * select2 phần giới tính
 */
$('#pxp-company-job-gender-requirement').select2({
    placeholder: "Chọn",
    minimumResultsForSearch: Infinity
});


/**
  * select2 chọn mức lương
  */
$('#pxp-company-job-currency').select2({
    placeholder: "Chọn",
    minimumResultsForSearch: Infinity,
}).val(null).trigger('change');


/**
 * select2 phần cấp bậc
 */
async function getListPosition() {
    try {
        let res = await httpService.getAsync("api/job-category/list");
        if (res.isSucceeded) {
            dataJobCategory = res.resources;
            $.each(dataJobCategory, function (index, item) {
                $("#pxp-company-job-category").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-category').select2({
                placeholder: "Chọn",
                minimumResultsForSearch: Infinity
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu cấp bậc", e);
    }
}

/**
 * selec2 phần chọn ngành nghề
 */
async function getListJobPosition() {
    try {
        let res = await httpService.getAsync("api/job-position/list");
        if (res.isSucceeded) {
            dataJobPosition = res.resources;
            $.each(dataJobPosition, function (index, item) {
                $("#pxp-company-job-primary-position").append(new Option(item.name, item.id));
                $("#pxp-company-job-secondary-position").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-primary-position').select2({
                placeholder: "Lựa chọn tối đa 1 ngành nghề chính",
            }).val(null).trigger('change');

            $('#pxp-company-job-secondary-position').select2({
                placeholder: "Lựa chọn tối đa 2 ngành nghề phụ",
                maximumSelectionLength: 2,
                multiple: true,
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu ngành nghề", e);
    }
}


/**
 * select2 chọn kỹ năng
 */
async function getListJobSkill() {
    try {
        let res = await httpService.getAsync("api/job-skill/list");
        if (res.isSucceeded) {
            dataRequireSkill = res.resources;
            $.each(dataRequireSkill, function (index, item) {
                $("#pxp-company-job-requirementSkill").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-requirementSkill').select2({
                placeholder: "Lựa chọn tối thiểu 1 kỹ năng",
                multiple: true,
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu kinh nghiệm", e);
    }
}



async function getListSalaryType() {
    try {
        let res = await httpService.getAsync("api/salary-type/list");
        if (res.isSucceeded) {
            dataSalaryType = res.resources;
            $.each(dataSalaryType, function (index, item) {
                $("#pxp-company-job-salaryType").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-salaryType').select2({
                placeholder: "Chọn",
                minimumResultsForSearch: Infinity
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu loại lương", e);
    }
}

$('#pxp-company-job-salaryType').on('change', function () {
    let type = $(this).val();
    switch (parseInt(type)) {
        case SALARY_TYPE_FROM:
            $('#pxp-company-job-salary-to').closest('.col-salary').hide('visibility');
            $('#pxp-company-job-salary-from').closest('.col-salary').show('visibility');
            break;
        case SALARY_TYPE_TO:
            $('#pxp-company-job-salary-from').closest('.col-salary').hide('visibility');
            $('#pxp-company-job-salary-to').closest('.col-salary').show('visibility');
            break;
        case SALARY_TYPE_AGREEMENT:
            $('#pxp-company-job-salary-from').closest('.col-salary').hide('visibility');
            $('#pxp-company-job-salary-to').closest('.col-salary').hide('visibility');
            break;
        default:
            $('#pxp-company-job-salary-from').closest('.col-salary').show('visibility');
            $('#pxp-company-job-salary-to').closest('.col-salary').show('visibility');
            break;
    }

})



async function getListExperienceRange() {
    try {
        let res = await httpService.getAsync("api/experience-range/list");
        if (res.isSucceeded) {
            dataExperienceRange = res.resources;
            $.each(dataExperienceRange, function (index, item) {
                $("#pxp-company-job-experience").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-experience').select2({
                placeholder: "Chọn",
                minimumResultsForSearch: Infinity
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu kinh nghiệm", e);
    }
}


async function getListJobType() {
    try {
        let res = await httpService.getAsync("api/job-type/list");
        if (res.isSucceeded) {
            dataJobType = res.resources;
            $.each(dataJobType, function (index, item) {
                $("#pxp-company-job-type").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-type').select2({
                placeholder: "Chọn",
                minimumResultsForSearch: Infinity
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu loại công việc", e);
    }
}

async function getListCity() {
    try {
        let res = await httpService.getAsync("api/workplace/list-city");
        if (res.isSucceeded) {
            dataCity = res.resources;
            $.each(dataCity, function (index, item) {
                $(".pxp-company-job-city").append(new Option(item.name, item.id));
            })

            $('.pxp-company-job-city').select2({
                placeholder: "Chọn tỉnh/thành phố",
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu tỉnh/thành phố", e);
    }
}

$('.pxp-company-job-district').select2({
    placeholder: "Chọn quận/huyện",
}).val(null).trigger('change');
async function getListDistrictByCity(dataRow, cityId) {
    // clear all option before append new
    $('div[data-row=' + dataRow + '] .pxp-company-job-district').empty().trigger("change");
    try {
        let res = await httpService.getAsync(`api/workplace/list-district-by-cityid/${cityId}`);
        if (res.isSucceeded) {
            dataDistrict = res.resources;

            $.each(dataDistrict, function (index, item) {
                $('div[data-row=' + dataRow + '] .pxp-company-job-district').not('.assigned').append(new Option(item.name, item.id));
            })

            $('div[data-row=' + dataRow + '] .pxp-company-job-district').select2({
                placeholder: "Chọn quận/huyện",
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu quận/huyện", e);
    }
}


$('.pxp-company-job-city').on('change', function () {
    let cityId = $(this).val();
    var parent = $(this).parents('.row-workplace').attr('data-row');
    getListDistrictByCity(parent, cityId);
})


function reInitialSelect2() {
    // khởi tạo select2
    $.each(dataCity, function (index, item) {
        $('.pxp-company-job-city:not(".assigned")').append(new Option(item.name, item.id));
    })

    $('.pxp-company-job-city:not(".assigned")').select2({
        placeholder: "Chọn tỉnh/thành phố",
    }).val(null).trigger('change');

    $('.pxp-company-job-district:not(".assigned")').select2({
        placeholder: "Chọn quận/huyện",
    }).val(null).trigger('change');

    // khởi tạo lại sự kiện select
    $('.pxp-company-job-city').on('change', function () {
        let cityId = $(this).val();
        var parent = $(this).parents('.row-workplace').attr('data-row');
        getListDistrictByCity(parent, cityId);
    })
}



async function getListTag() {
    try {
        let res = await httpService.getAsync("api/tag/list-tag-type-job");
        if (res.isSucceeded) {
            dataTag = res.resources;
            $.each(dataTag, function (index, item) {
                $("#pxp-company-job-tag").append(new Option(item.name, item.id));
            })

            $("#pxp-company-job-tag").select2({//remote data tag
                placeholder: "Chọn thẻ",
                multiple: "multiple",
                language: "vi",
                ajax: {
                    url: systemConfig.defaultAPI_URL + "api/tag/list-tag-select",
                    dataType: 'json',
                    type: "GET",
                    delay: 250,
                    data: function (params) {
                        var keyword = $.trim(params.term);
                        if (keyword != undefined && keyword != null && keyword != "" && keyword.startsWith('#')) {
                            keyword = keyword.slice(1);
                        }
                        return {
                            keyWord: keyword, // search term
                            pageIndex: params.page,
                            pageSize: 20, //page size
                            tagTypeId: TAG_TYPE,
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.listData,
                            pagination: {
                                more: (params.page * 20) < data.totalRecord//20 is page size
                            }
                        };
                    },
                    cache: true
                },
                allowClear: true,
                tags: true,
                templateResult: formatResult,
                templateSelection: formatSelection,
                createTag: function (params) {
                    var term = $.trim(params.term);
                    if (term === '') {
                        return null;
                    }
                    if (!term.startsWith('#') || term.length < 2) {//check # in created tag
                        return null;
                    }
                    else {
                        term = term.slice(1);
                    }
                    return {
                        id: term,
                        text: term,
                        newTag: true // add additional parameters
                    }
                }
            }).on("select2:select", function (e) {
                for (var i = 0; i < e.target.length; i++) {
                    var target = e.target[i];
                    if ($(target).attr("data-select2-tag")) {
                        $.ajax({
                            url: systemConfig.defaultAPI_URL + "api/tag/add-tag-for-job",
                            type: "POST",
                            contentType: "application/json",
                            dataType: "json",
                            data: JSON.stringify({ name: $(target).text() }),
                            async: false,
                            success: function (res) {
                                if (res.isSucceeded) {
                                    var obj = res.resources;
                                    $(target).attr("value", obj.id);
                                    $(target).prop("data-select2-tag", false);
                                }
                                else {
                                    $(target).remove();
                                }
                            },
                            error: function (e) {
                                $(target).remove();
                            }
                        })
                    }
                }
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu thẻ", e);
    }
}

function formatResult(item) {
    if (item.loading) {
        return item.text;
    }
    return item.text;
}

function formatSelection(item) {
    return item.text;
}


/**
 * bấm vào icon lịch
 */
$('#receiver-info > div i.fa').click(() => {
    $('#pxp-company-job-applyDate').click();
})




/**____________________________________________________________________________________________________________________________________________________________________________________ */



async function getData(id) {
    try {
        let response = await httpService.getAsync("api/job/job-detail/" + id);
        if (response.isSucceeded) {
            dataJob = response.resources;

            $.each(dataJob.listTag, function (index, item) {
                listTagId.push(item);
            })
            $.each(dataJob.listJobSecondaryJobPosition, function (index, item) {
                listSecondaryPositionId.push(item);
            })
            $.each(dataJob.listJobRequireSkill, function (index, item) {
                listSkillId.push(item);
            })

            // gán giá trị vào html
            $('#pxp-company-job-title').val(dataJob.name);
            $('#pxp-company-job-tag').val(dataJob.listTag).trigger('change');
            $('#pxp-company-job-primary-position').val(dataJob.primaryJobPositionId).trigger('change');
            $('#pxp-company-job-secondary-position').val(dataJob.listJobSecondaryJobPosition).trigger('change');
            $('#pxp-company-job-total-recruitment').val(dataJob.totalRecruitment);
            $('#pxp-company-job-gender-requirement').val(dataJob.genderRequirement).trigger('change');
            $('#pxp-company-job-experience').val(dataJob.experienceRangeId).trigger('change');
            $('#pxp-company-job-category').val(dataJob.jobCategoryId).trigger('change');
            $('#pxp-company-job-type').val(dataJob.jobTypeId).trigger('change');
            $('#pxp-company-job-currency').val(dataJob.currency).trigger('change');
            $('#pxp-company-job-salaryType').val(dataJob.salaryTypeId).trigger('change');
            $('#pxp-company-job-salary-from').val(dataJob.salaryFrom);
            $('#pxp-company-job-salary-to').val(dataJob.salaryTo);
            $('#pxp-company-job-requirementSkill').val(dataJob.listJobRequireSkill).trigger('change');
            $('#pxp-company-job-applyDate').val(dataJob.applyEndDate != null ? moment(dataJob.applyEndDate).format('DD-MM-YYYY') : '');
            $('#pxp-company-job-receiverName').val(dataJob.receiverName);
            $('#pxp-company-job-receiverPhone').val(dataJob.receiverPhone);
            $('#pxp-company-job-receiverEmail').val(dataJob.receiverEmail);

            // CK-editer inputs
            CKEDITOR.instances["pxp-company-job-overview"].setData(dataJob.overview);
            CKEDITOR.instances["pxp-company-job-requirement"].setData(dataJob.requirement);
            CKEDITOR.instances["pxp-company-job-benefit"].setData(dataJob.benefit);

            // gán giá trị vào html reasons apply
            var reasons = $('#reason-zone .row-reason');
            listReason = dataJob.listJobReasonApply;
            if (listReason.length > 0) {
                $.each(reasons, function (index, item) {
                    $(item).find('input').val(listReason[index].name);
                    $(item).find('input').attr('data-id', listReason[index].id);
                })
            }

            // gán giá trị vào html workplace inputs
            listWorkplaceObject = dataJob.listJobRequireWorkplace;
            if (listWorkplaceObject.length > 0) {
                $.each(listWorkplaceObject, async function (index, item) {
                    //const rand = randomNumber();
                    var row = `<div class="row align-items-center mb-2 row-workplace assigned" data-row="workplace-${item.jobRequireDistrictId}">
                                    <div class="w-10">
                                        <label for="pxp-company-job-location" class="form-label">Khu vực <span>${index + 1}</span></label>
                                    </div>
                                    <div class="w-20">
                                        <select class="form-select pxp-company-job-city" data-city-id="${item.jobRequireDistrictId}" disabled></select>
                                    </div>
                                    <div class="w-20">
                                        <select class="form-select pxp-company-job-district" disabled></select>
                                    </div>
                                    <div class="w-35">
                                        <input type="text" class="form-control pxp-company-job-address" placeholder="Nhập địa chỉ" disabled>
                                    </div>
                                    <div class="text-end w-15">
                                        <button type="button" class="btn rounded-pill btn-secondary btn-delete assigned" data-id="${item.jobRequireDistrictId}">
                                            Xóa khu vực <span class="fa fa-minus"></span>
                                        </button>
                                    </div>
                                </div>`;

                    // render html
                    $('#workplace-zone > div').append(row);
                    // thêm vào mảng số dòng html workplace
                    listRowWorkplace.push(`${item.jobRequireDistrictId}`);

                    // khởi tạo lại các sự kiện cho select2 mới tạo
                    //reInitialSelect2();
                    $.each(dataCity, function (index, item) {
                        $(`.pxp-company-job-city`).append(new Option(item.name, item.id, false, false)).trigger("change");
                    })

                    $(`.pxp-company-job-city`).select2({
                        placeholder: "Chọn tỉnh/thành phố",
                    });

                    // khởi tạo sựu kiện xong mới gán giá trị cho city
                    $(`div[data-row=workplace-${item.jobRequireDistrictId}] .pxp-company-job-city`).val(item.cityId).trigger('change').addClass('assigned');

                    // có các option cho district sau khi chọn city rồi mới gán giá trị được
                    await getListDistrictByCity(`workplace-${item.jobRequireDistrictId}`, item.cityId);
                    $(`div[data-row=workplace-${item.jobRequireDistrictId}] .pxp-company-job-district`).val(item.districtId).trigger('change').addClass('assigned');
                    $(`div[data-row=workplace-${item.jobRequireDistrictId}] .pxp-company-job-address`).val(item.addressDetail);

                    //
                    $(`.btn-delete[data-id=${item.jobRequireDistrictId}]`).on('click', function () {
                        var id = $(this).attr('data-id');
                        checkNumberWorkplace(id);
                    })
                })
            }


            // khởi tạo format input money
            $.each($("input[data-type='currency']"), function (item, index) {
                formatCurrency($(this));
            });
        }
        else if (response.status === 404) {
            window.location.replace('/khong-tim-thay-trang');
        }
        else {
            var errorHtml = `<ul style="text-align: left;">`;
            $.each(response.errors, function (index, item) {
                errorHtml += `<li>${item}</li>`;
            })
            errorHtml += `</ul>`;

            Swal.fire({
                icon: 'warning',
                title: 'Chi tiết tin tuyển dụng',
                html: errorHtml,
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
        }
    } catch (e) {
        Swal.fire({
            icon: 'warning',
            title: 'Chi tiết tin tuyển dụng',
            html: 'Có lỗi xảy ra khi lấy thông tin của tin tuyển dụng, <br>vui lòng thử lại sau',
            showCloseButton: false,
            showConfirmButton: false,
        })
    }
}
