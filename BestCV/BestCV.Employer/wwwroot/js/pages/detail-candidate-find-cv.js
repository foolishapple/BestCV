// Get a reference to the PDF container
const containers = document.getElementById('pdfContainer');

// Path to your PDF file
var pdfPath = "";



$(document).ready(async function () {
    await loadDataCVPDF(candidateID);
    await loadDetailCV(candidateID);
    $(".list-cv li .nav-link").click(function () {
        //console.log(this);
        $(".nav-link").removeClass('cv-active')
        $(this).addClass('cv-active')
        let cv = dataCV.find(x => x.id == $(this).attr('data-id'));
        pdfPath = "https://storage-jobi.dion.vn/" + cv.url;
        $("#pdfContainer").html('')

        loadPDFCV()
    })

    if ($(".list-cv li").first().length > 0) {
        $(".list-cv li").first().children().addClass('cv-active').trigger('click')
    }
})

var dataCV = [];
async function loadDataCVPDF(candidateId) {
    try {
        let res = await httpService.getAsync("api/candidate-cv-pdf/list-by-candidate-id/" + candidateId);
        if (res.isSucceeded) {
            //console.log(res.resources)
            dataCV = res.resources;
            let content = "";
            res.resources.forEach(function (item) {
                let nameCV = item.url.split('/');
                content += `<li class="pxp-text-right mt-2"><a class="nav-link" data-id='${item.id}'>${nameCV.pop()}</a></li>`
            })
            $(".list-cv").html(content);
        }
    }
    catch (e) {
        console.error(e)
    }
}

async function loadDetailCV(id) {
    try {
        let result = await httpService.getAsync("api/candidate/detail/" + id);
        if (result.isSucceeded) {
            console.log(result.resources)
            var content = `
                            <div class="pxp-logo mb-4 mt-4">
                                    <a href="#!" class="pxp-animate"><span style="color: var(--pxpMainColor)">j</span>obi</a>
                            </div>
                            <div class="row">
                                <div class="col-2">
                                    <div class="pxp-company-dashboard-candidate-avatar pxp-cover bg-secondary" style="background-image: url(${result.resources.photo != "" && result.resources.photo != null ? systemConfig.defaultStorage_URL + result.resources.photo : systemConfig.defaultStorage_URL + "/uploads/candidates/avatars/avatar.jpg"});">
                                    </div>
                                </div>
                                <div class="d-flex flex-column col-10">
                                    <span class="fw-bold mb-1">
                                        ${result.resources.fullName} ${result.resources.gender == 1 ? '<i class="fa-solid fa-mars text-primary"></i>' : (result.resources.gender == 2 ? '<i class="fa-solid fa-venus text-danger"></i>' : "")}
                                    </span>
                                    <span class="o-hidden d-flex align-items-center">
                                        ${result.resources.email != null && result.resources.email != "" ? '<i class="fa-solid fa-envelope me-1"></i>' + result.resources.email : ""}
                                    </span>

                                    <span class="o-hidden d-flex align-items-center">
                                        ${result.resources.phone != null && result.resources.phone != "" ? '<i class="fa-solid fa-phone me-1"></i>' + result.resources.phone : ""}
                                    </span>
                                    <span>
                                        ${result.resources.doB != null ? '<i class="fa-solid fa-calendar me-1"></i>' + moment(result.resources.doB).format('DD/MM/YYYY') : ""}
                                    </span>
        
                                    <span>
                                        ${result.resources.addressDetail != null && result.resources.addressDetail != "" ? '<i class="fa-solid fa-earth-asia me-1"></i>' + result.resources.addressDetail : ""}
                                    </span>
                                    
                                    <div class="mt-3">
                                        <a href="tel:${result.resources.phone}" class="btn btn-contact me-2"><i class="fa-solid fa-phone text-success"></i></a>
                                        <a href="mailto:${result.resources.email}" class="btn btn-contact me-2"><i class="fa-solid fa-envelope text-success"></i></a>
                                    </div>
                                </div>


                            </div>`;
            $(".candidate-info").html(content)
        }
    }
    catch (e) {

    }
}

function loadPDFCV() {
    // Load the PDF
    pdfjsLib.getDocument(pdfPath).promise.then(pdf => {
        const numPages = pdf.numPages;

        // Loop through all pages and render them
        for (let pageNum = 1; pageNum <= numPages; pageNum++) {
            pdf.getPage(pageNum).then(page => {
                // Set the desired scale and rotation (optional)
                //const scale = 1.2;
                //const rotation = 0;
                var desiredWidth = containers.offsetWidth
                var viewport = page.getViewport({ scale: 1, });
                var scale = desiredWidth / viewport.width;
                var scaledViewport = page.getViewport({ scale: scale, });

                //console.log(page.getViewPort())
                // Get the viewport of the page
                //const viewport = page.getViewport({ scale, rotation });

                // Create a canvas element to render the page
                const canvas = document.createElement('canvas');
                const context = canvas.getContext('2d');
                canvas.width = scaledViewport.width;
                canvas.height = scaledViewport.height;
                // Append the canvas to the container
                containers.appendChild(canvas);

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