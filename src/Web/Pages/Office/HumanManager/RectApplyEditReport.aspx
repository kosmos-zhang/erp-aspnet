<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RectApplyEditReport.aspx.cs" Inherits="Pages_Office_HumanManager_RectApplyEditReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>招聘申请</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <style type="text/css">
        #tblMain
        {
            margin-top:0px;
            margin-left:0px;
		    background-color:#F0f0f0;
      	    font-family:"tahoma";
      	    color:#333333;
      	    font-size:12px; 
        }
        .errorMsg
        {
	        filter:progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
	        position:absolute;
	        top:240px;
	        left:450px;
	        border-width:1pt;
	        border-color:#666666;
	        border-style:solid;
	        width:290px;
	        display:none;
	        margin-top:10px;
	        z-index:21;
        }
    </style>
</head>
<body>
<form id="frmMain" runat="server">
<span id="spanMsg" class="errorMsg"></span>        
<a name="DetailListMark"  ></a>
<span id="Forms" class="Spantype" name="Forms"></span>
    <table width="100%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="tblMain">
   
        <tr>
            <td colspan="2">

                <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr>
                        <td colspan="2"  >
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                AutoDataBind="true" PrintMode="ActiveX" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            </CR:CrystalReportSource>
                            
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    
    
    
</form>
</body>
</html>

