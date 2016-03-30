var app = angular.module('myapp', ['LocalStorageModule', 'ngSanitize', 'ngRoute', 'ngAnimate', 'toaster'])
.run(['$rootScope', '$timeout', function ($rootScope, $timeout) {
    $rootScope.$on('$routeChangeStart', function (event, toState, toParams, fromState, fromParams) {
        //console.log('route-start');
        $rootScope.loadingView = true;
    });
    $rootScope.$on('$routeChangeSuccess', function (e, curr, prev) {
        //console.log('route-end: ', curr, prev);
        $rootScope.loadingView = false;
        $timeout(function () {
            if (window.initui) initui();
            if (window._initui) _initui();
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
    .when('/settings', { templateUrl: 'views/settings.html', controller: 'settingsCtrl' });
     

}).filter('moment', function () {
    return function (dateString, format) {
        if (dateString && dateString != '' && moment(dateString).isValid()) return moment(dateString).format(format);
    };
});

function _initui() {
    //$('.table tr td .fa-edit').on('click', function (event) {
    //    console.log('click');
    //    $(this).addClass('highlight-row').siblings().removeClass('highlight-row');
    //});
}
$(document).ready(function () {
    $('ul.nav li').click(function () {
        $(this).siblings('li').removeClass('active');
        $(this).addClass('active');
    });
});