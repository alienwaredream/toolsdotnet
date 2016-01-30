# Idioms #

## topLayoutGuide - manual approach ##

http://jdkuzma.tumblr.com/post/79294999487/xcode-mapview-offsetting-the-compass-and-legal

```
@interface MapLayoutGuide : NSObject <UILayoutSupport>
@property (nonatomic) CGFloat insetLength;
-(id)initWithLength:(CGFloat)length;
@end

@implementation MapLayoutGuide
@synthesize insetLength = _length;

- (id)initWithLength:(CGFloat)insetlength
{
    self = [super init];
    if (self) {
        _length = insetlength;
    }
    return self;
}

@end

And within the view controller displaying the map:

- (id)topLayoutGuide
{
   // #define kCompassInset 128.0/2.0
   return [[MapLayoutGuide alloc] initWithLength:kCompassInset];
}

- (id)bottomLayoutGuide
{
  // #define kLegalInset = 44.0
  return [[MapLayoutGuide alloc] initWithLength:kLegalInset];
}
```

# User messages #

## Unavailabiliies ##
### location services ###
You have currently disabled location services for this app. By doing this you're missing out on some great features that come with this app! If you change your mind, you can enable it by going to settings > general > location services. Thanks!

### altitude ###
```
If (signbit(newLocation.verticalAccuracy)) {
// get the altitude
}
```

### airplane mode ###
http://stackoverflow.com/questions/4804398/iphone-detect-airplane-mode


You can add the SBUsesNetwork boolean flag set to true in your Info.plist to display the popup used in Mail when in Airplane Mode.

**UIRequiresPersistentWifi, SBUsesNetwork should not be used**

### SMS ###

http://stackoverflow.com/questions/3622744/check-if-iphone-can-send-texts-w-mfmessagecomposeviewcontroller

```

Example from the MessageComposer sample:

-(IBAction)showSMSPicker:(id)sender {
//  The MFMessageComposeViewController class is only available in iPhone OS 4.0 or later. 
//  So, we must verify the existence of the above class and log an error message for devices
//      running earlier versions of the iPhone OS. Set feedbackMsg if device doesn't support 
//      MFMessageComposeViewController API.
Class messageClass = (NSClassFromString(@"MFMessageComposeViewController"));

if (messageClass != nil) {          
    // Check whether the current device is configured for sending SMS messages
    if ([messageClass canSendText]) {
        [self displaySMSComposerSheet];
    }
    else {  
        feedbackMsg.hidden = NO;
        feedbackMsg.text = @"Device not configured to send SMS.";

    }
}
else {
    feedbackMsg.hidden = NO;
    feedbackMsg.text = @"Device not configured to send SMS.";
}
}

```

http://developer.apple.com/library/ios/#samplecode/MessageComposer/Listings/Classes_MessageComposerViewController_m.html

### Check if a call can be placed from a device ###

```
NSURL *phoneNumberURL = [NSURL URLWithString:@"tel://004412345"];
if([[UIApplication sharedApplication] openURL:phoneNumberURL] == YES) {
  ...
}

```

http://stackoverflow.com/questions/6034504/how-to-check-device-is-not-able-to-send-sms

### model and platform ###

http://stackoverflow.com/questions/448162/determine-device-iphone-ipod-touch-with-iphone-sdk

```


Usage

UIDeviceHardware *h=[[UIDeviceHardware alloc] init];
[self setDeviceModel:[h platformString]];   
[h release];

UIDeviceHardware.h

//
//  UIDeviceHardware.h
//
//  Used to determine EXACT version of device software is running on.

#import <Foundation/Foundation.h>

@interface UIDeviceHardware : NSObject 

- (NSString *) platform;
- (NSString *) platformString;

@end

UIDeviceHardware.m

//
//  UIDeviceHardware.m
//
//  Used to determine EXACT version of device software is running on.

#import "UIDeviceHardware.h"
#include <sys/types.h>
#include <sys/sysctl.h>

@implementation UIDeviceHardware

- (NSString *) platform{
    size_t size;
    sysctlbyname("hw.machine", NULL, &size, NULL, 0);
    char *machine = malloc(size);
    sysctlbyname("hw.machine", machine, &size, NULL, 0);
    NSString *platform = [NSString stringWithCString:machine];
    free(machine);
    return platform;
}

- (NSString *) platformString{
    NSString *platform = [self platform];
    if ([platform isEqualToString:@"iPhone1,1"])    return @"iPhone 1G";
    if ([platform isEqualToString:@"iPhone1,2"])    return @"iPhone 3G";
    if ([platform isEqualToString:@"iPhone2,1"])    return @"iPhone 3GS";
    if ([platform isEqualToString:@"iPhone3,1"])    return @"iPhone 4";
    if ([platform isEqualToString:@"iPhone3,2"])    return @"Verizon iPhone 4";
    if ([platform isEqualToString:@"iPod1,1"])      return @"iPod Touch 1G";
    if ([platform isEqualToString:@"iPod2,1"])      return @"iPod Touch 2G";
    if ([platform isEqualToString:@"iPod3,1"])      return @"iPod Touch 3G";
    if ([platform isEqualToString:@"iPod4,1"])      return @"iPod Touch 4G";
    if ([platform isEqualToString:@"iPad1,1"])      return @"iPad";
    if ([platform isEqualToString:@"iPad2,1"])      return @"iPad 2 (WiFi)";
    if ([platform isEqualToString:@"iPad2,2"])      return @"iPad 2 (GSM)";
    if ([platform isEqualToString:@"iPad2,3"])      return @"iPad 2 (CDMA)";
    if ([platform isEqualToString:@"i386"])         return @"Simulator";
    return platform;
}

@end

```

### GPS ###

http://stackoverflow.com/questions/5897204/show-gps-availabilty-and-accuracy-in-iphone-sdk

```
[CLLocationManager locationServicesEnabled]
```

#### GPS fix ####

http://stackoverflow.com/questions/2004689/detect-gps-hardware-in-iphone

use of verticalAccuracy, when -1 - there is no fix (on iphone)

CLLocation.verticalAccuracy

## Rounded corners for anything ##

http://stackoverflow.com/questions/5138996/how-can-i-draw-rounded-rectangles-around-my-textfields-and-buttons-like-foursquar

## Debugging with NSZombie ##

http://www.cocoadev.com/index.pl?DebuggingAutorelease

To add and set to YES in environment arguments:

> --NSDebugEnabled
> NSZombieEnabled
> MallocStackLogging
> MallocStackLoggingNoCompact

When you face EXC\_BAD\_ACCESS:

`2011-06-19 22:27:07.363 Taxican[15971:207] *** -[CALayer retain]: message sent to deallocated instance 0x60d7af0`

shell malloc\_history 15971 0x60d7af0

## Sample of an address dictionary in a placemark ##

```

{
    City = "Prague 8";
    Country = "Czech Republic";
    CountryCode = CZ;
    FormattedAddressLines =     (
        "\U0160im\U016fnkova 1183/16",
        "182 00 Prague 8-Kobylisy",
        "Czech Republic"
    );
    State = Prague;
    Street = "\U0160im\U016fnkova 16";
    SubAdministrativeArea = Prague;
    SubLocality = Kobylisy;
    SubThoroughfare = 16;
    Thoroughfare = "\U0160im\U016fnkova";
    ZIP = "182 00";
}
```

# Memory management #
## detach new thread and autorelease pool ##

```
[NSThread detachNewThreadSelector:@selector(displayMap) toTarget:self
withObject:nil];

- (void)displayMap {
NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
...
[pool drain];
}
```

# Selectors #
## _cmd ##
```
-(void) tenMinutesElapsed:(ccTime)delta
{
// unschedule the current method by using the _cmd keyword
[self unschedule:_cmd];
}
```
The hidden variable_cmd is available in all Objective-C methods. It is the selector of the
current method. In the previous example, _cmd is equivalent to writing
@selector(tenMinutesElapsed:). Unscheduling_cmd effectively stops the
tenMinutesElapsed method from ever being called again. You can also use _cmd for
scheduling the selector in the first place, if you want the current method to be
scheduled._

# Logging #
## NSLog in debug and release ##
```
#ifndef NS_BLOCK_ASSERTIONS
    #define _DEBUG
#endif

#ifdef _DEBUG
// for debug mode 
#define DLog(fmt,...) NSLog(@"%s " fmt, __FUNCTION, ##__VA_ARGS__) 
... /// something extra
#else
// for release mode
#define DLog(fmt,...) /* noop */
... /// something extra
#endif
```
source: http://stackoverflow.com/questions/2025471/do-i-need-to-disable-nslog-before-release-application
## Logging in cocos ##
```
CCLOG(@"%@: %@", NSStringFromSelector(_cmd), self);
```

## Body for the support email ##
```
    NSString *emailBody = [NSString stringWithFormat:@"%@%@\r\niOS Version:%@\r\nDevice:%@\r\nOS language:%@\r\nReginal settings:%@\r\nApp: %@", NSLocalizedString(@"Description of the problem:",nil), @"\r\n\r\n\r\n",
                           [[UIDevice currentDevice] systemVersion],
                           [[UIDevice currentDevice] platform],
                           [[NSLocale preferredLanguages] objectAtIndex:0],
                           [[NSLocale currentLocale] localeIdentifier],
                           [[NSProcessInfo processInfo] processName]
                           ];
    
    [picker setMessageBody:emailBody isHTML:NO];
```

With UIDevice extension as:
.h
```
#import <UIKit/UIKit.h>

@interface UIDevice (Hardware)
- (NSString *) platform;
@end

```

.m
```
/* Thanks to Emanuele Vulcano, Kevin Ballard/Eridius, Ryandjohnson */

/*
 - Bluetooth? Screen pixels? Dot pitch? Accelerometer? GPS disabled in Egypt (and others?). - @halm
*/

#import "UIDevice-hardware.h"
#include <sys/types.h>
#include <sys/sysctl.h>

@implementation UIDevice (Hardware)

- (NSString *) platform
{
	size_t size;
    sysctlbyname("hw.machine", NULL, &size, NULL, 0);
    char *machine = malloc(size);
	sysctlbyname("hw.machine", machine, &size, NULL, 0);
	NSString *platform = [NSString stringWithCString:machine encoding: NSUTF8StringEncoding];
	free(machine);
	return platform;
}

@end
```

## Using DS-Digital font in the app ##
```
1) Copy the font file into the resources of your project. (Just drag it into the 'supporting files or whatever directory you use').

2) In your info.plist file enter the filename of the font file for key "Fonts Provided by the Application" - like in the picture below (I use two versions of the font, you must enter your font file name (DS-DIGI.TTF))



3) In places you want to use the font do [UIFont fontWithName:@"Digital-7" size: yoursize]

example:

someuilabel.font = [UIFont fontWithName:@"Digital-7" size: yoursize];
* EDIT: Note that the font has some confusing names. You can list all fonts installed on the phone with

// List all fonts on iPhone
  NSArray *familyNames = [[NSArray alloc] initWithArray:[UIFont familyNames]];
  NSArray *fontNames;
  NSInteger indFamily, indFont;
  for (indFamily=0; indFamily<[familyNames count]; ++indFamily)
  {
      NSLog(@"Family name: %@", [familyNames objectAtIndex:indFamily]);
      fontNames = [[NSArray alloc] initWithArray:
          [UIFont fontNamesForFamilyName:
          [familyNames objectAtIndex:indFamily]]];
      for (indFont=0; indFont<[fontNames count]; ++indFont)
      {
          NSLog(@"    Font name: %@", [fontNames objectAtIndex:indFont]);
      }
      [fontNames release];
  }
  [familyNames release];
And find the exact name of the font you want.
```

## Map view ##

## route-me ##
embedding: https://code.google.com/p/route-me/wiki/EmbeddingGuide

### Avoid calibration ###
http://stackoverflow.com/questions/9751970/iphone-locationmanagershoulddisplayheadingcalibration-ignored-does-nothing/9852402#9852402
> I believe I know what is causing this. If you use a MKMapView, and that uses the tracking mode with heading, then the mapview has its own location manager and that location manager is auto-set to ask for heading calibration. What a nightmare, as the location manager is a hidden variable with no access to it.

> The solution for MKMapView is to call this:
_mapView.userTrackingMode = MKUserTrackingModeNone;
just before you release_mapView.
This will make the calibration message disappear when you navigate to different pages.
> _I guess this answer suggests the dialog might be shown even after the mapView is released, but this never happened to me_

### About the need to set the mapview delegate to nil ###
http://stackoverflow.com/questions/8022609/ios-5-mapkit-crashes-with-overlays-when-zoom-pan
> You probably already thought about this, but I have seen numerous distinct-looking crashers from MapKit all stemming from not nil-ing out the delegate. Just make sure you set the map view's delegate to nil before you release it.

> Additionally, I've seen a number of developers inclined to use performSelector:afterDelay: on their map view. That's fine so long as you put a cancelperform call in the right places too.
### Check how often - (MKOverlayView **)mapView:(MKMapView**)mapView viewForOverlay:(id 

&lt;MKOverlay&gt;

)overlay is called!!!! ###

### Following the route with annotation ###
http://stackoverflow.com/questions/12918453/trying-to-simulate-a-route-in-mapview
http://stackoverflow.com/questions/11420482/ios-mapkit-following-a-path/11458574#11458574

**Suggestion is to change coordinates and UIanimation to move the annotation**
https://github.com/100grams/Moving-MKAnnotationView


### Animating the MKAnnotationView - blinking ###
http://stackoverflow.com/questions/14916974/ios-custom-mkannotation-object-resembling-mkuserlocation-animation

### Animating the annotation view coordinate? ###

```
Making the pins expand from the cluster center is actually pretty easy. When you make the new single-pin annotations, set their coordinates to the cluster center:

id <MKAnnotation> pin;
CLLocationCoordinate2D clusterCenter;
// ...
pin.coordinate = clusterCenter;
In viewForAnnotation:, don't animate the new pins:

MKPinAnnotationView *pinView;
// ...
pinView.animatesDrop = NO;
Then, after you've added the pins to the map view, you'll animate moving them to their real positions:

MKMapView *mapView;
id <MKAnnotation> pin;
// ...
// probably loop over annotations
[mapView addAnnotation:pin];
NSTimeInterval interval = 1.0; // or whatever
[UIView animateWithDuration:interval animations:^{
    // probably loop over annotations here again
    CLLocationCoordinate2D realCoord;
    // ...
    pin.coordinate = realCoord;
}];
As for the problem of non-clustered pins, that's harder to answer without knowing the implementation in detail, but I think there are lots of possibilities. You could just have a simple flag that skips the animation. Or you could just treat them exactly the same, and still "cluster" them even when they're solo, and still animate them ... not maximally efficient, but it would work and your code would be cleaner.
```

## SMTP clients ##
https://github.com/tcurdt/edmessage
http://stackoverflow.com/questions/740939/open-source-cocoa-cocoa-touch-pop3-smtp-library
not that good but for ios:
https://github.com/kailoa/iphone-smtp
**looking interesting:**
http://libmailcore.com

## Json iOS5+ ##
protocol
```
#import <Foundation/Foundation.h>

@protocol ObjectSerializer <NSObject>

- (id)deserializeStringToObject:(NSString *)string;
- (NSString *)serializeObjectToString:(id)object;

@end
```
.h
```
#import <Foundation/Foundation.h>
#import "ObjectSerializer.h"

@interface NativeJsonSerializer : NSObject<ObjectSerializer>

@end
```
.m
```
#import "NativeJsonSerializer.h"

@implementation NativeJsonSerializer

- (id)deserializeStringToObject:(NSString *)string
{
    NSData *data = [string dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error = nil;
    id result = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&error];
    if (!result) {
        NSLog(@"%@", error.description);
    }

    return result;
}

- (NSString *)serializeObjectToString:(id)data
{
    NSError *error;
    NSData *result = [NSJSONSerialization dataWithJSONObject:data options:NSJSONReadingAllowFragments|NSJSONWritingPrettyPrinted error:&error];
    if (!result) {
        NSLog(@"%@", error.description);
    }

    return [[NSString alloc] initWithData:result encoding:NSUTF8StringEncoding];
}

@end
```

## Rest libs for iOS and Mac ##
http://projects.lukeredpath.co.uk/resty/

## Vacuuming the sqlite db ##
```
 - (void) shrinkDB
    {
        sqlite3 * database;
        NSString * string = [shareStoreURL path];
        const char * filename = [string cStringUsingEncoding:[NSString defaultCStringEncoding]];
        char *errMsg;
        if (sqlite3_open(filename, &database) == SQLITE_OK)
        {
            NSLog(@"Shrinking...");
            if (sqlite3_exec(database, "VACUUM;", NULL, NULL, &errMsg) != SQLITE_OK)
            {
                NSLog(@"Failed execute VACUUM");
            }
            sqlite3_close(database);
         }
        }
```

## Quick forms ##
https://github.com/escoz/QuickDialog

## Google maps sdk for iOS ##
Example of deallocing in the viewcontroller with the map:

```
- (void)viewWillDisappear:(BOOL)animated
{
  [super viewWillDisappear:animated] ;

  [m_mapView clear] ;
  [m_mapView stopRendering] ;
  [m_mapView removeFromSuperview] ;
  m_mapView.delegate = nil;
  m_mapView = nil;
}
```

## Tracking retains with no ARC ##

```
-(id)retain
    {
    NSLog(@"%@", [NSThread callStackSymbols]);
    return ([super retain]);
    }
```

## KML ##

https://github.com/FLCLjp/KML-Logger/blob/master/GPSLogger/MapViewController.m

## Douglas Peucker in objective-c ##
https://github.com/tomislav/ShapeReducer-objc
```
#import <Foundation/Foundation.h>

@interface ShapePoint : NSObject {
    double latitude;
    double longitude;
    unsigned int sequence;
}

@property (nonatomic, assign) double latitude;
@property (nonatomic, assign) double longitude;
@property unsigned int sequence;

- (id)initWithLatitude:(double)aLatitude longitude:(double)aLongitude sequence:(unsigned int)aSequence;

@end

@interface Shape : NSObject {
@private
    NSMutableArray *_points;
    BOOL _needs_sort;
}

@property (nonatomic, retain) NSMutableArray *_points;
@property BOOL _needs_sort;

- (void)addPoint:(ShapePoint *)point;
- (NSArray *)points;

@end

@interface ShapeReducer : NSObject

- (Shape*)reduce:(Shape*)aShape tolerance:(double)tolerance;
- (void) douglasPeuckerReductionWithTolerance:(double)tolerance shape:(Shape*)shape outputShape:(Shape*)outputShape firstIndex:(int)first lastIndex:(int)last;
+ (double)orthogonalDistanceWithPoint:(ShapePoint *)point lineStart:(ShapePoint *)lineStart lineEnd:(ShapePoint *)lineEnd;
@end
```

```
#import "ShapeReducer.h"

@implementation ShapePoint

@synthesize latitude, longitude, sequence;

- (id)initWithLatitude:(double)aLatitude longitude:(double)aLongitude sequence:(unsigned int)aSequence {
	if ((self = [super init])) { 
		self.latitude = aLatitude;
		self.longitude = aLongitude;
        self.sequence = aSequence;
	} 
	return self; 
}

- (id)init
{
    self = [super init];
    return self;
}

@end

@implementation Shape

@synthesize _points, _needs_sort;

- (id)init
{
	if ((self = [super init])) { 
        _points = [[NSMutableArray alloc] init];
		_needs_sort = NO;
	} 
    return self;
}

- (void)addPoint:(ShapePoint *)point {
    [_points addObject:point];
    _needs_sort = YES;
}

- (NSArray *)points {
    if (_needs_sort) {
        NSComparisonResult (^sortBlock)(id, id) = ^(id obj1, id obj2) {
            if ([obj1 sequence] > [obj2 sequence]) { 
                return (NSComparisonResult)NSOrderedDescending;
            }
            if ([obj1 sequence] < [obj2 sequence]) {
                return (NSComparisonResult)NSOrderedAscending;
            }
            return (NSComparisonResult)NSOrderedSame;
        };
        NSArray *sortedPoints = [_points sortedArrayUsingComparator:sortBlock];
        return sortedPoints;
    }
    return _points;
}

- (void)dealloc {
	[_points release];
	[super dealloc];
}

@end


@implementation ShapeReducer

- (id)init
{
    self = [super init];
    
    return self;
}

- (Shape*)reduce:(Shape*)aShape tolerance:(double)tolerance {
    if (tolerance <= 0 || [aShape.points count] < 3) {
        return aShape;
    }
    
    NSArray *points = [aShape points];
    Shape *newShape = [[[Shape alloc] init] autorelease];
    
    [newShape addPoint:[points objectAtIndex:0]];
    [newShape addPoint:[points lastObject]];

    [self douglasPeuckerReductionWithTolerance:tolerance shape:aShape
                               outputShape:newShape firstIndex:0 lastIndex:[points count]-1];
        
    return newShape;
    
}

- (void) douglasPeuckerReductionWithTolerance:(double)tolerance shape:(Shape*)shape outputShape:(Shape*)outputShape firstIndex:(int)first lastIndex:(int)last { 
    if (last <= first + 1) {
        return;
    }
        
    NSArray *points = [shape points];
    
    double distance, maxDistance = 0.0;
    int indexFarthest = 0;
    
    ShapePoint *firstPoint = [points objectAtIndex:first];
    ShapePoint *lastPoint = [points objectAtIndex:last];
    
    for (int idx=first+1; idx<last; idx++) {
        ShapePoint *point = [points objectAtIndex:idx];
        
        distance = [ShapeReducer orthogonalDistanceWithPoint:point lineStart:firstPoint lineEnd:lastPoint];
        
        // if the current distance is larger then the other distances
        if (distance>maxDistance) {
            maxDistance=distance;
            indexFarthest=idx;
        }
    }
        
    if (maxDistance>tolerance && indexFarthest!=0) {
        //add index of Point to list of Points to keep
        [outputShape addPoint:[points objectAtIndex:indexFarthest]];
        
        [self douglasPeuckerReductionWithTolerance:tolerance shape:shape
                                   outputShape:outputShape firstIndex:first lastIndex:indexFarthest];
        
        [self douglasPeuckerReductionWithTolerance:tolerance shape:shape outputShape:outputShape firstIndex:indexFarthest lastIndex:last];
    }
}

+ (double)orthogonalDistanceWithPoint:(ShapePoint *)point lineStart:(ShapePoint *)lineStart lineEnd:(ShapePoint *)lineEnd
{
    double area = 0.0, bottom = 0.0, height = 0.0;
    area = ABS(
                      (
                       lineStart.latitude * lineEnd.longitude
                       + lineEnd.latitude * point.longitude
                       + point.latitude * lineStart.longitude
                       - lineEnd.latitude * lineStart.longitude
                       - point.latitude * lineEnd.longitude
                       - lineStart.latitude * point.longitude
                       ) / 2.0);
     
    bottom = sqrt(pow(lineStart.latitude - lineEnd.latitude, 2) +
                         pow(lineStart.longitude - lineEnd.longitude, 2));

    height = area / bottom * 2.0;
    
    return height;
}


@end
```

## Good one on location services enabled/disabled ##
http://www.peachpit.com/articles/article.aspx?p=1830485

## send image and text via facebook ios6 ##
```
if([SLComposeViewController isAvaliableForServiceType:SLServiceTypeFacebook]) {     
       SLComposeViewController*fvc = [SLComposeViewController
         composeViewControllerForServiceType:SLServiceTypeFacebook];
      [fvc setInitialText:@"Lhasa"];
      [fvc addImage:[UIImage imageNamed:@"lhasa"]];
      [self presentViewController:fvc animated:YES completion:nil];
}

```
## save view as an image ##
```
#import <QuartzCore/QuartzCore.h>

UIGraphicsBeginImageContext(pictureView.bounds.size);                  

[pictureView.layer renderInContext:UIGraphicsGetCurrentContext()]; 

UIImage *viewImage = UIGraphicsGetImageFromCurrentImageContext();

UIGraphicsEndImageContext();

UIImageWriteToSavedPhotosAlbum(viewImage, nil, nil, nil);
```

## Or go via all sharing options ? ##
```
NSString *text = @"Cat";
UIImage *image = [UIImage imageNamed:@"cat"];
NSArray *activityItems = [NSArray arrayWithObjects:text,image , nil];
UIActivityViewController *avc = [[UIActivityViewController alloc]
     initWithActivityItems: activityItems applicationActivities:nil];
[self presentViewController:avc animated:YES completion:nil];

```

## Just another view to file ##
```
//Get the size of the screen
CGRect screenRect = [[UIScreen mainScreen] bounds];
 
//Create a bitmap-based graphics context and make
//it the current context passing in the screen size
UIGraphicsBeginImageContext(screenRect.size);
 
CGContextRef ctx = UIGraphicsGetCurrentContext();
[[UIColor blackColor] set];
CGContextFillRect(ctx, screenRect);
 
//render the receiver and its sublayers into the specified context
//choose a view or use the window to get a screenshot of the
//entire device
[yourView.layer renderInContext:ctx];
 
UIImage *newImage = UIGraphicsGetImageFromCurrentImageContext();
 
//End the bitmap-based graphics context
UIGraphicsEndImageContext();
 
//Save UIImage to camera roll
UIImageWriteToSavedPhotosAlbum(newImage, nil, nil, nil);
```

## Vibrate ##
```
AudioServicesPlaySystemSound(kSystemSoundID_Vibrate);
```
And don't use the Alert one as it bips on no-vibration devices

## Twitter ##
https://dev.twitter.com/docs/ios/making-api-requests-slrequest

## Weather apis ##

http://openweathermap.org/
http://developer.worldweatheronline.com/

## charting ##

https://code.google.com/p/core-plot/

## Audio ##

http://www.subfurther.com/blog/2009/04/28/an-iphone-core-audio-brain-dump/

## Master detail in iPad mail ##

http://useyourloaf.com/blog/2011/11/16/mail-app-style-split-view-controller-with-a-sliding-master-v.html