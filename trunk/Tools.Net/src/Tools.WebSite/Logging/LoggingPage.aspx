<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        traceEventTypeDropDown.DataSource = Enum.GetValues(typeof(System.Diagnostics.TraceEventType));
        traceEventTypeDropDown.DataBind();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        System.Diagnostics.TraceSource source = 
            new System.Diagnostics.TraceSource(sourceTextBox.Text);
        
        source.TraceData(System.Diagnostics.TraceEventType.Error, 100, "kdfjndjfkg dfghn");
        
        source.TraceData((System.Diagnostics.TraceEventType)Enum.Parse(typeof(System.Diagnostics.TraceEventType),
    traceEventTypeDropDown.SelectedValue), Convert.ToInt32(eventIdTextBox.Text), messageTextBox.Text);
      

        int i = 0;
        
  
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="firstColumn">
                    <asp:Label ID="sourceLabel" runat="server" Text="Source"></asp:Label>
                </td>
                <td class="secondColumn">
                    <asp:TextBox ID="sourceTextBox" Text="Test" runat="server"/>
                </td>
            </tr>
            <tr>
                <td class="firstColumn">
            <asp:Label ID="Label1" runat="server" Text="TraceEventType"></asp:Label>
            </td>
                <td class="secondColumn">
            &nbsp;<asp:DropDownList ID="traceEventTypeDropDown" runat="server">
            </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td class="firstColumn">
            <asp:Label ID="eventIdLabel" runat="server" Text="Event Id"></asp:Label>
                            </td>
                <td class="secondColumn">
                <asp:TextBox ID="eventIdTextBox" runat="server"></asp:TextBox>
                        </td>
            </tr>
            <tr>
                <td class="firstColumn">
            <asp:Label ID="messageLabel" runat="server" Text="Message"></asp:Label>
            </td>
                <td class="secondColumn"><asp:TextBox ID="messageTextBox" runat="server" TextMode="MultiLine" Height="80px"
                Width="368px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="submitButton" runat="server" Text="Submit" 
        onclick="submitButton_Click" />
    </form>
</body>
</html>
