
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

    调用的相关流程页面需要声明这些个变量

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
      [2009-07-03] var glbBtn_IsUnConfirm = false;此变量针对库存模块部分单据不需要取消确认按钮，其他开发人员不需要声明此变量
      
          
                       

**********************************************/
//===============================================按 钮 操 作=======================================
//审批流程按钮显示控制
function GetFlowButton_DisplayControl()
{
    
    //Start -逻辑判断(1：有没有定义审批流程 2：定义了审批流程(新建页面的 ||编辑时))
    //如果是停止中的审批流程需要再判断是否已经提交过审批，如果提交过审批则允许其继续审批
    //1:有没有定义审批流程
    try
    {
        glb_BillID = document.getElementById(FlowJS_HiddenIdentityID).value;
    }
    catch(e)
    {
        glb_BillID = 0;
    }
    var Action = "GetSet";
    var UrlParam = "Action="+Action+ "&BillTypeFlag="+glb_BillTypeFlag+"&BillTypeCode="+glb_BillTypeCode+"&BillID="+glb_BillID;
    $.ajax({ 
              type: "GET",
              url: "../../../Handler/Common/Flow.ashx?"+UrlParam,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
              error: function() 
              {
                //popMsgObj.ShowMsg('获取流程状态时：请求发生错误');
              }, 
              success:function(data) 
              { 
                SetFlowButton_DisplayControl(data.sta);
              } 
           }); 
                       
    //End -逻辑判断
      
}

//根据是否定义了流程状态处理相关按钮 status:流程状态
function SetFlowButton_DisplayControl(status)
{
    var glbBtn_IsShow_Tjsp = false;//蓝：提交审批
    var glbBtn_IsShow_Sp = false;  //蓝：审批
    var glbBtn_IsShow_Qr = false;  //蓝：确认
    var glbBtn_IsShow_Jd = false;  //蓝：结单
    var glbBtn_IsShow_Qxjd = false;//蓝：取消结单
    var glbBtn_IsShow_Cxsp = false;//蓝：撤消审批
    var glbBtn_IsShow_Record = false;//蓝：审批流程记录
    
    try
    {
        glb_BillID = document.getElementById(FlowJS_HiddenIdentityID).value;//document.getElementById('txtIndentityID').value;
    }
    catch(e)
    {
        glb_BillID = 0;
    }
    if(status==2)//发布中的流程
    {
        //2:定义了审批流程且
        if(parseInt(glb_BillID)>0)//编辑时
        {
            //Start========================================================================================================================================
            var Action = "Get";
            var UrlParam = "Action="+Action+
                                   "&BillTypeFlag="+glb_BillTypeFlag+
                                   "&BillTypeCode="+glb_BillTypeCode+
                                   "&BillID="+glb_BillID+"";
            $.ajax({ 
                      type: "GET",
                      url: "../../../Handler/Common/Flow.ashx?"+UrlParam,
                      dataType:'json',//返回json格式数据
                      cache:false,
                      beforeSend:function()
                      { 
                      }, 
                      error: function() 
                      {
                        //popMsgObj.ShowMsg('请求发生错误');
                      }, 
                      success:function(data) 
                      { 
                        // [ 加 载 单 据 审 批 状 态 ]
                        // 0:未提交审批   
                        // 1:当前单据正在待审批  
                        // 2:当前单据正在审批中   
                        // 3:当前单据已经通过审批  
                        // 4:当前单据审批未通过，不能进行确认操作 
                        // 5:撤消审批
                        if(data.sta==0)
                        { 
                            if(document.getElementById(FlowJS_BillStatus).value=='2' || document.getElementById(FlowJS_BillStatus).value=='4')//先未定义审批流程，后定义审批流程
                            {
                                GetFlowButton_Content(false,false,true,false,false,true,true,glb_BillID);
                            }
                            else
                            {
                                GetFlowButton_Content(true,false,false,false,false,false,false,glb_BillID);
                            }
                        }
                        if(data.sta==1 || data.sta==2) GetFlowButton_Content(false,true,false,false,false,true,true,glb_BillID);
                        if(data.sta==3) GetFlowButton_Content(false,false,true,false,false,true,true,glb_BillID);
                        if(data.sta==4) GetFlowButton_Content(true,false,false,false,false,false,true,glb_BillID);
                        if(data.sta==5) GetFlowButton_Content(true,false,false,false,false,false,true,glb_BillID);
                        try{SetSaveButton_DisplayControl(data.sta);}catch(e){}//此空方法返回当前单据审批状态值
                      } 
                   }); 
            //End===========================================================================================================================================
        }
        else//新建时
        {
            GetFlowButton_Content(glbBtn_IsShow_Tjsp,glbBtn_IsShow_Sp,glbBtn_IsShow_Qr,glbBtn_IsShow_Jd,glbBtn_IsShow_Qxjd,glbBtn_IsShow_Cxsp,glbBtn_IsShow_Record,glb_BillID);
        }
        
    }
    else
    {
        if(parseInt(glb_BillID)>0)
        {
            try{SetSaveButton_DisplayControl(0);}catch(e){}//此空方法返回当前单据审批状态值
            GetFlowButton_Content(glbBtn_IsShow_Tjsp,glbBtn_IsShow_Sp,true,glbBtn_IsShow_Jd,glbBtn_IsShow_Qxjd,glbBtn_IsShow_Cxsp,glbBtn_IsShow_Record,glb_BillID);
        }
        else
        {
            GetFlowButton_Content(glbBtn_IsShow_Tjsp,glbBtn_IsShow_Sp,glbBtn_IsShow_Qr,glbBtn_IsShow_Jd,glbBtn_IsShow_Qxjd,glbBtn_IsShow_Cxsp,glbBtn_IsShow_Record,glb_BillID);
        }
    }

}
function GetFlowButton_Content(glbBtn_IsShow_Tjsp,glbBtn_IsShow_Sp,glbBtn_IsShow_Qr,glbBtn_IsShow_Jd,glbBtn_IsShow_Qxjd,glbBtn_IsShow_Cxsp,glbBtn_IsShow_Record,billID)
{
    var glbBtn_Src_Tjsp ='<img id="btnPageFlowApply" src="../../../images/Button/Main_btn_submission.jpg" alt="提交审批" onclick="objFlow.Fun_Show(false,'+billID+')" style=\"cursor: pointer\" />';
    var glbBtn_Src_Tjsp_Un ='<img id="btnPageFlowApply" src="../../../images/Button/UnClick_tjsp.jpg" alt="提交审批" />';
    var glbBtn_Src_Sp ='<img id="btnPageFlowApprove" src="../../../images/Button/Main_btn_verification.jpg" alt="审批" onclick="objFlow.Fun_Show(true,'+billID+')" style=\"cursor: pointer\" />';
    var glbBtn_Src_Sp_Un ='<img id="btnPageFlowApprove" src="../../../images/Button/UnClick_sp.jpg" alt="审批"/>';
    var glbBtn_Src_Cxsp ='<img id="btnPageFlowUnApprove" src="../../../images/Button/Button_cxsp.jpg" alt="撤消审批" onclick=\"objFlow.Fun_Update_FlowApproval('+billID+');\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Cxsp_Un ='<img id="btnPageFlowUnApprove" src="../../../images/Button/Button_ucxsp.jpg" alt="撤消审批" />';
    var glbBtn_Src_Qr ='<img id="btnPageFlowConfrim" src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认" onclick=\"objFlow.Get_BillStatus('+billID+')\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qr_Un ='<img id="btnPageFlowConfrim" src="../../../images/Button/UnClick_qr.jpg" alt="确认"/>';
    var glbBtn_Src_Qxqr ='<img id="btnPageFlowConfrimUn" src="../../../images/Button/btn_qxqr.jpg" alt="取消确认" onclick=\"Fun_UnConfirmOperate();\" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qxqr_Un ='<img id="btnPageFlowConfrimUn" src="../../../images/Button/btn_uqxqr.jpg" alt="取消确认"/>';
    var glbBtn_Src_Jd ='<img id="btnPageFlowComplete" src="../../../images/Button/Main_btn_Invoice.jpg" alt="结单" onclick="Fun_CompleteOperate(true);" style=\"cursor: pointer\" />';
    var glbBtn_Src_Jd_Un ='<img id="btnPageFlowComplete" src="../../../images/Button/Button_jd.jpg" alt="结单"/>';
    var glbBtn_Src_Qxjd ='<img id="btnPageFlowCancle" src="../../../images/Button/Main_btn_qxjd.jpg" alt="取消结单" onclick="Fun_CompleteOperate(false);" style=\"cursor: pointer\" />';
    var glbBtn_Src_Qxjd_Un ='<img id="btnPageFlowCancle" src="../../../images/Button/Button_qxjd.jpg" alt="取消结单" />';
    var glbBtn_Src_Record='<img id="btnPageFlowRecord" src="../../../images/Button/btn_cklc.jpg" alt="审批流程操作记录" onclick="objFlow.Fun_Show_Record('+billID+');" style=\"cursor: pointer\" />';
    var glbBtn_Src_Record_Un ='<img id="btnPageFlowRecord" src="../../../images/Button/btn_ucklc.jpg" alt="审批流程操作记录" />';
    var glbBillStatus = document.getElementById(FlowJS_BillStatus).value;//document.getElementById('txtBillStatus').value;单证状态：单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
    var glbBtnCurrent_IsConfirm = true;//确认按钮是否显示（李裕松模块中不需要确认按钮）
    var glbBtnCurrent_IsUnConfirm = true;//取消确认按钮是否显示（库存模块中部分单据不需要确认按钮）
    var glbBtn_IsShow_Qxqr = false;

    var glbBut_Src_1 ='';
    var glbBut_Src_2 ='';
    var glbBut_Src_3 ='';
    var glbBut_Src_4 ='';
    var glbBut_Src_5 ='';
    var glbBut_Src_6 ='';
    var glbBtn_Src_7 ='';
    var glbBtn_Src_8 ='';
    
    if(glbBtn_IsShow_Qr)
    {
        if(glbBillStatus==2)
        { 
            glbBtn_IsShow_Qr =false;
            glbBtn_IsShow_Jd =true;
            glbBtn_IsShow_Qxqr = true;
            
        }
        if(glbBillStatus==4)
        {
            glbBtn_IsShow_Qr =false;
            glbBtn_IsShow_Qxjd =true;
        } 
        if(glbBillStatus!=1)
        {
            glbBtn_IsShow_Cxsp = false;
        }
    }
    glbBut_Src_1 = glbBtn_IsShow_Tjsp==true?glbBtn_Src_Tjsp:glbBtn_Src_Tjsp_Un;
    glbBut_Src_2 = glbBtn_IsShow_Sp==true?glbBtn_Src_Sp:glbBtn_Src_Sp_Un;
    glbBut_Src_3 = glbBtn_IsShow_Qr==true?glbBtn_Src_Qr:glbBtn_Src_Qr_Un;
    glbBut_Src_4 = glbBtn_IsShow_Jd==true?glbBtn_Src_Jd:glbBtn_Src_Jd_Un;
    glbBut_Src_5 = glbBtn_IsShow_Qxjd==true?glbBtn_Src_Qxjd:glbBtn_Src_Qxjd_Un;
    glbBut_Src_6 = glbBtn_IsShow_Cxsp==true?glbBtn_Src_Cxsp:glbBtn_Src_Cxsp_Un;
    glbBut_Src_7 = glbBtn_IsShow_Record==true?glbBtn_Src_Record:glbBtn_Src_Record_Un;
    glbBut_Src_8 = glbBtn_IsShow_Qxqr==true?glbBtn_Src_Qxqr:glbBtn_Src_Qxqr_Un;
    
    /*
       此处针对王玉贞开发模块，领料和退料处理 
    */
    try
    {
        if(typeof(FlowJS_Material)!='undefined')
        {
            var theDate = document.getElementById(FlowJS_Material).value;
            if(theDate!=null && strlen(cTrim(theDate,0))>0)
            {
                glbBut_Src_8 = glbBtn_Src_Qxqr_Un;
            }
        }
    }catch(e){}
    
    /*
       此处针对周军开发模块
    */
    try
    {
        if(typeof(FlowJS_FromSell)!='undefined')
        {
            var theSellBillStatus = document.getElementById(FlowJS_FromSell).value;
            if(theSellBillStatus!=null && strlen(cTrim(theSellBillStatus,0))>0 && theSellBillStatus=='2')
            {
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
        if(typeof(FlowJS_FromSellDetail)!='undefined')
        {
            var theSellBillStatus = document.getElementById(FlowJS_FromSellDetail).value;
            if(theSellBillStatus!=null && strlen(cTrim(theSellBillStatus,0))>0 && theSellBillStatus=='3')
            {
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
    }catch(e){}
    
    
    try
    {
        if(glb_IsComplete)
        {
            if(typeof(glbBtn_IsUnConfirm)=='undefined')
            {
                glbBtnCurrent_IsUnConfirm = true;
            }
            else
            {
                glbBtnCurrent_IsUnConfirm = glbBtn_IsUnConfirm;
            }
            if(glbBtnCurrent_IsUnConfirm)
            {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_3 +glbBut_Src_8 + glbBut_Src_4 + glbBut_Src_5 + glbBut_Src_7;
            }
            else
            {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_3  + glbBut_Src_4 + glbBut_Src_5 + glbBut_Src_7;
            }
        }
        else
        {
            if(typeof(glbBtn_IsConfirm)=='undefined')
            {
                glbBtnCurrent_IsConfirm = true;
            }
            else
            {
                glbBtnCurrent_IsConfirm = glbBtn_IsConfirm;
            }
            
            if(glbBtnCurrent_IsConfirm)
            {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_3 +glbBut_Src_8 + glbBut_Src_7;
            }
            else
            {
                document.getElementById('GlbFlowButtonSpan').innerHTML = glbBut_Src_1 + glbBut_Src_2 + glbBut_Src_6 + glbBut_Src_7;
            }
        }
    }
    catch(e){}
    
            
        /*************************************
        *根据调拨业务状态是否启用 结单 按钮 pdd
        **************************************/
        if(typeof(glbtn_IsClose)!="undefined")
        {
            var objStatus=document.getElementById("ddlBusiStatus").value;
            var objClose=document.getElementById("btnPageFlowComplete");
            if(objStatus!="4")
            {
                try
                {
                    objClose.src="../../../Images/Button/Button_jd.jpg";
                    objClose.onclick=null;
                }
                catch(e)
                {}
            }
        }
}
