var page = "";
var rejectNo ;
var Title ;
var TypeID ;
var Purchaser  ;
var PurchaserName  ;
var FromType  ;
var ProviderID  ;
var ProviderName  ;
var BillStatus ;
var UsedStatus ;
var DeptID;
var DeptName;
var Isliebiao ;

$(document).ready(function() 
{    
    fnGetExtAttr();
    requestobj = GetRequest();
    var intMasterSubStorageInID = document.getElementById("txtIndentityID").value;
    recordnoparam = intMasterSubStorageInID.toString();
    var ProductNo = requestobj['ProductNo'];
    var ProductID = requestobj['ProductID'];
    var ProductName = requestobj['ProductName'];
    var BillStatusName = requestobj['BillStatusName'];
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    if(typeof(Isliebiao)!="undefined")
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
    }
    if(typeof(BillStatusName) != "undefined")
    {
        document.getElementById("hidBillStatusName").value = BillStatusName;
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&ProductNo="+escape(ProductNo)+"&ProductID="+escape(ProductID)+"&ProductName="+escape(ProductName)+"&PageIndex="+escape(PageIndex)+"&PageCount="+escape(PageCount)+""; 
    document.getElementById("hidSearchCondition").value = URLParams;
      
    DealPage(recordnoparam);
});

/*重写toFixed*/
Number.prototype.toFixed = function(d) 
{
    return FormatAfterDotNumber(this, parseInt($("#hidSelPoint").val()));
}


//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }

   return theRequest;
  }

//处理加载页面
function DealPage(recordnoparam)
{
    //显示返回按钮
    if(typeof(document.getElementById("hidIsliebiao").value)!="undefined" && recordnoparam !=0)
    {
        document.getElementById("txtAction").value="2";
        GetSubStorageIn(recordnoparam);
        //页面加载完对页面进行设置
        var IsBillStatusName = document.getElementById("hidBillStatusName").value;
        if(IsBillStatusName != "制单")
        {
            //设置所有文本控件只读
            $(":text").each(function(){ 
            this.disabled=true; 
            });
            document.getElementById('txtremark').disabled = true;

            try
            {
                //按扭控制
                document.getElementById("imgSave").style.display = "none";
                try
                {
                    document.getElementById('btnGetGoods').style.display='none';
                    document.getElementById('unbtnGetGoods').style.display='';
                }
                catch(e){}
                document.getElementById("imgUnSave").style.display = "inline";
                document.getElementById("imgAdd").style.display = "none";
                document.getElementById("imgUnAdd").style.display = "inline";
                document.getElementById("imgDel").style.display = "none";
                document.getElementById("imgUnDel").style.display = "inline";
                document.getElementById("btn_confirm").style.display = "none";
                document.getElementById("btn_Unconfirm").style.display = "inline";
            }
            catch(e)
            {}
        }
        else
        {

            try
            {
                //按扭控制
                try
                {
                    document.getElementById('btnGetGoods').style.display='';
                    document.getElementById('unbtnGetGoods').style.display='none';
                }
                catch(e){}
                document.getElementById("imgSave").style.display = "inline";
                document.getElementById("imgUnSave").style.display = "none";
                document.getElementById("imgAdd").style.display = "inline";
                document.getElementById("imgUnAdd").style.display = "none";
                document.getElementById("imgDel").style.display = "inline";
                document.getElementById("imgUnDel").style.display = "none";
                document.getElementById("btn_confirm").style.display = "inline";
                document.getElementById("btn_Unconfirm").style.display = "none";
            }
            catch(e)
            {}
        }


    }
    else
    {
        // 填充扩展属性
        GetExtAttr("officedba.SubStorageIn", null);
    }
}

//返回
function Back()
{ 
var URLParams = document.getElementById("hidSearchCondition").value;
    window.location.href='SubStorageInitList.aspx?'+URLParams;
}


/****************************************************************
* 修改页面初始化
****************************************************************/ 
function GetSubStorageIn(ID)
{

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SubStoreManager/SubStorageInitEdit.ashx", //目标地址
        data: "&ID=" + ID + "",
        cache: false,
        beforeSend: function() { AddPop(); }, //发送数据之前           
        success: function(msg) {
            //数据获取完毕，填充页面据显示
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    //基本信息
                    $("#CodingRuleControl1_txtCode").attr("value", item.InNo); //合同编号
                    $("#divSubStorageInNo").attr("value", item.InNo); //合同编号
                    $("#txtTitle").attr("value", item.Title); //主题
                    $("#txtDeptName").attr("value", item.DeptName); //分店名称
                    $("#HidDeptID").attr("value", item.DeptID); //分店ID
                    $("#txtCreatorReal").attr("value", item.CreatorName); //制单人名称
                    $("#txtCreator").attr("value", item.Creator); //制单人ID
                    $("#txtCreateDate").attr("value", item.CreateDate); //制单日期
                    $("#ddlBillStatus").attr("value", item.BillStatus); //单据状态
                    $("#txtConfirmorReal").attr("value", item.ConfirmorName); //确认人名称
                    $("#txtConfirmor").attr("value", item.Confirmor); //确认人
                    $("#txtConfirmDate").attr("value", item.ConfirmDate); //确认人
                    $("#txtModifiedUserIDReal").attr("title", item.ModifiedUserID); //最后更新人
                    $("#txtModifiedUserID").attr("value", item.ModifiedUserID); //最后更新人
                    $("#txtModifiedDate").attr("value", item.ModifiedDate); //最后更新日期
                    $("#txtremark").attr("value", item.Remark); //备注

                    document.getElementById("divSubStorageInNo").innerHTML = item.InNo;
                    document.getElementById("divSubStorageInNo").style.display = "block";
                    document.getElementById("divInputNo").style.display = "none";
                    document.getElementById("divBatchNo").style.display = "none";
                    // 填充扩展属性
                    GetExtAttr("officedba.SubStorageIn", item);
                }

            });

            $.each(msg.data2, function(i, item) {
                if (item.ID != null && item.ID != "") {

                    var index = AddSignRow();
                    $("#txtProductID" + index).attr("value", item.ProductID);
                    $("#txtProductNo" + index).attr("value", item.ProductNo);
                    $("#txtProductName" + index).attr("value", item.ProductName);
                    $("#txtstandard" + index).attr("value", item.standard);
                    $("#txtUnitID" + index).attr("value", item.UnitID);
                    $("#txtUnitName" + index).attr("value", item.UnitName);
                    $("#txtSendCount" + index).attr("value", parseFloat(item.SendCount).toFixed(2));
                    //计量单位开启
                    if ($("#txtIsMoreUnit").val() == "1") {
                        GetUnitGroupSelect(item.ProductID, 'StockUnit', 'txtUsedUnit' + index, 'ChangeUnit(this,' + index + ')', "td_UsedUnitID_" + index, item.UsedUnitID);
                        if (item.UsedUnitCount != null && item.UsedUnitCount!='') {
                            $("#txtUsedUnitCount" + index).attr("value", parseFloat(item.UsedUnitCount).toFixed(2));
                        }
                    }
                    $("#hidBatchNo" + index).attr("value", item.BatchNo);
                    $("#hidIsBatchNo" + index).attr("value", item.IsBatchNo);
                    if (item.IsBatchNo == '1') {
                        document.getElementById("divBatchNo").style.display = "none";
                        document.getElementById('divBatchNoShow').innerHTML = item.BatchNo;
                    }
                }
            });
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() {
            hidePopup();
        } //接收数据完毕
    });
}


//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data)
{
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
    //有扩展属性才取值
    if (strKey != '')
    {
        var arrKey = strKey.split('|');
        for (var t = 0; t < arrKey.length; t++)
        {
            //不为空的字段名才取值
            if ($.trim(arrKey[t]) != '')
            {
                $("#" + $.trim(arrKey[t])).val(data[$.trim(arrKey[t])]);
            }
        }
    }
}

// 填充批次
function FillBatchNo(BatchNo)
{
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none" && $("#hidIsBatchNo"+(i+1)).val()=='1')
        {
            $("#hidBatchNo"+(i+1)).val(BatchNo);
        }
    }
}



//门店管理入库单
function InsertSubStorageIn() 
{ 

    if(!CheckBaseInfo())
        return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         var inNo = "";
         var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
         
         //基本信息
   
         if (CodeType == "")
         {
            inNo=$("#CodingRuleControl1_txtCode").val();
         }
         var title = document.getElementById('txtTitle').value; //单据主题
         var deptName = document.getElementById("txtDeptName").value;//分店名称
         var deptID = document.getElementById("HidDeptID").value;//分店ID
         
        
         //备注信息
         var creator=$("#txtCreator").val(); //制单人
         var createDate=$("#txtCreateDate").val(); //制单时间
         var billStatus=$("#ddlBillStatus").val();//单据状态
         var confirmor=$("#txtConfirmor").val();//确认人
         var confirmDate=$("#txtConfirmDate").val();//确认时间
         var modifiedUserID = $("#txtModifiedUserID").val();//最后更新人
         var modifiedDate=$("#txtModifiedDate").val();//最后更新日期
         var remark=$("#txtremark").val().Trim(); //备注
         if(document.getElementById("txtAction").value=="1")
        {
            action="Add";
        }
        else
        {
             action="Update";
        }
        
        var pcgz = "";
        var BatchNo = "";
        if(action=="Update")
        {// 修改
            BatchNo = document.getElementById("divBatchNoShow").innerHTML;  
        }
        else
        {// 新增
            var ddlBatchRule=$("#BatchRuleControl1_ddlBatchRule").val();
            if(ddlBatchRule == ""|| ddlBatchRule == null)//手工输入批次
            {
                BatchNo = $("#BatchRuleControl1_txtBatch").val();
                
                if(BatchNo =="")
                {
                    popMsgObj.Show("批次|","请输入批次|");
                    return;
                }
                else if (!CodeCheck(BatchNo))
                {
                    popMsgObj.Show("批次|","批次只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|");
                    return;
                }
            }
            else//自动生成批次
            {
                BatchNo = $("#BatchRuleControl1_ddlBatchRule").val();
                pcgz = "zd";
            }
        }
        
        var DetailID = new Array();
        var Detailchk = new Array();
        var DetailProductID = new Array();
        var DetailSendCount = new Array();
        var DetailUsedUnitID = new Array();
        var DetailUsedUnitCount = new Array();
        var DetailExRate = new Array();   
        var DetailIsBatchNo = new Array();   
        
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var length = 0;
        var signFrame = findObj("dg_Log",document); 
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            
            if(signFrame.rows[i+1].style.display!="none")
            {
                
                var objID = 'ID'+(i+1);
                var objchk = 'chk'+(i+1);
                var objProductID = 'txtProductID'+(i+1);
                var objSendCount = 'txtSendCount'+(i+1);
                
                
                Detailchk.push(document.getElementById(objchk).value);
                DetailProductID.push(document.getElementById(objProductID.toString()).value);
                DetailSendCount.push(document.getElementById(objSendCount.toString()).value);
                DetailIsBatchNo.push(document.getElementById("hidIsBatchNo"+(i+1)).value);
                
                //计量单位开启
                if ($("#txtIsMoreUnit").val() == "1") 
                {
                    var objUsedUnitCount = 'txtUsedUnitCount' + (i + 1);
                    var objUsedUnit = 'txtUsedUnit' + (i + 1);
                    DetailUsedUnitID.push(document.getElementById(objUsedUnit).value.split('|')[0]);
                    DetailUsedUnitCount.push(document.getElementById(objUsedUnitCount).value);
                    DetailExRate.push(document.getElementById(objUsedUnit).value.split('|')[1]);
                }
                else 
                {
                    DetailUsedUnitID.push('0');
                    DetailUsedUnitCount.push('0');
                    DetailExRate.push('0');
                }
                length++;
            }
        }
         if(inNo.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "入库单编号|";
   		    msgText = msgText +  "入库单编号不允许为空|";
         }
         

         var no=  document.getElementById("divSubStorageInNo").innerHTML;
         var txtIndentityID = $("#txtIndentityID").val();
        $.ajax({ 
                type: "POST",
                url: "../../../Handler/Office/SubStoreManager/SubStorageInit.ashx",
                dataType:'json',//返回json格式数据
                data: "inNo="+escape(inNo)
                    +"&title="+escape(title)
                    +"&deptID="+escape(deptID)
                    +"&createDate="+escape(createDate)
                    +"&billStatus="+escape(billStatus)
                    + "&confirmDate=" + escape(confirmDate) 
                    + "&modifiedUserID=" + escape(modifiedUserID) 
                    + "&remark=" + escape(remark) 
                    + "&BatchNo=" + escape(BatchNo) 
                    + "&DetailProductID=" + escape(DetailProductID) 
                    + "&DetailSendCount=" + escape(DetailSendCount)
                    + "&DetailUsedUnitID=" + escape(DetailUsedUnitID) 
                    + "&DetailUsedUnitCount=" + escape(DetailUsedUnitCount) 
                    + "&DetailExRate=" + escape(DetailExRate)
                    + "&DetailIsBatchNo=" + escape(DetailIsBatchNo)
                    +"&creator="+escape(creator)
                    +"&action="+escape(action)
                    +"&CodeType="+escape(CodeType)
                    +"&length="+escape(length)
                    +"&cno="+escape(no)
                    +"&pcgz=" + escape(pcgz)
                    +"&ID="+escape(txtIndentityID)
                    +GetExtAttrValue(),//数据
                cache:false,
                beforeSend:function()
                { 
                }, 
                error: function() {
                
                  
                }, 
                success:function(data) 
                {
                    if(data.sta>0) 
                    { 
                        $("#CodingRuleControl1_txtCode").val(data.data);
                        $("#CodingRuleControl1_txtCode").attr("readonly","readonly");
                        $("#CodingRuleControl1_ddlCodeRule").attr("disabled","false");
                        
                        if(action=="Add")
                        {
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('txtIndentityID').value = data.sta;
                            document.getElementById("btn_confirm").style.display = "inline";
                            document.getElementById("btn_Unconfirm").style.display = "none";
                            document.getElementById("txtAction").value="2";
                           if(CodeType!="")
                           {
                                isnew="edit";
                           }
                            document.getElementById("divSubStorageInNo").innerHTML= data.data.split('|')[0];
                            document.getElementById("divSubStorageInNo").style.display="block";
                            document.getElementById("divInputNo").style.display="none";
                            document.getElementById("btn_confirm").style.display = "inline";
                            document.getElementById("btn_Unconfirm").style.display = "none";
                            document.getElementById("divBatchNo").style.display = "none";
                            document.getElementById('divBatchNoShow').innerHTML = data.data.split('|')[1];
                            FillBatchNo($("#divBatchNoShow").html());
                         
                        }
                        else
                        {
                              popMsgObj.ShowMsg(data.info);
                              document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                              document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value;
                              document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                              document.getElementById("btn_confirm").style.display = "inline";
                                document.getElementById("btn_Unconfirm").style.display = "none";
                              
                        }
                             
                    } 
                    else 
                    { 
                        hidePopup();
                        popMsgObj.ShowMsg(data.info);
                    } 
                } 
               });  
} 

function Fun_Clear_Input()
{
    action="Add";
    window.location='PurchaseContract_Add.aspx'; 
}

function Isquanxuan()
{
    var signFrame = findObj("dg_Log",document);
    var quanxuan = true;
    for(var i=1;i<signFrame.rows.length;++i)
        {
            if(document.getElementById("chk"+i).checked == false)
            {
                quanxuan = false;
            }
        }
    if(quanxuan)
    {
        document.getElementById("checkall").checked = true;
    }
    else
    {
        document.getElementById("checkall").checked = false;
    }
}

///添加行
function AddSignRow() 
{      
        var BillStatusName = document.getElementById("hidBillStatusName").value;//单据状态
        var hidIsliebiao = document.getElementById("hidIsliebiao").value //为1表示从列表页面过来
        
        //读取最后一行的行号，存放在txtTRLastIndex文本框中 
 
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value);
        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "ID" + rowID;
        var i=0;
        
        var newNameXH=newTR.insertCell(i++);//选择
        newNameXH.className="tdColInputCenter";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' size='10' value="+rowID+" type='checkbox'   onclick=\"Isquanxuan();\" />";
      
        var newNameTD=newTR.insertCell(i++);//序号
        newNameTD.className="cell";
        newNameTD.id = "newNameTD"+rowID;
        newNameTD.innerHTML =GenerateNo(rowID);
        
        var newProductNo=newTR.insertCell(i++);//物品ID
        newProductNo.style.display = "none";      
        newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "'   type='text' style='width:100%'  /><input type='hidden' id='hidIsBatchNo"+rowID+"' ><input type='hidden' id='hidBatchNo"+rowID+"'  >";//添加列内容

        var newProductNo = newTR.insertCell(i++); //物品编号
        newProductNo.className = "proID"; 
        if(hidIsliebiao != "1"||BillStatusName =="制单"||BillStatusName =="")
        {
            newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "'  readonly onclick=\"popTechObj.ShowList('txtProductNo"+rowID+"');\" type='text' style='width:100%'  readonly />";//添加列内容
        }
        else
        {       
            newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "'  type='text' style='width:100%'   readonly disabled />";//添加列内容
        }
        
        var newProductName=newTR.insertCell(i++);//物品名称
        newProductName.className="proID";
        newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "'   type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        var newstandard=newTR.insertCell(i++);//规格(从物品表中带来显示，不往明细表中存)
        newstandard.className="proID";
        newstandard.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "'   type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        
        var newUnitID=newTR.insertCell(i++);//单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID"+rowID+"' id='txtUnitID" + rowID + "'  style='width:10%' type='text'  class='tdinput' readonly />";//添加列内容
        
        
        var newUnitName=newTR.insertCell(i++);//单位名称或基本单位
        newUnitName.className="proID";
        newUnitName.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "'   type='text' style='width:100%'  readonly disabled/>";//添加列内容
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newBackCount = newTR.insertCell(i++); //基本数量
            newBackCount.className = "proID";
            newBackCount.innerHTML = "<input name='txtSendCount" + rowID + "'  id='txtSendCount" + rowID + "'      style='width:100%'  type='text'  disabled />"

            var newBackCount = newTR.insertCell(i++); //单位
            newBackCount.className = "cell";
            newBackCount.id = "td_UsedUnitID_"+rowID;
            

            var newBackCount = newTR.insertCell(i++); //入库数量
            newBackCount.className = "proID";
            if (hidIsliebiao != "1" || BillStatusName == "制单" || BillStatusName == "") 
            {
                newBackCount.innerHTML = "<input name='txtUsedUnitCount" + rowID + "'  id='txtUsedUnitCount" + rowID + "'  value=''   onblur=\"zhuanhuanpp();\"  style='width:100%'  type='text'/>";
            }
            else 
            {
                newBackCount.innerHTML = "<input name='txtUsedUnitCount" + rowID + "'  id='txtUsedUnitCount" + rowID + "'  value=''   onblur=\"zhuanhuanpp();\"  style='width:100%'  type='text'  readonly />";
            }
        }
        else 
        {
            var newBackCount = newTR.insertCell(i++); //入库数量
            newBackCount.className = "proID";
            if(hidIsliebiao != "1"||BillStatusName =="制单"||BillStatusName =="")
            {
                newBackCount.innerHTML = "<input name='txtSendCount" + rowID + "'  id='txtSendCount" + rowID + "'  value=''   onblur=\"zhuanhuanpp();\"  style='width:100%'  type='text'/>";//添加列内容
            }
            else
            {
                newBackCount.innerHTML = "<input name='txtSendCount" + rowID + "'  id='txtSendCount" + rowID + "'  value=''   onblur=\"zhuanhuanpp();\"  style='width:100%'  type='text'  readonly  disabled/>";//添加列内容
            }
        }
        
        txtTRLastIndex.value = (rowID + 1).toString(); //将行号推进下一行
        return rowID;
}


function GenerateNo(Edge) 
{
    //生成序号
    var signFrame = findObj("dg_Log",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("newNameTD"+i).value = SortNo++;
            }
        }
        return SortNo;
    }
    return 0;//明细表不存在时返回0
}

function DeleteSignRowSubStorageIn() 
{
    //删除明细行，需要将序号重新生成
    var signFrame = findObj("dg_Log",document);        
    var ck = document.getElementsByName("chk");
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if ( ck[i].checked )
        {
           signFrame.rows[rowID].style.display="none";
        }
        document.getElementById("newNameTD"+rowID).innerHTML = GenerateNo(rowID);
    }
}


//全选
function SelectAll() 
{
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//计算各种合计信息
function fnTotalInfo() 
{
    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    var Discount = $("#txtDiscount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    var TotalDyfzk = $("#txtTotalDyfzk").val(); //抵应付账款
    var Zhekoumingxi = 0;//明细中折扣合计
    
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
            var rowid = i;
            var pProductCount = $("#txtProductCount" + rowid).val(); //到货数量
            var pCountDetail = $("#txtBackCount" + rowid).val(); //退货数量
            if(pCountDetail == "")
            {
                pCountDetail=0.00;
            }
            if(IsNumberOrNumeric(pCountDetail,12,2) == false)
            {
                alert("【退货数量】格式不正确！");
                return;
            }
            document.getElementById("txtBackCount" + rowid).value = FormatAfterDotNumber(pCountDetail, $("#hidSelPoint").val());
            if(pProductCount != "")
            {
                if(pProductCount<pCountDetail)
                {
                    alert("【退货数量】不能大于【到货数量】！");
                    return;
                }
            }
            var UnitPriceDetail = $("#txtUnitPrice" + rowid).val(); //单价
        //判断是否是增值税，不是增值税含税价始终等于单价
        if(!document.getElementById('chkisAddTax').checked)
        {
            $("#txtTaxPrice" + rowid).val($("#txtUnitPrice" + rowid).val());
        }
            var TaxPriceDetail = $("#txtTaxPrice" + rowid).val(); //含税价
            var DiscountDetail = $("#txtDiscount" + rowid).val(); //折扣
            var TaxRateDetail = $("#txtTaxRate" + rowid).val(); //税率

            var TotalPriceDetail = FormatAfterDotNumber((UnitPriceDetail * pCountDetail * DiscountDetail / 100), $("#hidSelPoint").val()); //金额=数量*单价*折扣
            var TotalTaxDetail = FormatAfterDotNumber((TotalPriceDetail * TaxRateDetail / 100), $("#hidSelPoint").val()); //税额=金额 *税率

            var TotalFeeDetail = FormatAfterDotNumber((TaxPriceDetail * pCountDetail * DiscountDetail / 100), $("#hidSelPoint").val()); //含税金额=数量*含税单价*折扣


            $("#txtTotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, $("#hidSelPoint").val())); //金额
            $("#txtTotalTax" + rowid).val(FormatAfterDotNumber(TotalTaxDetail, $("#hidSelPoint").val())); //税额
            $("#txtTotalFee" + rowid).val(FormatAfterDotNumber(TotalFeeDetail, $("#hidSelPoint").val())); //含税金额
            TotalPrice += parseFloat(TotalPriceDetail);//金额
            Tax += parseFloat(TotalTaxDetail);//税额
            TotalFee += parseFloat(TotalFeeDetail);//含税金额
            CountTotal += parseFloat(pCountDetail);//数量合计
            Zhekoumingxi +=  parseFloat(TaxPriceDetail*pCountDetail*(100-DiscountDetail)/100);//明细折扣金额=含税价*数量*（1-折扣）
        }
    }
    $("#txtTotalMoney").val(FormatAfterDotNumber(TotalPrice, $("#hidSelPoint").val())); //金额合计
    $("#txtTotalTaxHo").val(FormatAfterDotNumber(Tax, $("#hidSelPoint").val())); //税额合计
    $("#txtTotalFeeHo").val(FormatAfterDotNumber(TotalFee, $("#hidSelPoint").val())); //含税金额合计
    $("#txtCountTotal").val(FormatAfterDotNumber(CountTotal, $("#hidSelPoint").val())); //数量合计
    $("#txtDiscountTotal").val(FormatAfterDotNumber(((TotalFee * (100 - Discount) / 100) + Zhekoumingxi), $("#hidSelPoint").val())); //折扣金额
    $("#txtRealTotal").val(FormatAfterDotNumber((TotalFee * Discount / 100), $("#hidSelPoint").val())); //折后含税金额
    $("#txtTotalYthkhj").val(FormatAfterDotNumber(((TotalFee - ((TotalFee * (100 - Discount) / 100) + Zhekoumingxi)) - TotalDyfzk), $("#hidSelPoint").val())); //应退货款合计
}

function fnChangeAddTax()
{   //改变是否为增值税
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    if(isAddTax == true)
    {
        document.getElementById("chkisAddTaxText1").style.display="inline";
        document.getElementById("chkisAddTaxText2").style.display="none";
    }
    else
    {
        document.getElementById("chkisAddTaxText1").style.display="none";
        document.getElementById("chkisAddTaxText2").style.display="inline";
    }
    
    var signFrame = findObj("dg_Log",document);
    for (i = 1; i < signFrame.rows.length; i++)
    {
        if (signFrame.rows[i].style.display != "none")
        {
            var rowIndex = i;
            if(isAddTax == true)
            {//是增值税则
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxPrice"+rowIndex).value, $("#hidSelPoint").val());//含税价等于隐藏域含税价
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(document.getElementById("hiddTaxRate"+rowIndex).value, $("#hidSelPoint").val());//税率等于隐藏域税率
                
            }
            else
            {
                document.getElementById("txtTaxPrice"+rowIndex).value=FormatAfterDotNumber(document.getElementById("txtUnitPrice"+rowIndex).value, $("#hidSelPoint").val());//含税价等于单价
                document.getElementById("txtTaxRate"+rowIndex).value=FormatAfterDotNumber(0, $("#hidSelPoint").val());//税率等于0
                
            }
        }
    }
    fnTotalInfo();
}


//附件处理
function DealResume(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag)
    {
            //设置附件路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            //上传显示 
            document.getElementById("divUploadResume").style.display = "block";
    }
    //下载附件
    else if ("download" == flag)
    {
     //获取简历路径
            resumeUrl = document.getElementById("hfPageAttachment").value;
            window.open("..\\..\\..\\" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url)
{
    if (url != "")
    {
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none";        
        //设置简历路径
        document.getElementById("hfPageAttachment").value = url;
    }
}

function ChangeCurreny()
{//选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value;
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];
}

function DeleteAll()
{
    var Flag= document.getElementById("drpFromType").value;
    
     if(Flag == 0)
     {
         DeleteSignRow100();
         fnTotalInfo();
         document.getElementById("txtProviderID").style.display="block";
         document.getElementById("txtHiddenProviderID1").style.display="none";
         document.getElementById("divIsbishuxiang").style.display="none";
     }
     else
     {
         DeleteSignRow100();
         fnTotalInfo();
         document.getElementById("divIsbishuxiang").style.display="inline";
     }
}


function DeleteSignRow100()
{
    var signFrame = document.getElementById("dg_Log");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    findObj("txtTRLastIndex",document).value = 1;
}

function zhuanhuanpp()
{
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) != "undefined")||signFrame != null)
    {
        RealCount = 0;
        for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;

                var SendCount = "txtSendCount" + i;
                //计量单位开启
                if ($("#txtIsMoreUnit").val() == "1") 
                {
                    SendCount = "txtUsedUnitCount" + i;
                    CalCulateNum('txtUsedUnit' + i, "txtUsedUnitCount" + i, "txtSendCount" + i, "", "", $("#hidSelPoint").val());
                }
                
                var no = document.getElementById(SendCount).value.Trim();
                var no1 = no.replace(' ','');
                document.getElementById(SendCount).value = FormatAfterDotNumber(no1,parseInt($("#hidSelPoint").val()));
                
            }
            
        }
    }
   
}


//验证

/*
* 基本信息校验
*/
function CheckBaseInfo()
{

     var fieldText = "";
      var msgText = "";
      var isFlag = true;   
      
      //先检验页面上的特殊字符
      var RetVal=CheckSpecialWords();
      if(RetVal!="")
      {
          isFlag = false;
          fieldText = fieldText + RetVal+"|";
          msgText = msgText +RetVal+  "不能含有特殊字符|";
      }


    //新建时，编号选择手工输入时
    if (document.getElementById("txtAction").value=="1")
    {
//        获取编码规则下拉列表选中项
        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value;
            //编号必须输入
            if (employeeNo == "")
            {
                isFlag = false;
                fieldText += "入库单编号|";
   		        msgText += "请输入入库单编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "单据编号|";
   		            msgText = msgText +  "单据编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "单据编号|";
   		            msgText += "单据编号长度不大于50|";
   		        }
   		        
            }
        }    
     }         
    
    if(strlen(document.getElementById("txtTitle").value.Trim())>100)
    {
        isFlag = false;
        fieldText += "入库单主题|";
	    msgText +=  "入库单主题仅限于100个字符以内|";      
    }
    
    if(document.getElementById("txtDeptName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "分店名称|";
        msgText += "当前用户所在部门不是分店|";
    }
    
    
    //限制字数
    var Remark=document.getElementById("txtremark").value;//备注
    if(strlen(Remark)>800)
    {
        isFlag = false;
        fieldText += "备注|";
   		msgText +=  "备注仅限于800个字符以内|";      
    }
    
    //产品信息的校验
    var signFrame = document.getElementById("dg_Log");
    if((typeof(signFrame) != "undefined")||signFrame != null)
    {
       RealCount = 0;
         for(var i=1;i<signFrame.rows.length;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;

                var SendCount = "txtSendCount" + i;
                var no = document.getElementById(SendCount).value.Trim();
                var no1 = no.replace(' ','');
                if (IsNumericFH(no1, 8, parseInt($("#hidSelPoint").val())) == false)
                {
                    var ProductNo =document.getElementById("txtProductNo"+i).value.Trim();

                    isFlag = false;
                    if ($("#txtIsMoreUnit").val() == "1") {
                        fieldText += "基本数量|";
                        msgText += "物品编号为" + ProductNo + "的基本数量格式不正确|";
                    }
                    else {
                        fieldText += "入库数量|";
                        msgText += "物品编号为" + ProductNo + "的入库数量格式不正确|";
                    }
                    
                    
                }

                //计量单位开启
                if ($("#txtIsMoreUnit").val() == "1") {
                    SendCount = "txtUsedUnitCount" + i;
                    var no = document.getElementById(SendCount).value.Trim();
                    var no1 = no.replace(' ', '');
                    if (IsNumericFH(no1, 8, parseInt($("#hidSelPoint").val())) == false) {
                        var ProductNo = document.getElementById("txtProductNo" + i).value.Trim();

                        isFlag = false;
                        fieldText += "入库数量|";
                        msgText += "物品编号为" + ProductNo + "的入库数量格式不正确|";
                    }
                }
            }
        }
        if(RealCount == 0)
        {
            isFlag = false;
            fieldText += "产品信息|";
            msgText += "产品信息不能为空|";
        }
    }
    else
    {
        isFlag = false;
        fieldText +="产品信息|";
        msgText +="产品信息不能为空|";
    }
    

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}


function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate,StandardCost,GroupUnitNo,
    SaleUnitID,SaleUnitName,InUnitID,InUnitName,StockUnitID,StockUnitName,MakeUnitID,MakeUnitName,IsBatchNo)
{
    var temp = popTechObj.InputObj;
    var index = parseInt(temp.split('o')[2]);
    var ProductID = 'txtProductID'+index;
    var ProductNo = 'txtProductNo'+index;
    var ProductName='txtProductName'+index;
    var UnitID = 'txtUnitID'+index;
    var Unit='txtUnitName'+index;
    var Standard='txtstandard'+index;
    var BatchNo='hidIsBatchNo'+index;
    
    
    document.getElementById(Standard).value=standard;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value=unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(BatchNo).value = IsBatchNo;
}

/************************************************************************
 *                                                                      
 ************************************************************************/
function PurchaseRejectSelect()
{
    var Flag= document.getElementById("drpFromType").value;
    
    if(Flag == 0)
    {//无来源
        document.getElementById("txtProviderID").style.display="none";
        document.getElementById("txtHiddenProviderID1").style.display="block";
        document.getElementById("divIsbishuxiang").style.display="none";
    }
    else if(Flag==1)
    {//采购订单 
        var ProviderID = document.getElementById("txtHidProviderID").value;
        if(ProviderID =="")
        {
            alert("请先选择供应商！");
            return;
        }
        popOrder.ShowList('',ProviderID);
    }
}


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)//选择供应商带出部分信息
{  
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername;//当选择源单类型时，用于显示供应商并且不允许再修改
    document.getElementById("drpTakeType").value = taketype;
    document.getElementById("drpCarryType").value = carrytype;
    document.getElementById("drpPayType").value= paytype;
    
    closeProviderdiv();
}

//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(productid,frombillid,fromLineno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var productid1 = document.getElementById("txtProductID"+i).value;
        var frombillid1 = document.getElementById("txtFromBillID"+i).value;
        var fromlineno1 = document.getElementById("txtFromLineNo"+i).value;
        
        if((signFrame.rows[i].style.display!="none")&&(productid1 == productid)&&(frombillid1 == frombillid)&&(fromlineno1 == fromLineno))
        {
            return true;
        } 
    }
    return false;
}


function FillFromArrive(productid,productno,productname,standard,unitid,unitname,productcount,unitprice,taxprice,discount,
                       taxrate,totalprice,totalfee,totaltax,remark,frombillid,frombillno,fromLineno)
{
    if(!ExistFromBill(productid,frombillid,fromLineno))
        {
            var Index = findObj("txtTRLastIndex",document).value;
            AddSignRow();
            var ProductID = 'txtProductID'+Index;
            var ProductNo = 'txtProductNo'+Index;
            var ProductName = 'txtProductName'+Index;
            var Standard = 'txtstandard'+Index;
            var UnitID = 'txtUnitID'+Index;
            var UnitName = 'txtUnitName'+Index;
            var ProductCount = 'txtProductCount'+Index;
            var UnitPrice = 'txtUnitPrice'+Index;
            var TaxPrice = 'txtTaxPrice'+Index;
            var Discount = 'txtDiscount'+Index;
            var TaxRate = 'txtTaxRate'+Index;
            var Remark = 'txtRemark'+Index;
            var FromBillID = 'txtFromBillID'+Index;
            var FromBillNo = 'txtFromBillNo'+Index;
            var FromLineNo = 'txtFromLineNo'+Index;
            var HiddTaxPrice = 'hiddTaxPrice'+Index;
            var HiddTaxRate = 'hiddTaxRate'+Index;
           
            
            document.getElementById(ProductID).value = productid;
            document.getElementById(ProductNo).value = productno;
            document.getElementById(ProductName).value = productname;
            document.getElementById(Standard).value = standard;
            document.getElementById(UnitID).value = unitid;
            document.getElementById(UnitName).value = unitname;
            document.getElementById(ProductCount).value = FormatAfterDotNumber(productcount, $("#hidSelPoint").val());
            document.getElementById(UnitPrice).value = FormatAfterDotNumber(unitprice, $("#hidSelPoint").val());
            document.getElementById(TaxPrice).value = FormatAfterDotNumber(taxprice, $("#hidSelPoint").val());
            document.getElementById(Discount).value = discount;
            document.getElementById(TaxRate).value = FormatAfterDotNumber(taxrate, $("#hidSelPoint").val());
            document.getElementById(Remark).value = remark;
            document.getElementById(FromBillID).value = frombillid;
            document.getElementById(FromBillNo).value = frombillno;
            document.getElementById(FromLineNo).value = fromLineno;
            document.getElementById(HiddTaxPrice).value = FormatAfterDotNumber(taxprice, $("#hidSelPoint").val());
            document.getElementById(HiddTaxRate).value = FormatAfterDotNumber(taxrate, $("#hidSelPoint").val());
            
           
        }
        document.getElementById("txtProviderID").style.display="none";
        document.getElementById("txtHiddenProviderID1").style.display="block";
        closeMaterialdiv();
        
        var isAddTax = document.getElementById("chkIsAddTax").checked;
        var signFrame = findObj("dg_Log",document);
        for (i = 1; i < signFrame.rows.length; i++)
        {
            if (signFrame.rows[i].style.display != "none")
            {
                var rowIndex = i;
                if(isAddTax == true)
                {//是增值税则
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("hiddTaxPrice"+rowIndex).value;//含税价等于隐藏域含税价
                    document.getElementById("txtTaxRate"+rowIndex).value=document.getElementById("hiddTaxRate"+rowIndex).value;//税率等于隐藏域税率
                    document.getElementById("chkisAddTaxText1").style.display="inline";
                    document.getElementById("chkisAddTaxText2").style.display="none";
                }
                else
                {
                    document.getElementById("txtTaxPrice"+rowIndex).value=document.getElementById("txtUnitPrice"+rowIndex).value;//含税价等于单价
                    document.getElementById("txtTaxRate"+rowIndex).value="0.00";//税率等于0
                    document.getElementById("chkisAddTaxText1").style.display="none";
                    document.getElementById("chkisAddTaxText2").style.display="inline";
                }
            }
        }
}


function FillUnit(unitid,unitname) //回填单位
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID"+i;
    var UnitName = "txtUnitName"+i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}



//确认
function Fun_ConfirmOperate()
{

var ActionArrive = document.getElementById("txtAction").value
    
    if(ActionArrive == "1")
    {
        popMsgObj.ShowMsg('请先保存再确认！');
        return;
    }
    
    
    glb_BillID = document.getElementById('txtIndentityID').value;
    var no=  document.getElementById("divSubStorageInNo").innerHTML;
    var deptID = document.getElementById("HidDeptID").value;//分店ID
    document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value;
    document.getElementById("txtConfirmor").value = document.getElementById("UserID").value;
    document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value;
    action ="Confirm";
    
	var DetailProductID = new Array();
    var DetailSendCount = new Array();
    var DetailUnitPrice = new Array();
    var DetailBatchNo = new Array();
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var length = 0;
    var signFrame = findObj("dg_Log",document); 
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        
        if(signFrame.rows[i+1].style.display!="none")
        {
            var objProductID = 'txtProductID'+(i+1);
            var objSendCount = 'txtSendCount'+(i+1);
            
            
            DetailProductID.push(document.getElementById(objProductID.toString()).value);
            DetailSendCount.push(document.getElementById(objSendCount.toString()).value);
            DetailUnitPrice.push("0");
            DetailBatchNo.push(document.getElementById("hidBatchNo"+(i+1)).value);
            length++;
        }
    }
    
    var confirmor = document.getElementById("txtConfirmor").value;
    var strParams = "action="+action
                    +"&confirmor="+confirmor
                    +"&ID="+glb_BillID
                    +"&cno="+no
                    +"&deptID="+deptID
                    +"&DetailProductID="+DetailProductID
                    +"&DetailSendCount="+DetailSendCount
                    +"&DetailUnitPrice="+DetailUnitPrice
                    +"&DetailBatchNo="+DetailBatchNo
                    +"&length="+length+"";
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/SubStoreManager/SubStorageInit.ashx?"+strParams,
           
        dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                   //popMsgObj.ShowMsg('sdf');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                        popMsgObj.ShowMsg('确认成功');
                        document.getElementById("ddlBillStatus").value = "5";
                        document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                        document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value;
                        document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                        
                        try
                        {
                        $("#imgSave").css("display", "none");
                           try
                        {
                            document.getElementById('btnGetGoods').style.display='none';
                            document.getElementById('unbtnGetGoods').style.display='';
                        }
                        catch(e){}
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display", "inline");
                        $("#btn_confirm").css("display", "none");
                        $("#btn_Unconfirm").css("display", "inline");
                        }
                        catch(e)
                       {}
                    }
                  } 
               });   
}

//结单或取消结单按钮操作
function Fun_CompleteOperate(isComplete)
{
    
    glb_BillID = document.getElementById('txtIndentityID').value;
    document.getElementById("txtCloserReal").value = document.getElementById("UserName").value;
    document.getElementById("txtCloser").value = document.getElementById("UserID").value;
    document.getElementById("txtCloseDate").value = document.getElementById("SystemTime").value;
    var closer = document.getElementById("txtCloser").value;
    var rejectNo = document.getElementById("divSubStorageInNo").innerHTML;
    
    if(isComplete)
    {
        action ="Close";
    }
    else
    {
        action ="CancelClose";
    }
    //结单操作
        
        var strParams = "action="+action+"\
                                 &closer="+closer+"\
                                 &rejectNo="+rejectNo+"\
                                 &ID="+glb_BillID+"";
        $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseRejectAdd.ashx?"+strParams,
           
        dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                   //popMsgObj.ShowMsg('sdf');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0)
                    {
                        if(data.sta == 1)
                        {
                            document.getElementById("ddlBillStatus").value = "4";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value;
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
                        else
                        {
                            document.getElementById("ddlBillStatus").value = "2";
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value;
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
                            fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                            fnFlowStatus($("#FlowStatus").val()); 
                        }
                        popMsgObj.ShowMsg(data.info);
                        //审批处理
                    }
                  } 
               });   
}



function Fun_FlowApply_Operate_Succeed(operateType)
{
try
{
    if(operateType == "0")
    {//提交审批成功后,不可改
        $("#imgUnSave").css("display", "inline");//保存灰
        $("#imgSave").css("display", "none");//保存
           try
                    {
                        document.getElementById('btnGetGoods').style.display='none';
                        document.getElementById('unbtnGetGoods').style.display='';
                    }
                    catch(e){}
        $("#imgAdd").css("display", "none");//明细添加
        $("#imgUnAdd").css("display", "inline");//明细添加灰
        $("#imgDel").css("display", "none");//明细删除
        $("#imgUnDel").css("display", "inline");//明细删除灰
        $("#Get_Potential").css("display", "none");//源单总览
        $("#Get_UPotential").css("display", "inline"); //源单总览灰
    }
    else if(operateType == "1")
    {//审批成功后，不可改
        $("#imgUnSave").css("display", "inline");
        $("#imgSave").css("display", "none");
        try
          {
              document.getElementById('btnGetGoods').style.display='none';
              document.getElementById('unbtnGetGoods').style.display='';
          }
        catch(e){}
        
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#Get_Potential").css("display", "none");
        $("#Get_UPotential").css("display", "inline"); 
    }
    else if(operateType == "2")
    {//审批不通过
        $("#imgUnSave").css("display", "none");
        $("#imgSave").css("display", "inline");
           try
                    {
                        document.getElementById('btnGetGoods').style.display='';
                        document.getElementById('unbtnGetGoods').style.display='none';
                    }
                    catch(e){}
        
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#Get_Potential").css("display", "inline");
        $("#Get_UPotential").css("display", "none"); 
    }
  }
  catch(e)
   {}
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus,Isyinyong) {
try
    {
    switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        case '1': //制单

            break;
        case '2': //执行
            if(Isyinyong == '被引用')
            {//被引用不可编辑
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                   try
                    {
                        document.getElementById('btnGetGoods').style.display='none';
                        document.getElementById('unbtnGetGoods').style.display='';
                    }
                    catch(e){}
            }
            else
            {
                   try
                    {
                        document.getElementById('btnGetGoods').style.display='';
                        document.getElementById('unbtnGetGoods').style.display='none';
                    }
                    catch(e){}
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
                $("#imgAdd").css("display", "inline");
                $("#imgUnAdd").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#Get_Potential").css("display", "inline");
                $("#Get_UPotential").css("display", "none");  
            }
            
            break;
        case '3': //变更
            $("#FromType").attr("disabled", "disabled");
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            break;
        case '4': //手工结单
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
            break;

        case '5':
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            break;
            }
     }
     catch(e)
   {}
}

function fnFlowStatus(FlowStatus)
{
    try
    {
    switch (FlowStatus) {
        case "": //未提交审批         
            break;
        case "待审批": //当前单据正在待审批
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");                   
            break;
        case "审批中": //当前单据正在审批中
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#Get_Potential").css("display", "none");
            $("#Get_UPotential").css("display", "inline");
            break;
        case "审批通过": //当前单据已经通过审核
            //制单状态的审批通过单据,不可修改
            if ($("#ddlBillStatus").val() == "1") {
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "none");
                $("#imgUnAdd").css("display", "inline");
                $("#imgDel").css("display", "none");
                $("#imgUnDel").css("display", "inline");
                $("#Get_Potential").css("display", "none");
                $("#Get_UPotential").css("display", "inline");
            }

            break;
        case "审批不通过": //当前单据审批未通过
            break;
    }
    }
    catch(e)
    {}
}



function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
}

function ShowProdInfo()
{
    popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"','Check');
}



function GetValue()
{
   var ck = document.getElementsByName("CheckboxProdID");
        var strarr=''; 
        var str = "";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               strarr += ck[i].value+',';
            }
        }
         str = strarr.substring(0,strarr.length-1);
         if(str =="")
         {
            popMsgObj.ShowMsg('请先选择数据！');
            return;
         }
          $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str='+str,//目标地址
           cache:false,
               data:'',//数据
               beforeSend:function(){},//发送数据之前
         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item)
                    {//填充物品ID，物品编号，物品名称，规格，单位ID，单位名称，入库数量(默认为0)
                        if(!IsExist(item.ID))
                        {   
                            var rukucount = 0;
                            var index = AddSignRow();
                            $("#txtProductID" + index).attr("value", item.ID);
                            $("#txtProductNo" + index).attr("value", item.ProdNo);
                            $("#txtProductName" + index).attr("value", item.ProductName);
                            $("#txtstandard" + index).attr("value", item.Specification);
                            $("#txtUnitID" + index).attr("value", item.UnitID);
                            $("#txtUnitName" + index).attr("value", item.CodeName);
                            //计量单位开启
                            if ($("#txtIsMoreUnit").val() == "1") 
                            {
                                GetUnitGroupSelect(item.ID, 'StockUnit', 'txtUsedUnit' + index, 'ChangeUnit(this,' + index + ')', "td_UsedUnitID_" + index, item.UsedUnitID);
                            }
                            $("#txtSendCount" + index).attr("value", parseFloat(rukucount).toFixed(2));
                            $("#hidIsBatchNo" + index).attr("value", item.IsBatchNo);
                        }
                   });
                     
                },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
}  


//判断是否有相同记录有返回true，没有返回false
function IsExist(productid)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var productid1 = document.getElementById("txtProductID"+i).value;
        
        if((signFrame.rows[i].style.display!="none")&&(productid1 == productid))
        {
            return true;
        } 
    }
    return false;
}





function DBC2SBC(str, flag){
    var result = '';
    str = str.replace(/。/g,"．");
    for(var i=0;i<str.length;i++){
        code = str.charCodeAt(i);
        if(flag){
            if(code >= 65281 && code <= 65373) result += String.fromCharCode(str.charCodeAt(i) - 65248);
            else if(code == 12288) result += String.fromCharCode(str.charCodeAt(i) - 12288 + 32);
                else result += str.charAt(i);
        }
        else{
            if(code >= 33 && code <= 126) result += String.fromCharCode(str.charCodeAt(i) + 65248);
            else if(code == 32) result += String.fromCharCode(str.charCodeAt(i) - 32 + 12288);
                else result += str.charAt(i);
        }
    }
    return result;
}

function PrintInit()
{
    var intMasterSubStorageInID = document.getElementById("txtIndentityID").value;
    if(parseInt(intMasterSubStorageInID) < 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("SubStorageInitPrint.aspx?ID=" + intMasterSubStorageInID);
}


//---------------------------------------------------------------------------------------------------------条码扫描Start
function GetGoodsDataByBarCode(ID,ProdNo,ProductName,StandardSell,UnitID,CodeName,TaxRate,SellTax
                                ,Discount,Specification,CodeTypeName,TypeID,StandardBuy,TaxBuy,InTaxRate
                                ,StandardCost,IsBatchNo,ProductCount,CurrentStore,Source)
{   
    AddSignRowSearch(ID,ProdNo,ProductName,Specification,UnitID,CodeName,IsBatchNo);
}
                                                  


//添加行
 function AddSignRowSearch(ID,ProdNo,ProductName,Specification,UnitID,CodeName,IsBatchNo)
 {  
        var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
        var signFrame = findObj("dg_Log",document); 
        if((typeof(signFrame) == "undefined")||signFrame ==null)
        {
          return false;
        }
        else
        {
            for(var i=0;i<txtTRLastIndex-1;i++)
            {                
                if(signFrame.rows[i+1].style.display!="none")
                {
                    var objProductID = 'txtProductID'+(i+1);
                    if(document.getElementById(objProductID).value==ID)
                    {
                        //计量单位开启
                        if ($("#txtIsMoreUnit").val() == "1") 
                        {
                            if($("#txtUsedUnitCount"+(i+1)).val()=="")
                            {
                                $("#txtUsedUnitCount"+(i+1)).val(FormatAfterDotNumber(1,$("#hidSelPoint").val()));
                            }
                            $("#txtUsedUnitCount"+(i+1)).val(FormatAfterDotNumber( parseFloat( $("#txtUsedUnitCount"+(i+1)).val())+1,$("#hidSelPoint").val()));
                        }
                        else
                        {
                            if($("#txtSendCount"+(i+1)).val()=="")
                            {
                                $("#txtSendCount"+(i+1)).val(FormatAfterDotNumber(1,$("#hidSelPoint").val()));
                            }
                            $("#txtSendCount"+(i+1)).val(FormatAfterDotNumber( parseFloat( $("#txtSendCount"+(i+1)).val())+1,$("#hidSelPoint").val()));
                        }
                        ChangeUnit(this,(i+1));
                        return;
                    }
                 }
            }
        }
        var BillStatusName = document.getElementById("hidBillStatusName").value;//单据状态
        var hidIsliebiao = document.getElementById("hidIsliebiao").value //为1表示从列表页面过来
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
    
        var rowID = parseInt(txtTRLastIndex.value);
        
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "ID" + rowID;
        var i=0;
        
        var newNameXH=newTR.insertCell(i++);//选择
        newNameXH.className="tdColInputCenter";
        newNameXH.innerHTML="<input name='chk' id='chk" + rowID + "' size='10' value="+rowID+" type='checkbox'   onclick=\"Isquanxuan();\" />";
      
        var newNameTD=newTR.insertCell(i++);//序号
        newNameTD.className="cell";
        newNameTD.id = "newNameTD"+rowID;
        newNameTD.innerHTML =GenerateNo(rowID);
        
        var newProductNo=newTR.insertCell(i++);//物品ID
        newProductNo.style.display = "none";      
        newProductNo.innerHTML = "<input name='txtProductID" + rowID + "'  id='txtProductID" + rowID + "'  value='"+ID+"' type='text' style='width:100%'  /><input type='hidden' id='hidIsBatchNo"+rowID+"' value='"+IsBatchNo+"' ><input type='hidden' id='hidBatchNo"+rowID+"'  >";//添加列内容
        
        var newProductNo=newTR.insertCell(i++);//物品编号
        newProductNo.className="proID";        
        newProductNo.innerHTML = "<input name='txtProductNo" + rowID + "'  id='txtProductNo" + rowID + "' value='"+ProdNo+"' readonly onclick=\"popTechObj.ShowList('txtProductNo"+rowID+"');\" type='text' style='width:100%'  readonly />";//添加列内容
       
        var newProductName=newTR.insertCell(i++);//物品名称
        newProductName.className="proID";
        newProductName.innerHTML = "<input name='txtProductName" + rowID + "' id='txtProductName" + rowID + "'  value='"+ProductName+"' type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        var newstandard=newTR.insertCell(i++);//规格(从物品表中带来显示，不往明细表中存)
        newstandard.className="proID";
        newstandard.innerHTML = "<input name='txtstandard" + rowID + "' id='txtstandard" + rowID + "'  value='"+Specification+"' type='text'  style='width:100%'  readonly  disabled/>";//添加列内容
        
        var newUnitID=newTR.insertCell(i++);//单位ID
        newUnitID.style.display = "none";
        newUnitID.innerHTML = "<input name='txtUnitID"+rowID+"' id='txtUnitID" + rowID + "'  value='"+UnitID+"' style='width:10%' type='text'  class='tdinput' readonly />";//添加列内容
        
        var newUnitName=newTR.insertCell(i++);//单位名称或者基本单位
        newUnitName.className="proID";
        newUnitName.innerHTML = "<input name='txtUnitName" + rowID + "' id='txtUnitName" + rowID + "'  value='"+CodeName+"' type='text' style='width:100%'  readonly disabled/>";//添加列内容
       
        //计量单位开启
        if ($("#txtIsMoreUnit").val() == "1") 
        {
            var newBackCount = newTR.insertCell(i++); //基本数量
            newBackCount.className = "proID";
            newBackCount.innerHTML = "<input name='txtSendCount" + rowID + "'  id='txtSendCount" + rowID + "'   style='width:100%' type='text' readonly />";

            var newBackCount1 = newTR.insertCell(i++); //单位
            newBackCount1.className = "proID";
            newBackCount1.id = "td_UsedUnitID_" + rowID;
            newBackCount1.innerHTML = "";
            
            var newBackCount = newTR.insertCell(i++); //入库数量
            newBackCount.className = "proID";
            newBackCount.innerHTML = "<input name='txtUsedUnitCount" + rowID + "'  id='txtUsedUnitCount" + rowID + "' value='"+FormatAfterDotNumber(1, $("#hidSelPoint").val())+"' onblur=\"zhuanhuanpp();\" style='width:100%'  type='text'/>";
           
            GetUnitGroupSelectEx(ID, 'StockUnit', 'txtUsedUnit' + rowID, 'ChangeUnit(this,' + rowID + ')', "td_UsedUnitID_" + rowID, "",'ChangeUnit(this,' + rowID + ')');
        }
        else 
        {
            var newBackCount = newTR.insertCell(i++); //入库数量
            newBackCount.className = "proID";
            newBackCount.innerHTML = "<input name='txtSendCount" + rowID + "'  id='txtSendCount" + rowID + "'   value='"+FormatAfterDotNumber(1, $("#hidSelPoint").val())+"'  onblur=\"zhuanhuanpp();\" style='width:100%'  type='text'/>"; 
        }
        
   document.getElementById('txtTRLastIndex').value= (rowID + 1).toString() ;//将行号推进下一行
    return rowID;
}


/***************************************************
*切换单位
***************************************************/
function ChangeUnit(own, rowid) 
{
    CalCulateNum('txtUsedUnit' + rowid, "txtUsedUnitCount" + rowid, "txtSendCount" + rowid, "", "", $("#hidSelPoint").val());
    zhuanhuanpp();
}
   