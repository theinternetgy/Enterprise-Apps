app.directive('crud', function (crudService) {
    return {
        restrict: 'E',
        templateUrl: 'views/crud.html',
        scope: {
            primaryid: '=',
            collectionName: '@',
            list:'=',
            labels: '='
        },
        controller: function ($scope, $timeout, $rootScope) {
            
            $scope.init = function () {
                $scope.props = {};
                $scope.loadingView = $rootScope.loadingView;
                $scope.allSet = crudService.ok();
                $scope.$watch('primaryid', function (val) {
                    //console.log('val:',val);
                    //if ($scope.primaryid && $scope.primaryid > 0) $scope.actionList();
                });
                //if($scope.primaryid && $scope.primaryid>0)$scope.actionList();
            }
            $scope.edit = function (index) {
                $scope.props.selectedIndex = index;
                $scope.props.editrecord = true;
                $scope.item = $scope.list[index];
                console.log($scope.item);
            }
            $scope.cancel = function (index) {
                $scope.props.selectedIndex = undefined;
                $scope.props.editrecord = false;
                $scope.props.createnew = false;
                $scope.message = undefined;
                $scope.item = {};
            }
            $scope.save = function (item) {
                item.FeatureId = $scope.primaryid;
                var props=$scope.props;
                if (props.editrecord && Number(props.selectedIndex)>-1) {
                    console.log('save-edit');
                    $scope.item = {};
                    $scope.props.editrecord = false;
                } else {
                    console.log('save-new');
                    if ($scope.list) {
                        $scope.list.push(item);
                    }
                    else {
                        $scope.list = [];
                        $scope.list.push(item);
                    }
                    $scope.item = {};
                }
                //$scope.message = { content: 'Saved.', css: 'alert alert-success' };
                
            }
            var setMessage = function(message){
                $scope.message = message;
                $timeout(function() {
                    $scope.message = null;
                }, 1000);
            };

            $scope.actionList = function () {
                //console.log('primaryId:', $scope.primaryid);
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
 
            $scope.init();

        }
    }
});