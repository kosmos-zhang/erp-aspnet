
/**********************************************
* 类作用：   审批流程处理
* 建立人：   王玉贞
* 建立时间： 2009-04-07
* Copyright (C) 2005-2009 wangyz
* All rights reserved
* 备注信息如下：
 
[Step 1:]
var glb_BillTypeFlag =<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PRODUCTION %>;
var glb_BillTypeCode = <%=XBase.Common.ConstUtil.BILL_TYPECODE_PRODUCTION_SCHEDULE %>;
var glb_BillID = intMasterProductID;                                //单据ID
var glb_IsComplete = true;                                          //是否需要结单和取消结单(小写字母 true:需要结单和取消结单   false:不需要结单和取消结单)
var FlowJS_HiddenIdentityID ='txtIndentityID';                      //自增长后的隐藏域ID
var FlowJs_BillNo ='codruleMasterProductSchedule_txtCode';          //当前单据编码名称
var FlowJS_BillStatus ='txtBillStatus';                             //单据状态ID
var FlowJS_Material = false;                                        //此变量针对王玉贞所开发的模块领料退料，需调用时在页面声明FlowJS_Material=true,其它模块均不需要加上此变量
调用的相关流程页面需要声明这三个变量

[Step 2:]
<span id="GlbFlowButtonSpan"></span>
在保存按钮后面加上该span，用于动态加载不同审批状态的按钮显示

[Step 3:]
每个页面需加载该JS文件

[Step 4:]
调用的页面PageLoad时绑定指定参数(具体的常量值，请参考pubdba.BillType表中定义)
    
[sample]:
#region 提交审批调用方法例子
FlowApply.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_PRODUCTION;
FlowApply.BillTypeCode = ConstUtil.BILL_TYPECODE_PRODUCTION_SCHEDULE;
#endregion
    
[Step 5:]
页面在新建时：调用GetFlowButton_DisplayControl() 
页面在编辑时：加载完数据时调用GetFlowButton_DisplayControl()
    
[Step 6:]
每个模块加上确认、结单、取消结单三个方法，名称已经定义，逻辑部分在该方法名称里处理即可
    
Fun_ConfirmOperate();//确认按钮的调用的函数名称，业务逻辑不同模块各自处理
Fun_CompleteOperate(ture);//结单按钮调用的函数名称，业务逻辑不同模块各自处理
Fun_CompleteOperate(false);//取消结单按钮调用的函数名称，业务逻辑不同模块各自处理
    
[Step 7:]
每个模块加上取消确认方法，名称已定义，业务逻辑部分在该方法名称里处理即可
Fun_UnConfirmOperate();//点击取消确认按钮调用的方法名称
    
也可参考Pages/Office/ProductionManager/MasterProductSchedule_Add.aspx页面
    

    
修改记录如下：
[2009-04-17]:页面新加FlowJs_BillNo变量,请参考Step 1     [原因：个人桌面/流程管理里需要单据编码]
[2009-04-18]:页面新加glb_IsComplete变量,请参考Step 1    [原因：有些模块不需要结单和取消结单按钮]
[2009-04-28]:Fun_FlowApply_Operate_Succeed(operateType) [原因：有些模块需要控制保存按钮颜色]
operateType=0时是提交审批成功后
operateType=1时是审批成功后
[2009-05-06] Fun_FlowApply_Operate_Succeed(operateType)            
operateType=2时是审批不通过 成功后
[2009-05-11] 该JS新加撤消审批按钮
operateType=3时是撤消审批 成功后
[2009-05-13] 该JS新加审批流程按钮,用来查询用户审批操作记录，调用页面无需做改动
[2009-05-14] 个别模块不需要确认按钮，在页面调用时加上
var glbBtn_IsConfirm = false;此变量只针对李裕松所开发的模块不需要确认按钮，其他开发人员不需要声明此变量
[2009-05-23] 调拨模块 需要根据 业务状态 来启用 结单 按钮  有业务状态关联的单据 可引用，其他模块无需声明此变量  added by  pdd
var glbtn_IsClose=true; 定义此变量
[2009-06-06] 新加取消确认按钮，调用函数Fun_UnConfirmOperate(),此函数不同模块自己加，处理自己的业务逻辑,没有确认功能的（李裕松模块中的某个页面）不需要加此函数
另外取消确认相当于撤消审批的操作，大家在自己的data层需要调用一个通用的方法,在Data/Common/FlowDBHelper.cs下,方法名称是
OperateCancelConfirm(string CompanyCD,int BillTypeFlag,int BillTypeCode,int BillID,string loginUserID,TransactionManager tran)
tran是自己的事务，前面几个参数不需要声明了吧:)
补充：我的调用事务和某些开发人员不太一样,大家可以参考周军做了一个页面，已经实现取消确认,如需参考，获取以下文件:
SellOfferAdd.aspx
SellOfferAdd.js
SellOfferAdd.ashx
SellOfferDBHelper.cs
SellOfferBus.cs
ConstUtil.cs
JsonClass.cs
[2009-06-12] 页面加载时声明变量var FlowJS_FromSell = 'hiddState';此变量只针对周军开发的销售模块,hiddState为单据的状态值的控件名称,例:此值为数值型(1,2,3,4),当等于2时，审批相关按钮变灰
[2009-06-15] 页面加载时声明变量var FlowJS_FromSellDetail = 'hiddStatus';此变量只针对周军开发的销售模块,hiddState为单据的状态值的控件名称,例:此值为数值型(1,2,3,4),当等于3时，审批相关按钮变灰
      
          
                       

**********************************************/

$(document).ready(function() {
    fnGetExtAttr(); //物品控件拓展属性

});

//===============================================按 钮 操 作=======================================
//审批流程按钮显示控制
function GetFlowButton_DisplayControl() {

    //Start -逻辑判断(1：有没有定义审批流程 2：定义了审批流程(新建页面的 ||编辑时))
    //如果是停止中的审批流程需要再判断是否已经提交过审批，如果提交过审批则允许其继续审批
    //1:有没有定义审批流程
    glb_BillID = document.getElementById(FlowJS_HiddenIdentityID).value; //document.getElementById('txtIndentityID').value;
    var Action = "GetSet";
    var UrlParam = "Action=" + Action + "&BillTypeFlag=" + glb_BillTypeFlag + "&BillTypeCode=" + glb_BillTypeCode + "&BillID=" + glb_BillID;
    $.ajax({
        type: "GET",
        url: "../../../Handler/Common/Flow.ashx?" + UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            //popMsgObj.ShowMsg('获取流程状态时：请求发生错误');
        },
        success: function(data) {
            SetFlowButton_DisplayControl(data.sta);
        }
    });

    //End -逻辑判断

}

//根据是否定义了流程状态处理相关按钮 status:流程状态
function SetFlowButton_DisplayControl(status) {
    var glbBtn_IsShow_Tjsp = false; //蓝：提交审批
    var glbBtn_IsShow_Sp = false;  //蓝：审批
    var glbBtn_IsShow_Qr = false;  //蓝：确认
    var glbBtn_IsShow_Jd = false;  //蓝：结单
    var glbBtn_IsShow_Qxjd = false; //蓝：取消结单
    var glbBtn_IsShow_Cxsp = false; //蓝：撤消审批
    var glbBtn_IsShow_Record = false; //蓝：审批流程记录

    glb_BillID = document.getElementById(FlowJS_HiddenIdentityID).value; //document.getElementById('txtIndentityID').value;
    if (status == 2)//发布中的流程
    {
        //2:定义了审批流程且
        if (parseInt(glb_BillID) > 0)//编辑时
        {
            //Start========================================================================================================================================
            var Action = "Get";
            var UrlParam = "Action=" + Action +
                                   "&BillTypeFlag=" + glb_BillTypeFlag +
                                   "&BillTypeCode=" + glb_BillTypeCode +
                                   "&BillID=" + glb_BillID + "";
            $.ajax({
                type: "GET",
                url: "../../../Handler/Common/Flow.ashx?" + UrlParam,
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    //popMsgObj.ShowMsg('请求发生错误');
                },
                success: function(data) {
                    // [ 加 载 单 据 审 批 状 态 ]
                    // 0:未提交审批   
                    // 1:当前单据正在待审批  
                    // 2:当前单据正在审批中   
                    // 3:当前单据已经通过审批  
                    // 4:当前单据审批未通过，不能进行确认操作 
                    // 5:撤消审批
                    if (data.sta == 0) GetFlowButton_Content(true, false, false, false, false, false, false, glb_BillID);
                    if (data.sta == 1 || data.sta == 2) GetFlowButton_Content(false, true, false, false, false, true, true, glb_BillID);
                    if (data.sta == 3) GetFlowButton_Content(false, false, true, false, false, true, true, glb_BillID);
                    if (data.sta == 4) GetFlowButton_Content(true, false, false, false, false, false, true, glb_BillID);
                    if (data.sta == 5) GetFlowButton_Content(true, false, false, false, false, false, true, glb_BillID);
                    try { SetSaveButton_DisplayControl(data.sta); } catch (e) { } //此空方法返回当前单据审批状态值
                }
            });
            //End===========================================================================================================================================
        }
        else//新建时
        {
            GetFlowButton_Content(glbBtn_IsShow_Tjsp, glbBtn_IsShow_Sp, glbBtn_IsShow_Qr, glbBtn_IsShow_Jd, glbBtn_IsShow_Qxjd, glbBtn_IsShow_Cxsp, glbBtn_IsShow_Record, glb_BillID);
        }

    }
    else {
        if (parseInt(glb_BillID) > 0) {
            GetFlowButton_Content(glbBtn_IsShow_Tjsp, glbBtn_IsShow_Sp, true, glbBtn_IsShow_Jd, glbBtn_IsShow_Qxjd, glbBtn_IsShow_Cxsp, glbBtn_IsShow_Record, glb_BillID);
        }
        else {
            GetFlowButton_Content(glbBtn_IsShow_Tjsp, glbBtn_IsShow_Sp, glbBtn_IsShow_Qr, glbBtn_IsShow_Jd, glbBtn_IsShow_Qxjd, glbBtn_IsShow_Cxsp, glbBtn_IsShow_Record, glb_BillID);
        }
    }

}
function GetFlowButton_Content(glbBtn_IsShow_Tjsp, glbBtn_IsShow_Sp, glbBtn_IsShow_Qr, glbBtn_IsShow_Jd, glbBtn_IsShow_Qxjd, glbBtn_IsShow_Cxsp, glbBtn_IsShow_Record, billID) {
    var glbBtn_Src_Tjsp = '<img id="btnPageFlowApply" src="../../../images/Button/Main_btn_submission.jpg" alt="提交审批" onclick="objFlow.Fun_Show(false,' + billID + ')" style=\"cursor: pointer\" />';
    var glbBtn_Src_Tjsp_Un = '<img id="btnPageFlowApply" src="../../../images/Button/UnClick_tjsp.jpg" alt="提交审批" />';
    var glbBtn_Src_Sp = '<img id="btnPageFlowApprove" src="../../../images/Button/Main_btn_verification.jpg" alt="审批" onclick="objFlow.Fun_Show(true,' + billID + ')" style=\"cursor: pointer\" />';
    var glbBtn_Src_Sp_Un = '<img id="btnPageFlowApprove" src="../../../images/Button/UnClick_sp.jpg" alt="审批"/>';
    var glbBtn_Src_Cxsp = '<img id="btnPageFlowUnApprove" src="../../../images/Button/Button_cxsp.jpg" alt="撤消审批" onclick=\"objFlow.Fun_Update_FlowApproval(' + billID + ');\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Cxsp_Un = '<img id="btnPageFlowUnApprove" src="../../../images/Button/Button_ucxsp.jpg" alt="撤消审批" />';
    var glbBtn_Src_Qr = '<img id="btnPageFlowConfrim" src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认" onclick=\"objFlow.Get_BillStatus(' + billID + ')\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qr_Un = '<img id="btnPageFlowConfrim" src="../../../images/Button/UnClick_qr.jpg" alt="确认"/>';
    var glbBtn_Src_Qxqr = '<img id="btnPageFlowConfrimUn" src="../../../images/Button/btn_qxqr.jpg" alt="取消确认" onclick=\"Fun_UnConfirmOperate();\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qxqr_Un = '<img id="btnPageFlowConfrimUn" src="../../../images/Button/btn_uqxqr.jpg" alt="取消确认"/>';
    var glbBtn_Src_Jd = '<img id="btnPageFlowComplete" src="../../../images/Button/Main_btn_Invoice.jpg" alt="结单" onclick="Fun_CompleteOperate(true);" style=\"cursor: pointer\" />';
    var glbBtn_Src_Jd_Un = '<img id="btnPageFlowComplete" src="../../../images/Button/Button_jd.jpg" alt="结单"/>';
    var glbBtn_Src_Qxjd = '<img id="btnPageFlowCancle" src="../../../images/Button/Main_btn_qxjd.jpg" alt="取消结单" onclick="Fun_CompleteOperate(false);" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qxjd_Un = '<img id="btnPageFlowCancle" src="../../../images/Button/Button_qxjd.jpg" alt="取消结单" />';
    var glbBtn_Src_Record = '<img id="btnPageFlowRecord" src="../../../images/Button/btn_cklc.jpg" alt="审批流程操作记录" onclick="objFlow.Fun_Show_Record(' + billID + ');" style=\"cursor: pointer\" />';
    var glbBtn_Src_Record_Un = '<img id="btnPageFlowRecord" src="../../../images/Button/btn_ucklc.jpg" alt="审批流程操作记录" />';

    var storageCheck_Button = '<img id="btnStorageCheck" src="../../../images/Button/Button_kctz.jpg" alt="库存调整" onclick="storageCheck();" >';
    var storageCheck_Button_Un = '<img id="btnStorageCheck" src="../../../images/Button/Button_ukctz.jpg" alt="库存调整">'



    var glbBillStatus = document.getElementById(FlowJS_BillStatus).value; //document.getElementById('txtBillStatus').value;单证状态：单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
    var glbBtnCurrent_IsConfirm = true; //确认按钮是否显示（李裕松模块中不需要确认按钮）
    var glbBtn_IsShow_Qxqr = false;
    var glbBut_Src_Check = storageCheck_Button_Un;
    var IsCheck = document.getElementById("txtStorageCheck").value;  //指示是否进行库存调整 0：未调整 1：已调整

    var glbBut_Src_1 = '';
    var glbBut_Src_2 = '';
    var glbBut_Src_3 = '';
    var glbBut_Src_4 = '';
    var glbBut_Src_5 = '';
    var glbBut_Src_6 = '';
    var glbBtn_Src_7 = '';
    var glbBtn_Src_8 = '';

    if (glbBtn_IsShow_Qr) {
        if (glbBillStatus == 2) {
            if (IsCheck != "0" && IsCheck != "") {
                glbBtn_IsShow_Qr = false;
                glbBtn_IsShow_Jd = true;
                glbBut_Src_Check = storageCheck_Button_Un;
            }
            else {
                glbBtn_IsShow_Qr = false;
                glbBtn_IsShow_Jd = false;
                glbBut_Src_Check = storageCheck_Button;
            }
            glbBtn_IsShow_Qxqr = true;

        }
        if (glbBillStatus == 4) {
            glbBtn_IsShow_Qr = false;
            glbBtn_IsShow_Qxjd = true;
        }
        if (glbBillStatus != 1) {
            glbBtn_IsShow_Cxsp = false;
        }
    }
    glbBut_Src_1 = glbBtn_IsShow_Tjsp == true ? glbBtn_Src_Tjsp : glbBtn_Src_Tjsp_Un;
    glbBut_Src_2 = glbBtn_IsShow_Sp == true ? glbBtn_Src_Sp : glbBtn_Src_Sp_Un;
    glbBut_Src_3 = glbBtn_IsShow_Qr == true ? glbBtn_Src_Qr : glbBtn_Src_Qr_Un;
    glbBut_Src_4 = glbBtn_IsShow_Jd == true ? glbBtn_Src_Jd : glbBtn_Src_Jd_Un;
    glbBut_Src_5 = glbBtn_IsShow_Qxjd == true ? glbBtn_Src_Qxjd : glbBtn_Src_Qxjd_Un;
    glbBut_Src_6 = glbBtn_IsShow_Cxsp == true ? glbBtn_Src_Cxsp : glbBtn_Src_Cxsp_Un;
    glbBut_Src_7 = glbBtn_IsShow_Record == true ? glbBtn_Src_Record : glbBtn_Src_Record_Un;
    glbBut_Src_8 = glbBtn_IsShow_Qxqr == true ? glbBtn_Src_Qxqr : glbBtn_Src_Qxqr_Un;

    /*
    此处针对王玉贞开发模块，领料和退料处理 
    */
    try {
        if (typeof (FlowJS_Material) != 'undefined') {
            var theDate = document.getElementById(FlowJS_Material).value;
            if (theDate != null && strlen(cTrim(theDate, 0)) > 0) {
                glbBut_Src_8 = glbBtn_Src_Qxqr_Un;
            }
        }
    } catch (e) { }

    /*
    此处针对周军开发模块
    */
    try {
        if (typeof (FlowJS_FromSell) != 'undefined') {
            var theSellBillStatus = document.getElementById(FlowJS_FromSell).value;
            if (theSellBillStatus != null && strlen(cTrim(theSellBillStatus, 0)) > 0 && theSellBillStatus == '2') {
                glbBut_Src_1 = glbBtn_Src_Tjsp_Un;
                glbBut_Src_2 = glbBtn_Src_Sp_Un;
                glbBut_Src_3 = glbBtn_Src_Qr_Un;
                glbBut_Src_4 = glbBtn_Src_Jd_Un;
                glbBut_Src_5 = glbBtn_Src_Qxjd_Un;
                glbBut_Src_6 = glbBtn_Src_Cxsp_Un;
                glbBut_Src_7 = glbBtn_Src_Record_Un;
                glbBut_Src_8 = glbBtn_Src_Qxqr_Un;
            }
        }
        if (typeof (FlowJS_FromSellDetail) != 'undefined') {
            var theSellBillStatus = document.getElementById(FlowJS_FromSellDetail).value;
            if (theSellBillStatus != null && strlen(cTrim(theSellBillStatus, 0)) > 0 && theSellBillStatus == '3') {
                glbBut_Src_1 = glbBtn_Src_Tjsp_Un;
                glbBut_Src_2 = glbBtn_Src_Sp_Un;
                glbBut_Src_3 = glbBtn_Src_Qr_Un;
                glbBut_Src_4 = glbBtn_Src_Jd_Un;
                glbBut_Src_5 = glbBtn_Src_Qxjd_Un;
                glbBut_Src_6 = glbBtn_Src_Cxsp_Un;
                glbBut_Src_7 = glbBtn_Src_Record_Un;
                glbBut_Src_8 = glbBtn_Src_Qxqr_Un;
            }
        }
    } catch (e) { }


    try {
        if (glb_IsComplete) {
            document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_3 + glbBut_Src_Check + glbBut_Src_8 + glbBut_Src_4 + glbBut_Src_5 + glbBut_Src_7;
        }
        else {
            if (typeof (glbBtn_IsConfirm) == 'undefined') {
                glbBtnCurrent_IsConfirm = true;
            }
            else {
                glbBtnCurrent_IsConfirm = glbBtn_IsConfirm;
            }

            if (glbBtnCurrent_IsConfirm) {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_3 + glbBut_Src_8 + glbBut_Src_7;
            }
            else {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_7;
            }
        }
    }
    catch (e) { }


    /*************************************
    *根据调拨业务状态是否启用 结单 按钮 pdd
    **************************************/
    if (typeof (glbtn_IsClose) != "undefined") {
        var objStatus = document.getElementById("ddlBusiStatus").value;
        var objClose = document.getElementById("btnPageFlowComplete");
        if (objStatus != "4") {
            try {
                objClose.src = "../../../Images/Button/Button_jd.jpg";
                objClose.onclick = null;
            }
            catch (e)
                { }
        }
    }
}



var UpLoadFileUrl = "";
var DetailProductID = new Array();
window.onload = function() {
    Init();
}
//初始化
function Init() {
    var objAction = document.getElementById("txtAction");
    if (objAction.value == "ADD" || objAction.value == "") {
        GetExtAttr('officedba.StorageCheck', null);
        document.getElementById("tdUpLoadFile").innerHTML = "<a href=\"javascript:ShowUploadFile();\">上传附件</>";
        document.getElementById("imgBack").style.display = "none";
        GetFlowButton_DisplayControl();
    }
    else if (objAction.value == "EDIT") {
        document.getElementById("divTitle").innerHTML = "期末盘点单"
        document.getElementById("div_InNo_uc").style.display = "none";
        document.getElementById("div_InNo_Lable").style.display = "block";
        document.getElementById("imgBack").style.display = "block";
        getBaseInfo();
    }
    if (document.getElementById("txtIsBack").value == "1") {
        document.getElementById("imgBack").style.display = "block";
    }
    if ($("#HiddenMoreUnit").val() == "False") {
        $("#baseuint").hide();
        $("#basecount").hide();
    }
    else {
        $("#baseuint").show();
        $("#basecount").show();
    }

}
//上传回调函数
function AfterUploadFile(FileUrl, DocName) {
    UpLoadFileUrl = FileUrl;
    var FileInfo = UpLoadFileUrl.split("\\");
    var objActive = document.getElementById("tdUpLoadFile");
    objActive.innerHTML = "<div id='divDealAttachment'><a id='attachname' href=\"javascript:void(0);\" onclick=\"DownLoadFile();\">" + FileInfo[FileInfo.length - 1] + "</a>" +
                                  "&nbsp;<a href=\"#\" onclick=\"DealAttachment();\">删除附件</a></div>";
}


//点击添加行按钮

/*点击添加行按钮*/
function AddRow() {
    var tbl = document.getElementById("tblCheck");
    var LastRowID = document.getElementById("txtLastRowID");
    var LastSortNo = document.getElementById("txtLastSortNo");
    var RowID = parseInt(LastRowID.value);
    //添加行
    var row = tbl.insertRow(-1);
    row.id = "tr_Row_" + RowID;
    row.className = "newrow";

    //添加checkbox列
    var cellCheck = row.insertCell(0);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + RowID + "\" name=\"chk\" value=\"" + RowID + "\" onclick=\"subSelect();\" />";

    //序号
    var cellNo = row.insertCell(1);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"txtSortNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(LastSortNo.value) + "\"  name=\"rownum\" disabled=\"true\"/>";

    //物品编号
    var cellProductNo = row.insertCell(2);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" id=\"txtProductNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onclick=\"getProdcutList(" + RowID + ");\"  readonly />";

    //物品名称
    var cellProductName = row.insertCell(3);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\" id=\"txtProductName_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\" disabled=\"true\" />";


    /*增加批次列：2010.4.14*/
    var newBatchNotd = row.insertCell(4); //添加列:批次
    newBatchNotd.className = "cell";
    newBatchNotd.id = 'SignItem_TD_BatchNo_' + RowID;
    newBatchNotd.innerHTML = "<select style=' width:90%;' id='BatchNo_SignItem_TD_Text_" + RowID + "' />"; //添加列内容



    //规格
    var cellProdcutSpec = row.insertCell(5);
    cellProdcutSpec.className = "cell";
    cellProdcutSpec.innerHTML = "<input type=\"text\" id=\"txtProductSpec_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  disabled=\"true\" />";



    /*增加基本单位、基本数量列：2010.4.14*/
    var newBaseUnit = row.insertCell(6); //添加列:基本单位
    newBaseUnit.className = "cell";
    newBaseUnit.id = 'SignItem_TD_BaseUnit_' + RowID;
    newBaseUnit.innerHTML = "<input id='BaseUnit_SignItem_TD_Text_" + RowID + "' type='text'  class=\"tdinput\"  style='width:90%' readonly />"; ; //添加列内容

    var newBaseCount = row.insertCell(7); //添加列：基本数量
    newBaseCount.className = "cell";
    newBaseCount.id = 'SignItem_TD_BaseCount_' + RowID;
    newBaseCount.innerHTML = "<input id='BaseCount_SignItem_TD_Text_" + RowID + "' value='0.00' type='text'  class=\"tdinput\"  readonly style='width:90%' />"; ; //添加列内容

    if ($("#HiddenMoreUnit").val() == "False") {
        newBaseUnit.style.display = "none"
        newBaseCount.style.display = "none"
        //单位
        var cellProductUnit = row.insertCell(8);
        cellProductUnit.className = "cell";
        cellProductUnit.innerHTML = "<input type=\"text\" id=\"txtProductUnit_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";

    }
    else {
        $("#baseuint").show();
        $("#basecount").show();
        var newFitNametd = row.insertCell(8); //添加列:单位
        newFitNametd.className = "cell";
        newFitNametd.id = 'SignItem_TD_UnitID_' + RowID;
        newFitNametd.innerHTML = "<div id=\"unitdiv" + RowID + "\"></div>"; //添加列内容
    }



    if ($("#HiddenMoreUnit").val() == "False") {
        //成本单价
        var cellTransferPrice = row.insertCell(9);
        cellTransferPrice.className = "cell";
        cellTransferPrice.innerHTML = "<input type=\"text\" id=\"txtStandardCost_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\" />";
    }
    else {
        var newFitDesctd = row.insertCell(9); //添加列:成本单价(根据基本单价计算,隐藏基本单价，比率)
        newFitDesctd.className = "cell";
        newFitDesctd.id = 'SignItem_TD_UnitPrice_' + RowID;
        newFitDesctd.innerHTML = "<input type='hidden' id='baseprice_td" + RowID + "'><input type='hidden' id='BaseExRate" + RowID + "'><input  id='txtStandardCost_" + RowID + "'  type='text' class=\"tdinput\"  style='width:90%' readonly=\"readonly\" />"; //添加列内容
    }

    //现存数量
    var cellTransferCount = row.insertCell(10);
    cellTransferCount.className = "cell";
    cellTransferCount.innerHTML = "<input type='hidden' id=\"txtNowCount_Hidden" + RowID + "\"><input type=\"text\" id=\"txtNowCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  disabled=\"true\"/>";

    var point = $("#HiddenPoint").val();
    if ($("#HiddenMoreUnit").val() == "False") {
        //实盘数量
        var cellTransferCount = row.insertCell(11);
        cellTransferCount.className = "cell";
        cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtCheckCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  onblur=\"cCheckDetail(0," + RowID + ")\" onchange=\"Number_round(this,'" + point + "');\" />";

    }
    else {
        var newFitNametd = row.insertCell(11); //添加列:实盘数量(根据基本数量计算)
        newFitNametd.className = "tdinput";
        newFitNametd.id = 'SignItem_TD_ProductCount_' + RowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"txtCheckCount_" + RowID + "\" value='0.00' class=\"tdinput\"  style='width:90%' onblur=\"Number_round(this,'" + point + "');cCheckDetail(0," + RowID + ");TotalPrice_MoreUnit(this.id," + RowID + ");\"/>"; //添加列内容
    }
    //盈亏类型
    var cellCheckType = row.insertCell(12);
    cellCheckType.className = "cell";
    cellCheckType.style.width = "55px";
    cellCheckType.innerHTML = "<select id=\"ddlDiffType_" + RowID + "\"  disabled=\"true\"  ><option value=\"0\" >正常</option><option value=\"1\">盘盈</option><option value=\"2\">盘亏</option></select>";

    //差异量
    var cellTransferTotalPrice = row.insertCell(13);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "<input type='hidden' id=\"txtDiffCount_Hidden" + RowID + "\" ><input type=\"text\" id=\"txtDiffCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\"/>";


    //备注
    var cellTransferRemark = row.insertCell(14);
    cellTransferRemark.className = "cell";
    cellTransferRemark.innerHTML = "<input type=\"text\" id=\"txtCheckRemark_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  maxlength=\"100\" />";

    //移动行号与序号
    LastRowID.value = parseInt(LastRowID.value) + 1;
    LastSortNo.value = parseInt(LastSortNo.value) + 1;
}



//载入产品信息
function getProdcutList(rowid) {
    var OutStorageID = document.getElementById("ddlStorageID").value;
    if (OutStorageID == "-1") {
        popMsgObj.Show("基本信息|", "请选择调货仓库|");
        return;
    }
    var Para = "OutStorageID=" + OutStorageID + "&OutDeptID=&action=GETPUT";
    popProductInfoObj.ShowList(Para, rowid);
}




//选择物品控件物品填充明细信息
function fnSelectProductInfo(ID, ProductNo, ProductName, ProductSpec, NowCount, ProdcutUnitName
                            , ProductUnitID, StandardCost, rowid, IsBatchNo
                            , /*后面几个参数页面查看加载时用*/BatchNo, BatchNoF, ChenkCount, DiffCount
                            , UsedUnitID, UsedUnitCount, UsedPrice, ExRate, Remark, DiffType) {
    if (UsedUnitID == "") UsedUnitID = "0";
    if (StandardCost == "") StandardCost = "0.00";
    if (UsedUnitCount == "") UsedUnitCount = "0.00";
    if (UsedPrice == "") UsedPrice = "0.00";
    if (ExRate == "") ExRate = "0";
    if (NowCount != "") NowCount = parseFloat(NowCount).toFixed($("#HiddenPoint").val());
    if (StandardCost != "") StandardCost = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
    if (ChenkCount != "") ChenkCount = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val());
    if (DiffCount != "") DiffCount = parseFloat(DiffCount).toFixed($("#HiddenPoint").val());
    if (UsedUnitCount != "") UsedUnitCount = parseFloat(UsedUnitCount).toFixed($("#HiddenPoint").val());
    if (UsedPrice != "") UsedPrice = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());
    if (BatchNo == "无")//不是 页面加载时才判断
    {
        for (var i = 0; i < DetailProductID.length; i++) {
            if (ID == DetailProductID[i]) {
                popMsgObj.Show("盘点单明细|", "已添加该产品到盘点单明细|");
                return;
            }
        }
    }
    document.getElementById("txtProductNo_" + rowid).value = ProductNo;
    document.getElementById("txtProductNo_" + rowid).title = ID;
    document.getElementById("txtProductName_" + rowid).value = ProductName;
    document.getElementById("txtProductSpec_" + rowid).value = ProductSpec;
    //    document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
    //    document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
    //    document.getElementById("txtStandardCost_" + rowid).value = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
    //document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val());//现存数量
    document.getElementById("txtNowCount_Hidden" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量(隐藏)

    //绑定批次
    var ListControlID = "BatchNo_SignItem_TD_Text_" + rowid;
    var StorageControlID = "ddlStorageID";
    if (BatchNo == "undefined")
        GetBatchList(ID, StorageControlID, ListControlID, IsBatchNo);
    else
        GetBatchList(ID, StorageControlID, ListControlID, IsBatchNo, BatchNoF);
    //多计量单位
    if ($("#HiddenMoreUnit").val() == "True") {
        var BasePriceControl = "baseprice_td" + rowid;
        var BaseUnitControl = "BaseUnit_SignItem_TD_Text_" + rowid;
        $("#" + BaseUnitControl).val(ProdcutUnitName);
        $("#" + BaseUnitControl).attr("title", ProductUnitID);
        $("#" + BasePriceControl).val(StandardCost);

        if (BatchNo == "无")
            GetUnitGroupSelectEx(ID, "StockUnit", "txtProductUnit_" + rowid, "ChangeUnit(this.id," + rowid + "," + StandardCost + ")", "unitdiv" + rowid, '', "FillContent(" + rowid + "," + StandardCost + ")");
        else//页面查看时
        {
            GetUnitGroupSelectEx(ID, "StockUnit", "txtProductUnit_" + rowid, "ChangeUnit(this.id," + rowid + "," + StandardCost + ")", "unitdiv" + rowid, UsedUnitID, "LoadUnitContent(" + rowid + "," + StandardCost + ")");
            document.getElementById("txtStandardCost_" + rowid).value = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val()); //成本单价
            document.getElementById("txtCheckCount_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //实盘数量

            document.getElementById("txtDiffCount_" + rowid).value = parseFloat(DiffCount).toFixed($("#HiddenPoint").val()); //差异量
            document.getElementById("txtCheckRemark_" + rowid).value = Remark; //备注
            document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量
            document.getElementById("BaseUnit_SignItem_TD_Text_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //基本数量
            document.getElementById("BaseUnit_SignItem_TD_Text_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //基本数量
            document.getElementById("ddlDiffType_" + rowid).value = DiffType; //类型
        }
    } //
    else {
        document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
        document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
        document.getElementById("txtStandardCost_" + rowid).value = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
        document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量
        if (BatchNo != "无") {
            document.getElementById("txtStandardCost_" + rowid).value = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val()); //成本单价
            document.getElementById("txtCheckCount_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //实盘数量
            //document.getElementById("txtDiffCount_" + rowid).value = parseFloat(DiffCount).toFixed($("#HiddenPoint").val());//差异量
            document.getElementById("txtCheckRemark_" + rowid).value = Remark; //备注
            document.getElementById("ddlDiffType_" + rowid).value = DiffType; //类型
        }
    }
    closeProductInfodiv();

    /*将产品ID加到数组，用来验证*/
    DetailProductID.push(ID);
    //计算现有库存及现有金额
    cTotal();
    cCheckTotal();

}

//暂时不用
function LoadUnitContent(rowid, usedunit) {
    //   var exrate=$("#BaseExRate"+rowid).val();
    //   var usedunitvalue=usedunit+"|"+exrate;
    //   $("#UnitID_SignItem_TD_Text_"+rowid).val(usedunitvalue); /*比率*/
}

//本行小计，，数量变动时根据比率算出基本数量
function TotalPrice_MoreUnit(id, rowid) {
    try {
        var EXRate = $("#txtProductUnit_" + rowid).val().split('|')[1].toString(); /*比率*/
        var AcCount = $("#txtCheckCount_" + rowid).val(); /*实盘数量*/
        if (EXRate != "0") {
            document.getElementById('BaseCount_SignItem_TD_Text_' + rowid).value = (AcCount * EXRate).toFixed($("#HiddenPoint").val());

        }
    } catch (Error) { }
    //CountAll(rowid);
}
//选择单位时计算
function ChangeUnit(ObjID/*下拉列表控件（单位）*/, RowID/*行号*/, UnitPrice/*基本单价*/) {
    var EXRate = $("#txtProductUnit_" + RowID).val().split('|')[1].toString(); /*比率*/
    var ProductCount = $("#txtCheckCount_" + RowID).val(); /*实盘数量*/
    var NowCount_Hidden = "txtNowCount_Hidden" + RowID;
    var NowCount_ = "txtNowCount_" + RowID;


    var diffbasecount = document.getElementById("txtDiffCount_Hidden" + RowID).value;


    if (EXRate != '') {
        var tempcount = parseFloat(ProductCount * EXRate).toFixed($("#HiddenPoint").val());
        var tempprice = parseFloat(UnitPrice * EXRate).toFixed($("#HiddenPoint").val());
        $("#BaseCount_SignItem_TD_Text_" + RowID).val(tempcount); /*基本数量根据实盘数量和比率算出*/
        $("#txtStandardCost_" + RowID).val(tempprice); /*单价根据基本单价和比率算出*/
        //$("#TD_CostPriceTotal_"+RowID).val(parseFloat(ProductCount*tempprice).toFixed($("#HiddenPoint").val()));/*金额*/
        $("#" + NowCount_).val(document.getElementById(NowCount_Hidden).value / EXRate); //新的现有存量
        //document.getElementById("txtDiffCount_" + RowID).value = diffbasecount*EXRate;
        cCheckDetail(0, RowID);

    }
    //计算现有库存及现有金额
    cTotal();
    cCheckTotal();

}
/*点击物品后填充单价根据基本单价和比率*/
function FillContent(RowID, UnitPrice) {
    var EXRate = $("#txtProductUnit_" + RowID).val().split('|')[1].toString(); /*比率*/
    var NowCount_Hidden = "txtNowCount_Hidden" + RowID; //隐藏现有存量（根据现有存量和比率计算新的现有存量）
    var NowCount_ = "txtNowCount_" + RowID;
    if (EXRate != '') {
        var tempprice = parseFloat(UnitPrice * EXRate).toFixed($("#HiddenPoint").val());
        $("#txtStandardCost_" + RowID).val(tempprice); /*单价根据基本单价和比率算出*/
        $("#" + NowCount_).val(document.getElementById(NowCount_Hidden).value / EXRate); //新的现有存量
    }
    cCheckDetail(0, RowID);
    // CountAll(RowID);
}
//全选
function selall() {
    var ck = document.getElementsByName("chk");
    var Flag = document.getElementById("chkDetail").checked;
    for (var i = 0; i < ck.length; i++) {
        ck[i].checked = Flag;
    }
}
//子项单击
function subSelect() {
    var Flag = true;
    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        if (!ck[i].checked) {
            Flag = false;
        }
    }
    document.getElementById("chkDetail").checked = Flag;
}


//获取选中行的ROWID
function getCheckedList() {
    var ck = document.getElementsByName("chk");
    var IDList = new Array();
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked & document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            IDList.push(ck[i].value);
        }
    }
    return IDList;
}

//移除行
function RemoveRow() {
    var IDList = getCheckedList();
    if (IDList.length <= 0) {
        popMsgObj.Show("调拨明细|", "请先选中需要操作的行|");
        return;
    }
    var LastSortNo = document.getElementById("txtLastSortNo");
    if (confirm("确认删除选中的调拨明细嘛？")) {
        for (var i = 0; i < IDList.length; i++) {
            document.getElementById("tr_Row_" + IDList[i]).style.display = "none";
            Resort(IDList[i]);
            /*移除列表中的产品ID*/
            delProductID(document.getElementById("txtProductNo_" + IDList[i]).title);
        }
        var ck = document.getElementsByName("chk");
        var flag = true;
        for (var i = 0; i < ck.length; i++) {
            if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
                flag = false;
                break;
            }
        }
        if (document.getElementById("chkDetail").checked) {
            if (flag) {
                document.getElementById("chkDetail").checked = false;
            }
        }

    }
    cTotal();
    cCheckTotal();
}


/*删除数组中指定产品的ID*/
function delProductID(ID) {
    var tempArray = new Array();
    for (var i = 0; i < DetailProductID.length; i++) {
        if (DetailProductID[i] != ID) {
            tempArray.push(DetailProductID[i]);
        }
    }
    DetailProductID = tempArray;
}


//序号重新排序
function Resort(id) {
    var ck = document.getElementsByName("chk");
    var index = 1;
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            document.getElementById("txtSortNo_" + ck[i].value).value = index;
            index++;
        }
    }
    document.getElementById("txtLastSortNo").value = index;
}


//合计现存量与现存金额
function cTotal() {
    var ck = document.getElementsByName("chk");
    var TotalNowCount = 0;
    var TotalNowCost = 0;
    //var TotalCheckCount=0
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {

            var dNowCount = document.getElementById("txtNowCount_" + ck[i].value).value;
            var dNowCoust = document.getElementById("txtStandardCost_" + ck[i].value).value;
            TotalNowCount = TotalNowCount + parseFloat(dNowCount == "" ? "0" : dNowCount);
            TotalNowCost = TotalNowCost + parseFloat(dNowCoust == "" ? "1" : dNowCoust) * parseFloat(dNowCount == "" ? "0" : dNowCount);
        }
    }
    document.getElementById("txtNowCount").value = parseFloat(TotalNowCount).toFixed($("#HiddenPoint").val());
    document.getElementById("txtNowMoney").value = parseFloat(TotalNowCost).toFixed($("#HiddenPoint").val());
}

//计算盘点明细
function cCheckDetail(count, rowid) {
    var checkCount = document.getElementById("txtCheckCount_" + rowid).value;
    var DiffCount = parseFloat(document.getElementById("txtDiffCount_" + rowid).value).toFixed($("#HiddenPoint").val());

    var NowCount = parseFloat(document.getElementById("txtNowCount_" + rowid).value).toFixed($("#HiddenPoint").val());
    var DiffType = document.getElementById("ddlDiffType_" + rowid);
    if (document.getElementById("txtProductNo_" + rowid).value == "") {
        popMsgObj.Show("盘点明细|", "请选择需要盘点的产品|");
        document.getElementById("txtCheckCount_" + rowid).value = "";
        return;
    }


    //        if(checkCount=="")
    //        {
    //             popMsgObj.Show("盘点明细|","实盘数量不能为空|");
    //             return;
    //        }
    if (!IsNumOrFloat(checkCount, false) && parseInt(checkCount, 10) != 0) {
        popMsgObj.Show("盘点明细|", "实盘数量输入有误，请输入有效的数值（0或大于0的数值）！|");
        document.getElementById("txtCheckCount_" + rowid).value = parseFloat(count).toFixed($("#HiddenPoint").val());
        return;
    }
    document.getElementById("txtDiffCount_" + rowid).value = parseFloat(Math.abs((NowCount - parseFloat(checkCount)).toFixed($("#HiddenPoint").val()))).toFixed($("#HiddenPoint").val());
    document.getElementById("txtDiffCount_Hidden" + rowid).value = parseFloat(Math.abs((NowCount - parseFloat(checkCount)).toFixed($("#HiddenPoint").val()))).toFixed($("#HiddenPoint").val());

    if (parseFloat(checkCount) > parseFloat(NowCount))
        DiffType.value = "1";
    else if (parseFloat(checkCount) < parseFloat(NowCount))
        DiffType.value = "2";

    cCheckTotal();
}

//合计 实盘数量与金额 差异量与差异金额
function cCheckTotal() {
    var NowCount = parseFloat(document.getElementById("txtNowCount").value == "" ? "0" : document.getElementById("txtNowCount").value).toFixed($("#HiddenPoint").val());
    var NowMoney = parseFloat(document.getElementById("txtNowMoney").value == "" ? "0" : document.getElementById("txtNowMoney").value).toFixed($("#HiddenPoint").val());
    var CheckCount = 0; //document.getElementById("txtCheckCount");
    var CheckMoney = 0; //document.getElementById("txtCheckMoney");

    var DiffCount = document.getElementById("txtDiffCount");
    var DiffMoney = document.getElementById("txtDiffMoney");

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        var rowid = ck[i].value;
        if (document.getElementById("tr_Row_" + rowid).style.display != "none") {
            var dCheckCount = document.getElementById("txtCheckCount_" + rowid).value == "" ? "0" : document.getElementById("txtCheckCount_" + rowid).value;
            var dCheckMoney = document.getElementById("txtStandardCost_" + rowid).value == "" ? "1" : document.getElementById("txtStandardCost_" + rowid).value;
            CheckCount = parseFloat(CheckCount) + parseFloat(dCheckCount);
            CheckMoney = parseFloat(CheckMoney) + parseFloat(dCheckCount) * parseFloat(dCheckMoney);

        }

    }

    document.getElementById("txtCheckCount").value = parseFloat(CheckCount).toFixed($("#HiddenPoint").val());
    document.getElementById("txtCheckMoney").value = parseFloat(CheckMoney).toFixed($("#HiddenPoint").val());

    DiffCount.value = parseFloat(Math.abs(parseFloat((parseFloat(NowCount) - parseFloat(CheckCount))).toFixed($("#HiddenPoint").val()))).toFixed($("#HiddenPoint").val());
    DiffMoney.value = parseFloat(Math.abs(parseFloat(parseFloat(NowMoney) - parseFloat(CheckMoney)).toFixed($("#HiddenPoint").val()))).toFixed($("#HiddenPoint").val());

}



function ValidateInfo() {
    var msg = "";
    var title = "";
    var codeRule = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    var action = document.getElementById("txtAction").value;
    //如果选中的是 手工输入时，校验编号是否输入
    if (codeRule == "" && action != "EDIT") {
        //获取输入的编号
        var txtPlanNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        //编号必须输入
        if (txtPlanNo == "") {
            title = title + "单据编号|";
            msg = msg + "请输入单据编号|";
        }
        else {
            if (!CodeCheck(txtPlanNo)) {
                title = title + "单据编号|";
                msg = msg + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
    }


    //           if(document.getElementById("txtRuleCodeNo_ddlCodeRule").value=="")
    //           {
    //                 if(document.getElementById("txtRuleCodeNo_txtCode").value=="")
    //                 {
    //                     title=title+"编号|";
    //                     msg=msg+"请输入单据编号|";
    //                 }
    //            }
    //            if(cTrim(document.getElementById("txtTitle").value,0)=="")
    //            {
    ////                    title=title+"主题|";
    ////                    msg=msg+"请输入主题|";
    //            }
    if (cTrim(document.getElementById("txtTitle").value, 0) != "") {
        if (!CheckSpecialWord(document.getElementById("txtTitle").value)) {
            title = title + "主题|";
            msg = msg + "盘点单主题不能含有特殊字符|";
        }
    }

    if (document.getElementById("txtTransactor").value == "") {
        title = title + "经办人|";
        msg = msg + "请输入经办人|";
    }
    if (document.getElementById("txtDeptID").value == "") {
        title = title + "盘点部门|";
        msg = msg + "请输入盘点部门|";
    }
    if (document.getElementById("ddlStorageID").value == "-1") {
        title = title + "盘点仓库|";
        msg = msg + "请输入盘点仓库|";
    }
    if (document.getElementById("txtStartDate").value == "") {
        title = title + "盘点起始日期|";
        msg = msg + "请输入盘点起始日期|";
    }
    if (document.getElementById("txtEndDate").value == "") {
        title = title + "盘点结束日期|";
        msg = msg + "请输入盘点结束日期|";
    }
    if (document.getElementById("txtStartDate").value > document.getElementById("txtEndDate").value) {
        title = title + "盘点结束日期|";
        msg = msg + "盘点结束日期不能小于起始日期|";
    }
    if (cTrim(document.getElementById("tboxSummary").value) != "" && !CheckSpecialWord(document.getElementById("tboxSummary").value)) {
        title = title + "摘要|";
        msg = msg + "摘要中不能包含特殊字符|";
    }
    if (cTrim(document.getElementById("tboxRemark").value) != "" && !CheckSpecialWord(document.getElementById("tboxRemark").value)) {
        title = title + "备注|";
        msg = msg + "备注中不能包含特殊字符|";
    }
    if (strlen(document.getElementById("tboxSummary").value) > 200) {
        title = title + "摘要|";
        msg = msg + "摘要字符数不能大于200|"
    }
    if (strlen(document.getElementById("tboxRemark").value) > 800) {
        title = title + "备注|";
        msg = msg + "备注字符数不能大于800|";
    }
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        // isFlag = false;
        title = title + RetVal + "|";
        msg = msg + RetVal + "不能含有特殊字符|";
    }
    var ck = document.getElementsByName("chk");
    var FlagHas = false; //是否有明细
    var FlagFill = false;  //明细是否完整
    for (var i = 0; i < ck.length; i++) {
        if (document.getElementById("tr_Row_" + ck[i].value).style.display != "none") {
            FlagHas = true;
            if (document.getElementById("txtCheckCount_" + ck[i].value).value == "")
                FlagFill = true;
        }
    }
    if (!FlagHas) {
        title = title + "盘点单明细|";
        msg = msg + "盘点单明细不能为空|";
    }
    if (FlagFill) {
        title = title + "盘点单明细|";
        msg = msg + "至少有一条盘点单明细没有填写完整|";
    }
    if (msg != "" && title != "") {
        popMsgObj.Show(title, msg);
        return false;
    }
    return true;

}



//保存数据
function Save() {
    if (!ValidateInfo())
        return;
    var objAction = document.getElementById("txtAction").value;
    if (objAction == "ADD") {
        Add();
    }
    else if (objAction == "EDIT") {
        Edit();
    }
}

function Add() {
    var CheckNo = "";
    var bmgz = "";
    if (document.getElementById("txtRuleCodeNo_ddlCodeRule").value == "") {
        CheckNo = document.getElementById("txtRuleCodeNo_txtCode").value;
        if (CheckNo == "") {
            popMsgObj.Show("编号|", "请输入单据编号");
            return;
        }
        else
            bmgz = "sd";
    }
    else {
        bmgz = "zd";
        CheckNo = document.getElementById("txtRuleCodeNo_ddlCodeRule").value;
    }

    var para = "CheckNo=" + CheckNo +
                        "&bmgz=" + bmgz +
                        "&Title=" + escape(document.getElementById("txtTitle").value) +
                        "&CheckStartDate=" + document.getElementById("txtStartDate").value +
                        "&CheckEndDate=" + document.getElementById("txtEndDate").value +
                        "&StorageID=" + document.getElementById("ddlStorageID").value +
                        "&DeptID=" + document.getElementById("txtDeptID").value +
                        "&Transactor=" + document.getElementById("txtTransactor").value +
                        "&NowCount=" + document.getElementById("txtNowCount").value +
                        "&CheckCount=" + document.getElementById("txtCheckCount").value +
                        "&DiffCount=" + document.getElementById("txtDiffCount").value +
                        "&NowMoney=" + document.getElementById("txtNowMoney").value +
                        "&CheckMoney=" + document.getElementById("txtCheckMoney").value +
                        "&DiffMoney=" + document.getElementById("txtDiffMoney").value +
                        "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                        "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                        "&Attachment=" + escape(UpLoadFileUrl) +
                        "&action=ADD" +
                        "&CheckType=" + document.getElementById("ddlCheckType").value;
    var SortNo = new Array();
    var ProductID = new Array();
    var UnitID = new Array(); //实际单位
    var NowCount = new Array();
    var CheckCount = new Array(); //实际数量
    var DiffCount = new Array();
    var DiffType = new Array();
    var RemarkList = new Array();

    var DetailBaseUnitID = new Array(); //基本单位
    var DetailBaseCount = new Array(); //基本数量
    var DetailBasePrice = new Array(); //基本单价
    var DetailExtRate = new Array(); //比率
    var DetailBatchNo = new Array(); //批次
    var DetailCostPrice = new Array(); //单价


    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        var rowid = ck[i].value;
        if (document.getElementById("tr_Row_" + rowid).style.display != "none") {
            var objBaseUnitID = 'BaseUnit_SignItem_TD_Text_' + (rowid); //基本单位
            var objBaseCount = 'BaseCount_SignItem_TD_Text_' + (rowid); //基本数量
            var objBasePrice = 'baseprice_td' + (rowid); //基本单价
            var objExtRate = 'UnitID_SignItem_TD_Text_' + (rowid); //比率
            var objBatchNo = 'BatchNo_SignItem_TD_Text_' + (rowid); //批次
            var objUnit = 'txtProductUnit_' + rowid; //单位
            var objPrice = 'txtStandardCost_' + rowid; //单价

            SortNo.push(document.getElementById("chk_list_" + rowid).value);
            ProductID.push(document.getElementById("txtProductNo_" + rowid).title);
            //UnitID.push(document.getElementById("txtProductUnit_" + rowid).title);
            NowCount.push(document.getElementById("txtNowCount_" + rowid).value);
            //CheckCount.push(document.getElementById("txtCheckCount_" + rowid).value);
            DiffCount.push(document.getElementById("txtDiffCount_" + rowid).value);
            DiffType.push(document.getElementById("ddlDiffType_" + rowid).value);
            RemarkList.push(escape(document.getElementById("txtCheckRemark_" + rowid).value));

            if ($("#HiddenMoreUnit").val() == "False")//未启用时(实际使用的存入基本单位中，单价没有存储)
            {
                DetailBaseUnitID.push(document.getElementById(objUnit).title); //单位                
                DetailBaseCount.push(document.getElementById("txtCheckCount_" + rowid).value); //数量
                //DetailBasePrice.push(theCostPrice);//单价
            }
            else {
                DetailBaseUnitID.push(document.getElementById(objBaseUnitID.toString()).title); //基本单位                
                DetailBaseCount.push(document.getElementById(objBaseCount.toString()).value); //基本数量
                DetailBasePrice.push(document.getElementById(objBasePrice.toString()).value); //基本单价

                UnitID.push(document.getElementById(objUnit.toString()).value.split('|')[0].toString()); //实际单位ID  
                CheckCount.push(document.getElementById("txtCheckCount_" + rowid).value); //实际数量 
                DetailCostPrice.push(document.getElementById(objPrice).value); //实际单价
                DetailExtRate.push(document.getElementById(objUnit.toString()).value.split('|')[1].toString()); //比率
            }
            DetailBatchNo.push(document.getElementById(objBatchNo.toString()).value); //批次
        }
    }

    para = para +
                  "&SortNos=" + SortNo.toString() +
                  "&ProductIDs=" + ProductID.toString() +
                  "&UnitIDs=" + UnitID.toString() +
                  "&NowCounts=" + NowCount.toString() +
                  "&CheckCounts=" + CheckCount.toString() +
                  "&DiffCounts=" + DiffCount.toString() +
                  "&DiffTypes=" + DiffType.toString() +
                  "&DetailBaseUnitID=" + escape(DetailBaseUnitID.toString()) + //
                  "&DetailBaseCount=" + escape(DetailBaseCount.toString()) + //
                  "&DetailCostPrice=" + DetailCostPrice.toString() +
                  "&DetailExtRate=" + escape(DetailExtRate.toString()) + //
                  "&DetailBatchNo=" + escape(DetailBatchNo.toString()) + //
                  "&RemarkList=" + RemarkList.toString() + GetExtAttrValue();

    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {

            var Flag = msg.split('|');
            if (Flag[0] == "1") {
                popMsgObj.Show("保存信息|", "保存成功|");
                document.getElementById("div_InNo_uc").style.display = "none";
                document.getElementById("div_InNo_Lable").style.display = "block";
                document.getElementById("txtNo").value = Flag[1].split('#')[1];
                document.getElementById("txtCheckID").value = Flag[1].split('#')[0];
                document.getElementById("txtAction").value = "EDIT";
                GetFlowButton_DisplayControl();
                GetCurrentInfo(3);
            }
            else if (Flag[0] == "2")
                popMsgObj.Show("保存信息|", "该编号已被使用，请输入未使用的编号！|");
            else if (Flag[0] == "6")
                popMsgObj.Show("保存信息|", "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置|");
            else
                popMsgObj.Show("保存信息|", "保存失败|");

        },
        error: function() { popMsgObj.Show("保存|", "保存失败|"); }
    });

}
//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    try {
        $("#ExtField1").val(data.ExtField1);
        $("#ExtField2").val(data.ExtField2);
        $("#ExtField3").val(data.ExtField3);
        $("#ExtField4").val(data.ExtField4);
        $("#ExtField5").val(data.ExtField5);
        $("#ExtField6").val(data.ExtField6);
        $("#ExtField7").val(data.ExtField7);
        $("#ExtField8").val(data.ExtField8);
        $("#ExtField9").val(data.ExtField9);
        $("#ExtField10").val(data.ExtField10);
    }
    catch (Error) { }
}
function Edit() {
    //alet(1);
    var para = "CheckNo=" + document.getElementById("txtNo").value +
                        "&Title=" + escape(document.getElementById("txtTitle").value) +
                        "&CheckStartDate=" + document.getElementById("txtStartDate").value +
                        "&CheckEndDate=" + document.getElementById("txtEndDate").value +
                        "&StorageID=" + document.getElementById("ddlStorageID").value +
                        "&DeptID=" + document.getElementById("txtDeptID").value +
                        "&Transactor=" + document.getElementById("txtTransactor").value +
                        "&NowCount=" + document.getElementById("txtNowCount").value +
                        "&CheckCount=" + document.getElementById("txtCheckCount").value +
                        "&DiffCount=" + document.getElementById("txtDiffCount").value +
                        "&NowMoney=" + document.getElementById("txtNowMoney").value +
                        "&CheckMoney=" + document.getElementById("txtCheckMoney").value +
                        "&DiffMoney=" + document.getElementById("txtDiffMoney").value +
                        "&Summary=" + escape(document.getElementById("tboxSummary").value) +
                        "&Remark=" + escape(document.getElementById("tboxRemark").value) +
                        "&Attachment=" + escape(UpLoadFileUrl) +
                        "&action=EDIT" +
                        "&CheckID=" + document.getElementById("txtCheckID").value +
                         "&CheckType=" + document.getElementById("ddlCheckType").value;


    var SortNo = new Array();
    var ProductID = new Array();
    var UnitID = new Array(); //实际单位
    var NowCount = new Array();
    var CheckCount = new Array(); //实际数量
    var DiffCount = new Array();
    var DiffType = new Array();
    var RemarkList = new Array();

    var DetailBaseUnitID = new Array(); //基本单位
    var DetailBaseCount = new Array(); //基本数量
    var DetailBasePrice = new Array(); //基本单价
    var DetailExtRate = new Array(); //比率
    var DetailBatchNo = new Array(); //批次
    var DetailCostPrice = new Array(); //单价

    var ck = document.getElementsByName("chk");
    for (var i = 0; i < ck.length; i++) {
        var rowid = ck[i].value;
        if (document.getElementById("tr_Row_" + rowid).style.display != "none") {

            var objBaseUnitID = 'BaseUnit_SignItem_TD_Text_' + (rowid); //基本单位
            var objBaseCount = 'BaseCount_SignItem_TD_Text_' + (rowid); //基本数量
            var objBasePrice = 'baseprice_td' + (rowid); //基本单价
            var objExtRate = 'UnitID_SignItem_TD_Text_' + (rowid); //比率
            var objBatchNo = 'BatchNo_SignItem_TD_Text_' + (rowid); //批次
            var objUnit = 'txtProductUnit_' + rowid; //单位
            var objPrice = 'txtStandardCost_' + rowid; //单价

            SortNo.push(document.getElementById("chk_list_" + rowid).value);
            ProductID.push(document.getElementById("txtProductNo_" + rowid).title);
            //UnitID.push(document.getElementById("txtProductUnit_" + rowid).title);
            NowCount.push(document.getElementById("txtNowCount_" + rowid).value);
            // CheckCount.push(document.getElementById("txtCheckCount_" + rowid).value);
            DiffCount.push(document.getElementById("txtDiffCount_" + rowid).value);
            DiffType.push(document.getElementById("ddlDiffType_" + rowid).value);
            RemarkList.push(escape(document.getElementById("txtCheckRemark_" + rowid).value));

            if ($("#HiddenMoreUnit").val() == "False")//未启用时(实际使用的存入基本单位中，单价没有存储)
            {
                DetailBaseUnitID.push(document.getElementById(objUnit).title); //单位                
                DetailBaseCount.push(document.getElementById("txtCheckCount_" + rowid).value); //数量
                //DetailBasePrice.push(theCostPrice);//单价
            }
            else {
                DetailBaseUnitID.push(document.getElementById(objBaseUnitID.toString()).title); //基本单位                
                DetailBaseCount.push(document.getElementById(objBaseCount.toString()).value); //基本数量
                DetailBasePrice.push(document.getElementById(objBasePrice.toString()).value); //基本单价

                UnitID.push(document.getElementById(objUnit.toString()).value.split('|')[0].toString()); //实际单位ID  
                CheckCount.push(document.getElementById("txtCheckCount_" + rowid).value); //实际数量 
                DetailCostPrice.push(document.getElementById(objPrice).value); //实际单价
                DetailExtRate.push(document.getElementById(objUnit.toString()).value.split('|')[1].toString()); //比率
            }
            DetailBatchNo.push(document.getElementById(objBatchNo.toString()).value); //批次
        }
    }

    para = para +
                  "&SortNos=" + SortNo.toString() +
                  "&ProductIDs=" + ProductID.toString() +
                  "&UnitIDs=" + UnitID.toString() +
                  "&NowCounts=" + NowCount.toString() +
                  "&CheckCounts=" + CheckCount.toString() +
                  "&DiffCounts=" + DiffCount.toString() +
                  "&DiffTypes=" + DiffType.toString() +
                  "&DetailBaseUnitID=" + escape(DetailBaseUnitID.toString()) + //
                  "&DetailBaseCount=" + escape(DetailBaseCount.toString()) + //
                  "&DetailCostPrice=" + DetailCostPrice.toString() +
                  "&DetailExtRate=" + escape(DetailExtRate.toString()) + //
                  "&DetailBatchNo=" + escape(DetailBatchNo.toString()) + //
                  "&RemarkList=" + RemarkList.toString() + GetExtAttrValue();

    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var Flag = msg.split('|');
            if (Flag[0] == "1") {
                popMsgObj.Show("保存信息|", "保存成功|");
                GetFlowButton_DisplayControl();
                GetCurrentInfo(3);
            }
            else if (Flag[0] == "2")
                popMsgObj.Show("保存信息|", Flag[1] + "|");
            else
                popMsgObj.Show("保存信息|", "保存失败|");

        },
        error: function() { popMsgObj.Show("保存|", "保存失败|"); }
    });
}



//修改单据状态和业务状态 type [1 确认] [2 结单] [3 取消结单]
function UpdateStatus(type) {
    var para = "action=STA&type=" + type + "&ID=" + document.getElementById("txtCheckID").value +
                        "&CheckNo=" + document.getElementById("txtNo").value;
    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {

            if (msg.split('|')[0] == "1") {
                if (type == 1) {
                    popMsgObj.Show("单据操作|", "确认单据成功|");
                    document.getElementById("ddlBillStatus").value = "2";
                    GetFlowButton_DisplayControl();
                    GetCurrentInfo(1);
                }
                else if (type == 2) {
                    popMsgObj.Show("单据操作|", "结单成功|");
                    document.getElementById("ddlBillStatus").value = "4";
                    GetFlowButton_DisplayControl();
                    GetCurrentInfo(2);
                }
                else if (type == 3) {
                    popMsgObj.Show("单据操作|", "取消结单成功|");
                    document.getElementById("ddlBillStatus").value = "2";
                    GetFlowButton_DisplayControl();
                    GetCurrentInfo(3)
                }
                else if (type == 4) {
                    popMsgObj.Show("单据操作|", "取消确认成功|");
                    document.getElementById("ddlBillStatus").value = "1";
                    document.getElementById("tboxConfirmor").value = "";
                    document.getElementById("tboxConfirmorDate").value = "";
                    GetFlowButton_DisplayControl();
                }
                else
                    popMsgObj.Show("单据操作|", "单据操作失败|");
            }
            else
                popMsgObj.Show("单据操作|", "单据操作失败|");


        },
        error: function() { popMsgObj.Show("单据操作|", "单据操作失败|"); }
    });

}

//单据确认
function Fun_ConfirmOperate() {
    if (confirm('是否确认单据？'))
        UpdateStatus(1);
}

function Fun_CompleteOperate(flag) {
    if (flag)
        if (confirm('是否结单？'))
        UpdateStatus(2);
    if (!flag)
        if (confirm('是否取消结单？'))
        UpdateStatus(3);
}

//执行库存调整
function storageCheck() {
    var para = "action=SCK&ID=" + document.getElementById("txtCheckID").value +
                            "&CheckNo=" + document.getElementById("txtNo").value;
    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "string", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            if (msg == "0") {
                popMsgObj.Show("库存调整|", "库存调整成功|");
                document.getElementById("txtStorageCheck").value = "1";
                GetFlowButton_DisplayControl();
                document.getElementById("txtChecker").value = document.getElementById("txtCurrentUserName").value;
                document.getElementById("txtCheckDate").value = document.getElementById("txtCurrentDate").value;
                GetCurrentInfo(3);
            }
            else {
                popMsgObj.Show("库存调整|", "库存调整失败|");
            }
        },
        error: function() { popMsgObj.Show("库存调整|", "库存调整失败|"); }
    });
}

function getBaseInfo() {
    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    var para = "action=GET&CheckID=" + document.getElementById("txtCheckID").value;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            GetExtAttr('officedba.StorageCheck', msg.data[0]);  //获取扩展属性并填充值 
            var data = msg.data[0];
            document.getElementById("txtNo").value = data.CheckNo;
            document.getElementById("txtTitle").value = data.Title;
            document.getElementById("UserTransactor").value = data.TransactorName;
            document.getElementById("txtTransactor").value = data.Transactor;
            document.getElementById("DeptDept").value = data.DeptName;
            document.getElementById("txtDeptID").value = data.DeptID;
            document.getElementById("ddlStorageID").value = data.StorageID;
            document.getElementById("ddlCheckType").value = data.CheckType;
            document.getElementById("txtStartDate").value = data.CheckStartDate;
            document.getElementById("txtEndDate").value = data.CheckEndDate;
            document.getElementById("tboxSummary").value = data.Summary;

            document.getElementById("txtNowCount").value = parseFloat(data.NowCount).toFixed($("#HiddenPoint").val());
            document.getElementById("txtCheckCount").value = parseFloat(data.CheckCount).toFixed($("#HiddenPoint").val());
            // alert(document.getElementById("txtCheckCount").value);
            document.getElementById("txtDiffCount").value = parseFloat(data.DiffCount).toFixed($("#HiddenPoint").val());
            document.getElementById("txtNowMoney").value = parseFloat(data.NowMoney).toFixed($("#HiddenPoint").val());
            document.getElementById("txtCheckMoney").value = parseFloat(data.CheckMoney).toFixed($("#HiddenPoint").val());
            document.getElementById("txtDiffMoney").value = parseFloat(data.DiffMoney).toFixed($("#HiddenPoint").val());
            document.getElementById("tboxCreator").value = data.CreatorName;
            document.getElementById("ddlBillStatus").value = data.BillStatus;
            document.getElementById("tboxRemark").value = data.Remark;
            if (document.getElementById("ddlBillStatus").value != "1") {
                document.getElementById("tboxConfirmor").value = data.ConfirmorName;
                document.getElementById("tboxConfirmorDate").value = data.ConfirmDate;
            }
            else {
                document.getElementById("tboxConfirmor").value = "";
                document.getElementById("tboxConfirmorDate").value = "";
            }
            document.getElementById("tboxCloser").value = data.CloserName;
            document.getElementById("tboxCloseDate").value = data.CloseDate;
            document.getElementById("tboxModifiedUser").value = data.ModifiedUserID;
            document.getElementById("tboxModifiedDate").value = data.ModifiedDate;
            document.getElementById("txtStorageCheck").value = data.CheckUserID;
            document.getElementById("txtChecker").value = data.CheckUserName;
            document.getElementById("txtCheckDate").value = data.CheckDate;
            UpLoadFileUrl = data.Attachment;
            var objActive = document.getElementById("tdUpLoadFile");
            if (UpLoadFileUrl == "") {

                objActive.innerHTML = "<a href=\"javascript:ShowUploadFile();\" href=\"javascript:void(0);\">上传附件</>";
            }
            else {
                var fileInfo = UpLoadFileUrl.split("\\");
                objActive.innerHTML = "<div id=\"divDealAttachment\" runat=\"server\">" +
                                               "<a id='attachname' href=\"javascript:void(0);\" onclick=\"DownLoadFile();\">" + fileInfo[fileInfo.length - 1] + "</a>" +
                                               "&nbsp;<a href=\"#\"" +
                                                 "   onclick=\"DealAttachment();\">删除附件</a>" +
                                          "</div>";

            }

            getDetailInfo();

        },
        error: function() { popMsgObj.Show("载入信息|", "载入基本信息出错|"); }
    });
}

function DownLoadFile() {
    window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + UpLoadFileUrl, "_blank");
}
//删除附件
function DealAttachment() {
    var FilePath = UpLoadFileUrl;
    var FileName = document.getElementById("attachname").innerHTML;
    DeleteUploadFile(FilePath, FileName);
}

function getDetailInfo() {
    var url = "../../../Handler/Office/StorageManager/StorageCheckSave.ashx";
    var para = "action=GETDTL&CheckNo=" + document.getElementById("txtNo").value;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //
        url: url, //目标地址
        cache: false,
        data: para, //数据
        beforeSend: function() { },
        success: function(msg) {
            var index = 1;
            $("#tblCheck tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item != null && item != "") {
                    //alert(item.IsBatchNo);
                    DetailProductID.push(item.ProductID);
                    //                    $("<tr class='newrow' id=\"tr_Row_" + item.SortNo + "\"></tr>").append(
                    //                          "<td class=\"cell\" align=\"center\"><input type=\"checkbox\" id=\"chk_list_" + item.SortNo + "\" name=\"chk\" value=\"" + item.SortNo + "\" onclick=\"subSelect();\" /></td>" +
                    //                          "<td class=\"cell\"><input type=\"text\" id=\"txtSortNo_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.SortNo + "\"  name=\"rownum\"  disabled=\"true\"/></td>" +
                    //                          "<td class=\"cell\"><input type=\"text\" id=\"txtProductNo_" + item.SortNo + "\" title=\"" + item.ProductID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProdNo + "\"  onclick=\"getProdcutList(" + item.SortNo + ");\"  readonly /></td>" +
                    //                          "<td class=\"cell\"><input type=\"text\" id=\"txtProductName_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.ProductName + "\" disabled=\"true\"/></td>" +
                    //                           "<td class=\"cell\"><input type=\"text\" id=\"txtProductSpec_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.Specification + "\"  disabled=\"true\" /></td>" +
                    //                          "<td class=\"cell\"><input type=\"text\" id=\"txtProductUnit_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.UnitName + "\"   disabled=\"true\"  title=\"" + item.UnitID + "\"/></td>" +
                    //                           "<td class=\"cell\"><input type=\"text\" id=\"txtStandardCost_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.StandardCost).toFixed(2) + "\"   disabled=\"true\" /></td>" +
                    //                            "<td class=\"cell\"><input type=\"text\" id=\"txtNowCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.NowCount).toFixed(2) + "\"  disabled=\"true\"/></td>" +
                    //                            "<td class=\"cell\"><input type=\"text\" id=\"txtCheckCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.CheckCount).toFixed(2) + "\"  onblur=\"cCheckDetail(" + item.CheckCount + "," + item.SortNo + ")\" /></td>" +
                    //                            "<td class=\"cell\" width=\"50px\">" + getDiffType(item) + "</td>" +
                    //                            "<td class=\"cell\"><input type=\"text\" id=\"txtDiffCount_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseFloat(item.DiffCount).toFixed(2) + "\"   disabled=\"true\"/></td>" +
                    //                            "<td class=\"cell\"><input type=\"text\" id=\"txtCheckRemark_" + item.SortNo + "\" class=\"tdinput tboxsize textAlign\" value=\"" + item.Remark + "\"  maxlength=\"100\" /></td>").appendTo($("#tblCheck tbody")); index++;



                    AddRow();
                    var LastRowID = document.getElementById("txtLastRowID");
                    var RowID = parseInt(LastRowID.value - 1);
                    fnSelectProductInfo(item.ProductID, item.ProdNo, item.ProductName, item.Specification, item.NowCount, item.UnitName, item.UnitID, item.StandardCost, RowID, item.IsBatchNo, item.BatchNo, item.BatchNo, item.CheckCount, item.DiffCount, item.UsedUnitID, item.UsedUnitCount, item.UsedPrice, item.ExRate, item.Remark, item.DiffType);
                    GetFlowButton_DisplayControl();
                }
            });
            document.getElementById("txtLastRowID").value = index;
            document.getElementById("txtLastSortNo").value = index;
            //oSaveButton();

        },
        error: function() { popMsgObj.Show("载入信息|", "载入明细失败|"); }
    });
}


function getDiffType(item) {
    if (item.DiffType == "0")
        return "<select id=\"ddlDiffType_" + item.SortNo + "\"  disabled=\"true\" ><option value=\"0\" selected>正常</option><option value=\"1\">盘盈</option><option value=\"2\">盘亏</option></select>";
    else if (item.DiffType == "1")
        return "<select id=\"ddlDiffType_" + item.SortNo + "\"  disabled=\"true\" ><option value=\"0\" >正常</option><option value=\"1\" selected>盘盈</option><option value=\"2\">盘亏</option></select>";
    else if (item.DiffType == "2")
        return "<select id=\"ddlDiffType_" + item.SortNo + "\"  disabled=\"true\" ><option value=\"0\" >正常</option><option value=\"1\" >盘盈</option><option value=\"2\" selected>盘亏</option></select>";

}


/*************************************************************
*即时更新 最后更新人 结单人 确认人 及时间
*type 1:确认操作 2：结单操作  其他为修改
*************************************************************/
function GetCurrentInfo(type) {
    var currentUsetID = document.getElementById("txtCurrentUserID").value;
    var currentDate = document.getElementById("txtCurrentDate").value;
    var currentUserName = document.getElementById("txtCurrentUserName").value;

    var objModifiedUser = document.getElementById("tboxModifiedUser");
    var objModifiedDate = document.getElementById("tboxModifiedDate");
    if (type == 1) {
        document.getElementById("tboxConfirmor").value = currentUserName;
        document.getElementById("tboxConfirmorDate").value = currentDate;
    }
    else if (type == 2) {
        document.getElementById("tboxCloser").value = currentUserName;
        document.getElementById("tboxCloseDate").value = currentDate;
    }

    objModifiedUser.value = currentUsetID;
    objModifiedDate.value = currentDate;
}


/**************************************
*返回列表
**************************************/
function backtolsit() {
    window.history.back(-1);
}

/************************************
*根据 单据状态 和审批状态 操作 保存按钮
************************************/
function oSaveButton() {
    var objSave = document.getElementById("imgSave");
    var objUnSave = document.getElementById("imgUnSave");
    if (document.getElementById("ddlBillStatus").value != "1") {
        objSave.style.display = "none";
        objUnSave.style.display = "block";
    }
    else {
        if (document.getElementById("txtFlowStatus").value != "0") {
            objSave.style.display = "none";
            objUnSave.style.display = "block";
        }
    }
}


/*清除明细 并且将标志位初始化*/
function clearDetail() {
    /*清空保存产品ID的列表*/
    DetailProductID.length = 0;
    /*清楚所有明细*/
    $("#tblCheck tbody").find("tr.newrow").remove();
    CloseBarCodeDiv();

    /*初始化行号与序号*/
    document.getElementById("txtLastRowID").value = 0;
    document.getElementById("txtLastSortNo").value = 1;
}

/*取消确认*/
function Fun_UnConfirmOperate() {

    if (document.getElementById("txtStorageCheck").value != "0" && document.getElementById("txtStorageCheck").value != "") {
        popMsgObj.Show("单据操作|", "该调拨单已经执行库存调整操作，不能取消确认|");
        return;
    }
    if (confirm("是否执行取消确认操作？")) {
        UpdateStatus(4);
    }

}

/*根据单据状态和审批状态 是否启用保存按钮*/
function SetSaveButton_DisplayControl(flowStatus) {
    //流程状态：0：待提交   1：待审批   2：审批中   3：审批通过     4：审批不通过   5：撤销审批
    //制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
    //制单状态且审批状态“撤销审批”、“ 审批拒绝”状态的可以进行修改
    //变更和手工结单的不可以修改
    var PageBillID = document.getElementById('txtCheckID').value;
    var PageBillStatus = document.getElementById('ddlBillStatus').value;
    var objSave = document.getElementById("imgSave");
    var objUnSave = document.getElementById("imgUnSave");
    if (PageBillID != "") {

        if (PageBillStatus == '2' || PageBillStatus == '3' || PageBillStatus == '4') {
            objSave.style.display = "none";
            objUnSave.style.display = "block";
        }
        else {
            if (PageBillStatus == 1 && (flowStatus == 1 || flowStatus == 2 || flowStatus == 3)) {
                //单据状态+审批状态：制单状态且审批状态为“待审批”、“ 审批中”、“审批通过”的单据不能修改
                objSave.style.display = "none";
                objUnSave.style.display = "block";
            }
            else {
                objSave.style.display = "block";
                objUnSave.style.display = "none";
            }
        }
    }
}
/*打印*/
function BillPrint() {
    var ID = document.getElementById("txtCheckID").value;
    var No = document.getElementById("txtNo").value;
    if (ID == "" || ID == "0" || No == "") {
        popMsgObj.Show("打印|", "请保存单据再打印|");
        return;
    }
    //if(confirm("请确认您的单据已经保存？"))
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageCheckPrint.aspx?ID=" + ID + "&No=" + No);
}


/*-----------------------------------------------------条码扫描Start-----------------------------------------------------------------*/

var rerowID = "";
//判断是否有相同记录有返回true，没有返回false
function IsExist(prodNo) {
    var signFrame = document.getElementById("tblCheck");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("txtProductNo_" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            rerowID = i;
            return true;
        }
    }
    return false;
}

//条码扫描方法
function GetGoodsDataByBarCode(id, prodNo, prodName,
                                a, unitID, codeName,
                                b, c, d,
                                specification, e, f,
                                g, h, i,
                                StandardCost, IsBatchNo, BatchNo, NowCount) {
    if (!IsExist(prodNo))//如果重复记录，就不增加
    {
        // AddRowByBarCode(id, prodNo, prodName, specification, NowCount, codeName, unitID, StandardCost);
        AddRow();
        var LastRowID = document.getElementById("txtLastRowID");
        var RowID = parseInt(LastRowID.value - 1);
        fnSelectProductInfo(id, prodNo, prodName, specification, NowCount, codeName, unitID, StandardCost, RowID, IsBatchNo, '无', BatchNo);
    }
    else {
        document.getElementById("txtCheckCount_" + rerowID).value = parseFloat(document.getElementById("txtCheckCount_" + rerowID).value) + 1;
        cCheckDetail(0, rerowID);
        cTotal();
        cCheckTotal();
    }
}


//扫描增加行
function AddRowByBarCode(ID, ProductNo, ProductName, ProductSpec, NowCount, ProdcutUnitName, ProductUnitID, StandardCost) {

    var tbl = document.getElementById("tblCheck");
    var LastRowID = document.getElementById("txtLastRowID");
    var LastSortNo = document.getElementById("txtLastSortNo");
    var RowID = parseInt(LastRowID.value);
    //添加行
    var row = tbl.insertRow(-1);
    row.id = "tr_Row_" + RowID;
    row.className = "newrow";

    //添加checkbox列
    var cellCheck = row.insertCell(0);
    cellCheck.className = "cell";
    cellCheck.align = "center";
    cellCheck.innerHTML = "<input type=\"checkbox\" id=\"chk_list_" + RowID + "\" name=\"chk\" value=\"" + RowID + "\" onclick=\"subSelect();\" />";

    //序号
    var cellNo = row.insertCell(1);
    cellNo.className = "cell";
    cellNo.innerHTML = "<input type=\"text\" id=\"txtSortNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + parseInt(LastSortNo.value) + "\"  name=\"rownum\" disabled=\"true\"/>";

    //物品编号
    var cellProductNo = row.insertCell(2);
    cellProductNo.className = "cell";
    cellProductNo.innerHTML = "<input type=\"text\" id=\"txtProductNo_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + ProductNo + "\" title=\"" + ID + "\"  onclick=\"getProdcutList(" + RowID + ");\"  readonly />";

    //物品名称
    var cellProductName = row.insertCell(3);
    cellProductName.className = "cell";
    cellProductName.innerHTML = "<input type=\"text\" id=\"txtProductName_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + ProductName + "\" disabled=\"true\" />";

    //规格
    var cellProdcutSpec = row.insertCell(4);
    cellProdcutSpec.className = "cell";
    cellProdcutSpec.innerHTML = "<input type=\"text\" id=\"txtProductSpec_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + ProductSpec + "\"  disabled=\"true\" />";

    //单位
    var cellProductUnit = row.insertCell(5);
    cellProductUnit.className = "cell";
    cellProductUnit.innerHTML = "<input type=\"text\" id=\"txtProductUnit_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + ProdcutUnitName + "\" title=\"" + ProductUnitID + "\" disabled=\"true\" />";

    //成本单价
    var cellTransferPrice = row.insertCell(6);
    cellTransferPrice.className = "cell";
    cellTransferPrice.innerHTML = "<input type=\"text\" id=\"txtStandardCost_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + StandardCost + "\"   disabled=\"true\" />";

    //现存数量
    var cellTransferCount = row.insertCell(7);
    cellTransferCount.className = "cell";
    cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtNowCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"" + NowCount + "\"  disabled=\"true\"/>";

    //实盘数量
    var cellTransferCount = row.insertCell(8);
    cellTransferCount.className = "cell";
    cellTransferCount.innerHTML = "<input type=\"text\" id=\"txtCheckCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"1\"  onblur=\"cCheckDetail(0," + RowID + ")\" onchange=\"Number_round(this,2)\" />";


    //盈亏类型
    var cellCheckType = row.insertCell(9);
    cellCheckType.className = "cell";
    cellCheckType.style.width = "55px";
    cellCheckType.innerHTML = "<select id=\"ddlDiffType_" + RowID + "\"  disabled=\"true\"  ><option value=\"0\" >正常</option><option value=\"1\">盘盈</option><option value=\"2\">盘亏</option></select>";

    //差异量
    var cellTransferTotalPrice = row.insertCell(10);
    cellTransferTotalPrice.className = "cell";
    cellTransferTotalPrice.innerHTML = "<input type=\"text\" id=\"txtDiffCount_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"   disabled=\"true\"/>";


    //备注
    var cellTransferRemark = row.insertCell(11);
    cellTransferRemark.className = "cell";
    cellTransferRemark.innerHTML = "<input type=\"text\" id=\"txtCheckRemark_" + RowID + "\" class=\"tdinput tboxsize textAlign\" value=\"\"  maxlength=\"100\" />";

    cCheckDetail(0, RowID);
    cTotal();
    cCheckTotal();
    //移动行号与序号
    LastRowID.value = parseInt(LastRowID.value) + 1;
    LastSortNo.value = parseInt(LastSortNo.value) + 1;

}

//弹出选择层
function Showtmsm() {
    var StoID = $("#ddlStorageID").val();
    GetGoodsInfoByBarCode(StoID);
}
//库存快照
function ShowSnapshot() {

    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var signFrame = document.getElementById("tblCheck");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        if (document.getElementById('chk_list_' + i).checked) {
            detailRows++;
            intProductID = document.getElementById('txtProductNo_' + (i)).title;
            snapProductName = document.getElementById('txtProductName_' + (i)).value;
            snapProductNo = document.getElementById('txtProductNo_' + (i)).value;
        }
    }
    if (detailRows == 1) {
        if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
            popMsgObj.ShowMsg('选中的明细行中没有添加物品');
            return false;
        }

        ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);
    }
    else {
        popMsgObj.ShowMsg('请选择单个物品查看库存快照');
        return false;
    }

}





/*Add by Moshenlin  盘点明细支持多选---start*/


//多选添加行
function getProdcutListCheck() {
    var OutStorageID = document.getElementById("ddlStorageID").value;
    if (OutStorageID == "-1") {
        popMsgObj.Show("基本信息|", "请选择调货仓库|");
        return;
    }
    var Para = "OutStorageID=" + OutStorageID + "&OutDeptID=&action=GETPUT";
    popProductInfoObj.ShowCheckList(Para);
}
//多选填充值
function fnSelectCheckProductInfo() {
    var ck = document.getElementsByName("checkboxEmp");
    var strarr = '';
    var str = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            strarr += ck[i].value + ',';
        }
    }
    str = strarr.substring(0, strarr.length - 1);
    if (str == "") {
        popMsgObj.ShowMsg('请先选择数据！');
        return;
    }

    var Vstr = str.split(',');
    for (var j = 0; j < Vstr.length; j++) {
        var VaStr = Vstr[j].toString().split('|');
        var ID = VaStr[0];
        var ProductNo = VaStr[1];
        var ProductName = VaStr[2];
        var ProductSpec = VaStr[3];
        var NowCount = VaStr[4];
        var ProdcutUnitName = VaStr[5];
        var ProductUnitID = VaStr[6];
        var StandardCost = VaStr[7];
        //var rowid = VaStr[8]; 
        var IsBatchNo = VaStr[9];
        var BatchNo = VaStr[10];
        var BatchNoF = VaStr[11];
        var ChenkCount = VaStr[12];
        var DiffCount = VaStr[13];
        var UsedUnitID = VaStr[14];
        var UsedUnitCount = VaStr[15];
        var UsedPrice = VaStr[16];
        var ExRate = VaStr[17];
        var Remark = VaStr[18];
        var DiffType = VaStr[19];

        if (UsedUnitID == "") UsedUnitID = "0";
        if (StandardCost == "") StandardCost = "0.00";
        if (UsedUnitCount == "") UsedUnitCount = "0.00";
        if (UsedPrice == "") UsedPrice = "0.00";
        if (ExRate == "") ExRate = "0";
        if (NowCount != "") NowCount = parseFloat(NowCount).toFixed($("#HiddenPoint").val());
        if (StandardCost != "") StandardCost = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
        if (ChenkCount != "") ChenkCount = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val());
        if (DiffCount != "") DiffCount = parseFloat(DiffCount).toFixed($("#HiddenPoint").val());
        if (UsedUnitCount != "") UsedUnitCount = parseFloat(UsedUnitCount).toFixed($("#HiddenPoint").val());
        if (UsedPrice != "") UsedPrice = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val());
        if (BatchNo == "无")//不是 页面加载时才判断
        {
            for (var i = 0; i < DetailProductID.length; i++) {
                if (ID == DetailProductID[i]) {
                    popMsgObj.Show("盘点单明细|", "已添加该产品到盘点单明细|");
                    return;
                }
            }
        }

        AddRow();
        var txtTRLastIndex = findObj("txtLastRowID", document);
        var rowid = parseInt(txtTRLastIndex.value - 1);

        document.getElementById("txtProductNo_" + rowid).value = ProductNo;
        document.getElementById("txtProductNo_" + rowid).title = ID;
        document.getElementById("txtProductName_" + rowid).value = ProductName;
        document.getElementById("txtProductSpec_" + rowid).value = ProductSpec;
        //    document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
        //    document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
        //    document.getElementById("txtStandardCost_" + rowid).value = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
        //document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val());//现存数量
        document.getElementById("txtNowCount_Hidden" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量(隐藏)

        //绑定批次
        var ListControlID = "BatchNo_SignItem_TD_Text_" + rowid;
        var StorageControlID = "ddlStorageID";
        if (BatchNo == "undefined")
            GetBatchList(ID, StorageControlID, ListControlID, IsBatchNo);
        else
            GetBatchList(ID, StorageControlID, ListControlID, IsBatchNo, BatchNoF);
        //多计量单位
        if ($("#HiddenMoreUnit").val() == "True") {
            var BasePriceControl = "baseprice_td" + rowid;
            var BaseUnitControl = "BaseUnit_SignItem_TD_Text_" + rowid;
            $("#" + BaseUnitControl).val(ProdcutUnitName);
            $("#" + BaseUnitControl).attr("title", ProductUnitID);
            $("#" + BasePriceControl).val(StandardCost);

            if (BatchNo == "无")
                GetUnitGroupSelectEx(ID, "StockUnit", "txtProductUnit_" + rowid, "ChangeUnit(this.id," + rowid + "," + StandardCost + ")", "unitdiv" + rowid, '', "FillContent(" + rowid + "," + StandardCost + ")");
            else//页面查看时
            {
                GetUnitGroupSelectEx(ID, "StockUnit", "txtProductUnit_" + rowid, "ChangeUnit(this.id," + rowid + "," + StandardCost + ")", "unitdiv" + rowid, UsedUnitID, "LoadUnitContent(" + rowid + "," + StandardCost + ")");
                document.getElementById("txtStandardCost_" + rowid).value = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val()); //成本单价
                document.getElementById("txtCheckCount_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //实盘数量

                document.getElementById("txtDiffCount_" + rowid).value = parseFloat(DiffCount).toFixed($("#HiddenPoint").val()); //差异量
                document.getElementById("txtCheckRemark_" + rowid).value = Remark; //备注
                document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量
                document.getElementById("BaseUnit_SignItem_TD_Text_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //基本数量
                document.getElementById("BaseUnit_SignItem_TD_Text_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //基本数量
                document.getElementById("ddlDiffType_" + rowid).value = DiffType; //类型
            }
        } //
        else {
            document.getElementById("txtProductUnit_" + rowid).value = ProdcutUnitName;
            document.getElementById("txtProductUnit_" + rowid).title = ProductUnitID;
            document.getElementById("txtStandardCost_" + rowid).value = parseFloat(StandardCost).toFixed($("#HiddenPoint").val());
            document.getElementById("txtNowCount_" + rowid).value = parseFloat(NowCount).toFixed($("#HiddenPoint").val()); //现存数量
            if (BatchNo != "无") {
                document.getElementById("txtStandardCost_" + rowid).value = parseFloat(UsedPrice).toFixed($("#HiddenPoint").val()); //成本单价
                document.getElementById("txtCheckCount_" + rowid).value = parseFloat(ChenkCount).toFixed($("#HiddenPoint").val()); //实盘数量
                //document.getElementById("txtDiffCount_" + rowid).value = parseFloat(DiffCount).toFixed($("#HiddenPoint").val());//差异量
                document.getElementById("txtCheckRemark_" + rowid).value = Remark; //备注
                document.getElementById("ddlDiffType_" + rowid).value = DiffType; //类型
            }
        }

        /*将产品ID加到数组，用来验证*/
        DetailProductID.push(ID);
        //计算现有库存及现有金额
        cTotal();
        cCheckTotal();

    }
    closeProductInfodiv();
}
//判断物品在明细中添加是否重复
function IsExistCheck(prodNo) {
    var sign = false;
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; //明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            var prodNo1 = document.getElementById("ProductID" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
                sign = true;
                break;
            }
        }
    }

    return sign;
}

/*Add by Moshenlin  盘点明细支持多选---End*/






