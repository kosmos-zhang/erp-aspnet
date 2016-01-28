<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FieldExpression.aspx.cs" Inherits="Pages_Office_DefManager_FieldExpression" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../../js/office/FinanceManager/BillingAdd.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <style type="text/css">
     input{ border:1px #ccc solid;}
    </style>
    <script type="text/javascript">
        //保存
        function SaveTable() {
            var Expresstion = "", Action1 = "addreguler";
//            if ($("#ExpressionText").val() == "") {
//                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "表达式不能为空！");
//                return;
//            }
            Expresstion = $("#ExpressionText").val() + "=#" + $("#ExpressionValue").val() + "#";

            if ($("#hidAction").val() == "1") {
                Action1 = "modreguler";
            }
            
            var URLParams = "action=" + Action1 + "&totaltype="+$("#ddlTotalType").val()+"&columnid=" + $("#hidColumnID").val() + "&tableid=" + $("#hidID").val() + "&relation=" + escape(Expresstion);
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/DefManager/DefForm.ashx",
                dataType: 'string', //返回json格式数据
                cache: false,
                data: URLParams,
                beforeSend: function() {
                    AddPop();
                },
                error: function() {
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
                },
                success: function(data) {
                    if (data == "yes") {
                        $("#hidAction").val("1");
                        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "保存成功");
                    }
                }
            });
        }
    </script>
    <script type="text/javascript">
    function goback()
    {
       window.location.href='CreateBusTable.aspx?ID='+$('#hidID').val()+$("#Hidpagestate").val()+'&action=tablelist&orderBy=&AliasTableName='; 
    }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <input name='DetailCount' type='hidden' id='DetailCount' value="1" />
    <asp:HiddenField ID="hidColumnID" runat="server" />
    <asp:HiddenField ID="hidAction" runat="server" Value="0" />
    <asp:HiddenField ID="Hidpagestate" runat="server" /><!--记录页码和当前页号-->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">创建表达式</div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img id="btnBilling_Save" src="../../../images/Button/Bottom_btn_save.jpg" onclick="SaveTable()"  runat="server" style="cursor: pointer"/>&nbsp;
                                        <img id="Img1" src="../../../images/Button/Bottom_btn_back.jpg" onclick="goback()"/>&nbsp;
                                    </td>
                                    <td align="right"> &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        行合计
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick')" />
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" id="Tb_02" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                     style="display: block">
                    <tr>
                        <td class="tdColTitle" width="10%">字段：</td>
                        <td class="tdColInput" style=" width:150px;">                  
                            <select id="FieldList"  style="height:100px;" multiple="multiple">
                                <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <option value="#<%#Eval("ccode")%>#"><%#Eval("cname")%>(#<%#Eval("ccode")%>#)</option>
                                </ItemTemplate>
                                </asp:Repeater>
                            </select>
                            
                        </td>
                        <script type="text/javascript">
                            //
                            function Handle(val) {
                                if ($("#Constant").val()!="") {
                                    if ($("#FieldList").val() == "")
                                    {
                                        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择后在进行操作！");
                                        return;
                                    } else {
                                        $("#ExpressionText").val($("#ExpressionText").val() + val + "#"+$("#Constant").val()+"#");
                                        return;
                                    }
                                }
                                $("#ExpressionText").val($("#ExpressionText").val() + val + $("#FieldList").val());
                                $("#Constant").val('');
                            }
                        </script>
                        <td class="tdColTitle" rowspan="2" style="width:20px;" align="center">
                            <input type="button" value="加" onclick="Handle('加')" /><br /><br />
                            <input type="button" value="减" onclick="Handle('减')"/><br /><br />
                            <input type="button" value="乘" onclick="Handle('乘')" /><br /><br />
                            <input type="button" value="除" onclick="Handle('除')" /><br /><br />
                            <input type="button" value="清空" onclick="$('#ExpressionText').val('')" />
                        </td>
                        <td class="tdColInput" rowspan="2" valign="top">
                             <asp:TextBox ID="ExpressionText" runat="server" CssClass="tdinput" ReadOnly="true" Width="70%" style="border:1px #ccc solid; height:30px; line-height:30px; font-size:14px;"></asp:TextBox> 
                            <b style=" font-size:14px;"> ＝</b><asp:TextBox ID="ExpressionValue" ReadOnly="true" runat="server" CssClass="tdinput" Width="25%" style="border:1px #ccc solid; height:30px; line-height:30px; font-size:14px;"></asp:TextBox>
                            <asp:HiddenField ID="hidID" Value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="10%">常量：</td>
                        <td class="tdColInput"><asp:TextBox ID="Constant" runat="server" CssClass="tdinput" Width="95%" ></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        列合计
                                    </td>
                                    <td align="right">
                                        <div id='searchClick1'><img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick1')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <table width="99%" border="0" align="center" id="Tb_03" cellpadding="2" cellspacing="1" bgcolor="#999999"
                     style="display: block">
                    <tr>
                        <td class="tdColTitle" width="10%">合计类型：</td>
                        <td class="tdColInput" style=" width:150px;">   
                            <asp:DropDownList ID="ddlTotalType" runat="server">
                            <asp:ListItem Value="" ></asp:ListItem>
                            <asp:ListItem Value="sum" >求和</asp:ListItem>
                            <asp:ListItem Value="avg" >求平均值</asp:ListItem>
                            </asp:DropDownList>      
                            
                        </td>
                       
                    </tr>
                </table>
                <br />
                
            </td>
        </tr>
    </table>
    <span id="Forms" class="Spantype"></span>
    <uc7:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
