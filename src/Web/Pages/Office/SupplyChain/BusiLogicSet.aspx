<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusiLogicSet.aspx.cs" Inherits="Pages_Office_SupplyChain_BusiLogicSet" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务规则设置</title>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
      <script language="javascript"  type="text/javascript">
 function Check()
{
 var j=0;
  var objs = document.getElementsByTagName('input');
  for (var i = 0; i < objs.length; i++) {
    if (objs[i].getAttribute("type") == "checkbox" && objs[i].checked)
    {
      j++;
      if(j>1)
      {
         popMsgObj.ShowMsg('只能选择一行进行修改！');
         return false;;
      }
    }
  }
  if(j==0)
  {
     popMsgObj.ShowMsg('请选择行！');
  }
}
      </script>
    <style type="text/css">

BODY {  font-family: "tahoma";
        color:#333333;
	    font-size: 12px;
        line-height:120%;
	    text-decoration:none;
        margin-top:0px;
        margin-left:0px;
        margin-right:0px;
		margin-bottom:0px;
		background-color:#666666;
}
#mainindex{
        margin-top:10px;
        margin-left:10px;
		background-color:#F0f0f0;
      	font-family:"tahoma";
      	color:#333333;
      	font-size:12px; 
}
.maintable{ filter:progid:dximagetransform.microsoft.dropshadow(color=#000000,offx=2,offy=3,positive=true)}
  .Title{
        font-family: "tahoma";
        color:#000000;
		font-weight: bolder;
	    font-size: 16px;
        line-height:120%;
	    text-decoration:none;
 }
   .orderClick{ cursor:pointer;}
 
 .orderTip{ margin-left:3px;}
 .PageList{ 
        font-family: "tahoma";
        color:#1E5CBA;
	    font-size: 12px;
        line-height:120%;
	    text-decoration:none;
}
.jPagerBar{FONT-SIZE: 12px;FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;}

    </style>
    </head>
<body>
    <form id="form1" runat="server">

    <uc2:Message ID="Message1" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">&nbsp;
                
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                公共规则设置</td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
          <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">                          
                            <asp:ImageButton ID="Save_SubjAssistant" visible="false" runat="server" 
                                ImageUrl="../../../Images/Button/Bottom_btn_save.jpg" onclick="Save_SubjAssistant_Click" />
                          
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" ><table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#C0C0C0">
              <tr>
                <td bgcolor="#FFFFFF">
                    <asp:DataList ID="BusiLogicSetList" runat="server" 
                        UseAccessibleHeader="True" Width="100%" DataKeyField="ID" 
                        onitemdatabound="BusiLogicSetList_ItemDataBound" >
                  <HeaderTemplate>
                    <table   width="100%" border="0" cellpadding="0" cellspacing="1" >
                      <tr>
                        <td align="center" width="14%" align="center"  background="../../../images/Main/Table_bg.jpg"> 选择 </td>
                        <td align="center" width="29%" background="../../../images/Main/Table_bg.jpg">规则名称 </td>
                        <td align="center" width="10%" background="../../../images/Main/Table_bg.jpg"> 规则设置 </td>
                        <td align="center" width="50%" background="../../../images/Main/Table_bg.jpg"> 规则描述 </td>
                      </tr>
                    </table>
                  </HeaderTemplate>
                  <ItemTemplate  >
                    <table  width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#C0C0C0"  id="dt_content" >
                      <tr>
                        <td width="14%" align="center" bgcolor="#FFFFFF"><asp:CheckBox ID="CheckStatus" runat="server" /></td>
                        <td width="29%" align="center" bgcolor="#FFFFFF"> <asp:Label ID="LabName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LogicName")%>'    ></asp:Label></td>
                         <td width="10%" align="center" bgcolor="#FFFFFF">  
                             <asp:DropDownList ID="DrpLogicSet" runat="server" Width="100px">
                             <asp:ListItem Text="是" Value="1"></asp:ListItem>
                             <asp:ListItem Text="否" Value="0"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:Label ID="Lab_Status" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LogicSet")%>' Visible="false"></asp:Label>
                              </td>  
                        <td width="50%" align="center" bgcolor="#FFFFFF"> <asp:Label ID="LabDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>'    ></asp:Label></td>
                      </tr>
                    </table>
                    <table>
                    </table>
                  </ItemTemplate>
                </asp:DataList></td>
              </tr>
            </table>
              <br />
                <br />
          </td>
        </tr>
    </table>
    


    <asp:Label ID="LabAction" runat="server" Text="Add" Visible="false"></asp:Label>

    </form>
</body>
</html>

