var updatingCandidateId = 0;
$(document).ready(function () {
    //$("#selectGender").select2({
    //	placeholder: 'Giới tính',
    //	allowClear: true,
    //	//templateResult: formatRepo,
    //	//templateSelection: formatRepoSelection,
    //	//data: recruitmentCampaignSource,
    //	width: "165px"
    //});

    //$("#selectWorkExperience").select2({
    //	placeholder: 'Kinh nghiệm làm việc',
    //	allowClear: true,
    //	width: "165px"
    //});

    loadDataExperienceRange();
    loadDataJobCategory();
    loadDataSalaryRange();
    loadDataWorkPlace();
    loadDataSkillLevel();
    loadDataJobPosition();
    loadDataJobSkill();
    //loadDataCandidateToTable();
    loadDataWallet();
    reGenTableSearcCV();
    $(".advanced-search-form").on("change", function () {
        if ($(this).val().length != 0) {
            $(this).attr('is-count', true)
        }
        else {
            $(this).attr('is-count', false)

        }
        let index = $(".advanced-search-form[is-count='true']").length;
        if (index > 0) {
            $("#advanced-search-text").text(`Lọc nâng cao (${index})`)
        }
        else {
            $("#advanced-search-text").text(`Lọc nâng cao`)

        }
        reGenTableSearcCV();
    })


    $("#advanced-candidate-skill").on("keypress keyup keydown", function () {
        if ($(this).val().length != 0) {
            $("#advanced-skill-level").parent().removeClass('d-none')
        }
        else {
            $("#advanced-skill-level").parent().addClass('d-none')
            $("#advanced-skill-level").val('').trigger('change');
        }
    })


    //filter 
    $("#fillter_unread").on('change', function () {
        reGenTableSearcCV();
    })

    //search all
    $("#search_all").on('keypress', function (e) {
        if (e.keyCode === 13) {
            reGenTableSearcCV();
        }
    })

    //submit modal report
    $("#submit-form-report").click(async function () {
        await reportCVCandidate(updatingCandidateId);
    })
});

//load data experience
var dataExperienceRange = [];
async function loadDataExperienceRange() {
    try {
        let result = await httpService.getAsync('api/experience-range/list');
        if (result.isSucceeded) {
            dataExperienceRange = result.resources
            result.resources.forEach(function (item) {
                $('#advanced-experience-range').append(new Option(item.name, item.id, false, false))
            })

            $('#advanced-experience-range').select2({
                allowClear: true,
                placeholder: 'Kinh nghiệm làm việc',
                dropdownCssClass: "no-border",
                containerCssClass: "error"
            })
        }


    } catch (e) {
        console.error(e)
    }
}

var dataJobCategory = [];
//load data job category
async function loadDataJobCategory() {
    try {
        let result = await httpService.getAsync('api/job-category/list');
        if (result.isSucceeded) {
            dataJobCategory = result.resources;
            result.resources.forEach(function (item) {
                $('#advanced-suggest-job-category').append(new Option(item.name, item.id, false, false))
            })

            $('#advanced-suggest-job-category').select2({
                allowClear: true,
                placeholder: 'Ngành nghề quan tâm',
                dropdownCssClass: "no-border"

            })
        }


    } catch (e) {
        console.error(e)
    }
}

var dataJobPosition = [];
//load data position
async function loadDataJobPosition() {
    try {
        let result = await httpService.getAsync('api/job-position/list');
        if (result.isSucceeded) {
            dataJobPosition = result.resources;
            result.resources.forEach(function (item) {
                $('#advanced-position').append(new Option(item.name, item.id, false, false))
            })

            $('#advanced-position').select2({
                allowClear: true,
                placeholder: 'Vị trí',
                dropdownCssClass: "no-border"

            })
        }


    } catch (e) {
        console.error(e)
    }
}

var dataJobSkill = [];
//load data job skill
async function loadDataJobSkill() {
    try {
        let result = await httpService.getAsync('api/job-skill/list');
        if (result.isSucceeded) {
            dataJobSkill = result.resources;
            result.resources.forEach(function (item) {
                $('#advanced-skill').append(new Option(item.name, item.id, false, false))
            })

            $('#advanced-skill').select2({
                allowClear: true,
                placeholder: 'Kỹ năng công việc',
                dropdownCssClass: "no-border"

            })
        }


    } catch (e) {
        console.error(e)
    }
}

var dataWorkplace = [];
//load data work place
async function loadDataWorkPlace() {
    try {
        let res = await httpService.getAsync('api/workplace/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-job-city').append(new Option("Tất cả vị trí", 0, false, false)).trigger('change');
            dataWorkplace = res.resources
            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#advanced-job-city').append(new Option(item.name, item.id, false, false));

            })

            $("#advanced-job-city").select2({
                placeholder: "Địa điểm làm việc",
                allowClear: true,
                dropdownCssClass: "no-border"

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}

var dataSalaryRange = [];
//load data salary range
async function loadDataSalaryRange() {
    try {
        let res = await httpService.getAsync('api/salary-range/list');
        if (res.isSucceeded) {
            dataSalaryRange = res.resources;
            res.resources.forEach(function (item, index) {
                $('#advanced-salary-range').append(new Option(item.name, item.id, false, false));
            })

            $("#advanced-salary-range").select2({
                placeholder: "Mức lương",
                allowClear: true,
                dropdownCssClass: "no-border"

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}
var dataSkillLevel = []
//load data skill level
async function loadDataSkillLevel() {
    try {
        let res = await httpService.getAsync('api/skill-level/list');
        if (res.isSucceeded) {
            dataSkillLevel = res.resources
            res.resources.forEach(function (item, index) {
                $('#advanced-skill-level').append(new Option(item.name, item.id, false, false));
            })

            $("#advanced-skill-level").select2({
                placeholder: "Mức độ",
                allowClear: true,
                dropdownCssClass: "no-border"

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}


//load data table candidate
var dataSourceTable = [];
function loadDataCandidateToTable() {
    tableFindCV = $("#table_candidate").DataTable({
        language: systemConfig.languageDataTable,
        processing: true,
        serverSide: true,
        autoWidth: false,
        paging: true,
        searching: { regex: true },
        ajax: {
            url: systemConfig.defaultAPI_URL + "api/candidate/list-candidate-find-cv",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            beforeSend: function (xhr) {
                if (localStorage.token) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
                }
            },
            data: function (d) {
                d.searchCategory = $("#advanced-suggest-job-category").val().toString();
                d.searchExperience = $("#advanced-experience-range").val().toString();
                d.searchPosition = $("#advanced-position").val().toString();
                d.searchSuggestSkill = $("#advanced-skill").val().toString();
                d.searchCity = $("#advanced-job-city").val().toString();
                d.searchSalaryRange = $("#advanced-salary-range").val().toString();
                d.searchEducation = $("#advanced-education").val().toString();
                d.searchCertificate = $("#advanced-certificate").val().toString();
                d.searchCandidateSkill = $("#advanced-candidate-skill").val().toString();
                d.searchCandidateSkillLevel = $("#advanced-skill-level").val().toString();
                d.filterCV = $("#fillter_unread").val();
                d.search.value = $("#search_all").val();
                return JSON.stringify(d);
            },
        },
        columns: [
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "info",
                render: function (data, type, row, meta) {
                    var content = "";
                    content +=
                        `<div class="d-flex">
							<div class="pxp-company-dashboard-candidate-avatar pxp-cover bg-secondary" style="background-image: url(${row.photo != "" && row.photo != null ? systemConfig.defaultStorage_URL + row.photo : systemConfig.defaultStorage_URL + "/uploads/candidates/avatars/avatar.jpg"});">
							</div>	
							<div class="d-flex flex-column font-size-table ">
								<span class="fw-bold mb-1">${row.fullName} ${row.gender == 1 ? '<i class="fa-solid fa-mars text-primary"></i>' : (row.gender == 2 ? '<i class="fa-solid fa-venus text-danger"></i>' : "")} ${row.isUsedCredit ? (row.isRefundRequest ? '<span class="badge bg-warning ms-1">Y/c hoàn Credit</span>' : '<span class="badge bg-primary ms-1">Đã xem</span>') : ""}</span>
								<span>${row.doB != null && row.doB != ""? '<i class="fa-solid fa-calendar me-1"></i>' + moment(row.doB).format('DD/MM/YYYY') : ""}</span>
								<span>${row.maritalStatus != null && row.maritalStatus != "" ? '<i class="fa-solid fa-ring me-1"></i>' + row.maritalStatus : ""}</span>
								<span>${row.addressDetail != null && row.addressDetail != "" ? '<i class="fa-solid fa-earth-asia me-1"></i>' + row.addressDetail : ""}</span>
                                
							</div>
 					
					</div>`
                    return content;
                }
            },
            {
                data: "experience",
                render: function (data, type, row, meta) {
                    return `<div class="d-flex flex-column font-size-table ">
						<span>${row.jobPosition != null ? row.jobPosition : ""}</span>
						<span>${row.suggestionExperienceRangeName}</span>
					</div>`;
                }
            },

            {
                data: "suggestion",
                render: function (data, type, row, meta) {
                    var contentCategory = "";
                    if (row.listCandidateSuggestionJobCategory.length > 0) {
                        row.listCandidateSuggestionJobCategory.forEach(function (item) {
                            let category = dataJobCategory.find(x => x.id == item);
                            if (category != undefined) {
                                contentCategory += category.name + ', ';
                            }
                        })
                    }


                    var contentPosition = "";
                    if (row.listCandidateSuggestionJobPosition.length > 0) {
                        row.listCandidateSuggestionJobPosition.forEach(function (item) {
                            let jobPosition = dataJobPosition.find(x => x.id == item);
                            if (jobPosition != undefined) {
                                contentPosition += jobPosition.name + ', ';
                            }
                        })
                    }




                    var contentWorkplace = "";
                    if (row.listCandidateSuggestionJobWorkplace.length > 0) {
                        row.listCandidateSuggestionJobWorkplace.forEach(function (item) {
                            let workplace = dataWorkplace.find(x => x.id == item);
                            if (workplace != undefined) {
                                contentWorkplace += workplace.name + ', ';

                            }
                        })
                    }
                    return `
					<div class="d-flex flex-column font-size-table ">
						<span>Mức lương: <span class="text-color-money fw-bold">${row.suggestionSalaryRangeName}</span></span>
						<span>${contentCategory != "" ? 'Ngành nghề: ' + contentCategory.substring(0, contentCategory.length - 2) : ""} </span>
						<span>${contentCategory != "" ? 'Địa điểm: ' + contentWorkplace.substring(0, contentWorkplace.length - 2) : ""}</span>
						<span>${contentPosition != "" ? 'Vị trí: ' + contentPosition.substring(0, contentPosition.length - 2) : ""} </span>

					</div>
					`;
                }
            },


            {
                data: "certificate",
                render: function (data, type, row, meta) {
                    var contentCertificate = "";
                    if (row.listCandidateCertificate.length > 0) {
                        row.listCandidateCertificate.forEach(function (item) {
                            contentCertificate += `<span>${item}</span>`
                        })
                    }
                    return `
					<div class="d-flex flex-column font-size-table "> 
						<span>${contentCertificate}</span>
					</div>

					`;
                }
            },

            {
                data: "id",
                render: function (data, type, row, meta) {
                    //console.log(row)
                    return `<div class="candidate_action dropdown text-center font-size-table ">
                                <button class="dropdown-toggle btn_icon" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><span class="fa fa-ellipsis-h"></span></button>
                                <ul class="dropdown-menu list-unstyled shadow ">
                                    <li><a class="dropdown-item btn_detail_candidate" data-id=${data} href="#!" title="Xem chi tiết ứng viên">Chi tiết ứng viên</a></li>
                                    <li class="${row.isRefundRequest || !row.isUsedCredit ? "d-none" : ""}"><a class="dropdown-item btn_report" href="#!" data-id=${data} title="Báo cáo">Báo cáo</a></li>
                                </ul>
                            </div>`;
                }
            }
        ],
        columnDefs: [
            { targets: [0, 1, 2, 3, 4, -1], orderable: false },
        ],

        drawCallback: function () {
            $("#rowSearch").removeClass("d-none");
            innitTblFindCV("#candidate_paging");
            dataSourceTable = tableFindCV.ajax.json().data;

        }

    });

    $(document).on("click", ".page-link-find-cv", function (e) {
        if ($(this).hasClass("active")) {
            return;
        }
        let pageIndex = $(this).attr("data-index");
        if (pageIndex) {
            tableFindCV.page(parseInt(pageIndex)).draw('page');
        }
    })



    //sự kiện khi bấm thao tác
    $(document).on('click', '.btn_detail_candidate', async function () {
        //console.log($(this).attr('data-id'))
        let candidateId = $(this).attr('data-id');
        let candidateDetail = dataSourceTable.find(x => x.id == candidateId);

        //kiểm tra xem đã sử dụng dịch vụ hay chưa
        if (!candidateDetail.isUsedCredit) {

            swal.fire({
                title: "Xem chi tiết ứng viên",
                html: "Bạn cần sử dụng <b>" + candidateDetail.countCredit + " Credit </b> để xem thông tin chi tiết ứng viên <b>" + candidateDetail.fullName + '</b>?',
                icon: 'info',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Hủy',
                confirmButtonText: 'Đồng ý'
            }).then(async(result) => {
                if (result.isConfirmed) {
                    //Nếu số dư lơn hơn số Credit cần trả
                    if (creditBalance >= candidateDetail.countCredit) {
                        try {
                            var obj = {
                                amount: candidateDetail.countCredit,
                                employerWalletId: employerWalletId,
                                walletHistoryTypeId: 1001,
                                name: "Tìm kiếm CV bằng Credit",
                                candidateId: candidateId,
                            }
                            let result = await httpService.postAsync('api/employer-wallet-history/add', obj);
                            if (result.isSucceeded) {
                                window.open('chi-tiet-cv-ung-vien/' + candidateId);
                            }
                        } catch (e) {
                            console.error(e)
                        }
                    }
                    //Nếu số dư nhỏ hơn số credit cần trả
                    else {
                        swal.fire({
                            title: "Xem chi tiết ứng viên",
                            html: "Bạn không đủ điểm Credit để xem ứng viên này",
                            icon: 'info',
                        })
                    }
                }
            })
        }

        //Nếu đã sử dụng thì sang trang mới luôn
        else {
            window.open('chi-tiet-cv-ung-vien/' + candidateId);
        }

    })

    $(document).on('click', '.btn_report', async function () {
        updatingCandidateId = $(this).attr('data-id');
        $("#modal_report").modal('show');
        //let candidateId = $(this).attr('data-id');
        
    })

}

function innitTblFindCV(element) {
    let info = tableFindCV.page.info();
    let totalPage = info.pages;
    let pageIndex = info.page;
    if (totalPage > 0) {
        let html = "";
        let startPage;
        if (totalPage <= 3) {
            startPage = 1;
        }
        else {
            if (totalPage == pageIndex) {
                startPage = totalPage - 2;
            }
            else {
                startPage = pageIndex == 0 ? 1 : pageIndex;
            }

        }
        let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
        if (pageIndex >= 1) {
            html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-find-cv page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-find-cv page-link"><i class="fa fa-angle-left"></i></a></li>`;
        }
        for (var i = startPage; i <= endPage; i++) {
            if (i > 0) {
                html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-find-cv ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
            }
        }
        if (pageIndex < totalPage - 1) {
            html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-find-cv page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-find-cv page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
        }
        $(element).html(html);
    }
    else {
        $(element).html("");
    }
}
var tableFindCV;
function reGenTableSearcCV() {
    if (tableFindCV != undefined) {
        tableFindCV.destroy();
    }
    $("#table_candidate tbody").html('');
    loadDataCandidateToTable();
}

//lấy thông tin wallet
var creditBalance = 0;
var employerWalletId = 0;
async function loadDataWallet() {
    try {
        let result = await httpService.getAsync('api/employer-wallet/detail-credit-wallet');
        if (result.isSucceeded) {
            creditBalance = result.resources.value;
            employerWalletId = result.resources.id;
            $("#info-wallet").text("Ví: " + creditBalance + " Credit")
        }
    } catch (e) {
        console.error(e)
    }
}

async function reportCVCandidate(candidateID) {
    try {
        let obj = {
            candidateId: candidateID,
            description: $("#description").val(),
        }
        let result = await httpService.putAsync("api/employer-wallet-history/report-cv-candidate", obj);
        if (result.isSucceeded) {
            swal.fire({
                title: "Yêu cầu hoàn Credit",
                html: "Bạn đã yêu cầu hoàn Credit thành công, hãy đợi quản trị viên kiểm tra và xác nhận",
                icon: 'success',
            })
            $("#modal_report").modal('hide');
            reGenTableSearcCV();
        }
    } catch (e) {
        console.error(e);
    }
}