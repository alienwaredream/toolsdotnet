
```
-- drop table raw_points
-- create table raw_points (gid bigserial primary key, stamp timestamp with time zone NOT NULL DEFAULT now(), d json[] not null); 
-- another way: insert into test(ja) values ('{"{\"name\":\"alex\", \"age\":20}", "{\"name\":\"peter\", \"age\":24}"}');
-- yet another way: INSERT INTO test(ja) VALUES( CAST (ARRAY['{"name":"Alex", "age":20}', '{"name":"Peter", "age":24}'] as JSON[]) ); -- Works FINE
INSERT INTO raw_points (d) VALUES( CAST (ARRAY['{"lat":"12.33", "long":"23.44","limit":"10"}', '{"lat":"13.33", "long":"23.44","limit":"22"}'] as JSON[]) )

--insert into raw_points (d) values (array['{"lat":"12.33", "long":"23.44","limit":"10"}'::json, '{"lat":"13.33", "long":"23.44","limit":"22"}'::json]);

select d from raw_points

select * from json_populate_recordset(null::points, (select d from raw_points))

insert into points select * from json_populate_recordset(null::points, '[{"lat":1,"long":2},{"lat":3,"long":4}]')

select * from points
```

curl for inserting the points from json array:
```
curl -X POST http://apps.blocoware.com/addlimits -d json=[{\"lat\":"12.22"\,\"long\":44.33\,\"uname\":\"standa_web\"},{\"long\":33.44}]
```

configuration:
```
location /addlimits {
        postgres_pass database;
        rds_json on;

        set_form_input $json json;
#       set_unescape_uri $ue_json $json;
postgres_escape $j $json;
      postgres_query
         POST "insert into limits select * from json_populate_recordset(null::limits, $j) returning *";
postgres_rewrite  POST changes 201;
}

```

## After PostGIS ##

New table:
```
CREATE TABLE limits(gid serial PRIMARY KEY, point  geography(POINT,4326), heading numeric, lim numeric, accuracy numeric(8,2), uname character varying(40), stamp timestamp with time zone NOT NULL default now());
```

Permissions:
```
limits=# GRANT SELECT ON limits TO limits;
GRANT
limits=# GRANT INSERT ON limits TO limits;
GRANT
limits=# GRANT USAGE, SELECT ON limits_gid_seq TO limits;
GRANT
limits=# \z
```

A json model table:
```
CREATE TABLE jlimits (lat numeric, long numeric, l numeric, a numeric, h numeric, n character varying(40));
```

nginx config:
```
POST "insert into limits (point, lim, accuracy, heading, uname)  select ST_MakePoint(lat,long), l, a, h, n from json_populate_recordset(null::jlimits, $j) returning *";
```

Curl:
```
curl -X POST http://apps.blocoware.com/addlimits -d json=[{\"lat\":"12.22"\,\"long\":44.33\,\"n\":\"s1\"\,\"l\":10.34\,\"a\":5\,\"h\":134},{\"lat\":"12.22"\,\"long\":44.33\,\"n\":\"s1\"\,\"l\":10.34\,\"a\":5\,\"h\":134}]
```

Select from db:
```
select ST_AsText(point) from limits;
```