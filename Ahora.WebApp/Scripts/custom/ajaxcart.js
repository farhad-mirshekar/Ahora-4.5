/*
** nopCommerce ajax cart implementation
*/
var AjaxCart = {

    //add a product to the cart/wishlist from the product details page
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

    success_process: function (response) {
        if (response.success) {
            var noty = window.noty({ text: response.message, type: 'success', timeout: 2500 });
            setTimeout(function () {
                if (response.redirect) {
                    location.href = response.redirect;
                    return true;
                }
            }, 4000);
        }
        else {
            var noty = window.noty({ text: response.message, type: 'warning', timeout: 2500 });
            return false;
        }
        return false;
    },

    ajaxFailure: function () {
        alert('Failed to add the product to the cart. Please refresh the page and try one more time.');
    }
};