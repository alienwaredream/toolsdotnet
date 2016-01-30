## Direct children or descendants ##

direct descendant combinator (>)

```

$('div#a > p') // find p(s) that are directly children of div with id a

$('div#a p') // find p(s) that are directly or indirectly under the div with id a

```

```

$('*') // select all elements on the page

```

!! - converts any value to the corresponding boolean value per javascript false/true rules.

## IE - going via frames ##

```
$($('#IFRAME_CMAddress_d')[0].document.forms[0]).find('input').each(function(e,d){console.log(d.id)})
```