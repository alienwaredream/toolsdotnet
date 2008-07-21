using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.RuleEngine;
using System.IO;
using System.Globalization;
using System.Xml;
using Tools.Core.Utils;
using System.Diagnostics;
using System.Xml.XPath;

namespace Tools.Logging.Biztalk
{
    /// <summary>
    /// Tracking interceptor class for the Biztalk Rules Engine.
    /// Formats events as xml and pushes them into the standard .NET logging pipe.
    /// </summary>
    /// <remarks>Not a thread safe class.
    /// </remarks>
    public class XmlDebugTrackingInterceptor : IRuleSetTrackingInterceptor, IDisposable
    {
        #region Fields

        private TraceSource source =
            new TraceSource(typeof(XmlDebugTrackingInterceptor).Assembly.GetName().Name);
        private Guid oldActivityId;

        private Action<string> XmlOutputTracker;

        private TrackingConfiguration trackingConfig;

        private static string m_addOperationTrace = "addOperation";
        private static string m_agendaUpdateTrace = "agendaUpdate";
        private static string m_assertOperationTrace = "assertOperation";
        private static string m_assertUnrecognizedOperationTrace = "assertUnrecognizedOperation";
        private static string m_conditionEvaluationTrace = "conditionEvaluation";
        private static string m_conflictResolutionCriteriaTrace = "conflictResolutionCriteria";
        #region Condition evaluation
        private static string m_operandInstanceIdTrace = "instanceId";
        private static string m_operandValueTrace = "value";
        private static string m_operandToStringValueTrace = "vizualizer";

        private static string m_leftOperandTrace = "leftOperand";
        private static string m_rightOperandTrace = "rightOperand";

        #endregion

        private static string m_objectInstanceTrace = "objectInstance";
        private static string m_objectTypeTrace = "objectType";
        private static string m_operationTypeTrace = "operationType";
        private static string m_removeOperationTrace = "removeOperation";
        private static string m_retractNotPresentOperationTrace = "retractNotPresentOperation";
        private static string m_retractOperationTrace = "retractOperation";
        private static string m_retractUnrecognizedOperationTrace = "retractUnrecognizedOperation";

        private string m_ruleEngineGuid;
        private static string m_ruleFiredTrace = "ruleFired";
        private static string m_ruleNameTrace = "ruleName";
        private string m_ruleSetName;
        private static string m_testExpressionTrace = "testExpression";
        private static string m_testResultFalseTrace = "False";
        private static string m_testResultTrace = "testResult";
        private static string m_testResultTrueTrace = "True";


        private static string m_traceHeaderTrace = "traceHeader";

        private static string m_unrecognizedOperationTrace = "unrecognizedOperation";
        private static string m_updateNotPresentOperationTrace = "updateNotPresentOperation";
        private static string m_updateOperationTrace = "updateOperation";
        private static string m_updateUnrecognizedOperationTrace = "updateUnrecognizedOperation";
        private static string m_workingMemoryUpdateTrace = "workingMemoryUpdate";

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public XmlDebugTrackingInterceptor()
        {
            oldActivityId = Trace.CorrelationManager.ActivityId;
        }

        public XmlDebugTrackingInterceptor(Action<string> outputTracker)
            : this()
        {
            this.XmlOutputTracker = outputTracker;
        }

        #endregion

        private void PrintHeader(string hdr, XmlWriter xml)
        {
            xml.WriteElementString("Action", hdr);
            xml.WriteElementString("EngineInstance", this.m_ruleEngineGuid);
            xml.WriteElementString("Ruleset", XmlUtility.Encode(this.m_ruleSetName));
        }

        public void SetTrackingConfig(TrackingConfiguration trackingConfig)
        {
            this.trackingConfig = trackingConfig;
        }

        public void TrackAgendaUpdate(bool isAddition, string ruleName, object conflictResolutionCriteria)
        {
            if (ruleName == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "strClassName" }), base.GetType().FullName, "ruleName");
            }
            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(300);



            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                AppendTraceHeader(xWriter);

                this.PrintHeader(m_agendaUpdateTrace, xWriter);

                xWriter.WriteElementString("Description", "Agenda Update");

                if (isAddition)
                {
                    xWriter.WriteElementString(m_operationTypeTrace, m_addOperationTrace);
                }
                else
                {
                    xWriter.WriteElementString(m_operationTypeTrace, m_removeOperationTrace);
                }
                xWriter.WriteElementString(m_ruleNameTrace, ruleName);
                if (conflictResolutionCriteria == null)
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, "null");
                }
                else
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, conflictResolutionCriteria.ToString());
                }

                CloseTrace(xWriter);
            }

            Log(builder);
        }

        public void TrackConditionEvaluation(string testExpression, string leftClassType, int leftClassInstanceId, object leftValue, string rightClassType, int rightClassInstanceId, object rightValue, bool result)
        {
            if (testExpression == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "strClassName" }), base.GetType().FullName, "testExpression");
            }
            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(200);

            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                AppendTraceHeader(xWriter);

                this.PrintHeader(m_conditionEvaluationTrace, xWriter);

                xWriter.WriteElementString("Description", String.Format("Evaluating condition: [{0},{1}] {2}",
                    leftClassInstanceId, rightClassInstanceId, testExpression));

                xWriter.WriteElementString(m_testExpressionTrace, testExpression);

                // write left instance and value information
                xWriter.WriteStartElement(m_leftOperandTrace);
                xWriter.WriteAttributeString(m_operandInstanceIdTrace, leftClassInstanceId.ToString());
                xWriter.WriteAttributeString(m_operandValueTrace, XmlUtility.Encode(GetOperandValue(leftValue)));
                //xWriter.WriteAttributeString(m_operandToStringValueTrace, XmlUtility.Encode((leftValue == null) ? "null" : leftValue.ToString()));

                xWriter.WriteEndElement();
                // write right instance and value information
                xWriter.WriteStartElement(m_rightOperandTrace);
                xWriter.WriteAttributeString(m_operandInstanceIdTrace, rightClassInstanceId.ToString());
                xWriter.WriteAttributeString(m_operandValueTrace, XmlUtility.Encode(GetOperandValue(rightValue)));
                //xWriter.WriteAttributeString(m_operandToStringValueTrace, XmlUtility.Encode((rightValue == null) ? "null" : rightValue.ToString()));
                xWriter.WriteEndElement();

                if (result)
                {
                    xWriter.WriteElementString(m_testResultTrace, m_testResultTrueTrace);
                }
                else
                {
                    xWriter.WriteElementString(m_testResultTrace, m_testResultFalseTrace);
                }
            }

            Log(builder);
        }

        private static string GetOperandValue(object leftValue)
        {
            if (leftValue == null)
            {
                return "null";
            }
            else if (leftValue.GetType().IsClass && (Type.GetTypeCode(leftValue.GetType()) != TypeCode.String))
            {
                return leftValue.GetHashCode().ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                return leftValue.ToString();
            }
        }

        public void TrackFactActivity(FactActivityType activityType, string classType, int classInstanceId)
        {
            if (classType == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "strClassName" }), base.GetType().FullName, "objectType");
            }
            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(200);

            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                AppendTraceHeader(xWriter);

                this.PrintHeader(m_workingMemoryUpdateTrace, xWriter);

                xWriter.WriteElementString("Description", String.Format("{0}ing {1} [{2}]",
                    activityType.ToString(), classType, classInstanceId));

                switch (activityType)
                {
                    case FactActivityType.Assert:
                        xWriter.WriteElementString(m_operationTypeTrace, m_assertOperationTrace);
                        break;

                    case FactActivityType.Retract:
                        xWriter.WriteElementString(m_operationTypeTrace, m_retractOperationTrace);
                        break;

                    case FactActivityType.Update:
                        xWriter.WriteElementString(m_operationTypeTrace, m_updateOperationTrace);
                        break;

                    case FactActivityType.AssertUnrecognized:
                        xWriter.WriteElementString(m_operationTypeTrace, m_assertUnrecognizedOperationTrace);
                        break;

                    case FactActivityType.RetractUnrecognized:
                        xWriter.WriteElementString(m_operationTypeTrace, m_retractUnrecognizedOperationTrace);
                        break;

                    case FactActivityType.UpdateUnrecognized:
                        xWriter.WriteElementString(m_operationTypeTrace, m_updateUnrecognizedOperationTrace);
                        break;

                    case FactActivityType.RetractNotPresent:
                        xWriter.WriteElementString(m_operationTypeTrace, m_retractNotPresentOperationTrace);
                        break;

                    case FactActivityType.UpdateNotPresent:
                        xWriter.WriteElementString(m_operationTypeTrace, m_updateNotPresentOperationTrace);
                        break;

                    default:
                        xWriter.WriteElementString(m_operationTypeTrace, m_unrecognizedOperationTrace);
                        break;
                }
                xWriter.WriteElementString(m_objectTypeTrace, classType);
                xWriter.WriteElementString(m_objectInstanceTrace, classInstanceId.ToString(CultureInfo.CurrentCulture));

                CloseTrace(xWriter);
            }

            Log(builder);
        }

        public void TrackRuleFiring(string ruleName, object conflictResolutionCriteria)
        {
            if (ruleName == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "ruleName" }), base.GetType().FullName, "ruleName");
            }
            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(200);

            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                AppendTraceHeader(xWriter);

                this.PrintHeader(m_ruleFiredTrace, xWriter);

                xWriter.WriteElementString("Description", "Firing Rule: " + ruleName);

                xWriter.WriteElementString(m_ruleNameTrace, ruleName);

                if (conflictResolutionCriteria == null)
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, "null");
                }
                else
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, conflictResolutionCriteria.ToString());
                }
                CloseTrace(xWriter);

            }
            Log(builder);
        }

        public void TrackRuleSetEngineAssociation(RuleSetInfo ruleSetInfo, Guid ruleEngineGuid)
        {
            Trace.CorrelationManager.ActivityId = ruleEngineGuid;

            source.TraceEvent(TraceEventType.Start, 0, String.Format("Executing ruleset {0} {1}.{2}",
                ruleSetInfo.Name, ruleSetInfo.MajorRevision, ruleSetInfo.MinorRevision));

            if (ruleSetInfo == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "strClassName" }), base.GetType().FullName, "ruleSetInfo");
            }

            this.m_ruleSetName = ruleSetInfo.Name;
            this.m_ruleEngineGuid = ruleEngineGuid.ToString();

            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(200);

            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                AppendTraceHeader(xWriter);
                xWriter.WriteElementString(m_traceHeaderTrace, this.m_ruleSetName);

                CloseTrace(xWriter);
            }

            Log(builder);
        }

        #region Helper methods

        private static void AppendTraceHeader(XmlWriter xWriter)
        {
            xWriter.WriteStartElement("TraceRecord", "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord/");
            xWriter.WriteElementString("TraceIdentifier", "http://code.google.com/p/toolsdotnet/log.aspx");
        }
        private static void CloseTrace(XmlWriter xWriter)
        {
            xWriter.WriteEndElement();
        }
        private void Log(StringBuilder builder)
        {
            // create navigator as a data to log, that to be normalized then either by the trace listener or
            // the logging adapter.
            XPathNavigator navigator = new XPathDocument(new StringReader(builder.ToString())).CreateNavigator();
            // log
            source.TraceData(TraceEventType.Verbose, 0, navigator);
            // provide extra output for test, etc purposes.
            if (this.XmlOutputTracker != null)
            {
                this.XmlOutputTracker(navigator.InnerXml);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Trace.CorrelationManager.ActivityId = oldActivityId;
        }

        #endregion
    }
}