DisplayLink #3 – Ubuntu 9.10 (32 bit)

Well, it’s time to see if I can get my DisplayLink device working on an old dying laptop using Ubuntu 9.10 (32 bit) – so here goes.

Install Ubuntu 9.10 (32 bit).  Once finally installed and booted lets drop to a console (via Applications > Accessories > Terminal) and do some stuff.

Start by updating the package list:

> sudo apt-get update

Then download and install any package & system updates:

> sudo apt-get upgrade
> sudo apt-get dist-upgrade

Then download some packages we’ll need later:

> sudo apt-get install build-essential xorg-dev libusb-dev git-core

Reboot (via console or other means):

> sudo shutdown -r now

Once the system is back up drop to a console and get the following package (I’m saving it to the Desktop):

> cd Desktop
> wget http://projects.unbit.it/downloads/udlfb-0.2.3_and_xf86-video-displaylink-0.3.tar.gz

NOTE: this package takes FOREVER to download.

Extract the package and delete the udlfb directory it contains (we’ll get a newer udlfb library in a bit):

> tar xzvf udlfb-0.2.3\_and\_xf86-video-displaylink-0.3.tar.gz
> rm -rf udlfb

Get the latest udlfb and build it:

> git clone http://git.plugable.com/webdav/udlfb
> cd udlfb
> make
> sudo make install

Compile the DisplayLink X driver:

> cd ../xf86-video-displaylink
> ./configure && make
> sudo make install
> sudo shutdown -r now

You should now be able to plug in your DisplayLink device and see the green screen.

Now, here’s where things may get a bit difficult – setting up an xorg.conf file.  Modern distros don’t ship with xorg.conf’s anymore so it’s up to the end user to create one when one is needed.  Also, since this laptop doesn’t have an ATI or NVIDIA card there’s no proprietary driver to install (that would then automatically set up an xorg.conf file for me).  I’m creating a xorg.conf file based on this previous DisplayLink blog entry.

Some things to note:

  * Which /dev/fb**entry maps to the DisplayLink device.  You can see this by unplugging the device, doing an ‘ls‘ in the /dev directory, then plugging back in the device and doing another ‘ls‘ and seeing which entry was just added.
  * The ‘BusId‘ of the non-DisplayLink video card.  This can be obtained through lspci and is needed in the xorg.conf file.
  * Be sure to modify /etc/gdm/Init/Default to contain the following blurb right after the definition of the gdmwhich() function (this is taken from Patrick Gilmore’s blog):**

> XRANDR=`gdmwhich xrandr`
> if ["x$XRANDR" != "x" ](.md) ; then
> > $XRANDR -o 0

> fi

Once you do/know these things you can start tailoring your xorg.conf.  Creating the xorg.conf file is above the scope of this blog and highly dependent on your own computers’ specific hardware.  However, here is how mine turned out:

  1. xorg.conf (X.Org X Window System server configuration file)

  1. ########## Original Video Settings ###########

> Section "Device"
> > Identifier      "Configured Video Device"
> > Driver		"intel"
> > > BusID           "PCI:0:2:0"

> EndSection

> Section "Monitor"
> > Identifier      "Configured Monitor"

> EndSection

> Section "Screen"
> > Identifier      "Default Screen"
> > Monitor         "Configured Monitor"
> > Device          "Configured Video Device"
> > SubSection "Display"
> > > Depth   24
> > > Modes   "1280x800"

> > EndSubSection

> EndSection

  1. ###############################################

> Section "ServerLayout"
> > Identifier      "Server Layout"
> > Screen  0       "DisplayLinkScreen" 0 0
> > Screen  1       "Default Screen" LeftOf "DisplayLinkScreen"
> > Option          "Xinerama" "off"

> EndSection

  1. ###############################################

> Section "Files"
> > ModulePath      "/usr/lib/xorg/modules"
> > ModulePath      "/usr/local/lib/xorg/modules"
> > ModulePath	"/usr/local/lib/xorg/modules/drivers"

> EndSection

  1. ############# DisplayLink Stuff ###############

> Section "Device"
> > Identifier      "DisplayLinkDevice"
> > driver          "displaylink"
> > Option  "fbdev" "/dev/fb1"

> EndSection

> Section "Monitor"
> > Identifier      "DisplayLinkMonitor"

> EndSection

> Section "Screen"
> > Identifier      "DisplayLinkScreen"
> > Device          "DisplayLinkDevice"
> > > Monitor         "DisplayLinkMonitor"
> > > SubSection "Display"
> > > > Depth   16

> > > Modes   "1280x1024"
> > > > EndSubSection

> EndSection

Some pictures:

  1. Green screen
> 2. Both monitors working

Posted by Mulchman on March 9, 2010 at 15:41 under Tech.
Tags: 32 bit, Bernie Thompson, DisplayLink, git, plugable.com, Roberto De Ioris, Sewell USB External Video Card, Ubuntu 9.10, udlfb
14 Comments.