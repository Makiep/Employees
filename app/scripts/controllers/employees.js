'use strict';

angular
	.module('onlineClientApp')
	.controller('EmployeesCtrl', ['$scope', '$route', '$location', '$routeParams', 'authService',
		function ($scope, $route, $location, $routeParams, authService) {

			$scope.employee = {
				id: 0,
				email: "",
				firstName: "",
				lastName: "",
				password: "",
				roleId: "",
				managerId: ""
			};

			$scope.mode = "Add";

			authService.getRoles()
				.then(function (results) {
					$scope.roles = results.data;
				});

			authService.getAllEmployeesDetails()
				.then(function (results) {
					$scope.employeesDetails = results.data;
				});

			authService.getManagerOptions($scope.employee.id)
				.then(function (results) {
					$scope.manageroptions = results.data;
				});

			$scope.register = function () {
				authService.saveEmployee($scope.employee).then(function success(response) {
					$route.reload();
				});
			};

			$scope.delete = function (id) {
				authService.deleteEmployee($scope.employee.id).then(function success(response) {
					$route.reload();
				});
			};

			$scope.loadEmployee = function (id) {
				authService.getEmployeeById(id)
					.then(function (result) {
						$scope.employee.email = result.data.email;
						$scope.employee.id = result.data.id;
						$scope.employee.firstName = result.data.firstName;
						$scope.employee.lastName = result.data.lastName;
						$scope.employee.password = result.data.password;
						$scope.employee.roleId = result.data.roleId;
						$scope.employee.managerId = result.data.managerId;
						$scope.mode = "Edit";
						$scope.showDelete = true;
					});
			};

			$scope.getEmployeesManagers = function (id) {
				authService.getEmployeesManagers($scope.employee.managerId)
					.then(function (results) {
						results.data;
					});
			};
		}]);
