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
                obj:'=obj'
            }
        }
        return directive;
        function preLink(scope, element, $event) {
            scope.removeVideo = removeVideo;
            scope.confirmRemoveVideo = confirmRemoveVideo;
            scope.obj.reset = reset;

            function init() {
                return $q.resolve().then(() => {
                    return attachmentService.list({ ParentID: $routeParams.id });
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === 7) // only video add
                                scope.obj.listUploaded = [].concat(result);
                        }
                    }
                    else
                        scope.obj.listUploaded = [];
                })
            }
            function removeVideo(item) {
                var model = { ID: item.ID, FileName: item.FileName, PathType: scope.obj.type };
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
                        scope.obj.listUploaded = [];
                        return init();
                    }
                })
            }
            function reset() {
                scope.obj.list = [];
                scope.obj.listUploaded = [];
            }
        }
    }
})();