<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceT.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
      <%@ Register src="../../../UserControl/Human/SelectedTemplateList.ascx" tagname="TemplateList" tagprefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员考核流程设置</title>
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

   <style type="text/css">
    
       .tab_a{width:99%; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
    </style>
    <style type="text/css">
       
        #txtTitle
        {
            width: 466px;
        }
       
        #txtToList
        {
            width: 467px;
        }
        .style1
        {
            width: 470px; 
        }
                       
          #typeListTab {background: #2255bb;padding:5px;margin:0px;width:202px;background:#999999;}
       /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */
       
       
       .tab{cursor:pointer;display:inline;color:White;background-color:inherit;margin-left:5px;border:solid 1px #666666;padding:2px;}
       .selTab{cursor:pointer;display:inline;color:Black;background-color:White;margin-left:5px;border:solid 1px #666666;padding:2px;}
       
       .tabe {cursor:pointer;display:inline;color:Black;background-color:White;margin-left:15px;padding:2px;}
       
       #userList  {border:solid 1px #999999;width:200px;height:300px;overflow:auto;padding-left:10px;}
              
        .style4
        {
            width: 208px;
        }
              
        .style5
        {
            font-family: "tahoma";
            color: #337ad2;
            font-weight: bolder;
            font-size: 12px;
            line-height: 120%;
            text-decoration: none;
            height: 19px;
        }
              
    </style>
     <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/personal/common.js" type="text/javascript"></script>
  <script src="../../../js/personal/MessageBox/UserListCtrl.js" type="text/javascript"></script>
    <script src="../../../js/personal/MessageBox/SendInfo.js" type="text/javascript"></script>
    
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
       <script src="../../../js/office/HumanManager/PerformanceT.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var firstload = true;
                 
        $(document).ready(function(){
        
            //LoadUserList('LoadUserList',BuildTree);        
            LoadUserList('LoadUserListWithDepartment',BuildTree)
        });
        
        
        
        var curFlag = 1;
        function swithEditPanel(flag)
        {
            var lastEle = document.getElementById("tab_"+curFlag);
            lastEle.className = "tab";
            curFlag = flag;
            document.getElementById("tab_"+flag).className = "selTab";
           
            //分组模式
            if(flag == 2)
            {
                document.getElementById("grouppanel").style.display = "block";
                document.getElementById("userspanel").style.display = "none";
            }else{
                document.getElementById("grouppanel").style.display = "none";
                document.getElementById("userspanel").style.display = "block";
            }
            
        }
        
      
        
    </script>
        <script type="text/javascript">
        function ondd()
        {
           //alert (document.getElementById ("txtToList").value );
           alert ( document.getElementById("seluseridlist").value );
           alert ( document .getElementById ("templateNo").value);
        }
        
        </script>
</head>
<body>
<br/>

<form id="EquipAddForm" runat="server" >
<input id="hidCon1" type="hidden"   value="2" />
<div id="popupContent"></div>
<uc1:Message ID="msgError" runat="server" />
<uc2:TemplateList ID="TemplateList1" runat="server" />
<span id="Forms" class="Spantype"></span>
<table  width="98%" border="0" cellpadding="0" cellspacing="1" class="checktable" id="mainindex">
  <tr>
    <td align="right" colspan="2" style="height:20px" bgcolor="#f0f0f0"><a href="PerformanceType.aspx?ModuleID=2011801">考核类型设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceElem.aspx?ModuleID=2011801">考核指标设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceTemplate.aspx?ModuleID=2011801">考核模板设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="PerformanceT.aspx?ModuleID=2011801">人员考核流程设置</a></td>
    </tr>
    <tr>
        <td valign="top" colspan="2" bgcolor="#f0f0f0">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
                    <tr id="trSearch2" runat="server" style="display:block; "  >
                        <td height="20"  bgcolor="#f0f0f0" >
                           <span class="Blue"><img src="../../../images/Main/Arrow.jpg"  height="18" align="absmiddle" /></span>检索条件
                           
                           
                           
                        </td>
                        <td  bgcolor="#f0f0f0"   align="right"   >        <div id='searchClick6'>
                                          <img src="../../../images/Main/Close.jpg" style="cursor: pointer" id="img3" onclick="oprItem('tbss','searchClick6')" /></div></td>
                    </tr>
                     <tr id="trSearch" style="display:block;  "    >
       <td colspan="2" valign="top" width="100%" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC" id="tbss">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" >
                            <table width="100%" border="0" cellspacing="1" cellpadding="2" bgcolor="#CCCCCC" >
                           
                                
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">
                                     <span style="color:Black; font-weight:normal ">   考评人</span></td>
                                    <td class="tdColInput" width="23%">
                                               <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtSearchScoreEmployee');"
                                runat="server" CssClass="tdinput" style="width:100%"  ReadOnly="true"></asp:TextBox>
                            <input type="hidden" id="txtSearchScoreEmployee" runat="server" />
                                 </td>
                                    <td class="tdColTitle" width="10%">
                                     <span style="color:Black; font-weight:normal ">   被考核人</span></td>
                                    <td class="tdColInput" width="23%">
                                              <asp:TextBox ID="UserEmployeeID" MaxLength="50" onclick="alertdiv('UserEmployeeID,txtSearchEmployeeID');"
                                runat="server" CssClass="tdinput" style="width:100%" ReadOnly="true"></asp:TextBox>
                            <input type="hidden" id="txtSearchEmployeeID" runat="server" />
                                             </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                      <span style="color:Black; font-weight:normal ">   模板名称</span></td>
                                    <td class="tdColInput" width="24%">
                                        <input name="txtTestNo1" id="txtSearchTemplateName" runat="server" 
                                            maxlength="50" type="text" class="tdinput" style="width:100%"      /></td>
                               
                                </tr>    
                                <tr>
                              
                                               <td  colspan="7"  class="tdColInput" align="center">
             
              <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchFlowData()'    runat="server" visible="false"   id="btnSearch"/>
              <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false" style='cursor:pointer;' onclick="ClearSearchCondition()" width="52" height="23" />--%>
              </td>
                                
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
             <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" >
                 <tr>
                        <td valign="top" bgcolor="#f0f0f0">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title" bgcolor="#f0f0f0">人员考核流程设置</td>
                    </tr>
                           <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                               <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand"  onclick="showFlowEdit();"/>
                                <img src="../../../Images/Button/Main_btn_delete.jpg" alt="删除" visible="false"     id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'     />
                                  <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
             </table>
         <table  border="0"  align="center" cellpadding="2" cellspacing="1" id="tbSearch" width="99%"  >
                   
                    <tr>
                        <td colspan="2">
   
                            <table width="99%" border="0" align="center" cellpadding="0"  cellspacing="1" id="tbFlowDetail"  bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect1')"/></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('EmployeeName','oC0');return false;">
                                                被考评人员<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('TemplateName','oC1');return false;">
                                                模板名称<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('StepName','oC2');return false;">
                                                考核步骤<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('ScoreEmployee','oC3');return false;">
                                                考评人<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                   
                                    
                                </tbody>
                               
                            </table>
                     
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList" >
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="div4">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtPageCount" size="3"  maxlength="5"/>条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage2" type="text" id="txtToPage2" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtPageCount').val(),$('#txtToPage2').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>
                        </td>
                    </tr>
                </table>
       
       </td>
     
       </tr>
  
  <tr id="add_1"  style="display :none ">
    <td colspan="2" valign="top" width="100%" >
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999" id="Tb_01" class="checktable" >
         
             <tr bgcolor="#f0f0f0" class="Blue">
                        <td   align="center" valign="top" class="Title" colspan="2">人员考核流程设置</td> 
                    </tr>
                          <tr bgcolor="#F4F0ED" class="Blue">
                        <td height="35" colspan="2" valign="top">
                            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                      <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"  onclick="DoSaveInfo();"/>
                <img   id="btnBack" runat="server" alt="返回"   onclick="showFlowEdit();"    src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor:hand"   visible="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
      <tr>
           <td bgcolor="#FFFFFF" class="Blue">
                 <div style=""><img src="../../../images/Main/Arrow.jpg"  height="18" align="absmiddle" /></span>1.选择被考核人</div>
                 <input type="hidden" value="" id="seluseridlist" />
                  <input type="hidden" value="" id="templateNo" />
                 <%--<input type="text" id="txtKey" /><input type="button" value="搜" />--%>
           </td> 
               
                 <td bgcolor="#FFFFFF"    class="style1"  >
                <input type="text" id="txtToList" readonly   style="color:#999999;" value="请选择被考核人" class="tdinput" />               
                 </td>
        </tr>
        <tr>
           <td bgcolor="#FFFFFF" class="style4">
                      <ul id="typeListTab">
         <li id="tab_0" class="tab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)">全部</li>
                            <li  id="tab_1" class="selTab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">部门</li>
                         <%--   <li  id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)">分组</li>--%>
                            
                            
                          
    </ul>
                 </td> <td bgcolor="#FFFFFF" 
                class="Blue"  >
                   <span class="Blue"><img src="../../../images/Main/Arrow.jpg"  height="18" align="absmiddle" /></span>2.选择考核模板</td>
        </tr>   
        <tr>
           
                 <td valign="top" bgcolor="#FFFFFF" class="style4" >
                        <table>
                        <tr>
                        <td>
                                  <div id="userList" style="width:230px;"></div>
                                  <div style="border:solid 1px #999999;padding:5px;text-align:center;width:230px;" >
                                  <a href="#" onclick="treeview_selall()">全选/清空</a>
                                  </div>
                        </td>                    
                        </tr>
                        </table>
                       
                 </td> 
          
            <td bgcolor="#FFFFFF"  valign="top" style=" width:800px"  >        
            <div id="contentContainerDiv"  style="overflow-y:auto;width:91%; line-height:14pt;letter-spacing:0.2em;height:332px">
                     <table id="tbTemplate" class="tab_a"  >
                                <tbody align="center"></tbody>
                                </table>
            </div></td>
        </tr>       
        <tr>
        <td colspan="2">
        <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" >
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table border="0" cellspacing="0" cellpadding="3" style="width: 100%">
                                <tr bgcolor="#f0f0f0">
                                    <td>
                                        <span class="Blue"><img src="../../../images/Main/Arrow.jpg"  height="18" align="absmiddle" /></span>3.新建流程步骤信息</td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                          <img src="../../../images/Main/Open.jpg" style="cursor: pointer" id="imgSteps" onclick="oprItem('tbDetail','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
      <table id="tbDetail"  style="BEHAVIOR:url(../../../draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" class="tab_a"  width="100%">
       <thead >
       
       <td  colspan="6" >
   
      <%--  <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="true" id="btnNew" runat="server" style="cursor:hand" height="25" onclick="showEdit();"/>--%>
      <%--  <input  type="button" id="txtEd" onclick ="submitMessage();"  value="添加考评信息"/>--%>
        <img src="../../../images/Button/Button_zjlc.jpg" alt="增加流程" visible="false"      id="btnAddFlow" runat="server" onclick="submitMessage()"  style='cursor:pointer; />
        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDeleteFlowp" runat="server" onclick="Delete()" style='cursor:pointer;'   />
        <img alt=""  src="../../../Images/Button/Bottom_btn_up.jpg"  id="btnSwapUp" runat ="server" visible="false"    onclick="SwapRow();" />
          <img alt="" src="../../../Images/Button/Bottom_btn_down.jpg"  id="btnSwapDown" runat ="server" visible="false"      onclick="SwapRowDown();"/>
        </td>
       
       </thead>
        <tr ><td style="width:6%">选择<input type="checkbox" id="chkCheckAll2" name="chkCheckAll2" onclick="changEdit(this);"/></td><td  style="width:15%" align="center"><span style="color :Red ">*</span> 考评人</td><td  style="width:25%" align="center"><span style="color :Red ">*</span> 步骤名</td><td  style="width:20%" align="center"><span style="color :Red ">*</span> 权重</td><td  style="width:20%" align="center">顺序</td><td style="width:20%">备注</td></tr>
        <tbody >
       
        
        
        </tbody>
       
        </table>
        
        </td>
        
        </tr>
      </table>
      
      
      </td>
  </tr>
                </table>
<table   width="99%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">

  
      
      </table>
      <div id="divShowEdit"  
        style="background: #fff; padding: 10px; width: 850px; z-index:950; position: absolute;top: 18%; left: 5%;  display:none ; ">
          <table width="100%" width="100%" border="0" cellspacing="0" cellpadding="3"  class="tab_a">
        <tr>
        <td colspan="2" align="right"bgcolor="#FFFFFF">
    <%--    <input id="Button1" type="button" value="增加模板"  onclick =" EditTemplate()" />--%>
                <img src="../../../images/Button/Button_zjlc.jpg"  alt="增加流程" visible="false" id="btnAddFlowP" runat="server"  onclick="EditTemplate()" style='cursor:pointer;'   />
       <img  src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDeleteFlow"  runat="server" onclick="DeleteTemplate()" style='cursor:pointer;'   />
        <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSaveFlow" style="cursor:hand"   onclick="DoSaveMessage();"/>
      <img   id="Img6" runat="server" alt="返回"   onclick="Editcancel();"  src="../../../Images/Button/Bottom_btn_back.jpg" style="cursor:hand"    visible="true" />   </td>
         
        </tr>
        <tr>
        <td class="tdColTitle">被考评人</td>
        <td class="tdColInput"><input id="txtScorEmp" type="text" readonly="readonly"  class="tdinput"/></td>
            <td class="tdColTitle">考核模板</td>
        <td class="tdColInput"><input id="Text1" type="text" readonly="readonly"  class="tdinput"/>
        <input  id="txtEditTemp1" readonly ="readonly"  type="text"    class="tdinput"  class="tempList"/>
        </td>
        </tr>
         </table>
           
       <table id="tbTemplateEdit" width="100%" border="0" cellspacing="0" cellpadding="3"  class="tab_a">
      <thead >
      <tr >
<%--      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">模板名称</td>--%>
      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">考评人</td>
      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">顺序</td>
      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">步骤名称</td>
      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">权重(%)</td>
      <td align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">备注</td>
      <td>选择<input type="checkbox" id="Checkbox1" name="chkCheckAll3" onclick="changEdit2(this);"/></td>
      </tr></thead>
       <tbody  ></tbody>
       </table>
      
      </div>
      <div style ="visibility:hidden ; display:none ;">
     <div id="userspanel" style="display:none ;">
                                <input type="button" value="添加到分组" onclick="AddContact()"/><br /><br />
                                分组::<select id="slgroupid"><option value="-1">-选择分组-</option></select>
                                
                            </div>
<div id="grouppanel" style="display:none;">
                                     
                                    <input type="button" value="添加分组" onclick="AddGroup();document.getElementById('groupname').focus();" /><br /><br />
                                    <input type="button" value="修改分组" onclick="EditGroup()" /><br /><br />
                                    <input type="button"  value="删除分组" onclick="DelGroup()" /> <br /><br />
                                    
                                    <input type="button" value="删除联系人"   onclick="DelContact()"/>       
                                    
                                    
                                    <br /><br />
                                     分组名称<font color=red>*</font>：<br />
                                     <input type="text" id="groupname" />
                                    <input type="hidden" id="groupid" /><br /><br />
                                    
                                    <input type="button" value="保存" onclick="SaveGroup()" />
                                    <input type="button" value="取消"  onclick="CancelGroup()"/>                
                            </div></div>
    </form>
   

</body>

</html>
