var app = angular.module('myapp', ['LocalStorageModule', 'ngSanitize', 'ngRoute','ngAnimate', 'toaster'])
.run(['$rootScope', '$timeout', function ($rootScope, $timeout) {
    $rootScope.$on('$routeChangeStart', function (event, toState, toParams, fromState, fromParams) {
        //console.log('route-start');
        $rootScope.loadingView = true;
    });
    $rootScope.$on('$routeChangeSuccess', function (e, curr, prev) {
        //console.log('route-end');
        $rootScope.loadingView = false;
        $timeout(function () {
            if (window.initui) initui();
        }, 50);
    });
    
}])
.config(function ($routeProvider, localStorageServiceProvider) {
    localStorageServiceProvider.setPrefix('featuremgmt');
    $routeProvider
      .when('/', { templateUrl: 'views/dashboard.html', controller: 'dashboardCtrl' })
      .when('/addfeature', { templateUrl: 'views/newfeature.html', controller: 'newFeatureCtrl' })
      .when('/edit/:ind', { templateUrl: 'views/newfeature.html', controller: 'newFeatureCtrl' })
      .when('/features', { templateUrl: 'views/featureslist.html', controller: 'featureListCtrl' })
      .when('/addtask', { templateUrl: 'addexpense.html', controller: 'MainCtrl' })
      .when('/adduser', { templateUrl: 'addexpense.html', controller: 'MainCtrl' })
      .when('/addproject', { templateUrl: 'addexpense.html', controller: 'MainCtrl' })
      .when('/addmodule', { templateUrl: 'addexpense.html', controller: 'MainCtrl' })
      .when('/addnew/:ind', { controller: 'MainCtrl', templateUrl: 'addexpense.html' })
    .when('/settings', { templateUrl: 'views/settings.html', controller: 'settingsCtrl' })
}).filter('moment', function () {
    return function (dateString, format) {
        if(dateString && dateString!='') return moment(dateString).format(format);
    };
});