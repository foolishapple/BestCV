"use strict";

$(document).on("change", "[data-bs-toggle=tooltip]", function () {
	let tooltip = new bootstrap.Tooltip(this, {});
})
var table;
var updatingObj = {};
var candidateApplyJobStatusSource = [], recruitmentCampaignSource = [], candidateApplyJobSourceSource=[];
function initTable() {
	table = $("#table_candidate").DataTable({
		language: systemConfig.languageDataTable,
		processing: true,
		serverSide: true,
		paging: true,
		searching: { regex: true },
		autoWidth: false,
		ordering: false,
		ajax: {
			url: `${systemConfig.defaultAPI_URL}api/candidate-apply-job/list-to-employer`,
			type: "POST",
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.token);
			},
			dataType: "json",
			data: function (d) {
				d.search.value = $("#search_all").val().trim() || "";
				d.isViewUnread = $("#fillter_unread").val() == "unread" || false;
				d.candidateApplyJobStatusIds = $("#fillterStatus").val() != null ? [$("#fillterStatus").val()] : [];
				d.recruitmentCampaignIds = $("#fillterRecruitmentCampaig").val() != null ? [$("#fillterRecruitmentCampaig").val()] : [];
				d.candidateApplyJobSourceIds = $("#fillterSource").val() != null ? [$("#fillterSource").val()] : [];
				return JSON.stringify(d);
			}
		},
		columns: [
			{
				data: "id",
				render: function (data, type, row, meta) {
					return meta.row + 1;
				}
			},
			{
				data: "candidateAvatar",
				render: function (data, type, row, meta) {
					return `<div class="pxp-company-dashboard-candidate-avatar pxp-cover bg-secondary" style="background-image: url(${systemConfig.defaultStorage_URL + row.candidateAvatar});"></div>`;
				}
			},
			{
				data: "title",
				render: function (data, type, row, meta) {
					let viewed = row.isEmployerViewed;
					return `<a class="employerCV" data-target="${row.id}" href="${systemConfig.defaultStorage_URL + row.candidateCVPDFUrl}" target="_blank">
                                <div class="pxp-company-dashboard-job-title">${row.candidateName}</div>
                                <div class="pxp-company-dashboard-job-status">
                                    <span class=" badge  rounded-pill ${viewed ? `bg-secondary text-dark` : `bg-primary`} ">${viewed ? `Đã đọc` : `Chưa đọc`}</span>
                                </div>
                            </a>`;
				}
			},
			{
				data: "recruitmentCampaign",
				render: function (data, type, row, meta) {
					return `<div class="d-flex flex-column">
                                <a href="../recruitment-campaign/dashboard/${row.recruimentCampaignId}" class="candidate_recruitment_campaign" data-bs-toggle="tooltip" data-bs-placement="top"
                                   data-bs-custom-class="custom-tooltip"
                                   data-bs-title="${row.recruimentCampaignName}">${row.recruimentCampaignName}</a>
                                <div>
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.recruimentCampaignId}</span>
                                </div`;
				}
			},
			{
				data: "contact",
				render: function (data, type, row, meta) {
					return `<div class="candidate_contact">
                                <div class="item ${row.candidateEmail == null || row.candidateEmail == `` || row.candidateEmail == undefined ? `d-none` : ``} mb-1" title="Email"><i class="fa text-primary fa-envelope me-2"></i>${row.candidateEmail}</div>
                                <div class="item ${row.candidatePhone == null || row.candidatePhone == `` || row.candidatePhone == undefined ? `d-none` : ``}" title="Số điện thoại"><i class="fa text-primary fa-circle-phone me-2"></i>${row.candidatePhone}</div>
                            </div>`;
				}
			},
			{
				data: "other",
				render: function (data, type, row, meta) {
					return ` <div class="candidate_more">
                                <div class="item mb-1" title="Nguồn ứng tuyển"><i class="fa-regular fa-briefcase-arrow-right me-2"></i>${row.candidateApplyJobSourceName}</div>
                                <div class="item mb-1" title="Ngày ứng tuyển"><i class="fa-regular fa-clock me-2"></i>${moment(row.createdTime).format(`DD/MM/YYYY HH:mm`)}</div>
                                <div class="item" title="Tên công việc"><i class="fa-regular fa-briefcase me-2"></i>${row.jobName}</div>
                            </div>`;
				}
			},
			{
				data: "status",
				render: function (data, type, row, meta) {
					let color = row.candidateApplyJobStatusColor;
					return `<span type="button"  class="badge px-2 rounded-pill btn_change_status" data-id=${row.id} style="color: ${color};background-color: ${customBagdeColor(color)}; opacity: 0.7;">${row.candidateApplyJobStatusName}</span>`;
				}
			},
			{
				data: "action",
				render: function (data, type, row, meta) {
					return ` <div class="candidate_action dropdown text-center">
                                <button class="dropdown-toggle btn_icon" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><span class="fa fa-ellipsis-h"></span></button>
                                <ul class="dropdown-menu list-unstyled shadow ">
                                    <li><a class="dropdown-item btn_change_status" data-id=${row.id} href="#!" title="Đổi trạng thái CV">Đổi trạng thái CV</a></li>
                                    <li><a class="dropdown-item btn_add_note" href="#!" data-id=${row.id} title="Cập nhật ghi chú">Ghi chú</a></li>
									<li><a class="dropdown-item btn_add_shechule d-none" href="#!" title="Hẹn phỏng vấn">Hẹn phỏng vấn</a></li>
                                    <li><a class="dropdown-item d-none" href="#!" title="Approve">Tải CV</a></li>
                                    <li><a class="dropdown-item d-none" href="#!" title="Reject">Sao chép mã ứng viên</a></li>
                                </ul>
                            </div>`;
				}
			}
		],
		columnDefs: [
			{
				targets: [0, -1],
				className: "text-center"
			},
			{
				targets: [0],
				className: "pt-4"
			}
			,
			{
				targets: [1],
				className: "column-avatar"
			}
		],
		drawCallback: function () {
			$('#table_candidate tfoot').html("");
			$(".candidate_recruitment_campaign").trigger("change");
			innitTblPaging("#candidate_paging");
			initInfo("#tbl_info");
		}
	});
	$(document).on("click", ".page-link", function (e) {
		if ($(this).hasClass("active")) {
			return;
		}
		let pageIndex = $(this).attr("data-index");
		if (pageIndex) {
			table.page(parseInt(pageIndex)).draw('page');
		}
	})
	$("#search_all").on("keypress", function (e) {
		if (e.which == 13) {
			table.draw();
		}
	})
	$("#table_candidate").on("keypress", ".fillter_tbl", function (e) {
		if (e.which == 13) {
			table.draw();
		}
	})
	$("#table_candidate").on("click", ".btn_search", function (e) {
		table.draw();
	})
	$("#table_candidate").on("click", ".btn_reset", function (e) {
		$("#table_candidate .fillter_tbl").val(null).trigger("change");
		table.draw();
	})
	$("#fillter_unread,#fillterSource,#fillterStatus,#fillterRecruitmentCampaig").on("change", function () {
		table.draw();
	})

	$(".btn_search_all").on("click", function () {
		table.draw();
	})
	$(document).on("click", ".employerCV", function (e) {
		let id = $(this).attr("data-target");
		if (id) {
			let item = table.ajax.json().data.find(c => c.id == id);
			if (item) {
				if (!item.isEmployerViewed) {
					EmployerViewedCV(item)
				}
			}
		}
	})
}

function innitTblPaging(element) {
	let info = table.page.info();
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
			html += `<li class="page-item paging-first-item"><a type="button" href="#!" aria-label="Previous" data-index="0"  class="page-link"><i class="fa fa-angle-double-left"></i></a></li>
                                    <li class="page-item paging-previous"><a type="button" href="#!" aria-label="Previous" data-index="${pageIndex-1}"   class="page-link"><i class="fa fa-angle-left"></i></a></li>`;
		}
		for (var i = startPage; i <= endPage; i++) {
			if (i > 0) {
				html += `<li class="page-item ${i == pageIndex + 1 ? 'active' : ''}" aria-current="page">
                                    <a type="button" data-index="${i - 1}" class="page-link ${i == pageIndex + 1 ? 'active' : ''}">${i}</a>
                                </li>`
			}
		}
		if (pageIndex < totalPage -1) {
			html += `<li class="page-item paging-next"><a type="button" href="#!" aria-label="Next" data-index="${pageIndex + 1}"  class="page-link"><i class="fa fa-angle-right"></i></a></li>
                                    <li class="page-item paging-last-item"><a type="button" href="#!" data-index="${totalPage - 1}" aria-label="Next"  class="page-link"><i class="fa fa-angle-double-right"></i></a></li>`;
		}
		$(element).html(html);
	}
	else {
		$(element).html("");
	}
}

function initInfo(e) {
	let info = table.page.info();
	$(e).text(`${info.recordsDisplay} ứng viên`);
}
$(document).on("click", ".btn_add_note", function (e) {
	let id = $(this).attr("data-id");
	if (id) {
		updatingObj = table.ajax.json().data.find(c => c.id == id);
		if (updatingObj) {
			$("#modal_addNote #candidateApplyJobDescription").val(updatingObj.description);
			$("#modal_addNote").modal("show");
		}
	}
})
$("#modal_addNote .btn_submit").on("click", async function (e) {
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
				let obj = updatingObj;
				obj.description = $("#candidateApplyJobDescription").val();
				let result = await httpService.putAsync("api/candidate-apply-job/add-description", obj);
				$("#loading").removeClass("show");
				if (result.isSucceeded) {
					Swal.fire("Ứng viên", "Ghi chú đã được cập nhật thành công!", "success");
					table.draw();
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
$(document).on("click", ".btn_change_status", function (e) {
	let id = $(this).attr("data-id");
	if (id) {
		updatingObj = table.ajax.json().data.find(c => c.id == id);
		if (updatingObj) {
			$("#ApplyJobDescription").val(updatingObj.description).trigger("change");
			$("#pills-tab-status .active").removeClass("active");
			$(`#pills-tab-status .nav-link[data-id=${updatingObj.candidateApplyJobStatusId}]`).addClass("active");
			$("#modal_change_status").modal("show");
		}
	}
})
$("#modal_change_status .btn_submit").on("click", async function (e) {
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
				let obj = updatingObj;
				obj.description = $("#ApplyJobDescription").val();
				obj.candidateApplyJobStatusId = $("#pills-tab-status .active").attr("data-id");
				let result = await httpService.putAsync("api/candidate-apply-job/change-status", obj);
				$("#loading").removeClass("show");
				if (result.isSucceeded) {
					Swal.fire("Ứng viên", "Trạng thái CV đã được cập nhật thành công!", "success");
					table.draw();
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
	$('#pills-tab-status').html("");
	if (candidateApplyJobStatusSource.length > 0) {
		candidateApplyJobStatusSource.forEach(function (item) {
			$("#fillterStatus").append(new Option(item.name, item.id, false, false)).trigger("change");
			$('#pills-tab-status').append(`<li class="nav-item" role="presentation">
                            <button class="btn active nav-link me-1 mb-1" data-id="${item.id}" type="button" data-bs-toggle="pill" role="tab" aria-controls="pills-status" aria-selected="false">${item.name}</button>
                        </li>`);
		})
	}
	$("#fillterStatus").select2({
		placeholder: "Lọc trạng thái",
		allowClear: true,
		width: "165px"
	})
	$("#fillterStatus").val(null).trigger("change");
}
async function getCandidateApplyJobSource() {
	try {
		let result = await httpService.getAsync("api/candidate-apply-job-source/list-all");
		if (result.isSucceeded) {
			candidateApplyJobSourceSource = result.resources;
		}
		else {
			candidateApplyJobSourceSource = []
		}
	} catch (e) {
		candidateApplyJobSourceSource = [];
		console.error(e);
	}
	candidateApplyJobSourceSource.forEach(function (item) {
		$("#fillterSource").append(new Option(item.name, item.id, false, false));
	})
	$("#fillterSource").select2({
		placeholder: "Lọc nguồn CV",
		allowClear: true,
		width: "155px"
	})
	$("#fillterSource").val(null).trigger("change");
}
async function getRecruitmentCampaign() {
	try {
		let result = await httpService.getAsync("api/recruitment-campaign/list-to-employer");
		if (result.isSucceeded) {
			recruitmentCampaignSource = result.resources;
			recruitmentCampaignSource.forEach(function (item) {
				item.text = item.name + `#|${item.id}`;
			});
		}
		else {
			recruitmentCampaignSource = []
		}
	} catch (e) {
		recruitmentCampaignSource = [];
		console.error(e);
	}
	$("#fillterRecruitmentCampaig").select2({
		placeholder: 'Lọc chiến dịch',
		allowClear: true,
		templateResult: formatRepo,
		templateSelection: formatRepoSelection,
		data: recruitmentCampaignSource,
		width: "165px"
	});
	$("#fillterRecruitmentCampaig").val(null).trigger("change");
	
}
function formatRepo(row) {
	if (row.loading) {
		return row.text;
	}
	var $container = $(`<div >${row.name}</div>
                                <div>
                                    <span class="badge rounded text-dark fw-normal bg-secondary">#${row.id}</span>
                                </div>`)
	return $container;
}

function formatRepoSelection(row) {
	if (row.id == '') {
		return "Lọc chiến dịch";
	}
	return row.name;
}

async function EmployerViewedCV(item) {
	try {
		let result = await httpService.putAsync(`api/candidate-apply-job/employer-viewed/${item.id}`);
		if (result.isSucceeded) {
			$(`.employerCV[data-target=${item.id}]`).find(".pxp-company-dashboard-job-status").html(`<span class="badge  rounded-pill bg-secondary text-dark">Đã đọc</span>`);
		}
		else {
			console.error(e);
		}
	} catch (e) {
		console.error(e);
    }
}
async function init() {
	$.when(await getCandidateApplyJobStatus(), await getRecruitmentCampaign(), await getCandidateApplyJobSource()).done(function () {
		initTable();
	})
}
init();