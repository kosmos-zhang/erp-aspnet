<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusTableInit.aspx.cs" Inherits="Pages_Office_DefManager_BusTableInit" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>业务表初始化</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../../css/default.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/office/DefManager/BusTableInit.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="hidID" type="hidden" value="0" runat="server" />
    <input id="hidAliasTableName" type="hidden" runat="server" />
    <input id="isreturn" value="0" type="hidden" runat="server" />
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc1:Message ID="Message1" runat="server" />
    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <%= this.AliasTableName %>初始化
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr bgcolor="#FFFFFF">
                        <td>
                            <img id="btnSave" runat="server" src="../../../images/Button/Bottom_btn_save.jpg"
                                onclick="SaveData();" style="cursor: pointer" title="保存" />
                            <img onclick="BackToPage();" id="btnReturn" src="../../../images/Button/Bottom_btn_back.jpg"
                                border="0" style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblData"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 20%">
                                字段标题
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle" style="width: 20%">
                                控件类型
                            </td>
                            <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                                初始化值
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <div id="divText" style="display: none">
        <iframe id="iframeText" style="filter: Alpha(opacity=0); border: solid 10px #93BCDD;
            background: #fff; padding: 10px; width: 300px; height: 440px; z-index: 999; position: absolute;
            display: block; top: 30%; left: 80%; margin: 5px 0 0 -400px;"></iframe>
        <div style="border: solid 10px #93BCDD; background: #fff; padding: 10px; width: 300px;
            z-index: 1000; position: absolute; display: block; top: 30%; left: 80%; margin: 5px 0 0 -400px;">
            <table width="100%">
                <tr>
                    <td id="tdTextTitle" colspan="2" align="center">
                    </td>
                </tr>
                <tr>
                    <td>
                        初始化值
                    </td>
                    <td>
                        <input id="txtEditText" type="text" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <img onclick="ConfirmText('divText')" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                            style="text-align: left; cursor: pointer" />
                        <img onclick="cancelDIV('divText')" src="../../../Images/Button/Bottom_btn_cancel.jpg"
                            style="text-align: left; cursor: pointer" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divDropDownList" style="display: none">
        <iframe id="iframeDDL" style="filter: Alpha(opacity=0); border: solid 10px #93BCDD;
            background: #fff; padding: 10px; width: 600px; height: 840px; z-index: 999; position: absolute;
            display: block; top: 30%; left: 80%; margin: 5px 0 0 -400px;"></iframe>
        <div style="border: solid 10px #93BCDD; background: #fff; padding: 10px; width: 600px;
            z-index: 1000; position: absolute; display: block; top: 30%; left: 50%; margin: 5px 0 0 -400px;">
            <table width="100%">
                <tr>
                    <td id="tdDDLText" colspan="2" align="center">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id="radZDY" type="radio" value="0" onclick="RadDDLClick(true);" />自定义
                        <input id="radData" type="radio" value="1" onclick="RadDDLClick(false);" />绑定数据源
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%" id="tblDDLZDY" style="display: none">
                            <tr>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                    值
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                    标题
                                </td>
                                <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" id="tblDDLBind" style="display: none">
                            <tr>
                                <td colspan="3">
                                    字典表
                                    <select id="selDDLBinddic" onchange="changeDDL();" runat="server">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%; height: 150px">
                                    <select size="4" id="selDDLBindField" style="height: 150px; width: 155px;">
                                    </select>
                                </td>
                                <td style="width: 20%; height: 150px">
                                    <input id="btnValue" type="button" title="值" value="值" onclick="GetField(true)" /><input
                                        id="hidValueField" type="hidden" value="" />
                                    <br />
                                    <br />
                                    <br />
                                    <input id="btnTitle" type="button" title="标题" value="标题" onclick="GetField(false)" /><input
                                        id="hidTitleField" type="hidden" value="" />
                                </td>
                                <td style="width: 40%; height: 150px">
                                    <table width="100%" id="tblDDLBindValue">
                                        <tr>
                                            <td bgcolor="#E6E6E6" class="ListTitle" style="width: 50%">
                                                值
                                            </td>
                                            <td bgcolor="#E6E6E6" class="ListTitle" style="width: 50%">
                                                标题
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <img onclick="ConfirmDDL('divDropDownList')" src="../../../Images/Button/Bottom_btn_confirm.jpg"
                            style="text-align: left; cursor: pointer" />
                        <img onclick="cancelDIV('divDropDownList')" src="../../../Images/Button/Bottom_btn_cancel.jpg"
                            style="text-align: left; cursor: pointer" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <input id="hidSearchCondition" type="hidden" runat="server" />
    <input id="hidEditRow" type="hidden" />
    <input id="hidCompany" type="hidden" runat="server" />
    </form>
</body>
</html>
