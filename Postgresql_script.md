## Backup, including db creation script ##

```
pg_dump -C -d limits > p.txt
```

## Restore ##

_On target machine en\_us.utf-8 was  not present - had to delete from the script._

```
pg_dump -C -d limits > p.txt
```