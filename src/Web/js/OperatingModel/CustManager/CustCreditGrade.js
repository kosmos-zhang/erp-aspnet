
var orderBy = "";//排序字段

function SearchCustData()
{
    TurnToPage();
}

//jQuery-ajax获取JSON数据
function TurnToPage()
{
   document.getElementById("hdPara").value = "orderby="+escape(orderBy);

   $.ajax({
   type: "POST",//用POST方式传输
   dataType:"json",//数据格式:JSON
   url:  '../../../Handler/OperatingModel/CustManager/CustCreditGrade.ashx',//目标地址
   cache:false,
   data: "orderby="+escape(orderBy),//数据
   beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
   
   success: function(msg){
   
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();                
            $.each(msg.data,function(i,item){                
              if(item.num != null && item.num != "")
             {
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.TypeName + "</td>"+
                "<td height='22' align='center'>" + item.num + "</td>").appendTo($("#pageDataList1 tbody"));}
           });
          },
   error: function() 
   {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
   }, 
   complete:function(){hidePopup();pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

//排序
function OrderBy(orderColum,orderTip)
{    
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
    
    TurnToPage();
}

function pageSetup()
{
    var Url = document.getElementById("hdPara").value;
    if(Url == "")
    {
        popMsgObj.Show("打印|","请检索数据后再预览|");
        return;
    }
    window.open("CustCreditGradePrint.aspx?" + Url);
}

