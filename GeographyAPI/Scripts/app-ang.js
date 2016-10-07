var geoApp = angular.module("geoApp", []);

var model = {
    continents: [
        { name: "North America" },
        { name: "South America" },
        { name: "Asia" },
        { name: "Africa" },
        { name: "Europe" }
    ],
    counrties: [
        { name: "Country 1" },
        { name: "Country 2" },
        { name: "Country 3" }
    ],
    states: [
        { name: "State 1" },
        { name: "State 2" },
        { name: "State 3" }
    ],
    cities: [
        { name: "City 1", image: "../Content/img/slider/thumb1.jpg" },
        { name: "City 2", image: "../Content/img/slider/thumb2.jpg" },
        { name: "City 3", image: "../Content/img/slider/thumb3.jpg" }
    ]
};

geoApp.controller("ContinentsCtrl", function ($scope) {
    $scope.continents = model.continents;
});

geoApp.controller("CountriesCtrl", function ($scope) {
    $scope.countries = model.counrties;
});

geoApp.controller("StatesCtrl", function ($scope){
    $scope.states = model.states;
});

geoApp.controller("CitiesCtrl", function ($scope) {
    $scope.cities = model.cities;
    $scope.changeImage = function (city) {
        console.log(city);
    };
});