'use strict';

angular
    .module('onlineClientApp')
    .controller('SignupCtrl', ['$scope', '$location', '$routeParams', '$timeout', 'authService',
        function ($scope, $location, $routeParams, $timeout, authService) {

            $scope.savedSuccessfully = false;
            $scope.message = "";
            $scope.errors = "";


            $scope.signup = {
                email: "",
                firstName: "",
                lastName: "",
                password: "",
                confirmPassword: "",
                roleId: "",
                manager: ""
            };

            authService.getRoles()
                .then(function (results) {
                    $scope.roles = results.data;
                });

            authService.getEmployees()
                .then(function (results) {
                    $scope.employees = results.data;
                });

            $scope.register = function () {
                authService.saveRegistration($scope.signup).then(function success(response) {
                    $scope.savedSuccessfully = true;
                    $scope.message = "Employee has been registered successfully, please check your email to confirm your email address";
                    startTimer();
                },
                    function (error) {
                        var errors = [];
                        for (var key in error.data) {
                            for (var i = 0; i < error.data.length; i++) {
                                errors.push(error.data[i]);
                            }
                        }
                        $scope.errors = errors;
                    });
            };

            var startTimer = function () {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $location.path('/login');
                }, 2000);
            }

        }]);
