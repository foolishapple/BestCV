/**
 * Author: TUNGTD
 * Created:19/09/2023
 * Description:  Fillter find job javascript
 */
"use strict";
$(document).ready(function () {
    loadDataCity();
    $("#search-home-page").click(function (e) {
        e.preventDefault();
        let keyword = $("#keyword").val().trim();
        let location = $("#location").val();
        if (localStorage.searhHistory) {
            if (keyword.length > 0) {
                let history = JSON.parse(localStorage.searhHistory);
                if (history.length > 5) {
                    history.splice(0, 1);
                }
                history.push(keyword);
                localStorage.setItem("searhHistory", JSON.stringify(history));
            }
        }
        else {
            if (keyword.length > 0) {
                let history = [keyword];
                localStorage.setItem("searhHistory", JSON.stringify(history));
            }
        }
        window.location.href = "/tim-kiem-viec-lam?tukhoa=" + keyword + "&diadiem=" + location;
    })
    $("#keyword").on("focus", function () {
        if (!$(".SearchBar-module_searchBar__MO_5k").hasClass("SearchBar-module_isFocus__3DljF")) {
            $("#searchMenuBar").append(`<div id="dropMenuBar" class="SearchBar-module_bottomSection__2cTXr SearchBar-module_visible__3sF5E ">
                                                <div class="SearchBar-module_midList__1gbDK row p-2">
                                                <div class="SearchBar-module_leftRecentList__3dmRy col-6"><span class="SearchBar-module_recentListTitle__1Mvar mb-3">Có Thể Bạn Quan Tâm</span>
                                                <div id="searchItem" class="SearchBar-module_suggestionKeyword__2ieq1 d-flex flex-column">
                                                </div></div>
                                                <div class="SearchBar-module_rightPopularKeyword__tZ_xH col-6">
                                                <span>Tìm Kiếm Gần Đây</span> <span class="SearchBar-module_clearAll__2AMD4 d-none">Xoá hết</span>
                                                    <ul id="searhHistory">
                                                    </ul>
                                                    </div>
                                                </div>
                                            </div>`)
            loadSearchHistory();
            let value = $("#keyword").val().trim();
            if (value.length == 0) {
                loadJobSuggestion();
            }
            else {
                loadSearchJobSuggestion(value);
            }
            $(".SearchBar-module_searchBar__MO_5k").addClass("SearchBar-module_isFocus__3DljF");
            $(".SearchBar-module_rightPopularKeyword__tZ_xH").on("click", ".btn-close", function (e) {
                let dataIndex = parseInt($(this).attr("data-index"));
                if (localStorage.searhHistory) {
                    let history = JSON.parse(localStorage.searhHistory);
                    history.splice(dataIndex, 1);
                    localStorage.setItem("searhHistory", JSON.stringify(history));
                    loadSearchHistory();
                }
            });
            $(".SearchBar-module_clearAll__2AMD4").on("click", function () {
                localStorage.removeItem("searhHistory");
                loadSearchHistory();
                $(this).addClass("d-none");
            })
        }
    })
    $("#keyword").on("blur", function () {
        let val = $(this).val();
        $(this).val(val.trim());
    })
    $("#keyword").on("input", function () {
        let element = $(this);
        let value = element.val().trim();
        if (value.length == 0) {
            $(".SearchBar-module_recentListTitle__1Mvar").text("Có Thể Bạn Quan Tâm");
        }
        else {
            $(".SearchBar-module_recentListTitle__1Mvar").text("Tìm Kiếm Theo Từ Khóa");
        }
        setTimeout(function () {
            if (value == $("#keyword").val().trim()) {
                if (value.length == 0) {
                    loadJobSuggestion();
                }
                else {
                    loadSearchJobSuggestion(value);
                }
            }
        }, 700);
    })
    $(window).click(function (e) {
        if ($(".SearchBar-module_searchBar__MO_5k").hasClass("SearchBar-module_isFocus__3DljF")) {
            $("#dropMenuBar").animate({
                height: 'toggle'
            });
            $("#dropMenuBar").remove();
            $(".SearchBar-module_searchBar__MO_5k").removeClass("SearchBar-module_isFocus__3DljF");
        }
    });

    $('#searchMenuBar').click(function (event) {
        event.stopPropagation();
    });
})

async function loadSearchJobSuggestion(key) {
    $(".SearchBar-module_recentListTitle__1Mvar").text("Tìm Kiếm Theo Từ Khóa");
    let jobSource = [];
    try {
        let result = await httpService.getAsync(`api/job/search-suggestion?keyword=${key}`);
        if (result.isSucceeded) {
            jobSource = result.resources;
        }
    } catch (e) {
        console.error(e);
    }
    $("#searchItem").html("");
    if (jobSource.length > 0) {
        jobSource.forEach(function (item) {
            let jobName = item.name;
            let regex = new RegExp( key , "gi");
            let result = jobName.replaceAll(regex, word => "<b>" + word + "</b>");
            $("#searchItem").append(`<div><a href="/chi-tiet-cong-viec/${stringToSlug(item.name) + "-" + item.id}"><div class="suggest_job mb-2"><div class="company_photo" style="background-image: url('${systemConfig.defaultStorageURL + item.companyPhoto}');"></div><div class="job_name" data-bs-toggle="tooltip" data-bs-placement="top" title="${item.name}"> ${result}</div></div></div></a></div>`)
        });
        $(document).trigger("tooltip");
    }
    else {
        $("#searchItem").append('<div>Không tìm thấy kết quả phù hợp.</div>')
    }
}

async function loadJobSuggestion() {
    let result = await httpService.getAsync("api/job/list-suggestion");
    let jobSource = [];
    try {
        if (result.isSucceeded) {
            jobSource = result.resources;
        }
    } catch (e) {
        console.error(e);
    }
    $(".SearchBar-module_recentListTitle__1Mvar").text("Có Thể Bạn Quan Tâm");
    $("#searchItem").html("");
    if (jobSource.length > 0) {
        jobSource.forEach(function (item) {
            $("#searchItem").append(`<div><a href="/chi-tiet-cong-viec/${stringToSlug(item.name) + "-" + item.id}"><div class="suggest_job mb-2"><div class="company_photo" style="background-image: url('${systemConfig.defaultStorageURL + item.companyPhoto}');"></div><div class="job_name" data-bs-toggle="tooltip" data-bs-placement="top" title="${item.name}"> ${item.name}</div></div></div></a></div>`)
        });
        $(document).trigger("tooltip");
    }
    else {
        $("#searchItem").append('<div>Không thấy kết quả phù hợp.</div>')
    }
}
function loadSearchHistory() {
    $("#searhHistory").html("");
    if (localStorage.searhHistory) {
        let history = JSON.parse(localStorage.searhHistory);
        if (history.length > 0) {
            for (var i = 0; i <= 5; i++) {
                if (history[5-i]) {
                    $("#searhHistory").append(`<li> <a href="/tim-kiem-viec-lam?tukhoa=${history[5-i]}"><span class="fa fa-clock-o pe-2"></span><span>${history[5-i]}</span></a> <button class="btn btn-close" data-index="${5-i}"></button><li>`);   
                }
            }
            $(".SearchBar-module_clearAll__2AMD4").removeClass("d-none");
        }
        else {
            $(".SearchBar-module_clearAll__2AMD4").addClass("d-none");
            $("#searhHistory").append(`<li><span>Không có tìm kiếm nào gần đây.<span><li>`)
        }
    }
    else {
        $(".SearchBar-module_clearAll__2AMD4").addClass("d-none");
        $("#searhHistory").append(`<li><span>Không có tìm kiếm nào gần đây.<span><li>`)
    }
}

function loadDataCity() {
    $("#location").empty();
    $.ajax({
        url: systemConfig.defaultAPIURL + "api/workplace/list-city",
        type: "GET",
        success: function (responseData) {
            if (responseData.resources.length > 0) {
                $('#location').append(new Option("Tất cả tỉnh thành", 0, false, false)).trigger('change');

                responseData.resources.forEach(function (item) {
                    $('#location').append(new Option(item.name, item.id, false, false)).trigger('change');

                })
                $("#location").select2();
            }

        },
        error: function (e) {
            $("#loading").removeClass("show");
        },
    })
}