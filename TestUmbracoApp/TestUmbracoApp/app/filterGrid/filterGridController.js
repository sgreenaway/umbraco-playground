//import * as angular from 'angular';
//import './filterGridService';

(function () {
    'use strict';

    angular
        .module('filterGrid')
        .controller('filterGridController', filterGridCtrl);

    filterGridCtrl.$inject = ['filterGridService', '$scope'];

    function filterGridCtrl(filterGridService, $scope) {
        /* jshint validthis:true */
        $scope.filterGridModel = {};
        init();
        getData();

        //Specific functionality for last selected item in dropdown
        $scope.lastSelectedProps = {};
        $scope.lastIndex = -1;
        //End

        function init() {
            $scope.filterGridModel.DataModel = {
                PageNumber: 1,
                PageSize: 3
            };
            $scope.filterGridModel.FilterOptions = {
                DocTypeId: 1112,
                FilterRoot: 1160
            };
        }

        function getData() {
            filterGridService.getData($scope.filterGridModel).then(function (data) {
                $scope.filterGridModel = data;
                //This is to hack the last selected dropdown to be un-restricted - you wouldn't want to use the with other form controls
                if ($scope.lastIndex !== -1 && $scope.filterGridModel.FilterOptions.Options[$scope.lastIndex].SelectedProperty !== 0) {
                    $scope.filterGridModel.FilterOptions.Options[$scope.lastIndex].Properties = $scope.lastSelectedProps;
                }
            });
        }

        $scope.getChangeEvent = function (lastProps, lastIndex) {
            $scope.lastSelectedProps = lastProps;
            $scope.lastIndex = lastIndex;
            console.log(lastProps);
            getData();
        };

        $scope.getNewPage = function (page) {
            $scope.filterGridModel.DataModel.PageNumber = page;
            getData();
        };
    }
})();