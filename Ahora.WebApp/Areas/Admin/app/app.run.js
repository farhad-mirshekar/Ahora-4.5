(() => {
    angular
    .module('portal')
    .run(run);

    run.$inject = ['$rootScope', 'toolsService'];
    function run($rootScope, toolsService) {
        toolsService.getPermission();
        var hasuser = toolsService.hasuser();
        $rootScope.signOut = signOut;
        if (!hasuser)
            window.location.href = '/account/login';
        $rootScope.Menus = [
            {
                name: 'dashboard', title: 'داشبورد', icon: 'fa-home', hasShow: () => { return true; }
            }
            ,

            {
                name: 'settings', title: 'مدیریت سامانه', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnusetting') }
                , subMenus: [

                    { route: '#/profile', title: 'پروفایل', hasShow: () => { return toolsService.checkPermission('pgprofile') },icon:'fa-user' },
                    { route: '#/profile/change-password', title: 'تغییر رمز عبور', hasShow: () => { return toolsService.checkPermission('pgchange-password') }, icon: 'fa-low-vision'},
                    { route: '#/role/cartable', title: 'نقش ها', hasShow: () => { return toolsService.checkPermission('pgrole') }, icon: 'fa-group' },
                    { route: '#/command/cartable', title: 'مجوزها', hasShow: () => { return toolsService.checkPermission('pgcommand') }, icon: ' fa-asl-interpreting' },
                    { route: '#/position/cartable', title: 'جایگاه های سازمانی', hasShow: () => { return toolsService.checkPermission('pgposition') }, icon: 'fa-anchor'},
                ]
            },
            {
                name: 'settingsMain', title: 'مدیریت پایه', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnubasic') }
                , subMenus: [

                    { route: '#/faq-group/cartable', title: 'پرسش های متداول', hasShow: () => { return toolsService.checkPermission('pgfaq') }, icon: 'fa-question' },
                    { route: '#/menu/cartable', title: 'مدیریت منو', hasShow: () => { return toolsService.checkPermission('pgmenu') }, icon: 'fa-bars' },
                    { route: '#/general-setting', title: 'تنظیمات سایت', hasShow: () => { return toolsService.checkPermission('pggeneral-setting') }, icon: 'fa-gear' },
                    { route: '#/pages/cartable', title: 'تنظیمات سایت', hasShow: () => { return true; }, icon: 'fa-gear' },
                ]
            },
            {
                name: 'product', title: 'مدیریت محصولات', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnuproduct') }
                , subMenus: [
                    { route: '#/product-cartable', title: 'کارتابل محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-cartable') }, icon:'fa-folder-open-o' },
                    { route: '#/attribute/cartable', title: 'ویژگی های محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-attribute') }, icon: 'fa-diamond' },
                    { route: '#/category/cartable', title: 'دسته بندی محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-category') }, icon: 'fa-archive' },
                    { route: '#/discount/cartable', title: 'کارتابل تخفیفات', hasShow: () => { return toolsService.checkPermission('pgdiscount') }, icon:'fa-dollar'},
                    { route: '#/comment/cartable', title: 'کارتابل نظرات', hasShow: () => { return toolsService.checkPermission('pgproduct-comment') }, icon: 'fa-comments-o' },
                ]
            },
             {
                 name: 'portal', title: 'مدیریت پرتال', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnuportal') }
                , subMenus: [
                    { route: '#/category-portal/cartable', title: 'دسته بندی اخبار', hasShow: () => { return toolsService.checkPermission('pgportal-category') }, icon: 'fa-archive' },
                    { route: '#/article/cartable', title: 'کارتابل مقالات', hasShow: () => { return toolsService.checkPermission('pg-article') }, icon: 'fa-newspaper-o' },
                    { route: '#/news/cartable', title: 'کارتابل اخبار', hasShow: () => { return toolsService.checkPermission('pgnews') }, icon: 'fa-newspaper-o' },
                    { route: '#/slider/cartable', title: 'کارتابل تصاویر کشویی', hasShow: () => { return toolsService.checkPermission('pgsliders')}, icon: 'fa-sliders' },
                    { route: '#/events/cartable', title: 'کارتابل رویداد', hasShow: () => { return toolsService.checkPermission('pgevents') }, icon: 'fa-sliders' },
                    { route: '#/comment-portal/cartable', title: 'کارتابل نظرات', hasShow: () => { return true; }, icon: 'fa-comments-o' },
                ]
             }
        ]

        function signOut() {
            toolsService.signOut();
        }
    }
})();