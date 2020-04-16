(() => {
    angular
        .module('portal')
        .directive('portalUploadVideo', portalUploadVideo);
    portalUploadVideo.$inject = ['attachmentService', '$q','$routeParams'];
    function portalUploadVideo(attachmentService, $q, $routeParams) {
        var directive = {
            restrict: 'E',
            templateUrl: './Areas/Admin/app/directive/upload-video/upload-video.html',
            link: {
                pre: preLink
            },
            scope: {
                main: '=main',
                video: '=video'
            }
        }
        return directive;
        function preLink(scope, element, $event) {
            scope.filePondConfig = {
                allowMultiple: scope.video.allowMultiple,
                allowFileTypeValidation: false,
                acceptedFileTypes: ['.mp4'],
                labelIdle: `<div>فیلم یا ویدیو خود را اینجا رها کنید یا کلیک نمایید</div>`,
                required: true,
                server: {
                    process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
                        const formData = new FormData();
                        formData.append(fieldName, file, file.name);

                        const request = new XMLHttpRequest();
                        request.open('POST', `attachment/upload?type=${scope.video.type}`);

                        request.upload.onprogress = (e) => {
                            progress(e.lengthComputable, e.loaded, e.total);
                        };

                        request.onload = function () {
                            if (request.status >= 200 && request.status < 300) {
                                load(request.responseText);
                                var obj = JSON.parse(request.responseText);
                                scope.video.list.push(obj.Data.FileName);
                            }
                            else {
                                error('خطا در آپلود فایل');
                            }
                        };
                        request.send(formData);
                        return {
                            abort: () => {
                                request.abort();
                                abort();
                            }
                        };
                    }
                    , revert: (uniqueFileId, load, error) => {
                        const request = new XMLHttpRequest();
                        var obj = JSON.parse(uniqueFileId);
                        debugger
                        var videoID = obj.Data.FileName;
                        request.open('POST', `attachment/Remove?FileName=${videoID}&PathType=${scope.video.type}`, true);
                        request.onload = function () {
                            if (request.status >= 200 && request.status < 300) {
                                load(request.responseText);
                            }
                            else {
                                error('خطا در آپلود فایل');
                            }
                        };
                        request.send();
                    }
                }
            }
            scope.removeVideo = removeVideo;
            scope.confirmRemoveVideo = confirmRemoveVideo;

            function init() {
                element.find("#file-pond").value = "";
                return $q.resolve().then(() => {
                    return attachmentService.list({ ParentID: $routeParams.id });
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === 8) // only video add
                                scope.main.listVideoUploaded = [].concat(result);
                        }
                    }
                    else
                        scope.main.listVideoUploaded = [];
                })
            }
            function removeVideo(item) {
                var model = { ID: item.ID, FileName: item.FileName, PathType: scope.video.type };
                scope.deleteBuffer = model;
                element.find(".upload-delete-video").modal("show");
            }
            function confirmRemoveVideo() {
                return $q.resolve().then(() => {
                    return attachmentService.remove(scope.deleteBuffer);
                }).then((result) => {
                    if (result) {
                        scope.deleteBuffer = {};
                        element.find(".upload-delete-video").modal("hide");
                        scope.main.listVideoUploaded = [];
                        return init();
                    }
                })
            }
        }
    }
})();