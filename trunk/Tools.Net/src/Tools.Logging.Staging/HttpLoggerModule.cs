using System;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Tools.Logging
{
    /// <summary>
    /// The whole module, its placement, methods etc, is very, very ad-hoc.
    /// </summary>
    public class HttpLoggerModule : IHttpModule
    {
        //private MemoryStream responseStream = null;
        private HttpLoggerFilter lf;

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            // no need to register to those events if there is no need to log
            if (Log.Source.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                context.BeginRequest +=
                    OnBeginRequest;

                context.EndRequest +=
                    OnEndRequest;
            }
        }

        public void Dispose()
        {
            // TODO:  Add LoggerModule.Dispose implementation
        }

        #endregion

        public void OnBeginRequest(object o, EventArgs ea)
        {
            var httpApp = (HttpApplication) o;
            HttpContext ctx = HttpContext.Current;
            //ctx.Response.Filter = responseStream;
            var b = new byte[ctx.Request.InputStream.Length];

            ctx.Request.InputStream.Read(b, 0, b.Length);
            ctx.Request.InputStream.Position = 0;
            var serverVarsDetails = new StringBuilder();
            foreach (string key in ctx.Request.ServerVariables.AllKeys)
            {
                if (ctx.Request.ServerVariables[key] != null && ctx.Request.ServerVariables[key] != String.Empty)
                    serverVarsDetails.Append(key + ":" + ctx.Request.ServerVariables[key] + "\r\n");
            }
            var headersDetail = new StringBuilder();
            foreach (string key in ctx.Request.Headers.AllKeys)
            {
                if (ctx.Request.Headers[key] != null && ctx.Request.Headers[key] != String.Empty)
                    headersDetail.Append(key + ":" + ctx.Request.Headers[key] + "\r\n");
            }
            Log.Source.TraceEvent(TraceEventType.Verbose, 0,
                                  "Headers:" + Environment.NewLine + headersDetail + Environment.NewLine +
                                  "ServerVariables:" + Environment.NewLine + serverVarsDetails + Environment.NewLine +
                                  "Request: " + Environment.NewLine + Encoding.UTF8.GetString(b));

            lf = new HttpLoggerFilter(ctx.Response.Filter);
            lf.LogEncoding = ctx.Response.ContentEncoding;
            ctx.Response.Filter = lf;
        }

        public void OnEndRequest(object o, EventArgs ea)
        {
            var httpApp = (HttpApplication) o;
            HttpContext ctx = HttpContext.Current;

            #region proof of concept - failed

            //ctx.Response.Filter = responseStream;
            //byte[] b = new byte[ctx.Response.OutputStream.Length];
            //int iv = 0;
            //do
            //{
            //    iv = ctx.Response.OutputStream.ReadByte();
            //} 
            //while (iv != -1);

            //ctx.Response.OutputStream.Read(b, 0, b.Length);
            //ctx.Response.OutputStream.Position = 0;
            //ctx.Response.

            #endregion proof of concept - failed

            var logMessage = new StringBuilder();

            foreach (string cookieName in ctx.Response.Cookies.Keys)
            {
                logMessage.Append("Cookie ");
                logMessage.Append(cookieName);
                logMessage.Append(": ");
                logMessage.Append(ctx.Response.Cookies[cookieName].ToString());
                logMessage.Append(Environment.NewLine);
            }
            addLogLine(logMessage, "Expires:", ctx.Response.Expires.ToString());
            addLogLine(logMessage, "ExpiresAbsolute:", ctx.Response.ExpiresAbsolute.ToUniversalTime().ToString());
            addLogLine(logMessage, "IsClientConnected:", ctx.Response.IsClientConnected.ToString());
            addLogLine(logMessage, "CacheControl:", ctx.Response.CacheControl);
            addLogLine(logMessage, "Charset:", ctx.Response.Charset);
            addLogLine(logMessage, "IsRequestBeingRedirected:", ctx.Response.IsRequestBeingRedirected.ToString());

            Log.Source.TraceEvent(TraceEventType.Verbose, 0,
                                  "Response body: " + Environment.NewLine + lf.RawContent +
                                  Environment.NewLine +
                                  "Response headers:" + logMessage);
        }

        private void addLogLine(StringBuilder sb, string subject, string val)
        {
            sb.Append(subject);
            sb.Append(val);
            sb.Append(Environment.NewLine);
        }
    }
}