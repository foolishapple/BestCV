function loadDataDetailJob() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/job/detail-job-on-home-page/" + jobId,
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.currentUser) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
            }
        },
        success: function (response) {
            if (response.isSucceeded) {
                //console.log(response.resources);
                var data = response.resources;

                //$("#company-cover-photo").css("background-image", (data.companyCoverPhoto != null ? "url(" + systemConfig.defaultStorage_URL + data.companyCoverPhoto + ");" : "url(" + systemConfig.defaultStorage_URL + "/uploads/companys/cover-photo/default-cover.jpg" + ");"));
                //$("#company-logo").css("background-image", (data.companyLogo != null ? "url(" + systemConfig.defaultStorage_URL + data.companyLogo + ");" : "url(" + systemConfig.defaultStorage_URL + "uploads/companys/avatars/logo1.png" + ");"));

                $("#company-cover-photo").attr("style", (data.companyCoverPhoto != null ? "background-image : url(" + systemConfig.defaultStorageURL + data.companyCoverPhoto + ");" : "background-image :url(" + systemConfig.defaultStorageURL + "/uploads/companys/cover-photo/default-cover.jpg" + ");"))

                $("#company-logo").attr("src", (data.companyLogo != null ? systemConfig.defaultStorageURL + data.companyLogo : systemConfig.defaultStorageURL + "/uploads/companys/avatars/logo1.png"))
                $("#company-logo-2").css("background-color", "white")
                $("#company-logo-2").css("background-image", (data.companyLogo != null ? "url(" + systemConfig.defaultStorageURL + data.companyLogo + ")" : "url(" + systemConfig.defaultStorage_URL + "uploads/companys/avatars/logo1.png" + ");"))

                $("#job-name").text(data.name);
                $("#company-name").text(data.companyName);
                //hiển thị tag city
                let dataCity = "";
                for (let x = 0; x < data.jobRequireCity.length; x++) {
                    dataCity += `<span class='badge-city mb-2'>${data.jobRequireCity[x].cityName.replace("Thành phố ", "")}</span>`
                }
                if (dataCity != "") {
                    $("#job-address").html(dataCity)
                }
                else {
                    $("#div-job-address").addClass('d-none')
                }
                $("#job-address").html(dataCity)
                $("#job-category-filter").attr("href", "/tim-kiem-viec-lam?danhmuc=" + data.jobCategoryId)
                $("#job-category-icon").html(`<span class="${data.jobCategoryIcon}"></span>`);
                $("#job-category").text(data.jobCategoryName);
                $("#job-overView").html(data.overview != "" && data.overview != null ? "<h4>Tổng quan</h4>" + data.overview : "")

                $("#job-description").html(data.description != "" && data.description != null ? "<h4>Mô tả công việc</h4>" + data.description : "")

                $("#job-requirement").html(data.requirement != "" && data.requirement != null ? "<h4>Yêu cầu công việc</h4>" + data.requirement : "")
                $("#job-benefit").html(data.benefit != "" && data.benefit != null ? "<h4>Quyền lợi</h4>" + data.benefit : "")
                var contentJobSkill = "";
                if (data.jobRequireSkill.length > 0) {
                    data.jobRequireSkill.forEach(function (item) {
                        //contentJobSkill += "<li>" + item.jobSkillName + "</li>";
                        contentJobSkill += `<span class="badge-city mb-2">${item.jobSkillName}</span>`;
                    })
                }
                //$("#job-require-skill").html(contentJobSkill != "" ? "<h4>Kỹ năng cần có</h4>" + contentJobSkill : "");
                if (contentJobSkill != "") {
                    $("#job-skill-requirement").html(contentJobSkill)
                }
                else {
                    $("#div-job-skill-requirement").addClass('d-none')
                }

                $("#job-createdTime").text(timeAgo(data.createdTime));

                $("#job-experience").text(data.experienceRangeName);
                $("#job-position").text(data.primaryJobPositionName);
                $("#job-type").text(data.jobTypeName);

                var contentSalaryRange = "";
                switch (data.salaryTypeId) {
                    case 1001:
                        contentSalaryRange = "Từ " + formatNumber(data.salaryFrom.toString()) + " VNĐ";
                        break;
                    case 1002:
                        contentSalaryRange = "Đến " + formatNumber(data.salaryTo.toString()) + " VNĐ";
                        break;
                    case 1003:
                        contentSalaryRange = formatNumber(data.salaryFrom.toString()) + " - " + formatNumber(data.salaryTo.toString()) + " VNĐ";
                        break;
                    case 1004:
                        contentSalaryRange = data.salaryTypeName;
                        break;
                }
                $("#job-offer").text(contentSalaryRange);

                var genderRequirement = "";
                switch (data.genderRequirement) {
                    case 0:
                        genderRequirement = "Không yêu cầu";
                        break;
                    case 1:
                        genderRequirement = "Nam";
                        break;
                    case 2:
                        genderRequirement = "Nữ";
                        break;
                }
                $("#job-gender").text(genderRequirement);

                $("#job-total-requirement").text(data.totalRecruitment + " người")

                if (data.applyEndDate != null) {
                    $("#job-header").append(`<span class="badge bg-secondary badge-job-expire"><i class="fa fa-clock-o" aria-hidden="true"></i> Hạn nộp hồ sơ : <span id="job-apply-end-date">${moment(data.applyEndDate).format("DD/MM/YYYY")}</span></span>`)
                }
                var contentFieldOfCompany = "";
                if (data.fieldOfCompany != null) {
                    data.fieldOfCompany.forEach(function (item) {
                        contentFieldOfCompany += `<span class="badge bg-secondary badge-other me-2 mb-2">${item.fieldOfActivityName}</span>`;
                    })
                }
                //$("#company-field-of-activity").html(contentFieldOfCompany != "" ? contentFieldOfCompany : "");
                if (contentFieldOfCompany != "") {
                    $("#company-field-of-activity").html(contentFieldOfCompany);
                }
                else {
                    $("#div-company-field-of-activity").addClass('d-none')
                }
                $("#company-size").text(data.companySizeName);
                $("#company-founded-in").text(data.companyFoundedIn)
                $("#company-phone").text(data.companyPhone)
                $("#company-email").text(data.companyEmailAddress)
                $("#company-address").text(data.companyAddressDetail)
                $("#company-website").html(`<a href="${data.companyWebsite}">${data.companyWebsite}</a>`)

                $("#btn-save-job").attr('data-job-name', data.name)
                $("#btn-save-job").attr('data-job-id', jobId)

                if (data.isSaveJob) {
                    $("#is-in-save-job-list").removeClass("in-wishlist");
                    $("#is-in-save-job-list").addClass("in-wishlist-2");
                    $("#btn-save-job").css("border", "1px solid #e4eba3");
                }
                else {
                    $("#is-in-save-job-list").removeClass("in-wishlist-2");
                    $("#is-in-save-job-list").addClass("in-wishlist");
                    $("#btn-save-job").css("border", "1px solid black");

                }

                if (data.isApplyJob) {
                    $("#job-header").append(`<p class='mt-2'>Bạn đã gửi CV cho vị trí này!</p>`)
                    $("#apply-job").html(`<i class="fa fa-repeat me-2" aria-hidden="true"></i>Ứng tuyển lại`);
                    $("#apply-job-2").html(`<i class="fa fa-repeat me-2" aria-hidden="true"></i>Ứng tuyển lại`);
                }
                else {
                    $("#apply-job").html(`<i class="fa fa-paper-plane me-2" aria-hidden="true"></i>Ứng tuyển ngay`);
                    $("#apply-job-2").html(`<i class="fa fa-paper-plane me-2" aria-hidden="true"></i>Ứng tuyển ngay`);

                }
                $("#company-detail").attr('href', "/thong-tin-cong-ty/" + data.companyId)
                //loadJobReference(data.jobCategoryId, data.jobTypeId);
            }
        },
        error: function (e) {
            //console.log(e);
        }
    });
}
function timeAgo(input) {
    const date = (input instanceof Date) ? input : new Date(input);
    const formatter = new Intl.RelativeTimeFormat('vi');
    const ranges = {
        years: 3600 * 24 * 365,
        months: 3600 * 24 * 30,
        weeks: 3600 * 24 * 7,
        days: 3600 * 24,
        hours: 3600,
        minutes: 60,
        seconds: 1
    };
    const secondsElapsed = (date.getTime() - Date.now()) / 1000;
    for (let key in ranges) {
        if (ranges[key] < Math.abs(secondsElapsed)) {
            const delta = secondsElapsed / ranges[key];
            return formatter.format(Math.round(delta), key);
        }
    }
}

function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}


$(document).ready(async function () {
    await loadJobReference();
    await loadInterestedCompany();
    loadDataDetailJob();
    $("#dropdown-facebook").click(function () {
        window.open("https://www.facebook.com/sharer/sharer.php?u=https://jobi.dion.vn/chi-tiet-cong-viec?jobId=" + jobId, 'window', 'toolbar=no, menubar=no, resizable=yes');
    })

    $("#dropdown-twitter").click(function () {
        window.open("https://twitter.com/share?url=" + encodeURIComponent(window.location.href) + "&text=" + document.title, 'window', 'menubar=no,toolbar=no,resizable=yes')
    })

    $("#dropdown-linked-in").click(function () {
        window.open("https://www.linkedin.com/sharing/share-offsite/?url=https://jobi.dion.vn/chi-tiet-cong-viec?jobId=" + jobId, 'window', 'menubar=no,toolbar=no,resizable=yes')
    })
    $("#btn-save-job").click(function () {
        if (localStorage.currentUser) {
            //let jobId = $(this).attr("data-job-id");
            let jobName = $(this).attr("data-job-name");
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/candidate-save-job/save-job/" + jobId,
                type: "Post",
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    if (localStorage.currentUser) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
                    }
                },
                success: function (responseData) {
                    //$("[data-job-id=" + jobId + "] i").unbind('mouseover');
                    if (responseData.isSucceeded) {
                        if (responseData.message === "Deleted") {
                            //$("[data-job-id=" + jobId + "] i").removeClass('text-primary');
                            //$("[data-job-id=" + jobId + "] i").addClass('text-secondary');
                            //$("[data-job-id=" + jobId + "] i").removeClass('fa-heart');
                            //$("[data-job-id=" + jobId + "] i").addClass('fa-heart-o');
                            //$(this).toggleClass('icon-wishlist');
                            //$('[data-job-id=' + jobId + '] div').toggleClass('icon-wishlist');
                            //$('[data-job-id=' + jobId + '] div').addClass('icon-wishlist');
                            //$('[data-job-id=' + jobId + '] div').removeClass('in-wishlist-2');
                            //$('[data-job-id=' + jobId + '] div').removeClass('in-wishlist');
                            $("#is-in-save-job-list").removeClass("in-wishlist-2");
                            $("#is-in-save-job-list").addClass("in-wishlist");
                            $("#btn-save-job").css("border", "1px solid black");

                            //Toast1.fire({
                            //    icon: 'success',
                            //    title: 'Xóa khỏi tin tuyển dụng đã lưu thành công',
                            //    /*html: '<span>Tin tuyển dụng <strong>' + jobName + '</strong> đã được xóa khỏi danh sách yêu thích</span>'*/
                            //})

                        }
                        else {
                            //$("[data-job-id=" + jobId + "] i").removeClass('text-secondary');
                            //$("[data-job-id=" + jobId + "] i").addClass('text-primary');
                            //$("[data-job-id=" + jobId + "] i").removeClass('fa-heart-o');
                            //$("[data-job-id=" + jobId + "] i").addClass('fa-heart');
                            //$('[data-job-id=' + jobId + '] div').toggleClass('in-wishlist');
                            //$('[data-job-id=' + jobId + '] div').addClass('in-wishlist-2');
                            //$('[data-job-id=' + jobId + '] div').addClass('in-wishlist');
                            $("#is-in-save-job-list").removeClass("in-wishlist");
                            $("#is-in-save-job-list").addClass("in-wishlist-2");
                            $("#btn-save-job").css("border", "1px solid #e4eba3");
                            //swal.fire({
                            //    position: 'top-end',
                            //    icon: 'success',
                            //    html: '<div><label class="fs-14 fw-bold">Tin tuyển dụng</label><br/><strong class="color-success">"' + jobName + '"</strong><br/>đã được lưu thành công!</div>',
                            //    showConfirmButton: false,
                            //    timer: 1800
                            //}).then(function () {
                            //    //$(".rbt-review").click();
                            //    //$("[data-course-id="+courseId+"] i").addClass('text-primary');
                            //    //$('[data-course-id="+courseId+"] i').unbind();
                            //});

                            //Toast1.fire({
                            //    icon: 'success',
                            //    title: 'Lưu tin tuyển dụng thành công',
                            //    /*html: '<span>Tin tuyển dụng <strong>' + jobName + '</strong> đã được thêm vào danh sách yêu thích</span>'*/
                            //})
                        }
                    }
                },
                error: function (res) {
                    if (res.status == 401) {
                        swal.fire({
                            icon: 'warning',
                            title: 'Lưu ý',
                            html: 'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại!',
                            focusConfirm: true,
                            allowEnterKey: true
                        });
                    }
                }
            })
        } else {
            swal.fire({
                icon: 'warning',
                title: 'Lưu ý',
                html: 'Bạn chưa đăng nhập, vui lòng đăng nhập để sử dụng tính năng này!',
                focusConfirm: true,
                allowEnterKey: true
            });
        }
    })

    $(".apply-job-btn").click(async function () {

        await GetCVPDF();
        $("#apply-job-modal").modal('show');
    })

})

async function GetCVPDF() {
    try {
        let res = await httpService.getAsync(`api/candidate-cv-pdf/list-by-candidate-id`);
        if (res.isSucceeded) {
            if (res.status == 200) {
                //console.log(res)
                LoadDataCVPDF(res.resources)
            }
        }
    }
    catch (e) {
        //console.log(e)
        //swal.fire({
        //    icon: 'warning',
        //    title: 'Lấy dữ liệu thất bại',
        //    html: 'Có lỗi xảy ra khi lấy danh sách CV PDF đã lưu, vui lòng thử lại',
        //    showCloseButton: false,
        //    focusConfirm: true,
        //    confirmButtonText: 'Ok',
        //})
    }
}

var dataCVDefault = [];
var dataCVUpload = [];
function LoadDataCVPDF(data) {
    var content = "";
    var contentUpload = "";
    $("#body-list-cv").html("");
    if (data != null) {
        dataCVDefault = data.filter(x => x.candidateCVPDFTypeId == 1001);
        dataCVUpload = data.filter(x => x.candidateCVPDFTypeId == 1002);

        dataCVDefault.forEach(function (item) {
            content += `<div class="form-check">
                                <input class="form-check-input me-2" type="radio" name="radio-cv" id="${item.id}" value="${item.id}">
                                <label class="form-check-label me-2" for="${item.id}">
                                    ${item.url.split('/').pop()}
                                </label> <a href="${systemConfig.defaultStorageURL + item.url}" target="_blank">(Xem)</a>
                            </div>`
        })

        dataCVUpload.forEach(function (item) {
            contentUpload += `<div class="form-check">
                                <input class="form-check-input me-2" type="radio" name="radio-cv" id="${item.id}" value="${item.id}">
                                <label class="form-check-label me-2" for="${item.id}">
                                    ${item.url.split('/').pop()}
                                </label> <a href="${systemConfig.defaultStorageURL + item.url}" target="_blank">(Xem)</a>
                            </div>`
        })

    }
    else {
        content = "<span>Bạn chưa có CV nào, tạo CV ngay</span>"
    }
    //$("#body-list-cv").html(content);
    if (contentUpload != "") {
        $("#body-list-cv-upload").html(contentUpload);
        $("#upload-cv-div").removeClass('d-none')
    }
    else {
        $("#upload-cv-div").addClass('d-none')

    }

    if (content != "") {
        $("#body-list-cv").html(content);
        $("#default-cv-div").removeClass('d-none')
    }
    else {
        $("#default-cv-div").addClass('d-none')

    }
}

$("#send-cv").click(async function () {
    try {
        if ($("input[name='radio-cv']:checked").val() == undefined) {
            swal.fire({
                title: 'Ứng tuyển<br><span class="swal__admin__subtitle"> Ứng tuyển không thành công </span>',
                html: '<p class="text-center">Bạn cần chọn CV trước </p>',
                icon: 'warning'
            })
        }
        else {
            var obj = {
                "CandidateCVPDFId": $("input[name='radio-cv']:checked").val(),
                "JobId": jobId,
                "CandidateApplyJobStatusId": applyStatus,
                "CandidateApplyJobSourceId": applySource,
                "Description": "",
            }
            let res = await httpService.postAsync(`api/candidate-apply-job/add`, obj);
            if (res.isSucceeded) {

                swal.fire({
                    title: 'Ứng tuyển<br><span class="swal__admin__subtitle"> Ứng tuyển thành công </span>',
                    html: '<p class="text-center">Hãy ghi nhớ tên <strong> các công ty </strong> và <strong>việc làm</strong> bạn đã ứng tuyển để chuẩn bị tốt nhất cho một cuộc gọi hoặc email từ Nhà tuyển dụng trong một vài ngày tới, nếu hồ sơ của bạn đạt yêu cầu.</p>',
                    icon: 'success'
                }).then(function () {
                    window.location.reload();
                })
            }
            else {
                if (res.status == 400) {
                    if (res.errors != null) {
                        var contentError = "<ul>";
                        res.errors.forEach(function (item, index) {
                            contentError += "<li class='text-start pb-2'>" + item + "</li>";
                        })
                        contentError += "</ul>";
                        swal.fire(
                            'Ứng tuyển <br><span class="swal__admin__subtitle"> Ứng tuyển không thành công </span>',
                            contentError,
                            'warning'
                        );
                    } else {
                        swal.fire(
                            'Ứng tuyển',
                            res.errors,
                            'warning'
                        )
                    }
                }
                else {
                    swal.fire(
                        'Lưu ý',
                        res.errors,
                        'warning'
                    )
                }
            }
        }

    }
    catch (e) {
        if (e.status === 401) {
            swal.fire(
                'Ứng tuyển',
                'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                'error'
            )
        }

        else {
            swal.fire(
                'Ứng tuyển',
                'Ứng tuyển không thành công, <br> vui lòng thử lại sau!',
                'error'
            );
        }
    }
})


$("#upload-cv").click(function () {
    $("#upload-cv-file").click();
})

$("#upload-cv-file").change(function (e) {
    const maximumFileSize = 1024 * 1024 * 25;
    if ($(this).val().length > 0) {
        var files = e.target.files;

        if (files.length > 0) {

            if (files[0].size > maximumFileSize) {
                Swal.fire({
                    title: 'Lưu ý',
                    icon: 'warning',
                    html: "CV " + files[0].name + " bạn vừa tải có kích thước quá lớn, hãy chọn tệp khác và thử lại",
                    focusConfirm: true,
                    allowEnterKey: true
                })
                $("#upload-cv-file").val('');
            }
            else {

                uploadFile(files[0]);
            }

        }
    }
})

async function uploadFile(file) {
    try {
        var url = "api/file-explorer/upload/candidates/cvs";
        var formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            //console.log(response.resources[0].path)
            addCVtoCandidateCVPDF(response.resources[0].path);
        }
    } catch (res) {

    }
}

async function addCVtoCandidateCVPDF(filePath) {
    try {
        let obj = {
            url: filePath,
            candidateCVPDFTypeId: 1002,
        }
        let url = "api/candidate-cv-pdf/add-cv";
        let res = await httpService.postAsync(url, obj);
        if (res.isSucceeded) {
            Toast1.fire({
                icon: 'success',
                title: 'Tải lên CV thành công'
            })
            GetCVPDF();

            //console.log(res)
        }
    } catch (e) {

    }
}

const Toast1 = Swal.mixin({
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

async function loadJobReference() {
    try {

        //let url = "api/job/list-job-reference/" + categoryId + "/" + typeId;
        let url = "api/job-reference/list-job-reference-on-detail-job/" + jobId;
        let res = await httpService.getAsync(url);
        if (res.isSucceeded) {
            var data = res.resources;
            if (data.length > 0) {
                let content = ""
                data.forEach(function (item) {
                    //hiển thị tag city
                    let contentCity = "";
                    if (item.jobRequireCity.length > 1) {
                        let contentTitle = "";
                        for (let x = 0; x < 1; x++) {
                            contentCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName}</a>`
                        }
                        for (let y = 1; y < item.jobRequireCity.length; y++) {
                            contentTitle += item.jobRequireCity[y].cityName + ', ';
                        }
                        contentCity += `<a class='badge-city' title="${contentTitle.substring(0, contentTitle.length - 2)}">+${item.jobRequireCity.length - 1}</a>`
                    }
                    else {
                        for (let x = 0; x < item.jobRequireCity.length; x++) {
                            contentCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName}</a>`
                        }
                    }
                    //generate tên dạng slug
                    let slugJob = stringToSlug(item.jobName);

                    content += `<div class="col-6"><div class="card mb-2 hover-card-item card-border-item">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item m-10px img-cover-fit" src="${systemConfig.defaultStorageURL + item.companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between  m-10px">
                                    <a class="job-name" href="/chi-tiet-cong-viec/${slugJob + "-" + item.jobId}" title='${item.jobName}'>${item.jobName}</a>
                                    <span></span>
                                </div>
                                <div class="d-flex  m-10px">
                                 <a href="/thong-tin-cong-ty/${item.companyId}" class="job-company" title="${item.companyName}">${item.companyName}</a>
                                 </div>
                                <div class="d-flex flex-column  m-10px">
                                   <span class='text-color-money fw-bold salary-job'>${item.salaryTypeId == salaryFrom ? (item.salaryFrom != null ? "Từ " + formatNumber(item.salaryFrom.toString()) : 0) : (item.salaryTypeId == salaryTo ? (item.salaryTo != null ? "Đến " + formatNumber(item.salaryTo.toString()) : 0) : (item.salaryTypeId == salaryBetween ? formatNumber(item.salaryFrom.toString()) + " - " + formatNumber(item.salaryTo.toString()) : "Thỏa thuận"))}</span>
                                    <span>${contentCity}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>`;
                })
                $("#job-reference").html(content);
            }
            else {
                $("#div-reference-job").addClass('d-none')
            }
        }
    } catch (e) {
        console.error(e)
    }
}


async function loadInterestedCompany() {
    try {
        let url = "api/must-interesterd-company/list-interested-company-job-detail";
        let res = await httpService.getAsync(url);
        if (res.isSucceeded) {
            var data = res.resources;
            if (data.length > 0) {

                let content = ""
                data.forEach(function (item) {
                    //generate tên dạng slug
                    let slugJob = stringToSlug(item.companyName);

                    //field of company
                    let contentFieldOfCompany = "";
                    if (item.companyFields.length > 1) {
                        let contentTitle = "";
                        for (let x = 0; x < 1; x++) {
                            contentFieldOfCompany += `<a class='badge-city'>${item.companyFields[x].fieldOfActivityName}</a>`
                        }
                        for (let y = 1; y < item.companyFields.length; y++) {
                            contentTitle += item.companyFields[y].fieldOfActivityName + ', ';
                        }
                        contentFieldOfCompany += `<a class='badge-city' title="${contentTitle.substring(0, contentTitle.length - 2)}">+${item.companyFields.length - 1}</a>`
                    }
                    else {
                        for (let x = 0; x < item.companyFields.length; x++) {
                            contentFieldOfCompany += `<a class='badge-city'>${item.companyFields[x].fieldOfActivityName}</a>`
                        }
                    }
                    content += `<div class="col-6"><div class="card mb-2 hover-card-item card-border-item">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item m-10px img-cover-fit" src="${systemConfig.defaultStorageURL + item.companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between m-10px">
                                    <a class="job-name" href="/thong-tin-cong-ty/${item.companyId}" title='${item.companyName}'>${item.companyName}</a>
                                    <span></span>
                                </div>
                               <div class="d-flex justify-content-between m-10px">
                                    <span class="text-ellipsis" title="${item.countJob} việc làm"><span class="fa fa-briefcase me-1"></span>${item.countJob} việc làm</span>
                                    <span class="text-ellipsis" title="${item.countFollower} việc làm"><span class="fa fa-users me-1"></span>${item.countFollower} người theo dõi</span>
                               </div>

                               <div class=" m-10px">
                               ${contentFieldOfCompany}
                               </div>
                            </div>
                        </div>
                    </div>
                    </div>`;
                })
                $("#company-reference").html(content);
            }
            else {
                $("#div-reference-company").addClass('d-none')
            }
        }
    } catch (e) {
        console.error(e)
    }
}