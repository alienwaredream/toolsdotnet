## Users ##

```

#set user password, this is a purely db user, there is no corresponding system user
limits=# ALTER USER limits WITH PASSWORD 'xyz'


limits=# GRANT SELECT ON limits TO limits;
limits=# GRANT UPDATE ON limits TO limits;
# to list rights
limits=# \z
# or from the command line
sudo -u postgres psql -d limits -c 'GRANT INSERT ON limits TO limits;'
```

https://www.digitalocean.com/community/articles/how-to-use-roles-and-manage-grant-permissions-in-postgresql-on-a-vps--2



## Connecting with rds\_json and postgresql ##

Experimenting with GETs
```
location /limits {
      postgres_pass database;
      rds_json on;
      postgres_query    HEAD GET  "SELECT * FROM limits";
    }
location /addlimit {
        postgres_pass database;
        rds_json on;
postgres_escape $lat $arg_lat;
      postgres_escape $long  $arg_long;
        postgres_escape $course $arg_course;
        postgres_escape $speed $arg_speed;
        postgres_escape $uname $arg_uname;
      postgres_query
        HEAD GET "INSERT INTO limits (lat, long, course, speed, uname) VALUES($lat, $long, $course, $speed, $uname) RETURNING *";
      postgres_rewrite  POST changes 201;
}
```

Sample for post

```
postgres_escape $lat $arg_lat;
      postgres_escape $long  $arg_long;
        postgres_escape $course $arg_course;
        postgres_escape $speed $arg_speed;
        postgres_escape $uname $arg_uname;
      postgres_query
        POST "INSERT INTO limits (lat, long, course, speed, uname) VALUES($lat, $long, $course, $speed, $uname) RETURNING *";
      postgres_rewrite  POST changes 201;
```

## Test urls ##

http://apps.blocoware.com/addlimit?uname=stannn&lat=12.22&long=44.4&course=12.55&speed=23.1

http://apps.blocoware.com/limits

clean data

```
sudo -u postgres psql -d limits -c 'DELETE FROM limits;'
```

## After PostGIS ##

Remove any previous mess if required:

```
revoke all privileges on limits from limits;
```

Setup access:
```
limits=# GRANT SELECT ON limits TO limits;
GRANT
limits=# GRANT INSERT ON limits TO limits;
GRANT
limits=# GRANT USAGE, SELECT ON limits_gid_seq TO limits;
GRANT
limits=# \z
```
