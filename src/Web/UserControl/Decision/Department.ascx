<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Department.ascx.cs" Inherits="UserControl_Decision_Department" %>
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
        document.getElementById("spankeyword").innerHTML =document.getElementById("KeyWord").options[document.getElementById("KeyWord").selectedIndex].text+"<span style=\"cursor:pointer;color:green;\" onclick=popDeptObj.Show(\"ProductOrDeptId\");>["+name+"]</span>";
        
        document.getElementById(popDeptObj.HiddenObj).value = obj.value;
        
        document.getElementById('Department1_divDeptUser').style.display='none';
    }
    popDeptObj.Show = function(objHidden)
    {
        popDeptObj.HiddenObj = objHidden;
        document.getElementById('Department1_divDeptUser').style.display='block';
    }

    </script>
