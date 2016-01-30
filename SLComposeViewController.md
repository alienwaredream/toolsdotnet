## Behavior ##

```
    return [SLComposeViewController
            isAvailableForServiceType:SLServiceTypeTwitter];
```

available from ios 5.0

Returns false if there is no twitter account added in settings. Is true if there is at least one account in the settings>Twitter, but access is denied to the app by user.
