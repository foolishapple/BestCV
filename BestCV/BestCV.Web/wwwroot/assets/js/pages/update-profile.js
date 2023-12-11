var pathLogo = "";
var updatingId = 0;
var infoProfile = {};
var updatingSkillId = 0;
var updatingEduId = 0;
var updatingExpId = 0;
var updatingCerId = 0;
var updatingProjectId = 0;
var updatingHonorId = 0;
var updatingActivitesId = 0;
$(document).ready(function () {
    GetDetail(); 
    loadSkillData();
    loadDataSkillLevel();
    countStar();
    $("#gender").select2();
})
$("form").on('click','#submitButton', function (e) {
    validate();

})

function countStar() {
    const stars = $('.hover-icon');
    const selectedRating = $('.selected-rating');
    let selectedValue = 0;

    let ratingText = {};
    for (let skillLevel of dataSourseSkillLevel) {
        ratingText[skillLevel.id] = skillLevel.name;
    }

    stars.on('mouseover', function () {
        const rating = $(this).attr('data-rating');
        if (selectedValue === 0) {
            selectedRating.text(ratingText[rating]);
            stars.each(function () {
                if ($(this).attr('data-rating') <= rating) {
                    $(this).removeClass('fa-light').addClass('fa-solid');
                } else {
                    $(this).removeClass('fa-solid').addClass('fa-light');
                }
            });
        }
    });

    stars.on('mouseout', function () {
        if (selectedValue === 0) {
            stars.each(function () {
                $(this).removeClass('fa-solid').addClass('fa-light');
            });
        }
    });

    stars.on('click', function () {
        const rating = $(this).attr('data-rating');
        selectedRating.text(ratingText[rating]);
        selectedValue = rating;
        stars.each(function () {
            if ($(this).attr('data-rating') <= rating) {
                $(this).removeClass('fa-light').addClass('fa-solid');
            } else {
                $(this).removeClass('fa-solid').addClass('fa-light');
            }
        });
       
    });
}

// lấy thông tin cá nhân của candidate
function GetDetail() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/Candidate/candidate-detail",
        type: "GET",
        contentType: "application/json",
        beforeSend: function (xhr) {
            if (localStorage.currentUser) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
            }
        },
        success: function (response) {
            if (response.isSucceeded) {
                infoProfile = response.resources;
                updatingId = infoProfile.id;
                //console.log(infoProfile);
                // get photo
                if (infoProfile.photo != null && infoProfile.photo != "") {
                    pathLogo = infoProfile.photo;
                    let urlLogo = systemConfig.defaultStorage_URL + infoProfile.photo;
                    $('#pxp-candidate-photo-choose-file').next('label').css({
                        'background-image': `url('${urlLogo}')`,
                        'border': '0 none'
                    }).find('span').hide();
                }   //get cover
                if (infoProfile.coverPhoto != null && infoProfile.coverPhoto != "") {
                    pathCover = infoProfile.coverPhoto;
                    let urlCover = systemConfig.defaultStorage_URL + infoProfile.coverPhoto;
                    $('#pxp-candidate-cover-choose-file').next('label').css({
                        'background-image': `url('${urlCover}')`,
                        'border': '0 none'
                    }).find('span').hide();
                }
                $("#fullName").val(infoProfile.fullName);
                $("#email").val(infoProfile.email);
                $("#job-title").val(infoProfile.jobPosition);
                $("#phone").val(infoProfile.phone);

                $("#gender").val(infoProfile.gender);
                $("#gender").append().trigger('change');

                $("#address").val(infoProfile.addressDetail);
                $("#hobby").val(infoProfile.interests);
                $("#objective").val(infoProfile.objective);
                loadTableDataEducation(infoProfile);
                loadTableDataExperience(infoProfile);
                loadTableDataCertificate(infoProfile);
                loadTableDataProjects(infoProfile);
                loadTableDataHonor(infoProfile);
                loadTableDataActivites(infoProfile);
                loadTableDataSkill(infoProfile);
            }
        },
        error: function () {
            infoProfile = null;
        }
    });
}


function loadTableDataEducation(infoProfile) {
    $("#education-table").empty();
    var educationList = infoProfile.listCandidateEducation;

    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    educationList.forEach(function (education) {
        // Thực hiện các thao tác để hiển thị thông tin giáo dục
        if (education.isDeleted) {

        }
        else {
           var educationRow = '<tr>' +
                '<td><b>' + education.title + '</b></td>' +
                '<td>' + education.school + '</td>' +
                '<td>' + education.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-edu" data-id="' + education.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-edu" data-id="' + education.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#education-table').append(educationRow);
        }
    });
}

function loadTableDataExperience(infoProfile) {
    $("#experiences-table").empty();
    var experienceList = infoProfile.listCandidateWorkExperiences;
    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    experienceList.forEach(function (experience) {
        // Thực hiện các thao tác để hiển thị thông tin giáo dục
        if (experience.isDeleted) {

        }
        else {
            var experienceRow = '<tr>' +
                '<td><b>' + experience.jobTitle + '<b></td>' +
                '<td>' + experience.company + '</td>' +
                '<td>' + experience.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-exp" data-id="' + experience.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-exp" data-id="' + experience.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#experiences-table').append(experienceRow);
        }
        
    });
}

function validate() {
    let errorList = [];
    
    if ($("#fullName").val().length == 0 || $("#fullName").val().length > 255) {
        errorList.push("Tên trường không được để trống và không vượt quá 255 ký tự.");
    } 
    if ($("#phone").val().length == 0 || $("#phone").val().length > 255) {
        errorList.push("số điện thoại không được để trống và không vượt quá 255 ký tự.");
    } 
    if ($("#email").val().length == 0 || $("#email").val().length > 255 ) {
        errorList.push("Email không được để trống và không vượt quá 255 ký tự.");
    } 

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin cá nhân không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        UpdateProfile();
    }
}
//random ID
function generateUniqueId() {
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let id = '';
    for (let i = 0; i < 10; i++) {
        const randomIndex = Math.floor(Math.random() * chars.length);
        id += chars[randomIndex];
    }
    return id;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Skill
function loadTableDataSkill(infoProfile) {
    $("#skill-table").empty();

    var skillList = infoProfile.listCandidateSkill;

    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    skillList.forEach(function (skill) {
        // Thực hiện các thao tác để hiển thị thông tin giáo dục
        if (skill.isDeleted) {

        }
        else {
            let skillLevel = skill.skillLevelId;
            
            let skillLevelName = dataSourseSkillLevel.find(x => x.id == parseInt(skillLevel));
            let skillStar = "";
            if (skillLevelName.id == DEFAULT_VALUE_JUST_HEARD_ABOUT) {
                for (var i = 0; i < 1; i++) {
                    skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
                }
            } else if (skillLevelName.id == DEFAULT_VALUE_NO_COMPLETE_PRODUCT_YET) {
                for (var i = 0; i < 2; i++) {
                    skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
                }
            } else if (skillLevelName.id == DEFAULT_VALUE_HAVE_A_COMPLETE_PRODCUT) {
                for (var i = 0; i < 3; i++) {
                    skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
                }
            } else if (skillLevelName.id == DEFAULT_VALUE_HAVE_A_COMPLETE_PRODUCT_GROUP) {
                for (var i = 0; i < 4; i++) {
                    skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
                }
            } else if (skillLevelName.id == DEFAULT_VALUE_PROFICIENT) {
                for (var i = 0; i < 5; i++) {
                    skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
                }
            }
            

            var skillRow = '<tr>' +
                '<td><b>' + skill.name + '</b></td>' +
                '<td><b>' + skillStar + '</b></td>' +
                '<td><b>' + skill.description + '</b></td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Delete" class="delete-skill" data-id="' + skill.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#skill-table').append(skillRow);
        }
    });
}
//click button Certificate
$("form").on('click', '#add-skill', function (e) {
    e.preventDefault();
    validateSkill();
    clearInputSkill();
})
//clear input Certificate
function clearInputSkill() {
    $("#skill-title").text("");
    $("#skill-about").val("");
    //$("#skill-level").val("");
}
//validate Certificate
function validateSkill() {
    let errorList = [];
    var skillTitle = $("#skill-title").text().trim();
    if (skillTitle.length === 0) {
        errorList.push("Tên kỹ năng không được để trống.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin kỹ năng không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (!updatingSkillId) {
            AddSkill();
        } 
    }
}

// Cập nhật skill
function UpdateSkill() {
    let skill = infoProfile.listCandidateSkill.find(c => c.id == updatingSkillId);
    skill.isUpdated = true;
    var a = $("#skill-title").select2("trigger", "select", {
        data: {
            id: skill.id,
            text: skill.name
        },
    });
    /*skill.name = $("#skill-title").val();    */
    skill.name = a;
    skill.description = $("#skill-about").val();
    updatingSkillId = null;
    $("#add-skill").text("Thêm mới");
    clearInputSkill();
    loadTableDataSkill(infoProfile);
}

//thêm mới skill
function AddSkill() {
    var a = $(".courseReview-rating i.fa-solid").last().attr("data-rating");
    let newSkill = {
        id: generateUniqueId(), 
        name: $("#skill-title").text(),
        skillId: $("#skill-title").val(),
        skillLevelId: a, 
        description: $("#skill-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateSkill.push(newSkill);

    // Thêm dòng mới vào bảng
    appendTableSkill(newSkill);

    // Xóa dữ liệu từ các ô input
    clearInputSkill();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingSkillId = null;
    $("#add-skill").text("Thêm mới");
}

function loadSkillData() {
    $('#skill-title').select2({
        dropdownCssClass: 'bigdrop',
        minimumInputLength: 3,
        ajax: {
            url: systemConfig.defaultAPIURL + "api/skill/search-skills",
            type: "POST",
            contentType: "application/json",
            data: function (params) {
                return JSON.stringify({
                    searchString: params.term,
                    pageLimit: 10
                });
            },
            processResults: function (res) {
                var data = res.resources || [];

                var results = data.map(function (item) {
                    return {
                        id: item.id, 
                        text : item.name 
                    };
                });
                return {
                    results: results
                };
            }
        },
        escapeMarkup: function (markup) { return markup; },
        templateSelection: formatJobSelection
    });
}

var dataSourseSkillLevel = [];
function loadDataSkillLevel() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/skill-level/list",
        type: "GET",
        async: false,
        contentType: "application/json",
        success: function (response) {
            dataSourseSkillLevel = response.resources;
            var content = "";
            response.resources.forEach(function (item) {
                var option = new Option(item.name, item.id, false, false);
                $('#skill-level').append(option);
                content += `<i class="fa-solid fa-star hover-icon" data-rating="${item.id}"></i>`
                
            });

            $(".courseReview-rating").html(content)
            $("#skill-level").select2({
                allowClear: true,
                placeholder: ""
            });
        },
        error: function (e) {
            console.error('An error occurred:', e);
        }
    });
}

function formatJobSelection(data) {
    return `\
            <div class="">\
                <span><strong>` + data.text + `</strong></span>\
            </div>\
        `;
}

//click icon delete Certificate
$(document).on('click', ".delete-skill", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let skillId = $(this).attr("data-id");
    if (skillId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteSkill(skillId);
    }
});
//sự kiện xóa Certificate
function deleteSkill(skillId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateSkill.findIndex(skill => skill.id == skillId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateSkill[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#skill-table tr[data-id="' + skillId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateSkill[index].isAdded) {
            infoProfile.listCandidateSkill.splice(index, 1);
        }
        loadTableDataSkill(infoProfile);
    }
}
//add thêm table
function appendTableSkill(skill) {
    var skillLevel = $(".courseReview-rating i.fa-solid").last().attr("data-rating")
    let skillLevelName = dataSourseSkillLevel.find(x => x.id == parseInt(skillLevel));

    let skillStar = "";
    if (skillLevelName.id == DEFAULT_VALUE_JUST_HEARD_ABOUT) {
        for (var i = 0; i < 1; i++) {
            skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
        }
    } else if (skillLevelName.id == DEFAULT_VALUE_NO_COMPLETE_PRODUCT_YET) {
        for (var i = 0; i < 2; i++) {
            skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
        }
    } else if (skillLevelName.id == DEFAULT_VALUE_HAVE_A_COMPLETE_PRODCUT) {
        for (var i = 0; i < 3; i++) {
            skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
        }
    } else if (skillLevelName.id == DEFAULT_VALUE_HAVE_A_COMPLETE_PRODUCT_GROUP) {
        for (var i = 0; i < 4; i++) {
            skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
        }
    } else if (skillLevelName.id == DEFAULT_VALUE_PROFICIENT) {
        for (var i = 0; i < 5; i++) {
            skillStar += `<i class="fa-solid fa-star hover-icon" id="star-color"></i>`;
        }
    }

    var skillRow = '<tr>' +
        '<td><b>' + skill.name + '</b></td>' +
        '<td><b>' + skillStar + '</b></td>' +
        '<td><b>' + skill.description + '</b></td>' +
        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Delete" class="delete-skill" data-id="' + skill.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#skill-table').append(skillRow);
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//education
$("form").on('click', '#add-edu', function (e) {
    e.preventDefault();
    validateEdu();

})
//clear input Edu
function clearInputEdu() {
    $("#edu-title").val("");
    $("#edu-school").val("");
    $("#edu-time").val("");
    $("#edu-about").val("");
}
//validate education
function validateEdu() {
    let errorList = [];
 

    if ($("#edu-title").val().length == 0 || $("#edu-title").val().length > 255) {
        errorList.push("Chuyên ngành không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#edu-school").val().length == 0 || $("#edu-school").val().length > 255) {
        errorList.push("Trường học không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#edu-time").val().length == 0 || $("#edu-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin giáo dục và đào tạo không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingEduId) {
            UpdateEdu();
        } else {
            AddEdu();
        }
    }
}

//edit Edu
$(document).on('click', ".edit-edu", function (e) {
    e.preventDefault();
    let eduId = $(this).attr("data-id");
    if (eduId) {
        updatingEduId = eduId;
        let edu = infoProfile.listCandidateEducation.find(c => c.id == eduId);
        $("#edu-title").val(edu.title);
        $("#edu-school").val(edu.school);
        $("#edu-time").val(edu.timePeriod);
        $("#edu-about").val(edu.description);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-edu").text("Cập nhật");
        

    }
})

// Cập nhật Giáo dục và đào tạo
function UpdateEdu() {
    let edu = infoProfile.listCandidateEducation.find(c => c.id == updatingEduId);
    edu.isUpdated = true;
    edu.title = $("#edu-title").val();
    edu.school = $("#edu-school").val();
    edu.timePeriod = $("#edu-time").val();
    edu.description = $("#edu-about").val();
    updatingEduId = null;
    $("#add-edu").text("Thêm mới");
    clearInputEdu();
    loadTableDataEducation(infoProfile);     
}
//thêm mới Edu
function AddEdu() {
  
    let newEdu = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        title: $("#edu-title").val(),
        school: $("#edu-school").val(),
        timePeriod: $("#edu-time").val(),
        description: $("#edu-about").val(),
        isAdded :true,
    };
    
    // Thêm mục mới vào danh sách
    infoProfile.listCandidateEducation.push(newEdu);

    // Thêm dòng mới vào bảng
    appendTableEdu(newEdu);

    // Xóa dữ liệu từ các ô input
    clearInputEdu();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingEduId = null;
    $("#add-edu").text("Thêm mới");
}
//click icon delete Edu
$(document).on('click', ".delete-edu", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let eduId = $(this).attr("data-id");
    if (eduId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteEdu(eduId);
    }
});
//sự kiện xóa edu
function deleteEdu(eduId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách
   
    const index = infoProfile.listCandidateEducation.findIndex(edu => edu.id == eduId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateEducation[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#education-table tr[data-id="' + eduId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateEducation[index].isAdded) {
            infoProfile.listCandidateEducation.splice(index, 1);
        }
        loadTableDataEducation(infoProfile);    
    }
}
//add thêm table
function appendTableEdu(edu) {
    var educationRow = '<tr>' +
        '<td><b>' + edu.title + '</b></td>' +
        '<td>' + edu.school + '</td>' +
        '<td>' + edu.timePeriod + '</td>' +
       
        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-edu" data-id="' + edu.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-edu" data-id="' + edu.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#education-table').append(educationRow);
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//click button thêm mới exp
$("form").on('click', '#add-exp', function (e) {
    e.preventDefault();
    validateExp();

})
//clear input exp
function clearInputExp() {
    $("#work-title").val("");
    $("#work-company").val("");
    $("#work-time").val("");
    $("#work-about").val("");
}
//validate exp
function validateExp() {
    let errorList = [];
    if ($("#work-title").val().length == 0 || $("#work-title").val().length > 255) {
        errorList.push("Chức vụ không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#work-company").val().length == 0 || $("#work-company").val().length > 255) {
        errorList.push("Công ty không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#work-time").val().length == 0 || $("#work-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin kinh nghiệm làm việc không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingExpId) {
            UpdateExp();
        } else {
            AddExp();
        }
    }
}

//edit Exp
$(document).on('click', ".edit-exp", function (e) {
    e.preventDefault();
    let expId = $(this).attr("data-id");
    if (expId) {
        updatingExpId = expId;
        let exp = infoProfile.listCandidateWorkExperiences.find(c => c.id == expId);
        $("#work-title").val(exp.jobTitle);
        $("#work-company").val(exp.company);
        $("#work-time").val(exp.timePeriod);
        $("#work-about").val(exp.description);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-exp").text("Cập nhật");


    }
})

// Cập nhật Exp
function UpdateExp() {
    let exp = infoProfile.listCandidateWorkExperiences.find(c => c.id == updatingExpId);
    exp.isUpdated = true;
    exp.jobTitle = $("#work-title").val();
    exp.company = $("#work-company").val();
    exp.timePeriod = $("#work-time").val();
    exp.description = $("#work-about").val();
    updatingExpId = null;
    $("#add-exp").text("Thêm mới");
    clearInputExp();
    loadTableDataExperience(infoProfile);
}
//thêm mới Exp
function AddExp() {

    let newExp = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        jobTitle: $("#work-title").val(),
        company: $("#work-company").val(),
        timePeriod: $("#work-time").val(),
        description: $("#work-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateWorkExperiences.push(newExp);

    // Thêm dòng mới vào bảng
    appendTableExp(newExp);

    // Xóa dữ liệu từ các ô input
    clearInputExp();

    // Đặt văn bản nút "Thêm mới" và xóa updatingExpId (nếu có)
    updatingExpId = null;
    $("#add-exp").text("Thêm mới");
}
//click icon delete Exp
$(document).on('click', ".delete-exp", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let expId = $(this).attr("data-id");
    if (expId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteExp(expId);
    }
});
//sự kiện xóa Exp
function deleteExp(expId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateWorkExperiences.findIndex(exp => exp.id == expId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateWorkExperiences[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#experiences-table tr[data-id="' + expId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateWorkExperiences[index].isAdded) {
            infoProfile.listCandidateWorkExperiences.splice(index, 1);
        }
        loadTableDataExperience(infoProfile);
    }
}
//add thêm table Exp
function appendTableExp(exp) {
    var experienceRow = '<tr>' +
        '<td><b>' + exp.jobTitle + '</b></td>' +
        '<td>' + exp.company + '</td>' +
        '<td>' + exp.timePeriod + '</td>' +

        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-exp" data-id="' + exp.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-exp" data-id="' + exp.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#experiences-table').append(experienceRow);
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Certificate
function loadTableDataCertificate(infoProfile) {
    $("#certificate-table").empty();
    var certificateList = infoProfile.listCandidateCertificates;

    certificateList.forEach(function (certificate) {

        if (certificate.isDeleted) {

        }
        else {
            var certificateRow = '<tr>' +
                '<td><b>' + certificate.name + '</b></td>' +
                '<td>' + certificate.issueBy + '</td>' +
                '<td>' + certificate.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-cer" data-id="' + certificate.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-cer" data-id="' + certificate.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#certificate-table').append(certificateRow);
        }

    });
}
//click button Certificate
$("form").on('click', '#add-cer', function (e) {
    e.preventDefault();
    validateCer();

})
//clear input Certificate
function clearInputCer() {
    $("#cerfi-title").val("");
    $("#cerfi-school").val("");
    $("#cerfi-time").val("");
    $("#cerfi-about").val("");
}
//validate Certificate
function validateCer() {

    let errorList = [];

    if ($("#cerfi-title").val().length == 0 || $("#cerfi-title").val().length > 255) {
        errorList.push("Tên chứng chỉ không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#cerfi-school").val().length == 0 || $("#cerfi-school").val().length > 255) {
        errorList.push("Nơi cấp không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#cerfi-time").val().length == 0 || $("#cerfi-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin chứng chỉ không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingCerId) {
            UpdateCer();
        } else {
            AddCer();
        }
    }
}

//edit Certificate
$(document).on('click', ".edit-cer", function (e) {
    e.preventDefault();
    let cerId = $(this).attr("data-id");
    if (cerId) {
        updatingCerId = cerId;
        let cer = infoProfile.listCandidateCertificates.find(c => c.id == cerId);
        $("#cerfi-title").val(cer.name);
        $("#cerfi-school").val(cer.issueBy);
        $("#cerfi-time").val(cer.timePeriod);
        $("#cerfi-about").val(cer.description);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-cer").text("Cập nhật");


    }
})

// Cập nhật Certificate
function UpdateCer() {
    let cer = infoProfile.listCandidateCertificates.find(c => c.id == updatingCerId);
    cer.isUpdated = true;
    cer.name = $("#cerfi-title").val();
    cer.issueBy = $("#cerfi-school").val();
    cer.timePeriod = $("#cerfi-time").val();
    cer.description = $("#cerfi-about").val();
    updatingCerId = null;
    $("#add-cer").text("Thêm mới");
    clearInputCer();
    loadTableDataCertificate(infoProfile);
}
//thêm mới Certificate
function AddCer() {

    let newCer = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        name: $("#cerfi-title").val(),
        issueBy: $("#cerfi-school").val(),
        timePeriod: $("#cerfi-time").val(),
        description: $("#cerfi-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateCertificates.push(newCer);

    // Thêm dòng mới vào bảng
    appendTableCerfi(newCer);

    // Xóa dữ liệu từ các ô input
    clearInputCer();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingCerId = null;
    $("#add-cer").text("Thêm mới");
}
//click icon delete Certificate
$(document).on('click', ".delete-cer", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let cerId = $(this).attr("data-id");
    if (cerId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteCer(cerId);
    }
});
//sự kiện xóa Certificate
function deleteCer(cerId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateCertificates.findIndex(cer => cer.id == cerId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateCertificates[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#certificate-table tr[data-id="' + cerId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateCertificates[index].isAdded) {
            infoProfile.listCandidateCertificates.splice(index, 1);
        }
        loadTableDataCertificate(infoProfile);
    }
}
//add thêm table
function appendTableCerfi(cer) {
    var certificateRow = '<tr>' +
        '<td><b>' + cer.name + '</b></td>' +
        '<td>' + cer.issueBy + '</td>' +
        '<td>' + cer.timePeriod + '</td>' +

        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-cer" data-id="' + cer.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-cer" data-id="' + cer.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#certificate-table').append(certificateRow);
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Projects
function loadTableDataProjects(infoProfile) {
    $("#projects-table").empty();
    var projectList = infoProfile.listCandidateProjects;
    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    projectList.forEach(function (projects) {
        if (projects.isDeleted) {

        }
        else {
            var projectRow = '<tr>' +
                '<td><b>' + projects.projectName + '</b></td>' +
                '<td>' + projects.customer + '</td>' +              
                '<td>' + projects.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-project" data-id="' + projects.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-project" data-id="' + projects.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#projects-table').append(projectRow);
        }
        // Thực hiện các thao tác để hiển thị thông tin giáo dục
       
    });
}
$("form").on('click', '#add-project', function (e) {
    e.preventDefault();
    validateProject();

})
//clear input Project
function clearInputProject() {
    $("#project-title").val("");
    $("#project-custumer").val("");
    $("#project-size").val("");
    $("#project-position").val("");
    $("#project-respon").val("");
    $("#project-time").val("");
    $("#project-about").val("");
}
//validate Project
function validateProject() {
    let errorList = [];
    var numberRegex = /^\d+$/;
    if ($("#project-title").val().length == 0 || $("#project-title").val().length > 255) {
        errorList.push("Tên dự án không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#project-custumer").val().length == 0 || $("#project-custumer").val().length > 255) {
        errorList.push("Tên khách hàng không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#project-size").val().length == 0 || isNaN($("#project-size").val())) {
        errorList.push("Số lượng thành viên không được để trống và chỉ được nhập số.");
    }
    if ($("#project-position").val().length == 0 || $("#project-position").val().length > 255) {
        errorList.push("Chức vụ không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#project-respon").val().length == 0 || $("#project-respon").val().length > 255) {
        errorList.push("Trách nhiệm không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#project-time").val().length == 0 || $("#project-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin dự án không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingProjectId) {
            UpdateProject();
        } else {
            AddProject();
        }
    }
}

//edit Project
$(document).on('click', ".edit-project", function (e) {
    e.preventDefault();
    let projectId = $(this).attr("data-id");
    if (projectId) {
        updatingProjectId = projectId;
        let project = infoProfile.listCandidateProjects.find(c => c.id == projectId);
        $("#project-title").val(project.projectName);
        $("#project-custumer").val(project.customer);
        $("#project-size").val(project.teamSize);
        $("#project-position").val(project.position);
        $("#project-respon").val(project.responsibilities);
        $("#project-time").val(project.timePeriod);
        $("#project-about").val(project.info);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-project").text("Cập nhật");


    }
})

// Cập nhật Project
function UpdateProject() {
    let project = infoProfile.listCandidateProjects.find(c => c.id == updatingProjectId);
    project.isUpdated = true;
    project.projectName = $("#project-title").val();
    project.customer = $("#project-custumer").val();
    project.teamSize = $("#project-size").val();
    project.position = $("#project-position").val();
    project.responsibilities = $("#project-respon").val();
    project.timePeriod = $("#project-time").val();
    project.info = $("#project-about").val();
    updatingProjectId = null;
    $("#add-project").text("Thêm mới");
    clearInputProject();
    loadTableDataProjects(infoProfile);
}
//thêm mới Project
function AddProject() {

    let newProject = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        projectName: $("#project-title").val(),
        customer: $("#project-custumer").val(),
        teamSize: $("#project-size").val(),
        position: $("#project-position").val(),
        responsibilities: $("#project-respon").val(),           
        timePeriod: $("#project-time").val(),
        info: $("#project-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateProjects.push(newProject);

    // Thêm dòng mới vào bảng
    appendTableProject(newProject);

    // Xóa dữ liệu từ các ô input
    clearInputProject();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingProjectId = null;
    $("#add-project").text("Thêm mới");
}
//click icon delete Project
$(document).on('click', ".delete-project", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let projectId = $(this).attr("data-id");
    if (projectId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteProject(projectId);
    }
});
//sự kiện xóa Project
function deleteProject(projectId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateProjects.findIndex(project => project.id == projectId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateProjects[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#projects-table tr[data-id="' + projectId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateProjects[index].isAdded) {
            infoProfile.listCandidateProjects.splice(index, 1);
        }
        loadTableDataProjects(infoProfile);
    }
}
//add thêm table
function appendTableProject(projects) {
    var projectRow = '<tr>' +
        '<td><b>' + projects.projectName + '</b></td>' +
        '<td>' + projects.customer + '</td>' +
        '<td>' + projects.timePeriod + '</td>' +
        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-project" data-id="' + projects.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-project" data-id="' + projects.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#projects-table').append(projectRow);
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// HonorAward
function loadTableDataHonor(infoProfile) {
    $("#honor-table").empty();
    var honorList = infoProfile.listCandidateHonorAwards;

    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    honorList.forEach(function (honor) {
        // Thực hiện các thao tác để hiển thị thông tin giáo dục
        if (honor.isDeleted) {

        }
        else {
            var honorRow = '<tr>' +
                '<td><b>' + honor.name + '</b></td>' +
                '<td>' + honor.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-honor" data-id="' + honor.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-honor" data-id="' + honor.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#honor-table').append(honorRow);
}
        
    });
}
//click button HonorAward
$("form").on('click', '#add-honor', function (e) {
    e.preventDefault();
    validateHonor();

})
//clear input HonorAward
function clearInputHonor() {
    $("#honor-title").val("");
    $("#honor-time").val("");
    $("#honor-about").val("");
}
//validate HonorAward
function validateHonor() {
    let errorList = [];
    if ($("#honor-title").val().length == 0 || $("#honor-title").val().length > 255) {
        errorList.push("Tên giải thưởng không được để trống và không vượt quá 255 ký tự.");
    }   
    if ($("#honor-time").val().length == 0 || $("#honor-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin danh dự và giải thưởng không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingHonorId) {
            UpdateHonor();
        } else {
            AddHonor();
        }
    }
}

//edit HonorAward
$(document).on('click', ".edit-honor", function (e) {
    e.preventDefault();
    let honorId = $(this).attr("data-id");
    if (honorId) {
        updatingHonorId = honorId;
        let honor = infoProfile.listCandidateHonorAwards.find(c => c.id == honorId);
        $("#honor-title").val(honor.name);
        $("#honor-time").val(honor.timePeriod);
        $("#honor-about").val(honor.description);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-honor").text("Cập nhật");


    }
})

// Cập nhật HonorAward
function UpdateHonor() {
    let honor = infoProfile.listCandidateHonorAwards.find(c => c.id == updatingHonorId);
    honor.isUpdated = true;
    honor.name = $("#honor-title").val();    
    honor.timePeriod = $("#honor-time").val();
    honor.description = $("#honor-about").val();
    updatingCerId = null;
    $("#add-honor").text("Thêm mới");
    clearInputHonor();
    loadTableDataHonor(infoProfile);
}
//thêm mới HonorAward
function AddHonor() {

    let newHonor = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        name: $("#honor-title").val(),        
        timePeriod: $("#honor-time").val(),
        description: $("#honor-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateHonorAwards.push(newHonor);

    // Thêm dòng mới vào bảng
    appendTableHonor(newHonor);

    // Xóa dữ liệu từ các ô input
    clearInputHonor();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingHonorId = null;
    $("#add-honor").text("Thêm mới");
}
//click icon delete HonorAward
$(document).on('click', ".delete-honor", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let honorId = $(this).attr("data-id");
    if (honorId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteHonor(honorId);
    }
});
//sự kiện xóa HonorAward
function deleteHonor(honorId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateHonorAwards.findIndex(honor => honor.id == honorId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateHonorAwards[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#honor-table tr[data-id="' + honorId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateHonorAwards[index].isAdded) {
            infoProfile.listCandidateHonorAwards.splice(index, 1);
        }
        loadTableDataHonor(infoProfile);
    }
}
//add thêm table HonorAward
function appendTableHonor(honor) {
    var honorRow = '<tr>' +
        '<td><b>' + honor.name + '</b></td>' +     
        '<td>' + honor.timePeriod + '</td>' +

        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-honor" data-id="' + honor.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-honor" data-id="' + honor.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#honor-table').append(honorRow);
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Activites
function loadTableDataActivites(infoProfile) {
    $("#activites-table").empty();
    var activitesList = infoProfile.listCandidateActivites;

    // Duyệt qua danh sách giáo dục và hiển thị thông tin
    activitesList.forEach(function (activites) {
        if (activites.isDeleted) {

        }
        else { // Thực hiện các thao tác để hiển thị thông tin giáo dục
            var activitesRow = '<tr>' +
                '<td><b>' + activites.name + '</b></td>' +
                '<td>' + activites.timePeriod + '</td>' +
                '<td>' +
                '<div class="pxp-dashboard-table-options">' +
                '<ul class="list-unstyled">' +
                '<li><button title="Edit" class="edit-activites" data-id="' + activites.id + '"><span class="fa fa-pencil"></span></button></li>' +
                '<li><button title="Delete" class="delete-activites" data-id="' + activites.id + '"><span class="fa fa-trash-o"></span></button></li>' +
                '</ul>' +
                '</div>' +
                '</td>' +
                '</tr>';

            // Thêm dòng thông tin giáo dục vào bảng
            $('#activites-table').append(activitesRow);
        }
       
    });
}
$("form").on('click', '#add-activites', function (e) {
    e.preventDefault();
    validateActivites();

})
//clear input Activites
function clearInputActivites() {
    $("#activites-title").val("");
    $("#activites-time").val("");
    $("#activites-about").val("");
}
//validate Activites
function validateActivites() {
    let errorList = [];
    if ($("#activites-title").val().length == 0 || $("#activites-title").val().length > 255) {
        errorList.push("Tên hoạt động không được để trống và không vượt quá 255 ký tự.");
    }
    if ($("#activites-time").val().length == 0 || $("#activites-time").val().length > 255) {
        errorList.push("Thời gian không được để trống và không vượt quá 255 ký tự.");
    }

    if (errorList.length > 0) {
        var contentError = "<ul>";

        errorList.forEach(function (item, index) {
            contentError += "<li class='text-start'>" + item + "</li>";
        });
        contentError += "</ul>";
        var swalSubTitle = "<p class='swal-subtitle'>" + "Cập nhật thông tin hoạt động xã hội và tình nguyện không thành công</p>";

        Swal.fire(
            'Tài khoản' + swalSubTitle,
            contentError,
            'warning'
        );
    }
    else {
        if (updatingActivitesId) {
            UpdateActivites();
        } else {
            AddActivites();
        }
    }
}

//edit Activites
$(document).on('click', ".edit-activites", function (e) {
    e.preventDefault();
    let ActivitesId = $(this).attr("data-id");
    if (ActivitesId) {
        updatingActivitesId = ActivitesId;
        let activites = infoProfile.listCandidateActivites.find(c => c.id == ActivitesId);
        $("#activites-title").val(activites.name);
        $("#activites-time").val(activites.timePeriod);
        $("#activites-about").val(activites.description);

        // Thay đổi văn bản nút "Thêm mới" thành "Cập nhật"
        $("#add-activites").text("Cập nhật");


    }
})

// Cập nhật Activites
function UpdateActivites() {
    let activites = infoProfile.listCandidateActivites.find(c => c.id == updatingActivitesId);
    activites.isUpdated = true;
    activites.name = $("#activites-title").val();
    activites.timePeriod = $("#activites-time").val();
    activites.description = $("#activites-about").val();
    updatingActivitesId = null;
    $("#add-activites").text("Thêm mới");
    clearInputActivites();
    loadTableDataActivites(infoProfile);
}
//thêm mới Activites
function AddActivites() {

    let newActivites = {
        id: generateUniqueId(), // Hãy thay thế phần này bằng cách tạo id duy nhất cho mục mới
        name: $("#activites-title").val(),
        timePeriod: $("#activites-time").val(),
        description: $("#activites-about").val(),
        isAdded: true,
    };

    // Thêm mục mới vào danh sách
    infoProfile.listCandidateActivites.push(newActivites);

    // Thêm dòng mới vào bảng
    appendTableActivites(newActivites);

    // Xóa dữ liệu từ các ô input
    clearInputActivites();

    // Đặt văn bản nút "Thêm mới" và xóa updatingEduId (nếu có)
    updatingActivitesId = null;
    $("#add-activites").text("Thêm mới");
}
//click icon delete Activites
$(document).on('click', ".delete-activites", function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định của sự kiện click
    let activitesId = $(this).attr("data-id");
    if (activitesId) {
        // Gọi hàm để xóa mục giáo dục khỏi danh sách và cập nhật bảng
        deleteActivites(activitesId);
    }
});
//sự kiện xóa Activites
function deleteActivites(activitesId) {
    // Lọc ra chỉ mục của mục giáo dục cần xóa trong danh sách

    const index = infoProfile.listCandidateActivites.findIndex(activites => activites.id == activitesId);
    if (index !== -1) {
        //Gán thuộc tính isDeleted = true cho mục giáo dục bị xóa
        infoProfile.listCandidateActivites[index].isDeleted = true;
        // Xóa dòng liên quan đến mục giáo dục khỏi bảng
        $('#activites-table tr[data-id="' + activitesId + '"]').remove();

        // Xóa mục giáo dục khỏi danh sách
        if (infoProfile.listCandidateActivites[index].isAdded) {
            infoProfile.listCandidateActivites.splice(index, 1);
        }
        loadTableDataActivites(infoProfile);
    }
}
//add thêm table Activites
function appendTableActivites(activites) {
    var activitesRow = '<tr>' +
        '<td><b>' + activites.name + '</b></td>' +
        '<td>' + activites.timePeriod + '</td>' +
        '<td>' +
        '<div class="pxp-dashboard-table-options">' +
        '<ul class="list-unstyled">' +
        '<li><button title="Edit" class="edit-activites" data-id="' + activites.id + '"><span class="fa fa-pencil"></span></button></li>' +
        '<li><button title="Delete" class="delete-activites" data-id="' + activites.id + '"><span class="fa fa-trash-o"></span></button></li>' +
        '</ul>' +
        '</div>' +
        '</td>' +
        '</tr>';

    // Thêm dòng thông tin giáo dục vào bảng
    $('#activites-table').append(activitesRow);
}
// cập nhật thông tin cá nhân của Candidate
function UpdateProfile() {
    let educations = infoProfile.listCandidateEducation;
    let workExperiences = infoProfile.listCandidateWorkExperiences;
    let certificates = infoProfile.listCandidateCertificates;
    let candidateProjects = infoProfile.listCandidateProjects;
    let honorAwards = infoProfile.listCandidateHonorAwards;
    let candidateActivites = infoProfile.listCandidateActivites;
    let Skills = infoProfile.listCandidateSkill;
    educations.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
   
    workExperiences.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
    certificates.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
    candidateProjects.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
    honorAwards.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
    candidateActivites.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })
    Skills.forEach(function (item) {
        if (item.isAdded) {
            item.id = 0;
        }
    })


    var obj = {
        /*"Id": 0,*/
        "fullName": $("#fullName").val(),
        "gender": $("#gender").val(),
        "email": $("#email").val(),
        "jobPosition": $("#job-title").val(),
        "phone": $("#phone").val(),
        "addressDetail": $("#address").val(),
        "interests": $("#hobby").val(),
        "photo": pathLogo,
        "coverPhoto": pathCover,
        "objective": $("#objective").val(),
        "educations": educations,
        "workExperiences": workExperiences,
        "certificates": certificates,
        "candidateProjects": candidateProjects,
        "honorAwards": honorAwards,
        "candidateActivites": candidateActivites,
        "candidateSkills": Skills,
    }

    var actionName = "Cập nhật thông tin";
    swal.fire({
        title: actionName,
        html: "Bạn có chắc chắn muốn " + actionName.toLowerCase() + " cho tài khoản <b>" + $("#fullName").val() + '</b>?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then((result) => {
        if (result.isConfirmed) {
            //CALL AJAX TO UPDATE
            if (updatingId > 0) {
                $("#loading").addClass("show");
                $.ajax({
                    url: systemConfig.defaultAPIURL + "api/candidate/update-profile",
                    type: "PUT",
                    contentType: "application/json",
                    data: JSON.stringify(obj),
                    beforeSend: function (xhr) {
                        if (localStorage.currentUser) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(localStorage.currentUser).token);
                        }
                    },
                    success: function (responseData) {
                        $("#loading").removeClass("show");
                        if (responseData.isSucceeded) {
                            let infoProfile = responseData.resources;
                            var swaltitlesuccess = "<p class='swal-subtitle'>" + actionName + " thành công</p>";
                            Swal.fire(
                                'Tài khoản' + swaltitlesuccess,
                                'Tài khoản <b>' + $("#fullName").val() + '</b> đã được cập nhật thành công.',
                                'success'
                            );
                        }
                        if (!responseData.isSucceeded) {
                            var listError = responseData.errors;
                            if (listError != null) {
                                var contenterror = "<ul>";
                                listError.forEach(function (item) {
                                    contenterror += "<li class='text-start'>" + item + "</li>";
                                })
                                contenterror += "</ul>";
                                var swalsubtitle = "<p class='swal-subtitle'>" + actionName + " không thành công</p>";
                                swal.fire(
                                    'Tài khoản' + swalsubtitle,
                                    contenterror,
                                    'warning'
                                );
                            } else {
                                swal.fire(
                                    'Tài khoản' + swalsubtitle,
                                    responseData.title,
                                    'warning'
                                )
                            }
                        }
                    },
                    error: function (e) {
                        $("#loading").removeClass("show");
                        swal.fire(
                            'Lỗi khi cập nhật !',
                            'Không thể cập nhật, hãy kiểm tra lại thông tin.',
                            'error'
                        );
                    }
                });
            };
        }
    });
}

$("#pxp-candidate-photo-choose-file").change(async function (e) {
    var file = (e.target.files)[0];
    var typeFile = file.type.split('/')[0];
    if (typeFile == "image") {
        if (file.size > (5 * 1024 * 1024)) {

            html = '<div style="text-align:left;">Tệp: </div>' + file.name + 'mà bạn đã chọn quá lớn. Kích thước tối đa là <strong>5</strong> MB.';

            Swal.fire({
                title: 'Lưu ý',
                icon: 'warning',
                html: html,
                focusConfirm: true,
                allowEnterKey: true

            })
        }
        else {
            var type = "avatars";
            await uploadFile(file, type);
            $(this).val("");
        }
    }
    else {
        let html = '<li>Ảnh phải có định dạng của file image ví dụ như ".jpg", ".jpeg", ".png".</li>';
        $(this).val("");
        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
})


/*
 * gọi API để upload ảnh 
 */

async function uploadFile(file, type) {
    try {
        var url =  "api/file-explorer/upload/candidates/" + type;        
        var formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            pathLogo = response.resources[0].path;
            let urlLogo = systemConfig.defaultStorage_URL + pathLogo;
            $('#pxp-candidate-photo-choose-file').next('label').css({
                'background-image': `url('${urlLogo}')`,
                'border': '0 none'
            }).find('span').hide();
        }
    } catch (res) {

    }
}

$("#pxp-candidate-cover-choose-file").change(function (e) {
    var file = (e.target.files)[0];
    var typeFile = file.type.split('/')[0];
    if (typeFile == "image") {
        if (file.size > (5 * 1024 * 1024)) {

            html = '<div style="text-align:left;">Tệp: </div>' + file.name + 'mà bạn đã chọn quá lớn. Kích thước tối đa là <strong>5</strong> MB.';

            Swal.fire({
                title: 'Lưu ý',
                icon: 'warning',
                html: html,
                focusConfirm: true,
                allowEnterKey: true

            })
        }
        else {
            var type = "cover-photo";
            uploadFileCover(file, type);
        }
    }
    else {
        let html = '<li>Ảnh phải có định dạng của file image ví dụ như ".jpg", ".jpeg", ".png".</li>';

        Swal.fire({
            title: 'Lưu ý',
            icon: 'warning',
            html: html,
            focusConfirm: true,
            allowEnterKey: true
        })
    }
})


/*
 * gọi API để upload ảnh 
 */

async function uploadFileCover(file, type) {
    try {
        var url = "api/file-explorer/upload/candidates/" + type;
        var formData = new FormData();
        formData.append("files", file);
        let response = await httpService.postFormDataAsync(url, formData);   
        if (response.isSucceeded) {
            pathCover = response.resources[0].path;
            let urlCover = systemConfig.defaultStorage_URL + pathCover;
            $('#pxp-candidate-cover-choose-file').next('label').css({
                'background-image': `url('${urlCover}')`,
                'border': '0 none'
            }).find('span').hide();
        }
    } catch (res) {
       
    }
}

