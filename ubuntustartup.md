## bash ##

find file with pattern:

```
sudo find / -print | grep -i '.*[.]mml'
```

## Add user and change root password ##

Change root password
```
passwd
```

Add user:
```
adduser demo

visudo // add line demo    ALL=(ALL:ALL) ALL
```

## Mosh ##

https://mosh.mit.edu/

## Install and setup apache to run at start up ##



Run at start:
```
update-rc.d apache2 defaults
```

## OSM ##

http://switch2osm.org/serving-tiles/manually-building-a-tile-server-12-04/

## Disk ##
### See the top ###
iotop

df

seeing logical with partitions

### Copy my raid to a single disk ###

run via live cd/usb

raid should be seen as /dev/md1

unmount /dev/md1, unmount /dev/sdX

dd if=/dev/md1 of=/dev/sdX

? That can be long, how to see the progress

http://askubuntu.com/questions/215505/how-do-you-monitor-the-progress-of-dd

### create raid while running ###

http://www.howtoforge.com/how-to-set-up-software-raid1-on-a-running-system-incl-grub2-configuration-debian-squeeze-p2

### Recover the failed raid disk ###

https://raid.wiki.kernel.org/index.php/Recovering_a_failed_software_RAID

### Software raid how to ###

http://unthought.net/Software-RAID.HOWTO/Software-RAID.HOWTO-6.html

## Postgresql top ##

pg\_top

## History ##

http://askubuntu.com/questions/358867/how-to-view-the-bash-history-file-via-command-line

/home/username/.bash\_history