using System;

public partial class Logging_LoggingPage : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        traceEventTypeDropDown.DataSource = Enum.GetValues(typeof(System.Diagnostics.TraceEventType));
        traceEventTypeDropDown.DataBind();
    }

    protected void submitCodeBehindButton_Click(object sender, EventArgs e)
    {
        System.Diagnostics.TraceSource source =
            new System.Diagnostics.TraceSource(sourceTextBox.Text);

        source.TraceData(System.Diagnostics.TraceEventType.Error, 100, "Test from the code behind");  
    }
}
