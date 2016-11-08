var geoAppControllers = angular.module("geoApp.Controllers",
    [
        "geoApp.Services"
    ]);

geoApp.controller("ContinentsCtrl", function ($scope, continents) {
    $scope.continents = continents.allContinents;
});

geoApp.controller("CountriesCtrl", function ($scope, countries) {
    $scope.countries = countries.allCountries;
});

geoApp.controller("StatesCtrl", function ($scope, states) {
    $scope.states = states.allStates;
});

geoApp.controller("CitiesCtrl", function ($scope, cities) {
    $scope.cities = cities.allCities;
    $scope.changeImage = function (city) {
        console.log(city);
    };
});