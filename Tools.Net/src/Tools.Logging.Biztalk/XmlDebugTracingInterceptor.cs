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
    public class XmlDebugTrackingInterceptor : IRuleSetTrackingInterceptor
    {
        // Fields

        private TraceSource source =
            new TraceSource(typeof(XmlDebugTrackingInterceptor).Assembly.GetName().Name);

        private static string m_addOperationTrace = "addOperation";
        private static string m_agendaUpdateTrace = "agendaUpdate";
        private static string m_assertOperationTrace = "assertOperation";
        private static string m_assertUnrecognizedOperationTrace = "assertUnrecognizedOperation";
        private static string m_conditionEvaluationTrace = "conditionEvaluation";
        private static string m_conflictResolutionCriteriaTrace = "conflictResolutionCriteria";

        private static string m_leftOperandValueTrace = "leftOperandValue";

        private static string m_objectInstanceTrace = "objectInstance";
        private static string m_objectTypeTrace = "objectType";
        private static string m_operationTypeTrace = "operationType";
        private static string m_removeOperationTrace = "removeOperation";
        private static string m_retractNotPresentOperationTrace = "retractNotPresentOperation";
        private static string m_retractOperationTrace = "retractOperation";
        private static string m_retractUnrecognizedOperationTrace = "retractUnrecognizedOperation";
        private static string m_rightOperandValueTrace = "rightOperandValue";
        private string m_ruleEngineGuid;
        private static string m_ruleEngineInstanceTrace = "ruleEngineInstance";
        private static string m_ruleFiredTrace = "ruleFired";
        private static string m_ruleNameTrace = "ruleName";
        private string m_ruleSetName;
        private static string m_rulesetNameTrace = "ruleSetName";
        private static string m_testExpressionTrace = "testExpression";
        private static string m_testResultFalseTrace = "testResultFalse";
        private static string m_testResultTrace = "testResult";
        private static string m_testResultTrueTrace = "testResultTrue";


        private static string m_traceHeaderTrace = "traceHeader";

        private static string m_unrecognizedOperationTrace = "unrecognizedOperation";
        private static string m_updateNotPresentOperationTrace = "updateNotPresentOperation";
        private static string m_updateOperationTrace = "updateOperation";
        private static string m_updateUnrecognizedOperationTrace = "updateUnrecognizedOperation";
        private static string m_workingMemoryUpdateTrace = "workingMemoryUpdate";


        private void PrintHeader(string hdr, XmlWriter xml)
        {
            xml.WriteElementString("Action", hdr);
            xml.WriteElementString("EngineInstance", this.m_ruleEngineGuid);
            xml.WriteElementString("Ruleset", XmlUtility.Encode(this.m_ruleSetName));
        }

        public void SetTrackingConfig(TrackingConfiguration trackingConfig)
        {
            //if (this.disposed)
            //{
            //    throw new ObjectDisposedException(base.GetType().FullName);
            //}
        }

        public void TrackAgendaUpdate(bool isAddition, string ruleName, object conflictResolutionCriteria)
        {
            if (ruleName == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, "nullArgument", new object[] { "strClassName" }), base.GetType().FullName, "ruleName");
            }
            // Create a builder to write xml to
            StringBuilder builder = new StringBuilder(200);

            using (XmlWriter xWriter = XmlWriter.Create(builder,
                    new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment }))
            {
                this.PrintHeader(m_agendaUpdateTrace, xWriter);
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

            }
            source.TraceData(TraceEventType.Verbose, 0,
                new XPathDocument(new StringReader(builder.ToString())).CreateNavigator());
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
                this.PrintHeader(m_conditionEvaluationTrace, xWriter);
                xWriter.WriteElementString(m_testExpressionTrace, testExpression);
                if (leftValue == null)
                {
                    xWriter.WriteElementString(m_leftOperandValueTrace, "null");
                }
                else if (leftValue.GetType().IsClass && (Type.GetTypeCode(leftValue.GetType()) != TypeCode.String))
                {
                    xWriter.WriteElementString(m_leftOperandValueTrace, m_objectInstanceTrace + " " + leftValue.GetHashCode().ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    xWriter.WriteElementString(m_leftOperandValueTrace, leftValue.ToString());
                }
                if (rightValue == null)
                {
                    xWriter.WriteElementString(m_rightOperandValueTrace, "null");
                }
                else if (rightValue.GetType().IsClass && (Type.GetTypeCode(rightValue.GetType()) != TypeCode.String))
                {
                    xWriter.WriteElementString(m_rightOperandValueTrace, m_objectInstanceTrace + " " + rightValue.GetHashCode().ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    xWriter.WriteElementString(m_rightOperandValueTrace, rightValue.ToString());
                }
                if (result)
                {
                    xWriter.WriteElementString(m_testResultTrace, m_testResultTrueTrace);
                }
                else
                {
                    xWriter.WriteElementString(m_testResultTrace, m_testResultFalseTrace);
                }
            }
            source.TraceData(TraceEventType.Verbose, 0,
                new XPathDocument(new StringReader(builder.ToString())).CreateNavigator());
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
                this.PrintHeader(m_workingMemoryUpdateTrace, xWriter);
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

            }
            source.TraceData(TraceEventType.Verbose, 0,
                new XPathDocument(new StringReader(builder.ToString())).CreateNavigator());
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
                this.PrintHeader(m_ruleFiredTrace, xWriter);

                xWriter.WriteElementString(m_ruleNameTrace, ruleName);
                if (conflictResolutionCriteria == null)
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, "null");
                }
                else
                {
                    xWriter.WriteElementString(m_conflictResolutionCriteriaTrace, conflictResolutionCriteria.ToString());
                }

            }
            source.TraceData(TraceEventType.Verbose, 0,
                new XPathDocument(new StringReader(builder.ToString())).CreateNavigator());
        }

        public void TrackRuleSetEngineAssociation(RuleSetInfo ruleSetInfo, Guid ruleEngineGuid)
        {
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

                xWriter.WriteElementString(m_traceHeaderTrace, this.m_ruleSetName);
            }
            source.TraceData(TraceEventType.Verbose, 0,
                new XPathDocument(new StringReader(builder.ToString())).CreateNavigator());
        }
    }
}