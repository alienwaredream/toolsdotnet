## Callbacks vs events ##
With callback you don't need to care about unregistering as in case of event.
It is quite often hard to decide and create clean conditions when you should call for =- for you events unregistration.

Chaining of callbacks become quite messy when c# lambda are used. That is where system.reactive really helps