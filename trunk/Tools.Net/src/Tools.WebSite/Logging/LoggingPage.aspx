<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoggingPage.aspx.cs" Inherits="Logging_LoggingPage" %>

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
    </style>
    <script runat="server">
        protected void submitInlineCodeButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.TraceSource source =
                new System.Diagnostics.TraceSource(sourceTextBox.Text);

            source.TraceData(System.Diagnostics.TraceEventType.Error, 100, "Test from the inline code");
        }
    </script>
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
    <asp:Button ID="submitCodeBehindButton" runat="server" Text="Submit Via Code Behind" 
        onclick="submitCodeBehindButton_Click" />
     <asp:Button ID="submitInlineCodeButton" runat="server" Text="Submit Via Inline Code" 
        onclick="submitInlineCodeButton_Click" />       
    </form>
</body>
</html>
