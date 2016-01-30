## Improve GPS usability ##

http://stackoverflow.com/questions/488088/iphone-gps-development-tips-tricks

### core location cache ###
Core Location will cache data. The first reading it provides to your app is likely to be an old reading, which might or might not be accurate, depending on whether the phone has moved. Make sure to check the timestamp of any location and see if it's from before your app started.
### power modes ###
On the contrary it's easy to run down your phone's battery much more quickly than you'd expect if you lock the phone while an app that uses Core Location is running, because the phone will continue to update the app as new location data is available. You could avoid this in your application by listening for UIApplicationWillResignActiveNotification to detect the screen locking, and UIApplicationDidBecomeActiveNotification to detect unlock.

### out of order location updates ###

Very, very infrequently you may get updates out of order (i.e. you get a more accurate location followed by a slightly less accurate one dated earlier)

### Evaluating accuracy ###

http://stackoverflow.com/questions/2086649/whats-the-best-way-to-evaluate-the-accuracy-of-gps-sensor-data-for-a-users-loca