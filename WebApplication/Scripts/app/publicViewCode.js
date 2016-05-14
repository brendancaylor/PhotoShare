


function submitPayPal(itemname, itemnumber, amount) {

    var invoice = randomString(8, '2346789abcdefghjklmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ');

    $("#item_name").val(itemname);
    $("#invoice").val(invoice);
    $("#amount").val(amount);

    //var selectedPhotos = $("input[name*='selectedPhotos']");
    var selectedPhotos = $("input:checked");
    
    jQuery.each(selectedPhotos, function (indx, control) {
        var payPalId = $(control).val();
        if (itemnumber.indexOf(payPalId) == -1) {
            if (itemnumber.length > 0) {
                itemnumber += ",";
            }
            itemnumber += payPalId;
        }
        log(itemnumber);
    });

    if (selectedPhotos.length > 0) {
        $("#amount").val(amount * selectedPhotos.length);
    }

    $("#item_number").val(itemnumber);
    $("#paypalForm").submit();
}

function showBuy(control) {
    $("#" + control).html("click to pay or select more");
}

function randomString(length, chars) {
    var result = '';
    for (var i = length; i > 0; --i) result += chars[Math.round(Math.random() * (chars.length - 1))];
    return result;
}

var rString = randomString(32, '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ');


function log(obj) {
    if (typeof console != "undefined") {
        if (typeof obj == "string") {
            console.log(obj);
            return obj;
        } else {
            var fmt = JSON.stringify(obj);
            console.log(fmt);
            return fmt;
        }
    }

    return "";
};