var Messsage = new Object();
Messsage.success = '<div id="alert" class="alert alert-success"><button type="button" class="close" data-dismiss="alert">&times;</button> رای شما با موفقیت ثبت شد </div>';
Messsage.login = '<div id="alert" class="alert alert-danger"><button type="button" class="close" data-dismiss="alert">&times;</button> برای رای دادن باید عضو سایت باشید </div>';
Messsage.duplicate = '<div id="alert" class="alert alert-info"><button type="button" class="close" data-dismiss="alert">×</button>قبلا رای داده اید!</div>';
////////////////////////////////////////////////////////////////////////////
(function ($) {
    $.fn.InfiniteScroll = function (options) {
        var defaults = {
            moreInfoDiv: '#MoreInfoDiv',
            progressDiv: '#Progress',
            loadInfoUrl: '/',
            loginUrl: '/login',
            errorHandler: null,
            completeHandler: null,
            noMoreInfoHandler: null
        };
        options = $.extend(defaults, options);

        var showProgress = function () {
            $("#Progress").fadeIn('slow');
        };

        var hideProgress = function () {
            $("#Progress").fadeOut('fast');

        };

        var showbutton = function () {
            $('#moreInfoButton').show();
        }

        return this.each(function () {
            var moreInfoButton = $(this);
            var page = 1;
            $(moreInfoButton).click(function (event) {
                event.preventDefault();

                showProgress();
                $('#moreInfoButton').hide();
                $.ajax({
                    type: "POST",
                    url: options.loadInfoUrl,
                    data: JSON.stringify({ page: page }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    complete: function (xhr, status) {
                        hideProgress();
                        window.setTimeout(showbutton, 800);
                        var data = xhr.responseText;

                        if (xhr.status == 403) {
                            window.location = options.loginUrl;
                        }
                        else if (status === 'error' || !data) {
                            if (options.errorHandler) {
                                hideProgress();

                                options.errorHandler(this);
                            }
                        }
                        else {
                            if (data === 'no-more') {
                                if (options.noMoreInfoHandler) {
                                    hideProgress();

                                    options.noMoreInfoHandler(this);
                                }
                            }
                            else {
                                hideProgress();

                                var $boxes = $(data);
                                $(options.moreInfoDiv).append($boxes);
                                console.log(++page);
                            }

                        }
                        hideProgress();

                        if (options.completeHandler)
                            options.completeHandler(this);
                    }
                });
            });
        });
    };
})(jQuery);

$("#moreInfoButton").InfiniteScroll({
    moreInfoDiv: '#MoreInfoDiv',
    progressDiv: '#loadingMessage',
    loadInfoUrl: $("#moreInfoButton").attr('href'),
    errorHandler: function () {
        var noty = window.noty({ text: "خطایی رخ داده است", type: 'error', timeout: 2500 });
    },
    noMoreInfoHandler: function () {

        var noty = window.noty({ text: "مطلب بیشتری در دسترس نیست", type: 'information', timeout: 2500 });
    }
});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
var AddComment = new Object();

AddComment.onSuccess = function (data) {
    if (data.show == "false") {
        var message = '<div id="alert" class="alert alert-success"><button type="button" class="close" data-dismiss="alert">×</button>دیدگاه شما ثبت شد و پس از تایید،نمایش داده خواهد شد</div>';
        $('#addCommentResult').html(message);
        $('#frmCommentReply').html('');
    } else if (data.show == "true") {
        $('#addCommentResult').html(Messsage.login);
    }
    else {
        $('#commentReply').html(data);
        validateForm('frmCommentReply');
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

$('div.comment-like-link').on('click', function () {
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
