var geoAppControllers = angular.module("geoApp.Controllers",
    [
        "geoApp.Services"
    ]);

geoApp.controller("ContinentsCtrl", function ($scope, continents) {
    $scope.continents = continents.allContinents;
});

geoApp.controller("CountriesCtrl", function ($scope) {
    $scope.countries = model.counrties;
});

geoApp.controller("StatesCtrl", function ($scope) {
    $scope.states = model.states;
});

geoApp.controller("CitiesCtrl", function ($scope) {
    $scope.cities = model.cities;
    $scope.changeImage = function (city) {
        console.log(city);
    };
});