
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />




//var app = angular.module("Demo", ["ngRoute"])
var app = angular.module("Demo", ["ui.router"])

                    .config(function ($routeProvider, $locationProvider) {
                        $routeProvider.caseInsensitiveMatch = true;
                        $routeProvider
                         .when("/home", {
                             templateUrl: "Templates/Home.html",
                             controller: "homecontroller"
                         })
                           .when("/courses", {
                               templateUrl: "Templates/courses.html",
                               controller: "coursecontroller"
                           })
                           .when("/students", {
                               templateUrl: "Templates/Students.html",
                               controller: "Studentcontroller",
                               controllerAs: "StudentCtrl",
                               resolve: {
                                   studentList: function ($http) {
                                       return $http.get("StudentService.asmx/GetCityName")
                                                .then(function (response) {
                                                    return response.data;
                                                })

                                   }
                               }

                           })
                            .when("/students/:CityId", {
                                templateUrl: "Templates/StudentDetail.html",
                                controller: "StudentDetailcontroller"
                            })
                            .when("/StudentSearch/:CityName?", {
                                templateUrl: "Templates/studentsSearch.html",
                                controller: "StudentSearchcontroller",
                                controllerAs: "StudentSearchCtrl"
                            })
                         .otherwise({
                             redirectTo: "/home"
                         })

                        $locationProvider.html5Mode(true);

                    })

                    .controller("homecontroller", function ($scope) {
                        $scope.message = "Home Page";
                    })
                    .controller("coursecontroller", function ($scope) {
                        $scope.courses = ["ASP.NET", "C#", "JAVA", "PHP", "ANDROID"];
                    })
                    .controller("Studentcontroller", function (studentList, $http, $route, $scope, $location) {
                        var vm = this;

                        vm.searchstudent = function () {
                            if (vm.CityName) {
                                $location.url("/StudentSearch/" + vm.CityName)
                            }
                            else {
                                $location.url("/StudentSearch")
                            }
                        }

                        vm.Reloadf = function () {
                            $route.reload();
                        }

                        $scope.$on("$locationChangeStart", function (event, next, current) {
                            if (!confirm("Are you sure navigate" + next)) {
                                event.preventDefault();
                            }
                        });

                        vm.students = studentList;



                    })

                        .controller("StudentDetailcontroller", function ($scope, $http, $routeParams) {

                            $http({
                                url: "StudentService.asmx/GetCityNameById",
                                params: { CityId: $routeParams.CityId },
                                method: "get"

                            })
                                .then(function (response) {
                                    $scope.student = response.data;

                                })

                        })

                .controller("StudentSearchcontroller", function ($http, $routeParams) {
                    var vm = this;
                    if ($routeParams.CityName) {

                        $http({
                            url: "StudentService.asmx/GetCityNameByName",
                            params: { CityName: $routeParams.CityName },
                            method: "get"

                        })
                               .then(function (response) {
                                   vm.students = response.data;

                               })


                    }
                    else {
                        $http.get("StudentService.asmx/GetCityName")
                       .then(function (response) {
                           vm.students = response.data;

                       })
                    }
                })


