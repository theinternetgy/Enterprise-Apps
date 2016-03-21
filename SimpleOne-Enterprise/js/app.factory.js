app.factory('localstoragefac', function () {
    var fac = {};
    var lskey = '';
    fac.init = function (key) {
        lskey = '__simpleonefeatures_' + key;
    }
    fac.saveitem = function (item) {
        localStorage.setItem(lskey, JSON.stringify(item));
    }
    fac.getitem = function () {
        var items = {};
        var a = localStorage.getItem(lskey);
        if (a && a != undefined && a != 'undefined' && a != '') items = JSON.parse(a);
        return items;
    }
    return fac;
}).factory('utilsFac', function () {
    var fac = {};
    fac.formats = function () {
        var items = { datetimeformat: 'MMMM Do YYYY, h:mm:ss a', dateformat: 'MMMM Do YYYY' };
        return items;
    }
    return fac;
})
.factory('crudService', function ($http, $q, localstoragefac, toaster, $window) {

    var host = '';//http://localhost/webapi
    var hostNotFound = false;
    var init = function () {
        localstoragefac.init('settings');
        var settings = localstoragefac.getitem();
        if (settings && settings.Host && settings.Host!='') {
            host = settings.Host;
        } else {
            hostNotFound = true;
            toaster.warning({ title: "Attention", body: "Please configure host",timeout:1000000 });
        }
    }
    init();
    var configOk = function () {
        var res=true;
        if (hostNotFound) { res = false; console.log('warning: hostNotFound'); }
        return res;
    }
    var URL = {
        _items: function (collectionName) {
            return host+'/api/{collectionName}'.replace("{collectionName}", collectionName);
        },
        _item: function (collectionName, _id) {
            return host+'/api/{collectionName}/id/{_id}'.replace("{collectionName}", collectionName).replace("{_id}", _id);
        },
        _form: function (collectionName, _id) {
            return host + '/api/{collectionName}/form'.replace("{collectionName}", collectionName);
        }
    };

    var getItems = function (collectionName,filter,param) {
        var deferred = $q.defer();
        var apiurl = URL._items(collectionName);
        if (filter && filter != '') apiurl += '/' + filter; else if (param && param != '') apiurl += param;
        //console.log('apiurl:', apiurl);
        $http({ method: 'GET', url: apiurl })
            .success(function (data) {deferred.resolve(data);})
            .error(function (data) { deferred.reject(data);});
        return deferred.promise;
    };

    var getItem = function (collectionName, _id) {

        var deferred = $q.defer();

        $http({ method: 'GET', url: URL._item(collectionName, _id) }).success(function (data) {
            deferred.resolve({
                fields: data.config.fields,
                document: data.document
            });
        });

        return deferred.promise;
    };

    var getForm = function (collectionName) {

        var deferred = $q.defer();

        $http({ method: 'GET', url: URL._form(collectionName) }).success(function (data) {
            deferred.resolve({
                fields: data.config.fields,
                document: data.document
            });
        });

        return deferred.promise;
    };

    var saveItem = function (collectionName, document, callback) {
        var deferred = $q.defer();
        $http.post(URL._items(collectionName), document).success(function (data) {
            deferred.resolve(data);
        }).error(function (data) {
            deferred.reject(data);
        });
        return deferred.promise;
    };

    var updateItem = function (collectionName, document, callback) {
        $http.put(URL._item(collectionName, document._id), document).success(function (data) {
            callback(data);
        });
    };

    var removeItem = function (collectionName, _id, callback) {
        $http.delete(URL._item(collectionName, _id)).success(function (data) {
            callback(data);
        });
    };

    return {
        getItems: getItems,
        getItem: getItem,
        saveItem: saveItem,
        updateItem: updateItem,
        removeItem: removeItem,
        getForm: getForm,
        ok: configOk
    }
})