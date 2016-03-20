app.controller('dashboardCtrl', function ($scope) {
    $scope.title = 'Dashboard';
    $scope.subtitle = 'Welcome';

})
.controller('newFeatureCtrl', function ($scope, crudService, toaster, $window) {
    $scope.clear = function () {
        $scope.Feature = {Files:[]};
    }
    $scope.save = function (item) {
        //console.log(item);
        $scope.processing = true;
        crudService.saveItem('Features', item).then(function (d) {
            toaster.success({ title: "Success", body: "Feature Saved.", bodyOutputType: 'trustedHtml' });
            $scope.processing = false;
        }, function (d) {
            toaster.warning({ title: "Error", body: "something went wrong. " + d, bodyOutputType: 'trustedHtml' });
            $scope.processing = false;
        });
        //$scope.clear();
    }
    $scope.init = function () {
        $scope.title = 'Features';
        $scope.subtitle = 'Welcome';

        $scope.props = {};
        $scope.clear();
    }
    $scope.init();
})
.controller('featureListCtrl', function ($scope, crudService) {
    $scope.filterlist = function (flag) {
        var filter = { Module: $scope.Module, Page: $scope.Page, Status: $scope.Status, Type: $scope.Type }
        var f = ''; if(flag) f = JSON.stringify(filter);
        console.log('filter:', f);
        crudService.getItems('features', f).then(function (d) {
            $scope.features = d;
            $scope.loading = false;
        });
    }
    $scope.mod = function (i) { console.log(i); $scope.Module = i.Id; $scope.filterlist(true); }
    $scope.page = function (i) { $scope.Page = i.Id; $scope.filterlist(true); }
    $scope.status = function (i) { $scope.Status = i.Id; $scope.filterlist(true); }
    $scope.type = function (i) { $scope.Type = i.Id; $scope.filterlist(true); }
   
    $scope.loadmaster = function (filter) {
        var f = ''; if (filter) f = JSON.stringify(filter);
        crudService.getItems('masters',f).then(function (d) {
            $scope.Masters = d;
            $scope.loading = false;
        });
    }
    $scope.init = function () {
        $scope.loading = true;
        $scope.filter;
        $scope.loadmaster();
        $scope.filterlist();
    }
    $scope.init();

})
.controller('TypeaheadDemoCtrl', function ($scope) { })
.controller('FlotChartDemoCtrl', function ($scope) { })
.controller('MainCtrl', function ($scope) { })
.controller('settingsCtrl', function ($scope, $q,toaster, $window,localstoragefac) {
    var mode = 'local';    
    $scope.Settings = {};
    $scope.save = function () {
        var d = $q.defer();
        if (mode == 'local') {
            localstoragefac.saveitem($scope.Settings);
            toaster.success({ title: "Success", body: "Settings Saved." });
            d.resolve();
        }
        return d.promise;
    }
    $scope.edit=function(index){
        $scope.editmode=true;
    }
    $scope.init = function () {
        localstoragefac.init('settings');
        console.log(localstoragefac.getitem());
        $scope.Settings = localstoragefac.getitem();
    }
    $scope.init();
})