## custom sound ##
```
localNotification.soundName = UILocalNotificationDefaultSoundName;
```

## repeating notification ##

```
localNotification.repeatInterval = NSHourCalendarUnit;
```

## custom data in local notification ##

```
NSDictionary *infoDict=[NSDictionary dictionaryWithObject:@"1234" forKey:@"IDkey"];
localNotification.userInfo = infoDict
```

## schedule or not ##

nce you have created an instance of UILocalNotification, you schedule it using one of two methods of the UIApplication class: scheduleLocalNotification: or presentLocalNotificationNow:.

_I need to use **presentLocalNotificationNow** to avoid user mingling with the app before notification is shown. No need to set fireDate and timeZone then_

## handling states ##

If the notification is an **alert** and the user taps the action button (or, if the device is locked, drags open the action slider), the application is launched. _When it is an alert and when it is not??_

## Alert or not - Action thingy - Experiment with that ##

hasAction
A Boolean value that controls whether the notification shows or hides the alert action.
```
@property(nonatomic) BOOL hasAction
```
Discussion
Assign NO to this property to hide the alert button or slider. (This effect requires alertBody to be non-nil.) The default value is YES.

## Notification Launch Image - interesting ##

alertLaunchImage
Identifies the image used as the launch image when the user taps (or slides) the action button (or slider).
```
@property(nonatomic,copy) NSString *alertLaunchImage
```
Discussion
The string is a filename of an image file in the application bundle. This image is a launching image specified for a given notification; when the user taps the action button (for example, “View”) or moves the action slider, the image is used in place of the default launching image. If the value of this property is nil (the default), the system either uses the previous snapshot, uses the image identified by the UILaunchImageFile key in the application’s Info.plist file, or falls back to Default.png.

The value of this key has the exact same semantics as UILaunchImageFile. For more about this key, see the Information Property List Key Reference.

## Sound name - I want it ##

soundName
The name of the file containing the sound to play when an alert is displayed.
```
@property(nonatomic, copy) NSString *soundName
```
Discussion
For this property, specify the filename (including extension) of a sound resource in the application’s main bundle or UILocalNotificationDefaultSoundName to request the default system sound. When the system displays an alert for a local notification or badges an application icon, it plays this sound. The default value is nil (no sound). Sounds that last longer than 30 seconds are not supported. If you specify a file with a sound that plays over 30 seconds, the default sound is played instead.

For information on valid sound resources, see “Scheduling, Registering, and Handling Notifications” in Local and Push Notification Programming Guide.

## Take this into account, looks like presentLocalNotificationNow is flawed on iOS7 ##

Found a workaround for the problem.
It appears only on devices with iOS > 7.x (Bugreport: 15129773)

The issue don't occour when you schedule the UILocalNotification. Just do it with some miliseconds delay:
```
UILocalNotification *notif = [[UILocalNotification alloc] init];
notif.alertBody = @"Message-Text";
notif.soundName = @"Custom-Sound-Name.mp3";
notif.fireDate = [[NSDate date] dateByAddingTimeInterval:0.1];

notif.applicationIconBadgeNumber += 1;
notif.userInfo = @{some-key: some-data};
[[UIApplication sharedApplication] scheduleLocalNotification:notif];
```
