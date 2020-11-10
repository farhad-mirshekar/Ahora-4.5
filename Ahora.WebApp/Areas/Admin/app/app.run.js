(() => {
    angular
        .module('portal')
        .run(run);

    run.$inject = ['$rootScope', 'toolsService', 'authenticationService', '$window', 'positionService', 'userService', 'loadingService','$location'];
    function run($rootScope, toolsService, authenticationService, $window, positionService, userService, loadingService, $location) {
        $rootScope.currentUserPosition = authenticationService.get('currentUserPosition');
        $rootScope.currentUserPositions = authenticationService.get('currentUserPositions');
        $rootScope.changePosition = changePosition;
        $rootScope.signOut = signOut;
        $rootScope.checkPermission = checkPermission;

        $rootScope.Menus = [
            {
                name: 'dashboard', title: 'داشبورد', icon: 'fa-home', hasShow: () => { return true; }
            }
            ,

            {
                name: 'settings', title: 'مدیریت سامانه', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnusetting') }
                , subMenus: [

                    { route: '#/profile', title: 'پروفایل', hasShow: () => { return toolsService.checkPermission('pgprofile') }, icon: 'fa-user' },
                    { route: '#/profile/change-password', title: 'تغییر رمز عبور', hasShow: () => { return toolsService.checkPermission('pgchange-password') }, icon: 'fa-low-vision' },
                    { route: '#/role/cartable', title: 'نقش ها', hasShow: () => { return toolsService.checkPermission('pgrole') }, icon: 'fa-group' },
                    { route: '#/command/cartable', title: 'مجوزها', hasShow: () => { return toolsService.checkPermission('pgcommand') }, icon: ' fa-asl-interpreting' },
                    { route: '#/position/cartable', title: 'جایگاه های سازمانی', hasShow: () => { return toolsService.checkPermission('pgposition') }, icon: 'fa-anchor' },
                    { route: '#/department/cartable', title: 'دستگاه ها', hasShow: () => { return toolsService.checkPermission('pgdepartment') }, icon: 'fa-anchor' },
                    { route: '#/user/cartable', title: 'کاربران', hasShow: () => { return toolsService.checkPermission('pgusers') }, icon: 'fa-anchor' },

                ]
            },
            {
                name: 'settingsMain', title: 'مدیریت پایه', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnubasic') }
                , subMenus: [

                    { route: '#/faq-group/cartable', title: 'پرسش های متداول', hasShow: () => { return toolsService.checkPermission('pgfaq') }, icon: 'fa-question' },
                    { route: '#/menu/cartable', title: 'مدیریت منو', hasShow: () => { return toolsService.checkPermission('pgmenu') }, icon: 'fa-bars' },
                    { route: '#/general-setting', title: 'تنظیمات سایت', hasShow: () => { return toolsService.checkPermission('pggeneral-setting') }, icon: 'fa-gear' },
                    { route: '#/pages/cartable', title: 'آدرس صفحات', hasShow: () => { return toolsService.checkPermission('pgaddresspages') }, icon: 'fa-address-book' },
                    { route: '#/contact-us', title: 'ارتباط با ما', hasShow: () => { return toolsService.checkPermission('pgcontactus') }, icon: 'fa-at' },
                    { route: '#/link', title: 'پیوندها', hasShow: () => { return toolsService.checkPermission('pglink') }, icon: 'fa-anchor' },
                    { route: '#/language/cartable', title: 'مدیریت زبان', hasShow: () => { return toolsService.checkPermission('pglanguage') }, icon: 'fa-anchor' },

                ]
            },
            {
                name: 'product', title: 'مدیریت محصولات', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnuproduct') }
                , subMenus: [
                    { route: '#/product-cartable', title: 'کارتابل محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-cartable') }, icon: 'fa-folder-open-o' },
                    { route: '#/shipping-cost/cartable', title: 'ارسال محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-cartable') }, icon: 'fa-folder-open-o' },
                    { route: '#/attribute/cartable', title: 'ویژگی های محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-attribute') }, icon: 'fa-diamond' },
                    { route: '#/category/cartable', title: 'دسته بندی محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-category') }, icon: 'fa-archive' },
                    { route: '#/discount/cartable', title: 'کارتابل تخفیفات', hasShow: () => { return toolsService.checkPermission('pgdiscount') }, icon: 'fa-dollar' },
                    { route: '#/productComment/cartable', title: 'کارتابل نظرات', hasShow: () => { return toolsService.checkPermission('pgproduct-comment') }, icon: 'fa-comments-o' },
                    { route: '#/delivery-date/cartable', title: 'زمان ارسال محصولات', hasShow: () => { return toolsService.checkPermission('pgproduct-cartable') }, icon: 'fa-folder-open-o' },
                ]
            },
            {
                name: 'portal', title: 'مدیریت پرتال', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnuportal') }
                , subMenus: [
                    { route: '#/category-portal/cartable', title: 'دسته بندی اخبار', hasShow: () => { return toolsService.checkPermission('pgportal-category') }, icon: 'fa-archive' },
                    { route: '#/article/cartable', title: 'کارتابل مقالات', hasShow: () => { return toolsService.checkPermission('pg-article') }, icon: 'fa-newspaper-o' },
                    { route: '#/articleComment/cartable', title: ' کارتابل نظرات مقالات', hasShow: () => { return toolsService.checkPermission('pgarticle-comment') }, icon: 'fa-comments-o' },
                    { route: '#/news/cartable', title: 'کارتابل اخبار', hasShow: () => { return toolsService.checkPermission('pgnews') }, icon: 'fa-newspaper-o' },
                    { route: '#/newsComment/cartable', title: ' کارتابل نظرات اخبار', hasShow: () => { return toolsService.checkPermission('pgnews-comment') }, icon: 'fa-comments-o' },
                    { route: '#/events/cartable', title: 'کارتابل رویداد', hasShow: () => { return toolsService.checkPermission('pgevents') }, icon: 'fa-newspaper-o' },
                    { route: '#/eventsComment/cartable', title: ' کارتابل نظرات رویداد', hasShow: () => { return toolsService.checkPermission('pevents-comment') }, icon: 'fa-comments-o' },
                    { route: '#/slider/cartable', title: 'کارتابل تصاویر کشویی', hasShow: () => { return toolsService.checkPermission('pgsliders') }, icon: 'fa-sliders' },
                    { route: '#/comment-portal/cartable', title: 'کارتابل نظرات', hasShow: () => { return toolsService.checkPermission('pgcomment-portal'); }, icon: 'fa-comments-o' },
                    { route: '#/pages-portal/', title: 'ساخت صفحات', hasShow: () => { return toolsService.checkPermission('pgcreate-page'); }, icon: 'fa-window-restore' },
                    { route: '#/dynamic-page/cartable', title: 'کارتابل صفحات داینامیک', hasShow: () => { return toolsService.checkPermission('pgdynamic-page'); }, icon: 'fa-sticky-note-o' },
                    { route: '#/static-page/cartable', title: 'کارتابل صفحات استاتیک', hasShow: () => { return toolsService.checkPermission('pgstatic-page'); }, icon: 'fa-sticky-note' },
                    { route: '#/banner/cartable', title: 'مدیریت بنر', hasShow: () => { return toolsService.checkPermission('pgbanner'); }, icon: 'fa-camera-retro' },
                    { route: '#/gallery/cartable', title: 'مدیریت گالری تصاویر', hasShow: () => { return toolsService.checkPermission('pggallery'); }, icon: 'fa fa-photo' },
                ]
            }
            , {
                name: 'sales', title: 'مدیریت سفارشات', icon: 'fa-angle-down', hasShow: () => { return toolsService.checkPermission('mnusales'); }
                , subMenus: [
                    { route: '#/sales/cartable', title: 'کارتابل سفارشات', hasShow: () => { return toolsService.checkPermission('pgsales-cartable'); }, icon: 'fa fa-credit-card' },
                ]

            }
        ]

        $rootScope.$on('$locationChangeStart', locationChangeStart);
        function locationChangeStart(event, next, current) {
            if (authenticationService.isAuthenticated() && !$rootScope.permissions) {
                if (next.split('/')[next.split('/').length - 1] !== 'init')
                    $rootScope.pathBuffer = next.split('#/')[1] ? decodeURIComponent(next.split('#/')[1]) : '';
                return $location.path('init');
            }


            //if (restrictedPage && !authenticationService.isAuthenticated()) {
            //    if ($location.path() === '/')
            //        $location.path('/');
            //    else {
            //        $location.path('/');
            //        logout();
            //    }
            //}
            //else if (!restrictedPage && authenticationService.isAuthenticated()) {
            //    $location.path('/');
            //    return event.preventDefault();
            //}
        }

        function signOut() {
            toolsService.signOut();
        }
        function changePosition(position) {
            if (position.ID !== authenticationService.get('currentUserPosition').ID) {
                loadingService.show();
                return positionService.setDefault({ PositionID: position.ID }).then(() => {
                    authenticationService.set('currentUserPosition', position);
                    return userService.getRefreshToken({
                        RefreshToken: authenticationService.get('authorizationData').Refresh_Token
                    });
                }).then((result) => {
                    authenticationService.setCredentials(result);
                    $window.location.reload();
                }).catch(loadingService.hide);
            }
        }

        function checkPermission(name) {
            return $rootScope.permissions && $rootScope.permissions.some((e) => { return e.FullName === name; });
        }
    }
})();