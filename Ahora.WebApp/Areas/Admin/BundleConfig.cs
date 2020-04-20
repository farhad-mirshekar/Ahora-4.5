using System.Web.Optimization;

namespace Ahora.WebApp.Areas.Admin
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            //add bundles
            bundles.Add(new StyleBundle("~/bundles/bootstrap-rtl").Include(
                                        "~/Areas/Admin/Content/css/bootstrap-rtl/bootstrap-rtl.css"));
            bundles.Add(new StyleBundle("~/bundles/admin").Include(
                                        "~/Areas/Admin/Content/css/admin/admin.css"));

            bundles.Add(new StyleBundle("~/bundles/mainAdmin").Include(
                                        "~/Areas/Admin/Content/css/admin/treeGrid.css",
                                        "~/Content/fontawesome/font-awesome.css",
                                        "~/Content/toaster/toaster.css",
                                        "~/Areas/Admin/Content/fileponed/css/filepond.css",
                                        "~/Areas/Admin/Content/fileponed/css/filepond-plugin-image-preview.css",
                                        "~/Areas/Admin/Content/fileponed/css/filepond-plugin-image-edit.css",
                                        "~/Areas/Admin/Content/css/select2/select2.min.css",
                                        "~/Areas/Admin/Content/css/loader/loader.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-rtl-js").Include(
                                        "~/Areas/Admin/Content/js/popper.min.js",
                                        "~/Areas/Admin/Content/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                                        "~/Scripts/angular/angular.js",
                                        "~/Scripts/angular/angular-route.js",
                                        "~/Scripts/angular/angular-sanitize.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/config").Include(
                "~/Areas/Admin/app/app.module.js",
                //"~/Areas/Admin/app/app.config.js",
                "~/Areas/Admin/app/app.run.js"));
           bundles.Add(new ScriptBundle("~/bundles/angularadmin").Include(
                                         "~/Areas/Admin/app/controller/panel.controller.js",
                                         "~/Areas/Admin/app/service/panel.service.js",
                                         "~/Areas/Admin/app/service/loading.service.js",
                                         "~/Areas/Admin/app/service/tools.service.js",
                                         "~/Areas/Admin/app/service/authenticationService.js"
                                         ));

            bundles.Add(new ScriptBundle("~/bundles/froalajs").Include(
                "~/Scripts/froala-editor/js/froala-angular.js",
                "~/Scripts/froala-editor/js/froala_editor.min.js",
                "~/Scripts/froala-editor/js/languages/fa.js",
                "~/Scripts/froala-editor/js/plugins/align.min.js",
                "~/Scripts/froala-editor/js/plugins/font_family.min.js",
                "~/Scripts/froala-editor/js/plugins/font_size.min.js",
                "~/Scripts/froala-editor/js/plugins/inline_style.min.js",
                "~/Scripts/froala-editor/js/plugins/image.min.js",
                "~/Scripts/froala-editor/js/plugins/link.min.js",
                "~/Scripts/froala-editor/js/plugins/video.min.js"));

            bundles.Add(new StyleBundle("~/bundles/froalacss").Include(
                "~/Scripts/froala-editor/css/froala_editor.css",
                "~/Scripts/froala-editor/css/plugins/image.css",
                "~/Scripts/froala-editor/css/plugins/video.css",
                "~/Scripts/froala-editor/css/plugins/fullscreen.css",
                "~/Scripts/froala-editor/css/plugins/color.css",
                "~/Scripts/froala-editor/css/froala_style.css"));

            bundles.Add(new ScriptBundle("~/bundles/utility").Include(
                                        "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                                        "~/Scripts/ckeditor/angular-ckeditor.js",
                                        "~/Scripts/ckeditor/ckeditor.js",
                                        "~/Areas/Admin/app/directive/upload/upload.js",
                                        "~/Areas/Admin/app/directive/upload-video/upload-video.js",
                                        "~/Areas/Admin/app/directive/upload-file/upload-file.js",
                                        "~/Scripts/toaster/toaster.min.js",
                                        "~/Scripts/custom/UnderScore.js",
                                        "~/Scripts/smart-table.min.js",
                                        "~/Areas/Admin/app/directive/checkbox/checkbox.js",
                                        "~/Areas/Admin/app/directive/treegrid/tree-grid-directive.js",
                                        "~/Areas/Admin/Content/fileponed/js/filepond-plugin-image-preview.js",
                                        "~/Areas/Admin/Content/fileponed/js/filepond-plugin-image-edit.js",
                                        "~/Areas/Admin/Content/fileponed/js/filepond-plugin-file-validate-type.js",
                                        "~/Areas/Admin/Content/fileponed/js/filepond.min.js",
                                        "~/Areas/Admin/app/directive/grid/grid.js",
                                        "~/Areas/Admin/Content/js/select2.min.js"));
        }


    }
}