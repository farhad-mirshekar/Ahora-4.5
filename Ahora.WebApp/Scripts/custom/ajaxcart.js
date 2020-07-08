var AjaxCart = {

    //add a product to the cart
    addproducttocart_details: function (urladd, formselector) {

        $.ajax({
            cache: false,
            url: urladd,
            data: $(formselector).serialize(),
            type: 'post',
            success: this.success_process,
            error: this.ajaxFailure
        });
    },

    //add a product to the compare list
    addproducttocomparelist: function (urladd) {
        $.ajax({
            cache: false,
            url: urladd,
            type: "POST",
            success: this.success_process,
            error: this.ajaxFailure
        });
    },
    success_process: function (response) {
        if (response.message) {
            if (response.success === true)
                var noty = window.noty({ text: response.message, type: 'success', timeout: 1500 });
            else
                var noty = window.noty({ text: response.message, type: 'warning', timeout: 1500 });
        }
        if (response.redirect) {
            setTimeout(function () {
                location.href = response.redirect;
                return true;
            }, 1000);
        }
        return false;
    },
    ajaxFailure: function () {
        alert('Failed to add the product to the cart. Please refresh the page and try one more time.');
    }
};

function setLocation(url) {
    window.location.href = url;
}