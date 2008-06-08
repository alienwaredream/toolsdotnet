<%@ Page Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Logging test page</title>
    <style type="text/css">
        .firstColumn
        {
            width: 100px;
        }
        .secondColumn
        {
            width: 300px;
        }
        table { width:500px}
    </style>
    <script runat="server">
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (!IsPostBack)
            {
                traceEventTypeDropDown.DataSource = Enum.GetValues(typeof(TraceEventType));
                traceEventTypeDropDown.DataBind();
            }
            // restore ActivityId if already present in the hidden field
            if (Request["ActivityIdInput"] != null) 
                this.ActivityIdInput.Value = Request["ActivityIdInput"];
        }
        protected void submitInlineCodeButton_Click(object sender, EventArgs e)
        {
            if (Request["ActivityIdInput"]!=null)
            {
                System.Diagnostics.Trace.CorrelationManager.StartLogicalOperation(
                    new Guid(Request["ActivityIdInput"]));
                System.Diagnostics.Trace.CorrelationManager.ActivityId = new Guid(Request["ActivityIdInput"]);
            }
            
            TraceSource source =
                new TraceSource(sourceTextBox.Text);
            
            TraceEventType eventType = 
                (TraceEventType)Enum.Parse(typeof(TraceEventType), traceEventTypeDropDown.Text);
            
            if (eventType != TraceEventType.Transfer)
            {
                source.TraceData(eventType, Convert.ToInt32(eventIdTextBox.Text), messageTextBox.Text);
            }
            else
            {
                source.TraceTransfer(Convert.ToInt32(eventIdTextBox.Text), messageTextBox.Text, new Guid(transferGuidTextBox.Text));
                //SetActivityIdCookie(System.Diagnostics.Trace.CorrelationManager.ActivityId);
            }

            if (Request["ActivityIdInput"] != null)
            {
                System.Diagnostics.Trace.CorrelationManager.StopLogicalOperation();
            }

        }
        protected void generateActivityGuidButton_Click(object sender, EventArgs e)
        {
            activityGuidTextBox.Text = Guid.NewGuid().ToString();
        }
        protected void startActivityButton_Click(object sender, EventArgs e)
        {
            PersistActivityId(activityGuidTextBox.Text);
        }
        private void PersistActivityId(string activityId)
        {
            ActivityIdInput.Value = activityId;
        }

        protected void traceEventTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (traceEventTypeDropDown.Text == "Transfer")
            {
                transferGuidTextBox.Text = Guid.NewGuid().ToString();
                transferGuidTextBox.Style.Add("display", "block");
                return;
            }
            transferGuidTextBox.Style.Add("display","none");
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="startActivityLabel" runat="server" Text="Activity Guid"></asp:Label>
                </td>
                <td class="secondColumn">
                    <asp:TextBox ID="activityGuidTextBox" Text="{BDFF4AC0-81EA-4c2b-83CA-3537C445EB05}" runat="server" style="width:300px" />
                    <asp:Button ID="generateActivityGuidButton" runat="server" Text="Generate" OnClick="generateActivityGuidButton_Click" />
                </td>
                
            </tr>
                        <tr>
                <td colspan="2" style="width:100%">
                    <asp:Button ID="startActivityButton" style="width:100%" runat="server" Text="Start Activity!" OnClick="startActivityButton_Click" />
                </td>
                </tr>
        </table>
        <table>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="sourceLabel" runat="server" Text="Source"></asp:Label>
                </td>
                <td class="secondColumn">
                    <asp:TextBox ID="sourceTextBox" Text="Test" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="Label1" runat="server" Text="TraceEventType"></asp:Label>
                </td>
                <td class="secondColumn">
                    &nbsp;<asp:DropDownList ID="traceEventTypeDropDown" runat="server" 
                        AutoPostBack="true" 
                        onselectedindexchanged="traceEventTypeDropDown_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="transferGuidTextBox" Text="" runat="server" style="display:none" />
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="eventIdLabel" runat="server" Text="Event Id"></asp:Label>
                </td>
                <td class="secondColumn">
                    <asp:TextBox ID="eventIdTextBox" runat="server" Text="100"></asp:TextBox>
                    <asp:CompareValidator ID="eventIdCompareValidator" runat="server" ControlToValidate="eventIdTextBox"
                        Operator="DataTypeCheck" Type="Integer" ErrorMessage="Only integer values are  allowed for event id"
                        Text="*" />
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="messageLabel" runat="server" Text="Message"></asp:Label>
                </td>
                <td class="secondColumn">
                    <asp:TextBox ID="messageTextBox" runat="server" TextMode="MultiLine" Height="80px"
                        Width="368px"></asp:TextBox>
                </td>
            </tr>
            <tr><td colspan="2"><asp:Button ID="submitInlineCodeButton" style="width:100%" runat="server" Text="Log this!" OnClick="submitInlineCodeButton_Click" />
</td></tr>
        </table>
    </div>
    <input id="ActivityIdInput" type="hidden" runat="server" />
    </form>
</body>
</html>
