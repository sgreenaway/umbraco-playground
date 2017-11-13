//import * as angular from 'angular';

(function () {
    'use strict';

    angular
        .module('filterGrid')
        .factory('filterGridService', ['$http', '$q', filterGridService]);

    function filterGridService($http, $q) {
        var service = {
            getData: getData
        };

        return service;

        function getData(options) {
            var deferred = $q.defer();
            $http.post('http://localhost:59989/umbraco/api/filtergrid/getdata', options)
                .then(function (data) {
                    deferred.resolve(data.data);
                });
            return deferred.promise;
        }
    }
})();