'use strict';

angular
  .module('onlineClientApp')
  .controller('EmployeesCtrl', ['$scope', '$location', '$timeout', 'authService', 'roleService',
    function ($scope, $location, $timeout, authService, roleService) {

      //TODO:Remove variables injected but not being used

      $scope.employees = []

      authService.getEmployees()
        .then(function (results) {
          $scope.employees = results.data;
        });

      $scope.loadSignup = function (emailAddress) {
          $location.path('/signup').search({
              email: emailAddress,
              isAdmin: true });
      };
     
    }]);
