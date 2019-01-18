'use strict';

angular
	.module('onlineClientApp')
	.factory('authService', ['$http', '$q', 'ngAuthSettings', 'localStorageService',
		function ($http, $q, ngAuthSettings, localStorageService) {

			var serviceBase = ngAuthSettings.apiServiceBaseUri;

			var authentication = {
				isAuth: false,
				email: "",
				useRefreshTokens: false
			};

			var service = {
				saveRegistration: saveRegistration,
				logIn: logIn,
				logOut: logOut,
				authentication: authentication,
				getLoggedEmployeeEmail: getLoggedEmployeeEmail,
				getEmployeesDetails: getEmployeesDetails,
				getRoles: getRoles,
				getManagerOptions: getManagerOptions,
				getAllEmployeesDetails: getAllEmployeesDetails,
				getEmployeeById: getEmployeeById,
				saveEmployee: saveEmployee,
				deleteEmployee: deleteEmployee,
				getEmployeesManagers: getEmployeesManagers
			};

			return service;

			function saveRegistration(registration) {
				logOut();
				return $http.post(serviceBase + 'api/employees/signup', registration).then(function (response) {
					return response;
				});
			}
		
			function saveEmployee(employee) {
				return $http.post(serviceBase + 'api/employees/saveemployees', employee).then(function (response) {
					return response;
				});
			}

			function logIn(login) {
				return $http.post(serviceBase + 'api/employees/login', login).then(function (response) {

					if (login.useRefreshTokens) {
						localStorageService.set('authorization', { token: response.access_token, email: login.email, refreshToken: response.refresh_token, useRefreshTokens: true });
					}
					else {
						localStorageService.set('authorization', { token: response.access_token, email: login.email, refreshToken: "", useRefreshTokens: false });
					}
					authentication.isAuth = true;
					authentication.email = login.email;
					authentication.id = login.Id;
					authentication.useRefreshTokens = login.useRefreshTokens;

					return response;
				});
			}

			function getLoggedEmployeeEmail() {
				return localStorageService.authorization.email;
			}
			
			function getEmployeesDetails(email) {
				return $http.get(serviceBase + 'api/employees/employee?email=' + email).then(function (results) {
					return results;
				});
			}

			function logOut() {
				localStorageService.remove('authorization');
				authentication.isAuth = false;
				authentication.email = "";
				authentication.useRefreshTokens = false;
			}

			function getRoles() {
				return $http.get(serviceBase + 'api/employees/roles').then(function (results) {
					return results;
				});
			}

			function getAllEmployeesDetails() {
				return $http.get(serviceBase + 'api/employees/employeedetails')
					.then(function (results) {
					return results;
				});
			}

			function getEmployeesManagers(id) {
				return $http.get(serviceBase + 'api/employees/employeesmanagershierarchy?id=' + id)
					.then(function (results) {
						return results;
					});
			}

			function getManagerOptions(id) {
				return $http.get(serviceBase + 'api/employees/getmanageroptions?id=' + id)
					.then(function (results) {
					return results;
				});
			}

			function getEmployeeById(id) {
				return $http.get(serviceBase + 'api/employees/employeebyid?id=' + id)
					.then(function (results) {
					return results;
				});
			}

			function deleteEmployee(id) {
				return $http.post(serviceBase + 'api/employees/deleteemployeee?id=' + id)
					.then(function (results) {
						return results;
					});
			}

		}]);
