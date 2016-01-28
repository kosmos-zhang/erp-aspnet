<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectBillType.ascx.cs" Inherits="UserControl_SelectBillType" %>

<script language="javascript">
 function getBillSelectOption(objValues)
 {
    document.getElementById('txt_BillTypeID').value = objValues;
 }
  function getBillSelectText(objValues)
 {
    document.getElementById('txt_BillTypeName').value = objValues;
 }
 </script>
<div id="divBill" style="display:none">
   <table width="300" border="1" bgcolor="#FFFFFF">
  <tr>
    <td >请选择单据类型
      </td>
    <td align="right"><input type="button" value="关闭" />;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top">
        <asp:TreeView ID="Tree_BillTpye" runat="server" NavigateUrl="javascript:void(0)"  >
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
<script language="javascript">
var popBillObj=new Object();

popBillObj.show = function()
{
    document.getElementById('divBill').style.display='block';
    document.getElementById('divBill').style.position = 'absolute';
}
popBillObj.CheckSelect = function()
{
    var hiddenTreeNum = document.getElementById('<%=hiddenTreeNum.ClientID %>').value;
    var tempPrefix = 'SelectBillType1_Tree_BillTpyen';
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