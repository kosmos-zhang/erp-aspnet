<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceElem.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceElem" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRule"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考核设置</title>
     <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/PerformanceElem.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script language="javascript"  type="text/javascript">
    function gggg()
    {
    alert (document .getElementById ("divDeptTree").innerHTML);
    }
    </script>
    <style type="text/css">
    #mainindex2{
        margin-top:10px;   
        margin-left:10px;
		background-color:#F0f0f0;
      	font-family:"tahoma";
      	color:#333333;
      	font-size:12px; 
}
    </style>
</head>
<body>          
<form id="frmMain" runat="server"  >
<div id="popupContent"></div>

<uc1:Message ID="msgError" runat="server" />
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype"></span>
 <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td align="right" colspan="2" style="height:30px"><a href="PerformanceType.aspx?ModuleID=2011801">考核类型设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceElem.aspx?ModuleID=2011801">考核指标设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceTemplate.aspx?ModuleID=2011801">考核模板设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="PerformanceT.aspx?ModuleID=2011801">人员考核流程设置</a></td>
    </tr>
     <tr>
        <td valign="top" colspan="2">
            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
        </td>
    </tr>
  <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">考核指标设置</td>
                    </tr>
        <tr>
            <td  valign="top" align="left" class="Blue" height="100%" width="30%">
                <input type="hidden" id="hidSelectValue" />
                <div>
                    <table style="margin-left:10%;">
                        <tr>
                            <td>
                                <%--<a href="#" onclick="SetSelectValue('','','');">考核指标展示</a>--%>
                               考核指标
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                   
                                        <td>
                                            <div id="divDeptTree" style="overflow-x:auto;overflow-y:auto;height:460px;width:500px;">正在加载数据,请稍等......</div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td align="left" valign="top" width="50%">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <input id="hidParentElemNo" type="hidden" runat="server" value="" />
                   
                        <td align="left" colspan="2">
                         <span class="Blue "> 选择的指标: </span> <div id="divSelectName" style="text-align:left; float:left; " class="Blue"></div>
                        </td>
                    </tr>
                    <tr><td colspan="2" height="20"></td></tr>
                     <tr>
                        <td colspan="2">
                           <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand"   onclick="DoEditDept('2');"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img src="../../../Images/Button/cw_tjtj.jpg" alt="添加同级" visible="false" 
                                id="btnAddSame" runat="server" style="cursor:hand;  " 
                                onclick="DoEditDept('0');"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img src="../../../Images/Button/cw_xj.jpg" alt="添加下级" visible="false" 
                                id="btnAddSub" runat="server" style="cursor:hand;  " 
                                onclick="DoEditDept('1');"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img src="../../../images/Button/Bottom_btn_edit.jpg" alt="修改" visible="false" id="btnEdit" runat="server" onclick="DoEditDot()" style='cursor:pointer;'   />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'    />
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
<div id="PerformanceElem" style="display:none;" >
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex2">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">考核指标名称</td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtTestNo" id="txtSearchElemName" runat="server" maxlength="50" 
                                            type="text" class="tdinput" width="85%"/>
                                    </td>
                                    <td class="tdColTitle" width="10%">启用状态</td>
                                    <td class="tdColInput" width="23%">
                                        <select id="sltSearchUsedStatus" runat="server">
                                            <option value="">请选择</option>
                                         <option value="1">启用</option>  
                                           <option value="0">停用</option>
                                           
                                        </select>
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%"></td>
                                    <td class="tdColInput" width="24%">
                                    </td>
                                </tr>     
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">  
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearchElem" visible="false" runat="server" style='cursor:pointer;' onclick='DoSearchInfo()' width="52" height="23" />
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false" style='cursor:pointer;' onclick="ClearInput()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5"></td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                   
                   
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ElemName','oC0');return false;">
                                                考核指标名称<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('StandardScore','oC1');return false;">
                                                标准分<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('AsseStandard','oC2');return false;">
                                                评分范围<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('ScoreArrange ','oC3');return false;">
                                                评分标准<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('AsseFrom','oC4');return false;">
                                                评分来源<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('Remark','oC5');return false;">
                                              备注  <span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderBy('UsedStatusName','oC6');return false;">
                                              启用状态  <span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- </div> -->
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageClickInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divPage">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
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
    </table>
    
    
   </div> 
     <div id="divEditCheckItem" runat="server" style="background: #fff; padding: 10px; width: 800px; z-index:900; position: absolute;top: 20%; left: 15%;   display:none    ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblDepttemInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr><td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                            <tr>
                                <td height="30" class="tdColInput">
                                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="true" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSaveInfo();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand"   onclick="DoBack();"/>
                                </td>
                                <td height="30" class="tdColInput" align="right">
                                   <%-- <img src="../../../Images/Button/Main_btn_print.jpg" alt="打印" visible="false" id="btnPrint" style="cursor:hand" height="25" />--%>
                                </td>
                            </tr>
                        </table></td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain"  >
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                            <input type="hidden" id="hidElemID" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr>
                                    <td height="20"   class="tdColTitle"  style="width:6%">指标编号<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput"  style="width:25%">
                                             <uc2:CodingRule ID="AimNum" runat="server" class="tdinput"       onkeyup="value=value.replace(/[\W]/g,'')"   onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"/>
                                        </td>
                                        <td height="20"  class="tdColTitle" style="width:7%">指标名称<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" style="width:15%">
                                            <input type="text" id="txtEditElemName" maxlength="50" class="tdinput"  SpecialWorkCheck="指标名称" />
                                        </td>
                                        
                                        <td height="20"  class="tdColTitle" style="width:6%">启用状态<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput"   style="width:15%">
                                            <select id="sltEditUsedStatus" class="tdinput">
                                               <option value="1">启用</option>
                                                 <option value="0">停用</option>
                                               
                                            </select>
                                        </td>
                                        
                                    </tr>
                                   
                                    
                                    
                                   
                                    <tr> 
                                    <td height="20" align="right" class="tdColTitle" width="1%">标准分<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" >
                                            <input type="text" id="txtStandardScore" maxlength="6" class="tdinput"  onchange='Number_round(this,"0");'/>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="6%">评分最小值<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" >
                                            <input type="text" id="txtMinScore" maxlength="6" class="tdinput"  onchange='Number_round(this,"0");'/>
                                        </td>
                                      <td height="20" align="right" class="tdColTitle" width="7%">评分最大值<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" >
                                            <input type="text" id="txtMaxScore" maxlength="6" class="tdinput"  onchange='Number_round(this,"0");'/>
                                        </td>
                                    
                                    </tr>
                                   <tr>
                                   <td height="20" align="right" class="tdColTitle" width="1%">评分标准</td>
                                        <td height="20" class="tdColInput" width="35%" colspan="5">
 
         <input id="txtAsseStandard" maxlength ="1024"   style="width:100%;  " class="tdinput"  SpecialWorkCheck="评分标准"/>
                                        </td>
                                   
                                   
                                   
                                   </tr>
                                    <tr>
                                   <td height="20" align="right" class="tdColTitle" width="1%">评分来源</td>
                                        <td height="20" class="tdColInput" width="35%" colspan="5">
                
                                             
                                               <input id="txtAsseFrom" maxlength ="1024"   style="width:100%; " class="tdinput"  SpecialWorkCheck="评分来源"/>
                                        </td>
                                   
                                   
                                   
                                   </tr>
                                    <tr>
                                   <td height="20" align="right" class="tdColTitle" width="1%">评分细则</td>
                                        <td height="20" class="tdColInput" width="35%" colspan="5">
                                          
                                                
                                                   <input id="txtScoreRules" maxlength ="1024"    style="width:100%;  " class="tdinput"  SpecialWorkCheck="评分细则"/>
                                        </td>
                                   
                                   
                                   
                                   </tr>
                                   <tr>
                                   <td height="20" align="right" class="tdColTitle" width="1%">备注</td>
                                        <td height="20" class="tdColInput" width="35%" colspan="5">
                                              
                                              
                                                <input id="txtRemark" maxlength ="1024"  style="width:100%;  " class="tdinput"  SpecialWorkCheck="备注"/>
                                        </td>
                                   
                                   
                                   
                                   </tr>
                                   
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2" height="10"></td></tr>
                    </table>
                <!-- </div> -->
                </td>
            </tr>
        </table>    
    </div>
</form>
</body>
</html>
