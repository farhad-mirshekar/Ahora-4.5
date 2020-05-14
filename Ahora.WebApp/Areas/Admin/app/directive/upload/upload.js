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
                main: '=main',
                pic: '=pic'
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
            scope.main.reset = reset;
            scope.fileSelected = false;
            scope.uploading = false;
            scope.validTypes = scope.pic.validTypes || [
                "application/vnd.ms-excel",
                "application/msexcel",
                "application/x-msexcel",
                "application/x-ms-excel",
                "application/x-excel",
                "application/x-dos_ms_excel",
                "application/xls",
                "application/x-xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/pdf",
                "application/zip",
                "application/x-zip-compressed",
                "application/x-7z-compressed",
                "application/x-rar-compressed",
                "image/jpeg",
                "image/x-citrix-jpeg",
                "image/png",
                "image/x-citrix-png",
                "image/x-png",
                "image/tiff",
                "image/gif",
                "image/bmp",
                "image/svg+xml"
            ];
            scope.path = '';
            switch (scope.pic.type) {
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
                scope.tempListFile = [];
            }
            function removeTemp(item) {
                loadingService.show();
                return $q.resolve().then(() => {
                    var index = scope.tempListFile.indexOf(item);
                    scope.tempListFile.splice(index, 1);
                    scope.pic.list.splice(index, 1);
                    return attachmentService.remove({ FileName: item.FileName, PathType: scope.pic.type });
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
                    if (scope.pic.validTypes.length && scope.pic.validTypes.indexOf(file.type) === -1)
                        return toaster.pop('error', '', "قالب فایل انتخاب شده مجاز نیست");
                    if (window.FormData !== undefined) {
                        const formData = new FormData();
                        formData.append(file.name, file, file.name);
                        scope.uploading = true; // rename to state // **state
                        scope.fileSelected = false;
                        return uploadService.upload({ type: scope.pic.type, data: formData });
                    }
                }).then((result) => {
                    scope.tempListFile.push(result);
                    scope.pic.list.push(result.FileName);
                })
                    .finally(loadingService.hide);
            }
        }
    }
})();