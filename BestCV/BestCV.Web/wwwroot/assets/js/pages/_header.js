loadDataHeaderCandidate();
loadDataHeaderCandidateMobile();

function loadDataHeaderCandidate() {
    $(".header-candidate").html('');

    $.ajax({
        url: systemConfig.defaultAPIURL + "api/menu/list-all-header-candidate",
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {
            let data = responseData;
            dataSource = data.Resources;

            let parentItems = dataSource.filter(item => item.ParentId == null);

            parentItems.forEach(function (item, index) {
                var newRow = `
                <li class="nav-item dropdown" id="parent-${item.Id}">
                   <a href="${item.Link}">${item.Name}</a>
                </li>`;
                $(newRow).appendTo($(".header-candidate"));

                recursionMenuItem(item);
            });
        }
    });
}

function recursionMenuItem(parent_item) {
    var children = dataSource.filter(x => x.ParentId == parent_item.Id);
    if (children.length < 1) {
        return;
    }

    var newRow = "<ul class='dropdown-menu' style='padding:15px'>";

    children.forEach(function (item, index) {
        var children1 = dataSource.filter(x => x.ParentId == item.Id);
        if (children1.length < 1) {
            newRow += `
                <li class="nav-item click-menu" id="sub-menu-${item.Id}" style ="width:200px;padding-top: 5px;">
                    <a href="${item.Link}">${item.Name}</a>
                </li>`;
        } else {
            newRow += `<li class="nav-item dropdown" id="parent-${item.Id}">
                <a href="${item.Link}">${item.Name}</a>
            </li>`;
        }
    });

    newRow += "</ul>";

    $('#parent-' + parent_item.Id).append(newRow);

    $('#parent-' + parent_item.Id).on("mouseenter", function () {
        $(this).find(".dropdown-menu").show();
    }).on("mouseleave", function () {
        $(this).find(".dropdown-menu").hide();
    });

    children.forEach(function (item) {
        recursionMenuItem(item);
    });
}
function loadDataHeaderCandidateMobile() {
    $(".header-candidate-mobile").html('');

    $.ajax({
        url: systemConfig.defaultAPIURL + "api/menu/list-all-header-candidate",
        type: "GET",
        contentType: "application/json",
        success: function (responseData) {
            let data = responseData;
            dataSource = data.Resources;

            let parentItems = dataSource.filter(item => item.ParentId == null);

            parentItems.forEach(function (item, index) {
                var newRow = `
                <li class="nav-item dropdown" id="parent-${item.Id}">
                   <a href="${item.Link}">${item.Name}</a>
                </li>`;
                $(newRow).appendTo($(".header-candidate-mobile"));

                recursionMenuMobileItem(item);
            });
        }
    });
}

function recursionMenuMobileItem(parent_item) {
    var children = dataSource.filter(x => x.ParentId == parent_item.Id);
    if (children.length < 1) {
        return;
    }

    var newRow = "<ul class='dropdown-menu'>";

    children.forEach(function (item, index) {
        var children1 = dataSource.filter(x => x.ParentId == item.Id);
        if (children1.length < 1) {
            newRow += `
                <li class="nav-item click-menu" id="sub-menu-${item.Id}">
                    <a href="${item.Link}">${item.Name}</a>
                </li>`;
        } else {
            newRow += `<li class="nav-item dropdown" id="parent-${item.Id}">
                <a href="${item.Link}">${item.Name}</a>
            </li>`;
        }
    });

    newRow += "</ul>";

    $('#parent-' + parent_item.Id).append(newRow);

    $('#parent-' + parent_item.Id).on("mouseenter", function () {
        $(this).find(".dropdown-menu").show();
    }).on("mouseleave", function () {
        $(this).find(".dropdown-menu").hide();
    });

    children.forEach(function (item) {
        recursionMenuMobileItem(item);
    });
}