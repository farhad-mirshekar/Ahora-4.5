(() => {
    var app = angular.module('portal');
    app.factory('authenticationService', authenticationService);
    authenticationService.$inject = ['$window'];
    function authenticationService($window) {
        let service = {
            setCredentials: setCredentials,
            isAuthenticated: isAuthenticated,
            clearCredentials: clearCredentials
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
        }
    }
})();