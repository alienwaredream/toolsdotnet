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
        Int32 postExecutionDelay = 4000;
        private bool skipSendingToQueue;

        #endregion

        #region Properties

        public Int32 PostExecutionDelay { get { return postExecutionDelay; } set { postExecutionDelay = value; } }

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

                if (ErrorTrap.HasErrors)
                {
                    return false;
                }

                if (!skipSendingToQueue)
                {
                    queue.Open();

                    queue.WriteTextMessage(shim.Text, shim.CorrelationId, commandName);

                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CommandMessages.CommandDeliveredToRequestQueue, String.Format("Command[Type={0}, Id ={1}] - delivered to queue {2}:{3}", command.CommandType, shim.CorrelationId, queue.ServerConfig.Url, queue.QueueConfig.Name));
                }
                else
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Warning, CommandMessages.CommandDeliveredToRequestQueue, String.Format("Command[Type={0}, Id ={1}] - would be delivered to queue {2}:{3}. But this is skipped as skipSendingToQueue is set to true.", command.CommandType, shim.CorrelationId, queue.ServerConfig.Url, queue.QueueConfig.Name));
                }

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