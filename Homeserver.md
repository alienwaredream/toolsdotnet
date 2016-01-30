## Mac mini ##

388 has only space for 1 disk!

http://www.virten.net/2014/01/vmware-homeserver-esxi-on-mac-mini-md387-md388-md389/

Alza HP microserver:

G2020T - no hyper threading

http://www.alza.cz/hp-proliant-microserver-gen8-d1148689.htm

## HP MS Gen 8 CPU change ##

http://homeservershow.com/hp-proliant-gen8-microserver-xeon-e3-1265lv2-cpu-upgrade.html

http://homeservershow.com/forums/index.php?/topic/6596-hp-microserver-gen8-processor-faq/?p=72592

## B120i RAID in Ubuntu ##

http://alinuxpassionate.com/computing/2014/08/ubuntu-on-proliant-dl320e-gen8-with-b120i-raid/


http://homeservershow.com/forums/index.php?/topic/8426-installing-ubuntu-to-ssd-in-odd-bay-with-b120i-raid-1/

## Continued ##

SUPPORT COMMUNICATION - CUSTOMER ADVISORY
Document ID: c04439186
Version: 1
Advisory: Ubuntu 14.04 ISO - Installation of HPVSA Driver Will Fail if a ProLiant Gen8 Server Is Configured With a Smart Array B320i or Smart Array B120i Controller
NOTICE: The information in this document, including products and software versions, is current as of the Release Date. This document is subject to change without notice.
Release Date: 2014-09-08
Last Updated: 2014-09-09
DESCRIPTION
During an installation of the Ubuntu 14.04 operating system, if the underlying ProLiant server is configured with a Smart Array B320i or Smart Array B120i Controller, the installation will fail at the detect disk step because the HPVSA driver is not present (inbox) on the 14.04 Ubuntu Operating System ISO.

SCOPE
Any ProLiant Gen8 server configured with a Smart Array B320i or Smart Array B120i Controller and attempting an installation of Ubuntu 14.04.

RESOLUTION
Perform one of the following methods for installing the HPVSA driver:

The preferred method is to use MaaS (Metal-as-a-Service) Version 1.5 (or later). Maas will detect the server and controller and will automatically install the HPVSA driver with no user intervention required.

OR

Use a USB Key to install the HPVSA driver. Driver packages are available at the following URL: https://launchpad.net/~hp-iss-team/+archive/hp-storage Non-HP site

Rename the packages as shown; the leading digits control the order in which packages are installed. It is important the packages be installed/loaded in proper order:

# mv hpvsa-common_<driver ver>_

&lt;label&gt;



&lt;arch&gt;

.deb 01hpvsa-common_<driver ver>_

&lt;label&gt;



&lt;arch&gt;

.deb

# mv hpvsa-<kern ver>

&lt;label&gt;



&lt;arch&gt;

.deb 02hpvsa-<kern ver>

&lt;label&gt;



&lt;arch&gt;

.deb

# mv hpvsa_<driver ver>-_

&lt;label&gt;



&lt;arch&gt;

.deb 03hpvsa_<driver ver>-_

&lt;label&gt;



&lt;arch&gt;

.deb

The hp-storage-<kern ver>-di_<ldriver ver>-_

&lt;label&gt;



&lt;arch&gt;

.udeb does not require renaming

To create the USB key

Insert a USB key, create a partition, filesystem, and directory structure. This example assumes the USB key is /dev/sdb and will be mounted under /media.

Create a 10MB partition: NOTE: size is arbitrary

# parted /dev/sdb mklabel msdos

# parted /dev/sdb mkpart primary 1 10MB

Create the vfat filesystem with OEMDRV label

# mkfs -t vfat -n OEMDRV /dev/sdb1

Mount the filesystem

# mount /dev/sdb1 /media

Create the directory structure

# mkdir -p /media/driver-injection-disk/Ubuntu-drivers/<os codename>

# mkdir -p /media/Ubuntu-drivers/trusty<os codename>

Copy the driver packages to the USB key:

# cp **deb /media/Ubuntu-drivers/<os codename> NOTE: the wildcard is**deb to include the udeb package

# cp **deb /media/driver-injection-disk/Ubuntu-drivers/<os codename>**

To install version 14.04 LTS:

Insert OS media and USB key.
Boot the OS media.
Select language.
Press 

&lt;F6&gt;

 then immediately press 

&lt;Esc&gt;


Add the following to the command line:
modprobe.blacklist=ahci
Press 

&lt;Enter&gt;

 to start installation. When prompted about loading manufacturer's driver answer yes. The hpvsa driver will be loaded in the background. After the drivers are loaded remove the USB stick from the system.
To test if the drivers are loaded:

Press 

&lt;Ctrl&gt;

-

&lt;Alt&gt;

-

&lt;F2&gt;

. Press 

&lt;Enter&gt;

 to start a console session.

# lsmod | grep hp

The result should be similar to:

hpvsa 2485516 2

The driver is loaded and in use.

Check /proc/scsi/scsi:

# cat /proc/scsi/scsi

Attached devices:

Host: scsi0 Channel: 00 Id: 00 Lun: 00

Vendor: hp Model: USB Flash Drive Rev: 8192

Type: Direct-Access ANSI SCSI revision: 04

Host: scsi7 Channel: 00 Id: 00 Lun: 00

Vendor: HP Model: LOGICAL VOLUME Rev: 4.50

Type: Direct-Access ANSI SCSI revision: 05

Host: scsi7 Channel: 01 Id: 00 Lun: 00

Vendor: HP Model: B320i Rev: 4.50

Type: RAID ANSI SCSI revision: 05

Both the Smart Array controller and the logical volume(s) will be shown.

Complete the install, remove all removable media, and reboot.

Disclaimer:

Note: One or more of the links above will take you outside the Hewlett-Packard web site. HP does not control and is not responsible for information outside of the HP web site.



RECEIVE PROACTIVE UPDATES : Receive support alerts (such as Customer Advisories), as well as updates on drivers, software, firmware, and customer replaceable components, proactively via e-mail through HP Subscriber's Choice. Sign up for Subscriber's Choice at the following URL: http://www.hp.com/go/myadvisory

NAVIGATION TIP : For hints on navigating HP.com to locate the latest drivers, patches, and other support software downloads for ProLiant servers and Options, refer to the Navigation Tips document .

SEARCH TIP : For hints on locating similar documents on HP.com, refer to the Search Tips document .