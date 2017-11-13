import * as angular from 'angular';
import 'angular-utils-pagination';

var filterGrid = angular.module('filterGrid', ['angularUtils.directives.dirPagination']);

filterGrid.controller('filterGridController', [
    '$scope', '$http', function ($scope, $http) {

        $scope.Options = {
            RootId: 1115,
            DocType: "product",
            Page: 1,
            PageSize: 3,
            FilterOptions: { "Category": "bingo" }
        };
        $scope.PageNumber = 1;
        $scope.TotalPages = 1;
        $scope.Results = {};
        $scope.TotalNumberOfRecords = 1;

        (function initialize() {
            getResultsPage();
        })();


        function getResultsPage() {
            // this is just an example, in reality this stuff should be in a service
            $http.post('http://localhost:59989/umbraco/api/filtergrid/getdata', $scope.Options)
                .then(function (data) {
                    $scope.Results = data.data.Results;
                    $scope.PageNumber = data.data.PageNumber;
                    $scope.TotalPages = data.data.TotalPages;
                    $scope.TotalNumberOfRecords = data.data.TotalNumberOfRecords;
                });
        }

        $scope.getNewPage = function (page) {
            $scope.Options.Page = page;
            getResultsPage();
        };
    }
]);