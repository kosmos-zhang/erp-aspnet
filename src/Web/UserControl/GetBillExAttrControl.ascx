<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetBillExAttrControl.ascx.cs" Inherits="UserControl_GetBillExAttrControl" %>
<div id="divBillAttr" runat="server">
<asp:DropDownList ID="SelExtValue" runat="server"  style="margin-top:2px;margin-left:2px;" Width="50%"
AppendDataBoundItems="True"> 
<asp:ListItem Value="">--请选择--</asp:ListItem>
</asp:DropDownList>
<asp:TextBox ID="TxtExtValue" Enabled="false" SpecialWorkCheck="其他条件" 
runat="server" Width="40%"></asp:TextBox>
</div>
<!--
调用方法：
1、<td align="right" bgcolor="#E6E6E6">
   <span id="OtherConditon" style="display:none">其他条件</span>
   </td>加ID="OtherConditon"的<span>
2、页面加载时设置 GetBillExAttrControl1.TableName = "表名"}
3、js加载时调用$(document).ready(function()
                {
                    IsDiplayOther('用户控件ID_SelExtValue','用户控件ID_TxtExtValue');
                });
4、处理返回时有的是在js处理，有的是在服务端处理
   用js处理的 直接获取返回来的值，赋值给控件ID就行了，服务端处理的在调用查询前
   需要在处理方法中加           
    string EFIndex = Request.QueryString["EFIndex"];
    string EFDesc = Request.QueryString["EFDesc"];
    GetBillExAttrControl1.ExtIndex = EFIndex;
    GetBillExAttrControl1.ExtValue = EFDesc;
    GetBillExAttrControl1.SetExtControlValue();（服务端处理的可以参考出库管理/销售出库列表）
 5、若需在服务端获取下拉列表和文本框的值
    这样调用：GetBillExAttrControl1.GetExtIndexValue；（下拉列表值）
              GetBillExAttrControl1.GetExtTxtValue；（文本框值）

            
-->
<script type="text/javascript">
function IsDiplayOther(obj,objTxt)
{
    var SelLength=document.getElementById(obj).options.length;
    if(SelLength==1)
    {
        $("#OtherConditon").hide();
        try
        {
            IsDisplayTr("0");
        }
        catch(Error){}
    }
    else
    { 
        $("#OtherConditon").show();
        try
        {
            IsDisplayTr("1");
        }
        catch(Error){}
    }
    //对返回时控件处理

    if($("#"+obj).val()=="")
    {
        $("#"+objTxt).attr("disabled",true);
    }
    else
    {
        $("#"+objTxt).attr("disabled",false);
    }
}

</script>