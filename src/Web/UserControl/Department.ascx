<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Department.ascx.cs" Inherits="UserControl_Department" %>
    <div id="divDeptUser" runat="server" style="border: solid 10px #898989; background: #fff;
        padding: 10px; width: 200px; z-index: 1001; position: absolute; display: none;
        top: 50%; left: 70%; margin: -200px 0 0 -400px;">
    </div>
    <script language="javascript">
var popDeptObj=new Object();
popDeptObj.InputObj = null;//输入框对象
popDeptObj.HiddenObj = null;

popDeptObj.FillDeptValue = function(obj,name)
{
    document.getElementById(popDeptObj.InputObj).value = name;
    document.getElementById(popDeptObj.HiddenObj).value = obj.value;
    
    document.getElementById('Department1_divDeptUser').style.display='none';
}
popDeptObj.Show = function(obj,objHidden)
{
    popDeptObj.InputObj = obj ;
    popDeptObj.HiddenObj = objHidden;
    document.getElementById('Department1_divDeptUser').style.display='block';
}

</script>
