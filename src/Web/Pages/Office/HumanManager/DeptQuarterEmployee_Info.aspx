<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptQuarterEmployee_Info.aspx.cs" Inherits="Pages_Office_HumanManager_DeptQuarterEmployee_Info" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门人员选择</title>
    <base target="_self" ></base>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <style type="text/css">
body {
	color: #000000;
	font-family: 宋体;
	font-size: 12px;
	line-height: 150%;
	margin-bottom: 0px;
	margin-left: 0px;
	margin-right: 0px;
	margin-top: 0px;
	scrollbar-3dlight-color: #999999;
	scrollbar-arrow-color: #888888;
	scrollbar-darkshadow-color: #FFFFFF;
	scrollbar-face-color: #EBEBEB;
	scrollbar-highlight-color: #FFFFFF;
	scrollbar-shadow-color: #7A7A7A;
	scrollbar-track-color: #FFFFFF;} /*滚动条样式*/

.hidden{display:none}

table {  font-family: "宋体"; font-size: 12px;}

a:link {
	color: #135294;
	text-decoration: none;
}
a:visited {
	color: #135294;
	text-decoration: none;
}

a:hover {
	color: #FF0000;
	text-decoration: underline;
}

a:active {
	background-color: transparent;
	color: #000000;
	text-decoration: none;} /*加了链接的字符样式*/
</style>

    <script language="javascript" type="text/javascript">
    function getUserOrdept()
    {
        var div_id=document.getElementById("dv_tree");
        var objs=div_id.getElementsByTagName('input');
        var select="";
                //遍历所有控件
                for(var i = 0; i < objs.length; i++)
                {
                    //判断是否是选中的checkbox
                    if(objs[i].getAttribute("type") == "checkbox" && objs[i].checked)
                    {
                        //获取列的值
                        var values = objs[i].value;
                        
                        select += values.toString() + ",";
                    }
                     //判断是否是选中的radiobutton
                    if(objs[i].getAttribute("type") == "radio" && objs[i].checked)
                    {
                        //获取列的值
                       var values = objs[i].value;
                        select += values.toString() + ",";
                    }
                }
                if(select=="")
                {
                    alert("请选择部门或人员！");
                    return;
                }
                select=select.substring(0,select.length - 1);
                window.returnValue=select;
                window.close();
    }
    	
    function ClearInfo() 	
    {
                window.returnValue="ClearInfo";
                window.close();

    }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <br />
  <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#F0F0F0" class="checktable" id="tblDetailList" >
        <tr>
        <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /> </td>
      </tr>
      <tr>
        <td height="30" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />组织结构图</span></td>
      </tr>
      <tr>
        <td  ><table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
          <tr>
            <td bgcolor="#FFFFFF"><table width="100%"border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC"   id="mainindex">
              <tr>
                <td align="left"  valign="top" bgcolor="#FFFFFF" class="Blue" ><div><br />
                        <input type="hidden" id="hidSelectValue" />
                        <asp:TreeView ID="UserDeptTree" runat="server" ShowLines="True" Width="795px"   > </asp:TreeView>
                        <%--   <div style="height:750px; width:100%;overflow-x:hidden;overflow-y:auto;" id="dv_tree" bgcolor="#F0F0F0">
           </div>--%>
                        <br />
                        <br />
                </div></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <br /></td>
    </tr>
    </table>
    </form>
</body>
</html>
