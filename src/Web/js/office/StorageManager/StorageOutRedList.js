var point="";
$(document).ready(function()
{
    point=$("#HiddenPoint").val();
    IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
    if ($("#IsDisplayPrice").val() == "true")//是否显示表头金额
                     {
                     }
                    else{$("#tr_Money").hide();}
          
});
    /*
* 查询
*/
function DoSearch(currPage)
{

   var fieldText = "";
   var msgText = "";
   var isFlag = true;

   var OutNo= document.getElementById("txtOutNo").value;
           var Title=document.getElementById("txtTitle").value;
           var FromType=document.getElementById("sltFromType").value;
           var FromOutID=$("#hidFromBillID").val();//原始出库单ID
           var sltBillStatus=document.getElementById("sltBillStatus").value;
           var Transactor = $("#txtOuterID").val();
           var OutDateStart=document.getElementById("txtOutDateStart").value;
           var OutDateEnd=document.getElementById("txtOutDateEnd").value;
           var FromOutNo=$("#txtFromBillID").val();//原始出库单
           var UserOuter=$("#UserOuter").val();
           
       var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
       var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
       var BatchNo=$("#txtBatchNo").val();//批次
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
	        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
   if(CompareDate(OutDateStart, OutDateEnd)==1)
   {
        isFlag=false;
        fieldText=fieldText + "查询时间段|";
        msgText = msgText +  "起始时间不能大于终止时间|";
   }
//    if(EFIndex!="" && EFDesc=="")
//       {
//            isFlag=false;
//            fieldText=fieldText + "其他条件|";
//            msgText = msgText +  "其他条件不能为空|";
//       }
//        if(EFIndex=="" && EFDesc!="")
//       {
//            isFlag=false;
//            fieldText=fieldText + "其他条件|";
//            msgText = msgText +  "请选择其他条件|";
//       }
   if(!isFlag)
   {
        popMsgObj.Show(fieldText,msgText);
        return;
   }       
           
           var UrlParam="&action="+action+
           "&OutNo="+escape(OutNo)+"&Title="+escape(Title)+
           "&BillStatus="+escape(sltBillStatus)+
           "&FromType="+escape(FromType)+
           "&FromOutID="+escape(FromOutID)+
           "&Transactor="+escape(Transactor)+
           "&OutDateStart="+escape(OutDateStart)+"&OutDateEnd="+escape(OutDateEnd)+
           "&FromOutNo="+escape(FromOutNo)+"&UserOuter="+escape(UserOuter)+
           "&EFIndex="+escape(EFIndex)+
           "&EFDesc="+escape(EFDesc)+
           "&BatchNo="+escape(BatchNo)+
           "&Flag=1";
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage,10));
    }
}
 /*-----------------------------------------DoSearch结束----------------------------------------------*/

   
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           $("#checkall").attr("checked",false);
           currentPageIndex = pageIndex;
           //获取查询条件
           var searchCondition = document.getElementById("hidSearchCondition").value;
           var UrlParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageOutRedList.ashx?'+UrlParam,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var td="";
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != ""){
                        if ($("#IsDisplayPrice").val() == "true")
                         {
                            td="<td height='22' align='center' title=\""+parseFloat(item.TotalPrice).toFixed(point)+"\">"+parseFloat(item.TotalPrice).toFixed(point)+"</td>";
                         }
                        else{$("#tr_Money").hide();
                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='OptionCheck_"+item.ID+"' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.OutNo+"\"><a href='" + GetLinkParam() + "&OutNoID="+item.ID+"'>"+ fnjiequ(item.OutNo,16) +"</a></td>"+
                        "<td height='22' align='center' title=\""+item.Title+"\">"+ fnjiequ(item.Title,10) +"</td>"+
//                        "<td height='22' align='center' title=\""+item.BatchNo+"\">"+ fnjiequ(item.BatchNo,10) +"</td>"+
                        "<td height='22' align='center'>"+item.FromTypeName+"</td>"+
                        "<td height='22' align='center' title=\""+item.FromOutNo+"\">"+fnjiequ(item.FromOutNo,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.Transactor+"\">"+fnjiequ(item.Transactor,10)+"</td>"+
                        "<td height='22' align='center'>"+item.OutDate+"</td>"+
                        "<td height='22' align='center' title=\""+parseFloat(item.CountTotal).toFixed(point)+"\">"+parseFloat(item.CountTotal).toFixed(point)+"</td>"+
                        td+
                        "<td height='22' align='center'>"+item.BillStatusName+"</td>").appendTo($("#pageDataList1 tbody"));}
                        
                   });
                 
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  $("#Text2").val(msg.totalCount);
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow($("#Text2").val());pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Fun_Search_StorageOutRed(aa)
{
      search="1";
      TurnToPage(1);
}

function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById('divpage').style.display = "none";
            document.getElementById('pagecount').style.display = "none";
        }
        else
        {
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pagecount').style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
    
        
            //判断是否是数字
        if (!PositiveInteger(newPageCount))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
            return;
        }
        if (!PositiveInteger(newPageIndex))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
            return;
        }
    
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount,10);
            TurnToPage(parseInt(newPageIndex,10));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
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
        $("#txtorderBy").val(orderBy);//把排序字段放到隐藏域中，
        
        TurnToPage(1);
    }

var OptionCheckArray=new Array();
function OptionCheckOnes(obj)
{
    with(obj)
    {
        if(checked)
        {
            OptionCheckArray.push(value);
        }
        else
        {
            var OptionCheckArrayTM=new Array();
            for(var i=0;i<OptionCheckArray.length;i++)
            {
                if(OptionCheckArray[i]!=value)
                {
                    OptionCheckArrayTM.push(OptionCheckArray[i]);
                }
            }
            OptionCheckArray=OptionCheckArrayTM;
        }        
    }
}

function Fun_Delete_StorageOutRed()
{
    if(confirm('删除后不可恢复，你确定要删除吗？'))
     {
      var ck = document.getElementsByName("Checkbox1");
            var ck2 = "";    
            for( var i = 0; i < ck.length; i++ )
            {
                if ( ck[i].checked )
                {
                   ck2 += ck[i].value+',';
                }
            }
            var IDArray = ck2.substring(0,ck2.length-1);
            x = ck2.split(',');
            if(x.length-1<=0)
            {
               showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
                return;
            }
            else
            {
                var Act='Del';
                var UrlParam = "Act="+Act+"\
                                &strID="+IDArray;
                    $.ajax({ 
                          type: "POST",
                          url: "../../../Handler/Office/StorageManager/StorageOutRedAdd.ashx?"+UrlParam,
                          dataType:'json',//返回json格式数据
                          cache:false,
                          beforeSend:function()
                          { 
                             //AddPop();
                          }, 
                          //complete :function(){ //hidePopup();},
                          error: function() 
                          {
                            popMsgObj.ShowMsg('请求发生错误');
                            
                          }, 
                          success:function(data) 
                          { 
                            popMsgObj.ShowMsg(data.info);
                           
                            if(data.sta>0)
                            {
                                Fun_Search_StorageOutRed();
                            }
                          } 
                       });
             }
     }
   else
   {
      return false;
   }
}

/*---------------------弹出销售发货通知单层---------------------*/
var fnStorageOut=new Object();
fnStorageOut.objControlID = null;
function fnStorageOutList(objControlID)//保存传过来的SendNo控件ID
{
    if(document.getElementById('sltFromType').value!="")
    {
       fnStorageOut.objControlID = objControlID;
       document.getElementById("divStorageOutUC").style.display='block';
       TurnToOffPage(currentOffPageIndex);
    }
    else
    {
        Closediv();    
    }
}

function Closediv()
{
    document.getElementById("divStorageOutUC").style.display="none";
}

function FillFromOutNo(FromOutNo,FromOutID)//给保存好的控件赋值
{
    document.getElementById(fnStorageOut.objControlID).value = FromOutNo;
    $("#"+fnStorageOut.objControlID).attr("title",FromOutID);
    $("#hidFromBillID").val(FromOutID);
    Closediv();
}

function ClearFromOutNo()
{
    document.getElementById(fnStorageOut.objControlID).value = "";
    $("#"+fnStorageOut.objControlID).attr("title","");
    $("#hidFromBillID").val("");
    Closediv();
}



    var pageOffCount = 10;//每页计数
    var totalOffRecord = 0;
    var pagerOffStyle = "flickr";//jPagerBar样式
    var currentOffPageIndex = 1;
    var action = "";//操作
    var orderByOff = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToOffPage(pageIndex)
    {
           var OutType=document.getElementById("sltFromType").value;//这个不能放在外面，JS加载顺序问题，会找不到对象
           currentOffPageIndex = pageIndex;        
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageOutRedList.ashx?'
                + "pageIndex="+pageIndex+"&pageOffCount="+pageOffCount+"&orderByOff="+orderByOff+"&txtNo_UC="+escape($("#txtNo_UC").val())+"&txtTitle_UC="+escape($("#txtTitle_UC").val())+"&action=getStorageOutList&OutType="+OutType,//目标地址
           cache:false,
           beforeSend:function(){$("#pageOffList_Pager").show();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var index=1;
                    $("#offerDataList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"rd\" id=\"rdInType_"+index+"\" value=\""+item.ID+"\" onclick=\"FillFromOutNo('"+item.OutNo+"','"+item.ID+"')\" />"+"</td>"+
                             "<td height='22' align='center'>"+ item.FromType +"</td>"+
                             "<td height='22' align='center'>"+ item.OutNo +"</td>"+
                             "<td height='22' align='center'>"+ item.Title +"</a></td>"+
                             "<td height='22' align='center'>"+item.CreatDate+"</td>").appendTo($("#offerDataList tbody"));
                             index=index+1;
                        }
                   });
                    //页码
                   ShowPageBar("pageOffList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerOffStyle
                    ,mark:"pageOffDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageOffCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    End:"末页",
                    First:"首页",
                    onclick:"TurnToOffPage({pageindex});return false;"}//[attr]
                    );
                  totalOffRecord = msg.totalCount;
                  $("#ShowOffPageCount").val(pageOffCount);
                  ShowTotalPage(msg.totalCount,pageOffCount,pageIndex,$("#pageOffcount"));
                  $("#OffToPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageOffList_Pager").show();offerDataList("offerDataList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
    //table行颜色
function offerDataList(o,a,b,c,d)
{
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

//改变每页记录数及跳至页数
function ChangeOffPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalOffRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageOffCount=parseInt(newPageCount,10);
        TurnToOffPage(parseInt(newPageIndex,10));
    }
}
//排序
function OrderOffBy(orderColum,orderTip)
{
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderByOff = orderColum+"_"+ordering;
    TurnToOffPage(1);
}


/*------------------------------------新建-----------------------------------*/
/*
* 新建
*/
function DoNew()
{
    window.location.href = GetLinkParam();
}

/*
* 获取链接的参数
*/
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    linkParam = "StorageOutRedAdd.aspx?ModuleID=" + ModuleID 
                            + "&pageIndex=" + currentPageIndex + "&pageCount=" + pageCount +"&orderBy=" + orderBy + "&" + searchCondition;
    //返回链接的字符串
    return linkParam;
}
/*------------------------------------新建-----------------------------------*/

/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/