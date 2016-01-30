## Recursive list of files by directory ##
```
PS C:\dev\FORIS\Core\4.2SP\Source\Libraries> get-childitem | where {$_.Name -match "odp|oracle"} | foreach -process {wri
te $_.Name; get-childitem $_ -recurse | format-table Name}
```