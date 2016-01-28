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
    
    $(document).ready(function()
{
    requestobj = GetRequest(); 
    var CustNo = requestobj['CustNo'];
    var CustName = requestobj['CustName'];
    var CustNam = requestobj['CustNam'];
    var PYShort = requestobj['PYShort'];
    var CustType = requestobj['CustType'];
    var CustClass = requestobj['CustClass'];
    var CustClassName = requestobj['CustClassName'];
    var AreaID = requestobj['AreaID'];
    var CreditGrade = requestobj['CreditGrade'];
    var Manager = requestobj['Manager'];
    var ManagerName = requestobj['ManagerName'];
    var StartCreateDate = requestobj['StartCreateDate'];
    var EndCreateDate = requestobj['EndCreateDate'];
  
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    
//    if(typeof(ContractNo1)!="undefined")
    if(typeof(Isliebiao)!="undefined")
    { 
       $("#txtCustNo").attr("value",CustNo);
       $("#txtCustName").attr("value",CustName);
       $("#txtCustNam").attr("value",CustNam);
       $("#txtPYShort").attr("value",PYShort);
       $("#drpCustType").attr("value",CustType);
       $("#txtCustClass").attr("value",CustClass);
       $("#txtCustClassName").attr("value",CustClassName);
       $("#drpAreaID").attr("value",AreaID);
       $("#drpCreditGrade").attr("value",CreditGrade);
       $("#HidManager").attr("value",Manager);
       $("#UsertxtManager").attr("value",ManagerName);
       $("#txtStartCreateDate").attr("value",StartCreateDate);
       $("#txtEndCreateDate").attr("value",EndCreateDate);
       currentPageIndex = PageIndex;
       currentpageCount = PageCount;
       SearchProviderInfoData();
    }
});

    
    

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
//           var CustNo = document.getElementById("txtCustNo").value;//供应商编号
//            var CustName = document.getElementById("txtCustName").value;//供应商名称
//            var CustNam = document.getElementById("txtCustNam").value;//供应商简称
//            var PYShort = document.getElementById("txtPYShort").value;//拼音缩写
//            var CustType = document.getElementById("drpCustType").value;//供应商类别
//            if(CustType == 0)
//            {
//                CustType="";
//            }
//            var CustClass = document.getElementById("txtCustClass").value;//供应商分类id
//            if(CustClass == undefined)
//            {
//                CustClass="";
//            }
//            var CustClassName = document.getElementById("txtCustClassName").value;//供应商分类名称
//            var AreaID = document.getElementById("drpAreaID").value;//所在区域
//            if(AreaID == 0)
//            {
//                AreaID="";
//            }
//            var CreditGrade = document.getElementById("drpCreditGrade").value;//优质级别
//            if(CreditGrade == 0)
//            {
//                CreditGrade="";
//            }
//            var Manager = document.getElementById("HidManager").value;//分管采购员id
//            if(Manager == undefined)
//            {
//                Manager="";
//            }
//            var ManagerName = document.getElementById("UsertxtManager").value;//分管采购员名称
//            var StartCreateDate = document.getElementById("txtStartCreateDate").value;//建挡日期起
//            var EndCreateDate = document.getElementById("txtEndCreateDate").value;//建挡日期止
           
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/ProviderInfoInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+currentpageCount+"&action="+action+"&orderby="+orderBy+
                    document.getElementById("hidSearchCondition").value,
//                 "&CustNo="+escape(CustNo)+"&CustName="+escape(CustName)+"&CustNam="+escape(CustNam)+
//                 "&PYShort="+escape(PYShort)+"&CustType="+escape(CustType)+"&CustClass="+escape(CustClass)+
//                 "&AreaID="+escape(AreaID)+"&CreditGrade="+escape(CreditGrade)+"&Manager="+escape(Manager)+
//                 "&StartCreateDate="+escape(StartCreateDate)+"&EndCreateDate="+escape(EndCreateDate)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var j =1;
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        var CustName = item.CustName;
                        if (CustName != null) {
                        if (CustName.length > 6) {
                            CustName = CustName.substring(0, 6) + '...';
                            }
                        }
                        var CustNam = item.CustNam;
                        if(CustNam != null){
                        if (CustNam.length > 6) {
                            CustNam = CustNam.substring(0, 6) + '...';
                            }
                        }
                        var PYShort = item.PYShort;
                        if(PYShort != null){
                        if (PYShort.length > 6) {
                            PYShort = PYShort.substring(0, 6) + '...';
                            }
                        }
                        var CustNo = item.CustNo;
                        if(CustNo != null){
                        if (CustNo.length > 20) {
                            CustNo = CustNo.substring(0, 20) + '...';
                            }
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1'  Title='" + item.CustNo + "' name='Checkbox1'  value="+j+"  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />"+"</td>"+
                        "<td height='22' align='center' style='display:none'>"+(j++)+"</td>"+
//                        "<td height='22' align='center'><a href='" + GetLinkParam() +"&FixID=" + item.ID + "')>"+item.FixNo+"</a></td>"+
                        //"<td height='22' align='center'><a href='#' onclick=SelectPurchaseContract('"+item.ContractNo+"')>"+ item.ContractNo +"</a></td>"+
                        "<td height='22' align='center'><a href='" + GetLinkParam() +"&intMasterProviderID=" + item.ID + "')><span title=\"" + item.CustNo + "\">"+CustNo+"</a></td>"+
                        //"<td height='22' align='center'><a href='#' onclick=GetLinkParam('"+item.ContractNo+"')>"+item.ContractNo+"</a></td>"+

                        "<td height='22' align='center'><span title=\"" + item.CustName + "\">"+ CustName +"</a></td>"+
                        "<td height='22' align='center'><span title=\"" + item.CustNam + "\">"+ CustNam +"</a></td>"+
//                        "<td height='22' align='center'>"+ item.CustType +"</a></td>"+
                        "<td height='22' align='center'><span title=\"" + item.PYShort + "\">"+ PYShort +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustTypeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CustClassName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.AreaName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.ManagerName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CreditGradeName +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CreatorName +"</a></td>"+
                        //"<td height='22' align='center' style='display:none'>"+ item.BillStatus +"</a></td>"+
                        "<td height='22' align='center'>"+ item.CreateDate +"</a></td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
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
           complete:function(){hidePopup();$("#pageDataList1_PagerList").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

/*
* 获取链接的参数
*/
function GetLinkParam()
{
//    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    
    linkParam = "ProviderInfo_Add.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + currentpageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
 
}

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

function SearchProviderInfoData()
    {
    if(!fnCheck())
    return;
//检索条件
issearch=1;

    var CustNo = document.getElementById("txtCustNo").value.Trim();//供应商编号
    var CustName = document.getElementById("txtCustName").value.Trim();//供应商名称
    var CustNam = document.getElementById("txtCustNam").value.Trim();//供应商简称
    var PYShort = document.getElementById("txtPYShort").value.Trim();//拼音缩写
    var CustType = document.getElementById("drpCustType").value.Trim();//供应商类别
    if(CustType == 0)
    {
        CustType="";
    }
    var CustClass = document.getElementById("txtCustClass").value.Trim();//供应商分类id
    if(CustClass == undefined)
    {
        CustClass="";
    }
    var CustClassName = document.getElementById("txtCustClassName").value.Trim();//供应商分类id
    var AreaID = document.getElementById("drpAreaID").value.Trim();//所在区域
    if(AreaID == 0)
    {
        AreaID="";
    }
    var CreditGrade = document.getElementById("drpCreditGrade").value.Trim();//优质级别
    if(CreditGrade == 0)
    {
        CreditGrade="";
    }
    var Manager = document.getElementById("HidManager").value.Trim();//分管采购员id
    if(Manager == undefined)
    {
        Manager="";
    }
    var ManagerName = document.getElementById("UsertxtManager").value.Trim();//分管采购员名称
    var StartCreateDate = document.getElementById("txtStartCreateDate").value.Trim();//建挡日期起
    var EndCreateDate = document.getElementById("txtEndCreateDate").value.Trim(); //建挡日期止
    if (StartCreateDate != "" && EndCreateDate != "") {
        if (StartCreateDate > EndCreateDate) {
            alert("起始时间不能大于终止时间!");
            return;
        }
    }
    
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&CustNo="+escape(CustNo)+"&CustName="+escape(CustName)+"&CustNam="+escape(CustNam)+"&PYShort="+escape(PYShort)+
                     "&CustType="+escape(CustType)+"&CustClass="+escape(CustClass)+"&CustClassName="+escape(CustClassName)+"&AreaID="+escape(AreaID)+"&CreditGrade="+escape(CreditGrade)+
                     "&Manager="+escape(Manager)+"&ManagerName="+escape(ManagerName)+"&StartCreateDate="+escape(StartCreateDate)+"&EndCreateDate="+escape(EndCreateDate)+""; 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
      search="1";
      TurnToPage(currentPageIndex);
    }
function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtContractNo").value = "";
    document.getElementById("txtTitle").value = "";
    document.getElementById("DrpTypeID").value = "";
    document.getElementById("txtHidOurDept").value = "";
    document.getElementById("txtDeptID").value = "";
    document.getElementById("txtSeller").value = "";
    document.getElementById("txtSeller").title = "";
    document.getElementById("ddlFromType").value = "0";
    document.getElementById("txtHidProviderID").value = "";
    document.getElementById("txtProviderID").value = "";
    document.getElementById("ddlBillStatus").value = "0";
    document.getElementById("ddlUsedStatus").value = "0";
    
    document.getElementById("pagecount").style.display = "none";
//    document.getElementById("divpage").style.display = "none";
    document.getElementById("pageDataList1_PagerList").style.display = "none";
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
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//            return false;
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
 
    //新建供应商档案
   function CreateProviderInfo()
   {
 
    var CustNo = document.getElementById("txtCustNo").value.Trim();//供应商编号
    var CustName = document.getElementById("txtCustName").value.Trim();//供应商名称
    var CustNam = document.getElementById("txtCustNam").value.Trim();//供应商简称
    var PYShort = document.getElementById("txtPYShort").value.Trim();//拼音缩写
    var CustType = document.getElementById("drpCustType").value.Trim();//供应商类别
    if(CustType == 0)
    {
        CustType="";
    }
    var CustClass = document.getElementById("txtCustClass").value.Trim();//供应商分类id
    if(CustClass == undefined)
    {
        CustClass="";
    }
    var CustClassName = document.getElementById("txtCustClassName").value.Trim();//供应商分类名称
    var AreaID = document.getElementById("drpAreaID").value.Trim();//所在区域
    if(AreaID == 0)
    {
        AreaID="";
    }
    var CreditGrade = document.getElementById("drpCreditGrade").value.Trim();//优质级别
    if(CreditGrade == 0)
    {
        CreditGrade="";
    }
    var Manager = document.getElementById("HidManager").value.Trim();//分管采购员id
    if(Manager == undefined)
    {
        Manager="";
    }
    var ManagerName = document.getElementById("UsertxtManager").value.Trim();//分管采购员名称
    var StartCreateDate = document.getElementById("txtStartCreateDate").value.Trim();//建挡日期起
    var EndCreateDate = document.getElementById("txtEndCreateDate").value.Trim();//建挡日期止
    var ModuleID = document.getElementById("hidModuleID").value.Trim();
//    currentPageIndex = pageIndex;
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&PageIndex=" + currentPageIndex + "&PageCount="+currentpageCount+"&CustNo="+escape(CustNo)+"&CustName="+escape(CustName)+"&CustNam="+escape(CustNam)+"&PYShort="+escape(PYShort)+
                     "&CustType="+escape(CustType)+"&CustClass="+escape(CustClass)+"&CustClassName="+escape(CustClassName)+"&AreaID="+escape(AreaID)+"&CreditGrade="+escape(CreditGrade)+
                     "&Manager="+escape(Manager)+"&ManagerName="+escape(ManagerName)+"&StartCreateDate="+escape(StartCreateDate)+"&EndCreateDate="+escape(EndCreateDate)+"";  
    
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
        window.location.href='ProviderInfo_Add.aspx?'+URLParams ;
   }

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}



function  DelProviderInfo()
{ 
     var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
    var ck = document.getElementsByName("Checkbox1");
    var table=document.getElementById("pageDataList1"); 
    var URLParams = "";  
    var Action = "Delete";
    URLParams+="Action="+Action;
    var index = 0;
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {
//            if(table.rows[ck[i].value].cells[9].innerText !='制单')
//            {
//                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能删除制单状态的单据！");
//                return;
//            }$("#chk" + i).attr("title")   Title
//            URLParams+="&CustNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;
            URLParams+="&CustNo"+(index++)+"="+table.rows[ck[i].value].cells[2].innerText;
            
            
            
        }       
    }
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
        return;
    }
    URLParams+="&Length="+index+"";
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/ProviderInfoInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
            error: function() 
                {
                   popMsgObj.ShowMsg('请求发生错误！');
                   return;
                }, 
           success: function(msg){
                    SearchProviderInfoData();
                    popMsgObj.ShowMsg('删除成功！');
                  },
           error: function() {}, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){}
           });
    }
}
