    /*
* 查询
*/
function DoSearch(currPage)
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

   var DeptID= document.getElementById("hidtxtSubCom").value;
   var DateStart=document.getElementById("txtDateStart").value;
   var DateEnd=document.getElementById("txtDateEnd").value;
   
   
   if(CompareDate(DateStart, DateEnd)==1)
   {
        isFlag=false;
        fieldText=fieldText + "查询时间段";
        msgText = msgText +  "起始时间不能大于终止时间";
   }
   if(!isFlag)
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",fieldText+":"+msgText);
        //popMsgObj.Show(fieldText,msgText);
        return;
   }
   
   var UrlParam="Act=search"+
   "&DeptID="+escape(DeptID)+"&DateStart="+escape(DateStart)+
   "&DateEnd="+escape(DateEnd);
    
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
 /*-----------------------------------------上面是查询-----------------------------------------------------------*/     
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
             //进行查询获取数据 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputCompanyRoyalty.ashx?'+UrlParam,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='ckb_"+item.ID+"' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a style=\"cursor:pointer\" onclick=ShowNewDiv(\""+item.ID+"\",\""+item.DeptID+"\",\""+item.DeptName+"\",\""+item.BusinessMoney+"\",\""+item.RecordDate+"\")>查看</a></td>"+
                        "<td height='22' align='center' title=\""+item.DeptName+"\">"+ fnjiequ(item.DeptName,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.BusinessMoney+"\">"+fnjiequ(item.BusinessMoney,10)+"</td>"+
                        "<td height='22' align='center'>"+item.RecordDate+"</td>").appendTo($("#pageDataList1 tbody"));
                        
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  document.getElementById("Text2").value=msg.totalCount;
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
    orderBy = orderColum+"_"+ordering;
    TurnToPage(1);
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.all["divpage"].style.display = "none";
        document.all["pagecount"].style.display = "none";
    }
    else
    {
        document.all["divpage"].style.display = "block";
        document.all["pagecount"].style.display = "block";
    }
}

/*------------------------------------------删除 Start-------------------------------------------*/
function DoDel()
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
                var UrlParam = "Act="+Act+"&strID="+IDArray;
                    $.ajax({ 
                          type: "POST",
                          url: "../../../Handler/Office/HumanManager/InputCompanyRoyalty.ashx?"+UrlParam,
                          dataType:'json',//返回json格式数据
                          cache:false,
                          beforeSend:function()
                          { 
                             AddPop();
                          }, 
                          //complete :function(){ //hidePopup();},
                          error: function() 
                          {
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                            //popMsgObj.ShowMsg('请求发生错误');
                            
                          }, 
                          success:function(data) 
                          { 
                            
                            //popMsgObj.ShowMsg(data.info);
                           
                            if(data.sta>0)
                            {
                                TurnToPage(1);
                            }
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
                          } 
                       });
             }
     }
   else
   {
      return false;
   }
}


/*---------------------------保存------------------------------------*/
function DoSave()
{
    var ID = $("#txtIndentityID").val();
    var DeptID = $("#hidtxtSubCom_uc").val();
    var BusinessMoney = $("#txtBusinessMoney").val();
    var RecordDate = $("#txtRecordDate").val();

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if (DeptID == "")
    {
        isFlag = false;
        fieldText += "分公司|";
        msgText += "请选择分公司|";
    }
    if (BusinessMoney == "")
    {
        isFlag = false;
        fieldText += "业务量|";
        msgText += "请输入业务量|";
    }
    if (RecordDate == "")
    {
        isFlag = false;
        fieldText += "记录时间|";
        msgText += "请选择记录时间|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
    
    var UrlParam = "&ID="+escape(ID) +"&DeptID="+escape(DeptID)
                    +"&BusinessMoney="+escape(BusinessMoney)
                    +"&RecordDate="+escape(RecordDate)
                    +"&Act=edit";
                    
      $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/HumanManager/InputCompanyRoyalty.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     AddPop();
                  }, 
                  complete :function(){ hidePopup();},
                  error: function() 
                  {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误");
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(msg) 
                  { 
                    if(msg.sta==1) 
                    {
                        if(parseInt($("#txtIndentityID").val())<=0)
                        {
                            document.getElementById('txtIndentityID').value= msg.data;
                        }
                        TurnToPage(1);//刷新
                    }
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
                    popMsgObj.ShowMsg(msg.info);
                  } 
               });              
}


//打开编辑层
function ShowNewDiv(ID,DeptID,DeptName,BusinessMoney,RecordDate)
{
    if(typeof(ID)!="undefined")
    {
        $("#txtIndentityID").val(ID);
        $("#txtSubCom_uc").val(DeptName);
        $("#hidtxtSubCom_uc").val(DeptID);
        $("#txtBusinessMoney").val(BusinessMoney);
        $("#txtRecordDate").val(RecordDate);
        openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
        $('#divNew').show();
    }
    else
    {
        openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
        $('#divNew').show();
    }
}


function DoHide()
{
   closeRotoscopingDiv(false,'divBackShadow');
   $('#divNew').hide();
   New();
}
function New()
{
    $("#txtIndentityID").val("0");
    $("#txtSubCom_uc").val("");
    $("#hidtxtSubCom_uc").val("");
    $("#txtBusinessMoney").val("");
    $("#txtRecordDate").val("");
}

/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/

//验证业务量为有效数值
function CheckNum(EleID)
{
    if(!IsNumeric(document.getElementById(EleID).value,10,2))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","业务量输入有误，请输入有效的数值！");
        document.getElementById(EleID).value="0";
        document.getElementById(EleID).focus();
        return;
    }
    else
    {
       $("#"+EleID).val(NumRound(document.getElementById(EleID).value,2));
    }
}