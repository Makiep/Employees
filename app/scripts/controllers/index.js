'use strict';

angular.module('onlineClientApp')
  .controller('IndexCtrl', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.logOut = function () {
      authService.logOut();
      $location.path('/home');
    }

    $scope.authentication = authService.authentication;
  }]);
