"use strict";
const container = document.getElementById('kt_docs_toast_stack_container');
const targetElement = document.querySelector('[data-kt-docs-toast="stack"]'); // Use CSS class or HTML attr to avoid duplicating ids
// Remove base element markup
targetElement.parentNode.removeChild(targetElement);
var NOTI_CONNECTION = new signalR.HubConnectionBuilder().withUrl(systemConfig.defaultAPI_URL + "EmployerNotificationHub", {
    accessTokenFactory: () => JSON.parse(localStorage.currentLoggedInUserJobi).token
}).build();

NOTI_CONNECTION.start().then(function () {
    
    loadUnreadTotal();
}).catch(function (err) {
    return console.error(err.toString());
});

NOTI_CONNECTION.on("SendNotifications", function (res) {
   
   
    loadDataAndCountNotifications();

    Notification.requestPermission().then(perm => {
        if (perm === "granted") {
            const notification = new Notification(res.name, {
                body: res.description,
                icon: "/images/company-logo-1.png" // Đường dẫn tới biểu tượng
            });

            // Thêm sự kiện khi thông báo được nhấp vào
            notification.addEventListener("click", function () {
                window.location.href = res.link;
            });

            notification.addEventListener("error", e => {
                console.log(e);
            });
        }
    });

    const newToast = targetElement.cloneNode(true);
    $(newToast).find(".toast-content").html(res.description);
    $(newToast).find(".toast-title").html(res.name);
    container.append(newToast);

    const toast = bootstrap.Toast.getOrCreateInstance(newToast);
    toast.show();
});

