'use strict'

telemonitoreoApp.controller('gestanteController',  
  function GestanteController($scope, gestanteRepository) {
    $scope.error = false;
    gestanteRepository.get().then(
      function(gestantesData) {
        $scope.gestantes = gestantesData; },
      function(errorData) {
        $scope.error = true; });
})