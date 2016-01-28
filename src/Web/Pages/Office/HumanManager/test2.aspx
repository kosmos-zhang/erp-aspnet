<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test2.aspx.cs" Inherits="Pages_Office_HumanManager_test2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织机构表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/DeptInfo_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <%-- js--%>
    <table id='tbEdit' width='100%' border='0' cellspacing='0' cellpadding='3'  class='tab_a'><tr><th>选择<input type=\"checkbox\" id=\"chkCheckAll2\" name=\"chkCheckAll2\" onclick=\"changEdit(this);\"/></th><th  colspan='2' width='100'>指标名称</th><th width='200' >权重</th></tr><tbody>
    <tr class='newrow'><td height='22' align='center'><input  type='checkbox' class='Delete'  onpropertychange='getChageFlow(this)'/></td><td height='22' align='center'  ><input  id='txtEditTemp0' readonly ='readonly'  type='text' value=出勤率 title=1 class='tdinput'  class='tempList'/></td> <td height='22' align='center'><input  id='txtEditScore0'   type='text' value=50.00 title=1    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr>
    
    
    <tr class='newrow'> <td height='22' align='center'><input  type='checkbox' class='Delete'  onpropertychange='getChageFlow(this)'/></td>
    
    <td height='22' align='center'  rowspan=2  >工作认真程度</td><td height='22' align='center' ><input  id='txtEditTemp1' readonly ='readonly'  type='text' value=客户拜访率 title=3 class='tdinput'  class='tempList'/></td><td height='22' align='center'><input  id='txtEditScore1'   type='text' value=25.00 title=3    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr><tr class='newrow'>     <td height='22' align='center'></td><td height='22' align='center' ><input  id='txtEditTemp2' readonly ='readonly'  type='text' value=工作态度 title=4 class='tdinput'  class='tempList'/></td><td height='22' align='center'><input  id='txtEditScore2'   type='text' value=25.00 title=4    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr></tbody></table>
    
    
    
    <table id='Table1' width='100%' border='0' cellspacing='0' cellpadding='3'  class='tab_a'><tr><th>选择<input type=\"checkbox\" id='\"checkbox\"1' name=\"chkCheckAll2\" onclick=\"changEdit(this);\"/></th><th  colspan='2' width='100'>指标名称</th><th width='200' >权重</th></tr><tbody>
    <tr><td><input  type='checkbox'       class='Delete'  title='0'  onpropertychange='getChageFlow(this)'/></td><td >出勤率<td><td><input type='text' size='10' class='RateClass' title='1'  value='50.00'  maxlength='10' style='border:none;border-bottom:solid   0px   black;'  /></td></tr>
    
    
    <tr><td  ><input  type='checkbox'   class='Delete'   title='1' onpropertychange='getChageFlow(this)' /></td>
    
    <td rowspan=2 >工作认真程度</td><td>客户拜访率</td><td><input type='text' size='10' class='RateClass' title='3' maxlength='10'  value='25.00'  class='tdinput'  style='border:none;border-bottom:solid   0px   black;'/></td></tr><tr><td></td> <td>工作态度</td><td><input type='text' size='10'  value='25.00'  class='RateClass' title='4'   maxlength='10' class='tdinput' style='border:none;border-bottom:solid   0px   black;'/></td></tr>
    </tbody></table>
    
    
    
    </form>
</body>
</html>
