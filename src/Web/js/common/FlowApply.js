
//显示提交审批窗口
objFlow.Fun_Show = function(isSure,billID)
{
    
    objFlow.BillID = 0;
    objFlow.IsRemind = true;
    objFlow.IsSure = isSure;
    objFlow.BillID = billID;
    document.getElementById('isRemind').checked = true;
    document.getElementById('trApprovalNote').style.display='none';
    if(isSure)
    {
        document.getElementById('trApprovalNote').style.display='';
        document.getElementById('tdTopTitle').innerHTML='<strong>审   批</strong>';
        if(objFlow.BillID<=0)
        {
            popMsgObj.ShowMsg('没有找到单据ID');
            return false;
        }
        objFlow.Get_FlowStep(objFlow.BillID);//加载步骤
    }
    else
    {
        document.getElementById('tdTopTitle').innerHTML='<strong>提  交  审   批</strong>';
    }
    objFlow.Get_Flow(isSure,objFlow.BillID);//加载流程下拉列表

    if(isSure)
    {

        //此处判断是否有权限审批
        if(objFlow.BillID<=0)
        {
            popMsgObj.ShowMsg('没有找到单据ID');
            return false;
        }
        var sltFlow = document.getElementById('sltFlow').value.split('#')[0].toString();//此处默认选中
        var Action = "Authority";
        objFlow.FlowNo = sltFlow;
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"&FlowNo="+objFlow.FlowNo+"";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/Flow.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误!');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                       ShowFlowObject(isSure); 
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info); 
                    }
                  } 
               });  
    }
    else
    {
        ShowFlowObject(isSure);
    }
}

function ShowFlowObject(isSure)
{
    openRotoscopingDiv(false,'divFlowShadow','FlowShadowIframe');
    document.getElementById('txtFlowNote').value='';
    document.getElementById('trFlowApply').style.display= isSure?'none':'';
    document.getElementById('trFlowApproval').style.display= isSure?'':'none';
    document.getElementById('divApprovalPassRadio').style.display = isSure?'':'none';
    document.getElementById('trApprovalStep').style.display = isSure?'':'none';
    document.getElementById('divFlowApply').style.display='';
    document.getElementById('divFlowApply').style.position='absolute';  
}
objFlow.Fun_Hidden =function()
{
    closeRotoscopingDiv(false,'divFlowShadow');
    document.getElementById('trFlowApply').style.display= objFlow.IsSure?'none':'';
    document.getElementById('trFlowApproval').style.display= objFlow.IsSure?'':'none';
    document.getElementById('divApprovalPassRadio').style.display = objFlow.IsSure?'':'none';
    document.getElementById('trApprovalStep').style.display = objFlow.IsSure?'':'none';
    document.getElementById('btnPass').checked=true;
    document.getElementById('trApprovalNote').style.display='none';
    document.getElementById('divFlowApply').style.display='none';
}
objFlow.Fun_Show_Record =function(billID)
{
    objFlow.BillID = 0;
    objFlow.BillID = billID;
    objFlow.Get_FlowRecord(objFlow.BillID);
    openRotoscopingDiv(false,'divFlowShadow','FlowShadowIframe');
    document.getElementById('divStepRecordList').style.display='';
}
//页面提交审批
objFlow.Fun_Save_FlowApply =function()
{
        
        objFlow.BillNo = document.getElementById(FlowJs_BillNo).value;
        if(objFlow.BillID<=0)
        {
            popMsgObj.ShowMsg('没有找到单据ID');
            return false;
        }
        if(objFlow.BillNo=="")
        {
            popMsgObj.ShowMsg('没有找到单据编码');
            return false;  
        }
        var sltFlow = document.getElementById('sltFlow').value.split('#')[0].toString();
        var Action = "Add";
        var MsgRemind = 0;
        objFlow.FlowNo = sltFlow;
        objFlow.IsRemind = document.getElementById('isRemind').checked;
        if(objFlow.IsRemind)
        {
            MsgRemind = 1;
        }
        else
        {
            MsgRemind = 0;
        }
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"&BillNo=" + objFlow.BillNo + "&PageUrl=" + escape(objFlow.PageUrl) + "&MsgRemind=" + MsgRemind + "&FlowNo="+escape(objFlow.FlowNo)+"";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/Flow.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                        //加载单据状态，每个功能模块在各自的JS里定义，按钮名称可能不同
                       GetFlowButton_DisplayControl();
                        try
                        {
                            Fun_FlowApply_Operate_Succeed(0);//提交审批成功后
                        }catch(e){}
                    }
                    if(data.sta==1)
                    {
                        document.getElementById('sltFlow').disabled="disabled";
                    }
                    objFlow.Fun_Hidden();
                    popMsgObj.ShowMsg(data.info);
                  } 
               });
}

//页面审批
objFlow.Fun_Save_FlowApproval = function() {
    objFlow.BillNo = document.getElementById(FlowJs_BillNo).value;
    if (objFlow.BillID <= 0) {
        popMsgObj.ShowMsg('没有找到单据ID');
        return false;
    }
    var State = '0';
    if (document.getElementById('btnNoPass').checked) {
        State = '1';
    }
    var Note = document.getElementById('txtFlowNote').value;
    var sltFlow = document.getElementById('sltFlow').value.split('#')[0].toString(); //此处默认选中
    var Action = "Edit";
    var MsgRemind = 0;
    objFlow.FlowNo = sltFlow;
    /*第一个字符不以为英文状态下的问号，否则会报错*/
    if (Note.indexOf('?') == 0) {
        Note = '？' + Note.substr(1, Note.length);
    }

    objFlow.Note = Note;
    if (State == '1' && Note == "") {
        popMsgObj.ShowMsg('请输入审批意见.');
        return false;
    }
    if (!CheckSpecialWord2(Note)) {
        popMsgObj.ShowMsg('审批意见不能含有特殊字符.');
        return false;
    }
    objFlow.IsRemind = document.getElementById('isRemind').checked;
    if (objFlow.IsRemind) {
        MsgRemind = 1;
    }
    else {
        MsgRemind = 0;
    }
    var UrlParam = "Action=" + Action + "&BillTypeFlag=" + objFlow.BillTypeFlag + "&BillTypeCode=" + objFlow.BillTypeCode + "&BillID=" + objFlow.BillID + "&BillNo=" + objFlow.BillNo + "&FlowNo=" + objFlow.FlowNo + "&State=" + State + "&MsgRemind=" + MsgRemind + "&Note=" + escape(objFlow.Note) + "";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Common/Flow.ashx?" + UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            //popMsgObj.ShowMsg('请求发生错误');
        },
        success: function(data) {
            if (data.sta > 0) {
                //加载单据状态，每个功能模块在各自的JS里定义，按钮名称可能不同
                GetFlowButton_DisplayControl();
                try {
                    if (data.sta == 2) {
                        Fun_FlowApply_Operate_Succeed(2); //审批不通过 成功后
                    }
                    else {
                        Fun_FlowApply_Operate_Succeed(1); //审批成功后
                    }
                } catch (e) { }
            }
            if (data.sta == 2)//审批不通过
            {
                document.getElementById('sltFlow').removeAttribute("disabled");
            }
            objFlow.Fun_Hidden();
            popMsgObj.ShowMsg(data.info);
        }
    });
}
//页面撤消审批
objFlow.Fun_Update_FlowApproval = function(billID)
{
        objFlow.BillID = billID;
        if(objFlow.BillID<=0)
        {
            popMsgObj.ShowMsg('没有找到单据ID');
            return false;
        }
        var sltFlow = document.getElementById('sltFlow').value.split('#')[0].toString(); //此处默认选中
        var Action = "Cancel";
        objFlow.FlowNo = sltFlow;
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"";
        if(window.confirm('确认要进行撤消审批吗？'))
        {
            $.ajax({ 
                      type: "POST",
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
                        if(data.sta>0)
                        {
                            //加载单据状态，每个功能模块在各自的JS里定义，按钮名称可能不同
                            GetFlowButton_DisplayControl();
                            try
                            {
                              Fun_FlowApply_Operate_Succeed(3);//撤消审批成功后
                            }catch(e){}
                        }
                        popMsgObj.ShowMsg(data.info);
                      } 
                   }); 
        }

}

//单据状态(1：待审批 2：审批中 3：审批通过 4：审批不通过)
//注：此处只取出单据的具体状态，业务逻辑部分，每个功能模块自己处理
objFlow.Get_BillStatus =function(billID)
{
        objFlow.BillID = billID;
       //需要审批的
        var Action = "Get";
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"";
        $.ajax({ 
                  type: "POST",
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
                    var msg ='';
                    switch(data.sta)
                    {
                        case 0:
                                msg ='未提交审批';
                                break;
                         case 1:
                                msg ='当前单据正在待审批';
                                break;
                          case 2:
                                msg ='当前单据正在审批中';
                                break;
                          case 3:
                                msg ='当前单据已经通过审核';
                                break;
                          case 4:
                                msg ='当前单据审批未通过，不能进行确认操作！';
                                break;    
                    }
                    if(data.sta==3 || data.sta==0 || data.sta==5)//(状态等于0时，有可能是未定义审批流程的，未定义审批流程的也允许确认操作;状态等于5时，撤消审批的)
                    {
                        //每个功能模块在对应的JS里加上方法Fun_ConfirmOperate,处理自己的业务逻辑
                        Fun_ConfirmOperate();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(msg);
                    }
                  } 
               });
}
objFlow.Get_Flow = function(isSure, billID) {
    document.getElementById("sltFlow").length = 0;
    objFlow.BillID = billID;
    var intSure = 0;
    var browser = navigator.appName;
    var b_version = navigator.appVersion;


    if (isSure) {
        intSure = 1;
    }
    else {
        intSure = 0;
    }
    var Action = "GetFlow";
    var UrlParam = "";
    var TypeData = '';

    TypeData = 'json';
    UrlParam = "../../../Handler/Common/Flow.ashx?Action=" + Action + "&BillTypeFlag=" + objFlow.BillTypeFlag + "&BillTypeCode=" + objFlow.BillTypeCode + "&intSure=" + intSure + "&BillID=" + objFlow.BillID + "";

    $.ajax({
        type: "POST",
        url: UrlParam,
        dataType: TypeData, //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function(msg) {
            popMsgObj.ShowMsg('请求发生错误1');
        },
        success: function(data) {
            if (parseFloat(data.sta) > 0) {
                var tempOption = data.info.split('|');
                var obj = document.getElementById('sltFlow');
                obj.options.length = 0;
                for (var i = 0; i < tempOption.length; i++) {
                    try {
                        var tempOneOption = tempOption[i].toString().split(',');
                        obj.options.add(new Option(tempOneOption[1].toString(), tempOneOption[0].toString()));
                    }
                    catch (e)
		                { }
                }
                document.getElementById('tdTypeName').innerHTML = data.data;
                document.getElementById('isRemind').checked = document.getElementById('sltFlow').value.split('#')[1].toString() == '0' ? false : true;
                if (isSure) {
                    document.getElementById('sltFlow').disabled = "disabled";
                }
                else {
                    document.getElementById('sltFlow').removeAttribute("disabled");
                }
            }

        }
    });
}
/*审批流程切换时更新是否发送短信复选框的显示处理*/
function ChangeRemind(values) {
    document.getElementById('isRemind').checked = document.getElementById('sltFlow').value.split('#')[1].toString() == '0' ? false : true;
}
//加载步骤
objFlow.Get_FlowStep =function(billID)
{
        objFlow.BillID = billID;
       //需要审批的
        var Action = "GetStep";
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"";

        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/Flow.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                   if(data.sta>0)
                   {
                    document.getElementById('tdApprovalStepContent').innerHTML=data.info;
                   }
                   else
                   {
                    document.getElementById('tdApprovalStepContent').innerHTML=data.info;
                   }
                  } 
               });
}

//操作记录
objFlow.Get_FlowRecord =function(billID)
{
        objFlow.BillID = billID;
       //需要审批的
        var Action = "GetStep";
        var UrlParam = "Action="+Action+"&BillTypeFlag="+objFlow.BillTypeFlag+"&BillTypeCode="+objFlow.BillTypeCode+"&BillID="+objFlow.BillID+"";
               
    
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url: "../../../Handler/Common/FlowInfo.ashx?"+UrlParam,
       cache:false,          
       success: function(msg){
                    var tempStepListHtml = '';
                    var tempRecordListHtml = '';
                    var HaveStep = 1;
                    var HaveState = -1;

                    //加载审批操作记录
                    if(typeof(msg.dataRecord)!='undefined')
                    {

                        $.each(msg.dataRecord,function(i,item)
                        {
                            var tempStatus ='';
                            var tempAction ='';
                            if(item.State==0)
                            {
                                 tempStatus ='通过';
                                 tempAction ='审批';
                            }
                            if(item.State==1)
                            {
                                tempStatus ='不通过';
                                tempAction = '审批';
                            }
                            
                            if(item.State==2)
                            {
                                tempStatus ='撤消审批';
                                tempAction ='撤消审批';
                            }
                            if(item.State==-1)
                            {
                                tempStatus ='待审批';
                                tempAction ='审批';
                            }
                            //如果不等于撤消审批时列出下步待审批的
                            
                            //获取当前已经审批的第几步骤，和审批状态
                            if(i==0)
                            {
                               if(item.StepNo==null || item.StepNo=='' || item.StepNo==0)
                               {
                                    HaveStep = 1;
                                    HaveState = -1;
                               } 
                               else
                               {
                                    HaveStep = item.StepNo;
                                    HaveState = item.State;
                               }
                            }

                            if(item.StepNo==null || item.StepNo=='')
                            {
                                
                                tempRecordListHtml = tempRecordListHtml +'<tr><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.operateDate+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.Operator+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">提交审批</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\"></td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">&nbsp;</td></tr>';
                            }
                            else
                            {
                                if(item.StepNo==0)
                                {
                                    tempRecordListHtml = tempRecordListHtml +'<tr><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.operateDate+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.Operator+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+tempAction+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+tempStatus+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.Note+'</td></tr>';
                                }
                                else
                                {
                                    tempRecordListHtml = tempRecordListHtml +'<tr><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.operateDate+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.Operator+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+tempAction+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+tempStatus+'</td><td bgcolor=\"#FFFFFF\" class=\"flowFont\">'+item.Note+'</td></tr>';
                                }
                            }
                        });
                    }
                    if(tempRecordListHtml!='')
                    {
                        document.getElementById('tdRecordList').innerHTML='<table width=\"650\" border=\"0\" cellspacing=\"1\" bgcolor=\"#999999\">\
                                                                          <tr>\
                                                                          <td width=\"200\"  background=\"../../../images/Main/Table_bg.jpg\" class=\"flowFont\">操作时间</td>\
                                                                            <td width=\"100\" height=\"28\" background=\"../../../images/Main/Table_bg.jpg\" class=\"flowFont\">操作人</td>\
                                                                            <td width=\"50\" background=\"../../../images/Main/Table_bg.jpg\" class=\"flowFont\">操 作</td>\
                                                                            <td width=\"100\" background=\"../../../images/Main/Table_bg.jpg\" class=\"flowFont\">状 态</td>\
                                                                            <td width=\"200\" background=\"../../../images/Main/Table_bg.jpg\" class=\"flowFont\">意 见</td>\
                                                                          </tr>' +tempRecordListHtml +'</table>';
                    } 

              },
       error: function() {}, 
       complete:function(){}
       });
}

