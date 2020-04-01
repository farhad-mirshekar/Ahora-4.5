var Address = {
    showModal: function (modal, address,id) {
        $(modal).css('display', 'block');
        $('#address').text(address);
        $('#deleteButton').attr('attr-id', id);
        $(modal).modal('show');
    },
    remove: function () {
        var url = '/address/delete';
        var id = $('#deleteButton').attr('attr-id');
        $.ajax({
            cache: false,
            url: url,
            data: {ID:id},
            type: 'post',
            success: this.success_process,
            error: this.ajaxFailure
        });
    },
    success_process: function (response) {
        $('#deleteAddress').modal('hide');
        
        window.location.href='/user/address/index?page=1';
    },
    ajaxFailure: function (response) {
        $('#deleteAddress').modal('hide');
    }
}