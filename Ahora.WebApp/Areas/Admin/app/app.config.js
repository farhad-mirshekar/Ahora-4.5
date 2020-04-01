(function () {
    'use strict';

    var app = angular.module('portal');

    app.config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix('');
            $locationProvider.html5Mode(false);
            $routeProvider.when("/command/:state/:id?", { templateUrl: "/admin/command/index", controller: 'commandController', reloadOnUrl: false })
                .when("/role/:state/:id?", { templateUrl: "/admin/role/index", controller: 'roleController' })
                .when("/position/:state/:id?", { templateUrl: "/admin/position/index", controller: 'positionController' })
                .when("/product/:state/:id?", {
                    templateUrl: "/admin/product/index", controller: 'productController'
                })
                .when("/product-cartable", { templateUrl: "/admin/product/cartable", controller: 'ProductCartableController' })
                .when("/discount/:state/:id?", {
                    templateUrl: "/admin/discount/index", controller: 'discountController'
                })
                .when("/attribute/:state/:id?", {
                    templateUrl: "/admin/productAttribute/index", controller: 'attributeController'
                })
                .when("/faq-group/:state/:id?", {
                    templateUrl: "/admin/faqgroup/index", controller: 'faqGroupController', resolve: {
                        data: ['faqGroupService', '$route', function (faqGroupService, $route) {
                            return faqGroupService.list().then(function success(data) { return data; },
                                function error(reason) { return false; });
                        }]
                    }
                })
                .when("/section/:state/:id?", {
                    templateUrl: '/admin/section/index', controller: 'sectionController', resolve: {
                        country: ['sectionService', function (sectionService) {
                            return sectionService.country().then((result) => { return result; })
                        }],
                        province: ['sectionService', function (sectionService) {
                            return sectionService.province().then((result) => { return result; })
                        }],
                        list: ['sectionService', function (sectionService) {
                            return sectionService.List().then((result) => { return result; })
                        }],
                    }
                })
                .when("/profile", {
                    templateUrl: '/admin/profile/index', controller: 'profileController'
                })
                .when("/profile/change-password", {
                    templateUrl: '/admin/profile/ChangePassword', controller: 'changePasswordController'
                })
                .when("/category/:state/:id?", { templateUrl: "/admin/category/index", controller: 'categoryController' })
                .when("/", {
                    templateUrl: '/admin/home/main', controller: 'homeController'
                })
                .when("/comment/:state/:id?", {
                    templateUrl: '/admin/comment/index', controller: 'commentController',
                })
                .when("/category-portal/:state/:id?", { templateUrl: "/Admin/CategoryPortal/index", controller: 'categoryPortalController' })
                .when("/article/:state/:id?", { templateUrl: "/Admin/article/index", controller: 'articleController', reloadOnUrl: false })
                .when("/news/:state/:id?", { templateUrl: "/Admin/news/index", controller: 'newsController' })
                .when("/pages/:state/:id?", { templateUrl: "/Admin/pages/index", controller: 'pagesController', reloadOnUrl: false })
                .when("/menu/:state/:id?", { templateUrl: "/Admin/menu/index", controller: 'menuController', reloadOnUrl: false })
                .when("/slider/:state/:id?", { templateUrl: "/Admin/slider/index", controller: 'sliderController', reloadOnUrl: false })
                .when("/general-setting", { templateUrl: "/Admin/generalSetting/index", controller: 'generalSettingController' })
                .when("/events/:state/:id?", { templateUrl: "/Admin/events/index", controller: 'eventsController' })
                .when("/comment-portal/:state/:id?", { templateUrl: "/Admin/comment/listForportal", controller: 'commentPortalController', reloadOnUrl: false })// add;

                .otherwise({
                    templateUrl: './areas/admin/app/NotFound/not-found.html'
                });

        }
    ]);



})();