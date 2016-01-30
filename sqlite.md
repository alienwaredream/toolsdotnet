# Meta #

# .schema #

```
sqlite> .schema provider

CREATE TABLE provider (key string primary key, name string , country string, city string, cityCode string, data string, version real, source integer default 0);

sqlite> 

```

# ALTER #

## Add a column ##

```
alter table city add column source int default(0);
```

# Optimizations #

## sqlite3\_prepare with string compared to params binding ##

```
    NSString *updateStatementNS = [NSString stringWithFormat: @"UPDATE Trip SET distance=%f, maxspeed = %f, avgspeed = %f, maxalt = CASE WHEN %f > maxalt OR %d = 1 OR (%d = 0 AND reset = 1) THEN %f ELSE maxalt END, minalt = CASE WHEN %f < minalt OR %d = 1 OR (%d = 0 AND reset = 1) THEN %f ELSE minalt END, updated = CASE WHEN %d = 1 THEN 0 ELSE %f END, samplesCount=%d, reset=%d where id=%d", distance, maxspeed, avgspeed, maxalt, reset, reset, maxalt, minalt, reset, reset, minalt, reset, [[NSDate date] timeIntervalSince1970], samplesCount, reset, tid];
    
    
    
    if ([DbHelper prepareDBStatement:updateStatementNS statementPointer:&dbps db:db] == 0) {
        
        [DbHelper executeDBStep: dbps];
        
        sqlite3_finalize (dbps);
    }
```

2013-02-13 01:58:51.093 speedometer[207:907] timings, delta1: 0.001131, delta2: 0.002861, total delta: 0.003992

```
    NSString *updateStatementNS = @"UPDATE Trip SET distance=@Distance, maxspeed = @MaxSpeed, avgspeed = @AvgSpeed, maxalt = CASE WHEN @MaxAlt > maxalt OR @Reset = 1 OR (@Reset = 0 AND reset = 1) THEN @MaxAlt ELSE maxalt END, minalt = CASE WHEN @MinAlt < minalt OR @Reset = 1 OR (@Reset = 0 AND reset = 1) THEN @MinAlt ELSE minalt END, updated = CASE WHEN @Reset = 1 THEN 0 ELSE @TimeInterval END, samplesCount=@SamplesCount, reset=@Reset where id=@tid";
    
    
    
    if ([DbHelper prepareDBStatement:updateStatementNS statementPointer:&dbps db:db] == 0) {
        
        double time2 = [[NSDate date] timeIntervalSince1970];
        
        //int rc = 0;
        
        sqlite3_bind_double(dbps, 1, distance);
        sqlite3_bind_double(dbps, 2, maxspeed);
        sqlite3_bind_double(dbps, 3, avgspeed);
        sqlite3_bind_double(dbps, 4, maxalt);
        sqlite3_bind_int(dbps, 5, reset);
        sqlite3_bind_double(dbps, 6, minalt);
        sqlite3_bind_double(dbps, 7, [[NSDate date] timeIntervalSince1970]);
        sqlite3_bind_int(dbps, 8, samplesCount);
        sqlite3_bind_int(dbps, 9, tid);
        
        [DbHelper executeDBStep: dbps];
```

2013-02-13 01:53:36.387 speedometer[171:907] timings, delta1: 0.001781, delta2: 0.003518, total delta: 0.005299

## Queries for speedo ##

```
SELECT datetime(p.time, 'unixepoch') as "time", p.lat, p.lng, p.hacc, p.vacc, t.id, datetime(t.start, 'unixepoch') as "track start"  FROM point p left join track t on p.trackId = t.id
```

## Annoying sqlite3\_open creating a new db that has no tables then ##
http://www.dmertl.com/blog/?p=9
> I recently ran into an annoying problem getting data from an SQLite database in my iPhone application. I had bundled the database with the application and duplicated code from Apple’s example code. No matter what I did every time I tried to select from my table I would get “no such table: table\_name”. I tried checking what tables existed in the database using “SELECT name FROM sqlite\_master WHERE type = ‘table’”, and found there were none.
> What happened is sqlite3\_open() will create a new database if it can’t find the one you requested. It will also cache this empty database and use it in the simulator. So even though I had fixed the issue with my code, that empty database was still being used. The solution to this is to delete the app from the simulator (click and hold the app icon, then click the x) and re-build. This will clear the cached database and use the copy bundled with your app.