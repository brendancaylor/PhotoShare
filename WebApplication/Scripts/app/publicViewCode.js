function showPayPalModal() {
    if (!app.models.setPaylPalButton) {
        app.models.setPaylPalButton = true;
        setPaylPalButton();
    }
    $('#payPalModal').modal('show');
}

function uncheckAll() {

    var selectedPhotos = $("input:checked");

    jQuery.each(selectedPhotos, function (indx, control) {
        control.checked = false;
    });
    updateBuyVariables();
}

function updateBuyVariables() {
    //app.models
    var paylPallOptions = {
        amount: 0,
        itemnumber: '',
    };
    var selectedPhotos = $("input:checked");

    jQuery.each(selectedPhotos, function (indx, control) {
        var payPalId = $(control).val();
        if (paylPallOptions.itemnumber.indexOf(payPalId) == -1) {
            if (paylPallOptions.itemnumber.length > 0) {
                paylPallOptions.itemnumber += ",";
            }
            paylPallOptions.itemnumber += payPalId;
        }
        log(paylPallOptions.itemnumber);
    });

    $(".numberOfPicsSelected").html(selectedPhotos.length);
    if (selectedPhotos.length > 0) {
        $(".buyBox").show();
        paylPallOptions.amount = photoAmount * selectedPhotos.length;
    }
    else {
        $(".buyBox").hide();
    }

    $(".costOfPics").html(paylPallOptions.amount.toFixed(2));
    app.models.paylPallOptions = paylPallOptions;
}

function updateBuyDisplay() {
    // redundant
    var selectedPhotos = $("input:checked");
    var itemnumber = "";
    var amount = photoAmount;

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
    
    $(".numberOfPicsSelected").html(selectedPhotos.length);
    if (selectedPhotos.length > 0) {
        $(".buyBox").show();
        $("#amount").val(amount * selectedPhotos.length);
    }
    else {
        $(".buyBox").hide();
    }

    $("#item_number").val(itemnumber);
    $(".costOfPics").html($("#amount").val());

}

function submitPayPal() {
    // redundant now
    var invoice = randomString(8, '2346789abcdefghjklmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ');
    $("#invoice").val(invoice);

    //var selectedPhotos = $("input[name*='selectedPhotos']");
    $("#paypalForm").submit();
}

function showBuy(control) {
    //updateBuyDisplay();
    //$("#" + control).html("click to pay or select more");
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

function setPaylPalButton() {
    var invoiceNumber = randomString(8, '2346789abcdefghjklmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ');

    // production: 'AcXJb3pasps7gU322AUEdZheyxY6lWcAi-DHK6OFyf_32XxBhPzpvW2MmFejS9suao9kOyMYAG3v9VOA'
    // sandbox: 'AfbEML-pa6t7QzVEd19LZlOAiWVPw_aq-cCq9P2yTJYsa7JOcOQ-R-imOSkgGA5iJLAIKMpQi65iAKVf'

    paypal.Button.render({
        env: 'sandbox',
        client: {
            sandbox: 'AfbEML-pa6t7QzVEd19LZlOAiWVPw_aq-cCq9P2yTJYsa7JOcOQ-R-imOSkgGA5iJLAIKMpQi65iAKVf'
        },
        payment: function (data, actions) {
            return actions.payment.create(

                {
                    transactions: [{
                        amount: {
                            total: app.models.paylPallOptions.amount,
                            currency: 'GBP'
                        },
                        description: 'mi-photoshare images. £' + app.models.paylPallOptions.amount.toFixed(2),
                        invoice_number: invoiceNumber
                    }]
                }
                
            );
        },
        onAuthorize: function (data, actions) {
            return actions.payment.execute()
                .then(function (palPalAuthorizeResponse) {
                    debugger;
                    palPalAuthorizeResponse.photoCodes = app.models.paylPallOptions.itemnumber;
                    uncheckAll();
                    $('#payPalModal').modal('hide');
                    $('#pleaseWait').modal({ keyboard: false, backdrop: 'static', show: true});

                    app.api.callApi(palPalAuthorizeResponse, '../paypal/payPalCallBack', true,
                        function (salesCodes) {
                            window.location.href = './confirmation?salesCodes=' + salesCodes;
                            //setTimeout(function () { window.alert('Thank you for your purchase! You will be emailed shortly with your purchase codes :-)');}, 2000);
                        }
                    );
                    //
                });
        }
    }, '#paypal-button');

}