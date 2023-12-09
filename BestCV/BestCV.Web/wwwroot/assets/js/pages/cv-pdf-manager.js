var dataListCVPDF = [];
var pdfjsLib = window['pdfjs-dist/build/pdf'];
var workerSrc = '/lib/pdfjs/pdf.worker.js';

$(document).ready(function () {
    loadDataCandidateCVPDF();

    // Sự kiện click button upload
    $("#btnUploadCVPDF").click(() => {
        $("#inputUploadCVPDF").click();
    });

    // input upload file thay đổi
    $("#inputUploadCVPDF").change(function (e) {
        const maxFileSize = 1024 * 1024 * 25;
        if ($(this).val().length > 0) {
            var files = e.target.files;
            if (files.length > 0) {
                if (files[0].size > maxFileSize) {
                    Swal.fire({
                        title: 'Lưu ý',
                        icon: 'warning',
                        html: "CV " + files[0].name + " bạn vừa tải có kích thước quá lớn, hãy chọn tệp khác và thử lại",
                        focusConfirm: true,
                        allowEnterKey: true
                    })
                    $("#inputUploadCVPDF").val('');
                }
                else {
                    uploadFile(files[0]);
                }
            }
        }
    })
});

// Hàm upload CV PDF lên file server
async function uploadFile(file) {
    try {
        let url = "api/file-explorer/upload/candidates/cvs";
        let formData = new FormData();
        formData.append("files", file);

        let response = await httpService.postFormDataAsync(url, formData);
        if (response.isSucceeded) {
            addCVtoCandidateCVPDF(response.resources[0].path);
        }
        else {

        }
    } catch (e) {
        console.error(e);
    }
}

// Hàm thêm CV vào bảng CandidateCVPDF
async function addCVtoCandidateCVPDF(filePath) {
    try {
        let obj = {
            url: filePath,
            candidateCVPDFTypeId: 1002,
        }
        let url = "api/candidate-cv-pdf/add-cv";
        let res = await httpService.postAsync(url, obj);
        if (res.isSucceeded) {
            toastMixin.fire({
                icon: 'success',
                title: 'Tải CV lên thành công.'
            });
            let candidateCVPDF = res.resources;
            let cardCandidateCVPDF = `
                <div class="col-12 col-sm-6 col-xxl-3 pb-4" data-id="${candidateCVPDF.id}">
                    <div class="card" >
                        <canvas data-url="${candidateCVPDF.url}"></canvas>
                        <div class="card-body">
                            <h5 class="card-title" style="height: 48px;">${getNameFromUrl(candidateCVPDF.url)}</h5>
                            <p class="card-text">
                                <i class="fa-solid fa-clock text-center" style="width: 20px;" title="Thời gian tạo"></i>
                                ${moment(candidateCVPDF.createdTime).format("DD/MM/YYYY HH:mm:ss")}
                            </p>
                            <div class="d-flex" style="gap: 10px;">
                                <a type="button" class="btn btn-outline-success rounded-circle btn-function" title="Tải xuống" href="${systemConfig.defaultStorage_URL + candidateCVPDF.url}" download>
                                    <i class="fa-solid fa-download" style="font-size: 14px;"></i>
                                </a>
                                <a type="button" class="btn btn-outline-primary rounded-circle btn-function" title="Xem" href="${systemConfig.defaultStorage_URL + candidateCVPDF.url}" target="_blank">
                                    <i class="fa-solid fa-eye" style="font-size: 14px;"></i>
                                </a>
                                <button type="button" class="btn btn-outline-danger rounded-circle btn-function" title="Xóa" onclick="deleteItem(${candidateCVPDF.id})">
                                    <i class="fa-solid fa-trash" style="font-size: 14px;"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            $("#areaListCVPDFUpload").append(cardCandidateCVPDF);
            loadPDFPreview($(`div[data-id=${candidateCVPDF.id}] canvas`)[0]);
        } else {
            toastMixin.fire({
                icon: 'error',
                title: 'Tải CV lên thất bại.'
            });
        }
    } catch (e) {
        console.error(e);
    }
}

// Khai báo toast mixin
const toastMixin = Swal.mixin({
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

// Hàm load danh sách CV
async function loadDataCandidateCVPDF() {
    let res = await httpService.getAsync("api/candidate-cv-pdf/list-by-candidate-id");
    if (res.isSucceeded) {
        dataListCVPDF = res.resources;
        let strCandidateCVPDFGenerate = "";
        let strCandidateCVPDFUpload = "";
        for (let candidateCVPDF of dataListCVPDF) {
            let strCandidateCVPDF = `
                <div class="col-12 col-sm-6 col-xxl-3 pb-4" data-id="${candidateCVPDF.id}">
                    <div class="card" >
                        <canvas data-url="${candidateCVPDF.url}"></canvas>
                        <div class="card-body">
                            <h5 class="card-title" style="height: 48px;">${getNameFromUrl(candidateCVPDF.url)}</h5>
                            <p class="card-text">
                                <i class="fa-solid fa-clock text-center" style="width: 20px;" title="Thời gian tạo"></i>
                                ${moment(candidateCVPDF.createdTime).format("DD/MM/YYYY HH:mm:ss")}
                            </p>
                            <div class="d-flex" style="gap: 10px;">
                                <a type="button" class="btn btn-outline-success rounded-circle btn-function" title="Tải xuống" href="${systemConfig.defaultStorage_URL + candidateCVPDF.url}" download>
                                    <i class="fa-solid fa-download" style="font-size: 14px;"></i>
                                </a>
                                <a type="button" class="btn btn-outline-primary rounded-circle btn-function" title="Xem" href="${systemConfig.defaultStorage_URL + candidateCVPDF.url}" target="_blank">
                                    <i class="fa-solid fa-eye" style="font-size: 14px;"></i>
                                </a>
                                <button type="button" class="btn btn-outline-danger rounded-circle btn-function" title="Xóa" onclick="deleteItem(${candidateCVPDF.id})">
                                    <i class="fa-solid fa-trash" style="font-size: 14px;"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            if (candidateCVPDF.candidateCVPDFTypeId == defaultTypeGenerate) {
                strCandidateCVPDFGenerate += strCandidateCVPDF;
            } else if (candidateCVPDF.candidateCVPDFTypeId == defaultTypeUpload) {
                strCandidateCVPDFUpload += strCandidateCVPDF;
            }
        }

        $("#areaListCVPDFGenerate").html(strCandidateCVPDFGenerate ? strCandidateCVPDFGenerate : `
            <p>Bạn chưa tạo CV nào.</p>
        `);
        $("#areaListCVPDFUpload").html(strCandidateCVPDFUpload ? strCandidateCVPDFUpload : `
            <p>Bạn chưa tải lên CV nào.</p>
        `);

        $("canvas").each(function () {
            loadPDFPreview(this);
        });

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Lấy danh sách CV',
            html: `
                Có lỗi trong quá trình tải danh sách CV.
                <br>
                Vui lòng liên hệ quản trị viên để báo cáo lỗi.
            `
        });
    }
}

// Hàm load PDF preview
function loadPDFPreview(element) {
    let url = systemConfig.defaultStorage_URL + "/" + $(element).attr("data-url");
    pdfjsLib.GlobalWorkerOptions.workerSrc = workerSrc;
    let loadingTask = pdfjsLib.getDocument(url);
    loadingTask.promise.then(function (pdf) {
        pdf.getPage(1).then(function (page) {
            let viewport = page.getViewport({ scale: 1.5 });

            let canvas = element
            let context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            let renderContext = {
                canvasContext: context,
                viewport: viewport
            };
            page.render(renderContext);
        });
    }, function (reason) {
        console.error(reason);
    });
}

// Hàm xóa CV
async function deleteItem(id) {
    let candidateCVPDF = dataListCVPDF.find(e => e.id == id);
    Swal.fire({
        icon: 'question',
        title: 'Xóa CV',
        html: `Bạn có chắc chắn muốn xóa CV <strong>${getNameFromUrl(candidateCVPDF.url)}</strong>?`,
        showCancelButton: true,
        cancelButtontext: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then(async (result) => {
        if (result.isConfirmed) {
            let res = await httpService.deleteAsync(`api/candidate-cv-pdf/delete/${id}`);
            if (res.isSucceeded) {
                dataListCVPDF = dataListCVPDF.filter(e => e.id != id);
                $(`#areaListCVPDF div[data-id=${id}]`).remove();
                Swal.fire({
                    icon: 'success',
                    title: 'Xóa CV',
                    html: `CV <strong>${getNameFromUrl(candidateCVPDF.url)}</strong> đã được xóa thành công.`
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Xóa CV',
                    html: `
                        Có lỗi trong quá trình xóa CV <strong>${getNameFromUrl(candidateCVPDF.url)}</strong>.
                        <br>
                        Liên hệ quản trị viên website để báo cáo lỗi.
                    `
                })
            }
        }
    });
}

// Hàm lấy tên CV theo url
function getNameFromUrl(url) {
    let arr = url.split('/');
    return arr[arr.length - 1];
}