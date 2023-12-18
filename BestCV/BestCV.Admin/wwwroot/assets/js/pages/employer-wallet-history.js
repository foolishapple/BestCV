

var iconeye = '<svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 576 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><style>svg{fill:#04a6ec}</style><path d="M288 32c-80.8 0-145.5 36.8-192.6 80.6C48.6 156 17.3 208 2.5 243.7c-3.3 7.9-3.3 16.7 0 24.6C17.3 304 48.6 356 95.4 399.4C142.5 443.2 207.2 480 288 480s145.5-36.8 192.6-80.6c46.8-43.5 78.1-95.4 93-131.1c3.3-7.9 3.3-16.7 0-24.6c-14.9-35.7-46.2-87.7-93-131.1C433.5 68.8 368.8 32 288 32zM144 256a144 144 0 1 1 288 0 144 144 0 1 1 -288 0zm144-64c0 35.3-28.7 64-64 64c-7.1 0-13.9-1.2-20.3-3.3c-5.5-1.8-11.9 1.6-11.7 7.4c.3 6.9 1.3 13.8 3.2 20.7c13.7 51.2 66.4 81.6 117.6 67.9s81.6-66.4 67.9-117.6c-11.1-41.5-47.8-69.4-88.6-71.1c-5.8-.2-9.2 6.1-7.4 11.7c2.1 6.4 3.3 13.2 3.3 20.3z"/></svg>'
$(document).ready( function () {

    loadDataCandidateLevel();
    loadData();
    loadDataAccountStatus();
    loadDataExperienceRange();
    loadDataSalaryRange();
    loadDataJobCategory();
    loadDataJobPosition();
    loadDataJobSkill();
    loadDataWorkPlace();
    loadDataSkillLevel();

    $("#tableData").on("click", ".btn-admin-edit", function () {
        var id = parseInt($(this).attr("data-idItem"));
        getItemById(id);
    })

    $("#tableData").on("click", ".btn-admin-edit_1", function () {
        var id = parseInt($(this).attr("data-idItem"));
        editItem(id);
    })
    $("#table_search_all").on('keyup', function (e) {
        if (e.keyCode == 13) {
            table.search($(this).val()).draw();
        }
    });

    $("#btnTableSearch").click(function () {
        tableSearch();
    });

    $("#tableData thead:nth-child(2)").find("input").keypress(function (e) {
        let key = e.which;
        if (key == 13) {
            $("#btnTableSearch").click();
        }
    })

   
   

   

    
    $("#searchIsApproved").val("").trigger('change')
    $("#searchIsApproved").select2({
        placeholder: ""
    });
    $("#tableData tbody").on("click", ".checkIsApproved", function () {
        quickActivated($(this).is(":checked"), $(this).attr("data-id"));
    })
    $("#btnTableResetSearch").click(function () {
        $("#searchEmployerName").val("");
        $("#searchCandidateName").val("");
        $("#searchIsApproved").val("").trigger('change');
        $("#fillter_startDate_value").val("");
        $("#fillter_endDate_value").val("");
        linked1.clear();
        linked2.clear();
        //tableSearch();
        reGenTable();
    });

    const linkedPicker1Element = document.getElementById("fillter_startDate");
    linked1 = new tempusDominus.TempusDominus(linkedPicker1Element, datePickerOption);
    linked2 = new tempusDominus.TempusDominus(document.getElementById("fillter_endDate"), datePickerOption);
    // updateOption
    linked2.updateOptions({
        useCurrent: false,
    });
    //using event listeners
    linkedPicker1Element.addEventListener(tempusDominus.Namespace.events.change, (e) => {
        var minDate = $("#fillter_startDate_value").val() == "" ? undefined : new Date(moment(e.detail.date).add(-1, "d"));
        linked2.updateOptions({
            restrictions: {
                minDate: minDate
            },
        });
    });
    //using subscribe method
    const subscription = linked2.subscribe(tempusDominus.Namespace.events.change, (e) => {
        var maxdate = $("#fillter_endDate_value").val() == "" ? undefined : new Date(moment(e.date).add(1, "d"));
        linked1.updateOptions({
            restrictions: {
                maxDate: maxdate
            },
        });
    });
})
$("#accountPassword").on("keypress", function (e) {
    if (e.keyCode == 13) {

        e.preventDefault();
        changePassword();

    }
});
$("#accountConfirmPassword").on("keypress", function (e) {
    if (e.keyCode == 13) {

        e.preventDefault();
        changePassword();

    }
});


function reGenTableJobSkill() {

    if (tableJobSkill != null) {
        tableJobSkill.destroy();
    }
    $("#tableJobSkill tbody").html('');
    loadTableJobSkill();
}
function loadTableJobSkill() {
    $("#tableJobSkill tbody").html("");
    if (updatingObj != null && updatingObj.listCandidateSkill.length > 0) {
        updatingObj.listCandidateSkill.forEach(function (item, index) {
            var rowContent = "<tr>";
            rowContent += "<td class='column-index' style='text-align: center;'>" + (index + 1) + "</td>";
            rowContent += "<td>" + item.name + "</td>";
            rowContent += "<td>" + (item.description != null ? item.description : "") + "</td>";

            rowContent += "</tr>";
            $(rowContent).appendTo($("#tableJobSkill tbody"));
        })
    }

    initTableJobSkill();
}
function initTableJobSkill() {
    tableJobSkill = $('#tableJobSkill').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        autoWidth: false,

        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },

        ],

        'order': [
            [1, 'asc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableJobSkill tfoot').html("");
            $("#tableJobSkill thead:nth-child(1) tr").clone(true).appendTo("#tableJobSkill tfoot");
        }
    });

    tableJobSkill.on('order.dt search.dt', function () {
        tableJobSkill.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function reGenTableWorkExperience() {
    if (tableWorkExperience != null) {
        tableWorkExperience.destroy();
    }
    $("#tableWorkExperience tbody").html('');
    loadTableWorkExperience();
    initTableWorkExperience();

}
function loadTableWorkExperience() {
    $("#tableWorkExperience tbody").html("");
    if (updatingObj != null && updatingObj.listCandidateWorkExperiences.length > 0) {
        updatingObj.listCandidateWorkExperiences.forEach(function (item, index) {
            var rowContent = "<tr>";
            rowContent += "<td style='text-align: center;'>" + (index + 1) + "</td>";
            rowContent += "<td>" + item.jobTitle + "</td>";
            rowContent += "<td>" + item.company + "</td>";
            rowContent += "<td class='column-dateTime'>" + item.timePeriod + "</td>";

            rowContent += "</tr>";
            $(rowContent).appendTo($("#tableWorkExperience tbody"));
        })
    }

}
function initTableWorkExperience() {
    tableWorkExperience = $('#tableWorkExperience').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        autoWidth: false,

        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },

        ],

        'order': [
            [1, 'asc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableWorkExperience tfoot').html("");
            $("#tableWorkExperience thead:nth-child(1) tr").clone(true).appendTo("#tableWorkExperience tfoot");
        }
    });

    tableWorkExperience.on('order.dt search.dt', function () {
        tableWorkExperience.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function reGenTableEducation() {
    if (tableEducation) {

        tableEducation.destroy();
    }
    $("#tableEducation tbody").html('');
    loadTableEducation();
}
function loadTableEducation() {
    $("#tableEducation tbody").html("");
    if (updatingObj != null && updatingObj.listCandidateEducation.length > 0) {
        updatingObj.listCandidateEducation.forEach(function (item, index) {
            var rowContent = "<tr>";
            rowContent += "<td class='column-index' style='text-align: center;'>" + (index + 1) + "</td>";
            rowContent += "<td>" + item.title + "</td>";
            rowContent += "<td>" + item.school + "</td>";
            rowContent += "<td class='column-dateTime'>" + item.timePeriod + "</td>";
            rowContent += "<td>" + (item.description != null ? item.description : "") + "</td>";

            rowContent += "</tr>";
            $(rowContent).appendTo($("#tableEducation tbody"));
        })
    }

    initTableEducation()
}
function initTableEducation() {
    tableEducation = $('#tableEducation').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        autoWidth: false,

        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },

        ],

        'order': [
            [1, 'asc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableEducation tfoot').html("");
            $("#tableEducation thead:nth-child(1) tr").clone(true).appendTo("#tableEducation tfoot");
        }
    });

    tableEducation.on('order.dt search.dt', function () {
        tableEducation.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function reGenTableCertificate() {
    if (tableCertificate) {

        tableCertificate.destroy();
    }
    $("#tableCertificate tbody").html('');
    loadTableCertificate();
}
function loadTableCertificate() {
    $("#tableCertificate tbody").html("");
    if (updatingObj != null && updatingObj.listCandidateCertificates.length > 0) {
        updatingObj.listCandidateCertificates.forEach(function (item, index) {
            var rowContent = "<tr>";
            rowContent += "<td class='column-index' style='text-align: center;'>" + (index + 1) + "</td>";
            rowContent += "<td>" + item.name + "</td>";
            rowContent += "<td>" + item.issueBy + "</td>";
            rowContent += "<td class='column-dateTime'>" + item.timePeriod + "</td>";
            rowContent += "<td>" + (item.description != null ? item.description : "") + "</td>";

            rowContent += "</tr>";
            $(rowContent).appendTo($("#tableCertificate tbody"));
        })
    }

    initTableCertificate()
}
function initTableCertificate() {
    tableCertificate = $('#tableCertificate').DataTable({
        language: systemConfig.languageDataTable,
        searching: {
            regex: true
        },
        autoWidth: false,

        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: [0], orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },

        ],

        'order': [
            [1, 'asc']
        ],

        aLengthMenu: [
            [10, 25, 50, 100],
            [10, 25, 50, 100]
        ],
        drawCallback: function () {
            $('#tableCertificate tfoot').html("");
            $("#tableCertificate thead:nth-child(1) tr").clone(true).appendTo("#tableCertificate tfoot");
        }
    });

    tableCertificate.on('order.dt search.dt', function () {
        tableCertificate.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function loadData() {
    table = $("#tableData").DataTable({
        language: systemConfig.languageDataTable,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ajax: {
            url: systemConfig.defaultAPIURL + "api/employer-wallet-history/list-employer-wallet-history",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
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
                data: "companyName",
                render: function (data, type, row, meta) {
                    var employerName = row.employerName;
                    var employerEmail = row.employerEmail;
                    var employerPhone = row.employerPhone;
                    var html = '<div style="margin-bottom: 5px;" class = "text-primary">' + data + '</div><div style="margin-bottom: 5px;" >' + employerName + '</div><div style="margin-bottom: 5px;" >' + employerEmail + '</div><div >' + formatPhoneNumber(employerPhone) + '</div>';
                    return html;
                }
            },
            {
                data: "candidateName",
                render: function (data, type, row, meta) {
                    var candidateEmail = row.candidateEmail;
                    var candidatePhone = row.candidatePhone;
                    var html = '<div style="margin-bottom: 5px;">' + data + '</div><div style="margin-bottom: 5px;" >' + candidateEmail + '</div><div>' + formatPhoneNumber(candidatePhone) + '</div>';
                    return html;
                }
            },
            {
                data: "description",
                render: function (data, type, row, meta) {
                    return data;
                }
            },
            {
                data: "updatedTime",
                render: function (data, type, row, meta) {
                    return `<div class='text-center'>` + moment(data).format("DD/MM/YYYY HH:mm:ss") + `</div>`;
                }
            },
            {
                data: "isApproved",
                render: function (data, type, row, meta) {
                    return `<span class="btn-admin-edit btn btn-icon"  data-idItem='` + data.id + `' id='row` + row.id + `-column-activated'><div class="form-check form-switch form-check-custom form-check-solid justify-content-center column-approve"><input class="form-check-input checkIsApproved" data-id="${row.id}" type="checkbox" value="" ${data ? 'checked=""' : ''}></div></span>`

                        ;
                }
            },
            {
                data: "id",
                render: function (data, type, row, meta) {
                    return `<div class='d-flex justify-content-center'>`
                        + `<button class="btn-admin-edit_1 btn btn-icon" title='Xem chi tiết' data-idItem='` + row.candidateId + `'><span class='svg-icon-primary svg-icon  svg-icon-1'> ` + iconeye + ` </span></button>`;
                        

                }
            }
        ],
        columnDefs: [

            { targets: [0, -1], orderable: false },

        ],
        'order': [
            [4, 'asc']
        ],
        drawCallback: function () {
            $('#tableData tfoot').html("");
            $("#tableData thead:nth-child(1) tr").clone(true).appendTo("#tableData tfoot");
            $("#rowSearch").removeClass("d-none");
        }

    });
    table.on('draw', function (e) {
        dataExport = table.ajax.json().allData;

    });
    
}
async function getItemById1(id) {
    //return (await $.ajax({
    //    url: systemConfig.defaultAPIURL + "api/candidate/detail/" + id,
    //    type: "GET",
    //    success: function (responseData) {
    //    },
    //    error: function (e) {
    //    },
    //})).resources;

    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/candidate/detail/" + id,
        type: "GET",
        success: function (responseData) {
            updatingObj = responseData.resources;
            
        },
        error: function (e) {
            $("#loading").removeClass("show");

            swal.fire(
                'Ứng viên',
                'Đã có lỗi xảy ra, hãy kiểm tra lại thông tin.',
                'error'
            );
        },
    })
}
async function editItem(id) {
    $("#candidateModal").find('.nav-tabs a:first').tab('show');

    autosize.destroy($('#candidateObjective'));
    autosize.destroy($('#candidateInterests'));
    autosize.destroy($('#candidateInfoReferences'));
    autosize($('#candidateObjective'));
    autosize($('#candidateInterests'));
    autosize($('#candidateInfoReferences'));
    updatingId = id;
    $("#divCandidateJobPosition").removeClass('d-none')
    $("#divCandidateDoB").removeClass('d-none')
    $("#divCandidateAddress").removeClass('d-none')
    $("#divCandidatePhone").removeClass("d-none")
    $("#divCandidateEmail").removeClass("d-none")

    $("#loading").addClass("show");
    if (id > 0) {
        await getItemById1(updatingId);
        $("#isActiveVerify").removeClass('d-none')
        if (updatingObj != null && updatingObj != undefined) {
            $("#CandidateName").text(updatingObj.fullName)
            //hiển thị nghề nghiệp
            if (updatingObj.jobPosition != null) {
                $("#CandidateJobPosition").text(updatingObj.jobPosition);
            }
            else {
                $("#divCandidateJobPosition").addClass("d-none")
            }
            //hiển thị địa chỉ
            if (updatingObj.addressDetail != null) {
                $("#CandidateAddress").text(updatingObj.addressDetail);
            }
            else {
                $("#divCandidateAddress").addClass("d-none")
            }
            //hiển thị ngày sinh
            if (updatingObj.doB != null) {
                $("#CandidateDoB").text(moment(updatingObj.doB, "YYYY-MM-DDTHH:mm:ss").format("DD/MM/YYYY"));
            }
            else {
                $("#divCandidateDoB").addClass("d-none")
            }
            //hiển thị sđt
            if (updatingObj.phone != null && updatingObj.phone != "") {
                $("#CandidatePhone").text(formatPhoneNumber(updatingObj.phone));
            }
            else {
                $("#divCandidatePhone").addClass("d-none")
            }
            //hiển thị email
            if (updatingObj.email != null && updatingObj.email != "") {
                $("#CandidateEmail").text(updatingObj.email);
            }
            else {
                $("#divCandidateEmail").addClass("d-none")
            }

            //hiển thị quốc tịch
            if (updatingObj.nationality != null && updatingObj.nationality != "") {
                $("#CandidateNationality").text(updatingObj.nationality);
            }
            else {
                $("#divCandidateNationality").addClass("d-none")
            }
            //mật khẩu
            $("#accountPassword").val('');
            $("#accountConfirmPassword").val('');
            $("#spanNewPass").addClass('d-none');
            $("#spanConfirmNewPass").addClass('d-none');
            //thông tin xung quanh
            //$("#CandidateEmail").text(updatingObj.email)
            //$("#CandidatePhone").text(formatPhoneNumber(updatingObj.phone));
            $("#candidatePhoto").prop("src", updatingObj.photo != null ? systemConfig.defaultStorageURL + updatingObj.photo : systemConfig.defaultStorageURL + "/assets/media/images/avatar1.jpg")
            $("#isActiveVerify").addClass(updatingObj.isActivated ? " " : "d-none");
            $("#selectCandidateStatus").val(updatingObj.candidateStatusId).trigger('change');
            $("#selectCandidateLevel").val(updatingObj.candidateLevelId).trigger('change');
            $("#candidateGender").val(updatingObj.gender).trigger('change');
            $("#candidateMaritalStatus").val(updatingObj.maritalStatus);
            //$("#candidateLocked").prop("checked", updatingObj.lockEnabled);
            //$("#candidateDoB").val(updatingObj.doB != null ? moment(updatingObj.doB, "YYYY-MM-DDTHH:mm:ss").format("DD/MM/YYYY") : "");
            $("#selectSuggestionExperienceRangeId").val(updatingObj.suggestionExperienceRangeId).trigger('change');
            $("#selectSuggestionSalaryRangeId").val(updatingObj.suggestionSalaryRangeId).trigger('change');
            $("#createdTime").val(moment(updatingObj.createdTime, "YYYY-MM-DDTHH:mm:ss").format("DD/MM/YYYY HH:mm:ss"))
            $("#candidateInterests").val(updatingObj.interests);
            $("#candidateObjective").val(updatingObj.objective);
            $("#candidateInfoReferences").val(updatingObj.references);
            //kiểm tra checked
            $("#IsSubcribeEmailImportantSystemUpdate").prop("checked", updatingObj.isSubcribeEmailImportantSystemUpdate);
            $("#IsSubcribeEmailEmployerViewCV").prop("checked", updatingObj.isSubcribeEmailEmployerViewCV);
            $("#IsSubcribeEmailNewFeatureUpdate").prop("checked", updatingObj.isSubcribeEmailNewFeatureUpdate);
            $("#IsSubcribeEmailOtherSystemNotification").prop("checked", updatingObj.isSubcribeEmailOtherSystemNotification);
            $("#IsSubcribeEmailJobSuggestion").prop("checked", updatingObj.isSubcribeEmailJobSuggestion);
            $("#IsSubcribeEmailEmployerInviteJob").prop("checked", updatingObj.isSubcribeEmailEmployerInviteJob);
            $("#IsSubcribeEmailServiceIntro").prop("checked", updatingObj.isSubcribeEmailServiceIntro);
            $("#IsSubcribeEmailProgramEventIntro").prop("checked", updatingObj.isSubcribeEmailProgramEventIntro);
            $("#IsSubcribeEmailGiftCoupon").prop("checked", updatingObj.isSubcribeEmailGiftCoupon);
            $("#IsCheckOnJobWatting").prop("checked", updatingObj.isCheckOnJobWatting);
            $("#IsCheckJobOffers").prop("checked", updatingObj.isCheckJobOffers);
            $("#IsCheckViewCV").prop("checked", updatingObj.isCheckViewCV);
            $("#IsCheckTopCVReview").prop("checked", updatingObj.isCheckTopCVReview);

            //mong muốn công việc
            //ngành nghề
            if (updatingObj.listCandidateSuggestionJobCategory.length > 0) {
                var jobCategoryId = [];
                updatingObj.listCandidateSuggestionJobCategory.forEach(function (item) {
                    jobCategoryId.push(item.jobCategoryId);
                })
                $("#selectSuggestionJobCategory").val(jobCategoryId).trigger('change');
            }
            else {
                $("#selectSuggestionJobCategory").val("").trigger('change');

            }

            //vị trí
            if (updatingObj.listCandidateSuggestionJobPosition.length > 0) {
                var jobPositionId = [];
                updatingObj.listCandidateSuggestionJobPosition.forEach(function (item) {
                    jobPositionId.push(item.jobPositionId);
                })
                $("#selectSuggestionJobPosition").val(jobPositionId).trigger('change');
            }
            else {
                $("#selectSuggestionJobPosition").val("").trigger('change');

            }

            //kỹ năng
            if (updatingObj.listCandidateSuggestionJobSkill.length > 0) {
                var jobSkillId = [];
                updatingObj.listCandidateSuggestionJobSkill.forEach(function (item) {
                    jobSkillId.push(item.jobSkillId);
                })
                $("#selectSuggestionJobSkill").val(jobSkillId).trigger('change');
            }
            else {
                $("#selectSuggestionJobSkill").val("").trigger('change');

            }

            //địa điểm
            if (updatingObj.candidateSuggestionWorkPlaces.length > 0) {
                var workplaceId = [];
                updatingObj.candidateSuggestionWorkPlaces.forEach(function (item) {
                    workplaceId.push(item.workPlaceId);
                })
                $("#selectSuggestionWorkPlace").val(workplaceId).trigger('change');
            }
            else {
                $("#selectSuggestionWorkPlace").val("").trigger('change');
            }

            $("#candidateGender").select2();
            reGenTableJobSkill();
            reGenTableWorkExperience();
            reGenTableEducation();
            reGenTableCertificate();
            $("#candidateModalTitle").text( "Thông tin ứng viên");
            $("#loading").removeClass("show");
            $("#candidateModal").modal("show");
        }
        else {
            $("#loading").removeClass("show");
            swal.fire(
                'Ứng viên',
                'Không thể xem chi tiết ứng viên, hãy kiểm tra lại thông tin.',
                'error'
            );
        }
    }

}

function loadDataCandidateLevel() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/candidate-level/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            //dataSource = response.resources;
            response.resources.forEach(function (item, index) {
                //console.log(item);
                $('#searchLevel').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#selectCandidateLevel').append(new Option(item.name, item.id, false, false)).trigger('change');
            })
            $("#selectCandidateLevel").select2();
            $("#searchLevel").val("").trigger('change')
            $("#searchLevel").select2({
                allowClear: true,
                placeholder: ""
            });

        },
        error: function (e) {
        }
    });
}
function loadDataSalaryRange() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/salary-range/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            //$('#advanced-salary-range').append(new Option("Tất cả mức lương", 0, false, false)).trigger('change');
            //dataSource = response.resources;
            response.resources.forEach(function (item, index) {
                //console.log(item);

                $('#selectSuggestionSalaryRange').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#advanced-salary-range').append(new Option(item.name, item.id, false, false));

            })
            $("#selectSuggestionSalaryRange").select2();
            $("#advanced-salary-range").select2({
                placeholder: "Mức lương mong muốn",
                allowClear: true,

            });

        },
        error: function (e) {
        }
    });
}
async function loadDataSkillLevel() {
    try {
        let res = await httpService.getAsync('api/skill-level/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-job-city').append(new Option("Tất cả vị trí", 0, false, false)).trigger('change');

            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#advanced-skill-level').append(new Option(item.name, item.id, false, false));

            })
            $("#advanced-skill-level").select2({
                placeholder: "Mức độ thành thạo",
                allowClear: true,
            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}

async function loadDataWorkPlace() {
    try {
        let res = await httpService.getAsync('api/workplace/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-job-city').append(new Option("Tất cả vị trí", 0, false, false)).trigger('change');

            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#selectSuggestionWorkPlace').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#advanced-job-city').append(new Option(item.name, item.id, false, false));

            })
            $("#selectSuggestionWorkPlace").select2({
                placeholder: "",
            });
            $("#advanced-job-city").select2({
                placeholder: "Địa điểm làm việc",
                allowClear: true,

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}
async function loadDataJobSkill() {
    try {
        let res = await httpService.getAsync('api/job-skill/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-skill').append(new Option("Tất cả kỹ năng", 0, false, false)).trigger('change');

            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#selectSuggestionJobSkill').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#advanced-skill').append(new Option(item.name, item.id, false, false));

            })
            $("#selectSuggestionJobSkill").select2({
                placeholder: "",
            });
            $("#advanced-skill").select2({
                placeholder: "Gợi ý kỹ năng",
                allowClear: true,

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}
async function loadDataJobPosition() {
    try {
        let res = await httpService.getAsync('api/job-position/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-position').append(new Option("Tất cả vị trí", 0, false, false)).trigger('change');

            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#selectSuggestionJobPosition').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#advanced-position').append(new Option(item.name, item.id, false, false));

            })
            $("#selectSuggestionJobPosition").select2({
                placeholder: "",
            });
            $("#advanced-position").select2({
                placeholder: "Vị trí quan tâm",
                allowClear: true,

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}
async function loadDataJobCategory() {
    try {
        let res = await httpService.getAsync('api/job-category/list');
        if (res.isSucceeded) {
            //dataSource = response.resources;
            //$('#advanced-suggest-job-category').append(new Option("Tất cả danh mục", 0, false, false)).trigger('change');

            res.resources.forEach(function (item, index) {
                //console.log(item);
                $('#selectSuggestionJobCategory').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#advanced-suggest-job-category').append(new Option(item.name, item.id, false, false));

            })
            $("#selectSuggestionJobCategory").select2({
                placeholder: "",
            });
            $("#advanced-suggest-job-category").select2({
                placeholder: "Danh mục quan tâm",
                allowClear: true,

            });
        }
        else {
            console.error(res);

        }
    } catch (e) {
        console.error(e);
    }
}
function loadDataExperienceRange() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/experience-range/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            //dataSource = response.resources;
            //$('#advanced-experience-range').append(new Option("Tất cả kinh nghiệm", 0, false, false)).trigger('change');

            response.resources.forEach(function (item, index) {
                //console.log(item);

                $('#selectSuggestionExperienceRangeId').append(new Option(item.name, item.id, false, false)).trigger('change');
                $('#selectSuggestionExperience').append(new Option(item.name, item.id, false, false)).trigger('change');
                

            })
            $("#selectSuggestionExperienceRangeId").select2();
            $("#selectSuggestionExperience").select2({
                placeholder: ""
            });
           

        },
        error: function (e) {
        }
    });
}
function loadDataAccountStatus() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/account-status/list",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            //dataSource = response.resources;
            response.resources.forEach(function (item, index) {
                //console.log(item);
                $('#selectCandidateStatus').append(new Option(item.name, item.id, false, false)).trigger('change');

            })
            $("#selectCandidateStatus").select2();
            
            

        },
        error: function (e) {
        }
    });
}
$("#candidateModal").on('shown.bs.modal', function () {
    $(this).find('.nav-tabs a:first').tab('show');
    autosize.destroy($('#candidateInterests'));
    autosize.destroy($('#candidateObjective'));
    autosize.destroy($('#candidateInfo'));

    autosize($('#candidateInterests'));
    autosize($('#candidateObjective'));
    autosize($('#candidateInfo'));

})
$("#candidateModal").on('hiden.bs.modal', function () {
    $(this).find('.nav-tabs a:first').tab('show');
    autosize.destroy($('#candidateInfo'));
    autosize.destroy($('#candidateInterests'));
    autosize.destroy($('#candidateObjective'));

})
function tableSearch() {
    table.column(1).search($("#searchEmployerName").val());
    table.column(2).search($("#searchCandidateName").val());
    table.column(3).search($("#searchDescription").val());

    var searchValue = $("#table_search_all").val().trim().replace(/\./g, '');
    table.search(searchValue);

    if ($("#fillter_startDate_value").val().length > 0 || $("#fillter_endDate_value").val().length > 0) {
        var minDate = $("#fillter_startDate_value").val();
        var maxDate = $("#fillter_endDate_value").val();
        let searchDateArrs = [];
        if (minDate.length > 0) {
            searchDateArrs.push(moment(minDate, "DD/MM/YYYY").format("YYYY-MM-DD 00:00:00"));
        } else {
            searchDateArrs.push("");
        }
        if (maxDate.length > 0) {
            searchDateArrs.push(moment(maxDate, "DD/MM/YYYY").format("YYYY-MM-DD 23:59:59"));
        } else {
            searchDateArrs.push("");
        }
        table.column(4).search(searchDateArrs.toString());
    } else {
        table.column(4).search("");
    }

    table.column(5).search($("#searchIsApproved").val());

    table.draw();
}




async function getItemById(id) {
    await $.ajax({
        url: systemConfig.defaultAPIURL + "api/employer-wallet-history/detail/" + id,
        type: "GET",
        success: function (responseData) {
            updatingObj = responseData.resources;
        },
        error: function (e) {
            swal.fire(
                'Đơn hàng',
                'Đã có lỗi xảy ra, hãy kiểm tra lại thông tin.',
                'error'
            );
        },
    })
}

async function quickActivated(isApproved, id) {
    var titleName = "Lịch sử ví nhà tuyển dụng";
    var actionName = isApproved ? "duyệt" : "bỏ duyệt";
    await getItemById(id);
    var obj = { "id": id, "IsApproved": isApproved }
    
    Swal.fire({
        title: titleName,
        html: "Bạn có chắc chắn muốn " + actionName + " lịch sử ví nhà tuyển dụng <strong>" + updatingObj.employerName + "</strong> không?",
        icon: 'info',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu',
        showCancelButton: true
    }).then((result) => {
        if (result.isConfirmed) {
            $("#loading").addClass('show');

            //CALL AJAX TO UPDATE
            $.ajax({
                url: systemConfig.defaultAPIURL + "api/employer-wallet-history/quick-isApproved?id=" + id,
                type: "PUT",
                contentType: "application/json",
                success: function (response) {
                    $("#loading").removeClass('show');
                    if (response.isSucceeded) {
                        Swal.fire(
                            'Kích hoạt ứng viên',
                            'Ứng viên <b>' + updatingObj.employerName + ' </b> đã được ' + actionName + ' thành công.',
                            'success'
                        );
                        reGenTable();
                    }
                    else {
                        if (response.status == 400) {
                            if (response.errors != null) {
                                var contentError = "<ul>";
                                response.errors.forEach(function (item, index) {
                                    contentError += "<li class='text-start pb-2'>" + item + "</li>";
                                })
                                contentError += "</ul>";
                                Swal.fire(
                                    'Ứng viên <p class="swal__admin__subtitle"> Kích hoạt không thành công </p>',
                                    contentError,
                                    'warning'
                                );
                            } else {
                                Swal.fire(
                                    'Lưu ý',
                                    response.message,
                                    'warning'
                                )
                            }
                        }
                        else {
                            Swal.fire(
                                'Lưu ý',
                                response.message,
                                'warning'
                            )
                        }
                    }

                },
                error: function (e) {
                    //console.log(e)
                    $("#loading").removeClass('show');
                    if (e.status === 401) {
                        Swal.fire(
                            'Lịch sử ví nhà tuyển dụng',
                            'Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập để sử dụng tính năng này.',
                            'error'
                        ).then(function () {
                            window.location.href = "/home/login";
                        });
                    }
                    else if (e.status == 403) {
                        Swal.fire(
                            'Lịch sử ví nhà tuyển dụng',
                            'Bạn không có quyền sử dụng tính năng này.',
                            'error'
                        );
                    }
                    else {
                        Swal.fire(
                            'Lịch sử ví nhà tuyển dụng',
                            'Duyệt nhà tuyển dụng không thành công, <br> vui lòng thử lại sau!',
                            'error'
                        );
                    }


                }
            });
        }
        else {
            $(".checkIsApproved[data-id=" + id + "]").prop("checked", !isApproved);
        }
    })
}

$("#form-order").on('submit', function (e) {
    e.preventDefault();
})
function changePassword() {
    var listErr = [];
    if ($("#accountPassword").val().length == 0) {
        listErr.push("Mật khẩu không được để trống")
    }
    else {
        if ($("#accountPassword").val().length < 8) {
            listErr.push("Mật khẩu phải lớn hơn 8 ký tự")
        }
    }

    if ($("#accountConfirmPassword").val() != $("#accountPassword").val()) {
        listErr.push("Nhập lại mật khẩu phải trùng khớp với mật khẩu mới")
    }

    if (listErr.length > 0) {
        var content = "<ul>";
        listErr.forEach(function (item) {
            content += "<li class='text-start'>" + item + "</li>";
        })
        content += "</ul>";
        swal.fire({
            title: "Cập nhật mật khẩu",
            html: content,
            icon: "warning",
        })
    }
    else {
        $("#loading").addClass("show");

        var obj = {
            id: updatingId,
            newPassword: $("#accountPassword").val(),
            confirmPassword: $("#accountConfirmPassword").val(),
            oldPassword: "000"
        }
        $.ajax({
            url: systemConfig.defaultAPIURL + "api/candidate/change-password",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (res) {
                $("#loading").removeClass("show");
                if (!res.isSucceeded) {
                    if (res.status == 400) {
                        if (res.errors != null) {
                            var contentError = "<ul>";
                            res.errors.forEach(function (item, index) {
                                contentError += "<li class='text-start pb-2'>" + item + "</li>";
                            })
                            contentError += "</ul>";
                            Swal.fire(
                                'Ứng viên <p class="swal__admin__subtitle"> Kích hoạt không thành công </p>',
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
                else {
                    Swal.fire(
                        'Cập nhật mật khẩu!',
                        'Cập nhật mật khẩu cho ứng viên ' + updatingObj.fullName + ' thành công!',
                        'success'
                    );
                    $("#accountPassword").val('');
                    $("#accountConfirmPassword").val('');
                }
            },
            error: function (e) {
                $("#loading").removeClass("show");
                Swal.fire(
                    'Cập nhật mật khẩu!',
                    'Đã có lỗi xảy ra khi cập nhật mật khẩu.',
                    'warning'
                );
            }
        })
    }
}
$("#accountPassword").on('keyup keypress', function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
    }
    if ($(this).val().length > 0) {
        $("#spanNewPass").removeClass('d-none')
    }
    else {
        $("#spanNewPass").addClass('d-none')

    }
})
$("#accountConfirmPassword").on('keyup keypress', function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
    }
    if ($(this).val().length > 0) {
        $("#spanConfirmNewPass").removeClass('d-none')
    }
    else {
        $("#spanConfirmNewPass").addClass('d-none')

    }
})
$("#changePassword").click(function (e) {
    e.preventDefault();
    changePassword();

})
