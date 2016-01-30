# on change for radio button and checkboxes - IE #

http://norman.walsh.name/2009/03/24/jQueryIE


# jQuery.hide() and display:none for select in IE6 #
The solution is to move select under a different element like div:

```
                 <div id="IntervalDiv" style="display:inline">
                    <select name="Interval" id="Interval" >
...
                    </select>
            </div>
```