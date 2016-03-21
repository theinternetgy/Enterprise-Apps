app.directive('crud', function (crudService) {
    return {
        restrict: 'E',
        templateUrl: 'views/crud.html',
        scope: {
            collectionName: '@',
            list:'='
        },
        controller: function ($scope, $timeout, $rootScope) {
            $scope.loadingView = $rootScope.loadingView;
            $scope.allSet = crudService.ok();
            $scope.save = function (item) {
                if ($scope.list) {
                    $scope.list.push(item);
                }
                else {
                    $scope.list = [];
                    $scope.list.push(item);
                }
                $scope.message = { content: 'Saved.', css: 'alert alert-success' };
                $scope.item = {};
            }
            var setMessage = function(message){
                $scope.message = message;
                $timeout(function() {
                    $scope.message = null;
                }, 1000);
            };

            $scope.actionList = function () {
                $scope.viewMode = 'list';
                $scope.loadingView = true;
                if (!$scope.allSet) return false;
                crudService.getItems($scope.collectionName).then(function (list) {
                    $scope.list = list;
                    $scope.loadingView = false;
                }, function (d) {
                    $scope.somethingwrong = true;
                    $scope.loadingView = false;
                });
            };
 
            $scope.actionCreate = function () {
                $scope.viewMode = 'create';
                if (!$scope.allSet) return false;
                crudService.getForm($scope.collectionName).then(function (details) {
                    $scope.details = details;
                });
            };
 
            $scope.actionDetails = function (id) {
                if (!$scope.allSet) return false;
                $scope.viewMode = 'details';
 
                crudService.getItem($scope.collectionName, id).then(function (details) {
                    $scope.details = details;
                });
            };
 
            $scope.actionSave = function () {
                if (!$scope.allSet) return false;
                crudService.saveItem($scope.collectionName, $scope.details.document, function (data) {
                    setMessage(data.message);
                    $scope.actionList();
                });
            };
 
            $scope.actionUpdate = function () {
                if (!$scope.allSet) return false;
                crudService.updateItem($scope.collectionName, $scope.details.document, function (data) {
                    setMessage(data.message);
                    $scope.actionList();
                });
            };
 
            $scope.actionRemove = function (_id) {
                if (!$scope.allSet) return false;
                crudService.removeItem($scope.collectionName, _id, function (data) {
                    setMessage(data.message);
                    $scope.actionList();
                });
            };
 
            $scope.actionList();
        }
    }
});