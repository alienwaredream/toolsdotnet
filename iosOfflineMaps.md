http://www.viggiosoft.com/blog/blog/2014/01/21/custom-and-offline-maps-using-overlay-tiles/

Using mapquest OSM tile server http://developer.mapquest.com/web/products/open/map

Open source project from MapBox that built on top of a MapKit: https://github.com/mapbox/mbxmapkit

## ESRI ##

http://www.d3noob.org/2014/02/using-esri-world-imagery-tile-server.html

http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/15/20512/32306

http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/15/20512/32306

## BING ##

http://stackoverflow.com/questions/14442055/use-bing-maps-tiles-with-leaflet

`http:// ecn.t2.tiles.virtualearth.net/tiles/h[q]?g=761&mkt=en-us`

http://ecn.t2.tiles.virtualearth.net/tiles/r121032121.png?g=383&mkt=en-us&shading=hill


? how to move from x,y,z to a quadkey?

```
_quadKey: function (x, y, z) {
    var quadKey = [];
    for (var i = z; i > 0; i--) {
        var digit = '0';
        var mask = 1 << (i - 1);
        if ((x & mask) != 0) {
            digit++;
        }
        if ((y & mask) != 0) {
            digit++;
            digit++;
        }
        quadKey.push(digit);
    }
    return quadKey.join('');
}
```

or

```
function toQuad(x, y, z) {
    var quadkey = '';
    for ( var i = z; i >= 0; --i) {
        var bitmask = 1 << i;
        var digit = 0;
        if ((x & bitmask) !== 0) {
            digit |= 1;}
        if ((y & bitmask) !== 0) {
            digit |= 2;}
        quadkey += digit;
    }
    return quadkey;
};
```

From quad to mercator (source: http://gis.stackexchange.com/questions/9026/how-to-convert-a-bing-maps-quadkey-to-mercator-coordinates):

```
private void BingTest()
{
    const double EarthRadius = 6378137;

    string qdkey = "03200320023";
    double x = 0;
    double y = 0;
    double offset = EarthRadius * Math.PI / 2.0;
    Debug.Print("quadkey: {0}", qdkey);
    for (int i = 0; i < qdkey.Length; i++)
    {
        string s = qdkey.Substring(i, 1);
        Debug.Print("{0} offset: {1} lod {2}",s, offset,i);
        switch (s)
        {
            case "0":  x -= offset; y += offset; break;
            case "1":  x += offset; y += offset; break;
            case "2":  x -= offset; y -= offset; break;
            case "3":  x += offset; y -= offset; break;
            default: throw new Exception("bad quadkey");
        }
        offset *= 0.5;
    }

    // compare with expected result
    double x0 = -9373014;
    double y0 = 4011415;
    Debug.Print("Mercator: x {0}, y {1}", x, y);
    Debug.Print("error x {0} y{1}", x - x0, y - y0);            
}
```

## Yandex ##

http://stackoverflow.com/questions/15306111/using-yandex-map-tiles-in-google-maps-v3

http://vec01.maps.yandex.net/tiles?l=map&v=2.2.3&x=19295&y=24639&z=16

http://vec01.maps.yandex.net/tiles?l=map&v=2.2.3&x={x}&y={y}&z={z}

## NOAA ##
```
var slrMap = {
 
	//default properties
	map : null,
	//class functions
	loadMap: function(){
 
			this.map = new OpenLayers.Map({
				div: "map",
				projection: "EPSG:900913"
			});
 
			this.map.addLayer(
				new OpenLayers.Layer.XYZ("esri", 'http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/${z}/${y}/${x}', {
					transitionEffect: "resize",
					sphericalMercator: true,
					format: "image/jpg"
			}));
			
			this.map.addLayer(
				new OpenLayers.Layer.XYZ("noaa", 'http://csc.noaa.gov/arcgis/rest/services/dc_slr/marsh_000/MapServer/tile/${z}/${y}/${x}', {
					transitionEffect: "resize",
					//sphericalMercator: true,
					format: "image/jpg",
					isBaseLayer : false,
					transparent : "true",
					opacity : 0.9
			}));
			
			this.map.addControl(new OpenLayers.Control.LayerSwitcher());
			this.map.setCenter(new OpenLayers.LonLat(-10670712.603429, 4605008.2646843), 4);
			//this._fetchEsriLayerData();
		},
};
```

```
<!DOCTYPE html>
<html>
<head>
    <title>Simple Leaflet Map</title>
    <meta charset="utf-8" />
    <link 
        rel="stylesheet" 
        href="http://cdn.leafletjs.com/leaflet-0.7/leaflet.css"
    />
</head>
<body>
    <div id="map" style="width: 600px; height: 400px"></div>
 
    <script
        src="http://cdn.leafletjs.com/leaflet-0.7/leaflet.js">
    </script>
 
    <script>
        var map = L.map('map').setView([-41.2858, 174.78682], 14);
        mapLink = 
            '<a href="http://www.esri.com/">Esri</a>';
        wholink = 
            'i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community';
        L.tileLayer(
            'http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
            attribution: '&copy; '+mapLink+', '+wholink,
            maxZoom: 18,
            }).addTo(map);
  
    </script>
</body>
</html>
```

## NSURLCache ##

### monitoring and checking the disk size and memory usage ###
```
- (void)URLSession:(NSURLSession *)session downloadTask:(NSURLSessionDownloadTask *)downloadTask didFinishDownloadingToURL:(NSURL *)downloadURL {

    // Added these lines...
    NSLog(@"DiskCache: %@ of %@", @([[NSURLCache sharedURLCache] currentDiskUsage]), @([[NSURLCache sharedURLCache] diskCapacity]));
    NSLog(@"MemoryCache: %@ of %@", @([[NSURLCache sharedURLCache] currentMemoryUsage]), @([[NSURLCache sharedURLCache] memoryCapacity]));
    /*
    OUTPUTS:
    DiskCache: 4096 of 209715200
    MemoryCache: 0 of 62914560
    */
}
```

### Purging ###

```
- (void)applicationDidReceiveMemoryWarning:(UIApplication *)application {
    [[NSURLCache sharedURLCache] removeAllCachedResponses];
}
```

### Sizing and tricks ###

source: http://stackoverflow.com/questions/21957378/how-to-cache-using-nsurlsession-and-nsurlcache-not-working
```
Note that the following SO post helped me solve my problem: Is NSURLCache persistent across launches?

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    // Set app-wide shared cache (first number is megabyte value)
    NSUInteger cacheSizeMemory = 500*1024*1024; // 500 MB
    NSUInteger cacheSizeDisk = 500*1024*1024; // 500 MB
    NSURLCache *sharedCache = [[NSURLCache alloc] initWithMemoryCapacity:cacheSizeMemory diskCapacity:cacheSizeDisk diskPath:@"nsurlcache"];
    [NSURLCache setSharedURLCache:sharedCache];
    sleep(1); // Critically important line, sadly, but it's worth it!
}
In addition to the sleep(1) line, also note the size of my cache; 500MB. You need a cache size that is way bigger than what you're trying to cache. So for example if you want to be able to cache a 10MB image, then a cache size of 10MB or even 20MB will not be enough. If you need to be able to cache 10MB I'd recommend a 100MB cache. Hope this helps!! Did the trick for me.
```

refers to: http://stackoverflow.com/questions/18451990/is-nsurlcache-persistent-across-launches/18453447#comment33390183_18453447

## Total and free space ##

source: http://stackoverflow.com/questions/5712527/how-to-detect-total-available-free-disk-space-on-the-iphone-ipad-device

```
- (uint64_t)freeDiskspace
{
    uint64_t totalSpace = 0;
    uint64_t totalFreeSpace = 0;

    __autoreleasing NSError *error = nil;  
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);  
    NSDictionary *dictionary = [[NSFileManager defaultManager] attributesOfFileSystemForPath:[paths lastObject] error: &error];  

    if (dictionary) {  
        NSNumber *fileSystemSizeInBytes = [dictionary objectForKey: NSFileSystemSize];  
        NSNumber *freeFileSystemSizeInBytes = [dictionary objectForKey:NSFileSystemFreeSize];
        totalSpace = [fileSystemSizeInBytes unsignedLongLongValue];
        totalFreeSpace = [freeFileSystemSizeInBytes unsignedLongLongValue];
        NSLog(@"Memory Capacity of %llu MiB with %llu MiB Free memory available.", ((totalSpace/1024ll)/1024ll), ((totalFreeSpace/1024ll)/1024ll));
    } else {  
        NSLog(@"Error Obtaining System Memory Info: Domain = %@, Code = %d", [error domain], [error code]);  
    }  

    return totalFreeSpace;
}
```

### Formatting size ###
```
#define MB (1024*1024)
#define GB (MB*1024)

@implementation ALDisk

#pragma mark - Formatter

+ (NSString *)memoryFormatter:(long long)diskSpace {
    NSString *formatted;
    double bytes = 1.0 * diskSpace;
    double megabytes = bytes / MB;
    double gigabytes = bytes / GB;
    if (gigabytes >= 1.0)
        formatted = [NSString stringWithFormat:@"%.2f GB", gigabytes];
    else if (megabytes >= 1.0)
        formatted = [NSString stringWithFormat:@"%.2f MB", megabytes];
    else
        formatted = [NSString stringWithFormat:@"%.2f bytes", bytes];

    return formatted;
}

#pragma mark - Methods

+ (NSString *)totalDiskSpace {
    long long space = [[[[NSFileManager defaultManager] attributesOfFileSystemForPath:NSHomeDirectory() error:nil] objectForKey:NSFileSystemSize] longLongValue];
    return [self memoryFormatter:space];
}

+ (NSString *)freeDiskSpace {
    long long freeSpace = [[[[NSFileManager defaultManager] attributesOfFileSystemForPath:NSHomeDirectory() error:nil] objectForKey:NSFileSystemFreeSize] longLongValue];
    return [self memoryFormatter:freeSpace];
}

+ (NSString *)usedDiskSpace {
    return [self memoryFormatter:[self usedDiskSpaceInBytes]];
}

+ (CGFloat)totalDiskSpaceInBytes {
    long long space = [[[[NSFileManager defaultManager] attributesOfFileSystemForPath:NSHomeDirectory() error:nil] objectForKey:NSFileSystemSize] longLongValue];
    return space;
}

+ (CGFloat)freeDiskSpaceInBytes {
    long long freeSpace = [[[[NSFileManager defaultManager] attributesOfFileSystemForPath:NSHomeDirectory() error:nil] objectForKey:NSFileSystemFreeSize] longLongValue];
    return freeSpace;
}

+ (CGFloat)usedDiskSpaceInBytes {
    long long usedSpace = [self totalDiskSpaceInBytes] - [self freeDiskSpaceInBytes];
    return usedSpace;
}
```

source: https://github.com/andrealufino/ALSystemUtilities/blob/develop/ALSystemUtilities/ALSystemUtilities/ALDisk/ALDisk.m

## Navigation using OSM data ##

http://wiki.openstreetmap.org/wiki/Navit

http://wiki.openstreetmap.org/wiki/Gosmore

## More sources ##

source: http://www.flink.com.au/tips-tricks/27-reasons-not-to-use-google-maps
```
1.
"Bing road, satellite and hybrid layers"
hybrid
"//ak.dynamic.t{s}.tiles.virtualearth.net/comp/ch/{q}?it=A,G,L&shading=hill"
"Tiles <a target="attr" href="http://www.bing.com/maps">Bing</a> &copy; Microsoft and suppliers"

road
"//ecn.t{s}.tiles.virtualearth.net/tiles/r{q}?g=1236"

satellite
"//ak.t{s}.tiles.virtualearth.net/tiles/a{q}?g=1236"

2.
"Esri Navigation Charts (parts of the world, zoom 0..10)"
"//server.arcgisonline.com/ArcGIS/rest/services/Specialty/World_Navigation_Charts/MapServer/tile/{z}/{y}/{x}.png"
"Tiles &copy; <a target="attr" href="http://esri.com">Esri</a>"

3.
"Esri World Imagery (zoom 0..17)"
"//server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}.png"

4. 
"Esri National Geographic (zoom 0..12)"
"//server.arcgisonline.com/ArcGIS/rest/services/NatGeo_World_Map/MapServer/tile/{z}/{y}/{x}.png"

5.
"Esri Physical (zoom 0..8)"
"//server.arcgisonline.com/ArcGIS/rest/services/World_Physical_Map/MapServer/tile/{z}/{y}/{x}.png"

6.
"Esri Ocean (zoom 0..10)"
"//server.arcgisonline.com/ArcGIS/rest/services/Ocean_BaseMap/MapServer/tile/{z}/{y}/{x}.png"

7.
"Esri Reference/World Transportation (zoom 0..18)"
"http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer/tile/{z}/{y}/{x}.png"

8. 
"Esri World Topo Map (zoom 0..18)"
"//server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}.png"

9. 
"Esri World Street Map (zoom 0..18)"
"//server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}.png"

10.
"Google hybrid"
"//khm{s}.googleapis.com/kh?v=142&x={x}&y={y}&z={z}"
"Map data &copy; <a target="attr" href="http://googlemaps.com">Google</a>"

overlay:
"//mt{s}.googleapis.com/vt?lyrs=h&x={x}&y={y}&z={z}"

11.
"Google satellite"
"//khm{s}.googleapis.com/kh?v=142&x={x}&y={y}&z={z}"

12.
"Google roadmap"
"//mt{s}.googleapis.com/vt?x={x}&y={y}&z={z}"

13.
"Google road & terrain layers, with high-res (Retina) support"
roadmap:
"//mt{s}.googleapis.com/vt?x={x}&y={y}&z={z}"
terrain:
"//mt{s}.googleapis.com/vt?lyrs=t,r&x={x}&y={y}&z={z}"

14.
"MapBox Warden (zoom 0..17)"
"//{s}.tiles.mapbox.com/v3/mapbox.mapbox-warden/{z}/{x}/{y}.png"
"Tiles by <a target="attr" href="http://mapbox.com">MapBox</a>. Map data &copy; <a href="http://openstreetap.org">OpenStreetMap</a> and contributors"

15.
"MapQuest Hybrid"
base:
"//mtile0{s}.mqcdn.com/tiles/1.0.0/vy/sat/{z}/{x}/{y}.png"
overlay:
"//mtile0{s}.mqcdn.com/tiles/1.0.0/vy/hyb/{z}/{x}/{y}.png"
"Tiles by <a target="attr" href="http://mapquest.com">MapQuest</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

16.
"MapQuest rendering of OpenStreetMap, as well as Aerial layer"
"//mtile0{s}.mqcdn.com/tiles/1.0.0/vy/sat/{z}/{x}/{y}.png"
"//mtile0{s}.mqcdn.com/tiles/1.0.0/vy/map/{z}/{x}/{y}.png"

17.
"OSM Thunderforest Cycle (zoom 0..18)"
"//{s}.tile.opencyclemap.org/cycle/{z}/{x}/{y}.png"
"Thunderforest <a target="attr" href="http://thunderforest.com">OpenCycleMap</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

18.
"OpenCycleMap with train & tram lines"
"http://{s}.tile2.opencyclemap.org/transport/{z}/{x}/{y}.png"
"Thunderforest <a target="attr" href="http://thunderforest.com">OpenCycleMap</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

19. 
"OpenStreetMap for skiers with 3 layers"
base:
"//{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
"<a target="attr" href="http://openpistemap.org">OpenPisteMap</a> Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
piste:
"http://tiles.openpistemap.org/nocontours/{z}/{x}/{y}.png"
"<a target="attr" href="http://openpistemap.org">OpenPisteMap</a> Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
relief shading:
"//tiles.openpistemap.org/landshaded/{z}/{x}/{y}.png"
"<a target="attr" href="http://openpistemap.org">OpenPisteMap</a> Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

20.
"OpenStreetMap with sea marks overlay"
base:
"//{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
sea marks:
"//tiles.openseamap.org/seamark/{z}/{x}/{y}.png"
"OpenSeaMap. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

21.
"Precipitation with pressure contours"
base:
"//{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"

precipitation:
"OpenWeatherMap. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
"//{s}.tile.openweathermap.org/map/precipitation/{z}/{x}/{y}.png"

pressure:
"OpenWeatherMap. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
"//{s}.tile.openweathermap.org/map/pressure_cntr/{z}/{x}/{y}.png"

22.
"Neutral basemap" - "Geocommons Acetate (zoom 2..18)"
"//a{s}.acetate.geoiq.com/tiles/acetate/{z}/{x}/{y}.png"
"Tiles by Geocommons &copy; <a target="attr" href="http://geocommons.com/overlays/acetate">Esri & Stamen</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"

23.
"Skobbler maps (zoom 0..18)"
"<a target="attr" href="http://maps.skobbler.com">Skobbler</a> Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
"//tiles{s}.api.skobbler.net/tiles/{z}/{x}/{y}.png?api_key=feb1b1f7e89cada111d31772f0206bf4"

24.
"Stamen Toner (zoom 0..18) layer."
"Tiles by <a target="attr" href="http://stamen.com">Stamen Design</a> under <a href="http://creativecommons.org/licenses/by/3.0">CC BY 3.0</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
"//{s}.tile.stamen.com/toner/{z}/{x}/{y}.png"

25.
"Stamen Watercolor (zoom 0..18) layer."
"Tiles by <a target="attr" href="http://stamen.com">Stamen Design</a> under <a href="http://creativecommons.org/licenses/by/3.0">CC BY 3.0</a>. Map data &copy; <a target="attr" href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors"
"//{s}.tile.stamen.com/watercolor/{z}/{x}/{y}.png"

26.
"Yandex Maps (zoom 0..7 in many areas)"
people layer:
"//0{s}.pvec.maps.yandex.net/tiles?l=pmap&x={x}&y={y}&z={z}"
"Map data &copy; <a target="attr" href="http://maps.yandex.ru">Yandex.Maps</a>"
road layer:
"//vec0{s}.maps.yandex.net/tiles?l=map&x={x}&y={y}&z={z}"
sat:
"//sat0{s}.maps.yandex.net/tiles?l=sat&x={x}&y={y}&z={z}"
```

## Interesting calcs for quad and not only ##

https://bitbucket.org/striker2000/mapius-maps/commits/738cfae02aa75a462d97c6cb8c62dd91