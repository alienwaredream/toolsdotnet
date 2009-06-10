using System;
using Tools.Failover;
using TIBCO.EMS;
using Tools.Core.Asserts;
using System.Configuration;
using System.Diagnostics;
using Tools.Core.Configuration;

namespace Tools.Coordination.Ems
{
    public class EmsWriterQueue : EmsQueueBase, IDisposable
    {
        private SessionConfiguration sessionConfig;
        public ServerConfiguration ServerConfig { get; private set; }
        private EmsReaderQueueConfiguration queueConfig;

        private ConnectionFactory factory;
        private Connection connection;
        private Session session;
        private MessageProducer producer;
        private Destination destination;

        private bool initialized;
        private IFailureExceptionHandler connectionFailureExceptionHandler =
            new DelayedFailureExceptionHandler(10000, 2, 30000);

        public IFailureExceptionHandler ExceptionHandler
        {
            get { return connectionFailureExceptionHandler; }
            set { connectionFailureExceptionHandler = value; }
        }

        public bool IsInitialized
        {
            get { return initialized; }
            set { initialized = value; }
        }

        public EmsWriterQueue(SessionConfiguration sessionConfig, ServerConfiguration serverConfig, EmsReaderQueueConfiguration queueConfig, IFailureExceptionHandler connectionFailureExceptionHandler)
        {
            this.sessionConfig = sessionConfig;
            ServerConfig = serverConfig;
            this.queueConfig = queueConfig;
            this.connectionFailureExceptionHandler = connectionFailureExceptionHandler;
        }
        // This is a bit from the java code used for the commands sending.


        //   public static boolean sendFullCommandMessage(String paramString1, String paramString2, String paramString3)
        //        TextMessage localTextMessage = localQueueSession.createTextMessage();
        //localTextMessage.setText(paramString3);
        //localTextMessage.setJMSCorrelationID(paramString1);
        //localTextMessage.setJMSType(paramString2);
        //localTextMessage.setJMSReplyTo(localTemporaryQueue);
        //localQueueSender.send(localTextMessage);
        //log.info(" --- Message sent --- Correlation: " + paramString1 + ", --- Type: " + paramString2 + ", --- Body: " + paramString3);
        // For the mts commands messageType is a command name and a sample is "increaselimit" for example.
        // I'm not sure if this is used for anything on the tibco side.
        public void WriteTextMessage(string body, string correlationId, string messageType)
        {

            TextMessage msg = session.CreateTextMessage(body);
            msg.CorrelationID = correlationId;
            msg.MsgType = messageType;

            producer.Send(msg);
            
        }
        
        public void Commit()
        {
            session.Commit();
        }
        public void Rollback()
        {
            session.Rollback();
        }

        public void Write(string obj)
        {

            Message msg = session.CreateMessage();

            msg.SetStringProperty("req", obj.ToString());
            msg.CorrelationID = Guid.NewGuid().ToString();
            msg.MessageID = "y6754";

            producer.Send(msg);
        }

        public void Close()
        {
            initialized = false;
            string exText = null;

            try
            {
                try
                {
                    if (producer != null) producer.Close();
                }
                catch (Exception ex)
                {
                    exText += ex.ToString();
                }
                try
                {
                    if (connection != null) connection.Close();
                }
                catch (Exception ex)
                {
                    exText += ex.ToString();
                }
            }
            catch (Exception ex)
            {
                exText += ex.ToString();
            }

            Log.TraceData(Log.Source, TraceEventType.Error, EmsCoordinationMessages.ErrorDuringEmsResourceCleanup, exText);
        }
        /// <summary>
        /// Opens if is not initialized yet, otherwise just returns with no action.
        /// </summary>
        public void Open()
        {
            if (!initialized)
            {

                ValidateQueueConfiguration();

                try
                {
                    factory = new ConnectionFactory(ServerConfig.Url, ServerConfig.ClientId);
                }
                catch (EMSException e)
                {
                    Log.TraceData(Log.Source, TraceEventType.Error, 15000, "URL/Client ID is wrong. " + e.ToString());
                    throw;
                }

                IConfigurationValueProvider configProvider = new SingleTagSectionConfigurationProvider(this.ServerConfig.AuthenticationSectionName);

                try
                {
                    connection = factory.CreateConnection(configProvider["userName"], configProvider["password"]);
                }
                catch (EMSException e)
                {
                    Log.TraceData(Log.Source, TraceEventType.Error, 15001, "Connection to ems server failed! " + e.ToString());
                    throw;
                }

                try
                {
                    session = connection.CreateSession(sessionConfig.IsTransactional, sessionConfig.Mode);
                }
                catch (EMSException e)
                {
                    Log.TraceData(Log.Source, TraceEventType.Error, 15002, "Error during session creation. " + e.ToString());
                    throw;
                }

                try
                {
                    destination =
                        CreateDestination(session, queueConfig.Name, queueConfig.Type);

                    producer =
                        session.CreateProducer(destination);
                    producer.MsgDeliveryMode = MessageDeliveryMode.Persistent;

                    connection.Start();
                }
                catch (EMSException e)
                {
                    Log.TraceData(Log.Source, TraceEventType.Error, 15003, "Queue initialization error. " + e);
                    throw;
                }
                initialized = true;
            }
        }

        public bool RecoverFromConnectionError(Exception ex)
        {
            if (ex is ConfigurationErrorsException)
            {
                return false;
            }
            if (connectionFailureExceptionHandler != null)
            {
                if (connectionFailureExceptionHandler.HandleFailure(ex) == FailureExceptionType.NonRecoverable)
                {
                    return false;
                }
                return true;
            }
            // if there is no handler, return false so we can fail-fast or stop
            return false;
        }

        private void ValidateQueueConfiguration()
        {
            ErrorTrap.AddAssertion(sessionConfig != null, "SessionConfig != null", EmsCoordinationMessages.InvalidConfiguration, null);

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

        }

        private static Destination CreateDestination(Session sess, string name, QueueType type)
        {
            Destination dest;
            switch (type)
            {
                case QueueType.Queue:
                    dest = sess.CreateQueue(name);
                    break;
                case QueueType.Topic:
                    dest = sess.CreateTopic(name);
                    break;
                default:
                    throw new ApplicationException("Internal error");
            }
            return dest;
        }



        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
#region Original java code - decompiled

//package com.empiry.emswrapper.service;

//import com.empiry.emswrapper.Constants;
//import com.empiry.emswrapper.jms.JMSQueueConnectionFactory;
//import java.util.ArrayList;
//import java.util.Arrays;
//import java.util.List;
//import java.util.ResourceBundle;
//import java.util.logging.Logger;
//import javax.jms.JMSException;
//import javax.jms.Queue;
//import javax.jms.QueueConnection;
//import javax.jms.QueueReceiver;
//import javax.jms.QueueSender;
//import javax.jms.QueueSession;
//import javax.jms.TemporaryQueue;
//import javax.jms.TextMessage;

//public class JMSMessageSender
//{
//  private static final Logger log = Logger.getLogger("com.empiry.emswrapper.service");
//  private static final ResourceBundle rb = ResourceBundle.getBundle("emswrapper");

//  public static boolean sendFullCommandMessage(String paramString1, String paramString2, String paramString3)
//    throws Exception
//  {
//    int i = 1;
//    QueueConnection localQueueConnection = null;
//    try
//    {
//      ArrayList localArrayList = new ArrayList(Arrays.asList(Constants.JMS_MESSAGE_TYPES));
//      if (!(localArrayList.contains(paramString2)))
//        throw new JMSException("Bad message type");
//      localQueueConnection = JMSQueueConnectionFactory.getInstance().getConnection("command.send.jms.");
//      QueueSession localQueueSession = localQueueConnection.createQueueSession(false, 1);
//      Queue localQueue = localQueueSession.createQueue(JMSQueueConnectionFactory.getInstance().getQueueName("command.send.jms."));
//      QueueSender localQueueSender = localQueueSession.createSender(localQueue);
//      TemporaryQueue localTemporaryQueue = localQueueSession.createTemporaryQueue();
//      TextMessage localTextMessage = localQueueSession.createTextMessage();
//      localTextMessage.setText(paramString3);
//      localTextMessage.setJMSCorrelationID(paramString1);
//      localTextMessage.setJMSType(paramString2);
//      localTextMessage.setJMSReplyTo(localTemporaryQueue);
//      localQueueSender.send(localTextMessage);
//      log.info(" --- Message sent --- Correlation: " + paramString1 + ", --- Type: " + paramString2 + ", --- Body: " + paramString3);
//    }
//    catch (Exception localException)
//    {
//      log.info(" ---ERROR--- Send Message --- Queue: " + JMSQueueConnectionFactory.getInstance().getQueueName("command.send.jms."));
//      localException.printStackTrace();
//      i = 0;
//    }
//    finally
//    {
//      if (localQueueConnection != null)
//        localQueueConnection.close();
//    }
//    return i;
//  }

//  public static boolean sendCommand(String paramString1, String paramString2, String paramString3)
//    throws Exception
//  {
//    String str1 = rb.getString("command.jms.msg.body.prefix");
//    String str2 = rb.getString("command.jms.msg.body.postfix");
//    String str3 = str1 + paramString3 + str2;
//    return sendFullCommandMessage(paramString1, paramString2, str3);
//  }

//  public static String sendReqRes(String paramString1, String paramString2, String paramString3)
//    throws Exception
//  {
//    String str1 = "noData";
//    QueueConnection localQueueConnection = null;
//    String str2 = paramString1 + "." + "send.jms.";
//    try
//    {
//      localQueueConnection = JMSQueueConnectionFactory.getInstance().getConnection(str2);
//      QueueSession localQueueSession = localQueueConnection.createQueueSession(false, 1);
//      Queue localQueue = localQueueSession.createQueue(JMSQueueConnectionFactory.getInstance().getQueueName(str2));
//      QueueSender localQueueSender = localQueueSession.createSender(localQueue);
//      TemporaryQueue localTemporaryQueue = localQueueSession.createTemporaryQueue();
//      QueueReceiver localQueueReceiver = localQueueSession.createReceiver(localTemporaryQueue);
//      TextMessage localTextMessage1 = localQueueSession.createTextMessage();
//      localTextMessage1.setText(paramString3);
//      localTextMessage1.setJMSCorrelationID(paramString2);
//      localTextMessage1.setJMSType(paramString1);
//      localTextMessage1.setJMSReplyTo(localTemporaryQueue);
//      localQueueSender.send(localTextMessage1);
//      log.info(" --- Message sent --- Type: " + paramString1 + ", --- Correlation: " + paramString2 + ", --- Body: " + paramString3);
//      TextMessage localTextMessage2 = (TextMessage)localQueueReceiver.receive(JMSQueueConnectionFactory.getInstance().getTimeout(str2));
//      if (localTextMessage2 == null)
//      {
//        log.info(" --- No response for --- Correlation: " + paramString2 + ", --- Body: " + paramString3);
//        str1 = "noResponse";
//        throw new JMSException("There is no response for sent message!");
//      }
//      log.info(" --- Response received for --- Correlation: " + paramString2 + ", --- Body: " + paramString3);
//      str1 = localTextMessage2.getText();
//      log.info(" --- Response text " + str1);
//    }
//    catch (Exception localException)
//    {
//      log.info(" ---ERROR--- Send Message --- Queue: " + JMSQueueConnectionFactory.getInstance().getQueueName(str2));
//      localException.printStackTrace();
//    }
//    finally
//    {
//      if (localQueueConnection != null)
//        localQueueConnection.close();
//    }
//    return str1;
//  }

//  public static String sendTypedReqRes(String paramString1, String paramString2, String paramString3)
//    throws Exception
//  {
//    String str = "noData";
//    try
//    {
//      ArrayList localArrayList = new ArrayList(Arrays.asList(Constants.ACTION_TYPES));
//      if (!(localArrayList.contains(paramString1)))
//        throw new JMSException("Bad action type");
//      str = sendReqRes(paramString1, paramString2, paramString3);
//    }
//    catch (Exception localException)
//    {
//      log.info(" ---ERROR--- Send Message --- Type: " + paramString1);
//      localException.printStackTrace();
//    }
//    return str;
//  }

//  public static String sendIncreaseLimit(String paramString1, String paramString2)
//    throws Exception
//  {
//    return sendTypedReqRes("increaselimit", paramString1, paramString2);
//  }
//}

#endregion