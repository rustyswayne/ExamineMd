angular.module('umbraco').controller("ExamineMd.PropertyEditors.PathPicker", function ($scope, umbRequestHelper, $http) {

    var url = Umbraco.Sys.ServerVariables["examineMd"]["examindMdPropertyEditorsBaseUrl"] + "GetAllPaths";

    umbRequestHelper.resourcePromise(
            $http.get(url),
            'Failed to retrieve starting paths')
        .then(function (data) {
            $scope.fileStorePaths = data;
        });
});