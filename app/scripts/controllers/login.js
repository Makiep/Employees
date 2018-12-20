'use strict';
angular.module('onlineClientApp')
  .controller('LoginCtrl',
  [
    '$scope', '$location', 'authService',
    function ($scope, $location, authService) {

      $scope.login = {
        email: "",
        password: "",
        useRefreshTokens: false
      };

      $scope.message = "";
      $scope.employeeLogin = employeeLogin;

      function employeeLogin(){
        authService.logIn($scope.login)
          .then(function (saveReault) {
            $scope.message = "Login Successful";
            $location.path('/employee');
          })
          .catch(function (error) {
            $scope.message = err.error_description;
          });
      };
    }
  ]);


