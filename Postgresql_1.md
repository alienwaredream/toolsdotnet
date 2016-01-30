# Objectives #
  * Install postgresql with PostGIS and enable PostGIS
  * Compile and run the most simple, but functional postgresql c program
  * Find C CLI (command line) IDE and learn tricks on c programming in vim


## Installing postgis on postgre 9.1 and ubuntu 12.04 ##

### Uninstalling first any existing: ###

_As I got to install 9.1 by default on Digital Ocean and then I learnt that there is no any easy way to get PostGIS on 9.1, but I found the working way for 9.3, so I had to uninstall 9.1 first as contrib-9.1 was giving me a trouble_

http://stackoverflow.com/questions/2748607/how-to-thoroughly-purge-and-reinstall-postgresql-on-ubuntu

First: If your install isn't already damaged, you can drop unwanted PostgreSQL servers ("clusters") in Ubuntu using pg\_dropcluster. Use that in preference to a full purge and reinstall if you just want to start with a freshly initdb'd PostgreSQL instance.

If you really need to do a full purge and reinstall, first make sure PostgreSQL isn't running. ps -C postgres should show no results.

Now run:
```
apt-get --purge remove postgresql\*
to remove everything PostgreSQL from your system. Just purging the postgres package isn't enough since it's just an empty meta-package.
```
Once all PostgreSQL packages have been removed, run:
```
rm -r /etc/postgresql/
rm -r /etc/postgresql-common/
rm -r /var/lib/postgresql/
userdel -r postgres
groupdel postgres
```

### Installing with postGis ###

http://stackoverflow.com/questions/18696078/postgresql-error-when-trying-to-create-an-extension

```
echo "deb http://apt.postgresql.org/pub/repos/apt/ precise-pgdg main" | sudo tee /etc/apt/sources.list.d/postgis.list
wget --quiet -O - http://apt.postgresql.org/pub/repos/apt/ACCC4CF8.asc | sudo apt-key add -
sudo apt-get update
sudo apt-get install postgresql-9.3 postgresql-9.3-postgis-2.1 postgresql-client-9.3
sudo -u postgres psql -c 'create extension postgis;' // This will install extensions in postgres db. Probably I don't need it.
```

## PostGIS 2+ enable extensions on the concrete database ##

source: http://gis.stackexchange.com/questions/19432/why-does-postgis-installation-not-create-a-template-postgis

```
//From version 2 Postgis is enabled by using the extension system. To //spatially enable a database, log to your database and then:

 CREATE EXTENSION postgis;
 CREATE EXTENSION postgis_topology;
```

## Removing postgresql ##

sudo apt-get --purge autoremove postgresql

## Reboot the machine ##

sudo reboot

## Compile sample ##

### How to build ###
http://www.postgresql.org/docs/9.3/static/libpq-build.html

### Sample code to start with ###

http://www.postgresql.org/docs/9.3/static/libpq-example.html

Sample code, good for a start as it doesn't require your db and has safe defaults (falling back to the postgres account for example)

```
#first see what's the include folder is
pg_config --includedir
#then compile
cc -c -I/usr/include/postgresql limits2.c

#then see what lib directory is
pg_config --libdir
#then link
cc -o limits2 limits2.o -L/usr/lib -lpq
#run
sudo -u postgres ~/c/psql/limits2

```

## Get into the database ##

sudo -u postgres psql -d limits


---


## Troubleshooting ##

Db list:
```
\list or
SELECT datname FROM pg_database WHERE datistemplate = false;
```

## C CLI IDE with VIM ##

### Setting up cvim ###

http://www.thegeekstuff.com/2009/01/tutorial-make-vim-as-your-cc-ide-using-cvim-plugin/

### ctags and taglist ###

http://www.thegeekstuff.com/2009/04/ctags-taglist-vi-vim-editor-as-sourece-code-browser/

## MRU plugin ##

http://www.thegeekstuff.com/2009/08/vim-editor-how-to-setup-most-recently-used-documents-features-using-mru-plugin/