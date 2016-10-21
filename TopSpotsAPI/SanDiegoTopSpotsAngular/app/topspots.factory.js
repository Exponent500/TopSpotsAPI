(function() {
    'use strict';

    angular
        .module('sanDiegoTopSpots')
        .factory('sanDiegoTopSpotsFactory', sanDiegoTopSpotsFactory);

    sanDiegoTopSpotsFactory.$inject = ['$http', '$q'];

    /* @ngInject */
    function sanDiegoTopSpotsFactory($http, $q) { 
        var service = { 
            getSanDiegoTopSpots: getSanDiegoTopSpots 
        };
        return service;


        //pulls San Diego Top Spots info from our Top Spots API we created in C#
        function getSanDiegoTopSpots() {

        	var defer = $q.defer(); 

        	$http({
        		method: 'GET',
        		url: 'http://localhost:55471/api/topspots'
                
        	})
            .then(function(response){ 
                
    			if(typeof response.data === 'object'){ 
    				defer.resolve(response); 
    			} else {
    				defer.reject(response); 
    			}
			},
			function(error){ 
        		defer.reject(error);
        		
			});

			return defer.promise; 
        	  
        }
    }
})();

//code reviewed by Tristan, John Abanto and Jesse on 10/12/2016