var dataCity = [];
var dataDistrict = [];
var dataJobType = [];
var dataExperienceRange = [];
var dataJobPosition = [];
var dataJobCategory = [];
var dataSalaryType = [];
var dataRequireSkill = [];
var dataTag = [];
var listWorkplaceObject = [];
var listReason = [];
var listTag = [];




$(document).ready(() => {
    $.when(
        getListCity(),
        getListJobType(),
        getListExperienceRange(),
        getListJobPosition(),
        getListCategory(),
        getListSalaryType(),
        getListJobSkill(),
        getListTag(),
        getListRecruitmentCampaign(),
        getListRecruitmentCampaignStatus(),
    ).done(() => {
        // date picker
        flatpickr("#pxp-company-job-applyDate", {
            "locale": "vn", // locale for this instance only
            dateFormat: "d-m-Y",
            parseDate: (datestr, format) => {
                return moment(datestr, format, true).toDate();
            },
        });
        $('#kt_docs_repeater_advanced').repeater({
            initEmpty: false,

            defaultValues: {
                'text-input': 'foo'
            },

            show: function () {
                $(this).slideDown();
                let districtTarget = $(this).find('[data-repeater="select-district"]').select2();
                // Re-init select2
                $(this).find('[data-repeater="select-city"]').select2({
                    data: dataCity
                }).on("select2:select", async function (e) {
                    let cityId = e.params.data.id;
                    districtTarget.html("").trigger("change");
                    try {
                        let res = await httpService.getAsync(`api/workplace/list-district-by-cityid/${cityId}`);
                        if (res.isSucceeded) {
                            $.each(res.resources, function (index, item) {
                                districtTarget.append(new Option(item.name, item.id)).trigger("change");
                            })
                        }
                    } catch (e) {
                        console.error("Có lỗi xảy ra khi lấy dữ liệu quận/huyện", e);
                    }
                });

            },

            hide: function (deleteElement) {
                $(this).slideUp(deleteElement);
            },

            ready: function () {
                // Init select2
                $('[data-repeater="select-city"]').select2({
                    data: dataCity
                }).on("select2:select", async function (e) {
                    let cityId = e.params.data.id;
                    let element = $(this);
                    let elementTarget = element.parent().parent().parent().find('[data-repeater="select-district"]');
                    elementTarget.html("").trigger("change");
                    console.log(elementTarget);
                    try {
                        let res = await httpService.getAsync(`api/workplace/list-district-by-cityid/${cityId}`);
                        if (res.isSucceeded) {
                            $.each(res.resources, function (index, item) {
                                elementTarget.append(new Option(item.name, item.id)).trigger("change");
                            })
                        }
                    } catch (e) {
                        console.error("Có lỗi xảy ra khi lấy dữ liệu quận/huyện", e);
                    }
                });
                $('[data-repeater="select-district"]').select2();
                $("[data-repeater-item]:first-child [data-repeater-delete]").remove()
            }
        });
    })
})


$('.save-button').click(() => {
    $('#loading').addClass('show')
    listReason = [];
    listWorkplaceObject = [];

    // gán giá trị cho vùng quyền lợi nổi bật
    var reasons = $('#reason-zone .row-reason');
    $.each(reasons, function (index, item) {
        listReason.push($(item).find('input').val().trim());
    })

    // gán giá trị cho phần khu vực
    let workplaceElements = $(".workplace-item");
    for (var i = 0; i < workplaceElements.length; i++) {
        let cityObj;
        let workplaceItem = $(workplaceElements[i]);
        let cityId = workplaceItem.find('[data-repeater=select-city]').val();
        if (cityId != null && cityId != undefined && cityId!="") {
            cityObj = {
                cityId: cityId,
                districtId: workplaceItem.find('[data-repeater=select-district]').val(),
                addressDetail: workplaceItem.find('[data-repeater=address-detail]').val(),
            }
            listWorkplaceObject.push(cityObj);
        }
    }

    var data = {
        name: $('#pxp-company-job-title').val().trim(),
        recruimentCampaignId: recruitmentCampaginId,
        jobStatusId: $('.save-button').attr('id') == 'save-job' ? 1 : 0,
        primaryJobCategoryId: $('#pxp-company-job-primary-category').val(),
        totalRecruitment: $('#pxp-company-job-total-recruitment').val().trim(),
        genderRequirement: $('#pxp-company-job-gender-requirement').val(),
        jobTypeId: $('#pxp-company-job-type').val(),
        jobPositionId: $('#pxp-company-job-position').val(),
        experienceRangeId: $('#pxp-company-job-experience').val(),
        currency: $('#pxp-company-job-currency').val(),
        salaryTypeId: $('#pxp-company-job-salaryType').val(),
        salaryFrom: $('#pxp-company-job-salary-from').val().trim().replaceAll('.', ''),
        salaryTo: $('#pxp-company-job-salary-to').val().trim().replaceAll('.', ''),
        overview: CKEDITOR.instances["pxp-company-job-overview"].getData(),
        requirement: CKEDITOR.instances["pxp-company-job-requirement"].getData(),
        benefit: CKEDITOR.instances["pxp-company-job-benefit"].getData(),
        receiverName: $('#pxp-company-job-receiverName').val().trim(),
        receiverPhone: $('#pxp-company-job-receiverPhone').val().trim(),
        receiverEmail: $('#pxp-company-job-receiverEmail').val().trim(),
        applyEndDate: $('#pxp-company-job-applyDate').val() == '' ? null : moment($('#pxp-company-job-applyDate').val(), "DD-MM-YYYY").format("YYYY-MM-DD"),
        createdTime: moment(moment()).format("YYYY-MM-DD HH:mm:ss"),
        listJobSecondaryJobCategory: $('#pxp-company-job-secondary-category').val(),
        listJobRequireSkill: $('#pxp-company-job-requirementSkill').val(),
        listJobReasonApply: listReason,
        listJobRequireWorkplace: listWorkplaceObject,
        listTag: $('#pxp-company-job-tag').val(),
    }

    validateInput(data);
})
async function editJob(data) {
    try {
        let res = await httpService.postAsync("api/job/add", data);
        if (res.isSucceeded && res.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Tạo tin tuyển dụng',
                html: 'Chúc mừng, tin tuyển dụng đã được tạo thành công',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            }).then(() => {
                window.location.replace('/quan-ly-tin-tuyen-dung');
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
                title: 'Tạo tin tuyển dụng',
                html: errorHtml,
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
        }
        $('#loading').removeClass('show');
    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: 'Tạo tin tuyển dụng',
            html: 'Có lỗi xảy ra khi tạo tin tuyển dụng, <br>vui lòng thử lại sau',
            showCloseButton: false,
            showConfirmButton: false,
        })
        $('#loading').removeClass('show');
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
        errors.push('Mức lương từ không được để trống');
    }
    if (salaryTo == '' && salaryType == SALARY_TYPE_TO) {
        $('#pxp-company-job-salary-from').val(null)
        errors.push('Mức lương đến không được để trống');
    }
    if (salaryTo == '' && salaryFrom == '' && salaryType == SALARY_TYPE_BETWEEN) {
        errors.push('Mức lương từ và đến không được để trống');
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
        $('#loading').removeClass('show');
    }
    else {
        editJob(data);
        $('#loading').removeClass('show');
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

    // khởi tạo select2
    $.each(dataCity, function (index, item) {
        $('div[data-row=workplace-' + rand + '] .pxp-company-job-city').append(new Option(item.name, item.id));
    })

    $('div[data-row=workplace-' + rand + '] .pxp-company-job-city').select2({
        placeholder: "Chọn tỉnh/thành phố",
    }).val(null).trigger('change');

    $('div[data-row=workplace-' + rand + '] .pxp-company-job-district').select2({
        placeholder: "Chọn quận/huyện",
    }).val(null).trigger('change');

    // khởi tạo lại sự kiện select
    $('.pxp-company-job-city').on('change', function () {
        let cityId = $(this).val();
        var parent = $(this).parents('.row-workplace').attr('data-row');
        getListDistrictByCity(parent, cityId);
    })
})
/**
 * author: truongthieuhuyen
 * created: 14.08.2023
 * sự kiện xóa khu vực làm việc
 */
$('#workplace-zone .row-workplace').on('click', '.btn-delete', function () {
    var id = $(this).attr('data-id');
    checkNumberWorkplace(id);
})
function checkNumberWorkplace(id) {
    // nếu có nhiều hơn 1 dòng
    if (listRowWorkplace.length > 1) {
        // tìm index của element để xóa khỏi mảng
        let i = listRowWorkplace.indexOf(id);
        // xóa phần tử khỏi mảng
        listRowWorkplace.splice(i, 1)
        // xóa phần tử khỏi html
        $(`div[data-row=workplace-${id}]`).remove();

        // đặt lại stt các khu vực
        $.each(listRowWorkplace, function (index, item) {
            $(`div[data-row=workplace-${item}]`).children('label').children('span').text(index + 1);
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
}).trigger('change');


/**
 * select2 phần nghành nghề
 */
async function getListCategory() {
    try {
        let res = await httpService.getAsync("api/job-category/list");
        if (res.isSucceeded) {
            dataJobCategory = res.resources;
            $.each(dataJobCategory, function (index, item) {
                $("#pxp-company-job-primary-category").append(new Option(item.name, item.id));
                $("#pxp-company-job-secondary-category").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-primary-category,#pxp-company-job-secondary-category').select2({
                placeholder: "Chọn nghành nghề",
                minimumResultsForSearch: Infinity
            }).val(null).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu cấp bậc", e);
    }
}

/**
 * selec2 phần chọn vị trí tuyển dụng
 */
async function getListJobPosition() {
    try {
        let res = await httpService.getAsync("api/job-position/list");
        if (res.isSucceeded) {
            dataJobPosition = res.resources;
            $.each(dataJobPosition, function (index, item) {
                $("#pxp-company-job-position").append(new Option(item.name, item.id));
            })

            $('#pxp-company-job-position').select2({
                placeholder: "Lựa chọn vị trí tuyển dụng",
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
                item.text = item.name;
                item.value = item.id;
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
                $('div[data-row=' + dataRow + '] .pxp-company-job-district').append(new Option(item.name, item.id));
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

/**______________________________________________________________________________________________________________________________________________________________________ */


async function getListRecruitmentCampaign() {
    try {
        let res = await httpService.getAsync("api/recruitment-campaign/list-to-employer");
        if (res.isSucceeded) {
            let recuitmentCampaginSource = res.resources;
            $("#pxp-company-campaign-name").val(recuitmentCampaginSource.find(c => c.id == recruitmentCampaginId).name + " - #" + recruitmentCampaginId);
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu danh sách chiến dịch", e);
    }
}


async function getListRecruitmentCampaignStatus() {
    try {
        let res = await httpService.getAsync("api/recruitment-campaign-status/list");
        if (res.isSucceeded) {
            dataJobType = res.resources;
            $.each(dataJobType, function (index, item) {
                $("#campaign-status").append(new Option(item.name, item.id));
            })

            $('#campaign-status').select2({
                placeholder: "Chọn",
            }).trigger('change');
        }
    } catch (e) {
        console.error("Có lỗi xảy ra khi lấy dữ liệu danh sách chiến dịch", e);
    }
}


$('.selection-campaign>.pxp-nav-btn').on('click', function () {
    if (!$(this).hasClass('active')) {
        $('.pxp-nav-btn').removeClass('active');
        $('.pxp-hero-card').hide('visibility');
        $(this).addClass('active');
        $(this).siblings('.pxp-hero-card').show('visibility');
    }
})

$('.btn-action').click(function () {
    $('#loading').addClass('show');
    setTimeout(checkPage($(this))
        , 3000)
});
function checkPage(element) {
    if ($(element).attr('id') == 'next-action') {
        if ($(element).attr('data-campaign-id') != undefined && $(element).attr('data-campaign-id') != '') {
            $('.pxp-dashboard-content-details').addClass('action');
            $('#loading').removeClass('show');
        }
        else {
            Swal.fire({
                icon: 'warning',
                title: 'Bạn chưa chọn chiến dịch',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
            $('#loading').removeClass('show');
        }
    }
    if ($(element).attr('id') == 'previous-action') {
        $('.pxp-dashboard-content-details').removeClass('action');
        $('#loading').removeClass('show');
    }
}


$('#pxp-campaign').on('change', function () {
    $('#next-action').attr('data-campaign-id', $(this).val());
    $('#pxp-company-campaign-name').val($(this).children('option:selected').text());
})


$('.btn_submit').click(() => {
    var data = {
        "name": $('#campaign-name').val().trim(),
        "description": $('#campaign-description').val(),
        "recruitmentCampaignStatusId": $('#campaign-status').val(),
    }
    createCampaign(data);
})
async function createCampaign(data) {
    try {
        let res = await httpService.postAsync("api/recruitment-campaign/add-to-employer", data);
        if (res.isSucceeded) {
            Swal.fire({
                icon: 'success',
                title: 'Tạo chiến dịch tuyển dụng',
                html: 'Chúc mừng, chiến dịch mới đã được tạo thành công',
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            }).then(() => {
                $('#next-action').attr('data-campaign-id', res.resources.id);
                $('#pxp-company-campaign-name').val(res.resources.name);
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
                title: 'Tạo chiến dịch tuyển dụng',
                html: errorHtml,
                showCloseButton: false,
                focusConfirm: true,
                confirmButtonText: 'Ok',
            })
        }
    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: 'Tạo chiến dịch tuyển dụng',
            html: 'Có lỗi xảy ra khi tạo chiến dịch, <br>vui lòng thử lại sau',
            showCloseButton: false,
            showConfirmButton: false,
        })
    }
}