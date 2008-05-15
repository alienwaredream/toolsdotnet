using System;
using System.Collections.Generic;
using System.Text;
using Tools.Common.Exceptions;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Tools.Common.Utils;
using Tools.Common.Logging;

namespace Tools.Common.Asserts
{
    /// <summary>
    /// An utility class for assertions.
    /// </summary>
    public static class ErrorTrap
    {
        private struct AssertionEvent
        {
            Enum MessageId { get; set; }
            string Text { get; set; }
            Func<bool> Condition { get; set; }
        }
        #region Fields
        [ThreadStatic()]
        private static List<Tools.Common.Messaging.Message> messages = new List<Tools.Common.Messaging.Message>();
        [ThreadStatic()]
        private static string accumulatedText = null;


        private static bool allowUIInteraction = false;

        #endregion

        #region Properties

        /// <summary>
        /// AppDomain scoped. Not thread static!
        /// If true allows interaction with an UI via Debug.Assert for example.
        /// This interaction is not always expected even in the Debug builds, for
        /// example for unit test runs.
        /// </summary>
        public static bool AllowUIInteraction
        {
            get { return allowUIInteraction; }
            set { allowUIInteraction = value; }
        }
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public static List<Tools.Common.Messaging.Message> Messages
        {
            get
            {
                if (messages != null)
                {
                    return messages;
                }
                return messages = new List<Tools.Common.Messaging.Message>();
            }
            set
            {
                messages = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public static bool HasErrors
        {
            get
            {
                return Messages.Count > 0;
            }
        }

        /// <summary>
        /// Gets the accumulated error trap text.
        /// </summary>
        /// <value>The text.</value>
        public static string Text
        {
            get
            {
                return accumulatedText;
            }
        } 
        #endregion
        
        /// <summary>
        /// Appends text to the current text of the errorTrap.
        /// </summary>
        /// <param name="textToAppend"></param>
        public static void AppendText(string textToAppend)
        {
            accumulatedText += textToAppend;
        }

        /// <summary>
        /// Adds the raisable assertion.
        /// </summary>
        /// <typeparam name="ExceptionType">The type of the xception type.</typeparam>
        /// <param name="condition">Condition to assert.</param>
        /// <param name="messageId">The message id.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of an exception to be thrown. Parameter of type T can't be provided.")]
        public static bool AddRaisableAssertion<ExceptionType>(bool condition,
            Enum messageId)
            where ExceptionType : Exception, new()
        {
            return AddRaisableAssertion<ExceptionType>(condition,
                FormatterUtility.GetEnumMemberNameForLogging(messageId));
        }


        /// <summary>
        /// Adds the raisable assertion.
        /// </summary>
        /// <typeparam name="ExceptionType">The type of the xception type.</typeparam>
        /// <param name="condition">Condition to assert.</param>
        /// <param name="messageId">The message id.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of an exception to be thrown. Parameter of type T can't be provided.")]
        public static bool AddRaisableAssertion<ExceptionType>(bool condition,
            Enum messageId, string serviceName)
            where ExceptionType : Exception, new()
        {
            return AddRaisableAssertion<ExceptionType>(condition,
                FormatterUtility.GetEnumMemberNameForLogging(messageId));
        }
        /// <summary>
        /// Adds the raisable assertion.
        /// </summary>
        /// <typeparam name="ExceptionType">The type of the xception type.</typeparam>
        /// <param name="condition">Condition to assert.If false, than the error is trapped.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of an exception to be thrown. Parameter of type T can't be provided.")]
        public static bool AddRaisableAssertion<ExceptionType>(bool condition,
            string text)
            where ExceptionType : Exception, new()
        {
            if (!AddAssertion(condition, text))
            {
                RaiseTrappedErrors<ExceptionType>();
            }
            return true;

        }

        /// <summary>
        /// Adds the assertion.
        /// </summary>
		/// <param name="condition">Condition to assert.If false, than the error is trapped.</param>
        /// <param name="messageId">The message id.</param>
        /// <param name="messageArguments">The message arguments.</param>
        /// <returns></returns>
        public static bool AddAssertion(bool condition, Enum messageId,
                    params object[] messageArguments)
        {
            return AddAssertion(condition,
                 FormatterUtility.GetEnumMemberNameForLogging(messageId));
        }
        public static bool AddAssertion(bool condition, string text,
                    params object[] messageArguments)
        {
            return AddAssertion(condition,
                 String.Format(text, messageArguments));
        }
        public static bool AddAssertion(bool condition, string text, Enum messageId,
            params object[] messageArguments)
        {
            return AddAssertion(condition,
                 FormatterUtility.GetEnumMemberNameForLogging(messageId));
        }
        /// <summary>
        /// Adds the assertion.
        /// </summary>
		/// <param name="condition">Condition to assert.If false, than the error is trapped.</param>
        /// <param name="messageId">The message id.</param>
        /// <returns></returns>
        public static bool AddAssertion(bool condition, Enum messageId)
        {
            return AddAssertion(condition,
                 FormatterUtility.GetEnumMemberNameForLogging(messageId));
        }
        /// <summary>
        /// Adds the assertion.
        /// </summary>
		/// <param name="condition">Condition to assert.If false, than the error is trapped.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static bool AddAssertion
            (
            bool condition,
            string text
            )
        {
            return AddAssertion(condition, 0, text, false);
        }

        /// <summary>
        /// Adds the assertion.
        /// </summary>
		/// <param name="condition">Condition to assert.If false, than the error is trapped.<see cref="Debug.Assert"/></param>
        /// <param name="messageId">The message id.</param>
        /// <param name="text">The text.</param>
        /// <param name="raiseException">if set to <c>true</c> [raise exception].</param>
        /// <returns></returns>
        private static bool AddAssertion
            (
            bool condition,
            object messageId,
            string text,
            bool raiseException
            )
        {
            //TODO: (SD) complete parameters check
            if (condition) return true;
            // Give the chance to the standard debug assert debugging compiles.
            if (allowUIInteraction) Debug.Assert(condition, text);

            int resolvedMessageId = 0;

            if (messageId == null || !int.TryParse(messageId.ToString(), out resolvedMessageId))
            {
                Log.Source.TraceEvent(TraceEventType.Warning, 0, "messageId received in the call to AddAssertion is either null or can't " +
                    "be parsed as an integer type. Supplied value is " + messageId ?? "null");
            }

            // TODO: (SD) Think about SeverityLevel
            Tools.Common.Messaging.Message message = new Tools.Common.Messaging.Message
                (Convert.ToInt32(messageId), messageId.ToString(), text,
                Tools.Common.Messaging.MessageType.None);


            accumulatedText += text;
            Messages.Add(message);

            Log.Source.TraceEvent(TraceEventType.Warning, 0, text, resolvedMessageId);

            // TODO: That call normally would not be here, to be redone in the next iter! (SD)
            if (raiseException)
            {
                throw new BaseException(message.Id, message.Text);
            }
            return false;
        }

        /// <summary>
        /// Raises the trapped errors.
        /// </summary>
        /// <typeparam name="ExceptionType">The type of the Exception type.</typeparam>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of an exception to be thrown. Parameter of type T can't be provided.")]
        public static void RaiseTrappedErrors<ExceptionType>() where ExceptionType : Exception, new()
        {
            if (!HasErrors) return;

            Exception exception = Activator.CreateInstance(typeof(ExceptionType), 
                new object[]{"Conditions are not met: " + accumulatedText}) as Exception;

            // If it is a base exception add accumulated messages as a collection
            BaseException baseExceptionTest = exception as BaseException;
            if (baseExceptionTest != null)
            {
                baseExceptionTest.Messages.AddRange(Messages);
            }

            Messages.Clear();
            accumulatedText = null;

            throw exception;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            if (messages!= null) messages.Clear();
            accumulatedText = null;
        }
    }
}
