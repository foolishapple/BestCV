
"use strict";
var table;
async function getServicePackage() {
    let serviceSource = [];
    try {
        let res = await httpService.getAsync("api/employer-service-package-employer/list-service-of-employer");
        if (res.isSucceeded) {
            serviceSource = res.resources;
        }
    } catch (e) {
        console.error(e);
    }
    if (table) {
        table.destroy();
    }
    $("#tbl_service_pack tbody").html("").trigger("change");
    serviceSource.forEach(function (item) {
        $("#tbl_service_pack tbody").append(
            `
            <tr>
                <td class="column-index">#${item.orderId}</td>
                <td>${item.servicePackageName}</td>
                <td>${item.servicePackageGroupName}</td>
                <td>${item.servicePackageTypeName}</td>
                <td>${item.quantity}</td>
                <td class="column-action"></td>
            </tr>
        `)
    })
    table = $("#tbl_service_pack").DataTable({
        language: systemConfig.languageDataTable,
        ordering: false,
        paging: false,
        info: false
    });
}

$(document).ready(function () {
    getServicePackage();
})