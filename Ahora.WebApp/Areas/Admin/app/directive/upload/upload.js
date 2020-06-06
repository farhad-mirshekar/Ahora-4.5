(() => {
    angular
        .module('portal')
        .directive('portalUpload', portalUpload);
    portalUpload.$inject = ['uploadService', 'attachmentService', '$q', '$routeParams', 'loadingService', 'toaster','enumService'];
    function portalUpload(uploadService, attachmentService, $q, $routeParams, loadingService, toaster, enumService) {
        var directive = {
            restrict: 'E',
            templateUrl: './Areas/Admin/app/directive/upload/upload.html',
            scope: {
                obj: '=obj'
            }
            , link: link
        }
        return directive;
        function link(scope, element, $event) {
            let file;
            scope.tempListFile = [];
            scope.selectFile = selectFile;
            scope.remove = remove;
            scope.removeTemp = removeTemp;
            scope.browse = browse;
            scope.upload = upload;
            scope.confirmRemove = confirmRemove;
            scope.obj.reset = reset;
            scope.fileSelected = false;
            scope.uploading = false;
            scope.validTypes = scope.obj.validTypes || [
                "application/pdf",
                "application/zip",
                "application/x-zip-compressed",
                "application/x-7z-compressed",
                "application/x-rar-compressed",
                "image/jpeg",
                "image/png"
            ];
            scope.path = '';
            scope.path = enumService.PathType[parseInt(scope.obj.type)];

            function selectFile() {
                file = element.find("input[type='file']").get(0).files[0];
                scope.fileSelected = true; //**state
                scope.uploading = false;
                scope.$apply();
            }
            function init() {
                return $q.resolve().then(() => {
                    return attachmentService.list({ ParentID: $routeParams.id });
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === parseInt(scope.obj.type)) // only obj add
                                scope.obj.listUploaded = [].concat(result);
                        }
                    }
                    else
                        scope.obj.listUploaded = [];
                })
            }
            function remove(item) {
                var model = { ID: item.ID, FileName: item.FileName, PathType: scope.obj.type };
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
                        scope.obj.listUploaded = [];
                        return init();
                    }
                })
            }
            function reset() {
                scope.tempListFile = [];
                scope.obj.list = [];
                scope.obj.listUploaded = [];
            }
            function removeTemp(item) {
                loadingService.show();
                return $q.resolve().then(() => {
                    var index = scope.tempListFile.indexOf(item);
                    scope.tempListFile.splice(index, 1);
                    scope.obj.list.splice(index, 1);
                    return attachmentService.remove({ FileName: item.FileName, PathType: scope.obj.type });
                }).catch(() => {
                    loadingService.hide();
                }).finally(loadingService.hide);
            }
            function browse() {
                element.find("input[type='file']").trigger("click");
            }
            function upload() {
                loadingService.show();
                return $q.resolve().then(() => {
                    if (scope.obj.validTypes.length && scope.obj.validTypes.indexOf(file.type) === -1) {
                        toaster.pop('error', '', 'قالب فایل انتخاب شده مجاز نیست');
                        return $q.reject();
                    }
                    else {

                        if (window.FormData !== undefined) {
                            const formData = new FormData();
                            formData.append(file.name, file, file.name);
                            scope.uploading = true; // rename to state // **state
                            scope.fileSelected = false;
                            return uploadService.upload({ type: scope.obj.type, data: formData });
                        }
                    }
                }).then((result) => {
                    scope.tempListFile.push(result);
                    scope.obj.list.push(result.FileName);
                })
                    .finally(loadingService.hide);
            }
        }
    }
})();