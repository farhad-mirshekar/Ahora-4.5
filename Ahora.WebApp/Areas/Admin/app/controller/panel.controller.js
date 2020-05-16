
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
            , initLoad:true
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
    profileController.$inject = ['$scope', 'profileService'];
    function profileController($scope, profileService) {
        let profile = $scope;

        init();

        function init() {
            var id = localStorage.userid;
            return profileService.get(id).then((result) => {
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
    positionController.$inject = ['$scope', '$q', '$timeout', '$routeParams', '$location', 'toaster', '$timeout', 'loadingService', 'positionService', 'toaster', 'profileService', 'roleService', 'departmentService'];
    function positionController($scope, $q, $timeout, $routeParams, $location, toaster, $timeout, loadingService, positionService, toaster, profileService, roleService, departmentService) {
        let position = $scope;
        position.department = $scope;
        position.department.Model = {};
        position.department.departmentDropDown = [];
        position.changeState = {
            cartable: cartable,
            add: add
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
        position.departmentChange = departmentChange;
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
            roleService.list().then((result) => {
                position.listRole = [].concat(result);
            })
        }
        function addPosition() {
            loadingService.show();
            $q.resolve().then(() => {
                if (!position.ResultSearch.Enabled) {
                    toaster.pop('error', '', 'کاربری را جست و جو نکردید');
                    return false;
                } else {
                    position.Model.UserID = position.ResultSearch.ID;
                    return true;
                }
            }).then((state) => {
                if (state) {
                    var json = '';
                    if (position.selectCommand.selected.length === 0) {
                        toaster.pop('error', '', 'نقشی انتخاب نکردید');
                        return false;
                    } else {
                        for (var i = 0; i < position.selectCommand.selected.length; i++) {
                            json += position.selectCommand.selected[i] + ',';
                        }
                        position.Model.Json = json;
                        return true;
                    }
                }
            }).then((state) => {
                if (state) {
                    positionService.add(position.Model).then((result) => {
                        toaster.pop('success', '', 'با موفقیت ذخیره گردید');
                    })
                }
            }).finally(loadingService.hide);
        }

        function departmentChange() {
            loadingService.show();
            return $q.resolve().then(() => {
                return positionService.list({ DepartmentID: position.department.Model.DepartmentID });
            }).then((result) => {
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
    productController.$inject = ['$scope', '$routeParams', 'loadingService', '$q', 'toaster', '$location', 'categoryService', 'productService', 'attachmentService', 'attributeService', 'productMapattributeService', 'productVariantattributeService', 'discountService', 'toolsService', 'enumService', 'froalaOption'];
    function productController($scope, $routeParams, loadingService, $q, toaster, $location, categoryService, productService, attachmentService, attributeService, productMapattributeService, productVariantattributeService, discountService, toolsService, enumService, froalaOption) {
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
            product.state = 'add';
            categoryType();
            $location.path('/product/add');
            loadingService.hide();
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
                product.pic.reset();
                product.file.reset();
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
            return categoryService.list().then((result) => {
                var list = [].concat(result);
                product.categoryType = [];
                for (var i = 0; i < list.length; i++) {
                    if (list[i].ParentID !== '00000000-0000-0000-0000-000000000000')
                        product.categoryType.push({ Model: list[i].ID, Name: list[i].Title });
                }
            })
        }
        function listAttribute() {
            return attributeService.list().then((result) => {
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
                product.Attribute.grid.getlist();
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

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('ProductCartableController', ProductCartableController);
    ProductCartableController.$inject = ['$scope', 'productService', '$q', '$location'];
    function ProductCartableController($scope, productService, $q, $location) {
        let product = $scope;
        product.grid = {
            bindingObject: product
            , columns: [{ name: 'Name', displayName: 'عنوان آگهی' },
            { name: 'TrackingCode', displayName: 'کدپیگیری' }]
            , listService: productService.list
            , onEdit: null
            , route: 'product'
            , globalSearch: true
            , initLoad: true
        };
        product.goToPageAdd = goToPageAdd;
        function goToPageAdd() {
            $location.path('/product/add');
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
        attribute.state = '';
        attribute.goToPageAdd = goToPageAdd;
        attribute.addAttribute = addAttribute;
        attribute.editAttribute = editAttribute;
        attribute.grid = {
            bindingObject: attribute
            , columns: [{ name: 'Name', displayName: 'عنوان' }]
            , listService: attributeService.list
            , onEdit: attribute.main.changeState.edit
            , globalSearch: true
            , initLoad:true
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
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryController', categoryController);
    categoryController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'categoryService', '$location', 'toaster', '$timeout', 'categoryMapDiscountService', 'discountService'];
    function categoryController($scope, $q, loadingService, $routeParams, categoryService, $location, toaster, $timeout, categoryMapDiscountService, discountService) {
        let category = $scope;
        category.Model = {};
        category.main = {};
        category.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        category.state = '';
        category.error = {};
        category.error.show = false;;
        category.goToPageAdd = goToPageAdd;
        category.addCategory = addCategory;
        category.editCategory = editCategory;
        category.grid = {
            bindingObject: category
            , columns: [{ name: 'Title', displayName: 'عنوان' }]
            , listService: categoryService.list
            , onEdit: category.main.changeState.edit
            , route: 'category'
            , globalSearch: true
            , initLoad:true
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
                        categoryService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function cartable() {
            category.state = 'cartable';
            $location.path('/category/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                return listDiscount();
            }).then(() => {
                category.state = 'add';
                $location.path('/category/add');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryService.get(model.ID);
            }).then((result) => {
                category.Model = result;
                return select();
            }).then(() => {
                return listDiscount();
            }).then(() => {
                category.state = 'edit';
                if (category.Model.ParentID !== "00000000-0000-0000-0000-000000000000") {
                    $('#hassubmenu').prop('checked', true);
                    $("#ParentID").prop("disabled", false);
                } else {
                    $('#hassubmenu').prop('checked', false);
                    $("#ParentID").prop("disabled", true);
                }
                $location.path(`/category/edit/${category.Model.ID}`);;
            }).finally(loadingService.hide);
        }
        function goToPageAdd() {
            add();
        }
        function addCategory() {
            loadingService.show();
            $q.resolve().then(() => {
                return categoryService.add(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    category.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                    }, 'slow');
                }
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');

            })
        }
        function editCategory() {
            loadingService.show();
            $q.resolve().then(() => {
                return categoryService.edit(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                category.error.show = true;
                $('#content > div').animate({
                    scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                }, 'slow');
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');

            }).finally(loadingService.hide);
        }
        function select() {
            categoryService.list().then((result) => {
                category.selectCategory = [];
                for (i = 0; i < result.length; i++) {
                    if (result[i].ParentID === '00000000-0000-0000-0000-000000000000') {
                        category.selectCategory.push({ Model: result[i].ID, Name: result[i].Title });
                    }
                }
            })
        }
        function listDiscount() {
            return $q.resolve().then(() => {
                return discountService.list();
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
        discount.goToPageAdd = goToPageAdd;
        discount.addDiscount = addDiscount;
        discount.editDiscount = editDiscount;
        discount.grid = {
            bindingObject: discount
            , columns: [{ name: 'Name', displayName: 'عنوان تخفیف' }]
            , listService: discountService.list
            , onEdit: discount.main.changeState.edit
            , globalSearch: true
            , initLoad: true
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
            discount.Model = {};
            discount.state = 'cartable';
            $location.path('/discount/cartable');
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
        function editDiscount() {
            loadingService.show();
            return $q.resolve().then(() => {
                return discountService.edit(discount.Model)
            }).then((result) => {
                discount.grid.getlist(false);
                discount.Model = result;
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
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentController', commentController);
    commentController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption'];
    function commentController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout, toolsService, enumService, froalaOption) {
        let comment = $scope;
        comment.Model = {};
        comment.state = '';
        comment.main = {};
        comment.froalaOptionComment = froalaOption.comment;
        comment.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        comment.editComment = editComment;
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'NameFamily', displayName: 'نام کاربر' },
            { name: 'ProductName', displayName: 'نام محصول' },
            { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: { 1: 'درحال بررسی', 2: 'تایید', 3: 'عدم تایید' } }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => {
                return 6;
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
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryPortalController', categoryPortalController);
    categoryPortalController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'categoryPortalService', '$location', 'toaster', '$timeout'];
    function categoryPortalController($scope, $q, loadingService, $routeParams, categoryPortalService, $location, toaster, $timeout) {
        let category = $scope;
        category.Model = {};
        category.main = {};
        category.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        category.state = '';
        category.goToPageAdd = goToPageAdd;
        category.addCategory = addCategory;
        category.editCategory = editCategory;
        category.grid = {
            bindingObject: category
            , columns: [{ name: 'Title', displayName: 'عنوان مقاله' }]
            , listService: categoryPortalService.list
            , onEdit: category.main.changeState.edit
            , globalSearch: true
            , initLoad: true
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
                        categoryPortalService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            category.state = 'cartable';
            $location.path('/category-portal/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                category.state = 'add';
                $location.path('/category-portal/add');
            }).finally(loadingService.hide)

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.get(model.ID);
            }).then((result) => {
                category.Model = result;
                if (category.Model.ParentID !== "00000000-0000-0000-0000-000000000000") {
                    $('#hassubmenu').prop('checked', true);
                    $("#ParentID").prop("disabled", false);
                } else {
                    $('#hassubmenu').prop('checked', false);
                    $("#ParentID").prop("disabled", true);
                }
                return select();
            }).then(() => {
                category.state = 'edit';
                $location.path(`/category-portal/edit/${category.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function goToPageAdd() {
            add();
        }
        function addCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.add(category.Model);
            }).then((result) => {
                category.grid.getlist(false);
                category.Model = result;
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت درج گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.edit(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function select() {
            return categoryPortalService.list().then((result) => {
                category.selectCategory = [];
                for (i = 0; i < result.length; i++) {
                    if (result[i].ParentID === '00000000-0000-0000-0000-000000000000') {
                        category.selectCategory.push({ Model: result[i].ID, Name: result[i].Title });
                    }
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('articleController', articleController);
    articleController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'articleService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService', 'toolsService', 'enumService', 'froalaOption'];
    function articleController($scope, $q, loadingService, $routeParams, articleService, $location, toaster, $timeout, categoryPortalService, attachmentService, toolsService, enumService, froalaOption) {
        let article = $scope;
        article.Model = {};
        article.main = {};
        article.Model.Errors = [];
        article.pic = { type: '4', allowMultiple: false, validTypes: 'image/jpeg' };
        article.pic.list = [];
        article.pic.listUploaded = [];

        article.state = '';
        article.addArticle = addArticle;
        article.editArticle = editArticle;
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
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' },
            { name: 'TrackingCode', displayName: 'کد پیگیری رویداد' }]
            , listService: articleService.list
            , deleteService: articleService.remove
            , onAdd: article.main.changeState.add
            , onEdit: article.main.changeState.edit
            , route: 'article'
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
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
                if (article.Model.IsShow) {
                    article.Model.IsShow = 1;
                }
                else {
                    article.Model.IsShow = 0;
                }
                if (article.Model.CommentStatus) {
                    article.Model.CommentStatus = 1;
                }
                else {
                    article.Model.CommentStatus = 0;
                }
                return true;
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    article.pic.listUploaded = [].concat(result);
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
                article.pics = [];
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    article.pic.listUploaded = [].concat(result);
                article.pic.reset();
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
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
                    if (article.pic.listUploaded.length === 0) {
                        article.pics.push({ ParentID: article.Model.ID, Type: 2, FileName: article.pic.list[0], PathType: article.pic.type });
                    }
                    return attachmentService.add(article.pics);
                }
                return true;
            }).then(() => {
                article.pics = [];
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    article.pic.listUploaded = [].concat(result);
                article.pic.reset();
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
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
            categoryPortalService.list().then((result) => {
                article.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        article.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            article.Model = {};
            article.pic.listUploaded = [];
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('newsController', newsController);
    newsController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'newsService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService', 'toolsService', 'enumService', 'froalaOption'];
    function newsController($scope, $q, loadingService, $routeParams, newsService, $location, toaster, $timeout, categoryPortalService, attachmentService, toolsService, enumService, froalaOption) {
        let news = $scope;
        news.Model = {};
        news.main = {};

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
        news.typeshow = toolsService.arrayEnum(enumService.ShowArticleType);
        news.typecomment = toolsService.arrayEnum(enumService.CommentArticleType);
        news.grid = {
            bindingObject: news
            , columns: [{ name: 'Title', displayName: 'عنوان خبر' },
            { name: 'CreationDatePersian', displayName: 'تاریخ ایجاد' },
            { name: 'TrackingCode', displayName: 'کد پیگیری رویداد' }]
            , listService: newsService.list
            , deleteService: newsService.remove
            , onEdit: news.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
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
            categoryPortalService.list().then((result) => {
                news.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        news.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            news.Model = {};
            news.pic.listUploaded = [];
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
                menu.Model = model;
                return menuService.getbyParentNode({ ParentNode: model.ParentNode });
            }).then((result) => {
                menu.state = 'edit';
                menu.Model.ParentID = result.ID;
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
        slider.pic = { type: '8', allowMultiple: false, validTypes: 'image/jpeg' };
        slider.pic.list = [];
        slider.pic.listUploaded = [];
        slider.main = {};
        slider.main.changeState = {
            cartable: cartable,
            edit: edit,
            add: add
        }
        slider.grid = {
            bindingObject: slider
            , columns: [{ name: 'Title', displayName: 'عنوان اسلایدر' }]
            , listService: sliderService.list
            , deleteService: sliderService.remove
            , onEdit: edit
            , route: 'slider'
            , globalSearch: true
            , displayNameFormat: ['Title']
            , initLoad: true
        };

        slider.addSlider = addSlider;
        slider.editSlider = editSlider;
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

        events.addEvents = addEvents;
        events.editEvents = editEvents;

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
            return categoryPortalService.list().then((result) => {
                events.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        events.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
        function clearModel() {
            $('.js-example-tags').empty();
            events.Model = {};
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentPortalController', commentPortalController);
    commentPortalController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout', 'toolsService', 'enumService', 'froalaOption'];
    function commentPortalController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout, toolsService, enumService, froalaOption) {
        let comment = $scope;
        comment.Model = {};
        comment.Model.Errors = [];

        comment.Search = {};
        comment.state = '';
        comment.main = {};
        comment.froalaOptionComment = angular.copy(froalaOption.comment);
        comment.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        comment.editComment = editComment;
        comment.changeDrop = changeDrop;
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'NameFamily', displayName: 'نام کاربر' },
            { name: 'ProductName', displayName: 'عنوان' },
            { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: { 1: 'درحال بررسی', 2: 'تایید', 3: 'عدم تایید' } }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => { return comment.Search.CommentForType }
            , initLoad: true
        };

        comment.selectCommentType = toolsService.arrayEnum(enumService.CommentType);
        comment.selectCommentForType = toolsService.arrayEnum(enumService.CommentForType);
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
            { name: 'CountBuy', displayName: 'تعداد خرید' }]
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
            bindingObject: payment
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
})();