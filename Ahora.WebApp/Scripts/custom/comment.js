var Messsage = new Object();
Messsage.success = '<div id="alert" class="alert alert-success"><button type="button" class="close" data-dismiss="alert">&times;</button> رای شما با موفقیت ثبت شد </div>';
Messsage.login = '<div id="alert" class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">&times;</button> برای رای دادن باید عضو سایت باشید </div>';
Messsage.duplicate = '<div id="alert" class="alert alert-info"><button type="button" class="close" data-dismiss="alert">×</button>قبلا رای داده اید!</div>';
Messsage.duplicate = '<div id="alert" class="alert alert-info"><button type="button" class="close" data-dismiss="alert">×</button>قبلا رای داده اید!</div>';
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
var AddComment = new Object();

AddComment.onSuccess = function (data) {
    if (data.show == "false") {
        var message = '<div id="alert" class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button>دیدگاه شما ثبت شد و پس از تایید،نمایش داده خواهد شد</div>';
        $('#addCommentResult').html(message);
        $('#frmCommentReply').html('');
    }
    else {
        $('#commentReply').html(data);
        AjaxForm.ValidateForm('frmCommentReply');
    }

};

AddComment.slideToggle = function () {
    $('.comment-reply-container').slideUp().html('');
    $('#addComment').slideDown('slow');
    $('#loadingMessage').fadeOut('slow');

};
///////////////////////////////////////////////////////////////////////
var CommentLike = new Object();
$('div.comment-like-link').children('span').on('click', function (event) {
    event.preventDefault();
});

$('div.card-footer >div.pull-right> div.comment-like-link').on('click', function () {
    var $this = $(this).children('span:nth-child(2)');
    var $loading = $('div#commentLikeLoading' + $this.attr('data-comment-id')).fadeIn('slow');
    $.ajax({
        type: "POST",
        url: $this.attr('data-href'),
        success: function (data) {
            CommentLike.onSuccess(data);
        }
    });
    $loading.fadeOut('fast');
})

CommentLike.onSuccess = function (data) {
    if (data.result === "login") {

        $('#comment-like-result-' + data.CommentID).html(Messsage.login).fadeIn('slow');
    }
    else if (data.result === "success" && data.state === "like") {

        $('#comment-like-count-' + data.CommentID).text(data.count);
        $('#comment-like-result-' + data.CommentID).html(Messsage.success).fadeIn('slow');
    }
    else if (data.result === "success" && data.state === "dislike") {

        $('#comment-dislike-count-' + data.CommentID).text(data.count);
        $('#comment-like-result-' + data.CommentID).html(Messsage.success).fadeIn('slow');
    }

    else if (data.result === "duplicate") {
        $('#comment-like-result-' + data.CommentID).html(Messsage.duplicate).fadeIn('slow');

    }
};
////////////////////////////////////////////////////////////////////////////////////////////////
var CommentReply = new Object();

CommentReply.slideDown = function (id, data, third) {

    $('#comment-reply-' + id).slideToggle('slow');
    validateForm('frmCommentReply');
};
CommentReply.onBegin = function () {
    $('.comment-reply-container').slideUp().html('');
    $('.comment-reply-container').css('display', '');
    $('#addComment').slideUp().html('');
};
////////////////////////////////////////////////////////////////////////////////////////////
var AjaxForm = new Object();
AjaxForm.EnableAjaxFormvalidate = function (formId) {
    $.validator.unobtrusive.parse('#' + formId);
};

AjaxForm.ValidateForm = function (formId) {
    var val = $('#' + formId).validate();
    val.form();
    return val.valid();
};
AjaxForm.EnablePostbackValidation = function () {
    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
};

AjaxForm.EnableBootstrapStyleValidation = function () {
    $.validator.setDefaults({
        highlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).addClass(errorClass).removeClass(validClass);
            } else {
                $(element).addClass(errorClass).removeClass(validClass);
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');

            }
            $(element).trigger('highlited');
        },
        unhighlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).removeClass(errorClass).addClass(validClass);
            } else {
                $(element).removeClass(errorClass).addClass(validClass);
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            }
            $(element).trigger('unhighlited');
        }
    });
};
