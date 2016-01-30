# Installing from the source #
```
sudo apt-get remove nginx # Removes all but config files.

sudo apt-get purge nginx # Removes everything.

sudo apt-get autoremove # After using any of the above commands, use this in order to remove dependencies used by nginx which are no longer required.
```

de best:
http://www.jeffmould.com/2013/11/26/install-nginx-source-ubuntu-12-04/

only left out bit is:
```
sudo mkdir /var/lib/nginx/body
```

adding the postgresql module:

https://github.com/FRiCKLE/ngx_postgres/

```
git clone https://github.com/FRiCKLE/ngx_postgres.git
```

```
./configure \
--prefix=/usr/share/nginx \
--conf-path=/etc/nginx/nginx.conf \
--error-log-path=/var/log/nginx/error.log \
--user=www-data \
--group=www-data \
--http-client-body-temp-path=/var/lib/nginx/body \
--http-fastcgi-temp-path=/var/lib/nginx/fastcgi \
--http-log-path=/var/log/nginx/access.log \
--http-proxy-temp-path=/var/lib/nginx/proxy \
--http-scgi-temp-path=/var/lib/nginx/scgi \
--http-uwsgi-temp-path=/var/lib/nginx/uwsgi \
--lock-path=/var/lock/nginx.lock \
--pid-path=/run/nginx.pid \
--with-pcre-jit \
--with-debug \
--add-module=/home/demo/nginx/modules/ngx_postgres \
--add-module=/home/demo/nginx/modules/rds-json-nginx-module \
--add-module=/home/demo/nginx/modules/echo-nginx-module \
--add-module=/home/demo/nginx/modules/ngx_devel_kit \
--add-module=/home/demo/nginx/modules/form-input-nginx-module \
--add-module=/home/demo/nginx/modules/set-misc-nginx-module

make

sudo make install
```

copying nginx executable to /usr/sbin
```
sudo cp objs/nginx /usr/sbin
```

## Removing system startup links if required/screwed :) ##

source: http://codedecoder.wordpress.com/2013/03/12/system-startstop-links-for-etcinit-dnginx-already-exist/comment-page-1/

```
$ sudo update-rc.d -f nginx remove 
#Adding defaults
$ sudo /usr/sbin/update-rc.d -f nginx defaults
```

## Troubleshooting ##

See all ubuntu uders

```
cut -d: -f1 /etc/passwd
```

Test nginx configuration and see where it is:

```
sudo nginx -t
```

truncate nginx error.log:
```
truncate -s0 error.log
```

Seeing error log:
```
sudo vim /var/log/nginx/error.log
```

See processes and kill:

```

ps aux | grep nginx

sudo killall -v nginx

```

## Startup script ##

```
#!/bin/sh

### BEGIN INIT INFO
# Provides:          nginx
# Required-Start:    $local_fs $remote_fs $network $syslog
# Required-Stop:     $local_fs $remote_fs $network $syslog
# Default-Start:     2 3 4 5
# Default-Stop:      0 1 6
# Short-Description: starts the nginx web server
# Description:       starts nginx using start-stop-daemon
### END INIT INFO

PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin
DAEMON=/usr/sbin/nginx
NAME=nginx
DESC=nginx

# Include nginx defaults if available
if [ -f /etc/default/nginx ]; then
        . /etc/default/nginx
fi

test -x $DAEMON || exit 0

set -e

. /lib/lsb/init-functions

test_nginx_config() {
        if $DAEMON -t $DAEMON_OPTS >/dev/null 2>&1; then
                return 0
        else
                $DAEMON -t $DAEMON_OPTS
                return $?
        fi
}

case "$1" in
        start)
                echo -n "Starting $DESC: "
                test_nginx_config
                # Check if the ULIMIT is set in /etc/default/nginx
                if [ -n "$ULIMIT" ]; then
                        # Set the ulimits
                        ulimit $ULIMIT
                fi
                start-stop-daemon --start --quiet --pidfile /var/run/$NAME.pid \
                    --exec $DAEMON -- $DAEMON_OPTS || true
                echo "$NAME."
                ;;

        stop)
                echo -n "Stopping $DESC: "
                start-stop-daemon --stop --quiet --pidfile /var/run/$NAME.pid \
                    --exec $DAEMON || true
                echo "$NAME."
                ;;

        restart|force-reload)
                echo -n "Restarting $DESC: "
                start-stop-daemon --stop --quiet --pidfile \
                    /var/run/$NAME.pid --exec $DAEMON || true
                sleep 1
                test_nginx_config
                start-stop-daemon --start --quiet --pidfile \
                    /var/run/$NAME.pid --exec $DAEMON -- $DAEMON_OPTS || true
                echo "$NAME."
                ;;

        reload)
                echo -n "Reloading $DESC configuration: "
                test_nginx_config
                start-stop-daemon --stop --signal HUP --quiet --pidfile /var/run/$NAME.pid \
                    --exec $DAEMON || true
                echo "$NAME."
                ;;

        configtest|testconfig)
                echo -n "Testing $DESC configuration: "
                if test_nginx_config; then
                        echo "$NAME."
                else
                        exit $?
                fi
                ;;

        status)
                status_of_proc -p /var/run/$NAME.pid "$DAEMON" nginx && exit 0 || exit $?
                ;;
        *)
                echo "Usage: $NAME {start|stop|restart|reload|force-reload|status|configtest}" >&2
                exit 1
                ;;
esac

exit 0
```
## First handler/module ##