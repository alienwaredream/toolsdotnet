using System;
using System.Data;

using Tools.Coordination.Ems;
using System.Configuration;
using Tools.Core.Asserts;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace Tools.Commands.Implementation
{
    public class CommandExecutor : ICommandExecutor
    {

        #region Fields
        private EmsWriterQueue queue;
        private ICommand2MessageTranslator translator;
        string commandName;
        #endregion


        public CommandExecutor(EmsWriterQueue queue, ICommand2MessageTranslator translator, string commandName)
        {
            Init(queue, translator, commandName);
        }

        private void Init(EmsWriterQueue queue, ICommand2MessageTranslator translator, string commandName)
        {
            ErrorTrap.AddAssertion(queue != null, "EmsWriterQueue queue can't be null for the" + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.AddAssertion(translator != null, "ICommand2MessageTranslator translator can't be null. Please correct the configuration for " + this.GetType().FullName + " and restart.");


            ErrorTrap.AddAssertion(!String.IsNullOrEmpty(commandName), "commandName can't be null or empty. Please correct the configuration for " + this.GetType().FullName + " and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.queue = queue;
            this.translator = translator;
            this.commandName = commandName;
        }

        public bool Execute(GenericCommand command)
        {
            bool success = false;

            try
            {
                MessageShim shim = translator.TranslateToShim(command);

                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CommandMessages.CommandPreparedToBeSentToRequestQueue, String.Format("CorellationId: {0}, message text: \r\n{1}", shim.CorrelationId, shim.Text));

                if (ErrorTrap.HasErrors)
                {
                    return false;
                }

                queue.Open();

                queue.WriteTextMessage(shim.Text, shim.CorrelationId, commandName);


                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CommandMessages.CommandPreparedToBeSentToRequestQueue, String.Format("Command: {0} - delivered to queue {1}:{2}", shim.CorrelationId, queue.ServerConfig.Url, queue.QueueConfig.Name));

                return true;

            }
            catch (Exception ex)
            {
                if (queue != null) queue.Close();
                throw;
            }
        }

        public void Commit()
        {
            queue.Commit();
        }
    }
}