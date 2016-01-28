<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendedSMList.aspx.cs" Inherits="Pages_Office_CustManager_SendedSMList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/Personal/Common.js" type="text/javascript"></script>
<script src="../../../js/Office/CustManager/SendedSMList.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<title>已发手机短信</title>
        <style type="text/css">
            .style1
            {
                width: 266px;
            }
            .style2
            {
                width: 77px;
            }
            .style3
            {
                width: 212px;
            }
            .style4
            {
                width: 218px;
            }
           #editPanel
        {
            width:400px;
            background-color:#fefefe;
            position:absolute;
            border:solid 1px #000000;
            padding:5px;
        }
        </style>
        <script language="javascript" type="text/javascript">
                  function viewMsg(id)
        {
              var action  ="GetItem";              
              $.ajax({ 
                    type: "POST",
                    url: "../../../Handler/Personal/MessageBox/SendBox.ashx?action=" + action,
                    dataType:'string',
                    data: 'id='+id,
                    cache:false,
                    success:function(data) 
                    {                          
                        var result = null;
                        eval("result = "+data);
                        
                        if(result.result)                    
                        {
                              //showInfo(result.data);  
                               dispData(result.data);              
                        }else{                  
                              showInfo(result.data);               
                        }                   
                    },
                    error:function(data)
                    {
                         showInfo(data.responseText);
                    }
                });
                
        }
        function dispData(data)
        {
            var ttID = document.getElementById("ttID");
            var ttTitle = document.getElementById("ttTitle");
            var ReceiveUserID = document.getElementById("ReceiveUserID");
            var ttSendDate = document.getElementById("ttSendDate");
            var ttContent = document.getElementById("ttContent");
            ttTitle.innerHTML = data.ID;
            ReceiveUserID.innerHTML = data.ReceiveUserID;
            ttTitle.innerHTML = data.Title;
            ttSendDate.innerHTML = data.CreateDate;
            ttContent.innerHTML = data.Content;
            
            document.getElementById("ttState").innerHTML = data.State =="0"?"未读":"已读";
            
            document.getElementById("editPanel").style.left = "200px";
            document.getElementById("editPanel").style.top ="200px";
            document.getElementById("editPanel").style.display = "";
        }
        function hideMsg()
        {
            document.getElementById("editPanel").style.display = "none";
        }
        
        </script>
</head>
<body>
<form id="form1" runat="server">
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td rowspan="2" align="right" valign="top"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/></div>
        &nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td  valign="top" class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件</td>
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF">
       
        <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right>发送时间   </td>
            <td bgcolor="#FFFFFF" class="style1"> <input onkeypress="return false;" name="createDate" id="createDate1" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
                ~<input onkeypress="return false;" name="createDate" id="createDate2" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
           </td>           
            <td bgcolor="#E7E7E7" align=right class="style2">发送人</td>
            <td bgcolor="#FFFFFF" class="style3"><input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:180px;" /></td>
            <td bgcolor="#FFFFFF" align=left class="style4"><img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchEquipData()' width="52" height="23" /></td>
            <td bgcolor="#FFFFFF">&nbsp;</td>
            
          </tr>   
          
                 
          
          <tr class="table-item">
            <td width="10%" height="20" bgcolor="#E7E7E7" align=right>&nbsp;</td>
            <td bgcolor="#FFFFFF" class="style1">&nbsp;</td>
          <td colspan="6" align="left" bgcolor="#FFFFFF">
          </td>
           </tr>  
        </table></td>
      </tr>
    </table>
    </td>
  </tr>
</table>

<table width="95%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td align="center" valign="top">&nbsp;
    
    </td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">已发手机短信</td>
  </tr>
  
  <tr>
    <td height="35" colspan="2" valign="top">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF">
          &nbsp;&nbsp;&nbsp; <%--<img src="../../../images/Button/Main_btn_delete.jpg" runat="server" title="删除" id="btn_delete" onclick="DelMessage()" style='cursor:pointer;' width="51" height="25" />
         --%>
        </td>
        </tr>
      </table>
    </td>
  </tr>
  
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>      
       
          <th width="30%"  align="center" background="../../../images/Main/Table_bg.jpg" 
              bgcolor="#FFFFFF"  ><div class="orderClick" onclick="OrderBy(this,'Content','oC5');return false;">
            内容<span></span></div></th>
           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
        <div class="orderClick" onclick="OrderBy(this,'ReceiveUserName','oC2');return false;">
            接收人姓名<span></span></div></th>
           <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
        <div class="orderClick" onclick="OrderBy(this,'ReceiveMobile','oC2');return false;">
            接收人手机号<span></span></div></th>
              
        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
        <div class="orderClick" onclick="OrderBy(this,'SendUserName','oGroup');return false;">
            发送人<span></span></div></th>
        <th align="center" background="../../../images/Main/Table_bg.jpg" 
              bgcolor="#FFFFFF" ><div class="orderClick" onclick="OrderBy(this,'SendDate','oC1');return false;">
            发送时间<span></span></div></th>
            
      </tr>
    </tbody>
    </table>

      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="PageCount"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total" style="display:none"></span>每页显示
              <input name="text" type="text" id="ShowPageCount"/>
                条 转到第
              <input name="text" type="text" id="ToPage"/>
                页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
</form>



<div id="editPanel" style="display:none;">            
    <table id="itemContainer" border="1" width="100%" cellpadding="3" style="border-collapse:collapse;">
       
        <tr>
            <td style="width:40px;">主题</td><td>
                <span id="ttTitle"></span>
            </td>
        </tr>       
         <tr>
            <td>发送时间</td><td>
                <span id="ttSendDate"></span>
            </td>
        </tr>               
         <tr>
            <td>接收人</td><td>
                <span id="ReceiveUserID"></span>
            </td>
        </tr>
        <tr>
            <td>阅读状态</td><td>
                <span id="ttState"></span>
            </td>
        </tr>
        <tr><td>短信内容：</td><td align="left" valign="top">
            <span id="ttContent"></span>
        </td></tr>
        
        <tr><td></td><td style="padding:3px;">
            <a href="#" onclick="hideMsg()">确定</a>&nbsp;&nbsp;            
        </td></tr>
    </table>
</div>

</body>
</html>
