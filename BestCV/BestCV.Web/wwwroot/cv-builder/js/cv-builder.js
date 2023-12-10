var $focusElement = null;
var $selectedCol = null;
var candidate = null;
var listCVTemplate = [];
var currentTemplateId = null;
var currentTemplate = null;
var tempTemplateId = 0;


$(document).ready(function () {
    // Kích hoạt tooltip
    const tooltipCopy = new bootstrap.Tooltip('#btnCopy');
    const tooltipAddPadding = new bootstrap.Tooltip('#btnAddPadding');
    const tooltipRemovePadding = new bootstrap.Tooltip('#btnRemovePadding');
    const tooltipChangeLayout = new bootstrap.Tooltip('#btn-change-layout');
    const tooltipRemoveElement = new bootstrap.Tooltip('#btn-remove-element');



 

    // Xử lý dữ liệu cho select font-family
    for (key in fontFamilies) {
        $("#selectFontFamily").append(`
            <option value="${key}">${fontFamilies[key]}</option>
        `);
    }
    $("#selectFontFamily").val("");
    $("#selectFontFamily").select2({
        templateResult: templateSelectFontFamily
    });

    // Xử lý dữ liệu cho select font-size
    for (key in fontSizes) {
        $("#selectFontSize").append(`
            <option value="${key}">${fontSizes[key]}</option>
        `);
    }
    $("#selectFontSize").val("");
    $("#selectFontSize").select2({
        templateResult: templateSelectFontSize
    });

    // Xử lý dữ liệu cho select line-height
    for (key in lineHeights) {
        $("#selectLineHeight").append(`
            <option value="${key}">${lineHeights[key]}</option>
        `);
    }
    $("#selectLineHeight").val("");
    $("#selectLineHeight").select2();


    // Sự kiện click vào từng column layout trong danh sách layout
    $(".cv-column-layout-item").click(function () {
        $(".cv-column-layout-item").removeClass("active");
        $(this).addClass("active");
    });

    // Sự kiện click vào từng Item trong danh sách Item
    $(".cv-list_item__item").click(function () {
        $(".cv-list_item__item .right-side").addClass("d-none");
        $(".cv-list_item__item").removeClass("active");
        $(this).addClass("active");
        $(this).find(".right-side").removeClass("d-none");
    });

    // Sự kiện click nút chọn layout
    $("#btnChooseColumnLayout").click(function () {
        // Lấy tên column layout
        let columnLayoutName = $(".cv-column-layout .cv-column-layout-item.active p").text();

        // Lấy dòng chứa nút thêm hàng mới
        let $row = $("#cv-btn-add-row").parent();

        // Chèn dòng mới
        $(genColumnFromLayoutName(columnLayoutName)).insertBefore($row);

        // Ẩn modal thêm hàng
        $("#modalEditRow").modal("hide");
    });

    // Sự kiện click nút chọn item
    $("#btnChooseItem").click(function () {
        let dataItemName = $(".cv-list_item__item.active").attr("data-item-name");
        let item = itemHtml[dataItemName];
        $selectedCol.append(item).removeClass("cv-col-empty");
        $("#modalAddItem").modal("hide");
    });

    // Sự kiện thay đổi select chọn font
    $("#selectFontFamily").change(function () {
        if ($focusElement) {
            $focusElement.css("fontFamily", $("#selectFontFamily").val());
        }
    });

    // Sự kiện thay đổi input font-size
    $("#selectFontSize").change(function () {
        if ($focusElement) {
            $focusElement.css("fontSize", $("#selectFontSize").val());
            let lineHeight = $("#selectLineHeight").val();
            $focusElement.css("lineHeight", parseInt($("#selectFontSize").val()) * lineHeight + 'px')
        }
    });

    // Sự kiện thay đổi line-height
    $("#selectLineHeight").change(function () {
        if ($focusElement) {
            let fontSize = parseInt($focusElement.css("fontSize"));
            $focusElement.css("lineHeight", ($("#selectLineHeight").val() * fontSize) + "px");
        }
    });

    // Sự kiện click button thay đổi background-color
    $("#btnSelectColor").click(function () {
        $('#selectColor')[0].click();
    });

    // Sự kiện thay đổi color
    $('#selectColor').on('input', function () {
        if ($focusElement) {
            $focusElement.css("color", $("#selectColor").val());
        }
    });

    // Sự kiện click button thay đổi background-color
    $("#btnSelectBackgroundColor").click(function () {
        $('#selectBackgroundColor')[0].click();
    });

    // Sự kiện thay đổi background-color
    $('#selectBackgroundColor').on('input', function () {
        if ($focusElement) {
            $focusElement.css("backgroundColor", $("#selectBackgroundColor").val());
        }
    });

    // Sự kiện click button Bold
    $("#btnBold").click(function () {
        if ($focusElement) {
            let html = $focusElement.html();
            $(this).toggleClass("active");
            if ($(this).hasClass("active")) {
                $focusElement.html(`<b>${html}</b>`);
            } else {
                $focusElement.html(html.replace("<b>", "").replace("</b>", ""));
            }
        }
    });

    // Sự kiện click button Italic
    $("#btnItalic").click(function () {
        if ($focusElement) {
            let html = $focusElement.html();
            $(this).toggleClass("active");
            if ($(this).hasClass("active")) {
                $focusElement.html(`<i>${html}</i>`);
            } else {
                $focusElement.html(html.replace("<i>", "").replace("</i>", ""));
            }
        }
    });

    // Sự kiện click button Underline
    $("#btnUnderline").click(function () {
        if ($focusElement) {
            let html = $focusElement.html();
            $(this).toggleClass("active");
            if ($(this).hasClass("active")) {
                $focusElement.html(`<u>${html}</u>`);
            } else {
                $focusElement.html(html.replace("<u>", "").replace("</u>", ""));
            }
        }
    });

    // Sự kiện click button Strikethrough
    $("#btnStrikeThrough").click(function () {
        if ($focusElement) {
            let html = $focusElement.html();
            $(this).toggleClass("active");
            if ($(this).hasClass("active")) {
                $focusElement.html(`<del>${html}</del>`);
            } else {
                $focusElement.html(html.replace("<del>", "").replace("</del>", ""));
            }
        }
    });

    // Sự kiện click các button text-align
    $(".cv-edit-text-align").click(function () {
        if ($focusElement) {
            $(".cv-edit-text-align").removeClass("active");
            $(this).addClass("active");
            $focusElement.css("textAlign", $(this).attr("data-text-align"));
        }
    });

    // Sự kiện click nút tăng khoảng cách lề
    $("#btnAddPadding").click(function () {
        if ($focusElement) {
            $focusElement.addClass("padding");
        }
    });

    // Sự kiện click nút giảm khoảng cách lề
    $("#btnRemovePadding").click(function () {
        if ($focusElement) {
            $focusElement.removeClass("padding");
        }
    });

    // Sự kiện click button xóa phần tử
    $("#btn-remove-element").click(function () {
        if ($focusElement) {
            $("#modalConfirmRemoveElement").modal("show");
        }
    });

    // Sự kiện click button xác nhận xóa
    $("#btnConfirmRemoveElement").click(function () {
        if ($focusElement) {
            $focusElement.remove();
            $("#cv-context-menu-horizontal").addClass("d-none");
            $("#cv-context-menu-vertical").addClass("d-none");
            $("#modalConfirmRemoveElement").modal("hide");
        }
    });

    // Sự kiện click button đi xuống
    $("#btnGoDown").click(function () {
        if ($focusElement && $focusElement.hasClass("movable")) {
            let $nextElement = $focusElement.next();
            if ($nextElement.hasClass("movable")) {
                $nextElement.after($focusElement);
                changeFocusElement();
            }
        }
    });

    // Sự kiện click button đi lên
    $("#btnGoUp").click(function () {
        if ($focusElement && $focusElement.hasClass("movable")) {
            let $beforeElement = $focusElement.prev();
            if ($beforeElement.hasClass("movable")) {
                $beforeElement.before($focusElement);
                changeFocusElement();
            }
        }
    });

    // Sự kiện click button copy
    $("#btnCopy").click(function () {
        if ($focusElement && $focusElement.hasClass("copyable")) {
            let $copyElement = $focusElement.clone();
            $focusElement.after($copyElement);
            $focusElement = $copyElement;
            changeFocusElement();
        }
    });

    // Sự kiện click button đi đến tiêu đề mục
    $("#btn-go-to-header").click(function () {
        onClickElement($focusElement.parents(".cv-item-header")[0]);
    });

    // Sự kiện click button đi đến thân mục
    $("#btn-go-to-body").click(function () {
        onClickElement($focusElement.parents(".cv-item-body")[0]);
    });

    // Sự kiện click button đi đến mục
    $("#btn-go-to-item").click(function () {
        let $item = $focusElement.parents(".cv-item").first();
        $item.append($("#btnAddItem"));
        onClickElement($focusElement.parents(".cv-item")[0]);
    });

    // Sự kiện click button đi đến cột
    $("#btn-go-to-column").click(function () {
        onClickElement($focusElement.parents(".cv-col")[0]);
    });

    // Sự kiện click button đi đến hàng
    $("#btn-go-to-row").click(function () {
        onClickElement($focusElement.parents(".cv-row")[0]);
    });

    // Sự kiện click button thoát
    $("#btn-exit").click(function () {
        $focusElement = null;
        $(".cv-focus").removeClass("cv-focus");
        $("#cv-context-menu-horizontal").addClass("d-none");
        $("#cv-context-menu-vertical").addClass("d-none");
    });

    $("#modalSelectTemplate .modal-body .card").click(function () {
        $("#modalSelectTemplate .modal-body .card").removeClass("active");
        $(this).addClass("active");
    });

    // Sự kiện chọn template
    $("#btnChooseTemplate").click(async function () {
        let templateId = $("#modalSelectTemplate .modal-body .card.active").attr("data-template-id");
        let res = await httpService.getAsync(`api/cv-template/detail/${templateId}`);
        if (res) {
            if (res.status == 200) {
                //console.log(res.resources);
                currentTemplate = res.resources;
                currentTemplateId = currentTemplate.id;
                
                if (candidateCVId) {
                    loadData();
                }
                else {
                    implementTemplate(currentTemplate);
                    loadCandidateData();
                }
                $("#modalSelectTemplate").modal("hide");
            } else if (res.status == 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Chọn mẫu',
                    html: 'Không thể tải được mẫu.<br>Vui lòng thử lại sau.'
                });
            }
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Chọn mẫu',
                html: 'Không thể tải được mẫu.<br>Vui lòng thử lại sau.'
            });
        }
    });

    // Sự kiện click button chọn template
    $("#btnSelectTemplate").click(async function () {
        $("#modalSelectTemplate").modal("show");
    });

    // Sự kiện click button PrintPreview
    $("#btnPrintPreview").click(function () {
        var params = [
            'height=' + screen.height,
            'width=' + screen.width
        ].join(',');
        var a = window.open('', '', 'width=2000, height=2000');
        a.moveTo(0, 0);
        a.document.write('<html><body style="padding: 0; margin: 0"><div">');
        a.document.write(`
            <script src=
            "https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js">
            </script>
            <script src=
        "https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js">
            </script>
            <script src=
            "https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js">
            </script>
        `)
        a.document.write(`<link rel="stylesheet" href="/cv-builder/css/style.css">`)
        let additionalCss = currentTemplate.additionalCSS
        a.document.write(`<style>${additionalCss}</style>`)
        a.document.write($(".cv-page").first().html());
        a.document.write('</div></body></html>');
        a.document.close();
        a.print();
    });

    // Sự kiện click button lưu CV
    $("#btnSave").click(async function () {
        // Nếu đã có tên sẵn trong inputCandidateCVName
        if ($("#inputCandidateCVName").val()) {
            let candidateCV = {
                id: candidateCVId,
                name: $("#inputCandidateCVName").val(),
                cVTemplateId: currentTemplateId,
                content: $(".cv-page").html(),
                additionalCss: $("style.additional-css").text()
            }

            saveCV(candidateCV);
        }
        // Nếu không có sẵn tên trong inputCandidateCVName thì hiện modal yêu cầu nhập tên CV
        else {
            $("#modalSave .modal-body .feedback").addClass("d-none");
            $("#modalSave").modal("show");
        }
    });

    // Sự kiện click button lưu CV trong modal
    $("#modalBtnSave").click(async function () {
        // Nếu đã nhập tên CV trong modal
        if ($("#modalInputCandidateCVName").val()) {
            let candidateCV = {
                id: candidateCVId,
                name: $("#modalInputCandidateCVName").val(),
                cVTemplateId: currentTemplateId,
                content: $(".cv-page").html(),
                additionalCss: $("style.additional-css").text()
            }

            saveCV(candidateCV);
        }
        // Nếu chưa nhập tên CV trong modal
        else {
            $("#modalSave .modal-body .feedback").removeClass("d-none");
        }
    });

    // Sự kiện keyup ô nhập tên CV trong modal
    $("#modalInputCandidateCVName").keyup(function () {
        if ($(this).val()) {
            $("#modalSave .modal-body .feedback").addClass("d-none");
        } else {
            $("#modalSave .modal-body .feedback").removeClass("d-none");
        }
    });

    //$("#btnPrint").click(function() {
    //    //debugger;
    //    var obj = {
    //        name: $("#inputCandidateCVName").val(),
    //        additionalCSS: $("style.additional-css").text().replaceAll('\\','\ '),
    //        content: $(".cv-page").html()
    //    }
    //    $.ajax({
    //        type: 'POST',
    //        contentType: "application/json",
    //        url: "/print-to-pdf",
    //        data: JSON.stringify(obj),
    //        success: function (res) {
    //            console.log(res)
    //            window.open(res)
    //        },
    //        error: function (e) {
    //            //console.error(e);
    //        }
    //    });
    //    //window.open("/print-to-pdf/" + obj.name + "/" + obj.content + "/" + obj.additionalCSS)
    //});

    $("#btnPrint").click(function () {
        //var img = $(".cv-item-avatar img").attr('src');
        var img = $("#profile-upload").css('background-image');
        img = img.replace('url(', '').replace(')', '').replace(/\"/gi, "");
        localImageToBase64(img);
        createPDF()
    })

    function createPDF() {
        var form = $('.cv-page'),
            cache_width = form.width(),
            a4 = [595.28, 841.89]; // for a4 size paper width and height

        form.width((a4[0] * 1.33333)).css('max-width', 'none');

        setTimeout(() => {
            html2canvas(document.querySelector(".cv-page"), {
                allowTaint: true,
                useCORS: true,
                scale : 1
            }).then((canvas) => {
                const imgWidth = 210;
                const pageHeight = 295;
                const imgHeight = (canvas.height * imgWidth) / canvas.width;
                let heightLeft = imgHeight;
                let position = 0;
                heightLeft -= pageHeight;
                const doc = new jsPDF('p', 'mm');
                doc.addImage(canvas.toDataURL("image/png"), 'PNG', 0, 0);
                while (heightLeft >= 0) {
                    position = heightLeft - imgHeight;
                    doc.addPage();
                    doc.addImage(canvas.toDataURL("image/png"), 'PNG', 0, position);
                    heightLeft -= pageHeight;
                }
                doc.save('Downld.pdf');
            })
        }, 1000);
    }

    loadData();
    loadListCVTemplate();
    
  
});

// Hàm load dữ liệu CandidateCV
async function loadData() {
    if (candidateCVId) {
        let res = await httpService.getAsync(`api/candidate-cv/detail/${candidateCVId}`);
        if (res.isSucceeded) {
            let candidateCV = res.resources;
            $("#inputCandidateCVName").val(candidateCV.name);
            $(".cv-page").html(candidateCV.content);
            $("style.additional-css").text(candidateCV.additionalCSS);
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Tải nội dung CV',
                html: `
                    Có lỗi khi tải nội dung CV.
                    <br>
                    Liên hệ quản trị viên website để báo cáo lỗi.
                `
            });
        }
    }
}

// Hàm lưu CV
async function saveCV(candidateCV) {
    // CREATE
    if (candidateCV.id == 0) {
        let res = await httpService.postAsync("api/candidate-cv/add", candidateCV);
        if (res.isSucceeded) {
            candidateCVId = res.resources;
            $("#inputCandidateCVName").val(candidateCV.name);
            $("#modalInputCandidateCVName").val(candidateCV.name);
            $("#modalSave").modal("hide");
            Swal.fire({
                icon: 'success',
                title: 'Lưu CV',
                html: 'Lưu CV thành công.'
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Lưu CV',
                html: `Có lỗi trong quá trình lưu CV.`
            });
        }
    }
    // UPDATE
    else {
        let res = await httpService.postAsync("api/candidate-cv/update", candidateCV);
        if (res.isSucceeded) {
            $("#inputCandidateCVName").val(candidateCV.name);
            $("#modalInputCandidateCVName").val(candidateCV.name);
            $("#modalSave").modal("hide");
            Swal.fire({
                icon: 'success',
                title: 'Lưu CV',
                html: 'Lưu CV thành công.'
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Lưu CV',
                html: `Có lỗi trong quá trình lưu CV.`
            });
        }
    }
}

// Hàm triển khai template
function implementTemplate(template) {
    $(".cv-page").html(template.content);
    //$(".cv-page").append(`
    //    <div class="d-flex" id="rowAddRow" data-html2canvas-ignore>
    //        <button id="cv-btn-add-row" onclick="editRow(this, 0)">
    //            <i class="fa-solid fa-plus"></i>
    //            Thêm hàng
    //        </button>
    //    </div>
    //`)
    $("style.additional-css").text(template.additionalCSS);
}

async function loadListCVTemplate() {
    let res = await httpService.getAsync("api/cv-template/get-all-publish");
    if (res && res.status == 200) {
        listCVTemplate = res.resources;
        let modalContent = "";
        for (cvTemplate of listCVTemplate) {
            modalContent += `
                <div class="card template-modal" onclick="onClickTemplate(this)" ondblclick="ondblClickTemplate(this)" style="width: 200px" data-template-id="${cvTemplate.id}">
                    <img src="${cvTemplate.photo}" class="card-img-top" alt="Mẫu CV ${cvTemplate.name}">
                    <div class="card-body">
                        <h5 class="card-title">${cvTemplate.name}</h5>
                    </div>
                </div>
            `;
        }
        $("#modalSelectTemplate .modal-body").html(modalContent);
        if (listCVTemplate.length > 0) {
            $("[data-template-id=" + listCVTemplate[0].id + "]").addClass('active').trigger('dblclick');
        }
    }
}

// Hàm click chọn template trong model chọn template
function onClickTemplate(element) {
    $("#modalSelectTemplate .modal-body .card").removeClass("active");
    $(element).addClass("active");
}


function ondblClickTemplate(element) {
    $("#modalSelectTemplate .modal-body .card").removeClass("active");
    $(element).addClass("active");
    $("#btnChooseTemplate").trigger('click')
}
// Hàm thêm dòng mới
function editRow(element, type) {
    $("#modalEditRow .modal-title").text(type == 0 ? "Chọn bố cục" : "Đổi bố cục");
    $("#modalEditRow").modal("show");
}

// Hàm gen column tự động từ tên Layout
function genColumnFromLayoutName(layoutName) {
    let strReturn = `<div class="cv-row cv-row-empty padding">`;
    let arrayColumnLayout = layoutName.split(":");
    let totalColumnValue = 0;
    for (let columnLayout of arrayColumnLayout) {
        totalColumnValue += parseInt(columnLayout);
    }

    for (let columnLayout of arrayColumnLayout) {
        let realColumnValue = parseInt(columnLayout) * 12 / totalColumnValue;
        strReturn += `
            <div class="cv-col cv-col-empty cv-col-${realColumnValue}" onmouseenter="onMouseEnterColumn(this)" onmouseleave="onMouseLeaveColumn(this)" data-html2canvas-ignore>
            </div>
        `;
    }
    strReturn += `</div>`;
    return strReturn;
}

// Sự kiện đưa chuột vào cột
function onMouseEnterColumn(element) {
    if ($(element).hasClass("cv-col-empty")) {
        $("#cv-btn-add-item").css("top", "50%");

    } else {
        $("#cv-btn-add-item").css("top", parseFloat($(element).css("height")));
    }
    $("#cv-btn-add-item").appendTo($(element));
    $("#cv-btn-add-item").removeClass("d-none");
}

// Sự kiện đưa chuột ra khỏi cột
function onMouseLeaveColumn(element) {
    $(element).find("#cv-btn-add-item").addClass("d-none");
}

// Hàm thêm mục mới
function addItem(element) {
    $selectedCol = $(element).parent();
    $("#modalAddItem").modal("show");
}

// Sự kiện click vào một phần tử
function onClickElement(element) {
    $focusElement = $(element);
    changeFocusElement();
}

// Sự kiện thay đổi $focusElement
function changeFocusElement() {
    $(".cv-focus").removeClass("cv-focus");
    $(".cv-btn-vertical").addClass("d-none");

    if ($focusElement) {
        $focusElement.addClass("cv-focus");

        // Xử lý context menu dọc
        if ($focusElement.hasClass("cv-item-header")) {
            $("#btn-info").html(`
                <i class="fa-solid fa-heading"></i>
                Tiêu đề mục
            `);
        } else if ($focusElement.hasClass("cv-item-body")) {
            $("#btn-info").html(`
                <i class="fa-solid fa-code-simple"></i>
                Thân mục
            `);
        } else if ($focusElement.hasClass("cv-item")) {
            $("#btn-info").html(`
                <i class="fa-solid fa-file"></i>
                Mục
            `);
        } else if ($focusElement.hasClass("cv-col")) {
            $("#btn-info").html(`
                <i class="fa-solid fa-chart-simple"></i>
                Cột
            `);
        } else if ($focusElement.hasClass("cv-row")) {
            $("#btn-info").html(`
                <i class="fa-solid fa-chart-simple-horizontal"></i>
                Hàng
            `);
        } else {
            $("#btn-info").html(`
                <i class="fa-solid fa-circle-info"></i>
                Chi tiết
            `);
        }
        $("#btn-info").removeClass("d-none");
        $("#btn-exit").removeClass("d-none");
        if ($focusElement.parents(".cv-item-header").length != 0) {
            $("#btn-go-to-header").removeClass("d-none");
        }
        if ($focusElement.parents(".cv-item-body").length != 0) {
            $("#btn-go-to-body").removeClass("d-none");
        }
        if ($focusElement.parents(".cv-item").length != 0) {
            $("#btn-go-to-item").removeClass("d-none");
        }
        if ($focusElement.parents(".cv-col").length != 0) {
            $("#btn-go-to-column").removeClass("d-none");
        }
        if ($focusElement.parents(".cv-row").length != 0) {
            $("#btn-go-to-row").removeClass("d-none");
        }

        // Đặt vị trí và hiện context menu ngang
        let boundingClientRect = $focusElement[0].getBoundingClientRect();
        let position = $focusElement.position();
        $("#cv-context-menu-horizontal").css("top", (position.top + 40) + "px");
        $("#cv-context-menu-horizontal").css("left", boundingClientRect.x + "px");
        $("#cv-context-menu-horizontal").removeClass("d-none");
        $("#cv-context-menu-vertical").css("top", (position.top + 80) + "px");
        $("#cv-context-menu-vertical").css("left", (boundingClientRect.x + boundingClientRect.width + 10) + "px");
        $("#cv-context-menu-vertical").removeClass("d-none");
        readCssProperties();
    }
}

// Hàm đọc dữ liệu CSS từ element để hiển thị lên menu chỉnh sửa
// Hàm này phải kiểm tra các thuộc tính CSS xem thuộc tính nào được, thuộc tính nào không
function readCssProperties() {
    $(".cv-edit").removeAttr("disabled");
    $(".cv-edit-text-align").removeClass("active");
    let html = $focusElement.html();
    if (html.includes("<b>")) {
        $("#btnBold").addClass("active");
    } else {
        $("#btnBold").removeClass("active");
    }
    if (html.includes("<i>")) {
        $("#btnItalic").addClass("active");
    } else {
        $("#btnItalic").removeClass("active");
    }
    if (html.includes("<u>")) {
        $("#btnUnderline").addClass("active");
    } else {
        $("#btnUnderline").removeClass("active");
    }
    if (html.includes("<del>")) {
        $("#btnStrikeThrough").addClass("active");
    } else {
        $("#btnStrikeThrough").removeClass("active");
    }

    let fontFamily = $focusElement.css("fontFamily");
    let fontSize = $focusElement.css("fontSize");
    let lineHeight = Math.round(parseInt($focusElement.css("lineHeight")) * 10 / parseInt($focusElement.css("fontSize"))) / 10;
    let color = $focusElement.css("color");
    let backgroundColor = $focusElement.css("backgroundColor");

    $("#selectFontFamily").val(fontFamily).trigger("change");
    $("#selectFontSize").val(fontSize).trigger("change");
    $("#selectLineHeight").val(lineHeight).trigger("change");
    $("#selectColor").val(w3color(color).toHexString());
    $("#selectBackgroundColor").val(w3color(backgroundColor).toHexString());

    let textAlign = $focusElement.css("textAlign");
    if (textAlign == 'start' || textAlign == 'left') {
        $(".cv-edit-text-align[data-text-align='left']").addClass("active");
    } else if (textAlign == 'center') {
        $(".cv-edit-text-align[data-text-align='center']").addClass("active");
    } else if (textAlign == 'end' || textAlign == 'right') {
        $(".cv-edit-text-align[data-text-align='right']").addClass("active");
    } else if (textAlign == 'justify') {
        $(".cv-edit-text-align[data-text-align='justify']").addClass("active");
    }
}

// Hàm format template cho select fontsize
function templateSelectFontFamily(data) {
    return $(`<li style="font-family:${data.id}">${data.text}<li>`);
};

// Hàm format template cho select fontsize
function templateSelectFontSize(data) {
    return $(`<li style="font-size:${data.id}">${data.text}<li>`);
};

$.fn.setData = function (data) {
    let html = this.html();
    let isBold = html.includes("<b>");
    let isItalic = html.includes("<i>");
    let isUnderline = html.includes("<u>");
    let isStrikeThrough = html.includes("<del>");

    if (isBold) data = `<b>${data}</b>`;
    if (isItalic) data = `<i>${data}</i>`;
    if (isUnderline) data = `<u>${data}</u>`;
    if (isStrikeThrough) data = `<del>${data}</del>`;
    this.html(data);
};

async function loadCandidateData() {
    let res = await httpService.getAsync("api/Candidate/candidate-detail");
    if (res.status == 200) {
        candidate = res.resources;

        $(".cv-item-fullname").text(candidate.fullName);
        $(".cv-item-nominee").text(candidate.jobPosition);
        $(".cv-item-email span").text(candidate.email);
        $(".cv-item-phone span").text(candidate.phone);
        $(".cv-item-address span").text(candidate.addressDetail);
        //$(".cv-item-avatar img").attr("src", systemConfig.defaultStorage_URL + candidate.photo);

        $("#profile-upload").css('background-image', 'url(' + systemConfig.defaultStorage_URL + candidate.photo + ')');
        //muc tieu
        $(".cv-item-career-goals .cv-item-body .cv-item-content").text(candidate.objective);

        //skill
        $("#cv-item-skill").removeClass('d-none')
        let contentSkill = "";
        if (candidate.listCandidateSkill.length > 0) {
            candidate.listCandidateSkill.forEach(function (item) {
                contentSkill += `<div class="cv-item-content" contenteditable="true" onclick="onClickElement(this)">
                    ${item.name}
                </div>`
            })
        }
        else {
            $("#cv-item-skill").addClass('d-none')
        }
        $("#cv-item-skill .cv-item-body").html(contentSkill);

        //certificate
        $("#cv-item-certificate").removeClass('d-none')
        let contentCertificate = "";
        if (candidate.listCandidateCertificates.length > 0) {
            candidate.listCandidateCertificates.forEach(function (item) {
                contentCertificate += `<div style="display: flex; justify-content: space-between;">
                    <span class="cv-item-content padding d-flex flex-column" contenteditable="true" onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                        <span class="fw-bold">${item.name}</span>
                        <b>${item.issueBy }</b>
                    </span>
                    <span class="cv-item-content padding text-nowrap" contenteditable="true"
                        onclick="onClickElement(this)"
                        style="line-height: 24px; font-size: 14px;">${item.timePeriod}
                    </span>
                </div>`
            })
            $("#cv-item-certificate .cv-item-body").html(contentCertificate);

        }
        else {
            $("#cv-item-certificate").addClass('d-none')
        }

        //hoc van

        $("#cv-item-education").removeClass('d-none')
        let contentEducation = "";
        if (candidate.listCandidateEducation.length > 0) {
            candidate.listCandidateEducation.forEach(function (item) {
                contentEducation += `<div style="display: flex; justify-content: space-between;">
                    <span class="cv-item-content padding d-flex flex-column" contenteditable="true" onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                        <span class='fw-bold'>${item.title}</span>
                        <b>${item.school}</b>
                    </span>
                    <span class="cv-item-content padding text-nowrap" contenteditable="true"
                        onclick="onClickElement(this)"
                        style="line-height: 24px; font-size: 14px;">${item.timePeriod}
                    </span>
                </div>`
            })
            $("#cv-item-education .cv-item-body").html(contentEducation);

        }
        else {
            $("#cv-item-education").addClass('d-none')
        }

        //kinh nghiem lam viec
        $("#cv-item-work-experience").removeClass('d-none')

        var contentExperience = ``;
        if (candidate.listCandidateWorkExperiences.length > 0) {
            candidate.listCandidateWorkExperiences.forEach(function (item) {
                contentExperience += `<div class="cv-item-body movable copyable">
                <div style="display: flex; justify-content: space-between;">
                    <span class="cv-item-content padding" contenteditable="true"
                        onclick="onClickElement(this)" style="line-height: 24px; font-size: 14px;">
                        <span class="fw-bold">${item.jobTitle} </span>
                    </span>
                    <span class="cv-item-content padding" contenteditable="true"
                        onclick="onClickElement(this)"
                        style="line-height: 24px; font-size: 14px;"><b>${item.timePeriod}</b></span>
                </div>
                <div class="cv-item-content padding" contenteditable="true"
                    onclick="onClickElement(this)" style="margin-top: -20px; font-size: 14px;">
                    <b><i>${item.company}</i></b>
                </div>
                <div class="cv-item-content padding" contenteditable="true"
                    onclick="onClickElement(this)"
                    style="margin-top: -10px; font-size: 14px; text-align: justify; line-height: 25px;">
                    ${item.description.replaceAll("\n","<br>")}
                </div>
            </div>`
            })
            //console.log(contentExperience)
            $("#cv-item-work-experience .cv-item-body").html(contentExperience);
        }
        else {
            $("#cv-item-work-experience").addClass('d-none')
        }
       

    }
}

function localImageToBase64(localImagePath) {
    // Đường dẫn tới tệp hình ảnh local
    //const localImagePath = 'path/to/your/local/image.png';
    localImagePath = localImagePath.replace('https://localhost:7049/','https://storage-jobi.dion.vn/')
    // Sử dụng Fetch API để đọc tệp hình ảnh
    fetch(localImagePath)
        .then(response => response.blob())
        .then(blob => {
            const reader = new FileReader();
            reader.onload = () => {
                const base64String = reader.result.split(',')[1];
                //console.log('Chuỗi Base64:', base64String);
                $("#profile-upload").css("background-image", "url(" + "data:image/png;base64," + base64String + ")");
            };
            reader.readAsDataURL(blob);
        })
        .catch(error => console.error(error));
}


//document.getElementById('getval').addEventListener('change', readURL, true);
$(document).on('change', '#getval',function () {
    readURL();
})
function readURL() {
    var file = document.getElementById("getval").files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        document.getElementById('profile-upload').style.backgroundImage = "url(" + reader.result + ")";
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
    }
}