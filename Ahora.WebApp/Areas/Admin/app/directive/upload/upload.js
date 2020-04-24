(() => {
    angular
        .module('portal')
        .directive('portalUpload', portalUpload);
    portalUpload.$inject = ['uploadService', 'attachmentService', '$q','$routeParams'];
    function portalUpload(uploadService, attachmentService, $q, $routeParams) {
        var directive = {
            restrict: 'E',
            templateUrl: './Areas/Admin/app/directive/upload/upload.html',
            link: {
                pre: preLink
            },
            scope: {
                main: '=main',
                pic: '=pic'
            }
        }
        return directive;
        function preLink(scope, element, $event) {
            scope.filePondConfig = {
                allowMultiple: scope.pic.allowMultiple,
                labelIdle: `<div>عکس خود را اینجا رها کنید یا کلیک نمایید</div>`,
                required: true,
                server: {
                    process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
                        const formData = new FormData();
                        formData.append(fieldName, file, file.name);

                        const request = new XMLHttpRequest();
                        request.open('POST', `attachment/upload?type=${scope.pic.type}`);

                        request.upload.onprogress = (e) => {
                            progress(e.lengthComputable, e.loaded, e.total);
                        };

                        request.onload = function () {
                            if (request.status >= 200 && request.status < 300) {
                                load(request.responseText);
                                var obj = JSON.parse(request.responseText);
                                scope.pic.list.push(obj.Data.FileName);
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
                        var PicID = obj.Data.FileName;
                        request.open('POST', `attachment/Remove?FileName=${PicID}&PathType=${scope.pic.type}`, true);
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
            scope.remove = remove;
            scope.confirmRemove = confirmRemove;
            scope.main.reset = reset;

            function init() {
                element.find("#file-pond").value = "";
                return $q.resolve().then(() => {
                    return attachmentService.list({ ParentID: $routeParams.id});
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType !== 7) // only pic add
                                scope.main.listPicUploaded = [].concat(result);
                        }
                    }
                    else
                        scope.main.listPicUploaded = [];
                })
            }
            function remove(item) {
                var model = { ID: item.ID, FileName: item.FileName, PathType: scope.pic.type };
                scope.deleteBuffer = model;
                element.find(".upload-delete").modal("show");
            }
            function confirmRemove() {
                return $q.resolve().then(() => {
                    return attachmentService.remove(scope.deleteBuffer);
                }).then((result) => {
                    if (result) {
                        scope.deleteBuffer = {};
                        element.find(".upload-delete").modal("hide");
                        scope.main.listPicUploaded = [];
                        return init();
                    }
                })
            }
            function reset() {
                element.find("#file-pond").value = "";
            }
        }
    }
})();