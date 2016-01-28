<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintStorageQualityCheck.aspx.cs"
    Inherits="Pages_PrinttingModel_StorageManager_PrintStorageQualityCheck" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>质检申请单</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            EnableParameterPrompt="false" PrintMode="ActiveX" DisplayGroupTree="False" HasCrystalLogo="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
        <br />
    </div>
    </form>
</body>
</html>
