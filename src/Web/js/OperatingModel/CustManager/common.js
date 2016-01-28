//Ajax操作

function AjaxHandle(PageIndex,Action,Condition,PageAddress)
{   
      $.ajax({
       type: "POST",//用POST方式传输
       dataType:"string",//数据格式:JSON
       url:  '../../../Handler/OperatingModel/CustManager/'+PageAddress,//目标地址
       cache:false,
       data: "pageIndex="+PageIndex+"&pageSize="+pageCount+"&action="+Action+"&"+Condition,//数据
       beforeSend:function()
                  {
                    AddPop();
                    $("#pageDataList1_Pager").hide();
                  },//发送数据之前
       success: function(data1)
                {  //alert(data1);
                    var msg = null;
                    
                    eval("msg = "+data1);
                    
                    DoData(msg.data);//数据处理
                    
                    PageHandle(msg.data.count,PageIndex);//分页处理
                
               },
       error: function() 
               {
                 showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
               },
       complete:function()
               {
                 CompleteHandle(totalRecord);
               }
       });   
}

function MsgBox(val)
{
   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",val);
}

//分页处理
function PageHandle(RecordCount,PageIndex)
{
    ShowPageBar("pageDataList1_Pager","",{style:"flickr",mark:"",totalCount:RecordCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:PageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
    onclick:"TurnToPage({pageindex});return false;"}
    );
    totalRecord = RecordCount;
    $("#ShowPageCount").val(pageCount);
    $("#ToPage").val(PageIndex);
    ShowTotalPage(RecordCount,pageCount,PageIndex,$("#pagecount"));
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

//数据加载完后的处理
function CompleteHandle(count)
{   
    hidePopup();//隐藏数据加载框
    pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");//切换行色
    $("#pageDataList1_Pager").show();
    if(count=="0")
    {
        $("#divpage").hide();
        $("#pagecount").hide(); 
    }
    else
    {
        $("#divpage").show();
        $("#pagecount").show();
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
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
        pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}

//排序
function OrderBy(orderColum,orderTip)
{
     if(Condition == "")
     {
        popMsgObj.Show("排序|","请检索数据后再排序|");
        return;
     }
    ifdel = "0";
    var ordering = "a";
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


//输入字符串处理
function FormatStr(str)
{
    str=str.replace(/\'/g,"''");
    str=str.replace(/%/g,"''%");
    return str;
}


//显示隐藏栏目
function oprItem(itemId,clickId)
{  
	if(document.getElementById(itemId).style.display== 'none')
	{
		document.getElementById(itemId).style.display="";
		document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Close.jpg" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem("'+ itemId +'","'+ clickId +'")></img>';
	}
	else
	{
		document.getElementById(itemId).style.display= 'none';
	    document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem("'+ itemId +'","'+ clickId +'")></img>';
	}
}