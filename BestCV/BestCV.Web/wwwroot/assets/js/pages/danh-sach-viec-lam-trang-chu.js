$(document).ready(async function () {
    getTopFeatureJob()
    getTopJobExtra()
    getTopJobUrgent()
})


var dataListFeatureJob = [];
const step = 5;
async function getTopFeatureJob() {
    try {
        let result = await httpService.getAsync('api/top-feature-job/top-feature-job-home-page')
        if (result.isSucceeded) {
            dataListFeatureJob = result.resources;
            loadDataFeatureJob();
        }
    } catch (e) {
        console.error(e)
    }
}

function loadDataFeatureJob() {
    var content = "";
    //load data carousel
    for (let i = 0; i < dataListFeatureJob.length; i += step) {
        content += `<div class="item">`
        //load data 5 job trong 1 item carousel
        for (let j = i; j < i + step && j < dataListFeatureJob.length; j++) {

            //hiển thị tag city
            let dataCity = "";
            if (dataListFeatureJob[j].jobRequireCity.length > 1) {
                let titleCity = "";
                for (let x = 0; x < 1; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListFeatureJob[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
                for (let y = 1; y < dataListFeatureJob[j].jobRequireCity.length; y++) {
                    titleCity += dataListFeatureJob[j].jobRequireCity[y].cityName + ', ';
                }
                dataCity += `<a class='badge-city jbi-bagde' title="${titleCity.substring(0, titleCity.length - 2)}">+${dataListFeatureJob[j].jobRequireCity.length - 1}</a>`
            }
            else {
                for (let x = 0; x < dataListFeatureJob[j].jobRequireCity.length; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListFeatureJob[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
            }
            //generate tên dạng slug
            let slugJob = stringToSlug(dataListFeatureJob[j].topFeatureJobName);

            //tim kiem trong list benefit
            let dataBenefit = dataListFeatureJob[j].listBenefit.find(x => x.id == ListBenefit.GAN_TAG_NEW);
            let dataRedTitle = dataListFeatureJob[j].listBenefit.find(x => x.id == ListBenefit.BOLD_AND_RED);
            content += `<div class="card mt--20 hover-card-item card-border-item jbi-card-2">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item p-2 m-10px img-cover-fit bg-white" src="${systemConfig.defaultStorageURL + dataListFeatureJob[j].companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between">
                                    <a class="job-name ${dataRedTitle != undefined ? "text-danger" : ""}" href="/chi-tiet-cong-viec/${slugJob + "-" + dataListFeatureJob[j].topFeatureJobId}" title='${dataListFeatureJob[j].topFeatureJobName}'>${dataBenefit != undefined && (moment() - moment(dataListFeatureJob[j].refreshDate)) < dayToMilisecond ? newTag : ""}${dataListFeatureJob[j].topFeatureJobName}</a>
                                    <span></span>
                                </div>
                                <div class="d-flex">
                                 <a href="/thong-tin-cong-ty/${dataListFeatureJob[j].companyId}" class="job-company jbi-card-sub-title" title="${dataListFeatureJob[j].companyName}">${dataListFeatureJob[j].companyName}</a>
                                 </div>
                                <div class="d-flex flex-column">
                                   <span class='text-color-money fw-bold salary-job'>${dataListFeatureJob[j].salaryTypeId == salaryFrom ? (dataListFeatureJob[j].salaryFrom != null ? "Từ " + formatNumber(dataListFeatureJob[j].salaryFrom.toString()) : 0) : (dataListFeatureJob[j].salaryTypeId == salaryTo ? (dataListFeatureJob[j].salaryTo != null ? "Đến " + formatNumber(dataListFeatureJob[j].salaryTo.toString()) : 0) : (dataListFeatureJob[j].salaryTypeId == salaryBetween ? formatNumber(dataListFeatureJob[j].salaryFrom.toString()) + " - " + formatNumber(dataListFeatureJob[j].salaryTo.toString()) : "Thỏa thuận"))}</span>
                                    <span>${dataCity}</span>
                                </div>
                            </div>
                        </div>
                    </div>`;
        }
        content += `</div>`;
    }
    $("#topFeatureJob").html(content)
    $('#topFeatureJob').owlCarousel({
        loop: true,
        items: 1,
        margin: 20,
        stagePadding: 10,
        center: true
    });
}

function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}

// load data top job extra
var dataListJobExtra = [];
async function getTopJobExtra() {
    try {
        let result = await httpService.getAsync('api/top-job-extra/get-list-top-job-extra')
        if (result.isSucceeded) {
            dataListJobExtra = result.resources;
            loadDataJobExtra();
        }
    } catch (e) {
        console.error(e)
    }
}

function loadDataJobExtra() {
    var content = "";
    for (let i = 0; i < dataListJobExtra.length; i += step) {
        content += `<div class="item">`
        for (let j = i; j < i + step && j < dataListJobExtra.length; j++) {
            let dataCity = "";
            if (dataListJobExtra[j].jobRequireCity.length > 1) {
                let titleCity = "";
                for (let x = 0; x < 1; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListJobExtra[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
                for (let y = 1; y < dataListJobExtra[j].jobRequireCity.length; y++) {
                    titleCity += dataListJobExtra[j].jobRequireCity[y].cityName + ', ';
                }
                dataCity += `<a class='badge-city jbi-bagde' title="${titleCity.substring(0, titleCity.length - 2)}">+${dataListJobExtra[j].jobRequireCity.length - 1}</a>`
            }
            else {
                for (let x = 0; x < dataListJobExtra[j].jobRequireCity.length; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListJobExtra[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
            }
            let slugJob = stringToSlug(dataListJobExtra[j].jobName);

            //tim kiem trong list benefit
            let dataBenefit = dataListJobExtra[j].listBenefit.find(x => x.id == ListBenefit.GAN_TAG_NEW);
            let dataTopTag = dataListJobExtra[j].listBenefit.find(x => x.id == ListBenefit.GAN_TAG_TOP);
            let dataRedTitle = dataListJobExtra[j].listBenefit.find(x => x.id == ListBenefit.BOLD_AND_RED);


            content += `<div class="card mt--20 hover-card-item card-border-item jbi-card-2">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item p-2 m-10px img-cover-fit bg-white" src="${systemConfig.defaultStorageURL + dataListJobExtra[j].companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between">
                                    <a class="job-name ${dataRedTitle != undefined ? "text-danger" : ""}" href="/chi-tiet-cong-viec/${slugJob + "-" + dataListJobExtra[j].jobId}" title='${dataListJobExtra[j].jobName}'>${dataBenefit != undefined && (moment() - moment(dataListJobExtra[j].jobCreatedTime)) < dayToMilisecond ? newTag : ""}${dataListJobExtra[j].jobName}</a>
                                    <span>${dataTopTag != undefined ? topTag : ""}</span>
                                </div>
                                <div class="d-flex">
                                <a href="/thong-tin-cong-ty/${dataListJobExtra[j].companyId}" class="job-company jbi-card-sub-title" title="${dataListJobExtra[j].companyName}">${dataListJobExtra[j].companyName}</a>

                                </div>
                                <div class="d-flex flex-column">
                                   <span class='text-color-money fw-bold salary-job'>${dataListJobExtra[j].salaryTypeId == salaryFrom ? (dataListJobExtra[j].salaryFrom != null ? "Từ " + formatNumber(dataListJobExtra[j].salaryFrom.toString()) : 0) : (dataListJobExtra[j].salaryTypeId == salaryTo ? (dataListJobExtra[j].salaryTo != null ? "Đến " + formatNumber(dataListJobExtra[j].salaryTo.toString()) : 0) : (dataListJobExtra[j].salaryTypeId == salaryBetween ? formatNumber(dataListJobExtra[j].salaryFrom.toString()) + " - " + formatNumber(dataListJobExtra[j].salaryTo.toString()) : "Thỏa thuận"))}</span>
                                    <span>${dataCity}</span>
                                </div>
                            </div>
                        </div>
                    </div>`;
        }
        content += `</div>`;
    }
    $("#topJobExtra").html(content)
    $('#topJobExtra').owlCarousel({
        loop: true,
        items: 1,
        margin: 20,
        stagePadding: 10,
        center: true
    });
}

// load data top job urgent
var dataListJobUrgent = [];
async function getTopJobUrgent() {
    try {
        let result = await httpService.getAsync('api/top-job-urgent/get-top-urgent-job')
        if (result.isSucceeded) {
            dataListJobUrgent = result.resources;
            loadDataJobUrgent();
        }
    } catch (e) {
        console.error(e)
    }
}

function loadDataJobUrgent() {
    var content = "";
    for (let i = 0; i < dataListJobUrgent.length; i += step) {
        content += `<div class="item">`
        for (let j = i; j < i + step && j < dataListJobUrgent.length; j++) {
            let dataCity = "";
            if (dataListJobUrgent[j].jobRequireCity.length > 1) {
                let titleCity = "";
                for (let x = 0; x < 1; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListJobUrgent[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
                for (let y = 1; y < dataListJobUrgent[j].jobRequireCity.length; y++) {
                    titleCity += dataListJobUrgent[j].jobRequireCity[y].cityName + ', ';
                }
                dataCity += `<a class='badge-city jbi-bagde' title="${titleCity.substring(0, titleCity.length - 2)}">+${dataListJobUrgent[j].jobRequireCity.length - 1}</a>`
            }
            else {
                for (let x = 0; x < dataListJobUrgent[j].jobRequireCity.length; x++) {
                    dataCity += `<a class='badge-city jbi-bagde'>${dataListJobUrgent[j].jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                }
            }
            let slugJob = stringToSlug(dataListJobUrgent[j].jobName);

            //tim kiem trong list benefit
            let dataBenefit = dataListJobUrgent[j].listBenefit.find(x => x.id == ListBenefit.GAN_TAG_NEW);
            let dataUrgentTag = dataListJobUrgent[j].listBenefit.find(x => x.id == ListBenefit.GAN_TAG_URGENT);
            let dataRedTitle = dataListJobUrgent[j].listBenefit.find(x => x.id == ListBenefit.BOLD_AND_RED);
            content += `<div class="card mt--20 hover-card-item card-border-item jbi-card-2">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item p-2 m-10px img-cover-fit bg-white" src="${systemConfig.defaultStorageURL + dataListJobUrgent[j].companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between">
                                    <a class="job-name ${dataRedTitle != undefined ? "text-danger" : ""}" href="/chi-tiet-cong-viec/${slugJob + "-" + dataListJobUrgent[j].jobId}" title='${dataListJobUrgent[j].jobName}'>${dataBenefit != undefined && (moment() - moment(dataListJobUrgent[j].jobCreatedTime)) < dayToMilisecond ? newTag : ""}${dataListJobUrgent[j].jobName}</a>
                                    <span>${dataUrgentTag != undefined ? urgentTag : ""}</span>
                       
                                </div>
                                <div class="d-flex">
                                <a href="/thong-tin-cong-ty/${dataListJobUrgent[j].companyId}" class="job-company jbi-card-sub-title" title="${dataListJobUrgent[j].companyName}">${dataListJobUrgent[j].companyName}</a>
                                </div>
                                <div class="d-flex flex-column">
                                   <span class='text-color-money fw-bold salary-job'>${dataListJobUrgent[j].salaryTypeId == salaryFrom ? (dataListJobUrgent[j].salaryFrom != null ? "Từ " + formatNumber(dataListJobUrgent[j].salaryFrom.toString()) : 0) : (dataListJobUrgent[j].salaryTypeId == salaryTo ? (dataListJobUrgent[j].salaryTo != null ? "Đến " + formatNumber(dataListJobUrgent[j].salaryTo.toString()) : 0) : (dataListJobUrgent[j].salaryTypeId == salaryBetween ? formatNumber(dataListJobUrgent[j].salaryFrom.toString()) + " - " + formatNumber(dataListJobUrgent[j].salaryTo.toString()) : "Thỏa thuận"))}</span>
                                    <span>${dataCity}</span>
                                </div>
                            </div>
                        </div>
                    </div>`;
        }
        content += `</div>`;
    }
    $("#topJobUrgent").html(content);
    $('#topJobUrgent').owlCarousel({
        loop: true,
        items: 1,
        margin: 20,
        stagePadding: 10,
        center: true
    });
}