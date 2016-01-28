
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "ModifiedDate_d";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    
     function closeProviderdiv()
{
    document.getElementById("divProviderInfohhh").style.display="none";
}

    $(document).ready(function()
{
 

    requestobj = GetRequest(); 
    var ContractNo1 = requestobj['ContractNo1'];
    var Title = requestobj['Title'];
    var TypeID = requestobj['TypeID'];
    var DeptID = requestobj['DeptID'];
    if(DeptID == undefined)
    {
        DeptID="";
    }
    var DeptName = requestobj['DeptName'];
    var Seller = requestobj['Seller'];
    var SellerName = requestobj['SellerName'];
    var FromType = requestobj['FromType'];
    var ProviderID = requestobj['ProviderID'];
    var ProviderName = requestobj['ProviderName'];
    var BillStatus = requestobj['BillStatus'];
    var UsedStatus = requestobj['UsedStatus'];
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var pageCount = requestobj['pageCount'];
    
//    if(typeof(ContractNo1)!="undefined")
    if(typeof(Isliebiao)!="undefined")
    { 
       $("#txtContractNo").attr("value",ContractNo1);
       $("#txtTitle").attr("value",Title);
       $("#DrpTypeID").attr("value",TypeID);
       $("#txtHidOurDept").attr("value",DeptID);
       $("#DeptDeptID").attr("value",DeptName);
       $("#HidSeller").attr("title",Seller);
       $("#UsertxtSeller").attr("value",SellerName);
       $("#ddlFromType").attr("value",FromType  == "" ? "-1" : FromType);
       $("#txtHidProviderID").attr("value",ProviderID);
       $("#txtProviderID").attr("value",ProviderName);
       $("#ddlBillStatus").attr("value",BillStatus == "" ? "0" : BillStatus);
       $("#ddlUsedStatus").attr("value",UsedStatus == "" ? "0" : UsedStatus);
       currentPageIndex = PageIndex;
       currentpageCount = pageCount;
       SearchContractData();
    }
       IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
});

function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
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
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
//           var ContractNo=document.getElementById("txtContractNo").value;//合同编号
//           var Title = document.getElementById("txtTitle").value;//合同主题
//           var TypeID = document.getElementById("DrpTypeID").value;//采购类别
//           if(TypeID == 0)
//           {
//                TypeID="";
//           }
//           var DeptID = document.getElementById("txtHidOurDept").value;//部门的id
//            //var result = document.all.SellModuleSelectEmployeeUC1_txtSeller.value;
//            
//           var Seller  = document.getElementById("HidSeller").value;//采购员id 
//           if(Seller == undefined)
//           {
//                Seller ="";
//           }
////           if(Seller == null)
////             {
////                Seller=0
////             }
//           var FromType = document.getElementById("ddlFromType").value;//源单类型id
//           if(FromType == -1)
//           {
//                FromType="";
//           }
//           var ProviderID = document.getElementById("txtHidProviderID").value;//供应商id
//           var BillStatus =document.getElementById("ddlBillStatus").value;//单据状态
//           if(BillStatus == 0)
//           {
//                BillStatus ="";
//           }
//           //var UsedStatus = document.getElementById("ddlUsedStatus").value;//审批状态
//           var shenpizhuangtai = document.getElementById("ddlUsedStatus").value;//审批状态
//           var UsedStatus = "";
//           if(shenpizhuangtai != 0)
//           {
//                UsedStatus = document.getElementById("ddlUsedStatus").value;
//           }
//           else
//           {
//                UsedStatus = "";
//           }
           
           
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx?orderby='+orderBy,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+currentpageCount+"&action="+action+document.getElementById("hidSearchCondition").value,
//                 "&ContractNo="+escape(ContractNo)+"&Title="+escape(Title)+"&TypeID="+escape(TypeID)+
//                 "&DeptID="+escape(DeptID)+"&Seller="+escape(Seller)+"&FromType="+escape(FromType)+
//                 "&ProviderID="+escape(ProviderID)+"&BillStatus="+escape(BillStatus)+
//                 "&UsedStatus="+escape(UsedStatus)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        
                        if(item.ID != null && item.ID != "")
                        var Title = item.Title;
                        if (Title != null) {
                        if (Title.length > 6) {
                            Title = Title.substring(0, 6) + '...';
                            }
                        }
                        var ContractNo = item.ContractNo;
                        if (ContractNo != null) {
                        if (ContractNo.length > 20) {
                            ContractNo = ContractNo.substring(0, 20) + '...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='chk" + i + "' name='Checkbox1'  Title='" + item.ContractNo + "' Class='" + item.Isyinyong + "'  value='" + item.ID + "' type='checkbox'  onclick=IfSelectAll('Checkbox1','checkall')  />"+"</td>"+
//                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
//                        "<td height='22' align='center'><a href='" + GetLinkParam() +"&FixID=" + item.ID + "')>"+item.FixNo+"</a></td>"+
                        //"<td height='22' align='center'><a href='#' onclick=SelectPurchaseContract('"+item.ContractNo+"')>"+ item.ContractNo +"</a></td>"+
                        "<td height='22' align='center'><a href='" + GetLinkParam(ContractNo) +"&ContractNo=" + item.ContractNo.Trim() + "&intMasterPurchaseContractID=" + item.ID + "&Isyinyong=" + item.Isyinyong.Trim() + "&Danjuzhuangtai=" + item.BillStatusName.Trim() + "&Shenpizhuangtai=" + item.UsedStatus.Trim() + "&FromTypeName=" + item.FromTypeName.Trim() + "&Rate=" + item.Rate.Trim() + "')><span title=\"" + item.ContractNo + "\">"+ContractNo+"</a></td>"+
                        //"<td height='22' align='center'><a href='#' onclick=GetLinkParam('"+item.ContractNo+"')>"+item.ContractNo+"</a></td>"+

                        "<td height='22' align='center'><span title=\"" + item.Title + "\">"+ Title +"</a></td>"+
                        "<td height='22' align='center'>"+ item.TypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.EmployeeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustName +"</a></td>"+
                        "<td height='22' align='center'>"+      (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val())+"</a></td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val())+"</a></td>"+
                        "<td height='22' align='center'>"+     (parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val())+"</a></td>"+
                        "<td height='22' align='center'>"+ item.BillStatusName +"</a></td>"+
                        "<td height='22' align='center' style='display:none'>"+ item.Isyinyong +"</a></td>"+
                        "<td height='22' align='center'>"+ item.UsedStatus +"</a></td>"+
                        "<td height='22' align='center' style='display:none'>"+ item.FromTypeName +"</a></td>"+ 
                        "<td height='22' align='center' style='display:none'>"+    (parseFloat(item.Rate)).toFixed($("#HiddenPoint").val())+"</a></td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );document.getElementById("checkall").checked = false;
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(currentpageCount);
                  ShowTotalPage(msg.totalCount,currentpageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		//t[i].onclick=function(){//鼠标点击
			//if(this.x!="1"){
				//this.x="1";//
				//this.style.backgroundColor=d;
			//}else{
			//	this.x="0";
				//this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
			//}
		//}
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }
    
function Isquanxuan()
{
    var signFrame = findObj("pageDataList1",document);
    var quanxuan = true;
    for(var i=1;i<signFrame.rows.length;++i)
        {
            if(document.getElementById("Checkbox1"+i).checked == false)
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


/*
* 获取链接的参数
*/
function GetLinkParam(ContractNo)
{
////    //获取模块功能ID
//    var ModuleID = document.getElementById("hidModuleID").value;
//    //获取查询条件
//    searchCondition = document.getElementById("hidSearchCondition").value;
//    //是否点击了查询标识
//    var flag = "0";//默认为未点击查询的时候
//    if (searchCondition != "") flag = "1";//设置了查询条件时
//    linkParam = "FixAssetInfo_Add.aspx?ModuleID=" + ModuleID 
//                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
//    //返回链接的字符串
//    return linkParam;


//    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
//    linkParam = "PurchaseContract_Add.aspx?ModuleID=" + ModuleID 
//                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
      //返回链接的字符串
     //     return linkParam;
    
    linkParam = "PurchaseContract_Add.aspx?ModuleID=" + ModuleID + "&PageIndex=" + currentPageIndex + "&pageCount="+currentpageCount+"&ContractNo=" + ContractNo + "&PageCount=" + pageCount +"&"+ searchCondition + "&Flag=" + flag;
 
    //返回链接的字符串
    return linkParam;
 
}

function SearchContractData()
    {
    if(!fnCheck())
    return;
//检索条件
  issearch=1;
  
  
  
//           currentPageIndex = pageIndex;
           var ContractNo=document.getElementById("txtContractNo").value.Trim();//合同编号
           var Title = document.getElementById("txtTitle").value.Trim();//合同主题
           var TypeID = document.getElementById("DrpTypeID").value.Trim();//采购类别
           if(TypeID == 0)
           {
                TypeID="";
           }
           var DeptID = document.getElementById("txtHidOurDept").value.Trim();//部门的id
               if(DeptID == undefined)
                {
                    DeptID="";
                }
            
           var Seller  = document.getElementById("HidSeller").value.Trim();//采购员id 
           if(Seller == undefined)
           {
                Seller ="";
           }
           var FromType = document.getElementById("ddlFromType").value.Trim();//源单类型id
           if(FromType == -1)
           {
                FromType="";
           }
           var ProviderID = document.getElementById("txtHidProviderID").value.Trim();//供应商id
           var BillStatus =document.getElementById("ddlBillStatus").value.Trim();//单据状态
           if(BillStatus == 0)
           {
                BillStatus ="";
           }
           var shenpizhuangtai = document.getElementById("ddlUsedStatus").value.Trim();//审批状态
           var UsedStatus = "";
           if(shenpizhuangtai != 0)
           {
                UsedStatus = document.getElementById("ddlUsedStatus").value.Trim();
           }
           else
           {
                UsedStatus = "";
           }
  
  
    var ContractNo1 = document.getElementById("txtContractNo").value.Trim();
    var DeptName = document.getElementById("DeptDeptID").value.Trim();
    var SellerName = document.getElementById("UsertxtSeller").value.Trim();
    var ProviderName = document.getElementById("txtProviderID").value.Trim();
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&ContractNo1="+escape(ContractNo1)+"&Title="+escape(Title)+"&TypeID="+escape(TypeID)+"&DeptName="+escape(DeptName)+"&DeptID="+escape(DeptID)+"&Seller="+escape(Seller)+"&SellerName="+escape(SellerName)+"&FromType="+escape(FromType)+"&ProviderName="+escape(ProviderName)+"&ProviderID="+escape(ProviderID)+"&BillStatus="+escape(BillStatus)+"&UsedStatus="+escape(UsedStatus)+""+"&EFIndex="+escape(EFIndex)+"&EFDesc="+escape(EFDesc); 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
      search="1";
      TurnToPage(currentPageIndex);
    }


    
function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数必须输入正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
            currentpageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if(issearch=="")
            return;
        var ordering = "a";
        //var orderTipDOM = $("#"+orderTip);
        var allOrderTipDOM  = $(".orderTip");
        if( $("#"+orderTip).html()=="↓")
        {
             allOrderTipDOM.empty();
             $("#"+orderTip).html("↑");
        }
        else
        {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#"+orderTip).html("↓");
        }
        orderBy = orderColum+"_"+ordering;
        TurnToPage(1);
    }
 
    //新建采购合同
   function CreatePurchaseContract()
   {
 
    var ContractNo1 = document.getElementById("txtContractNo").value.Trim();
    var Title = document.getElementById("txtTitle").value.Trim();
    var TypeID = document.getElementById("DrpTypeID").value.Trim();
    var DeptID = document.getElementById("txtHidOurDept").value.Trim();

    var DeptName = document.getElementById("DeptDeptID").value.Trim();
    var Seller =  document.getElementById("HidSeller").value.Trim();
    if(Seller == undefined)
    {
        Seller = "";
    }
    var SellerName = document.getElementById("UsertxtSeller").value.Trim();
    var FromType = document.getElementById("ddlFromType").value.Trim();
    var ProviderID = document.getElementById("txtHidProviderID").value.Trim();
    var ProviderName = document.getElementById("txtProviderID").value.Trim();
    var BillStatus = document.getElementById("ddlBillStatus").value.Trim();
    var UsedStatus = document.getElementById("ddlUsedStatus").value.Trim();
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
 
 
    
    //    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value.Trim();
//    currentPageIndex = pageIndex;

    var Isliebiao = 1;
    var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&EFDesc="+escape(EFDesc)+"&EFIndex="+escape(EFIndex)+"&PageIndex=" + currentPageIndex + "&pageCount="+currentpageCount+"&ContractNo1="+escape(ContractNo1)+"&Title="+escape(Title)+"&TypeID="+escape(TypeID)+"&DeptName="+escape(DeptName)+"&DeptID="+escape(DeptID)+"&Seller="+escape(Seller)+"&SellerName="+escape(SellerName)+"&FromType="+escape(FromType)+"&ProviderName="+escape(ProviderName)+"&ProviderID="+escape(ProviderID)+"&BillStatus="+escape(BillStatus)+"&UsedStatus="+escape(UsedStatus)+""; 
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
//    currentPageIndex = pageIndex;
    
   
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
//    linkParam = "PurchaseContract_Add.aspx?ModuleID=" + ModuleID 
//                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
//    //返回链接的字符串
//    return linkParam;
    
       //window.location.href='PurchaseContract_Add.aspx?" + ModuleID"&" + searchCondition + "';
        window.location.href='PurchaseContract_Add.aspx?'+URLParams ;
//    window.location.href='PurchaseContract_Add.aspx?ContractNo='+ContractNo+'&Pages=PurchaseContractInfo.aspx&ContractNo1='+ContractNo1+'&Title='+Title+
//                        '&TypeID='+TypeID+'&DeptID='+DeptID+'&Seller='+Seller+'&FromType='+FromType+'&ProviderID='+ProviderID+'&BillStatus='+BillStatus+'&UsedStatus='+UsedStatus;
   }


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}


function Confirm()
{//确认
    var ck = document.getElementsByName("Checkbox1");
    
    
//    var table=document.getElementById("pageDataList1");
    var URLParams = "";  
    var ActionPlan = "Confirm";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {   
        var ck = document.getElementsByName("Checkbox1");
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[10].innerText !='制单'&&table.rows[ck[i].value].cells[11].innerText !='变更')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能确认制单和变更状态的单据！");
                return;
            }
            URLParams+="&ContractNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//ContractNo
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条或几条数据来确认！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    SearchContractData();
                    popMsgObj.ShowMsg('确认成功！');
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });

}

function  DelContract()
{ 

    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DetailID = '';//id
    var DetailNo = '';//No
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            
            DetailNo += $("#chk" + i).attr("title") + ',';
            DetailID += $("#chk" + i).val() + ',';
//            var orderNo = $("#chk" + i).val();
            var FromBillText = $.trim($("#chk" + i).attr("class"));//是否引用，返回True和False
            var BillStatusText = pageDataList1.rows[i + 1].cells[9].innerText;
            var FlowInstanceText = pageDataList1.rows[i + 1].cells[11].innerText;
            if ($.trim(BillStatusText) != '制单' || $.trim(FlowInstanceText) != '' || FromBillText == 'True') {
                isFlag = false;
                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
//                msgText = msgText + "已被其他单据引用，不可删除|";
                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
            }
//            if ($.trim(BillStatusText) != '制单') {
//                isFlag = false;
//                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
////                msgText = msgText + "单据状态为" + $.trim(BillStatusText) + "状态，不可删除|";
//                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
//            }
//            if ($.trim(FlowInstanceText) != '') {
//                isFlag = false;
//                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
////                msgText = msgText + "审批状态为" + $.trim(FlowInstanceText) + "状态，不可删除|";
//                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
//            }
//            if (FromBillText == 'True') {
//                isFlag = false;
//                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
////                msgText = msgText + "已被其他单据引用，不可删除|";
//                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
//            }
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {
        if (isFlag) {
            if (confirm("确认执行删除操作吗？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx",
                    data: "ActionPlan=Delete&DetailNo=" + escape(DetailNo),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

                    error: function() {

                        popMsgObj.ShowMsg('请求发生错误！');
                    },
                    success: function(data) {
                        if (data.sta == 1) {
                            popMsgObj.ShowMsg('删除成功！');
                            SearchContractData();
                        }
                        else {
                            popMsgObj.ShowMsg('删除失败！');
                        }
                    }
                });
            }
        }
        else {
            popMsgObj.Show(fieldText, msgText);
        }
    }
    
    
//    var c=window.confirm("确认执行删除操作吗？")
//    if(c==true)
//    {
//    var ck = document.getElementsByName("Checkbox1");
//    var table=document.getElementById("pageDataList1");
//    var URLParams = "";  
//    var isErrorFlag = false;
//    var msgText = "";
//    var ActionPlan = "Delete";
//    URLParams+="ActionPlan="+ActionPlan;
//    var index = 0;
//    for( var i = 0; i < ck.length; i++ )
//    {
//        var ck = document.getElementsByName("Checkbox1");
//        if ( ck[i].checked )
//        {
//            if(table.rows[ck[i].value].cells[10].innerText =='制单' && table.rows[ck[i].value].cells[12].innerText =='')
//            {
//                URLParams+="&ContractNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;
//            }
//            else
//            {
//                isErrorFlag = true;
//                msgText += "第"+(i+1)+"条数据不能删";
//            }
//        }       
//    }
//    if(isErrorFlag)
//    {
//        popMsgObj.ShowMsg(msgText);
//        return;
//    }
//    if(index == 0)
//    {
//         msgText +="请选择要删除的采购合同！";
//         popMsgObj.ShowMsg(msgText);
//         return;
//    }
//    URLParams+="&Length="+index+"";
//    
//    $.ajax({
//           type: "POST",//用POST方式传输
//           dataType:"json",//数据格式:JSON
//           url:  '../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx?'+URLParams,//目标地址
//           cache:false,
//           beforeSend:function(){},//发送数据之前
//            error: function() 
//                {
//                   popMsgObj.ShowMsg('请求发生错误！');
//                   return;
//                }, 
//           success: function(msg){
//                    SearchContractData();
//                    popMsgObj.ShowMsg('删除成功！');
//                  },
//           error: function() {}, 
////           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
//           complete:function(){}
//           });
//    }
}

function ClosePurContract()
{//结单
    var ck = document.getElementsByName("Checkbox1");
    
    var table=document.getElementById("pageDataList1");
    var URLParams = "";  
    var ActionPlan = "Close";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[10].innerText !='执行')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能对执行状态的单据进行结单操作！");
                return;
            }
            URLParams+="&ContractNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//ContractNo
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条或几条数据来结单！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    SearchContractData();
                    popMsgObj.ShowMsg('结单成功！');
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
}


function CancelClosePurContract()
{//取消结单
    var ck = document.getElementsByName("Checkbox1");
    
    var table=document.getElementById("pageDataList1");
    var URLParams = "";  
    var ActionPlan = "CancelClose";
    URLParams+="ActionPlan="+ActionPlan;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
            if(table.rows[ck[i].value].cells[10].innerText !='手工结单'&&table.rows[ck[i].value].cells[10].innerText !='自动结单')
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能对结单状态的单据进行取消结单操作！");
                return;
            }
            URLParams+="&ContractNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;//ContractNo
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条或几条数据来取消结单！");
        return;
    }
    URLParams+="&Length="+index+"";
        
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseContractInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
           
           success: function(msg){
                    SearchContractData();
                    popMsgObj.ShowMsg('取消结单成功！');
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
}

function clearProviderdiv()
{
    document.getElementById("txtProviderID").value = "";
    document.getElementById("txtHidProviderID").value = "";
    closeProviderdiv();
}

////屏蔽输入的特殊字符 add by songfei
//var arr=new Array();   
//arr[0]=/[\`\~\!\@\#\$\%\^\&\*\(\)\+\\\]\[\}\{\'\;\:\"\/\.\,\>\<\]\s\|\=\-\?]/g;   
//arr[1]=/[^\d]/g;   
//function filtecharacter(obj, index) {   
//    obj.value = obj.value.replace(arr[index], "");   
//} 