(() => {
    angular
        .module('portal')
        .directive('portalUploadFile', portalUploadFile);
    portalUploadFile.$inject = ['attachmentService', '$q','$routeParams'];
    function portalUploadFile(attachmentService, $q, $routeParams) {
        var directive = {
            restrict: 'E',
            templateUrl: './Areas/Admin/app/directive/upload-file/upload-file.html',
            link: {
                pre: preLink
            },
            scope: {
                main: '=main',
                file: '=file'
            }
        }
        return directive;
        function preLink(scope, element, $event) {
            scope.filePondConfig = {
                allowMultiple: scope.file.allowMultiple,
                allowFileTypeValidation: false,
                acceptedFileTypes: ['.zip'],
                labelIdle: `<div>فایل خود را اینجا رها کنید یا کلیک نمایید</div>`,
                required: true,
                server: {
                    process: (fieldName, file, metadata, load, error, progress, abort, transfer, options) => {
                        const formData = new FormData();
                        formData.append(fieldName, file, file.name);

                        const request = new XMLHttpRequest();
                        request.open('POST', `attachment/upload?type=${scope.file.type}`);

                        request.upload.onprogress = (e) => {
                            progress(e.lengthComputable, e.loaded, e.total);
                        };

                        request.onload = function () {
                            if (request.status >= 200 && request.status < 300) {
                                load(request.responseText);
                                var obj = JSON.parse(request.responseText);
                                scope.file.list.push(obj.Data.FileName);
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
                        var fileID = obj.Data.FileName;
                        request.open('POST', `attachment/Remove?FileName=${fileID}&PathType=${scope.file.type}`, true);
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
            scope.removeFile = removeFile;
            scope.confirmRemoveFile = confirmRemoveFile;

            function init() {
                element.find("#file-pond").value = "";
                return $q.resolve().then(() => {
                    return attachmentService.list({ ParentID: $routeParams.id });
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === 10) // only video add
                                scope.main.listFileUploaded = [].concat(result);
                        }
                    }
                    else
                        scope.main.listFileUploaded = [];
                })
            }
            function removeFile(item) {
                var model = { ID: item.ID, FileName: item.FileName, PathType: scope.file.type };
                scope.deleteBuffer = model;
                element.find(".upload-delete-file").modal("show");
            }
            function confirmRemoveFile() {
                return $q.resolve().then(() => {
                    return attachmentService.remove(scope.deleteBuffer);
                }).then((result) => {
                    if (result) {
                        scope.deleteBuffer = {};
                        element.find(".upload-delete-file").modal("hide");
                        scope.main.listFileUploaded = [];
                        return init();
                    }
                })
            }
        }
    }
})();