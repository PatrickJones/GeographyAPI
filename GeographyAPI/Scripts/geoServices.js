var geoAppServices = angular.module("geoApp.Services", []);

geoApp.constant("baseUrl", "http://localhost:57043/api/");

geoApp.service("continents", function ($http, baseUrl) {
    var continentsArray = [];
    $http.get(baseUrl + "continents").then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            var con = response.data[i];
            continentsArray.push({ name: con.Name });
        }
    });

    this.allContinents = continentsArray;
});

geoApp.service("countries", function ($http, baseUrl) {
    var countriesArray = [];
    $http.get(baseUrl + "countries").then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            var cnt = response.data[i];
            countriesArray.push({ name: cnt.Name });
        }
    });

    this.allCountries = countriesArray;
});

geoApp.service("states", function ($http, baseUrl) {
    var statesArray = [];
    $http.get(baseUrl + "states").then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            var ste = response.data[i];
            statesArray.push({ name: ste.Name });
        }
    });

    this.allStates = statesArray;
});

geoApp.service("cities", function ($http) {
    var citiesArray = [];
    $http.get("http://localhost:57043/geography/cities").then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            var cty = response.data[i];
            citiesArray.push({
                name: cty.Name,
                image: "../Content/img/slider/thumb1.jpg"
            });
        }
    });

    this.allCities = citiesArray
});