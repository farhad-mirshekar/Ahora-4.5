(() => {
    var app = angular.module('portal');
    app.factory('authenticationService', authenticationService);
    authenticationService.$inject = ['$window'];
    function authenticationService($window) {
        let service = {
            setCredentials: setCredentials,
            isAuthenticated: isAuthenticated,
            clearCredentials: clearCredentials,
            set: set,
            get:get
        };
        return service;
        function setCredentials(model) {
            if (model.status) {
                localStorage.setItem('access_token', model.token.Access_Token);
                localStorage.setItem('userid', model.userid);
                localStorage.setItem('refresh_token', model.token.Refresh_Token);
                localStorage.setItem('type',"1");
            } else {
                $window.localStorage.removeItem('access_token');
                $window.localStorage.removeItem('userid');
                $window.localStorage.removeItem('refresh_token');
                $window.localStorage.removeItem('type');
                $window.location.href = '/account/login';
            }
        }
        function isAuthenticated() {
            return (angular.element('input[name="__isAuthenticated"]').attr('value') === 'true');
        }
        function clearCredentials() {
            $window.localStorage.removeItem('access_token');
            $window.localStorage.removeItem('userid');
            $window.localStorage.removeItem('refresh_token');
            $window.localStorage.removeItem('type');
            $window.localStorage.removeItem('currentUserPositions');
            $window.localStorage.removeItem('currentUserPosition');
        }
        function get(key) {
            try {
                return changeStringToDate(JSON.parse($window.localStorage.getItem(key)));

                function changeStringToDate(item) {
                    if (Object.prototype.toString.call(item) === '[object Array]') {
                        for (var i = 0; i < item.length; i++) {
                            item[i] = changeStringToDate(item[i]);
                        }
                    } else if (Object.prototype.toString.call(item) === '[object Object]') {
                        for (var key in item) {
                            if (item.hasOwnProperty(key)) {
                                item[key] = changeStringToDate(item[key]);
                            }
                        }
                    } else if (/-date-ms$/.test(item)) {
                        item = new Date(parseInt(item));
                    }

                    return item;
                }
            } catch (e) {
                if (/-date-ms$/.test($window.localStorage.getItem(key))) {
                    return new Date(parseInt($window.localStorage.getItem(key)));
                }
                return $window.localStorage.getItem(key);
            }
        }
        function set(key, value) {
            $window.localStorage.setItem(key, JSON.stringify(changeDateToJson(value)));

            function changeDateToJson(item) {
                if (Object.prototype.toString.call(item) === '[object Array]') {
                    for (var i = 0; i < item.length; i++) {
                        changeDateToJson(item[i]);
                    }
                } else if (Object.prototype.toString.call(item) === '[object Object]') {
                    for (var key in item) {
                        if (item.hasOwnProperty(key)) {
                            changeDateToJson(item[key]);
                        }
                    }
                } else if (Object.prototype.toString.call(item) === '[object Date]') {
                    item.toJSON = function () { return this.getTime() + '-date-ms'; }
                }

                return item;
            }
        }
    }
})();