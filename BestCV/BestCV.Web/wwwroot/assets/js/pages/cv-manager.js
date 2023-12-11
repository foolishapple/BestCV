var dataListCV = [];

$(document).ready(function () {
    loadDataCandidateCV();
});

// Hàm load danh sách CV
async function loadDataCandidateCV() {
    let res = await httpService.getAsync("api/candidate-cv/my-list");
    if (res.isSucceeded) {
        dataListCV = res.resources;
        let strCandidateCV = "";
        for (let candidateCV of dataListCV) {
            strCandidateCV += `
                <div class="col-12 col-sm-6 col-xxl-3 pb-4" data-candidate-cv-id="${candidateCV.id}">
                    <div class="card" >
                        <img src="${candidateCV.cvTemplatePhoto ? candidateCV.cvTemplatePhoto : "/cv-builder/images/cv-thumbnail/blank.png"}" class="card-img-top" alt="${candidateCV.name}">
                        <div class="card-body">
                            <div class="d-flex justify-content-between">
                                <h5 class="card-title">${candidateCV.name}</h5>
                                <div class="d-flex" style="gap: 10px;">
                                    <button type="button" class="btn btn-outline-primary rounded-circle btn-function" title="Chỉnh sửa" onclick="editItem(${candidateCV.id})">
                                        <i class="fa-solid fa-pen" style="font-size: 14px;"></i>
                                    </button>
                                    <button type="button" class="btn btn-outline-danger rounded-circle btn-function" title="Xóa" onclick="deleteItem(${candidateCV.id})">
                                        <i class="fa-solid fa-trash" style="font-size: 14px;"></i>
                                    </button>
                                </div>
                            </div>
                            <p class="card-text"> 
                                <i class="fa-solid fa-file-lines text-center" style="width: 20px;" title="Mẫu"></i>
                                ${candidateCV.cvTemplateName ? candidateCV.cvTemplateName : "Không dùng mẫu"}
                                <br>
                                <i class="fa-solid fa-clock text-center" style="width: 20px;" title="Cập nhật lần cuối"></i>
                                ${moment(candidateCV.modifiedTime).format("DD/MM/YYYY HH:mm:ss")}
                            </p>
                        </div>
                    </div>
                </div>
            `
        }

        $("#areaListCV").html(strCandidateCV);

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

// Hàm edit CV - đưa đến trang edit CV
function editItem(id) {
    window.location.href = `/viet-cv/${id}`;
}

// Hàm xóa CV
async function deleteItem(id) {
    let candidateCV = dataListCV.find(e => e.id == id);
    Swal.fire({
        icon: 'question',
        title: 'Xóa CV',
        html: `Bạn có chắc chắn muốn xóa CV <strong>${candidateCV.name}</strong>?`,
        showCancelButton: true,
        cancelButtontext: 'Hủy',
        confirmButtonText: 'Xóa'
    }).then(async (result) => {
        if (result.isConfirmed) {
            let res = await httpService.deleteAsync(`api/candidate-cv/delete/${id}`);
            if (res.isSucceeded) {
                dataListCV = dataListCV.filter(e => e.id != id);
                $(`#areaListCV div[data-candidate-cv-id=${id}]`).remove();
                Swal.fire({
                    icon: 'success',
                    title: 'Xóa CV',
                    html: `CV <strong>${candidateCV.name}</strong> đã được xóa thành công.`
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Xóa CV',
                    html: `
                        Có lỗi trong quá trình xóa CV <strong>${candidateCV.name}</strong>.
                        <br>
                        Liên hệ quản trị viên website để báo cáo lỗi.
                    `
                })
            }
        }
    });
}