## NMEA ##

## GPX ##

## KML ##

http://kmlframework.com/#code

### Touring with KML ###

https://developers.google.com/kml/documentation/touring

### Simple sample ###
```
<?xml version="1.0" encoding="UTF-8"?>
<kml xmlns="http://www.opengis.net/kml/2.2"
 xmlns:gx="http://www.google.com/kml/ext/2.2">   <!-- required when using gx-prefixed elements -->

<Placemark>
  <name>gx:altitudeMode Example</name>
  <LookAt>
    <longitude>146.806</longitude>
    <latitude>12.219</latitude>
    <heading>-60</heading>
    <tilt>70</tilt>
    <range>6300</range>
    <gx:altitudeMode>relativeToSeaFloor</gx:altitudeMode>
  </LookAt>
  <LineString>
    <extrude>1</extrude>
    <gx:altitudeMode>relativeToSeaFloor</gx:altitudeMode>
    <coordinates>
      146.825,12.233,400
      146.820,12.222,400
      146.812,12.212,400
      146.796,12.209,400
      146.788,12.205,400
    </coordinates>
  </LineString>
</Placemark>

</kml>
```

### Time in KML ###

http://earth.google.com/outreach/tutorial_time.html

### ON GPS Altitude Accuracy ###
```
GPS Altitude Readout > How Accurate? (rev. 2/10/01)
New GPS buyers are frequently concerned about the accuracy (or lack of it) of the altitude readout on their newly purchased GPS. Many suspect their equipment may even be defective when they see the altitude readout at a fixed point vary by many hundreds of feet. This is NORMAL.

With most low cost GPS receivers, the horizontal error (without SA now that it is off) is specified to be within about +/- 15 meters (50 feet) 95% of the time. Most users find this is a conservative specification and that their modern GPS receivers routinely perform better than this worst case specification. But.. Users should expect that SOMETIMES they may see the error approach the specification limits.   AND..  5% of the time,  the error may be "any value" from zero to whatever".  Note: Unless you have a CLEAR AND UNOBSTRUCTED view of the sky (on your dash or looking out of an airplane window with no externally mounted antenna, or similar obstructed view does not count!)  you can count on your error excursions to be much greater than the above numbers.   Your GPS <depends> on this clear and unobstructed view or it cannot make accurate range measurements to the satellites.

Generally,  Altitude error is specified to be 1.5 x Horizontal error specification.  This means that the user of standard consumer GPS receivers should consider +/-23meters (75ft) with a DOP of 1 for 95% confidence.  Altitude error is always considerably worse than the horizontal (position error). Much of this is a matter of geometry. If we (simplistically) consider just four satellites, the "optimum" configuration for best overall accuracy is having the four SVs at 40 to 55 degrees above the horizon and one (for instance) in each general direction N, E, W, and S.  (Note:  You will get a very BAD DOP if the SVs are at the exact same elevation.  Luckily,  this is a rare occurrence.)  See:  DOP demonstration site by Norris Weimer> How SV geometry affects GPS accuracy(Java Required)..  The similar "best" arrangement for vertical position is with one SV overhead and the others at the horizon and 120 degrees in azimuth apart. Obviously, this arrangement is very poor from a signal standpoint. As a result, of this geometry the calculated solution for altitude is not as accurate as it is for horizontal position.  Almost any calibrated altimeter will be more stable at reading altitude  than a GPS.

GPS altitude measures the users' distance from the center of the SVs orbits. These measurements are referenced to geodetic altitude or ellipsoidal altitude in some GPS equipment. Garmin and most equipment manufacturers utilize a mathematical model in the GPS software which roughly approximates the geodetic model of the earth and reference altitude to this model. As with any model, there will be errors as the earth is not a simple mathematical shape to represent.  What this means is that if you are walking on the seashore,  and see your altitude as -15 meters,  you should not be concerned.  First,  the geodetic model of the earth can have much more than this amount of error at any specific point and Second,  you have the GPS error itself to add in.  As a result of this combined error,  I am not surprised to be at the seashore and see -40 meter errors in some spots.

DGPS operation (where available) will dramatically improve the performance of even low cost GPS receivers. Horizontal accuracy of +/- 5 meters and altitude accuracy of +/- 10 meters (relative to the WGS-84 geode) with suitable DGPS receivers and low cost GPS receivers such as the Garmin GPS-12XL can be expected.

In any case, it is extremely unwise to overly depend on the altitude readout of a GPS. Those who use GPS altitude to aid in landing their small plane should have their insurance policies paid up at all times.

Joe Mehaffey 
```

## Spatial things ##
Super one for calculating the bounding coordinates, distance and retreiving from sql hopefully based on index

http://janmatuschek.de/LatitudeLongitudeBoundingCoordinates

Spatial lite for iOS:
http://stackoverflow.com/questions/4793970/how-to-compile-spatialite-for-ios