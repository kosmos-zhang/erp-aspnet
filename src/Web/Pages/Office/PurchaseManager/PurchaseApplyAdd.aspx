<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplyAdd.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_Purchase_Add" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc1" %>
    <%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc2" %>
   <%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"  TagPrefix="uc3" %>  

 <%@ Register Src="../../../UserControl/BillChoose.ascx" TagName="BillChoose" TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/MaterialChoose.ascx" TagName="MaterialChoose"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc7" %>
 <%@ Register src="../../../UserControl/Common/GetExtAttributeControl.ascx" tagname="GetExtAttributeControl" tagprefix="uc9" %> 
<%@ Register Src="../../../UserControl/GetGoodsInfoByBarCode.ascx" TagName="GetGoodsInfoByBarCode"  TagPrefix="uc10" %> 
      <%@ Register src="../../../UserControl/Common/StorageSnapshot.ascx" tagname="StorageSnapshot" tagprefix="uc12" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购申请单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

 <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script> 
 
     <script src="../../../js/common/Check.js" type="text/javascript"></script>
     
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    
         <script src="../../../js/common/page.js" type="text/javascript"></script>
         
             <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
         
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>



 <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script> 

</head>
<body  >
 
    <form id="form1" runat="server">
  
        <uc6:MaterialChoose ID="MaterialChoose1" runat="server" /> 
                      <uc3:ProductInfoControl ID="ProductInfoControl1" runat="server" /> 
        <input type="hidden" id="HiddenAction" runat="server" value="Add" />
                <input type="hidden" id="HiddenURLParams" runat="server" />
                <input type="hidden" id="HiddenTargetPage" runat="server" />
                <input type="hidden" id='ThisID' runat="server" value="0" />
                <input type="hidden" id='FlowStatus' runat="server" value="0" />
                <input type="hidden" id='IsCite' runat="server" value="False" />
                 <input type="hidden" id="hiddenEquipCode" value="" />
                       <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" /> 
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
    <table width="95%"  style="height:57px" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                
               
                <img src="../../../images/Main/Line.jpg" width="122" height="7"  alt=""/>
            </td>
            <td align="center" valign="top">
            
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title" id="AddTitle">
                            新建采购申请单
                        </td>
                        <td height="30" align="center" class="Title" id="UpdateTitle" style="display: none">
                            采购申请单
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF"> 
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img runat="server" visible="false" id="imgSave" src="../../../images/Button/Bottom_btn_save.jpg"    onclick="SavePurApply();" style="cursor: pointer" title="保存采购申请单" alt="保存采购申请单" />
                                        <img runat="server" visible="false" alt="保存" src="../../../Images/Button/UnClick_bc.jpg" id="imgUnSave" style="display: none;" />
                                        <span id="GlbFlowButtonSpan" runat="server" visible="false"></span>
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="imgBack" style="cursor: hand;   display: none;"  onclick="DoBack()"  />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer"     title="打印" onclick="fnPrint();" alt=""/>
                                    </td>
                                </tr>
                            </table>
                     
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td class="Blue">
                                        基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" alt="" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"     id="Tb_01" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            单据编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <div id="divCodeRule" runat="server">
                             <uc1:CodingRuleControl ID="PurApplyNo" runat="server" /> 
                            </div>
                            <div id="divPurApplyNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            主题 
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtTitle" MaxLength="50" runat="server" CssClass="tdinput" specialworkcheck="主题" Width="95%"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            采购类别
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                          <uc2:CodeTypeDrpControl ID="ddlTypeID" runat="server" /> 
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请人<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                          
                            <asp:TextBox ID="UserApplyUserName" MaxLength="50" onclick="alertdiv('UserApplyUserName,txtApplyUserID');"
                                runat="server" CssClass="tdinput" Width="95%"  ReadOnly="true" ></asp:TextBox>
                            <input type="hidden" id="txtApplyUserID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请部门<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="DeptName" MaxLength="50" runat="server" onclick="alertdiv('DeptName,txtDeptID');"
                                CssClass="tdinput" Width="95%" ReadOnly="true"></asp:TextBox>
                            <input type="hidden" id="txtDeptID" runat="server" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            源单类型<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <select name="ddlFromType" class="tdinput"  style=" width:119px" id="ddlFromType" onchange="fnChangeSourceBill(this.value);">
                                <option value="0" selected="selected">无来源</option>
                                <option value="1">销售订单</option>
                                <option value="2">物料需求计划</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            申请日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txtApplyDate" MaxLength="50" runat="server" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtApplyDate')})"
                                Width="95%" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            到货地址
                        </td>
                        <td height="20" class="tdColInput" colspan="3" width="23%">
                            <asp:TextBox ID="txtAddress" MaxLength="50" runat="server" CssClass="tdinput" specialworkcheck="到货地址"   Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                   <uc9:GetExtAttributeControl ID="GetExtAttributeControl1" runat="server" /> 
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td class="Blue">
                                        合计信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            数量总计
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <input type="text" id="txtCountTotal" name="txtCountTotal" class="tdinput" value="0.00"    readonly="readonly" disabled ="disabled" runat="server"/>
                       
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                        </td>
                        <td height="20" class="tdColTitle" width="10%">
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td class="Blue">
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonNote'>    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divButtonNote')" alt="" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="hidden" id="txtCreatorID" name="txtCreatorID" class="tdinput" runat="server"    readonly="readonly" />
                            <input type="text" id="txtCreatorName" name="txtCreatorName" class="tdinput" runat="server"       disabled="disabled"  readonly="readonly" />
                        </td>
                        <td height="20" align="right" class="tdColTitle" width="10%">
                            制单日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txtCreateDate" name="txtCreateDate" class="tdinput" runat="server"   disabled ="disabled" readonly ="readonly"/>
                        </td>
                        <td class="tdColTitle" width="10%">
                            单据状态
                        </td>
                        <td class="tdColInput" width="24%">
                            <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" class="tdinput"
                                value="1" />
                            <input type="text" id="txtBillStatusName" name="txtBillStatusName" class="tdinput"     value="制单" readonly="readonly" disabled ="disabled"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="tdColTitle">
                            确认人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" value="0" class="tdinput"
                                runat="server" readonly="readonly" />
                            <input type="text" id="txtConfirmorName" name="txtConfirmorName" class="tdinput"  disabled="disabled"  runat="server" readonly="readonly" />
                        </td>
                        <td align="right" class="tdColTitle">
                            确认日期
                        </td>
                        <td align="left" class="tdinput">
                            <asp:TextBox ID="txtConfirmDate" MaxLength="50" runat="server" CssClass="tdinput"    Enabled="false"   Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新人
                        </td>
                        <td class="tdColInput">
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" value="0" class="tdinput"   runat="server" readonly="readonly" />
                            <input type="text" id="txtModifiedUserName" name="txtModifiedUserName" class="tdinput"   disabled="disabled"  runat="server"  readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="tdColTitle">
                            结单人
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="hidden" id="txtCloserID" name="txtCloserID" value="0" class="tdinput"
                                runat="server" readonly="readonly" />
                            <input type="text" id="txtCloserName" name="txtCloserName" class="tdinput" runat="server"
                                disabled="disabled" readonly="readonly" />
                        </td>
                        <td height="20" align="right" class="tdColTitle">
                            结单日期
                        </td>
                        <td height="20" align="left" class="tdColInput">
                            <input type="text" id="txtCloseDate" name="txtCloseDate" class="tdinput" runat="server"     disabled="disabled"      readonly ="readonly"/>
                        </td>
                        <td align="right" class="tdColTitle">
                            最后更新日期
                        </td>
                        <td align="left" class="tdinput">
                            <input type="text" id="txtModifiedDate" name="txtModifiedDate" class="tdinput" runat="server" disabled="disabled"     readonly ="readonly"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdColTitle" width="100">
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <input type="text" id="txtRemark" name="txtRemark" class="tdinput" runat="server"
                                specialworkcheck="备注" style="width: 99%" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="right" valign="top">
                            <img src="../../../images/Main/LineR.jpg" width="122" height="7" />
                        </td>
                        <td width="8">
                        </td>
                    </tr>
                    <tr>
                        <td height="25" valign="top">
                            &nbsp; <span class="Blue">采购申请明细来源</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='searchClick3'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('Tb_04','searchClick3')" /></div>
                        </td>
                        <td width="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Tb_04">
                 
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF" valign="bottom">
                            <img runat="server"  id="imgAdd" visible="false" src="../../../images/Button/Show_add.jpg" style="cursor: hand"       onclick="ShowProdInfo();" alt="" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_tj.jpg"   style="display: none;" id="imgUnAdd" />
                            <img runat="server" visible="false" src="../../../images/Button/Show_del.jpg" style="cursor: hand"     id="imgDel" onclick="DeleteDtlSSignRow();"  alt=""/>
                                <img src="../../../images/Button/btn_kckz.jpg" style="cursor: hand" onclick="ShowSnapshot();" alt="库存快照" />
                            <img runat="server" visible="false" alt="" src="../../../Images/Button/UnClick_del.jpg"
                                style="display: none;" id="imgUnDel" />
                            <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_From.jpg" style="cursor: hand; display: none;" id="imgGetDtl" onclick="ChooseSourceBill();" alt="" />
                            <img runat="server" visible="false" alt="从源单选择明细" src="../../../Images/Button/Unclick_From.jpg"
                                id="imgUnGetDtl" />
                            <img alt="条码扫描" visible="false" src="../../../Images/Button/btn_tmsm.jpg" id="btnGetGoods"  style="cursor: pointer" onclick="GetGoodsInfoByBarCode()" runat="server"   />
                           
                                 <span id="spanExport" runat="server"  visible="false">
                             <img src="../../../images/Button/btn_drmx.jpg" style="cursor: pointer" id="imgExport" alt="导入明细"  onclick="Show();" />
                              <img src="../../../images/Button/btn_drmxu.jpg" style="cursor: pointer; display:none;" id="imgUnExport" alt="导入明细"  />  </span>
                            <input type="hidden" id="DetailCount" runat="server" value="1" />
                            <div style="display: none">
                                <asp:DropDownList ID="ddlApplyReason" runat="server">
                                </asp:DropDownList>
                            </div>
                      <uc7:FlowApply ID="FlowApply1" runat="server" /> 
                        </td>
                    </tr>
                </table>
             
                <div id="divDetailTbl1" runat="server">
                <table width="99%" border="0" id="DetailSTable" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" width="50" align="center">
                            全选<input type="checkbox" id="checkall" onclick="fnSelectAll();" title="全选" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                            序号
                        
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品编号<span class="redbold">*</span>
                          
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            规格<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            单位<span class="redbold">*</span>
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            需求数量<span class="redbold">*</span>
                        </td>
                         
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            需求日期<span class="redbold">*</span>
                        </td>
                        
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" style="width: 7%">
                            申请原因
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            源单序号
                        </td>
                        
                    </tr>
                </table>
                 
               <uc12:StorageSnapshot ID="StorageSnapshot1" runat="server" /> 
               
                </div>
                  <uc5:BillChoose ID="BillChoose1" runat="server" /> 
        
                <br />
                <input name='txtTRLastIndex' runat="server" type='hidden' id='txtTRLastIndex' value="1" />
                <input name='TempData' type='hidden' id='TempData' />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" alt="" />
                        </td>
                        <td align="right" valign="top">
                            <img src="../../../images/Main/LineR.jpg" width="122" height="7"  alt=""/>
                        </td>
                        <td width="8">
                        </td>
                    </tr>
                    <tr>
                        <td height="25" valign="top">
                            &nbsp; <span class="Blue">采购申请明细</span>
                        </td>
                        <td align="right" valign="top">
                            <div id='Div1'>
                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" border="0" onclick="oprItem('DetailTable','Div1')" alt="" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="Td1">
            <div id="divDetailTbl2" runat="server">
                <table width="99%" border="0" id="DetailTable" style="behavior: url(../../../css/draggrid.htc)"  align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#E6E6E6" class="Blue" width="50" align="center">
                            <input type="checkbox" id="Checkbox1" onclick="SelectAll()" title="全选" style="cursor: hand" />
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            序号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品编号
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            物品名称
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            规格
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                            单位
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            申请数量
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                            需求日期
                        </td>
                        <td align="center" bgcolor="#E6E6E6" class="ListTitle" width="6%" id="PlanedCount"
                            style="display: none">
                            已计划数量
                        </td>
                    </tr>
                </table>
                </div>
                <br />
                <input name='txtTRLastIndex' type='hidden' id='Hidden2' value="1" />
                <input name='TempData' type='hidden' id='Hidden3' />
            </td>
        </tr>
    </table>
    <div>
    </div>
    
        <div id="divExcelRotoscoping" style="display:none">
<iframe id="divExcelIframe" frameborder="0" width="100%" ></iframe>
</div>
<div id="divExportExcel" style="border: solid 4px #999999; background: #fff; z-index: 1000; position: absolute;display:none;left: 60%;top:50%; width:30%">
        
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" >
        <tr style="height: 5px"></tr>
        <tr>
        <td align="center" colspan="2" >
        <b>导入明细</b>

        </td>
        </tr>
        <tr>
        <td  align="center" colspan="2">
          <input type="file" id="fileExportExcel" style="width:70%" name="fileExportExcel" runat="server"/> <span style="padding-left:5px" ><a href="采购申请明细导入模板.rar">模板下载</a></span>
      
        </td>
        
        </tr>
        <tr style="height:10px">
        <td  align="center" colspan="2">
       
            <div id="divErrMsg" >
            </div>
       
        </td>
        </tr>
        <tr style="height:10px">
        <td height="10px">
        </td>
        <td align="center">
            <asp:ImageButton ID="imgExportExcelUC" runat="server" AlternateText="确认"  ImageUrl="../../../images/Button/Bottom_btn_confirm.jpg" OnClick="imgExportExcelUC_Click"/>
             <img alt="关闭" id="btn_Close" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: pointer;'
            onclick='Hide();' />
        </td>
        </tr>
        </table>

</div>
<script type="text/javascript" language="javascript">

 
 
 function Show()
 {
 if (document.readyState!="complete") return ;
 openRotoscopingDiv(false,"divExcelRotoscoping","divExcelIframe");
    document.getElementById("divExportExcel").style.display="";
    $("#divExportExcel").show();
    CenterToDocument("divExportExcel",true);
 }
 
function Hide()
{
if (document.readyState!="complete") return ;
$("#divExportExcel").hide();
    closeRotoscopingDiv(false,"divExcelRotoscoping");
}
</script>

       

    </form>
 

      <uc10:GetGoodsInfoByBarCode ID="GetGoodsInfoByBarCode1" runat="server" /> 
   <%--     <script src="../../../js/common/Common.js" type="text/javascript"  defer="defer"></script>--%>
        <script   type="text/javascript">
        var glb_BillTypeFlag = 6;
        var glb_BillTypeCode = 1;
        var glb_BillID = document.getElementById("ThisID").value;
        var glb_IsComplete = true;
        var FlowJS_HiddenIdentityID ='ThisID';
        var FlowJs_BillNo ='PurApplyNo_txtCode';
        var FlowJS_BillStatus ='txtBillStatusID';
</script>
   

</body>

<script src="../../../js/common/Flow.js" type="text/javascript"    ></script>
   <script src="../../../js/office/PurchaseManager/PurchaseApplyAdd.js" type="text/javascript"  ></script> 
     <uc4:Message ID="Message1" runat="server" />
</html>





