var geoAppServices = angular.module("geoApp.Services", []);

geoApp.constant("baseUrl", "http://localhost:57043/api/");

geoApp.service("continents", function ($http, baseUrl) {
    var continentsArray = []
    $http.get(baseUrl + "continents").then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
            var con = response.data[i];
            continentsArray.push({ name: con.Name });
        }
    });

    this.allContinents = continentsArray;
});
