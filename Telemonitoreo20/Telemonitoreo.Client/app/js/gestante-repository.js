'use strict'

telemonitoreoApp.factory('gestanteRepository', 
  function ($http, $q) {
    return {
      get: function() {
        var deferred = $q.defer();
        $http.get('http://localhost:52998/api/Gestantes').success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
      }
    }
})