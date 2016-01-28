<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBillType.aspx.cs" Inherits="Pages_Office_SystemManager_test" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"></base>
    <title>无标题页</title>

    <script src="../../../js/office/SystemManager/ApprovalFlowSet.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

   <table width="300" border="1" bgcolor="#FFFFFF">
  <tr>
    <td >请选择单据类型<uc1:Message ID="Message1" runat="server" />
      </td>
    <td align="right"><input type="button" value="关闭" />;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top">
        <asp:TreeView ID="Tree_BillTpye" runat="server"  >
        </asp:TreeView>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <input type="hidden" id="hiddenTreeNum" name="hiddenTreeNum" value="12" runat="server" />
      </td>
  </tr>
  <tr>
    <td colspan="2" valign="middle">
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
<script language="javascript">
function CheckSelect()
{
    var hiddenTreeNum = document.getElementById('hiddenTreeNum').value;
    var tempPrefix = 'Tree_BillTpyen';
    var tempFix = 'CheckBox';
    var isFlag = false;
    for(var i=0;i<hiddenTreeNum;i++)
    {
        var tempObj = tempPrefix + (i+1) + tempFix;
        if(document.getElementById(tempObj).checked)
        {
            isFlag = true;
            return isFlag;
        }
    }
    if(!isFlag)
    {
        popMsgObj.ShowMsg('请选择单据！');
    }
    return false;
}
</script>
