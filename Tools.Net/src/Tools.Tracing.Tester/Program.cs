using System;
using System.Diagnostics;
using Tools.Core.Context;
using Tools.Tracing.ClientHandler;
using Tools.Tracing.Common;

namespace Tools.Tracing.Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.Write("Press any key to test the client");
                Console.ReadKey();

                var tracingClient = new TraceEventHandlerClient("localhost", "9020", "TraceEventHandlerWrapper.rem");

                tracingClient.HandleEvent(new TraceEvent
                                              {
                                                  Category = EventCategory.Debugging,
                                                  Context = "Context",
                                                  ContextIdentifier = new ContextIdentifier
                                                                          {
                                                                              AuthenticationTokenId = 0,
                                                                              ContextGuid = Guid.NewGuid(),
                                                                              ContextHolderId = 1,
                                                                              ExternalId = "456454",
                                                                              ExternalReference = "ERef1",
                                                                              ExternalParentId = "EParentId",
                                                                              InternalId = 56,
                                                                              InternalParentId = 57
                                                                          },
                                                  EventId = new Random().Next(500000),
                                                  EventIdText = null,
                                                  Handled = false,
                                                  LifeCycleType = ApplicationLifeCycleType.Runtime,
                                                  Message = "This is a test message sent through the remoting channel",
                                                  Type = TraceEventType.Information
                                              });
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            Console.Write("Press any key to exit");
            Console.ReadKey();
        }
    }
}