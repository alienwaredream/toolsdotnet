## http\_load ##

```
demo@apps1:~/utils$ rm http_load-12mar2006.tar.gz
demo@apps1:~/utils$ cd http_load/
demo@apps1:~/utils/http_load$ wget 'http://www.acme.com/software/http_load/http_load-12mar2006.tar.gz'


demo@apps1:~/utils/http_load$ tar -zxvf http_load-12mar2006.tar.gz

demo@apps1:~/utils/http_load/http_load-12mar2006$ make

demo@apps1:~/utils/http_load/http_load-12mar2006$ ./http_load -man
usage:  ./http_load [-checksum] [-throttle] [-proxy host:port] [-verbose] [-timeout secs] [-sip sip_file]
            -parallel N | -rate N [-jitter]
            -fetches N | -seconds N
            url_file
One start specifier, either -parallel or -rate, is required.
One end specifier, either -fetches or -seconds, is required.

demo@apps1:~/utils/http_load/http_load-12mar2006$ ./http_load -p 10 -s 5 urls
34178 fetches, 10 max parallel, 683560 bytes, in 5 seconds
20 mean bytes/connection
6835.6 fetches/sec, 136712 bytes/sec
msecs/connect: 0.0445469 mean, 0.937 max, 0.021 min
msecs/first-response: 1.41242 mean, 101.975 max, 0.257 min
HTTP response codes:
  code 200 -- 34178
demo@apps1:~/utils/http_load/http_load-12mar2006$ ./http_load -p 10 -s 5 urls
33901 fetches, 10 max parallel, 678020 bytes, in 5 seconds
20 mean bytes/connection
6780.2 fetches/sec, 135604 bytes/sec
msecs/connect: 0.0450803 mean, 1.012 max, 0.022 min
msecs/first-response: 1.42492 mean, 17.848 max, 0.309 min
HTTP response codes:
  code 200 -- 33901
```

After posgre

```
5574 fetches, 10 max parallel, 6.22058e+06 bytes, in 5 seconds
1116 mean bytes/connection
1114.8 fetches/sec, 1.24412e+06 bytes/sec
msecs/connect: 0.0561055 mean, 0.37 max, 0.023 min
msecs/first-response: 8.68285 mean, 187.733 max, 0.812 min
```