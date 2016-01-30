Garmin extension for speed etc:
```
<trkpt lat="38.900207" lon="-77.008990">
<ele>11.67</ele><time>2013-06-11T04:04:49Z</time>
<extensions>
   <gpxtpx:TrackPointExtension><gpxtpx:speed>6.86</gpxtpx:speed> 
   <gpxtpx:course>271.06</gpxtpx:course>
   </gpxtpx:TrackPointExtension>
   </extensions></trkpt>
```

Sample showing garmin extensions namespace:
```
<gpx xmlns="http://www.topografix.com/GPX/1/1" xmlns:gpxx="http://www.garmin.com/xmlschemas/GpxExtensions/v3" xmlns:gpxtpx="http://www.garmin.com/xmlschemas/TrackPointExtension/v1" creator="Oregon 400t" version="1.1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd http://www.garmin.com/xmlschemas/GpxExtensions/v3 http://www.garmin.com/xmlschemas/GpxExtensionsv3.xsd http://www.garmin.com/xmlschemas/TrackPointExtension/v1 http://www.garmin.com/xmlschemas/TrackPointExtensionv1.xsd">
  <metadata>
    <link href="http://www.garmin.com">
      <text>Garmin International</text>
    </link>
    <time>2009-10-17T22:58:43Z</time>
  </metadata>
  <trk>
    <name>Example GPX Document</name>
    <trkseg>
      <trkpt lat="47.644548" lon="-122.326897">
        <ele>4.46</ele>
        <time>2009-10-17T18:37:26Z</time>
      </trkpt>
    </trkseg>
  </trk>
</gpx>
```


I'm using a GPS Datalogger to log my running sessions (time, distance, speed…). I can import a GPX into Sportstracker.
But the speed and the distance is not read from the GPX file. Does this is a normal way the software works, or something is wrong with my file ?
This is an extract of the GPX file :
```
<trkpt lat="47.179443333" lon="-1.573975000">
  <ele>134.437900</ele>
  <time>2011-09-25T18:04:55Z</time>
  <course>344.789978</course>
  <speed>2.884012</speed>
  <fix>2d</fix>
  <hdop>4710.765137</hdop>
</trkpt>
<trkpt lat="47.179463333" lon="-1.573978333">
  <ele>134.438500</ele>
  <time>2011-09-25T18:04:56Z</time>
  <course>353.536865</course>
  <speed>2.240630</speed>
  <fix>2d</fix>
  <hdop>4710.766602</hdop>
</trkpt>
<trkpt lat="47.179483333" lon="-1.573986667">
  <ele>134.438700</ele>
  <time>2011-09-25T18:04:57Z</time>
  <course>344.187347</course>
  <speed>2.313955</speed>
  <fix>2d</fix>
  <hdop>4710.767578</hdop>
</trkpt>
<trkpt lat="47.179508333" lon="-1.574001667">
  <ele>134.439200</ele>
  <time>2011-09-25T18:04:58Z</time>
  <course>337.813263</course>
  <speed>3.005522</speed>
  <fix>2d</fix>
  <hdop>4710.769043</hdop>
</trkpt>
```

Another extension sample:

```
<gpx xmlns="http://www.topografix.com/GPX/1/1" 
  xmlns:gpxdata="http://www.cluetrust.com/XML/GPXDATA/1/0" 
  creator="pytrainer http://sourceforge.net/projects/pytrainer" version="1.1">
  <metadata>
    <name>Run 2009-06-19</name>
    <link href="http://sourceforge.net/projects/pytrainer"/>
    <time>2009-06-19T10:13:04Z</time>
  </metadata>
  <trk>
    <trkseg>
      <trkpt lat="51.219983" lon="6.765224">
        <ele>52.048584</ele>
        <time>2009-06-19T10:13:04Z</time>
        <extensions>
          <gpxdata:hr>164</gpxdata:hr>
          <gpxdata:cadence>99</gpxdata:cadence> 
        </extensions>
      </trkpt>
      <trkpt lat="51.220137" lon="6.765098">
        <ele>52.529175</ele>
        <time>2009-06-19T10:13:09Z</time>
        <extensions>
          <gpxdata:hr>161</gpxdata:hr>
          <gpxdata:cadence>95</gpxdata:cadence> 
        </extensions>
      </trkpt>
      <trkpt lat="51.219983" lon="6.765224">
        <ele>52.048584</ele>
        <time>2009-06-19T10:13:15Z</time>
        <extensions>
          <gpxdata:hr>164</gpxdata:hr>
          <gpxdata:cadence>99</gpxdata:cadence> 
        </extensions>
      </trkpt>
      <trkpt lat="51.220137" lon="6.765098">
        <ele>52.529175</ele>
        <time>2009-06-19T10:13:19Z</time>
        <extensions>
          <gpxdata:hr>161</gpxdata:hr>
          <gpxdata:cadence>96</gpxdata:cadence> 
        </extensions>
      </trkpt>
    </trkseg>
  </trk>
  <extensions> 
    <gpxdata:lap>
      <gpxdata:index>1</gpxdata:index>
      <gpxdata:startPoint lat="51.219983" lon="6.765224"/>
      <gpxdata:endPoint lat="51.220137" lon="6.765098" />
      <gpxdata:startTime>2009-06-19T10:13:04Z</gpxdata:startTime>
      <gpxdata:elapsedTime>4.6700000</gpxdata:elapsedTime>
      <gpxdata:calories>1</gpxdata:calories>
      <gpxdata:distance>0.5881348</gpxdata:distance>
      <gpxdata:summary name="AverageHeartRateBpm" kind="avg">163</gpxdata:summary>
      <gpxdata:trigger kind="manual" />
      <gpxdata:intensity>active</gpxdata:intensity>
    </gpxdata:lap>
    <gpxdata:lap>
      <gpxdata:index>2</gpxdata:index>
      <gpxdata:startPoint lat="51.219983" lon="6.765224"/>
      <gpxdata:endPoint lat="51.220137" lon="6.765098" />
      <gpxdata:startTime>2009-06-19T10:13:15Z</gpxdata:startTime>
      <gpxdata:elapsedTime>4.6700000</gpxdata:elapsedTime>
      <gpxdata:calories>1</gpxdata:calories>
      <gpxdata:distance>0.5881348</gpxdata:distance>
      <gpxdata:summary name="AverageHeartRateBpm" kind="avg">163</gpxdata:summary>
      <gpxdata:trigger kind="manual" />
      <gpxdata:intensity>active</gpxdata:intensity>
    </gpxdata:lap>
  </extensions>
</gpx>
```