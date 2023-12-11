$(document).ready(async function () {
    await loadJobSuitable();
    await loadInterestedCompany();
})


async function loadJobSuitable() {
    try {
        let url = "api/job-suitable/list-job-suitable-dashboard";
        let res = await httpService.getAsync(url);
        if (res.isSucceeded) {
            var data = res.resources;
            if (data.length > 0) {
                let content = ""
                data.forEach(function (item) {
                    //hiển thị tag city
                    let dataCity = "";
                    if (item.jobRequireCity.length > 1) {
                        let contentTitle = "";
                        for (let x = 0; x < 1; x++) {
                            dataCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                        }
                        for (let y = 1; y < item.jobRequireCity.length; y++) {
                            contentTitle += item.jobRequireCity[y].cityName + ', ';
                        }
                        dataCity += `<a class='badge-city' title="${contentTitle.substring(0, contentTitle.length -2)}">+${item.jobRequireCity.length - 1}</a>`
                    }
                    else {
                        for (let x = 0; x < item.jobRequireCity.length; x++) {
                            dataCity += `<a class='badge-city'>${item.jobRequireCity[x].cityName.replace("Thành phố ", "")}</a>`
                        }
                    }
                    //generate tên dạng slug
                    let slugJob = stringToSlug(item.jobName);

                    content += `<div class="col-12"><div class="card mb-2 hover-card-item card-border-item">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item m-10px img-cover-fit" src="${systemConfig.defaultStorage_URL + item.companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between  m-10px">
                                    <a class="job-name" href="/chi-tiet-cong-viec/${slugJob + "-" + item.jobId}" title='${item.jobName}'>${item.jobName}</a>
                                    <span></span>
                                </div>
                                <div class="d-flex  m-10px">
                                 <a href="/thong-tin-cong-ty/${item.companyId}" class="job-company" title="${item.companyName}">${item.companyName}</a>
                                 </div>
                                <div class="d-flex flex-column  m-10px">
                                   <span class='text-color-money fw-bold salary-job'>${item.salaryTypeId == salaryFrom ? (item.salaryFrom != null ? "Từ " + formatNumber(item.salaryFrom.toString()) : 0) : (item.salaryTypeId == salaryTo ? (item.salaryTo != null ? "Đến " + formatNumber(item.salaryTo.toString()) : 0) : (item.salaryTypeId == salaryBetween ? formatNumber(item.salaryFrom.toString()) + " - " + formatNumber(item.salaryTo.toString()) : "Thỏa thuận"))}</span>
                                    <span>${dataCity}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>`;
                })
                $("#suitable-div").html(content);
            }

        }
    } catch (e) {
        console.error(e)
    }
}

function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}
function countRemainDay(input) {
    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    var firstDate = new Date();
    var secondDate = new Date(input);

    var diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
    return diffDays;
}
function timeAgo(input) {
    const date = (input instanceof Date) ? input : new Date(input);
    const formatter = new Intl.RelativeTimeFormat('vi');
    const ranges = {
        years: 3600 * 24 * 365,
        months: 3600 * 24 * 30,
        weeks: 3600 * 24 * 7,
        days: 3600 * 24,
        hours: 3600,
        minutes: 60,
        seconds: 1
    };
    const secondsElapsed = (date.getTime() - Date.now()) / 1000;
    for (let key in ranges) {
        if (ranges[key] < Math.abs(secondsElapsed)) {
            const delta = secondsElapsed / ranges[key];
            return formatter.format(Math.round(delta), key);
        }
    }
}

async function loadInterestedCompany() {
    try {
        let url = "api/must-interesterd-company/list-interested-company-job-detail";
        let res = await httpService.getAsync(url);
        if (res.isSucceeded) {
            var data = res.resources;
            if (data.length > 0) {

                let content = ""
                data.forEach(function (item) {

                    //field of company
                    let contentFieldOfCompany = "";
                    if (item.companyFields.length > 1) {
                        let contentTitle = "";
                        for (let x = 0; x < 1; x++) {
                            contentFieldOfCompany += `<a class='badge-city'>${item.companyFields[x].fieldOfActivityName}</a>`
                        }
                        for (let y = 1; y < item.companyFields.length; y++) {
                            contentTitle += item.companyFields[y].fieldOfActivityName + ', ';
                        }
                        contentFieldOfCompany += `<a class='badge-city' title="${contentTitle.substring(0, contentTitle.length - 2)}">+${item.companyFields.length - 1}</a>`
                    }
                    else {
                        for (let x = 0; x < item.companyFields.length; x++) {
                            contentFieldOfCompany += `<a class='badge-city'>${item.companyFields[x].fieldOfActivityName}</a>`
                        }
                    }
                    content += `<div class="col-12"><div class="card mb-2 hover-card-item card-border-item">
                        <div class="row  align-items-center">
                            <div class="col-3">
                                <img class="card-border-item m-10px img-cover-fit" src="${systemConfig.defaultStorage_URL + item.companyLogo}" width="100" height="100" />
                            </div>
                            <div class="col-9">
                                <div class="d-flex justify-content-between m-10px">
                                    <a class="job-name" href="/thong-tin-cong-ty/${item.companyId}" title='${item.companyName}'>${item.companyName}</a>
                                    <span></span>
                                </div>
                               <div class="d-flex justify-content-between m-10px">
                                    <span class="text-ellipsis" title="${item.countJob} việc làm"><span class="fa fa-briefcase me-1"></span>${item.countJob} việc làm</span>
                                    <span class="text-ellipsis" title="${item.countFollower} việc làm"><span class="fa fa-users me-1"></span>${item.countFollower} người theo dõi</span>
                               </div>

                               <div class=" m-10px">
                               ${contentFieldOfCompany}
                               </div>
                            </div>
                        </div>
                    </div>
                    </div>`;
                })
                $("#company-suitable").html(content);
            }

        }
    } catch (e) {
        console.error(e)
    }
}