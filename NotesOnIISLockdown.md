Location of urlscan:

c:\windows\system32\inetsrv\urlscan\

record in the IIS log:

12:42:48 127.0.0.1 GET /

&lt;Rejected-By-UrlScan&gt;

 404

log in the urlscan log:

```

[04-26-2010 - 14:42:35] ---------------- Initializing UrlScan.log ----------------
[04-26-2010 - 14:42:35] -- Filter initialization time: [04-26-2010 - 14:42:35]  --
[04-26-2010 - 14:42:35] ---------------- UrlScan.dll Initializing ----------------
[04-26-2010 - 14:42:35] UrlScan will return the following URL for rejected requests: "/<Rejected-By-UrlScan>"
[04-26-2010 - 14:42:35] URLs will be normalized before analysis.
[04-26-2010 - 14:42:35] URL normalization will be verified.
[04-26-2010 - 14:42:35] URLs must contain only ANSI characters.
[04-26-2010 - 14:42:35] URLs must not contain any dot except for the file extension.
[04-26-2010 - 14:42:35] Only the following verbs will be allowed (case sensitive):
[04-26-2010 - 14:42:35] 	'GET'
[04-26-2010 - 14:42:35] 	'HEAD'
[04-26-2010 - 14:42:35] 	'POST'
[04-26-2010 - 14:42:35] Requests for following extensions will be rejected:
[04-26-2010 - 14:42:35] 	'.exe'
[04-26-2010 - 14:42:35] 	'.bat'
[04-26-2010 - 14:42:35] 	'.cmd'
[04-26-2010 - 14:42:35] 	'.com'
[04-26-2010 - 14:42:35] 	'.htw'
[04-26-2010 - 14:42:35] 	'.ida'
[04-26-2010 - 14:42:35] 	'.idq'
[04-26-2010 - 14:42:35] 	'.htr'
[04-26-2010 - 14:42:35] 	'.idc'
[04-26-2010 - 14:42:35] 	'.shtm'
[04-26-2010 - 14:42:35] 	'.shtml'
[04-26-2010 - 14:42:35] 	'.stm'
[04-26-2010 - 14:42:35] 	'.printer'
[04-26-2010 - 14:42:35] 	'.ini'
[04-26-2010 - 14:42:35] 	'.log'
[04-26-2010 - 14:42:35] 	'.pol'
[04-26-2010 - 14:42:35] 	'.dat'
[04-26-2010 - 14:42:35] Requests containing the following headers will be rejected:
[04-26-2010 - 14:42:35] 	'translate:'
[04-26-2010 - 14:42:35] 	'if:'
[04-26-2010 - 14:42:35] 	'lock-token:'
[04-26-2010 - 14:42:35] Requests containing the following character sequences will be rejected:
[04-26-2010 - 14:42:35] 	'..'
[04-26-2010 - 14:42:35] 	'./'
[04-26-2010 - 14:42:35] 	'\'
[04-26-2010 - 14:42:35] 	':'
[04-26-2010 - 14:42:35] 	'%'
[04-26-2010 - 14:42:35] 	'&'
[04-26-2010 - 14:42:48] Client at 127.0.0.1: URL contains '.' in the path. Request will be rejected.  Site Instance='1', Raw URL='/Portal30.WebTest/Portal25.aspx'

```

Link to the technet page: http://technet.microsoft.com/en-us/library/cc751373.aspx

credit: http://www.faqshop.com/sus/default.htm?http://faqshop.com/sus/server/rejected%20by%20urlscan.htm

. ini file options page:
http://technet.microsoft.com/en-us/library/cc751376.aspx
```
AllowDotInPath=0
By default, this option is set to 0. If this option is set to 0, URLScan rejects any request that contains multiple periods (.). This prevents attempts to disguise requests for dangerous file name extensions by putting a safe file name extension in the path information or query string portion of the URL. For example, if this option is set to 1, URLScan might permit a request for http://servername/BadFile.exe/SafeFile.htm because it interprets it as a request for an HTML page, when it is actually a request for an executable (.exe) file with the name of an HTML page in the PATH_INFO area. When this option is set to 0, URLScan may also deny requests for directories that contain periods. 
```
So you need to add/change this line in the .ini file: AllowDotInPath=1 and don't forget about iisreset after making your changes.