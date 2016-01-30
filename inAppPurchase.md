## Retrieve list of products, show UI ##

https://developer.apple.com/library/ios/documentation/NetworkingInternet/Conceptual/StoreKitGuide/Chapters/ShowUI.html#//apple_ref/doc/uid/TP40008267-CH3-SW5


Embedding Product IDs in the App Bundle
Include a property list file in your app bundle containing an array of product identifiers, such as the following:
```
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN"
 "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<array>
    <string>com.example.level1</string>
    <string>com.example.level2</string>
    <string>com.example.rocket_car</string>
</array>
</plist>
```
To get product identifiers from the property list, locate the file in the app bundle and read it.
```
NSURL *url = [[NSBundle mainBundle] URLForResource:@"product_ids"
                                     withExtension:@"plist"];
NSArray *productIdentifiers = [NSArray arrayWithContentsOfURL:url];
```

## Retrieving Product Information ##

To make sure your users see only products that are actually available for purchase, query the App Store before displaying your app’s store UI.

Use a products request object to query the App Store. First, create an instance of SKProductsRequest and initialize it with a list of product identifiers. The products request retrieves information about valid products, along with a list of the invalid product identifiers, and then calls its delegate to process the result. The delegate must implement the SKProductsRequestDelegate protocol to handle the response from the App Store. Listing 2-2 shows a simple implementation of both pieces of code.

!!! Before starting the products request check for [canMakePayments](SKPaymentQueue.md):
```
if ([SKPaymentQueue canMakePayments]) {
    //prepare to purchase            
        SKProductsRequest *request =[[SKProductsRequest alloc] initWithProductIdentifiers:
                                     [NSSet setWithObject:completeIdentifier]];
        request.delegate = self;
        [request start];
    }
```

Listing 2-2  Retrieving product information
```
// Custom method
- (void)validateProductIdentifiers:(NSArray *)productIdentifiers
{
    SKProductsRequest *productsRequest = [[SKProductsRequest alloc]
        initWithProductIdentifiers:[NSSet setWithArray:productIdentifiers]];
    productsRequest.delegate = self;
    [productsRequest start];
}
 
// SKProductsRequestDelegate protocol method
- (void)productsRequest:(SKProductsRequest *)request
     didReceiveResponse:(SKProductsResponse *)response
{
    self.products = response.products;
 
    for (NSString *invalidIdentifier in response.invalidProductIdentifiers) {
        // Handle any invalid product identifiers.
    }
 
    [self displayStoreUI]; // Custom method
}
```

Listing 2-3  Formatting a product’s price
```
NSNumberFormatter *numberFormatter = [[NSNumberFormatter alloc] init];
[numberFormatter setFormatterBehavior:NSNumberFormatterBehavior10_4];
[numberFormatter setNumberStyle:NSNumberFormatterCurrencyStyle];
[numberFormatter setLocale:product.priceLocale];
NSString *formattedPrice = [numberFormatter stringFromNumber:product.price];
After a user selects a product to buy, your app connects to the App Store to request payment for the product.
```

## Creating a Payment Request ##

When the user selects a product to buy, create a payment request using a product object, and set the quantity if needed, as shown in Listing 3-1. The product object comes from the array of products returned by your app’s products request, as discussed in Retrieving Product Information.

Listing 3-1  Creating a payment request
```
SKProduct *product = <# Product returned by a products request #>;
SKMutablePayment *payment = [SKMutablePayment paymentWithProduct:product];
payment.quantity = 2;
```

## Submitting a Payment Request ##

Adding a payment request to the transaction queue submits it the App Store. If you add a payment object to the queue multiple times, it’s submitted multiple times—the user is charged multiple times and your app is expected to deliver the product multiple times.
```
[[SKPaymentQueue defaultQueue] addPayment:payment];
```

## Waiting for the App Store to Process Transactions ##

For every payment request your app submits, it gets back a corresponding transaction that it must process. Transactions and the transaction queue are discussed in Waiting for the App Store to Process Transactions.

Register a transaction queue observer when your app is launched, as shown in Listing 4-1. Make sure that the observer is ready to handle a transaction at any time, not just after you add a transaction to the queue. For example, consider the case of a user buying something in your app right before going into a tunnel. Your app isn’t able to deliver the purchased content because there’s no network connection. The next time your app is launched, Store Kit calls your transaction queue observer again and delivers the purchased content at that time. Similarly, if your app fails to mark a transaction as finished, Store Kit calls the observer every time your app is launched until the transaction is properly finished.

!!! Move as close to a start as possible! And then add the following line at the beginning of application:didFinishLaunchingWithOptions:
[sharedInstance](RageIAPHelper.md);

Listing 4-1  Registering the transaction queue observer
```
- (BOOL)application:(UIApplication *)application
 didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    /* ... */
 
    [[SKPaymentQueue defaultQueue] addTransactionObserver:observer];
}

```

Implement the paymentQueue:updatedTransactions: method on your transaction queue observer. Store Kit calls this method when the status of a transaction changes—for example, when a payment request has been processed. The transaction status tells you what action your app needs to perform, as shown in Table 4-1 and Listing 4-2. Transactions in the queue can change state in any order. Your app needs to be ready to work on any active transaction at any time.

Listing 4-2  Responding to transaction statuses
```
- (void)paymentQueue:(SKPaymentQueue *)queue
 updatedTransactions:(NSArray *)transactions
{
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            // Call the appropriate custom method for the transaction state.
            case SKPaymentTransactionStatePurchasing:
                [self showTransactionAsInProgress:transaction deferred:NO];
                break;
            case SKPaymentTransactionStateDeferred:
                [self showTransactionAsInProgress:transaction deferred:YES];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
            default:
                // For debugging
                NSLog(@"Unexpected transaction state %@", @(transaction.transactionState));
                break;
        }
    }
}
```

To keep your user interface up to date while waiting, the transaction queue observer can implement optional methods from the SKPaymentTransactionObserver protocol as follows. The paymentQueue:removedTransactions: method is called when transactions are removed from the queue—in your implementation of this method, remove the corresponding items from your app’s UI. The **paymentQueueRestoreCompletedTransactionsFinished:** or **paymentQueue:restoreCompletedTransactionsFailedWithError:** method is called when Store Kit finishes restoring transactions, depending on whether there was an error. In your implementation of these methods, update your app’s UI to reflect the success or error.

!!! Look how to finish
```
- (void)completeTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"completeTransaction...");
 
    [self provideContentForProductIdentifier:transaction.payment.productIdentifier];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}
 
- (void)restoreTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"restoreTransaction...");
 
    [self provideContentForProductIdentifier:transaction.originalTransaction.payment.productIdentifier];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}
 
- (void)failedTransaction:(SKPaymentTransaction *)transaction {
 
    NSLog(@"failedTransaction...");
    if (transaction.error.code != SKErrorPaymentCancelled)
    {
        NSLog(@"Transaction error: %@", transaction.error.localizedDescription);
    }
 
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
}
```

## Persisting the Purchase ##

After making the product available, your app needs to make a persistent record of the purchase. Your app uses that persistent record on launch to continue to make the product available. It also uses that record to restore purchases, as described in Restoring Purchased Products. Your app’s persistence strategy depends the type of products you sell and the versions of iOS.

For non-consumable products and auto-renewable subscriptions in iOS 7 and later, use the app receipt as your persistent record.
For non-consumable products and auto-renewable subscriptions in versions of iOS earlier than iOS 7, use the User Defaults system or iCloud to keep a persistent record.

### Persisting a Value in User Defaults or iCloud ###
To store information in User Defaults or iCloud, set the value for a key.
```
#if USE_ICLOUD_STORAGE
NSUbiquitousKeyValueStore *storage = [NSUbiquitousKeyValueStore defaultStore];
#else
NSUserDefaults *storage = [NSUserDefaults standardUserDefaults];
#endif
 
[storage setBool:YES forKey:@"enable_rocket_car"];
[storage setObject:@15 forKey:@"highest_unlocked_level"];
 
[storage synchronize];
```

Persisting a Receipt in User Defaults or iCloud
To store a transaction’s receipt in User Defaults or iCloud, set the value for a key to the data of that receipt.
```
#if USE_ICLOUD_STORAGE
NSUbiquitousKeyValueStore *storage = [NSUbiquitousKeyValueStore defaultStore];
#else
NSUserDefaults *storage = [NSUserDefaults standardUserDefaults];
#endif
 
NSData *newReceipt = transaction.transactionReceipt;
NSArray *savedReceipts = [storage arrayForKey:@"receipts"];
if (!receipts) {
    // Storing the first receipt
    [storage setObject:@[newReceipt] forKey:@"receipts"];
} else {
    // Adding another receipt
    NSArray *updatedReceipts = [savedReceipts arrayByAddingObject:newReceipt];
    [storage setObject:updatedReceipts forKey:@"receipts"];
}
 
[storage synchronize];
```

## Unlocking App Functionality ##

If the product enables app functionality, set a Boolean value to enable the code path and update your user interface as needed. To determine what functionality to unlock, consult the persistent record that your app made when the transaction occurred. Your app needs to update this Boolean value whenever a purchase is completed and at app launch.

For example, using the app receipt, your code might look like the following:
```
NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
NSData *receiptData = [NSData dataWithContentsOfURL:receiptURL];
 
// Custom method to work with receipts
BOOL rocketCarEnabled = [self receipt:receiptData
        includesProductID:@"com.example.rocketCar"];

```

Or, using the User Defaults system:
```
NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
BOOL rocketCarEnabled = [defaults boolForKey:@"enable_rocket_car"];
Then use that information to enable the appropriate code paths in your app.

if (rocketCarEnabled) {
    // Use the rocket car.
} else {
    // Use the regular car.
}
```

## Finishing the Transaction ##

Finishing a transaction tells Store Kit that you’ve completed everything needed for the purchase. Unfinished transactions remain in the queue until they’re finished, and the transaction queue observer is called every time your app is launched so your app can finish the transactions. Your app needs to finish every transaction, regardless of whether the transaction succeeded or failed.

Complete all of the following actions before you finish the transaction:

Persist the purchase.
Download associated content.
Update your app’s UI to let the user access the product.
To finish a transaction, call the finishTransaction: method on the payment queue.
```
SKPaymentTransaction *transaction = <# The current payment #>;
[[SKPaymentQueue defaultQueue] finishTransaction:transaction];
```

After you finish a transaction, don’t take any actions on that transaction or do any work to deliver the product. If any work remains, your app isn’t ready to finish the transaction yet.

## Restoring Purchased Products ##

Users restore transactions to maintain access to content they’ve already purchased. For example, when they upgrade to a new phone, they don’t lose all of the items they purchased on the old phone. Include some mechanism in your app to let the user restore their purchases, such as a Restore Purchases button. Restoring purchases prompts for the user’s App Store credentials, which interrupts the flow of your app: because of this, don’t automatically restore purchases, especially not every time your app is launched.

In most cases, all your app needs to do is refresh its receipt and deliver the products in its receipt. The refreshed receipt contains a record of the user’s purchases in this app, on this device or any other device.

### Refreshing the App Receipt ###

Create a receipt refresh request, set a delegate, and start the request. The request supports optional properties for obtaining receipts in various states during testing such as expired receipts—for details, see the values for the initWithReceiptProperties: method of SKReceiptRefreshRequest.
```
request = [[SKReceiptRefreshRequest alloc] init];
request.delegate = self;
[request start];
After the receipt is refreshed, examine it and deliver any products that were added.
```

### Restoring Completed Transactions ###

Your app starts the process by calling the restoreCompletedTransactions method of SKPaymentQueue. This sends a request to the App Store to restore all of your app’s completed transactions. If your app sets a value for the applicationUsername property of its payment requests, as described in Detecting Irregular Activity, use the restoreCompletedTransactionsWithApplicationUsername: method to provide the same information when restoring transactions.

The App Store generates a new transaction for each transaction that was previously completed. The restored transaction has a reference to the original transaction: instances of SKPaymentTransaction have a originalTransaction property, and the entries in the receipt have an Original Transaction Identifier field.

Note: The date fields have slightly different meanings for restored purchases. For details, see the Purchase Date and Original Purchase Date fields in Receipt Validation Programming Guide.
Your transaction queue observer is called with a status of SKPaymentTransactionStateRestored for each restored transaction, as described in Waiting for the App Store to Process Transactions. The action you take at this point depends on the design of your app.

If your app uses the app receipt and doesn’t have Apple-hosted content, this code isn’t needed because your app doesn’t restore completed transactions. Finish any restored transactions immediately.
If your app uses the app receipt and has Apple-hosted content, let the user select which products to restore before starting the restoration process. During restoration, re-download the user-selected content and finish any other transactions immediately.
```
NSMutableArray *productIDsToRestore = <# From the user #>;
SKPaymentTransaction *transaction = <# Current transaction #>;
 
if ([productIDsToRestore containsObject:transaction.transactionIdentifier]) {
    // Re-download the Apple-hosted content, then finish the transaction
    // and remove the product identifier from the array of product IDs.
} else {
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}
```

## Articles ##

http://www.raywenderlich.com/21081/introduction-to-in-app-purchases-in-ios-6-tutorial

## Sample implementations ##

https://github.com/mattt/CargoBay - Liked this one more

https://github.com/rsanchezsaez/CargoManager/tree/master/CargoManager