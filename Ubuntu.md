# ipconfig #

ifconfig

# configuring samba #
```
stan@ubs:~$ sudo chown nobody.nogroup /media/raid
stan@ubs:~$ sudo /etc/init.d/samba restart
 * Stopping Samba daemons                                                [ OK ] 
 * Starting Samba daemons                                                [ OK ] 
stan@ubs:~$ sudo gedit /etc/samba/smb.conf
```

## restarting samba ##

`sudo restart smbd `

# Apache #

restart: sudo /etc/init.d/apache2 restart

# dhcp: dhclient #

# svn #
```
stan@ubs:/etc/apache2/mods-available$ sudo htpasswd -cm /etc/subversion/passwd userName
```

http://www.howtogeek.com/howto/ubuntu/install-subversion-with-web-access-on-ubuntu/

http://www.sodeso.nl/?p=599

# Misc #

## Mounting virtual box shared folder after adding it ##
```
stan@stan-vb-ub1010:~$ sudo mount -t vboxsf ubs  ~/ubs
```
where ubs is name given to a shared folder and ~/ubs is folder you need to create before the mount.