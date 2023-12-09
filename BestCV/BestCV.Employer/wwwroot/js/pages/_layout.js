"use strict";


$(document).ready(() => {
    ///add class text_editor to textarea to use CK Editor 4
    for (var i = 0; i < $(".text_editor").length; i++) {
        var editor = CKEDITOR.replace($(".text_editor")[i]);
        $(editor.element).on("change", function (e) {
            //debugger;
            editor.setData($(this).val());//set data
        })
    }
    $("[control=select2]").select2({
        width: "resolved"
    });
    $(document).on("input", "input[type=number][control=number]", function (e) {
        e.preventDefault();
        try {
            let target = $(this);
            let type = target.attr("input-type");
            let min = parseInt(target.attr("min"));
            let max = parseInt(target.attr("max"));
            let value = new Number(target.val());
            if (min != null && min != undefined) {
                if (value < min) {
                    value = min;
                }
            }
            if (max != null && max != undefined) {
                if (value > max) {
                    value = max;
                }
            }
            switch (type) {
                case "int":
                    {
                        target.val(parseInt(value));
                    }
                    break;
                default:
                    break;
            }
            
        } catch (e) {
            console.error(e);
        }
    })
})

$(document).on("autoresize input", "textarea[autosize=true]", function(e) {
    e.preventDefault();
    this.style.height = 'auto'; // Reset the height to auto to allow the element to shrink
    if (this.scrollHeight > 250) {
        this.style.height = this.scrollHeight + 'px'; // Set the height to match the content
    }
})


function reGenDataTable(t, f) {
    t.destroy();
    f();
}
function reGenTable() {
    tableUpdating = 0;
    table.destroy();
    $(".tableHeaderFilter").val(null).trigger("change");
    $("#tableData tbody").html('');
    loadData();
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
function stringToSlug(text) {
    text = text.toLowerCase().trim();

    // Define a map of special characters and their replacements
    const specialChars = {
        'à': 'a',
        'á': 'a',
        'ả': 'a',
        'ã': 'a',
        'ạ': 'a',
        'ă': 'a',
        'ắ': 'a',
        'ằ': 'a',
        'ẳ': 'a',
        'ẵ': 'a',
        'ặ': 'a',
        'â': 'a',
        'ấ': 'a',
        'ầ': 'a',
        'ẩ': 'a',
        'ẫ': 'a',
        'ậ': 'a',
        'đ': 'd',
        'è': 'e',
        'é': 'e',
        'ẻ': 'e',
        'ẽ': 'e',
        'ẹ': 'e',
        'ê': 'e',
        'ế': 'e',
        'ề': 'e',
        'ể': 'e',
        'ễ': 'e',
        'ệ': 'e',
        'ì': 'i',
        'í': 'i',
        'ỉ': 'i',
        'ĩ': 'i',
        'ị': 'i',
        'ò': 'o',
        'ó': 'o',
        'ỏ': 'o',
        'õ': 'o',
        'ọ': 'o',
        'ô': 'o',
        'ố': 'o',
        'ồ': 'o',
        'ổ': 'o',
        'ỗ': 'o',
        'ộ': 'o',
        'ơ': 'o',
        'ớ': 'o',
        'ờ': 'o',
        'ở': 'o',
        'ỡ': 'o',
        'ợ': 'o',
        'ù': 'u',
        'ú': 'u',
        'ủ': 'u',
        'ũ': 'u',
        'ụ': 'u',
        'ư': 'u',
        'ứ': 'u',
        'ừ': 'u',
        'ử': 'u',
        'ữ': 'u',
        'ự': 'u',
        'ỳ': 'y',
        'ý': 'y',
        'ỷ': 'y',
        'ỹ': 'y',
        'ỵ': 'y',
        'ñ': 'n',
        'ç': 'c',
        'ß': 'ss',
        ' ': '-',
        '_': '-',
        '+': '-',
    };

    // Replace special characters with their counterparts
    text = text.replace(/[^a-z0-9-]/g, (char) => specialChars[char] || '');

    // Replace multiple consecutive hyphens with a single hyphen
    text = text.replace(/-+/g, '-');

    return text;
}

