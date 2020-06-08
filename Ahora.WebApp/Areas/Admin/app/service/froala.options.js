(() => {
    angular
        .module('portal')
        .factory('froalaOption', froalaOption);
    froalaOption.$inject = ['loadingService','attachmentService','$q'];
    function froalaOption(loadingService,attachmentService,$q) {
        var option = {};
        option.main = {
            toolbarButtons: {
                'moreText': {
                    'buttons': ['bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', 'fontFamily', 'fontSize', 'textColor', 'backgroundColor', 'inlineClass', 'inlineStyle', 'clearFormatting']
                },
                'moreParagraph': {
                    'buttons': ['alignLeft', 'alignCenter', 'formatOLSimple', 'alignRight', 'alignJustify', 'formatOL', 'formatUL', 'paragraphFormat', 'paragraphStyle', 'lineHeight', 'outdent', 'indent', 'quote']
                },
                'moreRich': {
                    'buttons': ['insertLink', 'insertImage', 'insertVideo', 'insertTable', 'emoticons', 'fontAwesome', 'specialCharacters', 'embedly', 'insertFile', 'insertHR']
                },
                'moreMisc': {
                    'buttons': ['undo', 'redo', 'fullscreen', 'print', 'getPDF', 'spellChecker', 'selectAll', 'html', 'help'],
                    'align': 'right',
                    'buttonsVisible': 2
                }
            },
            language: 'fa',
            quickInsertEnabled: false,
            charCounterCount: false,
            toolbarSticky: false,
            angularIgnoreAttrs: ['class', 'ng-model', 'id'],
            imageUploadURL: '/attachment/upload?type=9',
            // Set the video upload URL.
            videoUploadURL: '/attachment/upload?type=7',
            // Set request type.
            videoUploadMethod: 'POST',
            events: {
                'video.beforeUpload': function (videos) {
                    // Return false if you want to stop the video upload.
                },
                'video.uploaded': function (response) {

                    // Video was uploaded to the server.
                },
                'image.removed': function ($img) {
                    var fileName = $img.attr('src').split('/');
                    loadingService.show();
                    return $q.resolve().then(() => {
                        return attachmentService.remove({ FileName: fileName[4], PathType: 9 });
                    }).then((result) => {
                        if (result) {
                            loadingService.hide();
                        }
                    }).catch(() => {
                        loadingService.hide();
                    }).finally(loadingService.hide);
                },
                'video.removed': function ($video) {
                    var fileName = $video.attr('src').split('/');
                    loadingService.show();
                    return $q.resolve().then(() => {
                        return attachmentService.remove({ FileName: fileName[4], PathType: 7 });
                    }).then((result) => {
                        if (result) {
                            loadingService.hide();
                        }
                    }).catch(() => {
                        loadingService.hide();
                    }).finally(loadingService.hide);
                }
            }
        };
        option.comment = {
            toolbarButtons: {
                'moreText': {
                    'buttons': ['bold', 'italic', 'underline', 'fontFamily', 'fontSize', 'textColor']
                },
                'moreParagraph': {
                    'buttons': ['alignLeft', 'alignCenter', 'alignRight', 'alignJustify']
                }
            },
            language: 'fa'

        };

        return option;
    }
})();