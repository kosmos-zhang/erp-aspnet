<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeskTop.aspx.cs" Inherits="DeskTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .Title
        {
            font-family: "tahoma";
            color: #333333;
            font-weight: bolder;
            font-size: 18px;
            line-height: 120%;
            text-decoration: none;
        }
        BODY
        {
            font-family: "tahoma";
            color: #333333;
            font-size: 14px;
            line-height: 120%;
            text-decoration: none;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-color: #666666;
        }
        #selUserBox
        {
            background: #ffffff;
        }
        #userList
        {
            border: solid 1px #3366cc;
            width: 200px;
            height: 300px;
            overflow: auto;
            padding-left: 10px;
        }
        #typeListTab
        {
            background: #2255bb;
            padding: 5px;
            margin: 0px;
            width: 202px;
            background: #3366cc;
        }
        /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */.tab
        {
            cursor: pointer;
            display: inline;
            color: White;
            background-color: inherit;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        .selTab
        {
            cursor: pointer;
            display: inline;
            color: Black;
            background-color: White;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
            z-index: 1;
        }
        .style1
        {
            width: 266px;
        }
        .style2
        {
            width: 77px;
            height: 20px;
        }
        .style3
        {
            width: 212px;
            height: 20px;
        }
        .style4
        {
            width: 218px;
            height: 20px;
        }
        .style5
        {
            height: 20px;
        }
        .style6
        {
            width: 266px;
            height: 20px;
        }
        .DailyAttendanceSpanCss
        {
            position: absolute;
            top: 180px;
            left: 190px;
            border-width: 1pt;
            border-color: #EEEEEE;
            border-style: solid;
            width: 450px;
            display: none;
            z-index: 1;
        }
        #ShowPageCount1
        {
            width: 29px;
        }
        #ToPage1
        {
            width: 29px;
        }
        .noticetable
        {
            position: absolute;
            top: 180px;
            left: 190px;
            width: 450px;
            background-color: #dddddd;
            z-index: 100;
        }
    </style>

    <script type="text/javascript">
        function ChangeTitleClass(index){
           if(index == 1)
                            SearchTaskList();
           else if(index == 2)
                            SearchDeskFlow();
           else if( index == 3)
                            SearchUnreadMessage();
           for( var i = 1;i<= 3;i++){
              if( i != index  ){
                 document.getElementById("divli"+i).style.backgroundColor = "#B6B6B6";
              }
              else{
                    document.getElementById("divli"+i).style.backgroundColor = '#ffffff';
               }
           }
        }
    </script>

    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <link href="css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="js/SystemAlert.js" type="text/javascript"></script>

    <script src="js/DeskTop.js" type="text/javascript"></script>

    <script src="js/common/Common.js" type="text/javascript"></script>

    <%--<object   id="CrystalPrintControl"   classid="CLSID:BAEE131D-290A-4541-A50A-8936F159563A"   codebase="http://192.168.1.55:511/setup/PrintControl.cab"   viewastext ></object>
--%></head>
<body onload=" InitPage();">
    <form id="form1" runat="server">
    <span id="Forms" class="Spantype"></span>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top" class="Blue">
                            <img src="../Images/Main/Arrow.jpg" width="21" height="18" alt="" />我的待办任务
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td bgcolor="#FFFFFF">
                                                    <table id="tbList1" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top" class="Blue">
                            <img src="../Images/Main/Arrow.jpg" width="21" height="18" alt="" />待我审批的流程
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td bgcolor="#FFFFFF">
                                                    <table id="tbList2" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                        <td rowspan="2" align="right" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top" class="Blue">
                            <img src="../Images/Main/Arrow.jpg" width="21" height="18" alt="" />我的未读短信
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td colspan="8" bgcolor="#FFFFFF">
                                                    <table id="tbList3" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                        <td rowspan="2" align="right" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top" class="Blue">
                            <img src="../Images/Main/Arrow.jpg" width="21" height="18" alt="" />我的备忘录
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td colspan="2" bgcolor="#FFFFFF">
                                                    <table id="tbList4" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                        <td rowspan="2" align="right" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top" class="Blue">
                            <img src="../Images/Main/Arrow.jpg" width="21" height="18" alt="" />我的日程安排
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td colspan="2" bgcolor="#FFFFFF">
                                                    <table id="tbList5" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="255" valign="top">
                <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
                    id="mainindex">
                    <tr>
                        <td valign="top">
                            <img src="../Images/Main/Line.jpg" width="122" height="7" />
                        </td>
                    </tr>
                    <tr runat="server" id="signIO">
                        <td height="15" valign="top" class="Blue">
                            <img src="../Images/Button/Main_qd.jpg" onclick="showdailyattendance();" id="btnsignin"
                                style="cursor: pointer;" />
                        </td>
                    </tr>
                    <tr>
                        <td height="15" valign="top" class="Blue">
                            <img src="../Images/Button/Main_line.jpg" width="248" height="12" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30">
                            <span class="Blue">
                                <img src="../Images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />最新公告</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="98%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                                bgcolor="#CCCCCC">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="98%" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
                                            <tr>
                                                <td bgcolor="#FFFFFF">
                                                    <table id="tbdestnoticeList" width="95%">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="inputTime" runat="server" />
                            <table width="100%">
                                <tr>
                                    <td valign="top" align="center">
                                        <br />
                                        <label id="lblshowyear" runat="server">
                                        </label>
                                        <label id="lblmonth" runat="server">
                                        </label>
                                        <br />
                                        <br />
                                        <br />
                                        <span style="font-size: 72px; color: Blue; font-weight: bold;" id="spanday" runat="server">
                                        </span>
                                        <br />
                                        <br />
                                        <br />
                                        <label id="lblweek" runat="server">
                                        </label>
                                        <label id="lblnongli" runat="server">
                                        </label>
                                        <div id="divTime" style="text-align: center; border: 1px; padding: 3px; border-style: double;
                                            width: 60px;">
                                        </div>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top">
            </td>
        </tr>
    </table>
    <span id="DailyAttendanceSpan" class="DailyAttendanceSpanCss">
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
                <td height="20" bgcolor="#F4F0ED" class="Blue">
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td background="images/Main/Table_bg.jpg" bgcolor="#3F6C96">
                                考勤签到签退操作
                            </td>
                            <td align="right" background="images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <img src="Images/Pic/Close.gif" title="关闭" style="cursor: pointer" onclick="CloseDailyAttendanceSpan()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
            id="Table1">
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    当前日期
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <input id="Today" type="text" disabled class="tdinput" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    当前时间
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <input id="liveclock" class="tdinput" disabled type="text" />
                </td>
            </tr>
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    工号
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <input id="EmployNo" class="tdinput" disabled type="text" />
                </td>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    姓名
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <input id="EmployName" class="tdinput" disabled type="text" />
                </td>
            </tr>
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    考勤班段
                </td>
                <td height="20" colspan="3" bgcolor="#FFFFFF">
                    <asp:DropDownList ID="ddlworkshift" runat="server" class="tdinput">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="20" align="right" bgcolor="#E6E6E6">
                    备注
                </td>
                <td height="20" colspan="3" bgcolor="#FFFFFF">
                    <textarea name="Remark" id="Remark" class="tdinput" cols="40" rows="5"></textarea>
                </td>
            </tr>
            <tr>
                <td height="20" colspan="4" align="center" bgcolor="#FFFFFF">
                    <img src="Images/Button/btn_qd.jpg" title="签到" id="btn_signin" style="cursor: pointer;"
                        onclick='InsertEmploySignInData()' border="0" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img src="images/Button/main_qt.jpg" title="签退" id="btn_signout" style="cursor: pointer;"
                        onclick='UpdateEmploySignOutData()' border="0" />
                </td>
            </tr>
        </table>
    </span>
    <input id="HiddenEmployeeAttendanceSetID" type="hidden" runat="server" />
    </form>
    <div id="editPanel" style="display: none;">
        <table id="itemContainer" border="1" width="100%" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 40px;">
                    主题
                </td>
                <td>
                    <span id="ttTitle"></span>
                </td>
            </tr>
            <tr>
                <td>
                    接收时间
                </td>
                <td>
                    <span id="ttSendDate"></span>
                </td>
            </tr>
            <tr>
                <td>
                    发件人
                </td>
                <td>
                    <span id="ttSendUser"></span>
                </td>
            </tr>
            <tr>
                <td>
                    短信内容：
                </td>
                <td align="left" valign="top">
                    <span id="ttContent"></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding: 3px;">
                    <a href="#" onclick="hideMsg()">确定</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="desktopnotice" style="display: none;" class="noticetable">
        <table id="noticeTable" border="1" width="100%" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 80px; background-color: #eeeeee; float: right;">
                    公告主题
                </td>
                <td>
                    <input type="text" id="spNewsTitle" style="width: 94%;" />
                    <input type="hidden" id="itemID" />
                </td>
                <td>
                    <a href="#" onclick="javascript:document.getElementById('desktopnotice').style.display='none';">
                        关闭</a>
                </td>
            </tr>
            <tr>
                <td valign="top" style="background-color: #eeeeee">
                    公告内容
                </td>
                <td>
                    <textarea id="spNewsContent" rows="10" cols="40" colspan="2"> </textarea>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
