'use strict';

angular
  .module('onlineClientApp')
  .factory('authInterceptorService',
  [
    '$q', '$injector', '$location', 'localStorageService',

    function($q, $injector, $location, localStorageService) {
      
      var authInterceptorServiceFactory = {};
      var request = function(config) {
      config.headers = config.headers || {};
        
        var auth = localStorageService.get('authorization');
        if (auth) {
          config.headers.Authorization = 'Bearer ' + auth.token;
        }
        return config;
      }

      var responseError = function(rejection) {
        if (rejection.status === 401) {
          var authService = $injector.get('authService');
          var auth = localStorageService.get('authorization');
          if (auth) {
            if (auth.useRefreshTokens) {
              $location.path('/refresh');
              return $q.reject(rejection);
            }
          }
          authService.logOut();
         $location.path('/signup');
        }
        return $q.reject(rejection);
      }

      authInterceptorServiceFactory.request = request;
      authInterceptorServiceFactory.responseError = responseError;
      return authInterceptorServiceFactory;
    }

  ]);