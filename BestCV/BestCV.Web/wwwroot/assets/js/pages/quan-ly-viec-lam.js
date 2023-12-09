//Author : ThanhND
//load data danh sách đã ứng tuyển
initTableApplyJob();
async function initTableApplyJob() {
	tableApplyJob = $("#table_candidate_apply_job").DataTable({
		language: systemConfig.languageDataTable,
		processing: true,
		serverSide: true,
		paging: true,
		searching: { regex: true },
		autoWidth: false,
		ordering: false,
		info: false,
		ajax: {
			url: `${systemConfig.defaultAPIURL}api/candidate-apply-job/manage-list-job-apply`,
			type: "POST",
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
			},
			dataType: "json",
			data: function (d) {
				d.search.value = $("#table_search_all").val().trim() || "";

				return JSON.stringify(d);
			}
		},
		columns: [
			{
				data: "jobName",
				render: function (data, type, row, meta) {
					return `<div> <div class="mb-2"><a title='${row.jobName}' class='job-name-column pxp-company-dashboard-job-title' href="/chi-tiet-cong-viec?jobId=${row.jobId}">${data}</a></div>
						<div class="text-color-money fw-600"><span class='fa-regular fa-badge-dollar me-1'></span> ${row.salaryTypeId == 1001 ? (row.salaryFrom != null ? "Từ " + formatNumber(row.salaryFrom.toString()) : 0) : (row.salaryTypeId == 1002 ? (row.salaryTo != null ? "Đến " + formatNumber(row.salaryTo.toString()) : 0) : (row.salaryTypeId == 1003 ? formatNumber(row.salaryFrom.toString()) + " - " + formatNumber(row.salaryTo.toString()) : "Thỏa thuận"))}</div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.cityRequired.toString() != "" ? "" : "d-none"}'></span> ${row.cityRequired.toString().replaceAll(",", ", ")}</div>
					</div>`;
				}
			},
			{
				data: "companyName",
				render: function (data, type, row, meta) {
					return `<div><div class="mb-2"><a title='${row.companyName}' class='pxp-company-dashboard-job-title' href="/job-company-best/${row.companyId}">${data}</a></div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.companyAddress != "" ? "" : "d-none"}'></span> ${row.companyAddress}</div>
						<div><span class='fa-regular fa-earth-americas me-1 ${row.companyWebsite != "" ? "" : "d-none"}'></span><a href="${row.companyWebsite}" class='${row.companyWebsite != "" ? "" : "d-none"}'> ${row.companyWebsite}</a></div>
					</div>`;
				}
			},
			{
				data: "candidateCVPDFUrl",
				render: function (data, type, row, meta) {
					return `<a class='text-nowrap' href="${systemConfig.defaultStorage_URL + data}">${data.split('/').pop()}</a>`;
				}
			},
			{
				data: "candidateApplyJobStatusName",
				render: function (data, type, row, meta) {
					return `<span style="background-color: ${customBagdeColor(row.candidateApplyJobStatusColor)}; padding: 4px 6px; color: ${row.candidateApplyJobStatusColor};border-radius: 5px; font-weight: 500;" class='text-nowrap'>${data}</span>`;
				}
			},
			{
				data: "createdTime",
				render: function (data, type, row, meta) {
					return `<span class='text-nowrap'>${moment(data).format("DD/MM/YYYY HH:mm:ss") }</span>`;
				}
			},
			
		],
		columnDefs: [
			{
				targets: [-1, -2],
				className: "text-center status-apply-job"
			},
			{
				targets: [-3],
				className: "status-apply-job"
			}
		],
		drawCallback: function () {
			$('#table_candidate_apply_job tfoot').html("");
			innitTblPagingApplyJob("#apply-job-paging");
		}
	});
	$("#pills-home").on("click", ".page-link-apply-job", function (e) {
		if ($(this).hasClass("active")) {
			return;
		}
		let pageIndex = $(this).attr("data-index");
		if (pageIndex) {
			tableApplyJob.page(parseInt(pageIndex)).draw('page');
		}
	})
	$("#table_search_all").on("keypress", function (e) {
		if (e.which == 13) {
			tableApplyJob.draw();
		}
	})
}
function innitTblPagingApplyJob(element) {
	let info = tableApplyJob.page.info();
	let totalPage = info.pages;
	let pageIndex = info.page;
	if (totalPage > 0) {
		let html = "";
		let startPage;
		if (totalPage <= 3) {
			startPage = 1;
		}
		else {
			if (totalPage == pageIndex) {
				startPage = totalPage - 2;
			}
			else {
				startPage = pageIndex == 0 ? 1 : pageIndex;
			}

		}
		let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
		if (pageIndex >= 1) {
			html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-apply-job page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-apply-job page-link"><i class="fa fa-angle-left"></i></a></li>`;
		}
		for (var i = startPage; i <= endPage; i++) {
			if (i > 0) {
				html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-apply-job ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
			}
		}
		if (pageIndex < totalPage - 1) {
			html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-apply-job page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-apply-job page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
		}
		$(element).html(html);
	}
	else {
		$(element).html("");
	}
}

//load data danh sách đã lưu
initTableSaveJob();
async function initTableSaveJob() {
	tableSaveJob = $("#table_candidate_save_job").DataTable({
		language: systemConfig.languageDataTable,
		processing: true,
		serverSide: true,
		paging: true,
		searching: { regex: true },
		autoWidth: false,
		ordering: false,
		info: false,
		ajax: {
			url: `${systemConfig.defaultAPIURL}api/candidate-save-job/manage-list-job-save`,
			type: "POST",
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
			},
			dataType: "json",
			data: function (d) {
				d.search.value = $("#table_search_all_save_job").val().trim() || "";
				return JSON.stringify(d);
			}
		},
		columns: [
			{
				data: "jobName",
				render: function (data, type, row, meta) {
					var diffDate = 0;
					if (row.jobApplyEndDate != "" && row.jobApplyEndDate != null) {
						let endDate = new Date(row.jobApplyEndDate);
						diffDate = endDate - new Date();
					}
					//console.log(row.jobName + " " + diffDate)
					return `<div> <div class="mb-2"><a title='${row.jobName}' class='job-name-column pxp-company-dashboard-job-title' href="/chi-tiet-cong-viec?jobId=${row.jobId}">${data}</a></div>
						<div class="text-color-money fw-600"><span class='fa-regular fa-badge-dollar me-1'></span> ${row.salaryTypeId == 1001 ? (row.salaryFrom != null ? "Từ " + formatNumber(row.salaryFrom.toString()) : 0) : (row.salaryTypeId == 1002 ? (row.salaryTo != null ? "Đến " + formatNumber(row.salaryTo.toString()) : 0) : (row.salaryTypeId == 1003 ? formatNumber(row.salaryFrom.toString()) + " - " + formatNumber(row.salaryTo.toString()) : "Thỏa thuận"))}</div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.cityRequired.toString() != "" ? "" : "d-none"}'></span> ${row.cityRequired.toString().replaceAll(",", ", ")}</div>
						<div class="${row.jobApplyEndDate != null ? "" : "d-none"} ${diffDate > 0 ? "" : "text-chocolate"}"><span class="fa-regular fa-clock" aria-hidden="true"></span> </span><span>${row.jobApplyEndDate != null ? (diffDate > 0 ? "Hạn nộp hồ sơ : " + moment(row.jobApplyEndDate).format("DD/MM/YYYY HH:mm:ss") : "Đã hết hạn ứng tuyển") : ""}</div>
					</div>`;
				}
			},
			{
				data: "companyName",
				render: function (data, type, row, meta) {
					return `<div><div class="mb-2"><a title='${row.companyName}' class='pxp-company-dashboard-job-title' href="/job-company-best/${row.companyId}">${data}</a></div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.companyAddress != "" ? "" : "d-none"}'></span> ${row.companyAddress}</div>
						<div><span class='fa-regular fa-earth-americas me-1 ${row.companyWebsite != "" ? "" : "d-none"}'></span><a href="${row.companyWebsite}" class='${row.companyWebsite != "" ? "" : "d-none"}'> ${row.companyWebsite}</a></div>
					</div>`;
				}
			},
			
			{
				data: "createdTime",
				render: function (data, type, row, meta) {
					return moment(data).format("DD/MM/YYYY HH:mm:ss");
				}
			},

		],
		columnDefs: [
			{
				targets: [-1],
				className: "text-center status-apply-job"
			}
		],
		drawCallback: function () {
			$('#table_candidate_save_job tfoot').html("");
			innitTblPagingSaveJob("#save-job-paging");
		}
	});
	$("#pills-profile").on("click", ".page-link-save-job", function (e) {
		if ($(this).hasClass("active")) {
			return;
		}
		let pageIndex = $(this).attr("data-index");
		if (pageIndex) {
			tableSaveJob.page(parseInt(pageIndex)).draw('page');
		}
	})
	$("#table_search_all_save_job").on("keypress", function (e) {
		if (e.which == 13) {
			tableSaveJob.draw();
		}
	})
}
function innitTblPagingSaveJob(element) {
	let info = tableSaveJob.page.info();
	let totalPage = info.pages;
	let pageIndex = info.page;
	if (totalPage > 0) {
		let html = "";
		let startPage;
		if (totalPage <= 3) {
			startPage = 1;
		}
		else {
			if (totalPage == pageIndex) {
				startPage = totalPage - 2;
			}
			else {
				startPage = pageIndex == 0 ? 1 : pageIndex;
			}

		}
		let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
		if (pageIndex >= 1) {
			html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-save-job page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-save-job page-link"><i class="fa fa-angle-left"></i></a></li>`;
		}
		for (var i = startPage; i <= endPage; i++) {
			if (i > 0) {
				html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-save-job ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
			}
		}
		if (pageIndex < totalPage - 1) {
			html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-save-job page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-save-job page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
		}
		$(element).html(html);
	}
	else {
		$(element).html("");
	}
}

//load data danh sách đã xem
initTableViewedJob();
async function initTableViewedJob() {
	tableViewedJob = $("#table_candidate_viewed_job").DataTable({
		language: systemConfig.languageDataTable,
		processing: true,
		serverSide: true,
		paging: true,
		searching: { regex: true },
		autoWidth: false,
		ordering: false,
		info: false,
		ajax: {
			url: `${systemConfig.defaultAPIURL}api/candidate-viewed-job/manage-list-job-viewed`,
			type: "POST",
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
			},
			dataType: "json",
			data: function (d) {
				d.search.value = $("#table_search_all_viewed_job").val().trim() || "";

				return JSON.stringify(d);
			}
		},
		columns: [
			{
				data: "jobName",
				render: function (data, type, row, meta) {
					var diffDate = 0;
					if (row.jobApplyEndDate != "" && row.jobApplyEndDate != null) {
						let endDate = new Date(row.jobApplyEndDate);
						diffDate =  endDate - new Date();
					}
					//console.log(row.jobName + " " + diffDate)
					return `<div> <div class="mb-2"><a title='${row.jobName}' class='job-name-column pxp-company-dashboard-job-title' href="/chi-tiet-cong-viec?jobId=${row.jobId}">${data}</a></div>
						<div class="text-color-money fw-600"><span class='fa-regular fa-badge-dollar me-1'></span> ${row.salaryTypeId == 1001 ? (row.salaryFrom != null ? "Từ " + formatNumber(row.salaryFrom.toString()) : 0) : (row.salaryTypeId == 1002 ? (row.salaryTo != null ? "Đến " + formatNumber(row.salaryTo.toString()) : 0) : (row.salaryTypeId == 1003 ? formatNumber(row.salaryFrom.toString()) + " - " + formatNumber(row.salaryTo.toString()) : "Thỏa thuận"))}</div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.cityRequired.toString() != "" ? "" : "d-none"}'></span> ${row.cityRequired.toString().replaceAll(",", ", ")}</div>
						<div class="${row.jobApplyEndDate != null ? "" : "d-none"} ${diffDate > 0 ? "" : "text-chocolate"}"><span class="fa-regular fa-clock" aria-hidden="true"></span> </span><span>${row.jobApplyEndDate != null ? (diffDate > 0 ? "Hạn nộp hồ sơ : " + moment(row.jobApplyEndDate).format("DD/MM/YYYY HH:mm:ss") : "Đã hết hạn ứng tuyển") : ""}</div>
					</div>`;
				}
			},
			{
				data: "companyName",
				render: function (data, type, row, meta) {
					return `<div><div class="mb-2"><a title='${row.companyName}' class='pxp-company-dashboard-job-title' href="/job-company-best/${row.companyId}">${data}</a></div>
						<div><span class='fa-solid fa-location-dot me-1 ${row.companyAddress != "" ? "" : "d-none"}'></span> ${row.companyAddress}</div>
						<div><span class='fa-regular fa-earth-americas me-1 ${row.companyWebsite != "" ? "" : "d-none"}'></span><a href="${row.companyWebsite}" class='${row.companyWebsite != "" ? "" : "d-none"}'> ${row.companyWebsite}</a></div>
					</div>`;
				}
			},
			
			{
				data: "createdTime",
				render: function (data, type, row, meta) {
					return moment(data).format("DD/MM/YYYY HH:mm:ss");
				}
			},

		],
		columnDefs: [
			{
				targets: [-1],
				className: "text-center status-apply-job"
			}
		],
		drawCallback: function () {
			$('#table_candidate_viewed_job tfoot').html("");
			innitTblPagingViewedJob("#viewed-job-paging");
		}
	});
	$("#pills-contact").on("click", ".page-link-viewed-job", function (e) {
		if ($(this).hasClass("active")) {
			return;
		}
		let pageIndex = $(this).attr("data-index");
		if (pageIndex) {
			tableViewedJob.page(parseInt(pageIndex)).draw('page');
		}
	})
	$("#table_search_all_viewed_job").on("keypress", function (e) {
		if (e.which == 13) {
			tableViewedJob.draw();
		}
	})
}
function innitTblPagingViewedJob(element) {
	let info = tableViewedJob.page.info();
	let totalPage = info.pages;
	let pageIndex = info.page;
	if (totalPage > 0) {
		let html = "";
		let startPage;
		if (totalPage <= 3) {
			startPage = 1;
		}
		else {
			if (totalPage == pageIndex) {
				startPage = totalPage - 2;
			}
			else {
				startPage = pageIndex == 0 ? 1 : pageIndex;
			}

		}
		let endPage = startPage + 2 <= totalPage ? startPage + 2 : totalPage;
		if (pageIndex >= 1) {
			html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link-viewed-job page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex - 1}"   class="page-link-viewed-job page-link"><i class="fa fa-angle-left"></i></a></li>`;
		}
		for (var i = startPage; i <= endPage; i++) {
			if (i > 0) {
				html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link page-link-viewed-job ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
			}
		}
		if (pageIndex < totalPage - 1) {
			html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link-viewed-job page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link-viewed-job page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
		}
		$(element).html(html);
	}
	else {
		$(element).html("");
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

function formatNumber(n) {
	// format number 1000000 to 1,234,567
	return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}