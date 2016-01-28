<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebSiteOrderInfo.aspx.cs" Inherits="Pages_Office_CustomWebSiteManager_WebSiteOrderInfo" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建配送单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <style type="text/css">
        .tboxsize
        {
            width: 90%;
            height: 99%;
        }
        .textAlign
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <form id="frmMain" runat="server">
    <input type="hidden" id="txtLastRowID" value="0" />
    <input type="hidden" id="txtLastSortNo" value="1" />
    <input type="hidden" id="action" value="ADD" runat="server" />
    <input type="hidden" id="txtCurrentUserName" runat="server" />
    <input type="hidden" id="txtCurrentDate" runat="server" />
    <input type="hidden" id="txtCurrenUserID" runat="server" />
    <input type="hidden" id="txtIsBack" runat="server" />
    <input type="hidden" id="txtCurrentRowID" />
    <input type="hidden" id="txtIsDefault" />
    <input type="hidden" id="txtSendID" runat="server" value="0" />
    <input type="hidden" id="txtFlowStatus" runat="server" />
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:message ID="msgError" runat="server" />
    <div id="divStorage" style="display: none;">
        <asp:DropDownList ID="ddlStorage" runat="server">
        </asp:DropDownList>
    </div>
    <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td height="30" align="center" colspan="2" class="Title">
                <div id="divTitle" runat="server">
                    订单信息
                </div>
            </td>
        </tr>
     
        <tr>
            <td>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                          
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" runat="server" id="btnBack"
                                onclick="window.history.go(-1);" style="cursor: pointer" />
                         
                        </td>
                  
                    </tr>
                </table>
            
                <!-- <div style="height:500px;overflow-y:scroll;"> -->
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                    <tr>
                        <td>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr>
                                                <td>
                                                    基本信息
                                                </td>
                                                <td align="right">
                                                    <div id='divBasicInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblBasicInfo','divBasicInfo')" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblBasicInfo">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        订单编号
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div runat="server" id="divOrderNo">
                                        </div>
                                    </td>
                             
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        客户名称</td>
                                    <td height="20" class="tdColInput" width="24%">
                                  <div runat="server" id="divCustomName"></div></td>
                                  
                                  <td height="20" align="right" class="tdColTitle" width="10%">
                                       
                                        会员用户名</td>
                                    <td height="20" class="tdColInput" width="23%">
                                       <div runat="server" id="divLoginUserName"></div> 
                                    </td>
                                </tr>
                          <tr>
                                    
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                   最迟发货日期
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <div runat="server" id="divConDate"></div>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                    状态
                                     </td>
                                    <td height="20" class="tdColInput" width="24%">
                                     <div runat="server" id="divStatus"></div>
                                        </td>
                                        
                                               <td height="20" align="right" class="tdColTitle" width="10%">
                                      备注
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                     <div runat="server" id="divOrderTitle">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="20" bgcolor="#F4F0ED" class="Blue">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                            <tr >
                                                <td>
                                                   订单明细
                                                </td>
                                                <td align="right">
                                                    <div id='divLendInfo'>
                                                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('divlendList','divLendInfo')"
                                                            alt="展开收起栏目" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div id="divlendList" style="display: block;" runat="server">
                              
                                
                            </div>
                        </td>
                    </tr>
                </table>
                <!-- </div> -->
            </td>
        </tr>
    </table>
    </form>
</body>
</html>



