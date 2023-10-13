let map;
let panorama;
let xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "Maps/GetMaps", false)
xmlHttp.send(null);
getmaps = JSON.parse(xmlHttp.responseText);
console.log(getmaps)

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service is failed"
            : "Error: Your browser donsent support geolocation");
    infoWindow.open(map);

}

function geocodeMap(map, address) {
    var geocoder = new google.maps.Geocoder();
    var content = "<h3>" + address.Name + "<h3><hr/><p>" + address.Description + "<p/>"
    var infoWindow = new google.maps.InfoWindow({ content: content });
    geocoder.geocode({ address: address.Description }, function (result, status) {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: result[0].geometry.location
            });
            marker.addListener("click", function () {
                infoWindow.open(map, marker);
            }
            );
        }
    });
}

function initMap() {
    const berkeley = { lat: -34.397, lng: 150.644 };
    const sv = new google.maps.StreetViewService();
    panorama = new google.maps.StreetViewPanorama(
        document.getElementById("pano"),
    );
    map = new google.maps.Map(document.getElementById("map"), {
        center: berkeley,
        zoom: 8,
    });
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            position => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(pos);
            },
            () => {
                handleLocationError(false, infoWindow, map.getCenter());
            }
        );

    } else {
        handleLocationError(false, infoWindow, map.getCenter());
    }

    map.addListener("click", (event) => {
        sv.getPanorama({ location: event.latLng, radius: 50 })
            .then(processSVData)
            .catch((e) =>
                console.error("Street View data not found for this location."),
        );
    });

    //mark
    for (var i = 0; i < getmaps.length; i++) {
        geocodeMap(map, getmaps[i]);
    }
    //get start direction
    var start = document.getElementById("start")
    const autoComplete = new google.maps.places.Autocomplete(start);
    autoComplete.bindTo("bounds", map);

    //get end direction
    const directionsRenderer = new google.maps.DirectionsRenderer(); 
    const directionsService = new google.maps.DirectionsService(); 
    directionsRenderer.setMap(map);
    directionsRenderer.setPanel(document.getElementById("sidebar"));

    var getDirection = document.getElementById("get-direction");
    getDirection.addEventListener("click", () => {
        directionsService.route({
            origin: {
                query: document.getElementById("start").value
            },
            destination: {
                query: document.getElementById("end").value
            },
            travelMode: google.maps.TravelMode[document.getElementById("mode").value.toUpperCase()] 
        }, (response, status) => {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Unable to get direction due to " + status);
            }
        });
    });



}
function processSVData({ data }) {
    const location = data.location;
    const marker = new google.maps.Marker({
        position: location.latLng,
        map,
        title: location.description,
    });

    panorama.setPano(location.pano);
    panorama.setPov({
        heading: 270,
        pitch: 0,
    });
    panorama.setVisible(true);
    marker.addListener("click", () => {
        const markerPanoID = location.pano;

        // Set the Pano to use the passed panoID.
        panorama.setPano(markerPanoID);
        panorama.setPov({
            heading: 270,
            pitch: 0,
        });
        panorama.setVisible(true);
    });
}

window.initMap = initMap;
