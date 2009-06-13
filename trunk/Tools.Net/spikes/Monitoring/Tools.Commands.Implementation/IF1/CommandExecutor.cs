using System;
using System.Data;

using Tools.Coordination.Ems;
using System.Configuration;
using Tools.Core.Asserts;

namespace Tools.Commands.Implementation
{
    public delegate bool ExecuteCommand(GenericCommand command);


    public class CommandExecutor : ICommandExecutor
    {

        private EmsWriterQueue queue;
        private ICommand2MessageTranslator translator;


        public CommandExecutor(EmsWriterQueue queue, ICommand2MessageTranslator translator)
        {
            Init(queue, translator);
        }

        private void Init(EmsWriterQueue queue, ICommand2MessageTranslator translator)
        {
            ErrorTrap.AddAssertion(queue != null, "EmsWriterQueue queue can't be null for the" + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.AddAssertion(translator != null, "ICommand2MessageTranslator translator can't be null. Please correct the configuration for " + this.GetType().FullName + " and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.queue = queue;
            this.translator = translator;
        }

        public bool Execute(GenericCommand command)
        {
            bool success = false;

            try
            {
                MessageShim shim = translator.TranslateToShim(command);

                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Verbose, CommandMessages.CommandPreparedToBeSentToRequestQueue, String.Format("CorellationId: {0}, message text: \r\n{1}", shim.CorrelationId, shim.Text));
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}