<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowApply.ascx.cs" Inherits="UserControl_FlowApply" %>
<style>
    .flowTopTitle
    {
        font-family: "tahoma";
        font-size: 14px;
        line-height: 120%;
        text-decoration: none;
        text-align: center;
        background-color: #E6E6E6;
        height: 30px;
        font-weight:normal;
    }
    .flowTitle
    {
        font-family: "tahoma";
        font-size: 12px;
        line-height: 120%;
        text-decoration: none;
        text-align: center;
        color:#333333;
        height: 22px;
        font-weight:normal;
    }
    .flowFont
    {
        font-family: "tahoma";
        font-size: 12px;
        line-height: 120%;
        text-decoration: none;
        text-align: left;
        color:#333333;
        height: 22px;
        font-weight:normal;
    }
</style>
<!-- Start 流程提交审批 -->
<div id="divFlowShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="FlowShadowIframe" frameborder="0"
        width="100%"></iframe>
</div>
<div id="divFlowApply" style="border: solid 10px #898989; background: #fff; padding: 10px;
    width: 400px; z-index: 999; display: none; top: 50%; left: 50%;top:100px;
    margin: 5px 0 0 -400px;">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
    
        <tr>
            <td colspan="2" height="30" class="flowTopTitle" id="tdTopTitle">
            </td>
        </tr>
        <tr>
            <td width="70" height="30">
                <span class="flowTitle">单据类型</span>
            </td>
            <td align="left">
                <font color="red" id="tdTypeName">入库单</font>
            </td>
        </tr>
        
        <tr>
            <td width="55" height="30">
                <span class="flowTitle">审批流程</span>
            </td>
            <td align="left">
                <select id="sltFlow" style="float:left" onchange="ChangeRemind(this.value);"></select>
            </td>
        </tr>
        <tr id="divApprovalPassRadio" style="display: none">
         <td width="55" height="30" class="flowTitle"><input type="radio" id="btnPass" name="btnApproval" value="0" checked onclick="document.getElementById('trApprovalNote').style.display='none';" />通过</td>
         <td class="flowTitle"><input type="radio" id="btnNoPass" name="btnApproval" value="1" onclick="document.getElementById('trApprovalNote').style.display='';" />不通过</td>

        </tr>
        <tr id="trApprovalNote" style="display: none">
            <td width="55" height="30" class="flowTitle">审批意见</td>
            <td>

                <textarea id="txtFlowNote" name="txtFlowNote" rows="4" cols="35"></textarea>
            </td>
        </tr>
        <tr id="trApprovalStep" style="display: none">
            <td height="50" align="left">
                <span class="flowTitle">审批步骤<br />
                    记录</span>
            </td>
            <td  id="tdApprovalStepContent" class="flowFont">
            </td>
        </tr>
        <tr>
        <td width="50"></td>
        <td align="left" class="flowFont">
        <input type="checkbox" id="isRemind" value="" checked="checked" />发送手机短信提醒
        </td>
        </tr>
        <tr id="trFlowApply" style="display: none">
            <td>
            <img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确定" onclick="objFlow.Fun_Save_FlowApply();" style="float:left" />  
            </td>
            <td> <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" onclick="objFlow.Fun_Hidden();"  /></td>

        </tr>
        <tr id="trFlowApproval" style="display: none">
            <td>
            </td>
            <td>
                <table><tr><td><img src="../../../Images/Button/Bottom_btn_confirm.jpg" alt="确定" onclick="objFlow.Fun_Save_FlowApproval();" /></td>
                    <td>&nbsp;&nbsp;&nbsp;</td><td><img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" onclick="objFlow.Fun_Hidden();" /></td></tr></table>
                
            </td>
        </tr>
    </table>
</div>
<!-- End 流程提交审批 -->
<!-- Start 审批操作记录 -->
<div id="divStepRecordList" style="border: solid 10px #898989; background: #fff; padding: 10px; width: 660px; height: 300px;overflow: scroll; z-index: 1002; position: absolute; display: none;
    top: 20%; left: 68%; margin: 5px 0 0 -400px;scrollbar-face-color: #ffffff; scrollbar-highlight-color: #ffffff;
        scrollbar-shadow-color: COLOR:#000000;scrollbar-3dlight-color: #ffffff; scrollbar-arrow-color: #006c90; scrollbar-darkshadow-color: #ffffff;">
        <table width="650">
            <tr>
                <td>
                    <span class="flowTitle" style="font-family: tahoma; color:#337ad2;font-weight: bolder;font-size: 12px;line-height:120%;">查看流程</span>
                </td>
                <td align="right">
                    <img src="../../../Images/Button/closelabel.gif" alt="关闭" onclick="document.getElementById('divStepRecordList').style.display='none';closeRotoscopingDiv(false,'divFlowShadow');" />
                </td>
            </tr>
        </table>
        <span id="tdRecordList"></span>
 
</div>
<!-- End 审批操作记录 -->
<script language="javascript" type="text/javascript">
var objFlow = new Object ();
objFlow.BillTypeFlag =  '<%=BillTypeFlag %>';   //单据分类标识
objFlow.BillTypeCode ='<%=BillTypeCode %>';     //单据分类编码
objFlow.BillID = 0;                             //单据ID
objFlow.BillNo ='';                             //单据编码
objFlow.PageUrl ='<%=this.Page.ToString() %>';  //当前页面
objFlow.FlowNo = '';                            //流程编号
objFlow.Note ="";                               //审批意见
objFlow.IsSure = false;                         //true:提交审批   false:确认
objFlow.IsRemind = true;                        //true:发送短信提醒  false:不发送短信提醒

</script>

<script src="../../../js/common/FlowApply.js" type="text/javascript"></script>




