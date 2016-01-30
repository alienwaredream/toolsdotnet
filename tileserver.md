Install apache first!

http://switch2osm.org/serving-tiles/manually-building-a-tile-server-12-04/

```
apt-get install libgeos++-dev
```

Mapnik installation:

Removing any old bits:
```
sudo rm -rf /usr/local/include/mapnik
sudo rm /usr/local/lib/libmapnik*
sudo rm /usr/local/lib/mapnik*
```

https://github.com/mapnik/mapnik/wiki/UbuntuInstallation

Make sure to install and build from the source!! http://gis.stackexchange.com/questions/31381/could-not-create-datasource

## A must read for fixing issues ##

http://stackoverflow.com/questions/16834983/i-am-trying-to-config-my-own-map-server-with-mapnik-mod-tile-and-apache-no-tiles

## Reducing the size ##

https://lists.openstreetmap.org/pipermail/dev/2013-June/027140.html

## Avoiding the planet file error ##
```
osm2pgsql: PolygonBuilder.cpp:261: geos::geomgraph::EdgeRing* geos::operation::overlay::PolygonBuilder::findShell(std::vector<geos::operation::overlay::MinimalEdgeRing*>*): Assertion `shellCount <= 1' failed.
```
source: http://trac.osgeo.org/postgis/wiki/UsersWikiPostGIS20Ubuntu1204

Looks good... what's up with libGEOS?

$ geos-config --version
3.3.2
Crud... not quite 3.3.3. On to the next step.

3/4 Build GEOS 3.3.x

NOTE: This step may be out of date & unnecessary as of 12/18/12.

These are the instructions for building geos 3.3.3, based on instructions found elsewhere on trac.osgeo.org with some additions determined through trial and error:
```
sudo apt-get install g++ ruby ruby1.8-dev swig swig2.0   ''--- added to other instructions, not installed by default in 12.04 & required for this make
wget http://download.osgeo.org/geos/geos-3.3.3.tar.bz2
tar xvfj geos-3.3.3.tar.bz2
cd geos-3.3.3
./configure --enable-ruby --prefix=/usr
```
The end of this configure process should look something like this:
```
Swig: true
Python bindings: false
Ruby bindings: true
PHP bindings: false
```
If it's so, then move on to finishing the installation:
```
make
sudo make install
cd ..
```
To confirm this works, do the following:
```
$ geos-config --version
3.3.3 
```

## safe cache settings for vps ##
```
osm2pgsql --slim -d gis -C 16000 --cache 160 --number-processes 3 planet-latest.osm.pbf &> osm.log
```

## some stats ##

```
Node stats: total(2609086266), max(3187644577) in 2504s
Way stats: total(260489883), max(312856439) in 107029s
Relation stats: total(2925029), max(4196171) in 209658s
```


## OSM Bright ##

### paths ###

standa@bmap2:~$ sudo find / -print | grep -i '.