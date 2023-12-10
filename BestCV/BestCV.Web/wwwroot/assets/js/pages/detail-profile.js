
$(document).ready(function () {
    GetDetail();

});
// lấy thông tin cá nhân của candidate
function GetDetail() {
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/Candidate/detail/" + candidateId,
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            if (response.isSucceeded) {
                var infoProfile = response.resources;
                //updatingId = infoProfile.id;
                //console.log(infoProfile);
                urlCover = systemConfig.defaultStorageURL + infoProfile.coverPhoto;
                $('#cover-photo').css({
                    'background-image': `url('${urlCover}')`
                });
                //set ảnh cho logo
                urlLogo = systemConfig.defaultStorageURL + infoProfile.photo;
                $('#photo').css({
                    'background-image': `url('${urlLogo}')`
                });

                $("#fullName").html(infoProfile.fullName);
                $("#email").html(infoProfile.email);
                $("#job-title").html(infoProfile.jobPosition);
                $("#phone").html(infoProfile.phone);
                $("#location").html(infoProfile.addressDetail);            
                //$("#objective").html(infoProfile.objective);
                //Hiển thị mục tiêu
                var contentObjective = `<h2>Mục tiêu</h2>
                        <p>${infoProfile.objective}</p>`;
                $("#objective").html(contentObjective);
                //Hiển thị kỹ năng
                var contentlistSkill = "";
                infoProfile.listCandidateSkill.forEach(function (item) {
                    contentlistSkill += `<li>${item.name}</li>`;
                })
                var contentSkill = `<h2>Kỹ năng</h2>
                        <div class="pxp-single-candidate-skills">
                            <ul class="list-unstyled">
                                ${contentlistSkill}
                            </ul>
                        </div>`;
                if (infoProfile.listCandidateSkill.length > 0) {
                    $("#skill").html(contentSkill);
                }
                //Hiển thị kinh nghiệm làm việc
                var contentlistWorkExperience = "";
                infoProfile.listCandidateWorkExperiences.forEach(function (item) {
                    contentlistWorkExperience += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.jobTitle}</div>
                                    <div class="pxp-single-candidate-timeline-company pxp-text-light">${item.company}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">
                                        ${item.description === null ? "" : item.description}
                                    </div>
                                </div>
                            </div>`;
                });
                var contentWorkExperience = `<h2>Kinh nghiệm làm việc</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistWorkExperience}
                        </div>`;
                if (infoProfile.listCandidateWorkExperiences.length > 0) {
                    $("#experience").html(contentWorkExperience);
                }
                //Hiển thị giáo dục và đào tạo
                var contentlistEducation = "";
                infoProfile.listCandidateEducation.forEach(function (item) {
                    contentlistEducation += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.title}</div>
                                    <div class="pxp-single-candidate-timeline-company pxp-text-light">${item.school}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">${item.description === null ? "" : item.description}</div>
                                </div>
                            </div>`;
                });
                var contentEducation = `<h2>Giáo dục và đào tạo</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistEducation}
                        </div>`;
                if (infoProfile.listCandidateEducation.length > 0) {
                    $("#education").html(contentEducation);
                }
                //Hiển thị chứng chỉ
                var contentlistCertificates = "";
                infoProfile.listCandidateCertificates.forEach(function (item) {
                    contentlistCertificates += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.name}</div>
                                    <div class="pxp-single-candidate-timeline-company pxp-text-light">${item.issueBy}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">
                                        ${item.description === null ? "" : item.description}
                                    </div>
                                </div>
                            </div>`;
                });
                var contentCertificates = `<h2>Chứng chỉ</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistCertificates}
                        </div>`;
                if (infoProfile.listCandidateCertificates.length > 0) {
                    $("#certificate").html(contentCertificates);
                }
                //Hiển thị dự án
                var contentlistProjects = "";
                infoProfile.listCandidateProjects.forEach(function (item) {
                    contentlistProjects += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.projectName}</div>
                                    <div class="pxp-single-candidate-timeline-company pxp-text-light">${item.customer}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">
                                        ${item.info === null ? "" : item.info}
                                    </div>
                                </div>
                            </div>`;
                });
                var contentProjects = `<h2>Dự án</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistProjects}
                        </div>`;
                if (infoProfile.listCandidateCertificates.length > 0) {
                    $("#project").html(contentProjects);
                }
                //Hiển thị danh dự và giải thưởng
                var contentlistAwards = "";
                infoProfile.listCandidateHonorAwards.forEach(function (item) {
                    contentlistAwards += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.name}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">
                                        ${item.description === null ? "" : item.description}
                                    </div>
                                </div>
                            </div>`;
                });
                var contentAwards = `<h2>Danh dự và giải thưởng</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistAwards}
                        </div>`;
                if (infoProfile.listCandidateHonorAwards.length > 0) {
                    $("#honorAward").html(contentAwards);
                }
                //Hoạt động
                var contentlistActivites = "";
                infoProfile.listCandidateActivites.forEach(function (item) {
                    contentlistActivites += `<div class="pxp-single-candidate-timeline-item">
                                <div class="pxp-single-candidate-timeline-dot"></div>
                                <div class="pxp-single-candidate-timeline-info ms-3">
                                    <div class="pxp-single-candidate-timeline-time"><span class="me-3">${item.timePeriod}</span></div>
                                    <div class="pxp-single-candidate-timeline-position mt-2">${item.name}</div>
                                    <div class="pxp-single-candidate-timeline-about mt-2 pb-4">
                                        ${item.description === null ? "" : item.description}
                                    </div>
                                </div>
                            </div>`;
                });
                var contentActivites = `<h2>Hoạt động</h2>
                        <div class="pxp-single-candidate-timeline">
                            ${contentlistActivites}
                        </div>`;
                if (infoProfile.listCandidateActivites.length > 0) {
                    $("#honorAward").html(contentActivites);
                }
            }
        },
        error: function () {
            infoProfile = null;
        }
    });
}

