# Weather #

### Common notes ###

  * Looks like it is better to show wind only for hourly forecast a day ahead, but avoid doing it for a daily forecast a week ahead.


http://api.openweathermap.org/data/2.5/forecast?lat=35&lon=139&units=metric&lang=ru

http://metwit.com/weather-api/

### Checking if key for value exists ###
```
BOOL supportsSomeKey = YES;
@try
{
    [object valueForKey:somekey];
}
@catch (NSException *e)
{
    if ([[e name] isEqualTo:NSUndefinedKeyException])
    {
        supportsSomeKey = NO;
    }
}
```

### For complex paths ###

Similarly, valueForKeyPath: returns the value for the specified key path, relative to the receiver. Any object in the key path sequence that is not key-value coding compliant for the appropriate key receives a valueForUndefinedKey: message.


## NOAA ##

http://graphical.weather.gov/xml/rest.php

http://umcs.maine.edu/~wlamond/pdf/w-lamond-ios-report.pdf

_geo code for US? if US, use NOAA, else use openweathermap_

Aviation: http://www.aviationweather.gov/adds/


### Example for daily forecast 24 hours ###

http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdBrowserClientByDay.php?lat=38.99&lon=-77.01&format=24+hourly&numDays=7

Response:
```
<dwml xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1.0" xsi:noNamespaceSchemaLocation="http://www.nws.noaa.gov/forecasts/xml/DWMLgen/schema/DWML.xsd">
<head>
<product srsName="WGS 1984" concise-name="dwmlByDay" operational-mode="official">
<title>
NOAA's National Weather Service Forecast by 24 Hour Period
</title>
<field>meteorological</field>
<category>forecast</category>
<creation-date refresh-frequency="PT1H">2014-01-29T13:41:38Z</creation-date>
</product>
<source>
<more-information>http://www.nws.noaa.gov/forecasts/xml/</more-information>
<production-center>
Meteorological Development Laboratory
<sub-center>Product Generation Branch</sub-center>
</production-center>
<disclaimer>http://www.nws.noaa.gov/disclaimer.html</disclaimer>
<credit>http://www.weather.gov/</credit>
<credit-logo>http://www.weather.gov/images/xml_logo.gif</credit-logo>
<feedback>http://www.weather.gov/feedback.php</feedback>
</source>
</head>
<data>
<location>
<location-key>point1</location-key>
<point latitude="38.99" longitude="-77.01"/>
</location>
<moreWeatherInformation applicable-location="point1">
http://forecast.weather.gov/MapClick.php?textField1=38.99&textField2=-77.01
</moreWeatherInformation>
<time-layout time-coordinate="local" summarization="24hourly">
<layout-key>k-p24h-n7-1</layout-key>
<start-valid-time>2014-01-29T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-30T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-30T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-31T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-31T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-01T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-01T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-02T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-02T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-03T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-03T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-04T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-04T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-05T06:00:00-05:00</end-valid-time>
</time-layout>
<time-layout time-coordinate="local" summarization="12hourly">
<layout-key>k-p12h-n14-2</layout-key>
<start-valid-time>2014-01-29T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-29T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-29T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-30T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-30T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-30T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-30T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-31T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-31T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-31T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-31T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-01T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-01T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-01T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-01T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-02T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-02T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-02T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-02T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-03T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-03T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-03T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-03T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-04T06:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-04T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-04T18:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-04T18:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-05T06:00:00-05:00</end-valid-time>
</time-layout>
<time-layout time-coordinate="local" summarization="24hourly">
<layout-key>k-p7d-n1-3</layout-key>
<start-valid-time>2014-01-29T06:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-05T06:00:00-05:00</end-valid-time>
</time-layout>
<parameters applicable-location="point1">
<temperature type="maximum" units="Fahrenheit" time-layout="k-p24h-n7-1">
<name>Daily Maximum Temperature</name>
<value>22</value>
<value>30</value>
<value>43</value>
<value>49</value>
<value>42</value>
<value>39</value>
<value>39</value>
</temperature>
<temperature type="minimum" units="Fahrenheit" time-layout="k-p24h-n7-1">
<name>Daily Minimum Temperature</name>
<value>13</value>
<value>22</value>
<value>30</value>
<value>34</value>
<value>28</value>
<value>28</value>
<value xsi:nil="true"/>
</temperature>
<probability-of-precipitation type="12 hour" units="percent" time-layout="k-p12h-n14-2">
<name>12 Hourly Probability of Precipitation</name>
<value>12</value>
<value>0</value>
<value>0</value>
<value>5</value>
<value>11</value>
<value>9</value>
<value>5</value>
<value>13</value>
<value>14</value>
<value>8</value>
<value>26</value>
<value>14</value>
<value>40</value>
<value xsi:nil="true"/>
</probability-of-precipitation>
<weather time-layout="k-p24h-n7-1">
<name>Weather Type, Coverage, and Intensity</name>
<weather-conditions weather-summary="Becoming Sunny"/>
<weather-conditions weather-summary="Partly Sunny"/>
<weather-conditions weather-summary="Mostly Cloudy"/>
<weather-conditions weather-summary="Mostly Cloudy"/>
<weather-conditions weather-summary="Mostly Cloudy"/>
<weather-conditions weather-summary="Chance Rain/Snow">
<value coverage="chance" intensity="light" weather-type="rain" qualifier="none"/>
<value coverage="chance" intensity="light" additive="and" weather-type="snow" qualifier="none"/>
</weather-conditions>
<weather-conditions weather-summary="Chance Rain/Snow">
<value coverage="chance" intensity="light" weather-type="rain" qualifier="none"/>
<value coverage="chance" intensity="light" additive="and" weather-type="snow" qualifier="none"/>
</weather-conditions>
</weather>
<conditions-icon type="forecast-NWS" time-layout="k-p24h-n7-1">...</conditions-icon>
<hazards time-layout="k-p7d-n1-3">
<name>Watches, Warnings, and Advisories</name>
<hazard-conditions xsi:nil="true"/>
</hazards>
</parameters>
</data>
</dwml>
```

**Has no wind information!**

So it looks like that not daily, but detailed client should be used:

http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php?lat=38.99&lon=-77.01&product=time-series&Unit=m&begin=2014-01-29T00:00:00&end=2014-2-6T00:00:00&maxt=maxt&mint=mint&wspd=wspd&wdir=wdir

With this one you can add parameters (like above for a wind speed and direction. Here is example of a response:

```
<dwml xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1.0" xsi:noNamespaceSchemaLocation="http://www.nws.noaa.gov/forecasts/xml/DWMLgen/schema/DWML.xsd">
<head>
<product srsName="WGS 1984" concise-name="time-series" operational-mode="official">
<title>NOAA's National Weather Service Forecast Data</title>
<field>meteorological</field>
<category>forecast</category>
<creation-date refresh-frequency="PT1H">2014-01-29T14:02:25Z</creation-date>
</product>
<source>
<more-information>http://www.nws.noaa.gov/forecasts/xml/</more-information>
<production-center>
Meteorological Development Laboratory
<sub-center>Product Generation Branch</sub-center>
</production-center>
<disclaimer>http://www.nws.noaa.gov/disclaimer.html</disclaimer>
<credit>http://www.weather.gov/</credit>
<credit-logo>http://www.weather.gov/images/xml_logo.gif</credit-logo>
<feedback>http://www.weather.gov/feedback.php</feedback>
</source>
</head>
<data>
<location>
<location-key>point1</location-key>
<point latitude="38.99" longitude="-77.01"/>
</location>
<moreWeatherInformation applicable-location="point1">
http://forecast.weather.gov/MapClick.php?textField1=38.99&textField2=-77.01
</moreWeatherInformation>
<time-layout time-coordinate="local" summarization="none">
<layout-key>k-p24h-n7-1</layout-key>
<start-valid-time>2014-01-29T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-29T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-30T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-30T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-31T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-31T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-01T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-01T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-02T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-02T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-03T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-03T19:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-04T07:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-04T19:00:00-05:00</end-valid-time>
</time-layout>
<time-layout time-coordinate="local" summarization="none">
<layout-key>k-p24h-n6-2</layout-key>
<start-valid-time>2014-01-29T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-30T08:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-30T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-01-31T08:00:00-05:00</end-valid-time>
<start-valid-time>2014-01-31T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-01T08:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-01T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-02T08:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-02T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-03T08:00:00-05:00</end-valid-time>
<start-valid-time>2014-02-03T19:00:00-05:00</start-valid-time>
<end-valid-time>2014-02-04T08:00:00-05:00</end-valid-time>
</time-layout>
<time-layout time-coordinate="local" summarization="none">
<layout-key>k-p3h-n36-3</layout-key>
<start-valid-time>2014-01-29T10:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-29T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-29T16:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-29T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-29T22:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T04:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T10:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T16:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-30T22:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T04:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T10:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T16:00:00-05:00</start-valid-time>
<start-valid-time>2014-01-31T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-01T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-01T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-01T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-01T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-02T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-02T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-02T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-02T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-03T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-03T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-03T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-03T19:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-04T01:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-04T07:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-04T13:00:00-05:00</start-valid-time>
<start-valid-time>2014-02-04T19:00:00-05:00</start-valid-time>
</time-layout>
<parameters applicable-location="point1">
<temperature type="maximum" units="Celsius" time-layout="k-p24h-n7-1">
<name>Daily Maximum Temperature</name>
<value>-6</value>
<value>-1</value>
<value>6</value>
<value>9</value>
<value>6</value>
<value>4</value>
<value>4</value>
</temperature>
<temperature type="minimum" units="Celsius" time-layout="k-p24h-n6-2">
<name>Daily Minimum Temperature</name>
<value>-11</value>
<value>-6</value>
<value>-1</value>
<value>1</value>
<value>-2</value>
<value>-2</value>
</temperature>
<wind-speed type="sustained" units="meters/second" time-layout="k-p3h-n36-3">
<name>Wind Speed</name>
<value>5</value>
<value>6</value>
<value>5</value>
<value>4</value>
<value>3</value>
<value>3</value>
<value>1</value>
<value>0</value>
<value>1</value>
<value>3</value>
<value>3</value>
<value>3</value>
<value>3</value>
<value>4</value>
<value>3</value>
<value>2</value>
<value>3</value>
<value>3</value>
<value>1</value>
<value>1</value>
<value>1</value>
<value>2</value>
<value>4</value>
<value>4</value>
<value>3</value>
<value>3</value>
<value>5</value>
<value>2</value>
<value>2</value>
<value>3</value>
<value>1</value>
<value>1</value>
<value>0</value>
<value>1</value>
<value>2</value>
<value>2</value>
</wind-speed>
<direction type="wind" units="degrees true" time-layout="k-p3h-n36-3">
<name>Wind Direction</name>
<value>310</value>
<value>290</value>
<value>290</value>
<value>290</value>
<value>290</value>
<value>300</value>
<value>290</value>
<value>280</value>
<value>180</value>
<value>180</value>
<value>160</value>
<value>140</value>
<value>180</value>
<value>190</value>
<value>200</value>
<value>210</value>
<value>220</value>
<value>220</value>
<value>220</value>
<value>150</value>
<value>120</value>
<value>110</value>
<value>190</value>
<value>200</value>
<value>220</value>
<value>300</value>
<value>330</value>
<value>340</value>
<value>340</value>
<value>350</value>
<value>340</value>
<value>0</value>
<value>50</value>
<value>50</value>
<value>80</value>
<value>80</value>
</direction>
</parameters>
</data>
</dwml>
```

Here is a list of all possible params for a detailed client:

http://graphical.weather.gov/xml/docs/elementInputNames.php

**What looks really cool with NOAA is that I can combine a single request with params that will return me daily and hourly forecasts with all the data I need**
_But actually, probably not. The problem might be then to deduce a single icon for a whole day and calculate properly min/max temperatures. There is no way how to include any params in daily client, thus will go with two calls still - one for hourly and one for daily_

### Most probably what I need for hourly forecast ###

http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php?lat=38.99&lon=-77.01&product=time-series&Unit=m&begin=2014-01-29T00:00:00&end=2014-2-6T00:00:00&maxt=maxt&mint=mint&wspd=wspd&wdir=wdir&temp=temp&icons=icons

24 hours ahead of now will be enough for me

### Example for forecast, limited to mint and maxt ###

http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php?lat=38.99&lon=-77.01&product=time-series&begin=2014-01-29T00:00:00&end=2014-2-6T00:00:00&maxt=maxt&mint=mint

### US geo bounds ###

Coordinates for North America:
```
var strictBounds = new google.maps.LatLngBounds(
    new google.maps.LatLng(28.70, -127.50), 
    new google.maps.LatLng(48.85, -55.90)
);
```
weather.io: https://developer.forecast.io/

diy: http://sietse.net/iwdl-info/

### Norwegian, looks good ###

http://api.yr.no/weatherapi/documentation

http://api.yr.no/weatherapi/locationforecast/1.8/documentation

#### Sample ####
http://api.yr.no/weatherapi/locationforecast/1.8/?lat=50.051;lon=14.45

Looks really good, only following issues:
1. Looks like record for the current time is never present. So forecast starts from now + nearest 3 hours point. Like at 1:14pm, it will start from 3pm. I'd need to get the current conditions from somewhere else.
2. As it gives everything in hourly intervals, I'd have to calculate myself accumulated values as mint and maxt and probably avg for wind.


_Otherwise gives wind and many more for 10 days ahead with hourly interval_

_Values for percipitation are given in some irregular intervals. I'd have to study more to see how that can be used to be shown. Though it can be quite interesting as they look to provide those intervals in period for t1-t2 "SNOW" t3-t4 "RAIN" - can be otherwise quite precise and interesting, but I need to calculate myself the accumulated weather condition_

Response sample:

```
<weatherdata xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="http://api.met.no/weatherapi/locationforecast/1.8/schema" created="2014-01-30T12:05:52Z">
<meta>
<model name="EC.GEO.0.25" termin="2014-01-30T00:00:00Z" runended="2014-01-30T07:03:32Z" nextrun="2014-01-30T20:00:00Z" from="2014-01-30T15:00:00Z" to="2014-02-09T00:00:00Z"/>
</meta>
<product class="pointData">
<time datatype="forecast" from="2014-01-30T15:00:00Z" to="2014-01-30T15:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<temperature id="TTT" unit="celcius" value="-0.2"/>
<windDirection id="dd" deg="108.8" name="E"/>
<windSpeed id="ff" mps="3.4" beaufort="2" name="Svak vind"/>
<humidity value="69.0" unit="percent"/>
<pressure id="pr" unit="hPa" value="1013.2"/>
<cloudiness id="NN" percent="100.0"/>
<fog id="FOG" percent="0.0"/>
<lowClouds id="LOW" percent="0.0"/>
<mediumClouds id="MEDIUM" percent="24.2"/>
<highClouds id="HIGH" percent="100.0"/>
<dewpointTemperature id="TD" unit="celcius" value="-5.4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T12:00:00Z" to="2014-01-30T15:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T09:00:00Z" to="2014-01-30T15:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T18:00:00Z" to="2014-01-30T18:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<temperature id="TTT" unit="celcius" value="-1.1"/>
<windDirection id="dd" deg="104.8" name="E"/>
<windSpeed id="ff" mps="3.9" beaufort="3" name="Lett bris"/>
<humidity value="76.9" unit="percent"/>
<pressure id="pr" unit="hPa" value="1012.9"/>
<cloudiness id="NN" percent="100.0"/>
<fog id="FOG" percent="0.0"/>
<lowClouds id="LOW" percent="44.5"/>
<mediumClouds id="MEDIUM" percent="26.6"/>
<highClouds id="HIGH" percent="100.0"/>
<dewpointTemperature id="TD" unit="celcius" value="-4.9"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T15:00:00Z" to="2014-01-30T18:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T12:00:00Z" to="2014-01-30T18:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T21:00:00Z" to="2014-01-30T21:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<temperature id="TTT" unit="celcius" value="-0.0"/>
<windDirection id="dd" deg="116.4" name="SE"/>
<windSpeed id="ff" mps="3.7" beaufort="3" name="Lett bris"/>
<humidity value="76.1" unit="percent"/>
<pressure id="pr" unit="hPa" value="1011.5"/>
<cloudiness id="NN" percent="100.0"/>
<fog id="FOG" percent="0.0"/>
<lowClouds id="LOW" percent="1.6"/>
<mediumClouds id="MEDIUM" percent="86.7"/>
<highClouds id="HIGH" percent="99.2"/>
<dewpointTemperature id="TD" unit="celcius" value="-4.0"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T18:00:00Z" to="2014-01-30T21:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
<time datatype="forecast" from="2014-01-30T15:00:00Z" to="2014-01-30T21:00:00Z">
<location altitude="315" latitude="50.0510" longitude="14.4500">
<precipitation unit="mm" value="0.0"/>
<symbol id="CLOUD" number="4"/>
</location>
</time>
```

## XML to NSDictionary ##

http://ios.biomsoft.com/2011/09/11/simple-xml-to-nsdictionary-converter/

### Metar & Metaf parser ###

They mention NOAA as well in the demo. Looks like specific to pilots weather information. This is sourceforge, so look for a code as well!

http://metaf2xml.sourceforge.net/gui.html?lang=en


## SO - a good one ##

http://stackoverflow.com/questions/5775488/to-display-weather-forecast-for-currentday-next-day-and-day-after-next-day-in-ip

### Icons ###

http://line25.com/tutorials/how-to-create-a-set-of-vector-weather-line-icons


# Stock #

http://www.blogbyben.com/2009/01/truly-simple-stock-api.html

http://www.gummy-stuff.org/Yahoo-data.htm

http://finance.google.com/finance/info?client=ig&q=NASDAQ%3aGOOG

http://stackoverflow.com/questions/10040954/alternative-to-google-finance-api

stock news and much more: https://developers.tradeking.com/documentation/market-news-search-get-post

http://www.financialcontent.com/


# Exchange rates #

http://openexchangerates.org/api/latest.json?app_id=


# Twitter #

https://developer.apple.com/library/ios/documentation/Social/Reference/SLRequest_Class/Reference/Reference.html#//apple_ref/doc/uid/TP40012234

https://dev.twitter.com/docs/ios

https://dev.twitter.com/docs/ios/making-api-requests-slrequest


```
//include twitter.framework 
#import <Twitter/Twitter.h>

+ (void)getTweetsFortwitterID:(NSString *)twitterID

{
    if(twitterID.length >0)
    {
    NSString * finalURL = [NSString stringWithFormat:@"https://api.twitter.com/1.1/statuses/user_timeline.json?include_entities=true&include_rts=true&screen_name=%@&count=10", twitterID];

    TWRequest *postRequest = [[TWRequest alloc] initWithURL:[NSURL URLWithString:finalURL] parameters:nil requestMethod:TWRequestMethodGET];

    ACAccountStore *accountStore = [[ACAccountStore alloc] init] ;

    ACAccountType *accountType =  [accountStore accountTypeWithAccountTypeIdentifier:ACAccountTypeIdentifierTwitter];

        // Request access from the user to use their Twitter accounts.
        [accountStore requestAccessToAccountsWithType:accountType withCompletionHandler:^(BOOL granted, NSError *error)
         {
             if(granted)
             {
                 NSArray *twitterAccounts = [accountStore accountsWithAccountType:accountType];

                 if([twitterAccounts count] >0
                    )
                 {
                 ACAccount *twitterAccount = [twitterAccounts objectAtIndex:0];
                 [postRequest setAccount:twitterAccount];

                 NSLog(@"request.account:%@",postRequest.account);

                 // Perform the request created above and create a handler block to handle the response.
                 NSMutableArray *tweetsArray=[[NSMutableArray alloc]init];

                 [postRequest performRequestWithHandler:^(NSData *responseData, NSHTTPURLResponse *urlResponse, NSError *error) {

                     // Parse the responseData, which we asked to be in JSON format for this request, into an NSDictionary using NSJSONSerialization.
                     NSArray *publicTimeline = nil;
                     NSError *jsonParsingError = nil;
                     if (responseData)
                     {
                         publicTimeline = [NSJSONSerialization JSONObjectWithData:responseData options:0 error:&jsonParsingError];
                         NSLog(@"publicTimeline : %@", publicTimeline);
                     }

                     if ([publicTimeline isKindOfClass:[NSArray class]])
                     {

                         for (int i =0; i<[publicTimeline count]; i++)
                         {
                             NSMutableDictionary *twitterDict=[[NSMutableDictionary alloc]init];

                             if ([[publicTimeline objectAtIndex:i] objectForKey:@"text"])
                             {
                                 NSLog(@"ID: %@", [[publicTimeline objectAtIndex:i] objectForKey:@"text"]);
                                 [twitterDict setObject:[[publicTimeline objectAtIndex:i] objectForKey:@"text"] forKey:@"text"];
                             }
                             if ([[publicTimeline objectAtIndex:i] objectForKey:@"created_at"])
                             {
                                 NSLog(@"ID: %@", [[publicTimeline objectAtIndex:i] objectForKey:@"created_at"]);
                                 [twitterDict setObject:[[publicTimeline objectAtIndex:i] objectForKey:@"created_at"]
                                                 forKey:@"created_at"];
                             }

                             if ([[publicTimeline objectAtIndex:i] objectForKey:@"user"])
                             {
                                 NSLog(@"ID: %@", [[publicTimeline objectAtIndex:i] objectForKey:@"created_at"]);
                                 [twitterDict setObject:[[[publicTimeline objectAtIndex:i] objectForKey:@"user"]objectForKey:@"profile_image_url"]
                                                 forKey:@"profile_image_url"];
                             }


                             [tweetsArray addObject:twitterDict];
                             NSLog(@"tweets:%@", tweetsArray);

                         }
                     }

                     if([tweetsArray count]>0)
                         [[NSNotificationCenter defaultCenter] postNotificationName:@"tweetsLoaded" object:tweetsArray];


                 }];
                 }

             }

         }];
    }


}
```


## Russian news on twitter ##

https://twitter.com/russkie_novosti

https://twitter.com/sportnewsrus

## French news on twitter ##

https://twitter.com/LNEcanal

# Facebook infotainment #

http://codehunk.blogspot.cz/2014/01/ios-6-facebook-integration-tutorial-how.html