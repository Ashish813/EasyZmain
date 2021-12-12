app.directive("w3TestDirective", function () {
    return {
        restrict: "E",
        template: "<h1>Made by a directive!</h1>"
    };
});

app.controller("ResumeProcess2Controller", function ($scope, $http, $sce, $compile) {

    $scope.pageno = 1;

    $scope.Html = ``;
    $scope.varr = "ResumeProcess2";





});