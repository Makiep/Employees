'use strict';
var serviceBase = 'http://localhost:54887/';

angular
    .module('onlineClientApp',
        [
            'ngRoute',
            'LocalStorageModule'
        ])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/about',
                {
                    templateUrl: 'views/about.html',
                    controller: 'AboutCtrl',
                    controllerAs: 'about'
                })
            .when('/signup',
                {
                    templateUrl: 'views/signup.html',
                    controller: 'SignupCtrl',
                    controllerAs: 'signup'
                })
            .when('/signup/:email/:isAdmin',
                {
                    templateUrl: 'views/signup.html',
                    controller: 'SignupCtrl',
                    controllerAs: 'signup'
                })
            .when('/login',
                {
                    templateUrl: 'views/login.html',
                    controller: 'LoginCtrl',
                    controllerAs: 'login'
                })
            .when('/employee',
                {
                    templateUrl: 'views/employees.html',
                    controller: 'EmployeesCtrl',
                    controllerAs: 'employees'
                })
            .when('/',
                {
                    templateUrl: 'views/login.html',
                    controller: 'LoginCtrl',
                    controllerAs: 'login'
                })
            .when('/employees/:userId',
                {
                    templateUrl: 'views/employees.html',
                    controller: 'EmployeesCtrl',
                    controllerAs: 'employees'
                })
            .when('/books',
                {
                    templateUrl: 'views/books.html',
                    controller: 'BooksCtrl',
                    controllerAs: 'books'
                })
            .otherwise({
                redirectTo: '/'
            });
    })
    .constant('ngAuthSettings',
        {
            apiServiceBaseUri: serviceBase,
            clientId: 'ngAuthApp'
        });


