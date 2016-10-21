(function() {
    'use strict';

    angular
        .module('sanDiegoTopSpots')
        .controller('sanDiegoTopSpots', sanDiegoTopSpots);

    sanDiegoTopSpots.$inject = ['$http', '$scope', 'sanDiegoTopSpotsFactory', 'toastr', 'uiGmapGoogleMapApi']; 

    /* @ngInject */
    function sanDiegoTopSpots($http, $scope, sanDiegoTopSpotsFactory, toastr, uiGmapGoogleMapApi) {
        var vm = this;
        var location = []; // array to store the location enterered when user enters a location into the "add new topspot" field
        var markers = []; // array to store the google maps markers    
        
        //creates the default view of the google map upon initial load
        $scope.map = { 
            center: { 
                latitude: 32.7157,
                longitude: -117.1611
            },
            zoom: 12
        };

        vm.addRow = addRow;
        vm.getTopSpots = getTopSpots;
     
        activate(); 

        function activate() {
            getTopSpots();
        }

        // gets the TopSpots data from the TopSpots API
        function getTopSpots () {

       		sanDiegoTopSpotsFactory.getSanDiegoTopSpots().then( 
        		function(response) { 
                    markers = response.data;
        			vm.topspots = response.data; //grabs topspots API JSON data response from sanDiegoTopSpotsFactory

                    angular.forEach(markers, function(marker){ //grabs lat/lon data from json file and uses it to create the google maps markers.
                        marker.coords = { 
                            latitude: marker.location[0],
                            longitude: marker.location[1]
                        };
                    });
        
                    $scope.markers = markers; 					
    			},
    			function(error){ //handles errors given by the sanDiegoTopSpotsFactory
                    
                    toastr.error('There was an error');
       			}
        	);
        }

        //takes data entered into the "add a topspot form" and places that data into the topspots object
        function addRow () {
          
          location[0] = vm.latitude;
          location[1] = vm.longitude;
          vm.topspots.push(
            { 
            'name':vm.name,
            'description': vm.description,
            'location': location
          });

          //clears these form fields after adding a row
          vm.name = '';
          vm.description = '';
          vm.location = '';
        };
    }

})();
