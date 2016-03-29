app.controller('dashboardCtrl', function ($scope) {
    $scope.title = 'Dashboard';
    $scope.subtitle = 'Welcome';

})
.controller('newFeatureCtrl', function ($scope, crudService, toaster, $window, $route, $routeParams, utilsFac, $location) {
    $scope.clear = function () {
        $scope.Feature = {Files:[]};
    }
    $scope.save = function (item) {
        console.log('saving: ',item);
        $scope.processing = true;
        crudService.saveItem('Features', item).then(function (d) {
            toaster.success({ title: "Success", body: "Feature Saved.", bodyOutputType: 'trustedHtml' });
            console.log('response: ', d);
            $scope.Feature = d;
            $scope.processing = false;
        }, function (d) {
            toaster.warning({ title: "Error", body: "something went wrong. " + d, bodyOutputType: 'trustedHtml' });
            $scope.processing = false;
        });
        //$scope.clear();
    }
    $scope.loadmaster = function (filter) {
        var f = ''; if (filter) f = JSON.stringify(filter);
        crudService.getItems('masters', f).then(function (d) {
            $scope.Masters = d;
            $scope.loading = false;
            //console.log('Master Data:',d);
        });
    }
    $scope.loadData = function (id) {
        var f = '?id=' + id;
        crudService.getItems('features', undefined, f).then(function (d) {
            $scope.title = 'Edit Feature';
            $scope.Feature = d;
            $scope.loading = false;
            $scope.editmode = true;
        });
    }
    $scope.cancel = function () {
        $scope.editmode = false;
        $location.path('/features');
    }
    $scope.init = function () {
        $scope.loading = true;
        $scope.formats = utilsFac.formats();
        $scope.title = 'New Feature';
        $scope.subtitle = 'Welcome';
        $scope.loadmaster();
        $scope.props = {};
        $scope.clear();
        if ($routeParams.ind) {
            $scope.loadData($routeParams.ind);
        }
    }
    $scope.init();
})
.controller('featureListCtrl', function ($scope, crudService, $location, utilsFac) {
    
    $scope.filterlist = function (flag) {
        var filter = { Module: $scope.Module, Page: $scope.Page, Status: $scope.Status, Type: $scope.Type }
        var f = '';
        if (flag) {
            $scope.filtered = true;
            f = '?filter=' + JSON.stringify(filter);
        }
        //console.log('filter:', f);
        crudService.getItems('features', f).then(function (d) {
            $scope.features = d;
            $scope.loading = false;
        });
    }
    $scope.clearFilter = function () {
        $scope.filtered = undefined;
        $scope.activeModule = undefined;
        $scope.activePage = undefined;
        $scope.filterlist();
    }
    $scope.mod = function (i) { $scope.activeModule = i.text; $scope.Module = i.Id; $scope.filterlist(true); }
    $scope.page = function (i) { $scope.activePage = i.text; $scope.Page = i.Id; $scope.filterlist(true); }
    $scope.status = function (i) { $scope.Status = i.Id; $scope.filterlist(true); }
    $scope.type = function (i) { $scope.Type = i.Id; $scope.filterlist(true); }
    $scope.getclass = function (flag,selected) {
        var css='';
        if (flag == 'm' && selected == $scope.activeModule) css = 'active';
        if (flag == 'p' && selected == $scope.activePage) css = 'active';
        return css;
    }
    $scope.loadmaster = function (filter) {
        var f = ''; if (filter) f = JSON.stringify(filter);
        crudService.getItems('masters',f).then(function (d) {
            $scope.Masters = d;
            $scope.loading = false;
        });
    }
    $scope.ref = function () {
        $scope.loading = true;
        $scope.filterlist();
    }   
    $scope.edit = function ($index, item) {
        $location.path('/edit/' + item.Id);
    }
    $scope.addFeature = function ($index, item) {
        $location.path('/addfeature');
    }
    $scope.init = function () {
        $scope.formats = utilsFac.formats();
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
.controller('settingsCtrl', function ($scope, $q, toaster, $window, localstoragefac) {
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
    $scope.getclass = function (flag, selected) {
        var css = '';
        if (flag == 'p' && selected == $scope.title) css = 'active';
        return css;
    }
    $scope.nav = function (selected) {
        //console.log(selected.Name);
        $scope.title = selected.Name;   
        $scope.selectedConfig = selected.Name.toLowerCase();
        
    }
    $scope.init = function () {
        $scope.menus = [{ Name: 'Application', Url: 'settings.application', Css: 'active' }, { Name: 'Project', Url: 'settings.project', Css: '' }, { Name: 'Module', Url: 'settings.module' }, { Name: 'Page', Url: 'settings.page' }];
        $scope.title = 'Project';
        $scope.selectedConfig = $scope.title.toLowerCase();
        localstoragefac.init('settings');        
        $scope.Settings = localstoragefac.getitem();        
    }
    $scope.init();
})