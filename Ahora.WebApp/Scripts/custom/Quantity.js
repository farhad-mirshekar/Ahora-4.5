var Quantity = new Object();
$('.quantity').children('i').on('click', function (event) {
    event.preventDefault();
    var $this = $(this);
    $('#load').css('display', 'block');
    $.ajax({
        type: "POST",
        url: $this.attr('data-href'),
        success: Quantity.success
    });
})

Quantity.success = function (data) {
    if (data.error === 2) {
        var noty = window.noty({ text: 'موجودی کالا از تعداد تقاضای شما کمتر می باشد', type: 'warning', timeout: 2500 });
    } else if (data.error === 1){
        var noty = window.noty({ text: 'موجودی کالا نمی تواند کمتر از یک باشد', type: 'warning', timeout: 2500 });
    }
    else {
        $('#cart').empty();
        $('#cart').html(data);
    }
    $('#load').css('display', 'none');
}

//////////////////////////////////////////////////
var ShoppingCart = new Object();
$('#stepFirstShopping').on('click', function (event) {
    var $this = $(this);

    var type = $this.attr('data-type');
    event.preventDefault();
    switch (type) {
        case 'haveAddress':
            var flag = false;
            var item = '';
            var parent = $('.radio').children('input');
            for (var i = 0; i < parent.length; i++) {
                if (parent[i].type = "radio") {
                    if ($(parent[i]).attr("data-select") === "true") {
                        item = $(parent[i]).val();
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag) {
                var noty = window.noty({ text: 'آدرس را انتخاب کنید', type: 'error', timeout: 1000 });
            } else {
                $.ajax({
                    type: 'POST',
                    data: { ID: item },
                    url: '/shoppingCart/Shopping',
                    success: ShoppingCart.success
                });
            }
            break;
        case 'notHaveAddress':
            var flag = false;
            var postalCode = $('#PostalCode').val();
            var Address = $('#Address').val();

            if (postalCode === null || postalCode === "") {
                flag = true;
                var noty = window.noty({ text: 'کد پستی را وارد نمایید', type: 'error', timeout: 1000 });
            }

            if (Address === null || Address === "") {
                flag = true;
                var noty = window.noty({ text: 'آدرس را وارد نمایید', type: 'error', timeout: 1000 });
            }

            if (!flag) {
                var model = { Address: Address, PostalCode: postalCode };
                $.ajax({
                    type: 'POST',
                    data:model,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'},
                    url: '/shoppingCart/Shopping',
                    success: ShoppingCart.success
                });
            }

            break;
    }


})
$('.radio').children('input').on('click', function (event) {
    var $this = $(this);
    var id = $this[0].id;
    $this.removeAttr("data-select");
    $this.attr("data-select", "true");
    var parent = $('.radio').children('input');
    for (var i = 0; i < parent.length; i++) {
        if (parent[i].type = "radio") {
            if (id !== parent[i].id) {
                $(parent[i]).removeAttr("data-select");
            }
        }
    }

})
ShoppingCart.success = function (data) {
    if (data.status) {
        window.location = data.url;
    }
    else {
        switch (data.type) {
            case 1:
                var noty = window.noty({ text: data.message, type: 'error', timeout: 1000 });
                break;
            case 2:
                var noty = window.noty({ text: data.message, type: 'error', timeout: 1000 });
                break;
        }

    }
}
//////////////////////////////////////////////////
var CartRemove = new Object();
$('.delete').on('click', function (event) {
    event.preventDefault();
    event.preventDefault();
    var $this = $(this);
    $('#load').css('display', 'block');
    $.ajax({
        type: "POST",
        url: $this.attr('data-href'),
        success: CartRemove.success
    });
});
CartRemove.success = function (data) {
    $('#cart').empty();
    $('#load').css('display', 'none');
    $('#cart').html(data);
}