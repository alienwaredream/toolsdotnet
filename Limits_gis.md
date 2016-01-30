## Queries ##

Select points around the coordinate and within distance:

```
SELECT ST_AsText(point), lim, heading  FROM limits  WHERE ST_DWithin(point, ST_MakePoint(12.00,44), 100000);
```

## GET ##

```
http://apps.blocoware.com/getpoints?lat=12.222&long=44.33&distance=400&ts=1399386522.05032
```

## nginx ##

```
location /getpoints {
        postgres_pass database;
        rds_json on;
postgres_escape $lat $arg_lat;
      postgres_escape $long  $arg_long;
        postgres_escape $distance $arg_distance;
#set_unescape_uri $ts $tsu;
postgres_escape $ts $arg_ts;
      postgres_query
        HEAD GET "SELECT * FROM getPoints ($lat, $long, $distance,$ts);";

postgres_rewrite  POST changes 201;
}
```

## Stored proc ##

```
--drop function getPoints(numeric, numeric, numeric, numeric);



CREATE or REPLACE FUNCTION getPoints(lat numeric, lng numeric, distance numeric, tsi numeric) 
RETURNS  TABLE(
    i integer,
    lt numeric,
	ln numeric,
	l numeric,
	h numeric,
	a numeric,
	t int,
ts numeric
) LANGUAGE plpgsql AS

$BODY$BEGIN



RETURN QUERY SELECT  gid as i, (ST_X(point::geometry))::numeric(8,6) as lt, (ST_Y(point::geometry))::numeric(9,6) as ln, lim as l, heading as h,accuracy as a, type as t, extract(epoch from stamp)::numeric FROM limits  WHERE ST_DWithin(point, ST_MakePoint(lat,lng), distance) AND stamp > (TIMESTAMP WITH TIME ZONE 'epoch' + tsi * INTERVAL '1 second');

END$BODY$

GRANT EXECUTE ON FUNCTION getPoints(numeric, numeric, numeric) TO limits;

select * from getPoints(12.22, 44.33001, 100, 1399386522.05031);

```

## Geo index ##

```
CREATE INDEX limits_point_gix ON limits  USING GIST ( point );
```

## Set timezone ##

http://www.christopherirish.com/2012/03/21/how-to-set-the-timezone-on-ubuntu-server/

## Example of geography vs. geometry calcs ##

```
limits=# SELECT ST_Distance('LINESTRING(-122.33 47.606, 0.0 51.5)'::geography, 'POINT(-21.96 -64.15)':: geography);
   st_distance
------------------
 12965127.0564067
(1 row)

limits=# SELECT ST_Distance('LINESTRING(-122.33 47.606, 0.0 51.5)'::geometry, 'POINT(-21.96 -64.15)':: geometry);
   st_distance
------------------
 114.892776750932
(1 row)
```

## For inspiration ##

```
SELECT * FROM restaurants WHERE ST_Distance_Sphere(geo, ST_GeomFromText(POINT(55.98767 57.12345), -1)) < 5000; what do I do on the iPhone?
```