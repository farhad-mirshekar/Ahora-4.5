(() => {
    angular
        .module('portal')
        .directive('portalUpload', portalUpload);
    portalUpload.$inject = ['uploadService', 'attachmentService', '$q', '$routeParams', 'loadingService', 'toaster'];
    function portalUpload(uploadService, attachmentService, $q, $routeParams, loadingService, toaster) {
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
            switch (scope.obj.type) {
                case '1':
                    scope.path = 'pages';
                    break;
                case '3':
                    scope.path = 'news';
                    break;
                case '4':
                    scope.path = 'article';
                    break;
                case '5':
                    scope.path = 'slider';
                    break;
                case '6':
                    scope.path = 'product';
                    break;
                case '7':
                    scope.path = 'video';
                    break;
                case '8':
                    scope.path = 'events';
                    break;
                case '9':
                    scope.path = 'editor';
                    break;
                case '10':
                    scope.path = 'file';
                    break;
            }
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