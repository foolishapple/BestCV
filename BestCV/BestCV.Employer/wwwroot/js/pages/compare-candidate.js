// Get a reference to the PDF container
const containers_1 = document.getElementById('card-body-1');
const containers_2 = document.getElementById('card-body-2');

// Path to your PDF file
var pdfPath_1 = "" ;
var pdfPath_2 = "";

$(document).ready(function () {
    //loadPDFCVLeft();
    //loadPDFCVRight();

    loadDifferenceCandidate();
    loadDataCurrentCandidate();
    getCandidateApplyJobStatus();
    loadDataDetailJob();
    //tìm kiếm 
    $("#select-more-candidate").on("change", function () {
        //console.log($(this).val())
        let id = $(this).val()
        let item = dataSourceDifferenceCandidate.find(x => x.id == id);
        $("#btn_change_status_right, #btn_add_description_right").attr('data-id', id)

        pdfPath_2 = "https://storage-jobi.dion.vn" + item.candidateCVPDFUrl;
        loadPDFCVRight();
    })

    //nút action bên trái
    $(document).on("click", "#btn_change_status_left", function (e) {

        if (dataCurrentCandidate) {
            $("#ApplyJobDescription").val(dataCurrentCandidate.description).trigger("change");
            $("#pills-tab-status .active").removeClass("active");
            $(`#pills-tab-status .nav-link[data-id=${dataCurrentCandidate.candidateApplyJobStatusId}]`).addClass("active");
            $("#submit-status").attr('data-id', dataCurrentCandidate.id)
            $("#modal_change_status").modal("show");
        }
    })
    $(document).on("click", "#btn_add_description_left", function (e) {
            if (dataCurrentCandidate) {
                $("#modal_addNote #candidateApplyJobDescription").val(dataCurrentCandidate.description);
                $("#submit-description").attr('data-id', dataCurrentCandidate.id)

                $("#modal_addNote").modal("show");
            }
    })
    //nút action bên phải
    $(document).on("click", "#btn_change_status_right", function (e) {
        let id = $(this).attr('data-id');
        let item = dataSourceDifferenceCandidate.find(x => x.id == id);
        if (item) {
            $("#ApplyJobDescription").val(item.description).trigger("change");
            $("#pills-tab-status .active").removeClass("active");
            $(`#pills-tab-status .nav-link[data-id=${item.candidateApplyJobStatusId}]`).addClass("active");
            $("#submit-status").attr('data-id', item.id)

            $("#modal_change_status").modal("show");
        }
    })
    $(document).on("click", "#btn_add_description_right", function (e) {
        let id = $(this).attr("data-id");
        let item = dataSourceDifferenceCandidate.find(x => x.id == id);
        if (item) {
            $("#modal_addNote #candidateApplyJobDescription").val(item.description);
            $("#submit-description").attr('data-id', item.id)
            $("#modal_addNote").modal("show");
        }
    })
})

function loadPDFCVLeft() {
    $("#card-body-1").html("");
    // Load the PDF
    pdfjsLib.getDocument(pdfPath_1).promise.then(pdf => {
        const numPages = pdf.numPages;

        // Loop through all pages and render them
        for (let pageNum = 1; pageNum <= numPages; pageNum++) {
            pdf.getPage(pageNum).then(page => {
                var desiredWidth = containers_1.offsetWidth - 10;
                var viewport = page.getViewport({ scale: 1, });
                var scale = desiredWidth / viewport.width;
                var scaledViewport = page.getViewport({ scale: scale, });

                // Create a canvas element to render the page
                const canvas = document.createElement('canvas');
                const context = canvas.getContext('2d');
                canvas.width = scaledViewport.width;
                canvas.height = scaledViewport.height;
                // Append the canvas to the container
                containers_1.appendChild(canvas);

                // Render the PDF page to the canvas
                const renderContext = {
                    canvasContext: context,
                    viewport: scaledViewport
                };
                page.render(renderContext);
            });
        }
    }).catch(error => {
        console.error('Error loading PDF:', error);
    });
}
function loadPDFCVRight() {
    $("#card-body-2").html('')
    // Load the PDF
    pdfjsLib.getDocument(pdfPath_2).promise.then(pdf => {
        const numPages = pdf.numPages;

        // Loop through all pages and render them
        for (let pageNum = 1; pageNum <= numPages; pageNum++) {
            pdf.getPage(pageNum).then(page => {

                var desiredWidth = containers_2.offsetWidth
                var viewport = page.getViewport({ scale: 1, });
                var scale = desiredWidth / viewport.width;
                var scaledViewport = page.getViewport({ scale: scale, });

                // Create a canvas element to render the page
                const canvas = document.createElement('canvas');
                const context = canvas.getContext('2d');
                canvas.width = scaledViewport.width;
                canvas.height = scaledViewport.height;
                // Append the canvas to the container
                containers_2.appendChild(canvas);

                // Render the PDF page to the canvas
                const renderContext = {
                    canvasContext: context,
                    viewport: scaledViewport
                };
                page.render(renderContext);
            });
        }
    }).catch(error => {
        console.error('Error loading PDF:', error);
    });
}
var dataSourceDifferenceCandidate = [];
async function loadDifferenceCandidate() {
    try {
        let result = await httpService.getAsync("api/candidate-apply-job/get-list-candidate-apply-to-job/" + jobId + "/" + candidateApplyJobId);
        if (result.isSucceeded) {
            var data = [];
            dataSourceDifferenceCandidate = result.resources;
            result.resources.forEach(function (item) {
                //console.log(item)
                //$('#select-more-candidate').append(new Option(item.id, item.id, false, false)).trigger('change');
                let contentHtml = `<div class="card-cv-header d-flex ">
                                    <div style="background-image: url(${systemConfig.defaultStorage_URL + item.candidateAvatar});" class="pxp-cover bg-secondary bg-image" >
                                    </div>
                                    <div class="d-flex flex-column">
                                        <div><span class="fw-bold me-2">${item.candidateName} </span><span style="background-color: ${customBagdeColor(item.candidateApplyJobStatusColor)} ; color: ${item.candidateApplyJobStatusColor}; padding: 2px 4px; border-radius: 5px; font-size: 13px">${item.candidateApplyJobStatusName}</span></div>
                                        <span>${item.candidateEmail} | ${item.candidatePhone}</span>
                                    </div>
                                </div>`;
                let contentText =  `<div class="d-flex align-items-center p-2">
                                        <div style="background-image: url(${systemConfig.defaultStorage_URL + item.candidateAvatar});" class="pxp-cover bg-secondary bg-image-mini" >
                                        </div>
                                        <span class="fw-bold">${item.candidateName}</span>
                                     </div>`
                data.push({
                    id: item.id,
                    html: contentHtml,
                    text: contentText,
                })   

    
            })

            //custom hiển thị select2
            $('#select-more-candidate').select2({
                data: data,
                escapeMarkup: function (markup) {
                    return markup;
                },
                templateResult: function (data) {
                    return data.html;
                },
                templateSelection: function (data) {
                    return data.text;
                },
                //templateSelection: formatRepoSelection
            });

            //xóa attr title khi hover vào select2
            $(".select2-selection__rendered").hover(function () {
                $(this).removeAttr('title');
            });

            $('#select-more-candidate').trigger('change')
        }
    } catch (e) {
        console.error(e);
    }
}
var dataCurrentCandidate = [];
async function loadDataCurrentCandidate() {
    try {
        let result = await httpService.getAsync('api/candidate-apply-job/detail-by-id/' + jobId + "/" + candidateApplyJobId);
        if (result.isSucceeded) {
            //console.log(result);
            dataCurrentCandidate = result.resources;
            $("#avatar-current").css("background-image", "src(" + systemConfig.defaultStorage_URL + result.resources.candidateCVPDFUrl+ ")");
            $("#fullName-current").text(result.resources.candidateName);
            //load data info
            let content = "";
            if (result.resources.candidateEmail != null && result.resources.candidateEmail != "" && result.resources.candidatePhone != null && result.resources.candidatePhone != "") {
                content = result.resources.candidateEmail + ' | ' + result.resources.candidatePhone;
            }
            else if (result.resources.candidateEmail != null && result.resources.candidateEmail != "" && (result.resources.candidatePhone != null || result.resources.candidatePhone != "")) {
                content = result.resources.candidateEmail;

            }
            else if ((result.resources.candidateEmail != null || result.resources.candidateEmail != "") && result.resources.candidatePhone != null && result.resources.candidatePhone != "") {
                content = result.resources.candidatePhone;
            }
            $("#info-current").text(content)

            $("#address-current").text(result.resources.CandidateAddress != null ? result.resources.CandidateAddress : "")

            $("#btn_change_status_left, #btn_add_description_left").attr('data-id', result.resources.id)
            pdfPath_1 = "https://storage-jobi.dion.vn" + result.resources.candidateCVPDFUrl;
            loadPDFCVLeft();
        }
    } catch (e) {
        console.error(e)
    }
}

async function getCandidateApplyJobStatus() {
    try {
        let result = await httpService.getAsync("api/candidate-apply-job-status/list-all");
        if (result.isSucceeded) {
            candidateApplyJobStatusSource = result.resources;
        }
        else {
            candidateApplyJobStatusSource = []
        }
    } catch (e) {
        candidateApplyJobStatusSource = [];
        console.error(e);
    }
    if (candidateApplyJobStatusSource.length > 0) {
        candidateApplyJobStatusSource.forEach(function (item) {
            $('#pills-tab-status').append(`<li class="nav-item" role="presentation">
                            <button class="btn active nav-link me-1 mb-1" data-id="${item.id}" type="button" data-bs-toggle="pill" role="tab" aria-controls="pills-status" aria-selected="false">${item.name}</button>
                        </li>`);
            $("#fillterStatus").append(new Option(item.name, item.id, false, false)).trigger("change");
        })
    }
    $("#fillterStatus").select2({
        placeholder: "Lọc theo trạng thái...",
        allowClear: true
    })
}

$("#modal_change_status .btn_submit").on("click", async function (e) {
    let id = $(this).attr('data-id')
    swal.fire({
        title: "Ứng viên",
        html: "Bạn có chắc chắn muốn cập nhật trạng thái CV ứng viên ?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then(async (result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            try {
                let obj = {
                    id: id,
                    description: $("#ApplyJobDescription").val(),
                    candidateApplyJobStatusId: $("#pills-tab-status .active").attr("data-id"),
                };

                let result = await httpService.putAsync("api/candidate-apply-job/change-status", obj);
                $("#loading").removeClass("show");
                if (result.isSucceeded) {
                    Swal.fire("Ứng viên", "Trạng thái CV đã được cập nhật thành công!", "success");
                    //table.draw();
                    if (id == candidateApplyJobId) {
                        loadDataCurrentCandidate();
                    }
                    else {
                        //$("#select-more-candidate").val(id).trigger('change')
                        loadDifferenceCandidate();
                    }
                    $("#modal_change_status").modal("hide");
                }
                else {
                    if (result.status == 400) {
                        let contentError = "<ul>";
                        let errorList = result.errors;
                        errorList.forEach(function (item, index) {
                            contentError += "<li class='text-start'>" + item + "</li>";
                        });
                        contentError += "</ul>";
                        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật trạng thái CVkhông thành công</p>";
                        Swal.fire(
                            'Ứng viên' + swalSubTitle,
                            contentError,
                            'warning'
                        );
                    }
                    else {
                        Swal.fire("Cập nhật trạng thái CV ứng viên thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                    }
                }
            } catch (e) {
                Swal.fire("Cập nhật trạng thái CV ứng viên thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                console.error("e");
                $("#loading").removeClass("show");
            }
        }
    })

})

$("#modal_addNote .btn_submit").on("click", async function (e) {
    let id = $(this).attr('data-id')

    swal.fire({
        title: "Ứng viên",
        html: "Bạn có chắc chắn muốn cập nhật ghi chú ?",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#443',
        cancelButtonText: 'Hủy',
        confirmButtonText: 'Lưu'
    }).then(async (result) => {
        if (result.isConfirmed) {
            $("#loading").addClass("show");
            try {
                let obj = {
                    id: id,
                    description: $("#candidateApplyJobDescription").val(),
                };
                let result = await httpService.putAsync("api/candidate-apply-job/add-description", obj);
                $("#loading").removeClass("show");
                if (result.isSucceeded) {
                    Swal.fire("Ứng viên", "Ghi chú đã được cập nhật thành công!", "success");
                    if (id == candidateApplyJobId) {
                        loadDataCurrentCandidate();
                    }
                    else {
                        //$("#select-more-candidate").val(id).trigger('change')
                        loadDifferenceCandidate();
                    }
                    $("#modal_addNote").modal("hide");
                }
                else {
                    if (result.status == 400) {
                        let contentError = "<ul>";
                        let errorList = result.errors;
                        errorList.forEach(function (item, index) {
                            contentError += "<li class='text-start'>" + item + "</li>";
                        });
                        contentError += "</ul>";
                        let swalSubTitle = "<p class='swal__admin__subtitle'>Cập nhật ghi chú không thành công</p>";
                        Swal.fire(
                            'Ứng viên' + swalSubTitle,
                            contentError,
                            'warning'
                        );
                    }
                    else {
                        Swal.fire("Cập nhật ghi chú không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                    }
                }
            } catch (e) {
                Swal.fire("Cập nhật ghi chú không thành công", "Đã có lỗi xảy ra, xin vui lòng thử lại sau!", "error");
                console.error("e");
                $("#loading").removeClass("show");
            }
        }
    })

})

async function loadDataDetailJob() {
    try {
        let result = await httpService.getAsync("api/job/detail/" + jobId);
        if (result.isSucceeded) {
            $("#job-name").text(result.resources.name)
        }
    } catch (e) {
        console.error(e)
    }
}


function customBagdeColor(color) {
    var percent = 90;
    var fontColor = "";
    var backColor = color;
    // strip the leading # if it's there
    color = color.replace(/^\s*#|\s*$/g, '');

    // convert 3 char codes --> 6, e.g. `E0F` --> `EE00FF`
    if (color.length == 3) {
        color = color.replace(/(.)/g, '$1$1');
    }

    var r = parseInt(color.substr(0, 2), 16),
        g = parseInt(color.substr(2, 2), 16),
        b = parseInt(color.substr(4, 2), 16);

    return '#' +
        ((0 | (1 << 8) + r + (256 - r) * percent / 100).toString(16)).substr(1) +
        ((0 | (1 << 8) + g + (256 - g) * percent / 100).toString(16)).substr(1) +
        ((0 | (1 << 8) + b + (256 - b) * percent / 100).toString(16)).substr(1);
}