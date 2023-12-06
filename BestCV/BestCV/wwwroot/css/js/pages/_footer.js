function loadDataFooterCandidate() {
    $("#footer-section").html('');

    FooterCandidate.forEach(function (item, index) {
        if (item.ParentId == null || item.ParentId == 0) {
            var newRow = `
                    <li>
                        <a href="${item.Link}" target="_blank">${item.Name}</a>
                    </li>
            `
            $("#footer-section").append(newRow);
        };
    })

}

loadDataFooterCandidate();