
(() => {
    var app = angular.module('portal');

    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', '$q', 'loadingService', 'notificationService'];
    function homeController($scope, $q, loadingService, notificationService) {
        let home = $scope;
        home.notification = {};
        home.readNotification = readNotification;
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                return notificationService.getActiveNotification();
            }).then((result) => {
                if (result) {
                    home.notification = [].concat(result);
                }
            }).finally(loadingService.hide);
        }

        function readNotification(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return notificationService.readNotification(model);
            }).finally(loadingService.hide);
        }
    }
    //-------------------------------------------------------------------------------------------------------
    app.controller('faqGroupController', faqGroupController);
    faqGroupController.$inject = ['$scope', '$routeParams', '$location', 'toaster', 'loadingService', 'faqGroupService', 'faqService', '$q', '$timeout'];
    function faqGroupController($scope, $routeParams, $location, toaster, loadingService, faqGroupService, faqService, $q, $timeout) {
        let faqgroup = $scope;
        faqgroup.state = '';
        faqgroup.Model = {};
        faqgroup.faq = {};
        faqgroup.faq.Model = {};
        faqgroup.faq.state = '';
        faqgroup.main = {};
        faqgroup.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        };
        faqgroup.addFaqGroup = addFaqGroup;
        faqgroup.editFaqGroup = editFaqGroup;
        faqgroup.select = select;
        faqgroup.openModalFaq = openModalFaq;
        faqgroup.addFaq = addFaq;
        faqgroup.editFaq = editFaq;
        faqgroup.grid = {
            bindingObject: faqgroup
            , columns: [{ name: 'Title', displayName: 'دسته بندی سوال' }]
            , listService: faqGroupService.list
            , onEdit: faqgroup.main.changeState.edit
            , globalSearch: true
            , initLoad: true
        };
        init();
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        faqgroup.main.changeState.cartable();
                        break;
                    case 'add':
                        faqgroup.main.changeState.add();
                        break;
                    case 'edit':
                        faqgroup.state = 'edit';
                        faqGroupService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            clearModel();
            faqgroup.state = 'cartable';
            $location.path('faq-group/cartable');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            faqgroup.faq = {};
            return $q.resolve().then(() => {
                return faqService.list(model.ID);
            }).then((result) => {
                faqgroup.faq = result;
                faqgroup.state = 'edit';
                faqgroup.Model = model;
                $location.path(`faq-group/edit/${faqgroup.Model.ID}`);
            }).finally(loadingService.hide);
        }
        function add() {
            loadingService.show();
            faqgroup.state = 'add';
            $location.path('/faq-group/add');
            loadingService.hide();
        }

        function addFaqGroup() {
            loadingService.show();
            return faqGroupService.add(faqgroup.Model).then((result) => {
                faqgroup.grid.getlist(false);
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                faqgroup.Model = result;
                faqgroup.main.changeState.edit(faqgroup.Model);
            }).finally(loadingService.hide);
        }
        function editFaqGroup() {
            loadingService.show();
            return $q.resolve().then(() => {
                return faqGroupService.edit(faqgroup.Model);
            }).then((result) => {
                faqgroup.grid.getlist(false);
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                faqgroup.Model = result;
                $timeout(function () {
                    cartable();
                }, 1000);
            }).finally(loadingService.hide);
        }
        function openModalFaq() {
            faqgroup.faq.Model = {};
            faqgroup.faq.state = 'add';
            $(".grid-faq").modal("show");
        }
        function select(model) {
            faqgroup.faq.state = 'edit';
            faqgroup.faq.Model = angular.copy(model);
            $(".grid-faq").modal("show");
        }

        function addFaq() {
            loadingService.show();
            return $q.resolve().then(() => {
                return faqService.add(faqgroup.faq.Model);
            }).then(() => {
                toaster.pop('success', '', 'سوال با موفقیت ثبت گردید');
                $timeout(function () {
                    $('.grid-faq').modal('hide');
                }, 100);

            }).finally(loadingService.hide);
        }
        function editFaq() {
            loadingService.show();
            return $q.resolve().then(() => {
                return faqService.edit(faqgroup.faq.Model);
            }).then(() => {
                return faqGroupService.get(faqgroup.Model.ID);
            }).then((result) => {
                return edit(result);
            }).then(() => {
                toaster.pop('success', '', 'سوال با موفقیت ویرایش گردید');
                $timeout(function () {
                    $('.grid-faq').modal('hide');
                }, 100);
            }).finally(loadingService.hide);
        }
        function clearModel() {
            faqgroup.Model = {};
            faqgroup.faq.Model = {};
            faqgroup.faq.state = '';
        }
    }
    //-------------------------------------------------------------------------------------------------------
    app.controller('profileController', profileController);
    profileController.$inject = ['$scope', 'profileService', 'authenticationService'];
    function profileController($scope, profileService, authenticationService) {
        let profile = $scope;

        init();

        function init() {
            return profileService.get(authenticationService.get('authorizationData').UserID).then((result) => {
                profile.user = result;
            })
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------
    app.controller('commandController', commandController);
    commandController.$inject = ['$scope', '$q', 'commandService', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout', 'toolsService', 'enumService'];
    function commandController($scope, $q, commandService, loadingService, $routeParams, $location, toaster, $timeout, toolsService, enumService) {
        let command = $scope;
        command.Model = {};
        command.list = [];
        command.lists = [];
        command.state = '';

        command.addCommand = addCommand;
        command.addSubCommand = addSubCommand;
        command.editCommand = editCommand;
        command.commandType = toolsService.arrayEnum(enumService.CommandsType);
        command.changeState = {
            cartable: cartable,
            add: add
        }
        init();
        command.tree = {
            data: []
            , colDefs: [
                { field: 'Name', displayName: 'عنوان انگلیسی' }
                , { field: 'Title', displayName: 'نام مجوز' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div class='pull-left'>
                            <i class='fa fa-plus tgrid-action pl-1 text-success' style='cursor:pointer;' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action pl-1 text-danger' style='cursor:pointer;' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: edit,
                        add: addSubCommand,
                        remove: remove
                    }
                }
            ]
            , expandingProperty: {
                field: "Title"
                , displayName: "عنوان"
            }
        };
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                    case 'edit':
                        commandService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            commandService.list().then((result) => {
                setTreeObject(result);
            });
            $location.path('command/cartable');
        }
        function edit(parent) {
            loadingService.show();
            return $q.resolve().then(() => {
                return commandService.get(parent.ID);
            }).then((result) => {
                command.Model = result;
                return commandService.listByNode({ Node: result.ParentNode });
            }).then((result) => {
                command.Model.ParentID = result[0].ID;
                command.state = 'edit';
                $('#command-modal').modal('show');
            }).finally(loadingService.hide)
        }
        function add(parent) {
            loadingService.show();
            parent = parent || {};
            command.Model = { ParentID: parent.ID };
            command.state = 'add';
            $('#command-modal').modal('show');
            loadingService.hide();
        }

        function addCommand() {
            command.Model.Name = command.Model.FullName;
            loadingService.show();
            return $q.resolve().then(() => {
                commandService.add(command.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    $('#command-modal').modal('hide');
                    command.changeState.cartable();
                    loadingService.hide();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editCommand() {
            command.Model.Name = command.Model.FullName;
            loadingService.show();
            return $q.resolve().then(() => {
                commandService.edit(command.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $('#command-modal').modal('hide');
                    command.changeState.cartable();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function addSubCommand(parent) {
            command.changeState.add(parent);
        }
        function setTreeObject(commands) {
            commands.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            command.tree.data = toolsService.getTreeObject(commands, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return commandService.remove(model.ID);
            }).then(() => {
                return commandService.list();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------
    app.controller('roleController', roleController);
    roleController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'roleService', 'toaster', 'commandService', '$location', 'toolsService'];
    function roleController($scope, $q, loadingService, $routeParams, roleService, toaster, commandService, $location, toolsService) {
        let role = $scope;
        role.Model = {};
        role.main = {};
        role.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        };
        role.Model.Permissions = [];
        role.ListCommand = [];
        role.addRole = addRole;
        role.editRole = editRole;
        role.back = back;
        role.grid = {
            bindingObject: role
            , columns: [{ name: 'Name', displayName: 'نام نقش' }]
            , listService: roleService.list
            , onEdit: role.main.changeState.edit
            , globalSearch: true
            , initLoad: true
        };
        init();

        function init() {
            role.state = 'cartable';
            $q.resolve().then(() => {
                loadingService.show();
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        roleService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                    case 'add':
                        add();
                        break;
                }
            }).finally(loadingService.hide);

        } // end init

        function cartable() {
            role.Model = {};
            role.state = 'cartable';
            $location.path('role/cartable');
        }
        function edit(model) {
            loadingService.show;
            return $q.resolve().then(() => {
                role.Model = model;
                return commandService.list();
            }).then((result) => {
                if (result.length > 0) {
                    role.ListCommand = toolsService.getTreeObject(result, 'Node', 'ParentNode', '/');
                }
                return commandService.listForRole({ RoleID: role.Model.ID });
            }).then((result) => {
                if (result.length > 0) {
                    role.Model.Permissions = [];
                    for (var i = 0; i < result.length; i++) {
                        role.Model.Permissions.push(result[i]);
                    }
                }
                role.state = 'edit';
                $location.path(`role/edit/${role.Model.ID}`);
            }).catch(() => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function add() {
            role.state = 'add';
            $location.path('/role/add');
        }

        function editRole() {
            loadingService.show();
            return $q.resolve().then(() => {
                var json = '';
                if (role.Model.Permissions.length === 0)
                    toaster.pop('error', '', 'مجوزی برای نقش انتخاب نکردید');
                for (var i = 0; i < role.Model.Permissions.length; i++) {
                    json += role.Model.Permissions[i].ID + ',';
                }
                role.Model.Json = json;
                return roleService.edit(role.Model);
            }).then((result) => {
                role.Model = result;
                return commandService.listForRole({ RoleID: $routeParams.id });
            }).then((result) => {
                if (result.length > 0) {
                    role.Model.Permissions = [];
                    for (var i = 0; i < result.length; i++) {
                        role.Model.Permissions.push(result[i]);
                    }
                }
                role.grid.getlist(false);
                toaster.pop('success', '', 'ویرایش با موفقیت انجام شد');
            }).catch(() => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function addRole() {
            loadingService.show();
            return $q.resolve().then(() => {
                return roleService.add(role.Model);
            }).then((result) => {
                role.Model = result;
                return edit(role.Model.ID);
            }).catch((error) => {
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function back() {
            $location.path('role/cartable');
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('positionController', positionController);
    positionController.$inject = ['$scope', '$q', '$timeout', '$routeParams', '$location', 'toaster', '$timeout', 'loadingService', 'positionService', 'toaster', 'profileService', 'roleService', 'departmentService', 'userService', 'enumService', 'toolsService', 'authenticationService'];
    function positionController($scope, $q, $timeout, $routeParams, $location, toaster, $timeout, loadingService, positionService, toaster, profileService, roleService, departmentService, userService, enumService, toolsService, authenticationService) {
        let position = $scope;
        position.department = $scope;
        position.department.Model = {};
        position.department.departmentDropDown = [];
        position.changeState = {
            cartable: cartable,
            add: add,
            edit: edit
        }
        position.Model = {};
        position.Model.Errors = [];
        position.listPositions = [];
        position.state = '';
        position.ResultSearch = { Enabled: false };
        position.selectCommand = {
            selected: []
        };
        position.searchNationalCode = searchNationalCode;
        position.addPosition = addPosition;
        position.editPosition = editPosition;
        position.resetPassword = resetPassword;
        position.departmentChange = departmentChange;
        position.selectPositionType = toolsService.arrayEnum(enumService.PositionType);
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        positionService.get($routeParams.id).then((result) => {
                            position.changeState.edit(result);
                        })
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                return departmentService.list();
            }).then((result) => {
                position.department.departmentDropDown = result;
                position.state = 'cartable';
                $location.path('/position/cartable');
            }).finally(loadingService.hide)

        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return listRole();
            }).then(() => {
                position.state = 'add';
                $location.path('/position/add');
            }).catch(() => {
                position.changeState.cartable();
                loadingService.hide();
            }).finally(loadingService.hide)
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                if (model && model.ID !== null)
                    return positionService.get(model.ID);
                else
                    return positionService.get($routeParams.id);
            }).then((result) => {
                position.Model = angular.copy(result);
                return listRole();
            }).then(() => {
                position.state = 'edit';
                $location.path(`/position/edit/${position.Model.ID}`);
                loadingService.hide;
            })

                .finally(loadingService.hide);
        }
        function searchNationalCode() {
            loadingService.show();
            return $q.resolve().then(() => {
                return profileService.searchByNationalCode(position.Model);
            }).then((result) => {
                if (result.Enabled) {
                    position.ResultSearch = result;
                    position.search = true;
                } else {
                    toaster.pop('error', '', 'کاربری با کدملی وارد شده پیدا نشد');
                }
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);

        }
        function listRole() {
            return roleService.list().then((result) => {
                position.listRole = [].concat(result);
            })
        }
        function addPosition() {
            loadingService.show();
            return $q.resolve().then(() => {
                if (!position.ResultSearch.Enabled)
                    return $q.reject('کاربری را جست و جو نکردید');
                if (position.Model.Roles.length == 0)
                    return $q.reject('نقشی انتخاب نکرده اید');

                var json = '';
                for (var i = 0; i < position.Model.Roles.length; i++)
                    json += position.Model.Roles[i].ID + ',';
                position.Model.Json = json;
                position.Model.UserID = position.ResultSearch.ID;
                return positionService.add(position.Model);
            }).then(() => {
                toaster.pop('success', '', 'جایگاه جدید با موفقیت افزوده شد');
                loadingService.hide();
                position.changeState.cartable();
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', error || 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editPosition() {
            loadingService.show();
            return $q.resolve().then(() => {
                if (position.Model.Roles.length == 0)
                    return $q.reject('نقشی را انتخاب نمایید');

                var json = '';
                for (var i = 0; i < position.Model.Roles.length; i++)
                    json += position.Model.Roles[i].ID + ',';
                position.Model.Json = json;

                return positionService.edit(position.Model);
            }).then(() => {
                toaster.pop('success', '', 'جایگاه با موفقیت ویرایش گردید');
                loadingService.hide();
                position.changeState.cartable();
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', error || 'خطای نانشناخته');
            })
                .finally(loadingService.hide);
        }
        function resetPassword(selected) {
            loadingService.show();
            return $q.resolve().then(() => {
                return userService.resetPassword(selected.UserID);
            }).then(() => {
                loadingService.hide();
                toaster.pop('success', '', 'رمز عبور با موفقیت تغییر کرد');
            }).catch((error) => {
                toaster.pop('error', '', error || 'خطای ناشناخته');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function departmentChange() {
            position.listPositions = [];
            loadingService.show();
            return $q.resolve().then(() => {
                return positionService.list({ DepartmentID: position.department.Model.DepartmentID });
            }).then((result) => {
                position.showlistPositions = true;
                position.listPositions = [].concat(result);
            }).finally(loadingService.hide);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('changePasswordController', changePasswordController);
    changePasswordController.$inject = ['$scope', '$q', '$timeout', '$window', 'toaster', 'loadingService', 'profileService', 'toolsService'];
    function changePasswordController($scope, $q, $timeout, $window, toaster, loadingService, profileService, toolsService) {
        let profile = $scope;
        profile.Model = {};
        profile.changePassword = changePassword;

        function changePassword() {
            loadingService.show();
            $q.resolve().then(() => {
                profile.Model.UserID = toolsService.userID();
                return profileService.setPassword(profile.Model)
            }).then((result) => {
                toaster.pop('success', '', result.Message);
                $timeout(function () {
                    localStorage.clear();
                    $window.location.href('/account/login');
                }, 1000);
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('productController', productController);
    productController.$inject = ['$scope', '$routeParams', 'loadingService', '$q', 'toaster', '$location', 'categoryService', 'productService', 'attachmentService', 'attributeService', 'productMapattributeService', 'productVariantattributeService', 'shippingCostService', 'toolsService', 'enumService', 'froalaOption', 'deliveryDateService', 'relatedProductService'];
    function productController($scope, $routeParams, loadingService, $q, toaster, $location, categoryService, productService, attachmentService, attributeService, productMapattributeService, productVariantattributeService, shippingCostService, toolsService, enumService, froalaOption, deliveryDateService, relatedProductService) {
        var product = $scope;
        product.Model = {};
        product.Attribute = {};
        product.Attribute.Model = {};

        product.ProductVariant = {};
        product.ProductVariant.Model = {};
        product.Model.Errors = [];

        product.pic = { type: '6', allowMultiple: true, validTypes: 'image/jpeg' };
        product.pic.list = [];
        product.pic.listUploaded = [];

        product.file = { type: '10', allowMultiple: false, validTypes: 'application/x-zip-compressed' };
        product.file.list = [];
        product.file.listUploaded = [];

        product.relatedProduct = {};
        product.relatedProduct.Model = {};
        product.relatedProduct.selectProduct = [];

        product.froalaOption = angular.copy(froalaOption.main);
        product.froalaOptions = angular.copy(froalaOption.main);

        product.ProductVariant.showGrid = false;
        product.ProductVariant.Parent = true;
        product.addProductMapAttribute = addProductMapAttribute;
        product.addProduct = addProduct;
        product.addProductVarient = addProductVarient;
        product.listProductVarient = listProductVarient;
        product.editProduct = editProduct;
        product.showAttributeModal = showAttributeModal;
        product.relatedProductModal = relatedProductModal;
        product.addRelatedProduct = addRelatedProduct;
        product.editRelatedProduct = editRelatedProduct;
        product.selectDiscountType = toolsService.arrayEnum(enumService.DiscountType);
        product.Attribute.grid = {
            bindingObject: product.Attribute
            , columns: [{ name: 'Name', displayName: 'عنوان' }]
            , listService: productMapattributeService.list
            , options: () => {
                return product.Model.ID
            }
            , initLoad: false
            , actions: [
                {
                    class: "fa fa-plus text-info mr-2 cursor-grid operation-icon"
                    , name: "add"
                    , title: "افزودن اطلاعات"
                    , onclick: (selected) => {
                        loadingService.show();
                        product.ProductVariant.Model.ProductVariantAttributeID = selected.ID;
                        listProductVarient(selected.ID);
                        $('#attribute-modal').modal('show');
                        loadingService.hide();
                    }
                }
                , {
                    class: 'fa fa-search text-primary mr-2 cursor-grid operation-icon'
                    , name: 'show'
                    , title: 'مشاهده زیر گروه'
                    , onclick: (selected) => {
                        product.ProductVariant.showGrid = true;
                        product.Attribute.showGrid = false;
                        product.ProductVariant.Parent = false;
                        return listProductVarient(selected.ID);
                    }
                }
                , {
                    class: 'fa fa-times text-danger mr-2 cursor-grid operation-icon'
                    , name: 'remove'
                    , title: 'حذف'
                    , onclick: (selected) => {
                        loadingService.show();
                        productMapattributeService.remove(selected.ID).then(() => {
                            product.Attribute.grid.getlist();
                            loadingService.hide();
                        })
                    }
                }
            ]
            , hidePaging: true
        };
        product.ProductVariant.grid = {
            bindingObject: product.ProductVariant
            , columns: [{ name: 'Name', displayName: 'عنوان' },
            { name: 'Price', displayName: 'قیمت' }]
            , listService: productVariantattributeService.list
            , options: () => {
                return product.ProductVariant.search;
            }
            , initLoad: false
            , actions: [{
                class: 'fa fa-pencil text-info mr-2 cursor-grid operation-icon'
                , name: 'edit'
                , title: 'مشاهده'
                , onclick: (selected) => {
                    showAttributeModal(selected);
                }
            }
                , {
                class: 'fa fa-times'
                , name: 'remove'
                , title: 'حذف'
                , onclick: (selected) => {
                    productVariantattributeService.remove(selected.ID).then(() => {
                        product.ProductVariant.grid.getlist();
                        loadingService.hide();
                    })
                }
            }
            ]
        };
        product.relatedProduct.listProductGrid = {
            bindingObject: product.relatedProduct
            , columns: [{ name: 'Name', displayName: 'عنوان آگهی' },
            { name: 'CategoryName', displayName: 'دسته بندی' },]
            , listService: productService.list
            , actions: [{
                class: 'fa fa-pencil text-info mr-2 cursor-grid operation-icon'
                , name: 'edit'
                , title: 'مشاهده'
                , onclick: (selected) => {
                    loadingService.show();
                    product.relatedProduct.selectProduct.push({ ProductID1: $routeParams.id, ProductID2: selected.ID });
                    loadingService.hide();
                }
            }]
            , globalSearch: true
            , hidePaging: true
        }
        product.relatedProduct.relatedProductGrid = {
            bindingObject: product.relatedProduct
            , columns: [{ name: 'ProductName2', displayName: 'نام محصول مرتبط' },
            { name: 'Priority', displayName: 'اولویت' }]
            , options: () => {
                return { ProductID1: $routeParams.id }
            }
            , listService: relatedProductService.list
            , actions: [
                {
                    class: 'fa fa-pencil text-info mr-2 cursor-grid operation-icon'
                    , name: 'edit'
                    , title: 'ویرایش'
                    , onclick: (selected) => {
                        loadingService.show();
                        product.relatedProduct.Model = selected;
                        $('#related-product-modal').modal('show');
                        loadingService.hide();
                    }
                }
                ,
                {
                    class: 'fa fa-times text-danger mr-2 cursor-grid operation-icon'
                    , name: 'remove'
                    , title: 'حذف'
                    , onclick: (selected) => {
                        loadingService.show();
                        relatedProductService.remove(selected.ID).then(() => {
                            product.relatedProduct.relatedProductGrid.getlist();
                            loadingService.hide();
                        })
                    }
                }
            ]
            , globalSearch: true
            , initLoad: true
            , hidePaging: true
        }
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        productService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryType();
            }).then(() => {
                return listShippingCost();
            }).then(() => {
                return listDeliveryDate();
            }).then(() => {
                product.state = 'add';
                $location.path('/product/add');
                loadingService.hide();
            })
        }
        function edit(model) {
            product.Model = model;
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryType();
            }).then(() => {
                return listAttribute();
            }).then(() => {
                product.Attribute.grid.getlist();
            }).then(() => {
                return listShippingCost();
            }).then(() => {
                return listDeliveryDate();
            }).then(() => {
                return attachmentService.list({ ParentID: product.Model.ID });
            }).then((result) => {
                product.pic.listUploaded = [];
                product.file.listUploaded = [];

                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 10)
                            product.file.listUploaded.push(result[i]);
                        else
                            product.pic.listUploaded.push(result[i]);
                    }
                }
                product.state = 'edit';
                $location.path(`/product/edit/${product.Model.ID}`);
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', 'خطا');
            }).finally(loadingService.hide);

        }
        function addProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return productService.add(product.Model);
            }).then((result) => {
                product.Model = result;
                toaster.pop('success', '', 'محصول جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $location.path(`product/edit/${product.Model.ID}`);
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    product.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return productService.edit(product.Model);
            }).then((result) => {
                product.Model = result;

                if (product.pic.list.length) {
                    product.pics = [];
                    if (product.pic.listUploaded && product.pic.listUploaded.length === 0) {
                        product.pics.push({ ParentID: product.Model.ID, Type: 1, FileName: product.pic.list[0], PathType: product.pic.type });
                    } else {
                        for (var i = 0; i < product.pic.list.length; i++) {
                            product.pics.push({ ParentID: product.Model.ID, Type: 2, FileName: product.pic.list[i], PathType: product.pic.type });
                        }
                    }
                    return attachmentService.add(product.pics);
                }
                return true;
            }).then(() => {
                if (product.file.list.length) {
                    product.files = [];
                    product.files.push({ ParentID: product.Model.ID, Type: 2, FileName: product.file.list[0], PathType: product.file.type });
                    return attachmentService.add(product.files);
                }
                return true;
            }).then((result) => {
                product.pics = [];
                product.files = [];
                return attachmentService.list({ ParentID: product.Model.ID });
            }).then((result) => {
                product.pic.reset();
                product.file.reset();

                product.pic.listUploaded = [];
                product.file.listUploaded = [];

                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 10)
                            product.file.listUploaded.push(result[i]);
                        else
                            product.pic.listUploaded.push(result[i]);
                    }
                }
                toaster.pop('success', '', 'محصول جدید با موفقیت ویرایش گردید');
                loadingService.hide();
            })
                .catch((error) => {
                    if (!error) {
                        $('#content > div').animate({
                            scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        var listError = error.split('&&');
                        product.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
        }
        function categoryType() {
            return categoryService.list('dropdown').then((result) => {
                product.categoryType = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentNode !== '/')
                        product.categoryType.push({ Model: result[i].ID, Name: result[i].Title });
                }
            })
        }
        function listAttribute() {
            return attributeService.list({ PageSize: 0, PageIndex: 0 }).then((result) => {
                var list = [].concat(result);
                product.attributeType = [];
                for (var i = 0; i < list.length; i++) {
                    product.attributeType.push({ Model: list[i].ID, Name: list[i].Name });
                }
            })
        }
        function addProductMapAttribute() {
            product.Attribute.Model.ProductID = product.Model.ID;
            product.Attribute.Model.attributeControlType = 1;
            product.Attribute.Model.IsRequired = 1;
            loadingService.show();
            return productMapattributeService.add(product.Attribute.Model).then((result) => {
                return product.Attribute.grid.getlist();
            }).catch((error) => {
                loadingService.hide();
            }).finally(loadingService.hide)
        }
        function addProductVarient() {
            loadingService.show();
            return $q.resolve().then(() => {
                return productVariantattributeService.save(product.ProductVariant.Model);
            }).then((result) => {
                product.ProductVariant.Model = {};
                $('#attribute-modal').modal('hide');
                product.ProductVariant.grid.getlist();
            }).catch(() => {
                toaster.pop('error', '', 'خطای نامشخص');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function listProductVarient(model) {
            product.ProductVariant.search = model;
            product.ProductVariant.grid.getlist(model);
        }
        function showAttributeModal(item) {
            loadingService.show();
            product.ProductVariant.Model = item;
            $('#attribute-modal').modal('show');
            loadingService.hide();
        }
        function listShippingCost() {
            loadingService.show();
            return $q.resolve().then(() => {
                //retuen list product type enabled
                return shippingCostService.list({ Enabled: 1 });
            }).then((result) => {
                if (result && result.length > 0) {
                    product.shippingCost = [];
                    product.shippingCost.push({ Model: '-1', Name: `ساده` });
                    for (var i = 0; i < result.length; i++) {
                        product.shippingCost.push({ Model: result[i].ID, Name: `${result[i].Name} - ${result[i].Price}` });
                    }
                }
            }).finally(loadingService.hide);
        }
        function listDeliveryDate() {
            loadingService.show();
            return $q.resolve().then(() => {
                //retuen list delivery date enabled
                return deliveryDateService.list({ Enabled: 1 });
            }).then((result) => {
                if (result && result.length > 0) {
                    product.deliveryDate = [];
                    for (var i = 0; i < result.length; i++) {
                        product.deliveryDate.push({ Model: result[i].ID, Name: result[i].Name });
                    }
                }
            }).finally(loadingService.hide);
        }

        //related product
        function relatedProductModal() {
            loadingService.show();
            return product.relatedProduct.listProductGrid.getlist().then(() => {
                $('#related-product-select-modal').modal('show');
                loadingService.hide();
            }).finally(loadingService.hide);

        }
        function addRelatedProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return relatedProductService.add(product.relatedProduct.selectProduct);
            }).then(() => {
                product.relatedProduct.selectProduct = [];
                return product.relatedProduct.relatedProductGrid.getlist();
            }).then(() => {
                $('#related-product-select-modal').modal('hide');
            }).finally(loadingService.hide);
        }
        function editRelatedProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return relatedProductService.edit(product.relatedProduct.Model);
            }).then(() => {
                return product.relatedProduct.relatedProductGrid.getlist();
            }).then(() => {
                $('#related-product-modal').modal('hide');
            }).finally(loadingService.hide);
        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('ProductCartableController', ProductCartableController);
    ProductCartableController.$inject = ['$scope', 'productService', '$q', '$location', 'enumService', 'toolsService', 'loadingService'];
    function ProductCartableController($scope, productService, $q, $location, enumService, toolsService, loadingService) {
        let product = $scope;
        product.search = [];
        product.search.Model = {};
        product.search.selectYesOrNoType = toolsService.arrayEnum(enumService.YesOrNoType);
        product.search.selectYesOrNoType.map((item) => {
            if (item.Name === 'خیر')
                item.Model = 0;
        })
        product.grid = {
            bindingObject: product
            , columns: [{ name: 'Name', displayName: 'عنوان آگهی' },
            { name: 'CategoryName', displayName: 'دسته بندی' },
            { name: 'TrackingCode', displayName: 'کدپیگیری' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: productService.list
            , route: 'product'
            , globalSearch: true
            , initLoad: true
            , options: () => {
                return product.search.Model;
            }
        };
        product.goToPageAdd = goToPageAdd;
        product.search.clear = clear;
        function goToPageAdd() {
            $location.path('/product/add');
        }
        function clear() {
            loadingService.show();
            product.search.Model = {};
            product.search.searchPanel = false;
            product.grid.getlist();
            loadingService.hide();
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('attributeController', attributeController);
    attributeController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout', 'attributeService'];
    function attributeController($scope, $q, loadingService, $routeParams, $location, toaster, $timeout, attributeService) {
        let attribute = $scope;
        attribute.Model = {};
        attribute.main = {};
        attribute.main.changeState = {
            cartable: cartable,
            edit: edit
        };
        attribute.Model.Errors = [];
        attribute.search = [];
        attribute.search.Model = {};
        attribute.state = '';
        attribute.goToPageAdd = goToPageAdd;
        attribute.addAttribute = addAttribute;
        attribute.editAttribute = editAttribute;
        attribute.search.clear = clear;
        attribute.grid = {
            bindingObject: attribute
            , columns: [{ name: 'Name', displayName: 'عنوان' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: attributeService.list
            , onEdit: attribute.main.changeState.edit
            , globalSearch: true
            , initLoad: true
            , options: () => {
                return attribute.search.Model;
            }
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        attributeService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            attribute.Model = {};
            attribute.state = 'cartable';
            $location.path('/attribute/cartable');
        }
        function add() {
            attribute.state = 'add';
            $location.path('/attribute/add');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                attribute.state = 'edit';
                attribute.Model = model;
                $location.path(`/attribute/edit/${attribute.Model.ID}`);
            }).finally(loadingService.hide);
        }
        function goToPageAdd() {
            add();
        }
        function addAttribute() {
            loadingService.show();
            return $q.resolve().then(() => {
                return attributeService.add(attribute.Model);
            }).then((result) => {
                attribute.Model = result;
                attribute.grid.getlist(false);
                toaster.pop('success', '', 'ویژگی جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editAttribute() {
            loadingService.show();
            return $q.resolve().then(() => {
                return attributeService.edit(attribute.Model);
            }).then((result) => {
                attribute.Model = result;
                attribute.grid.getlist(false);
                toaster.pop('success', '', 'ویژگی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function clear() {
            loadingService.show();
            attribute.search.Model = {};
            attribute.search.searchPanel = false;
            attribute.grid.getlist();
            loadingService.hide();
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryController', categoryController);
    categoryController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'categoryService', '$location', 'toaster', 'discountService', 'toolsService'];
    function categoryController($scope, $q, loadingService, $routeParams, categoryService, $location, toaster, discountService, toolsService) {
        let category = $scope;
        category.Model = {};
        category.list = [];
        category.lists = [];
        category.state = '';

        category.addCategory = addCategory;
        category.addSubCategory = addSubCategory;
        category.editCategory = editCategory;
        category.confirmRemove = confirmRemove;
        category.changeState = {
            cartable: cartable,
            add: add
        }
        init();
        category.tree = {
            data: []
            , colDefs: [
                , { field: 'Title', displayName: 'نام' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div class='pull-left'>
                            <i class='fa fa-plus tgrid-action pl-1 text-success' style='cursor:pointer;' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action pl-1 text-danger' style='cursor:pointer;' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: edit,
                        add: addSubCategory,
                        remove: remove
                    }
                }
            ]
            , expandingProperty: {
                field: "Title"
                , displayName: "عنوان"
            }
        };
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                    case 'edit':
                        categoryService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryService.list('cartable');
            }).then((result) => {
                setTreeObject(result);
            }).then(() => {
                return listDiscount();
            }).then(() => {
                $location.path('category/cartable');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function edit(parent) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryService.get(parent.ID);
            }).then((result) => {
                category.Model = result;
                category.state = 'edit';
                $('#category-modal').modal('show');
            }).finally(loadingService.hide)
        }
        function add(parent) {
            loadingService.show();
            parent = parent || {};
            category.Model = { ParentID: parent.ID };
            category.state = 'add';
            $('#category-modal').modal('show');
            loadingService.hide();
        }

        function addCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                categoryService.add(category.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    $('#category-modal').modal('hide');
                    category.changeState.cartable();
                    loadingService.hide();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                categoryService.edit(category.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $('#category-modal').modal('hide');
                    category.changeState.cartable();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function addSubCategory(parent) {
            category.changeState.add(parent);
        }
        function setTreeObject(categorys) {
            categorys.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            category.tree.data = toolsService.getTreeObject(categorys, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            category.deleteBuffer = model;
            category.displayName = model.Title;
            $('#category-delete').modal('show');
            loadingService.hide();
        }
        function confirmRemove() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryService.remove(category.deleteBuffer.ID);
            }).then(() => {
                return categoryService.list();
            }).then((result) => {
                $('#category-delete').modal('hide');
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
        function listDiscount() {
            return $q.resolve().then(() => {
                return discountService.list({});
            }).then((result) => {
                category.listDiscount = [];
                for (i = 0; i < result.length; i++) {
                    category.listDiscount.push({ Model: result[i].ID, Name: result[i].Name });
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('discountController', discountController);
    discountController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'discountService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService'];
    function discountController($scope, $q, loadingService, $routeParams, discountService, $location, toaster, $timeout, toolsService, enumService) {
        let discount = $scope;
        discount.Model = {};
        discount.main = {};
        discount.main.changeState = {
            cartable: cartable,
            edit: edit
        };
        discount.Errors = [];
        discount.state = '';
        discount.search = [];
        discount.search.Model = {};
        discount.goToPageAdd = goToPageAdd;
        discount.addDiscount = addDiscount;
        discount.editDiscount = editDiscount;
        discount.search.clear = clear;
        discount.grid = {
            bindingObject: discount
            , columns: [{ name: 'Name', displayName: 'عنوان تخفیف' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: discountService.list
            , onEdit: discount.main.changeState.edit
            , globalSearch: true
            , initLoad: true
            , options: () => {
                return discount.search.Model;
            }
        };
        discount.selectDiscountType = toolsService.arrayEnum(enumService.DiscountType);
        init();


        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        discountService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            discount.Model = {};
            discount.state = 'cartable';
            $location.path('/discount/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                discount.state = 'add';
                $location.path('/discount/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                discount.state = 'edit';
                discount.Model = model;
                $location.path(`/discount/edit/${discount.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function goToPageAdd() {
            add();
        }
        function addDiscount() {
            loadingService.show();
            return $q.resolve().then(() => {
                return discountService.add(discount.Model)
            }).then((result) => {
                discount.grid.getlist(false);
                toaster.pop('success', '', 'تخفیف جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editDiscount() {
            loadingService.show();
            return $q.resolve().then(() => {
                return discountService.edit(discount.Model)
            }).then((result) => {
                discount.grid.getlist(false);
                discount.Model = result;
                toaster.pop('success', '', 'تخفیف با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function clear() {
            loadingService.show();
            discount.search.Model = {};
            discount.search.searchPanel = false;
            discount.grid.getlist();
            loadingService.hide();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentController', commentController);
    commentController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption'];
    function commentController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout, toolsService, enumService, froalaOption) {
        let comment = $scope;
        comment.Model = {};
        comment.state = '';
        comment.main = {};
        comment.search = [];
        comment.search.Model = {};
        comment.search.Model = { ShowChildren: false, CommentForType : 6};
        comment.froalaOptionComment = froalaOption.comment;
        comment.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        comment.editComment = editComment;
        comment.search.clear = clear;
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'CreatorName', displayName: 'نام کاربر' },
            { name: 'ProductName', displayName: 'نام محصول' },
            { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: enumService.CommentType },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => {
                return comment.search.Model;
            }
            , initLoad: true
        };
        comment.selectCommentType = toolsService.arrayEnum(enumService.CommentType);
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        commentService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'cartable';
                $location.path('/comment/cartable');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'edit';
                comment.Model = model;
                $location.path(`/comment/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }
        function editComment() {
            loadingService.show();
            return $q.resolve().then(() => {
                if (comment.Model.CommentType === 0) {
                    loadingService.hide();
                    toaster.pop('error', '', 'وضعیت نظر را تعیین نمایید');
                } else {
                    commentService.edit(comment.Model).then((result) => {
                        loadingService.hide();
                        comment.grid.getlist(false);
                        $timeout(function () {
                            toaster.pop('success', '', 'نظر ویرایش گردید');
                            cartable();

                        }, 1000);
                    }).finally(loadingService.hide);
                }
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            })
        }
        function clear() {
            loadingService.show();
            comment.search.Model = {};
            comment.search.searchPanel = false;
            comment.grid.getlist();
            loadingService.hide();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryPortalController', categoryPortalController);
    categoryPortalController.$inject = ['$scope', '$q', 'categoryPortalService', 'loadingService', '$routeParams', '$location', 'toaster', 'toolsService'];
    function categoryPortalController($scope, $q, categoryPortalService, loadingService, $routeParams, $location, toaster, toolsService) {
        let category = $scope;
        category.Model = {};
        category.list = [];
        category.lists = [];
        category.state = '';

        category.addCategory = addCategory;
        category.addSubCategory = addSubCategory;
        category.editCategory = editCategory;
        category.confirmRemove = confirmRemove;
        category.changeState = {
            cartable: cartable,
            add: add
        }
        init();
        category.tree = {
            data: []
            , colDefs: [
                , { field: 'Title', displayName: 'نام' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div class='pull-left'>
                            <i class='fa fa-plus tgrid-action pl-1 text-success' style='cursor:pointer;' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action pl-1 text-danger' style='cursor:pointer;' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: edit,
                        add: addSubCategory,
                        remove: remove
                    }
                }
            ]
            , expandingProperty: {
                field: "Title"
                , displayName: "عنوان"
            }
        };
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                    case 'edit':
                        categoryPortalService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            categoryPortalService.list('cartable').then((result) => {
                setTreeObject(result);
            });
            $location.path('category-portal/cartable');
        }
        function edit(parent) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.get(parent.ID);
            }).then((result) => {
                category.Model = result;
                //    return categoryPortalService.listByNode({ Node: result.ParentNode });
                //}).then((result) => {
                category.state = 'edit';
                $('#category-portal-modal').modal('show');
            }).finally(loadingService.hide)
        }
        function add(parent) {
            loadingService.show();
            parent = parent || {};
            category.Model = { ParentID: parent.ID };
            category.state = 'add';
            $('#category-portal-modal').modal('show');
            loadingService.hide();
        }

        function addCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                categoryPortalService.add(category.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    $('#category-portal-modal').modal('hide');
                    category.changeState.cartable();
                    loadingService.hide();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                categoryPortalService.edit(category.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $('#category-portal-modal').modal('hide');
                    category.changeState.cartable();
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function addSubCategory(parent) {
            category.changeState.add(parent);
        }
        function setTreeObject(categorys) {
            categorys.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            category.tree.data = toolsService.getTreeObject(categorys, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            category.deleteBuffer = model;
            category.displayName = model.Title;
            $('#category-portal-delete').modal('show');
            loadingService.hide();
        }
        function confirmRemove() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.remove(category.deleteBuffer.ID);
            }).then(() => {
                return categoryPortalService.list();
            }).then((result) => {
                $('#category-portal-delete').modal('hide');
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('articleController', articleController);
    articleController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'articleService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService', 'toolsService', 'enumService', 'froalaOption'];
    function articleController($scope, $q, loadingService, $routeParams, articleService, $location, toaster, $timeout, categoryPortalService, attachmentService, toolsService, enumService, froalaOption) {
        let article = $scope;
        article.Model = {};
        article.main = {};
        article.search = [];
        article.search.Model = {};
        article.Model.Errors = [];
        article.pic = { type: '4', allowMultiple: false, validTypes: 'image/jpeg' };
        article.pic.list = [];
        article.pic.listUploaded = [];

        article.video = { type: '7', allowMultiple: false, validTypes: 'video/mp4' };
        article.video.list = [];
        article.video.listUploaded = [];

        article.state = '';
        article.addArticle = addArticle;
        article.editArticle = editArticle;
        article.search.clear = clear;
        article.typeshow = toolsService.arrayEnum(enumService.ShowArticleType);
        article.typecomment = toolsService.arrayEnum(enumService.CommentArticleType);
        init();
        article.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        article.grid = {
            bindingObject: article
            , columns: [{ name: 'Title', displayName: 'عنوان مقاله' },
            { name: 'TrackingCode', displayName: 'کد پیگیری' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: articleService.list
            , deleteService: articleService.remove
            , onAdd: article.main.changeState.add
            , onEdit: article.main.changeState.edit
            , route: 'article'
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
            , options: () => {
                return article.search.Model;
            }
        };
        article.froalaOption = angular.copy(froalaOption.main);
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        articleService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            $('.js-example-tags').empty();
            clearModel();
            article.state = 'cartable';
            $location.path('/article/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropCategory();
            }).then(() => {
                article.state = 'add';
                $location.path('/article/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return articleService.get(model.ID);
            }).then((model) => {
                article.Model = model;
                if (article.Model.Tags !== null && article.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < article.Model.Tags.length; i++) {
                        newOption.push(new Option(article.Model.Tags[i], article.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }
                return true;
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                article.pic.listUploaded = [];
                article.video.listUploaded = [];
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 8)
                            article.pic.listUploaded = [].concat(result[i]);
                        if (result[i].PathType === 7)
                            article.video.listUploaded = [].concat(result[i]);
                    }
                }
                article.state = 'edit';
                $location.path(`/article/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }

        function addArticle() {
            loadingService.show();
            return $q.resolve().then(() => {
                return articleService.add(article.Model);
            }).then((result) => {
                article.Model = result;
                if (article.pic.list.length) {
                    article.pics = [];
                    if (article.pic.listUploaded === 0) {
                        article.pics.push({ ParentID: article.Model.ID, Type: 2, FileName: article.pic.list[0], PathType: article.pic.type });
                    }
                    return attachmentService.add(article.pics);
                }
                return true;
            }).then(() => {
                if (article.video.list.length) {
                    article.videos = [];
                    if (article.video.listUploaded.length === 0) {
                        article.videos.push({ ParentID: article.Model.ID, Type: 2, FileName: article.video.list[0], PathType: article.video.type });
                        return attachmentService.add(article.videos);
                    }
                }
            }).then(() => {
                article.videos = [];
                article.pics = [];
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                article.pic.reset();
                article.video.reset();
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 8)
                            article.pic.listUploaded = [].concat(result[i]);
                        if (result[i].PathType === 7)
                            article.video.listUploaded = [].concat(result[i]);
                    }
                }
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله جدید با موفقیت اضافه گردید');
                loadingService.hide();
                article.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    article.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editArticle() {
            loadingService.show();
            return $q.resolve().then(() => {
                return articleService.edit(article.Model);
            }).then((result) => {
                article.Model = result;
                if (article.pic.list.length) {
                    article.pics = [];
                    if (article.pic.listUploaded === 0) {
                        article.pics.push({ ParentID: article.Model.ID, Type: 2, FileName: article.pic.list[0], PathType: article.pic.type });
                    }
                    return attachmentService.add(article.pics);
                }
                return true;
            }).then(() => {
                if (article.video.list.length) {
                    article.videos = [];
                    if (article.video.listUploaded.length === 0) {
                        article.videos.push({ ParentID: article.Model.ID, Type: 2, FileName: article.video.list[0], PathType: article.video.type });
                        return attachmentService.add(article.videos);
                    }
                }
            }).then(() => {
                article.videos = [];
                article.pics = [];
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                article.pic.reset();
                article.video.reset();
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 8)
                            article.pic.listUploaded = [].concat(result[i]);
                        if (result[i].PathType === 7)
                            article.video.listUploaded = [].concat(result[i]);
                    }
                }
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله موفقیت ویرایش گردید');
                loadingService.hide();
                article.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    article.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }

        function fillDropCategory() {
            categoryPortalService.list('dropdown').then((result) => {
                article.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentNode !== '/') {
                        article.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            article.Model = {};
            article.pic.listUploaded = [];
        }
        function clear() {
            loadingService.show();
            article.search.Model = {};
            article.search.searchPanel = false;
            article.grid.getlist();
            loadingService.hide();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('newsController', newsController);
    newsController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'newsService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService', 'toolsService', 'enumService', 'froalaOption'];
    function newsController($scope, $q, loadingService, $routeParams, newsService, $location, toaster, $timeout, categoryPortalService, attachmentService, toolsService, enumService, froalaOption) {
        let news = $scope;
        news.Model = {};
        news.main = {};
        news.search = [];
        news.search.Model = {};

        news.pic = { type: '3', allowMultiple: false, validTypes: 'image/jpeg' };
        news.pic.listUploaded = [];
        news.pic.list = [];
        news.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        }
        news.Model.Errors = [];
        news.state = '';
        news.froalaOption = angular.copy(froalaOption.main);
        news.addNews = addNews;
        news.editNews = editNews;
        news.search.clear = clear;
        news.typeshow = toolsService.arrayEnum(enumService.ShowArticleType);
        news.typecomment = toolsService.arrayEnum(enumService.CommentArticleType);
        news.grid = {
            bindingObject: news
            , columns: [{ name: 'Title', displayName: 'عنوان خبر' },
            { name: 'TrackingCode', displayName: 'کد پیگیری' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: newsService.list
            , deleteService: newsService.remove
            , onEdit: news.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
            , options: () => {
                return news.search.Model;
            }
        };

        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        newsService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            $('.js-example-tags').empty();
            news.state = 'cartable';
            clearModel();
            $location.path('/news/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropCategory();
            }).then(() => {
                news.attachment.reset();
            }).then(() => {
                news.Model = {};
                news.state = 'add';
                $location.path('/news/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return newsService.get(model.ID);
            }).then((result) => {
                news.Model = angular.copy(result);
                if (news.Model.IsShow) {
                    news.Model.IsShow = 1;
                }
                else {
                    news.Model.IsShow = 0;
                }
                if (news.Model.CommentStatus) {
                    news.Model.CommentStatus = 1;
                }
                else {
                    news.Model.CommentStatus = 0;
                }
                if (news.Model.Tags !== null && news.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < news.Model.Tags.length; i++) {
                        newOption.push(new Option(news.Model.Tags[i], news.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }

                return true;
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    news.pic.listUploaded = [].concat(result);
            }).then(() => {
                news.state = 'edit';
                $location.path(`/news/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }

        function addNews() {
            loadingService.show();
            return $q.resolve().then(() => {
                return newsService.add(news.Model)
            }).then((result) => {
                news.Model = result;
                if (news.pic.list.length) {
                    news.pics = [];
                    if (!news.pic.listUploaded) {
                        news.pics.push({ ParentID: news.Model.ID, Type: 1, FileName: news.pic.list[0], PathType: news.pic.type });
                    }
                    return attachmentService.add(news.pics);
                }
                return true;
            }).then((result) => {
                news.pics = [];
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    news.pic.listUploaded = [].concat(result);
                news.pic.reset();
                news.grid.getlist(false);
                toaster.pop('success', '', 'خبر جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
            }).catch((error) => {
                if (!error) {
                    var listError = error.split('&&');
                    news.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editNews() {
            loadingService.show();
            return $q.resolve().then(() => {
                return newsService.edit(news.Model);
            }).then((result) => {
                news.Model = result;
                if (news.pic.list.length) {
                    news.pics = [];
                    if (!news.pic.listUploaded) {
                        news.pics.push({ ParentID: news.Model.ID, Type: 1, FileName: news.pic.list[0], PathType: news.pic.type });
                    }
                    return attachmentService.add(news.pics);
                }
                return true;
            }).then((result) => {
                news.pics = [];
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    news.pic.listUploaded = [].concat(result);
                news.pic.reset();
                news.grid.getlist(false);
                toaster.pop('success', '', 'خبر جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
            }).catch((error) => {
                if (!error) {
                    var listError = error.split('&&');
                    news.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function fillDropCategory() {
            categoryPortalService.list('dropdown').then((result) => {
                news.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentNode !== '/') {
                        news.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            news.Model = {};
            news.pic.listUploaded = [];
        }
        function clear() {
            loadingService.show();
            news.search.Model = {};
            news.search.searchPanel = false;
            news.grid.getlist();
            loadingService.hide();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('pagesController', pagesController);
    pagesController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'pagesService', '$location', 'toaster', '$timeout', 'toolsService'];
    function pagesController($scope, $q, loadingService, $routeParams, pagesService, $location, toaster, $timeout, toolsService) {
        let pages = $scope;
        pages.Model = {};
        pages.main = {};
        pages.state = 'cartable';
        pages.goToPageAdd = goToPageAdd;
        pages.addPages = addPages;
        pages.editPages = editPages;
        pages.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        //< i class='fa fa-plus tgrid-action' ng-click='cellTemplateScope.add(row.branch)' title ='افزودن'></i >
        //< i class='fa fa-trash tgrid-action' ng-click='cellTemplateScope.remove(row.branch)' title ='حذف'></i >
        pages.tree = {
            data: []
            , colDefs: [
                { field: 'Title', displayName: 'عنوان' }
                , { field: 'RouteUrl', displayName: 'آدرس' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div style='float: left'>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: view,
                        //add: addFirst,
                        //remove:remove,

                    }
                }
            ]
            , expandingProperty: {
                field: "Title"
                , displayName: "عنوان"
            }
        };
        pagesService.list().then((result) => {
            setTreeObject(result);
        });
        init();
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        pagesService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                    case 'view':
                        pagesService.get($routeParams.id).then((result) => {
                            view(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function cartable() {
            pages.Model = {};
            pages.state = 'cartable';
            $location.path('pages/cartable');
        }
        function add(parent) {
            parent = parent || {};
            pages.Model = { ParentID: parent.ID };
            pages.state = 'add';
            $location.path('/pages/add');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.state = 'edit';
                pages.Model = model;
                $location.path(`/pages/edit/${pages.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function addFirst(parent) {
            pages.state = 'add';
            goToPageAdd(parent);
        }
        function goToPageAdd(parent) {
            add(parent);
        }
        function addPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.add(pages.Model)
            }).then((result) => {
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.edit(pages.Model)
            }).then((result) => {
                pages.Model = result;
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function setTreeObject(items) {
            items.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            pages.tree.data = toolsService.getTreeObject(items, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.remove(model.ID);
            }).then((result) => {
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
        function view(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.state = 'view';
                pages.Model = model;
                $location.path(`/pages/view/${pages.Model.ID}`);
            }).finally(loadingService.hide);

        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('menuController', menuController);
    menuController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'menuService', '$location', '$timeout', 'toolsService', 'enumService', 'toaster'];
    function menuController($scope, $q, loadingService, $routeParams, menuService, $location, toaster, toolsService, enumService, toaster) {
        let menu = $scope;
        menu.Model = {};
        menu.list = [];
        menu.lists = [];
        menu.state = '';
        menu.addMenu = addMenu;
        menu.editMenu = editMenu;
        menu.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        }
        menu.EnableType = toolsService.arrayEnum(enumService.EnableMenuType);
        menu.tree = {
            data: []
            , colDefs: [
                { field: 'Name', displayName: 'عنوان' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div style='float: left'>
                            <i class='fa fa-plus tgrid-action pl-1 text-success' style='cursor:pointer;' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action pl-1 text-danger' style='cursor:pointer;' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: edit,
                        add: addFirst,
                        remove: remove
                    }
                }
            ]
            , expandingProperty: {
                field: "Name"
                , displayName: "عنوان"
            }
        };
        init();


        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        menuService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            menu.Model = {};
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.list();
            }).then((result) => {
                setTreeObject(result);
                menu.state = 'cartable';
                $location.path('menu/cartable');
            }).catch(() => {
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function add(parent) {
            loadingService.show();
            parent = parent || {};
            menu.Model = { ParentID: parent.ID };
            menu.state = 'add';
            $('#menu-modal').modal('show');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.get(model.ID)
            }).then((result) => {
                menu.Model = angular.copy(result);
                menu.state = 'edit';
                $('#menu-modal').modal('show');
            }).finally(loadingService.hide);

        }

        function addFirst(parent) {
            menu.state = 'add';
            menu.changeState.add(parent);
        }
        function addMenu() {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.add(menu.Model)
            }).then((result) => {
                toaster.pop('success', '', 'منو جدید با موفقیت اضافه گردید');
                loadingService.hide();
                menu.changeState.cartable();
                $('#menu-modal').modal('hide');
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editMenu() {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.edit(menu.Model)
            }).then((result) => {
                menu.Model = result;
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                menu.changeState.cartable();
                $('#menu-modal').modal('hide');
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function setTreeObject(items) {
            items.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            menu.tree.data = toolsService.getTreeObject(items, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.remove(model.ID);
            }).then(() => {
                return menuService.list();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('sliderController', sliderController);
    sliderController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'sliderService', '$location', 'toaster', '$timeout', 'attachmentService', 'toolsService', 'enumService'];
    function sliderController($scope, $q, loadingService, $routeParams, sliderService, $location, toaster, $timeout, attachmentService, toolsService, enumService) {
        let slider = $scope;
        slider.Model = {};
        slider.Model.Errors = [];
        slider.state = '';
        slider.pic = { type: '5', allowMultiple: false, validTypes: 'image/jpeg' };
        slider.pic.list = [];
        slider.pic.listUploaded = [];
        slider.main = {};
        slider.search = [];
        slider.search.Model = {};
        slider.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        }
        slider.grid = {
            bindingObject: slider
            , columns: [{ name: 'Title', displayName: 'عنوان اسلایدر' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: sliderService.list
            , deleteService: sliderService.remove
            , onEdit: edit
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
            , options: () => {
                return slider.search.Model;
            }
        };

        slider.addSlider = addSlider;
        slider.editSlider = editSlider;
        slider.search.clear = clear;
        slider.typeEnable = toolsService.arrayEnum(enumService.ShowArticleType);
        init();
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        add();
                        loadingService.hide();
                        break;
                    case 'edit':
                        sliderService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        loadingService.hide();
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            slider.state = 'cartable';
            $location.path('/slider/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                slider.state = 'add';
                $location.path('/slider/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                slider.Model = model;
                return attachmentService.list({ ParentID: slider.Model.ID });
            }).then((result) => {
                slider.pic.listUploaded = [];
                if (result && result.length > 0)
                    slider.pic.listUploaded = [].concat(result);
            }).then(() => {
                slider.state = 'edit';
                $location.path(`/slider/edit/${model.ID}`);
            }).catch(() => {
                loadingService.hide();
            }).finally(loadingService.hide);
        }

        function addSlider() {
            loadingService.show();
            return $q.resolve().then(() => {
                sliderService.add(slider.Model).then((result) => {
                    slider.Model = result;
                    if (slider.pic.list.length) {
                        slider.pics = [];
                        if (slider.pic.listUploaded.length === 0) {
                            slider.pics.push({ ParentID: slider.Model.ID, Type: 2, FileName: slider.pic.list[0], PathType: slider.pic.type });
                        }
                        return attachmentService.add(slider.pics);
                    }
                    return true;
                }).then((result) => {
                    slider.pics = [];
                    return attachmentService.list({ ParentID: slider.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        slider.pic.listUploaded = [].concat(result);

                    slider.pic.reset();
                    slider.grid.getlist();
                    toaster.pop('success', '', 'اسلایدر جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (error && error.length > 0) {
                        var listError = error.split('&&');
                        slider.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function editSlider() {
            loadingService.show();
            return $q.resolve().then(() => {
                sliderService.edit(slider.Model).then((result) => {
                    if (slider.pic.list.length) {
                        slider.pics = [];
                        if (slider.pic.listUploaded.length === 0) {
                            slider.pics.push({ ParentID: slider.Model.ID, Type: 2, FileName: slider.pic.list[0], PathType: slider.pic.type });
                        }
                        return attachmentService.add(slider.pics);
                    }
                    slider.Model = result;
                    return true;
                }).then((result) => {
                    slider.pics = [];
                    return attachmentService.list({ ParentID: slider.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        slider.pic.listUploaded = [].concat(result);
                    slider.pic.reset();
                    slider.grid.getlist();
                    toaster.pop('success', '', 'اسلایدر جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        slider.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function clear() {
            loadingService.show();
            slider.search.Model = {};
            slider.search.searchPanel = false;
            slider.grid.getlist();
            loadingService.hide();
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------
    app.controller('generalSettingController', generalSettingController);
    generalSettingController.$inject = ['$scope', 'loadingService', 'generalSettingService', 'toaster', '$q'];
    function generalSettingController($scope, loadingService, generalSettingService, toaster, $q) {
        let setting = $scope;
        setting.Model = {};
        setting.editSetting = editSetting;
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                return generalSettingService.getSetting();
            }).then((result) => {
                setting.Model = angular.copy(result);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا در بازیابی اطلاعات');
            }).finally(loadingService.hide);
        }

        function editSetting() {
            loadingService.show();
            return $q.resolve().then(() => {
                return generalSettingService.edit(setting.Model);
            }).then(() => {
                toaster.pop('success', '', 'تنظیمات با موفقیت ذخیره گردید');
                init();
            }).finally(loadingService.hide);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('eventsController', eventsController);
    eventsController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'eventsService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService', 'toolsService', 'enumService', 'froalaOption'];
    function eventsController($scope, $q, loadingService, $routeParams, eventsService, $location, toaster, $timeout, categoryPortalService, attachmentService, toolsService, enumService, froalaOption) {
        let events = $scope;
        events.state = '';
        events.froalaOption = angular.copy(froalaOption.main);

        events.Model = {};
        events.Model.Errors = [];

        events.pic = { type: '8', allowMultiple: false, validTypes: 'image/jpeg' };
        events.pic.list = [];
        events.pic.listUploaded = [];

        events.video = { type: '7', allowMultiple: false, validTypes: 'video/mp4' };
        events.video.list = [];
        events.video.listUploaded = [];

        events.main = {};
        events.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        }

        events.search = [];
        events.search.Model = {};

        events.addEvents = addEvents;
        events.editEvents = editEvents;
        events.search.clear = clear;

        events.typeshow = toolsService.arrayEnum(enumService.ShowArticleType);
        events.typecomment = toolsService.arrayEnum(enumService.CommentArticleType);
        events.grid = {
            bindingObject: events
            , columns: [{ name: 'Title', displayName: 'عنوان رویداد' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' },
            { name: 'TrackingCode', displayName: 'کد پیگیری رویداد' }]
            , listService: eventsService.list
            , deleteService: eventsService.remove
            , onEdit: events.main.changeState.edit
            , globalSearch: true
            , searchBy: 'Title'
            , displayNameFormat: ['Title']
            , initLoad: true
            , options: () => {
                return events.search.Model;
            }
        };
        init();
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        eventsService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            clearModel();
            events.state = 'cartable';
            $location.path('/events/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropCategory();
            }).then(() => {
                events.state = 'add';
                $location.path('/events/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            return $q.resolve().then(() => {
                return eventsService.get(model.ID);
            }).then((result) => {
                events.Model = angular.copy(result);
                if (events.Model.Tags !== null && events.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < events.Model.Tags.length; i++) {
                        newOption.push(new Option(events.Model.Tags[i], events.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: events.Model.ID });
            }).then((result) => {
                events.pic.listUploaded = [];
                events.video.listUploaded = [];
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].PathType === 8)
                            events.pic.listUploaded = [].concat(result[i]);
                        if (result[i].PathType === 7)
                            events.video.listUploaded = [].concat(result[i]);
                    }
                }
            }).then(() => {
                events.state = 'edit';
                $location.path(`/events/edit/${model.ID}`);
            })
        }

        function addEvents() {
            loadingService.show();
            return $q.resolve().then(() => {
                eventsService.add(events.Model).then((result) => {
                    events.Model = angular.copy(result);
                    if (events.pic.list.length) {
                        events.pics = [];
                        if (events.pic.listUploaded.length === 0) {
                            events.pics.push({ ParentID: events.Model.ID, Type: 2, FileName: events.pic.list[0], PathType: events.pic.type });
                            return attachmentService.add(events.pics);
                        }
                    }
                    return true;
                }).then(() => {
                    if (events.video.list.length) {
                        events.videos = [];
                        if (events.video.listUploaded.length === 0) {
                            events.videos.push({ ParentID: events.Model.ID, Type: 2, FileName: events.video.list[0], PathType: events.video.type });
                            return attachmentService.add(events.videos);
                        }
                    }
                }).then((result) => {
                    events.pics = [];
                    events.videos = [];
                    return attachmentService.list({ ParentID: events.Model.ID });
                }).then((result) => {
                    events.pic.reset();
                    events.video.reset();
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === 8)
                                events.pic.listUploaded = [].concat(result[i]);
                            if (result[i].PathType === 7)
                                events.video.listUploaded = [].concat(result[i]);
                        }
                    }
                    events.grid.getlist(false);
                    toaster.pop('success', '', 'رویداد جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        events.main.changeState.cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        events.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function editEvents() {
            loadingService.show();
            return $q.resolve().then(() => {
                eventsService.edit(events.Model).then((result) => {
                    events.Model = angular.copy(result);
                    if (events.pic.list.length) {
                        events.pics = [];
                        if (events.pic.listUploaded.length === 0) {
                            events.pics.push({ ParentID: events.Model.ID, Type: 2, FileName: events.pic.list[0], PathType: events.pic.type });
                            return attachmentService.add(events.pics);
                        }
                    }
                    return true;
                }).then(() => {
                    if (events.video.list.length) {
                        events.videos = [];
                        if (events.video.listUploaded.length === 0) {
                            events.videos.push({ ParentID: events.Model.ID, Type: 2, FileName: events.video.list[0], PathType: events.video.type });
                            return attachmentService.add(events.videos);
                        }
                    }
                }).then((result) => {
                    events.pics = [];
                    events.videos = [];
                    return attachmentService.list({ ParentID: events.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0) {
                        for (var i = 0; i < result.length; i++) {
                            if (result[i].PathType === 8)
                                events.pic.listUploaded = [].concat(result[i]);
                            if (result[i].PathType === 7)
                                events.video.listUploaded = [].concat(result[i]);
                        }
                    }

                    events.pic.reset();
                    events.video.reset();
                    events.grid.getlist(false);
                    toaster.pop('success', '', 'رویداد جدید با موفقیت ویرایش گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        events.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }

        function fillDropCategory() {
            return categoryPortalService.list('dropdown').then((result) => {
                events.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentNode !== '/') {
                        events.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            $('.js-example-tags').empty();
            events.Model = {};
        }
        function clear() {
            loadingService.show();
            events.search.Model = {};
            events.search.searchPanel = false;
            events.grid.getlist();
            loadingService.hide();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentPortalController', commentPortalController);
    commentPortalController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption'];
    function commentPortalController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout, toolsService, enumService, froalaOption) {
        let comment = $scope;
        comment.Model = {};
        comment.Model.Errors = [];

        comment.search = [];
        comment.search.Model = {};
        comment.state = '';
        comment.main = {};
        comment.froalaOptionComment = angular.copy(froalaOption.comment);
        comment.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        comment.editComment = editComment;
        comment.changeDrop = changeDrop;
        comment.search.clear = clear;
        comment.search.Model = { ShowChildren: false, OnlyProduct:0 };
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'CreatorName', displayName: 'نام کاربر' },
            { name: 'ProductName', displayName: 'عنوان' },
            { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: enumService.CommentType },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => { return comment.search.Model; }
            , initLoad: true
        };

        comment.selectCommentType = toolsService.arrayEnum(enumService.CommentType);
        comment.selectCommentForType = toolsService.arrayEnum(enumService.CommentForType);
        comment.search.selectCommentForType = [];
        comment.selectCommentForType.map((item) => {
            if (item.Model !== 6)
                comment.search.selectCommentForType.push(item);
        })
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        commentService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'cartable';
                $location.path('/comment-portal/cartable');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'edit';
                comment.Model = model;
                $location.path(`/comment-portal/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }

        function editComment() {
            loadingService.show();
            return $q.resolve().then(() => {
                return commentService.edit(comment.Model)
            }).then((result) => {
                comment.grid.getlist(false);
                loadingService.hide();
                $timeout(function () {
                    toaster.pop('success', '', 'نظر ویرایش گردید');
                    cartable();

                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function changeDrop() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.grid.getlist(false);
            }).finally(loadingService.hide);

        }
        function clear() {
            loadingService.show();
            comment.search.Model = {};
            comment.search.searchPanel = false;
            comment.search.Model = { ShowChildren: false, OnlyProduct: 0 };
            comment.grid.getlist();
            loadingService.hide();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('paymentController', paymentController);
    paymentController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'paymentService', '$location', 'toaster', '$timeout'];
    function paymentController($scope, $q, loadingService, $routeParams, paymentService, $location, toaster, $timeout) {
        let payment = $scope;
        payment.Model = {};
        payment.Search = {};
        payment.state = '';
        payment.main = {};
        payment.main.changeState = {
            cartable: cartable,
            view: view
        }
        payment.print = print;
        payment.getExcel = getExcel;
        payment.grid = {
            bindingObject: payment
            , columns: [{ name: 'BuyerInfo', displayName: 'نام و نام خانوادگی خریدار' },
            { name: 'BuyerPhone', displayName: 'اطلاعات تماس' },
            { name: 'BankNameString', displayName: 'نام بانک' },
            { name: 'CountBuy', displayName: 'تعداد خرید' },
            { name: 'CreationDatePersian', displayName: 'تاریخ خرید' }
            ]
            , listService: paymentService.list
            , globalSearch: true
            , onEdit: payment.main.changeState.view
            , checkActionVisibility: (action) => {
                if (action === 'remove') {
                    return false;
                }
                else
                    return true;
            }
            , initLoad: true
        };
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'view':
                        payment.main.changeState.view($routeParams.id);
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                payment.state = 'cartable';
                $location.path('/payment/cartable');
            }).finally(loadingService.hide);
        }
        function view(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                if (model && model.ID)
                    return paymentService.getDetail(model.ID);
                else
                    return paymentService.getDetail($routeParams.id);
            }).then((result => {
                payment.Model = angular.copy(result);
                payment.state = 'view';
                $location.path(`/payment/view/${result.ID}`);
            }))
                .finally(loadingService.hide);
        }
        function print(elementId) {
            let toPrint = document.getElementById(elementId);
            let popupWin = window.open(
                "",
                "_blank",
                "width=1000,height=800,location=no,left=200px"
            );
            popupWin.document.open();
            popupWin.document.write(
                '<html dir="rtl"><link href="http://localhost:61837/Areas/Admin/Content/css/bootstrap-rtl/bootstrap-rtl.css" rel="stylesheet" /><link href="http://localhost:61837/Areas/Admin/Content/css/admin/admin.css" rel="stylesheet" /></head><body onload="window.print()">'
            );
            popupWin.document.write(toPrint.innerHTML);
            popupWin.document.write("</html>");
            popupWin.document.close();
        }
        function getExcel() {
            loadingService.show();
            return $q.resolve().then(() => {
                return paymentService.getExcel();
            }).then((result) => {
                window.location = `${window.location.origin}${result.FilePath}`;
                loadingService.hide();
            }).catch((error) => {
                toaster.pop('error', '', 'خطا در دریافت اطلاعات');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('notificationController', notificationController);
    notificationController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'notificationService', '$location'];
    function notificationController($scope, $q, loadingService, $routeParams, notificationService, $location) {
        let notification = $scope;
        notification.Model = {};
        notification.main = {};
        notification.state = '';
        notification.main.changeState = {
            cartable: cartable,
            view: view
        }
        init();
        notification.grid = {
            bindingObject: notification
            , columns: [{ name: 'Title', displayName: 'عنوان پیام' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' },
            { name: 'ReadDatePersian', displayName: 'تاریخ مشاهده' }]
            , listService: notificationService.list
            , onEdit: notification.main.changeState.view
            , globalSearch: true
            , searchBy: 'Title'
            , displayNameFormat: ['Title']
            , checkActionVisibility: (action, item) => {
                if (action === 'remove')
                    return false;
                if (action === 'edit')
                    return true;
            }
            , initLoad: true
        };
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'view':
                        notification.main.changeState.view($routeParams.id);
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            notification.state = 'cartable';
            $location.path('/notification/cartable');
        }
        function view(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                if (model && model.ID)
                    return notificationService.get(model.ID);
                else
                    return notificationService.get($routeParams.id);
            }).then((result) => {
                notification.Model = angular.copy(result);
                notification.state = 'view';
                $location.path(`notification/view/${notification.Model.ID}`);
            })
                .finally(loadingService.hide);
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    app.controller('departmentController', departmentController);
    departmentController.$inject = ['$scope', '$q', 'departmentService', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout', 'toolsService'];
    function departmentController($scope, $q, departmentService, loadingService, $routeParams, $location, toaster, $timeout, toolsService) {
        let department = $scope;
        department.Model = {};
        department.list = [];
        department.lists = [];
        department.state = 'cartable';
        department.goToPageAdd = goToPageAdd;
        department.addDepartment = addDepartment;
        department.addSubDepartment = addSubDepartment;
        department.editDepartment = editDepartment;
        department.changeState = {
            cartable: cartable
        }
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                    case 'edit':
                        departmentService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            department.tree = {
                data: []
                , colDefs: [
                    { field: 'Name', displayName: 'نام دستگاه' }
                    , {
                        field: ''
                        , displayName: ''
                        , cellTemplate: (
                            `<div class='pull-left'>
                            <i class='fa fa-plus tgrid-action pl-1 text-success' style='cursor:pointer;' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action pl-1 text-primary' style='cursor:pointer;' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                        </div>`)
                        , cellTemplateScope: {
                            edit: edit,
                            add: addSubDepartment,
                            //remove: remove
                        }
                    }
                ]
                , expandingProperty: {
                    field: "Title"
                    , displayName: "عنوان"
                }
            };
            departmentService.list().then((result) => {
                setTreeObject(result);
            });
            department.state = 'cartable';
            $location.path('department/cartable');
        }
        function edit(parent) {
            loadingService.show();
            return $q.resolve().then(() => {
                return departmentService.get(parent.ID)
            }).then((result) => {
                department.Model = result;
                return departmentService.listByNode({ Node: result.ParentNode });
            }).then((result) => {
                if (result && result.length > 0) {
                    department.Model.ParentID = result[0].ID;
                } else {
                    department.Model.ParentID = '00000000-0000-0000-0000-000000000000';
                }
                $location.path(`/department/edit/${result.ID}`);
                department.state = 'edit';

            }).finally(loadingService.hide)
        }
        function goToPageAdd(parent) {
            parent = parent || {};
            department.state = 'add';
            department.Model = { ParentID: parent.ID };
            $location.path('/department/add');
        }
        function addDepartment() {
            loadingService.show();
            $q.resolve().then(() => {
                departmentService.add(department.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editDepartment() {
            loadingService.show();
            $q.resolve().then(() => {
                departmentService.edit(department.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function addSubDepartment(parent) {
            department.state = 'add';
            goToPageAdd(parent);
        }
        function setTreeObject(departments) {
            departments.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            department.tree.data = toolsService.getTreeObject(departments, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return departmentService.remove(model.ID);
            }).then(() => {
                return departmentService.list();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    app.controller('userController', userController);
    userController.$inject = ['$scope', '$q', 'userService', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout'];
    function userController($scope, $q, userService, loadingService, $routeParams, $location, toaster, $timeout) {
        let user = $scope;
        user.Model = {};
        user.goToPageAdd = goToPageAdd;
        user.addUser = addUser;
        user.changeState = {
            cartable: cartable
        }
        user.grid = {
            bindingObject: user
            , columns: [{ name: 'FirstName', displayName: 'نام' },
            { name: 'LastName', displayName: 'نام خانوادگی' }]
            , listService: userService.list
            , globalSearch: true
            , checkActionVisibility: (action) => {
                if (action === 'edit') {
                    return false;
                }
                else
                    return true;
            }
            , initLoad: true
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            user.state = 'cartable';
            $location.path('user/cartable');
        }
        function goToPageAdd() {
            user.state = 'add';
            $location.path('/user/add');
        }
        function addUser() {
            loadingService.show();
            $q.resolve().then(() => {
                userService.add(user.Model).then((result) => {
                    toaster.pop('success', '', 'کاربر جدید با موفقیت اضافه گردید');
                    user.grid.getlist();
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return userService.remove(model.ID);
            }).then(() => {
                return user.grid.getlist();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------
    app.controller('contactController', contactController);
    contactController.$inject = ['$scope', 'contactService', 'loadingService'];
    function contactController($scope, contactService, loadingService) {
        let contact = $scope;
        contact.Model = {};
        contact.grid = {
            bindingObject: contact
            , columns: [{ name: 'FirstName', displayName: 'نام' },
            { name: 'LastName', displayName: 'نام خانوادگی' },
            { name: 'Title', displayName: 'موضوع پیام' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: contactService.list
            , deleteService: contactService.remove
            , globalSearch: true
            , onEdit: show
            , initLoad: true
            , displayNameFormat: (selected) => {
                return `پیام ${selected.FirstName} ${selected.LastName}`;
            }
        };
        function show(selected) {
            loadingService.show();
            contact.Model = selected;
            $('#contact-modal').modal('show');
            loadingService.hide();
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    app.controller('pagesPortalController', pagesPortalController);
    pagesPortalController.$inject = ['$scope', '$q', 'loadingService', 'pagesPortalService', 'toolsService', 'enumService', 'toaster'];
    function pagesPortalController($scope, $q, loadingService, pagesPortalService, toolsService, enumService, toaster) {
        let pages = $scope;
        pages.Model = {};
        pages.main = {};
        pages.search = [];
        pages.search.Model = {};

        pages.addPages = addPages;
        pages.editPages = editPages;
        pages.search.clear = clear;
        pages.main.changeState = {
            edit: edit,
            add: add
        }
        pages.pageType = toolsService.arrayEnum(enumService.PageType);
        pages.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        pages.grid = {
            bindingObject: pages
            , columns: [{ name: 'Name', displayName: 'نام صفحه' },
            { name: 'PageType', displayName: 'نوع صفحه', type: 'enum', source: enumService.PageType },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: pagesPortalService.list
            , deleteService: pagesPortalService.remove
            , globalSearch: true
            , onEdit: pages.main.changeState.edit
            , initLoad: true
            , displayNameFormat: ['Name']
            , searchBy: 'Name'
            , options: () => {
                return pages.search.Model;
            }
        };
        function add() {
            loadingService.show();
            $('#pages-portal-modal').modal('show');
            pages.Model = {};
            pages.state = 'add';
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.state = 'edit';
                pages.Model = model;
                $('#pages-portal-modal').modal('show');
            }).finally(loadingService.hide);

        }
        function addPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesPortalService.add(pages.Model);
            }).then((result) => {
                toaster.pop('success', '', 'صفحه جدید با موفقیت اضافه گردید');
                pages.Model = {};
                pages.grid.getlist();
                $('#pages-portal-modal').modal('hide');
                loadingService.hide();
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesPortalService.edit(pages.Model)
            }).then((result) => {
                toaster.pop('success', '', 'صفحه جدید با موفقیت ویرایش گردید');
                pages.Model = {};
                pages.grid.getlist();
                $('#pages-portal-modal').modal('hide');
                loadingService.hide();
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function clear() {
            loadingService.show();
            pages.search.Model = {};
            pages.search.searchPanel = false;
            pages.grid.getlist();
            loadingService.hide();
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('dynamicPageController', dynamicPageController);
    dynamicPageController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'dynamicPageService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption', 'pagesPortalService', 'attachmentService'];
    function dynamicPageController($scope, $q, loadingService, $routeParams, dynamicPageService, $location, toaster, $timeout, toolsService, enumService, froalaOption, pagesPortalService, attachmentService) {
        let pages = $scope;
        pages.Model = {};
        pages.main = {};
        pages.search = {};
        pages.search.Model = {};
        pages.Model.Errors = [];
        pages.pic = { type: '1', allowMultiple: false, validTypes: 'image/jpeg' };
        pages.pic.list = [];
        pages.pic.listUploaded = [];

        pages.state = '';
        pages.addDynamicPage = addDynamicPage;
        pages.editDynamicPage = editDynamicPage;
        pages.search.clear = clear;
        pages.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        init();
        pages.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        pages.grid = {
            bindingObject: pages
            , columns: [{ name: 'Name', displayName: 'نام صفحه' },
            { name: 'TrackingCode', displayName: 'کد پیگیری' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: dynamicPageService.list
            , deleteService: dynamicPageService.remove
            , onAdd: pages.main.changeState.add
            , onEdit: pages.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Name']
            , initLoad: true
            , options: () => { return pages.search.Model }
        };
        pages.froalaOption = angular.copy(froalaOption.main);
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesPortalService.list({ PageType: 1 });
            }).then((result) => {
                if (result.length > 0) {
                    pages.listPages = [];
                    for (var i = 0; i < result.length; i++) {
                        pages.listPages.push({ Name: result[i].Name, Model: result[i].ID });
                    }
                }
            }).then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        dynamicPageService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            $('.js-example-tags').empty();
            clearModel();
            pages.state = 'cartable';
            $location.path('/dynamic-page/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            pages.state = 'add';
            $location.path('/dynamic-page/add');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return dynamicPageService.get(model.ID);
            }).then((model) => {
                pages.Model = model;
                return attachmentService.list({ ParentID: pages.Model.ID });
            }).then((result) => {
                pages.pic.reset();
                if (result && result.length > 0)
                    pages.pic.listUploaded = [].concat(result);
                if (pages.Model.Tags !== null && pages.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < pages.Model.Tags.length; i++) {
                        newOption.push(new Option(pages.Model.Tags[i], pages.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }
                pages.state = 'edit';
                $location.path(`/dynamic-page/edit/${pages.Model.ID}`);
            }).finally(loadingService.hide);
        }

        function addDynamicPage() {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.Model.PageID = pages.Search.Model.PageID;
                return dynamicPageService.add(pages.Model);
            }).then((result) => {
                pages.Model = result;
                if (pages.pic.list.length) {
                    pages.pics = [];
                    if (pages.pic.listUploaded.length === 0) {
                        pages.pics.push({ ParentID: pages.Model.ID, Type: 2, FileName: pages.pic.list[0], PathType: pages.pic.type });
                    }
                    return attachmentService.add(pages.pics);
                }
                return true;
            }).then(() => {
                pages.pic.reset();
                pages.grid.getlist(false);
                toaster.pop('success', '', 'صفحه جدید با موفقیت اضافه گردید');
                pages.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#dynamicPageSection').offset().top - $('#dynamicPageSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    pages.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#dynamicPageSection').offset().top - $('#dynamicPageSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editDynamicPage() {
            loadingService.show();
            return $q.resolve().then(() => {
                return dynamicPageService.edit(pages.Model);
            }).then((result) => {
                pages.Model = result;
                if (pages.pic.list.length) {
                    pages.pics = [];
                    if (pages.pic.listUploaded.length === 0) {
                        pages.pics.push({ ParentID: pages.Model.ID, Type: 2, FileName: pages.pic.list[0], PathType: pages.pic.type });
                    }
                    return attachmentService.add(pages.pics);
                }
                return true;
            }).then(() => {
                pages.grid.getlist(false);
                toaster.pop('success', '', 'صفحه با موفقیت ویرایش گردید');
                pages.pic.reset();
                pages.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#dynamicPageSection').offset().top - $('#dynamicPageSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    pages.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#dynamicPageSection').offset().top - $('#dynamicPageSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function clearModel() {
            pages.Model = {};
        }
        function clear() {
            loadingService.show();
            pages.search.Model = {};
            pages.search.searchPanel = false;
            pages.grid.getlist();
            loadingService.hide();
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    app.controller('linkController', linkController);
    linkController.$inject = ['$scope', '$q', 'loadingService', 'linkService', 'toolsService', 'enumService', 'toaster'];
    function linkController($scope, $q, loadingService, linkService, toolsService, enumService, toaster) {
        let link = $scope;
        link.Model = {};
        link.Model.Errors = [];
        link.main = {};

        link.addLink = addLink;
        link.editLink = editLink;
        link.main.changeState = {
            edit: edit,
            add: add
        }
        link.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        link.grid = {
            bindingObject: link
            , columns: [{ name: 'Name', displayName: 'نام لینک' },
            { name: 'Url', displayName: 'آدرس' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: linkService.list
            , deleteService: linkService.remove
            , globalSearch: true
            , onEdit: link.main.changeState.edit
            , initLoad: true
            , displayNameFormat: ['Name'],
            searchBy: 'Name'
        };
        function add() {
            loadingService.show();
            $('#link-modal').modal('show');
            link.Model = {};
            link.state = 'add';
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                link.state = 'edit';
                link.Model = model;
                $('#link-modal').modal('show');
            }).finally(loadingService.hide);

        }
        function addLink() {
            loadingService.show();
            return $q.resolve().then(() => {
                return linkService.add(link.Model);
            }).then((result) => {
                toaster.pop('success', '', 'پیوند جدید با موفقیت اضافه گردید');
                link.Model = {};
                link.grid.getlist();
                $('#link-modal').modal('hide');
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#linkSection').offset().top - $('#linkSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    link.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#link-modal').offset().top - $('#link-modal').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editLink() {
            loadingService.show();
            return $q.resolve().then(() => {
                return linkService.edit(link.Model)
            }).then((result) => {
                toaster.pop('success', '', 'صفحه جدید با موفقیت ویرایش گردید');
                link.Model = {};
                link.grid.getlist();
                $('#link-modal').modal('hide');
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#linkSection').offset().top - $('#linkSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    link.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#linkSection').offset().top - $('#linkSection').offsetParent().offset().top
                    }, 'slow');
                }
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    app.controller('staticPageController', staticPageController);
    staticPageController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'staticPageService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption', 'attachmentService', 'bannerService'];
    function staticPageController($scope, $q, loadingService, $routeParams, staticPageService, $location, toaster, $timeout, toolsService, enumService, froalaOption, attachmentService, bannerService) {
        let pages = $scope;
        pages.Model = {};
        pages.main = {};
        pages.Model.Errors = [];
        pages.pic = { type: '1', allowMultiple: false, validTypes: 'image/jpeg' };
        pages.pic.list = [];
        pages.pic.listUploaded = [];
        pages.search = [];
        pages.search.Model = {};

        pages.state = '';
        pages.editStaticPage = editStaticPage;
        pages.changeDrop = changeDrop;
        pages.search.clear = clear;
        pages.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        pages.bannerShow = toolsService.arrayEnum(enumService.EnableMenuType);
        init();
        pages.main.changeState = {
            edit: edit,
            cartable: cartable
        }
        pages.grid = {
            bindingObject: pages
            , columns: [{ name: 'Name', displayName: 'نام صفحه' },
            { name: 'TrackingCode', displayName: 'کد پیگیری' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: staticPageService.list
            , deleteService: staticPageService.remove
            , onAdd: pages.main.changeState.add
            , onEdit: pages.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Name']
            , initLoad: true
            , options: () => {
                return pages.search.Model;
            }
        };
        pages.froalaOption = angular.copy(froalaOption.main);
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        staticPageService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            $('.js-example-tags').empty();
            clearModel();
            pages.state = 'cartable';
            $location.path('/static-page/cartable');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return staticPageService.get(model.ID);
            }).then((model) => {
                pages.Model = model;
                return attachmentService.list({ ParentID: pages.Model.ID });
            }).then((result) => {
                pages.pic.reset();
                if (result && result.length > 0)
                    pages.pic.listUploaded = [].concat(result);
                if (pages.Model.Tags !== null && pages.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < pages.Model.Tags.length; i++) {
                        newOption.push(new Option(pages.Model.Tags[i], pages.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }
                return changeDrop();
            }).then(() => {
                pages.state = 'edit';
                $location.path(`/static-page/edit/${pages.Model.ID}`);
            }).finally(loadingService.hide);
        }

        function editStaticPage() {
            loadingService.show();
            return $q.resolve().then(() => {
                return staticPageService.edit(pages.Model);
            }).then((result) => {
                pages.Model = result;
                if (pages.pic.list.length) {
                    pages.pics = [];
                    if (pages.pic.listUploaded.length === 0) {
                        pages.pics.push({ ParentID: pages.Model.ID, Type: 2, FileName: pages.pic.list[0], PathType: pages.pic.type });
                    }
                    return attachmentService.add(pages.pics);
                }
                return true;
            }).then(() => {
                pages.grid.getlist(false);
                toaster.pop('success', '', 'صفحه با موفقیت ویرایش گردید');
                pages.pic.reset();
                pages.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#staticPageSection').offset().top - $('#staticPageSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    pages.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#staticPageSection').offset().top - $('#staticPageSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function clearModel() {
            pages.Model = {};
        }
        function changeDrop() {
            loadingService.show();
            if (pages.Model.BannerShow === 1) {
                return $q.resolve().then(() => {
                    return bannerService.list({ bannerType: 1 });
                }).then((result) => {
                    if (result.length > 0) {
                        pages.bannerList = [];
                        for (var i = 0; i < result.length; i++) {
                            pages.bannerList.push({ Name: result[i].Name, Model: result[i].ID });
                        }
                    }
                }).catch(() => {
                    loadingService.hide();
                }).finally(loadingService.hide)
            }
            loadingService.hide();
        }
        function clear() {
            loadingService.show();
            pages.search.Model = {};
            pages.search.searchPanel = false;
            pages.grid.getlist();
            loadingService.hide();
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('bannerController', bannerController);
    bannerController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'bannerService', '$location', 'toaster', 'toolsService', 'enumService', 'attachmentService'];
    function bannerController($scope, $q, loadingService, $routeParams, bannerService, $location, toaster, toolsService, enumService, attachmentService) {
        let banner = $scope;
        banner.Model = {};
        banner.main = {};
        banner.search = {};
        banner.search.Model = {};
        banner.Model.Errors = [];
        banner.pic = { type: '2', allowMultiple: true, validTypes: 'image/jpeg' };
        banner.pic.list = [];
        banner.pic.listUploaded = [];

        banner.state = '';
        banner.addBanner = addBanner;
        banner.editBanner = editBanner;
        banner.search.clear = clear;
        banner.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        banner.bannerType = toolsService.arrayEnum(enumService.BannerType);
        banner.search.bannerType = toolsService.arrayEnum(enumService.BannerType);
        init();
        banner.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        banner.grid = {
            bindingObject: banner
            , columns: [{ name: 'Name', displayName: 'نام بنر' },
            { name: 'BannerType', displayName: 'نوع بنر', type: 'enum', source: enumService.BannerType },
            { name: 'Enabled', displayName: 'فعال/غیرفعال', type: 'enum', source: enumService.EnableMenuType },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }
            ]
            , listService: bannerService.list
            , deleteService: bannerService.remove
            , onAdd: banner.main.changeState.add
            , onEdit: banner.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Name']
            , initLoad: true
            , options: () => {
                return banner.search.Model;
            }
        };
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        bannerService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            clearModel();
            banner.state = 'cartable';
            $location.path('/banner/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            clearModel();
            banner.state = 'add';
            $location.path('/banner/add');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return bannerService.get(model.ID);
            }).then((model) => {
                banner.Model = model;
                return attachmentService.list({ ParentID: banner.Model.ID });
            }).then((result) => {
                banner.pic.reset();
                if (result && result.length > 0)
                    banner.pic.listUploaded = [].concat(result);
                banner.state = 'edit';
                $location.path(`/banner/edit/${banner.Model.ID}`);
            }).finally(loadingService.hide);
        }

        function addBanner() {
            loadingService.show();
            return $q.resolve().then(() => {
                return bannerService.add(banner.Model);
            }).then((result) => {
                banner.Model = result;
                if (banner.pic.list.length) {
                    banner.pics = [];
                    if (banner.pic.listUploaded.length === 0) {
                        for (var i = 0; i < banner.pic.list.length; i++) {
                            banner.pics.push({ ParentID: banner.Model.ID, Type: 2, FileName: banner.pic.list[i], PathType: banner.pic.type });
                        }
                    }
                    return attachmentService.add(banner.pics);
                }
                return true;
            }).then(() => {
                banner.grid.getlist(false);
                toaster.pop('success', '', 'بنر با موفقیت اضافه گردید');
                banner.pic.reset();
                banner.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#bannerSection').offset().top - $('#bannerSection').offsetParent().offset().top
                    }, 'slow');
                    toaster.pop('error', '', error || 'خطایی اتفاق افتاده است');
                } else {
                    var listError = error.split('&&');
                    banner.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#bannerSection').offset().top - $('#bannerSection').offsetParent().offset().top
                    }, 'slow');
                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }


            }).finally(loadingService.hide);
        }
        function editBanner() {
            loadingService.show();
            return $q.resolve().then(() => {
                return bannerService.edit(banner.Model);
            }).then((result) => {
                banner.Model = result;
                if (banner.pic.list.length) {
                    banner.pics = [];
                    for (var i = 0; i < banner.pic.list.length; i++) {
                        banner.pics.push({ ParentID: banner.Model.ID, Type: 2, FileName: banner.pic.list[i], PathType: banner.pic.type });
                    }
                    return attachmentService.add(banner.pics);
                }
                return true;
            }).then(() => {
                banner.grid.getlist(false);
                toaster.pop('success', '', 'بنر با موفقیت ویرایش گردید');
                banner.pic.reset();
                debugger
                banner.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#bannerSection').offset().top - $('#bannerSection').offsetParent().offset().top
                    }, 'slow');
                    toaster.pop('error', '', error || 'خطایی اتفاق افتاده است');
                } else {
                    var listError = error.split('&&');
                    banner.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#bannerSection').offset().top - $('#bannerSection').offsetParent().offset().top
                    }, 'slow');
                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }


            }).finally(loadingService.hide);
        }
        function clearModel() {
            banner.Model = {};
            banner.pic.listUploaded = [];
        }
        function clear() {
            loadingService.show();
            banner.search.Model = {};
            banner.search.searchPanel = false;
            banner.grid.getlist();
            loadingService.hide();
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('galleryController', galleryController);
    galleryController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'galleryService', '$location', 'toaster', 'toolsService', 'enumService', 'attachmentService'];
    function galleryController($scope, $q, loadingService, $routeParams, galleryService, $location, toaster, toolsService, enumService, attachmentService) {
        let gallery = $scope;
        gallery.Model = {};
        gallery.main = {};
        gallery.Model.Errors = [];

        gallery.pic = { type: '11', allowMultiple: true, validTypes: 'image/jpeg' };
        gallery.pic.list = [];
        gallery.pic.listUploaded = [];

        gallery.search = [];
        gallery.search.Model = {};

        gallery.state = '';
        gallery.addGallery = addGallery;
        gallery.editGallery = editGallery;
        gallery.search.clear = clear;
        gallery.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        init();
        gallery.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        gallery.grid = {
            bindingObject: gallery
            , columns: [{ name: 'Name', displayName: 'نام گالری' },
            { name: 'Enabled', displayName: 'فعال/غیرفعال', type: 'enum', source: enumService.EnableMenuType },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }
            ]
            , listService: galleryService.list
            , deleteService: galleryService.remove
            , onAdd: gallery.main.changeState.add
            , onEdit: gallery.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Name']
            , initLoad: true
            , options: () => {
                return gallery.search.Model;
            }
        };
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        galleryService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            loadingService.show();
            clearModel();
            gallery.state = 'cartable';
            $location.path('/gallery/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            clearModel();
            gallery.state = 'add';
            $location.path('/gallery/add');
            loadingService.hide();
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return galleryService.get(model.ID);
            }).then((model) => {
                gallery.Model = model;
                if (gallery.Model.Tags !== null && gallery.Model.Tags.length > 0) {
                    var newOption = [];
                    for (var i = 0; i < gallery.Model.Tags.length; i++) {
                        newOption.push(new Option(gallery.Model.Tags[i], gallery.Model.Tags[i], false, true));
                    }
                    $timeout(() => {
                        $('.js-example-tags').append(newOption).trigger('change');
                    }, 0);
                }
                return attachmentService.list({ ParentID: gallery.Model.ID });
            }).then((result) => {
                gallery.pic.reset();
                if (result && result.length > 0)
                    gallery.pic.listUploaded = [].concat(result);
                gallery.state = 'edit';
                $location.path(`/gallery/edit/${gallery.Model.ID}`);
            }).finally(loadingService.hide);
        }

        function addGallery() {
            loadingService.show();
            return $q.resolve().then(() => {
                return galleryService.add(gallery.Model);
            }).then((result) => {
                gallery.Model = result;

                if (gallery.pic.list.length) {
                    gallery.pics = [];
                    if (gallery.pic.listUploaded && gallery.pic.listUploaded.length === 0) {
                        gallery.pics.push({ ParentID: gallery.Model.ID, Type: 1, FileName: gallery.pic.list[0], PathType: gallery.pic.type });

                        for (var i = 1; i < gallery.pic.list.length; i++) {
                            gallery.pics.push({ ParentID: gallery.Model.ID, Type: 2, FileName: gallery.pic.list[i], PathType: gallery.pic.type });
                        }
                    } else {
                        for (var i = 0; i < gallery.pic.list.length; i++) {
                            gallery.pics.push({ ParentID: gallery.Model.ID, Type: 2, FileName: gallery.pic.list[i], PathType: gallery.pic.type });
                        }
                    }
                    return attachmentService.add(gallery.pics);
                }
                return true;
            }).then((result) => {
                gallery.pics = [];
                return attachmentService.list({ ParentID: gallery.Model.ID });
            }).then((result) => {
                gallery.pic.reset();
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        gallery.pic.listUploaded.push(result[i]);
                    }
                }
                gallery.grid.getlist();
                gallery.main.changeState.cartable();
                toaster.pop('success', '', 'گالری جدید با موفقیت اضافه گردید');
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#GallerySection').offset().top - $('#GallerySection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    gallery.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#GallerySection').offset().top - $('#GallerySection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editGallery() {
            loadingService.show();
            return $q.resolve().then(() => {
                return galleryService.edit(gallery.Model);
            }).then((result) => {
                gallery.Model = result;
                if (gallery.pic.list.length) {
                    gallery.pics = [];
                    if (gallery.pic.listUploaded && gallery.pic.listUploaded.length === 0) {
                        gallery.pics.push({ ParentID: gallery.Model.ID, Type: 1, FileName: gallery.pic.list[0], PathType: gallery.pic.type });

                        for (var i = 1; i < gallery.pic.list.length; i++) {
                            gallery.pics.push({ ParentID: gallery.Model.ID, Type: 2, FileName: gallery.pic.list[i], PathType: gallery.pic.type });
                        }
                    } else {
                        for (var i = 0; i < gallery.pic.list.length; i++) {
                            gallery.pics.push({ ParentID: gallery.Model.ID, Type: 2, FileName: gallery.pic.list[i], PathType: gallery.pic.type });
                        }
                    }
                    return attachmentService.add(gallery.pics);
                }
                return true;
            }).then((result) => {
                gallery.pics = [];
                return attachmentService.list({ ParentID: gallery.Model.ID });
            }).then((result) => {
                gallery.pic.reset();
                if (result && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        gallery.pic.listUploaded.push(result[i]);
                    }
                }
                toaster.pop('success', '', 'گالری جدید با موفقیت ویرایش گردید');
                gallery.grid.getlist();
                gallery.main.changeState.cartable();
                loadingService.hide();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#GallerySection').offset().top - $('#GallerySection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    gallery.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#GallerySection').offset().top - $('#GallerySection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function clearModel() {
            $('.js-example-tags').empty();
            gallery.Model = {};
            gallery.pic.listUploaded = [];
        }
        function clear() {
            loadingService.show();
            gallery.search.Model = {};
            gallery.search.searchPanel = false;
            gallery.grid.getlist();
            loadingService.hide();
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('shippingCostController', shippingCostController);
    shippingCostController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'shippingCostService', '$location', 'toaster', 'toolsService', 'enumService'];
    function shippingCostController($scope, $q, loadingService, $routeParams, shippingCostService, $location, toaster, toolsService, enumService) {
        let shipping = $scope;
        shipping.Model = {};
        shipping.main = {};
        shipping.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        };
        shipping.Errors = [];
        shipping.search = [];
        shipping.search.Model = {};
        shipping.state = '';
        shipping.enableType = toolsService.arrayEnum(enumService.EnableMenuType);
        shipping.search.selectYesOrNoType = toolsService.arrayEnum(enumService.YesOrNoType);
        shipping.search.selectYesOrNoType.map((item) => {
            if (item.Name === 'خیر')
                item.Model = 0;
        })
        shipping.addShippingCost = addShippingCost;
        shipping.editShippingCost = editShippingCost;
        shipping.search.clear = clear;
        shipping.grid = {
            bindingObject: shipping
            , columns: [{ name: 'Name', displayName: 'نام' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: shippingCostService.list
            , onEdit: shipping.main.changeState.edit
            , globalSearch: true
            , initLoad: true
            , options: () => {
                return shipping.search.Model;
            }
        };
        init();


        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        shippingCostService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            shipping.Model = {};
            shipping.state = 'cartable';
            $location.path('/shipping-cost/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            shipping.state = 'add';
            $location.path('/shipping-cost/add');
            loadingService.hide();

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                shipping.state = 'edit';
                shipping.Model = model;
                $location.path(`/shipping-cost/edit/${shipping.Model.ID}`);
            }).finally(loadingService.hide);

        }

        function addShippingCost() {
            loadingService.show();
            return $q.resolve().then(() => {
                return shippingCostService.add(shipping.Model)
            }).then((result) => {
                shipping.grid.getlist(false);
                toaster.pop('success', '', 'با موفقیت اضافه گردید');
                loadingService.hide();
                shipping.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#ShippingCostSection').offset().top - $('#ShippingCostSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    shipping.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#ShippingCostSection').offset().top - $('#ShippingCostSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editShippingCost() {
            loadingService.show();
            return $q.resolve().then(() => {
                return shippingCostService.edit(shipping.Model)
            }).then((result) => {
                shipping.grid.getlist(false);
                toaster.pop('success', '', 'با موفقیت ویرایش گردید');
                loadingService.hide();
                shipping.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#ShippingCostSection').offset().top - $('#ShippingCostSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    shipping.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#ShippingCostSection').offset().top - $('#ShippingCostSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function clear() {
            loadingService.show();
            shipping.search.Model = {};
            shipping.search.searchPanel = false;
            shipping.grid.getlist();
            loadingService.hide();
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('deliveryDateController', deliveryDateController);
    deliveryDateController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'deliveryDateService', '$location', 'toaster', 'toolsService', 'enumService'];
    function deliveryDateController($scope, $q, loadingService, $routeParams, deliveryDateService, $location, toaster, toolsService, enumService) {
        let delivery = $scope;
        delivery.Model = {};
        delivery.main = {};
        delivery.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        };
        delivery.Errors = [];
        delivery.state = '';
        delivery.search = [];
        delivery.search.Model = {};
        delivery.enableType = toolsService.arrayEnum(enumService.EnableMenuType);

        delivery.addDeliveryDate = addDeliveryDate;
        delivery.editDeliveryDate = editDeliveryDate;
        delivery.search.clear = clear;
        delivery.grid = {
            bindingObject: delivery
            , columns: [{ name: 'Name', displayName: 'نام' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' }]
            , listService: deliveryDateService.list
            , onEdit: delivery.main.changeState.edit
            , globalSearch: true
            , initLoad: true
            , options: () => {
                return delivery.search.Model;
            }
        };
        init();


        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        deliveryDateService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            delivery.Model = {};
            delivery.state = 'cartable';
            $location.path('/delivery-date/cartable');
            loadingService.hide();
        }
        function add() {
            loadingService.show();
            delivery.Model = {};
            delivery.state = 'add';
            $location.path('/delivery-date/add');
            loadingService.hide();

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                delivery.state = 'edit';
                delivery.Model = model;
                $location.path(`/delivery-date/edit/${delivery.Model.ID}`);
            }).finally(loadingService.hide);

        }

        function addDeliveryDate() {
            loadingService.show();
            return $q.resolve().then(() => {
                return deliveryDateService.add(delivery.Model)
            }).then((result) => {
                delivery.grid.getlist(false);
                toaster.pop('success', '', 'با موفقیت اضافه گردید');
                loadingService.hide();
                delivery.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#DeliveryDateSection').offset().top - $('#DeliveryDateSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    delivery.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#DeliveryDateSection').offset().top - $('#DeliveryDateSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editDeliveryDate() {
            loadingService.show();
            return $q.resolve().then(() => {
                return deliveryDateService.edit(delivery.Model)
            }).then((result) => {
                delivery.grid.getlist(false);
                toaster.pop('success', '', 'با موفقیت ویرایش گردید');
                loadingService.hide();
                delivery.main.changeState.cartable();
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#DeliveryDateSection').offset().top - $('#DeliveryDateSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    delivery.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#DeliveryDateSection').offset().top - $('#DeliveryDateSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function clear() {
            loadingService.show();
            delivery.search.Model = {};
            delivery.search.searchPanel = false;
            delivery.grid.getlist();
            loadingService.hide();
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('salesController', salesController);
    salesController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'salesService', '$location', 'toaster', 'documentFlowService', 'paymentService', 'enumService', 'paymentService'];
    function salesController($scope, $q, loadingService, $routeParams, salesService, $location, toaster, documentFlowService, paymentService, enumService, paymentService) {
        let sales = $scope;
        sales.Model = {};
        sales.main = {};
        sales.Search = {};
        sales.Payment = [];
        sales.Payment.Model = {};
        sales.Flow = [];
        sales.Flow.Model = {};
        sales.Search.Model = {};
        sales.Search.Model.ActionState = 1;
        sales.main.state = '';
        sales.main.changeState = {
            cartable: cartable,
            view: view
        }
        sales.grid = {
            bindingObject: sales
            , columns: [{ name: 'BuyerInfo', displayName: 'نام و نام خانوادگی خریدار' },
            { name: 'BuyerPhone', displayName: 'اطلاعات تماس' },
            { name: 'Price', displayName: 'مبلغ' },
            { name: 'LastDocState', displayName: 'آخرین وضعیت', type: 'enum', source: enumService.SalesDocState },
            ]
            , listService: salesService.list
            , globalSearch: true
            , initLoad: true
            , options: () => { return sales.Search.Model }
            , actions:
                [
                    {
                        class: 'fa fa-pencil text-info mr-2 cursor-grid operation-icon'
                        , name: 'edit'
                        , title: 'مشاهده'
                        , onclick: (selected) => {
                            loadingService.show();
                            return $q.resolve().then(() => {
                                return documentFlowService.setAsRead(selected.ID);
                            }).then(() => {
                                loadingService.hide();
                                return sales.main.changeState.view(selected);
                            }).finally(loadingService.hide);
                        }
                    }
                ]
        };
        sales.Flow.grid = {
            columns: [
                { name: 'FromUserFullName', displayName: 'ارجاع دهنده' }
                , { name: 'ToUserFullName', displayName: 'گیرنده' }
                , { name: 'DatePersian', displayName: 'تاریخ' }
                , { name: 'ActionDatePersian', displayName: 'تاریخ اقدام' }
                , { name: 'ToDocState', displayName: 'آخرین وضعیت', type: 'enum', source: enumService.SalesDocState }
                , { name: 'SendType', displayName: 'نوع ارجاع', type: 'enum', source: enumService.SendDocumentType }
            ]
            , listService: salesService.listFlow
            , options: () => { return $routeParams.id }
            , actions: [{
                title: 'توضیحات', name: 'comment', class: 'fa fa-comment-o grid-action-blue', onclick: function (selected) {
                    sales.Flow.comment = selected.Comment;
                    sales.Flow.selectedID = selected.ID;
                    $(`#flow-comment-modal-${sales.Flow.selectedID}`).modal('show');
                }
            }]
        }
        sales.Flow.confirm = confirm;
        sales.getExcel = getExcel;
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        sales.main.changeState.cartable();
                        break;
                    case 'view':
                        sales.main.changeState.view({ ID: $routeParams.id });
                        break;
                }
            }).finally(loadingService.hide);
        }

        function cartable() {
            loadingService.show();
            sales.main.state = 'cartable';
            $location.path('/sales/cartable');
            loadingService.hide();
        }
        function view(selected) {
            loadingService.show();
            return $q.resolve().then(() => {
                if (selected && selected.ID)
                    return salesService.get(selected.ID);
            }).then((result) => {
                sales.Model = angular.copy(result);
                $location.path(`/sales/view/${sales.Model.ID}`)
                return paymentService.getDetail(result.PaymentID);
            }).then((result) => {
                sales.Payment.Model = angular.copy(result);
                return sales.Flow.grid.getlist();
            }).then(() => {
                sales.main.state = 'view';
                loadingService.hide();
            }).finally(loadingService.hide);
        }

        function confirm(state) {
            loadingService.show();
            return $q.resolve().then(() => {
                sales.Flow.Model.DocumentID = $routeParams.id;
                switch (state) {
                    case 'confirm':
                        return salesService.confirm(sales.Flow.Model);
                        break;
                }
            }).then(() => {
                $('#sales-modal').modal('hide');
                sales.grid.getlist();
                sales.main.changeState.cartable();
            })

                .finally(loadingService.hide);
        }
        function getExcel() {
            loadingService.show();
            return $q.resolve().then(() => {
                return paymentService.getExcel();
            }).then((result) => {
                window.location = `${window.location.origin}${result.FilePath}`;
                loadingService.hide();
            }).catch((error) => {
                toaster.pop('error', '', 'خطا در دریافت اطلاعات');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
    }

})();