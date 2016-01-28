    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;   
    var orderBy = "";//排序字段
    var ifdel="0";//是否删除
        

function SearchOrders(aa)
{
    if(!CheckInput())
    {
        return;
    }
    //search="1";
    TurnToPage(aa);
}

//得到物品名称和ID
function Fun_FillParent_Content(ProductID,No,Name)
{
    document.getElementById("txtProductName").value = Name;
    document.getElementById("txtProductID").value = ProductID;
}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{
       currentPageIndex = pageIndex;       
       var CustID = document.getElementById("hfCustID").value;//客户ID
       var ProductID = document.getElementById("txtProductID").value;//物品ID
       
       var NumBegin = document.getElementById("txtNumBegin").value;//订单数量
       var NumEnd = document.getElementById("txtNumEnd").value;//订单数量
       
       var PriceBegin = document.getElementById("txtPriceBegin").value;//购买金额       
       var PriceEnd =document.getElementById("txtPriceEnd").value;
       
       var DateBegin =document.getElementById("txtDateBegin").value;//下单日期
       var DateEnd =document.getElementById("txtDateEnd").value;//
       
       document.getElementById("hdPara").value = "orderby="+orderBy+"&NumBegin="+escape(NumBegin)+"&NumEnd="+escape(NumEnd)+"&CustID="+escape(CustID)+
       "&ProductID="+escape(ProductID)+"&PriceBegin="+escape(PriceBegin)+"&PriceEnd="+escape(PriceEnd)+"&DateBegin="+escape(DateBegin)+"&DateEnd="+escape(DateEnd);
      
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/OperatingModel/CustManager/CustOrders.ashx',//目标地址
       cache:false,
       data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&orderby="+orderBy+"&NumBegin="+escape(NumBegin)+"&NumEnd="+escape(NumEnd)+"&CustID="+escape(CustID)+
       "&ProductID="+escape(ProductID)+"&PriceBegin="+escape(PriceBegin)+"&PriceEnd="+escape(PriceEnd)+"&DateBegin="+escape(DateBegin)+"&DateEnd="+escape(DateEnd),//数据
       beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
       
       success: function(msg){
       
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                var RelaGrade;
                $.each(msg.data,function(i,item){
                  if(item.CustNo != null && item.CustNo != "")
                 {
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.CustNo + "</td>"+
                    "<td height='22' align='center'>" + item.CustName + "</td>"+
                    "<td height='22' align='center'>"+ item.ProductCount +"</td>"+
                    "<td height='22' align='center'>" + item.totalFee + "</td>").appendTo($("#pageDataList1 tbody"));}
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
              document.getElementById("Text2").value=msg.totalCount;
              $("#ShowPageCount").val(pageCount);
              ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
              $("#ToPage").val(pageIndex);
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (totalRecord == 0) 
     {
        return;
     }
        ifdel = "0";
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
         $("#hiddExpOrder").val(orderBy);
        TurnToPage(1);
    }
    
//打印 
function pageSetup()
{   
    var Url = $("#hdPara").val();
     if(Url == "")
     {
        popMsgObj.Show("打印|","请检索数据后再预览|");
        return;
     }
    window.open("CustOrdersPrint.aspx?" + Url);
}

//主表单验证
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var txtNumBegin = document.getElementById('txtNumBegin').value;
    var txtNumEnd = document.getElementById('txtNumEnd').value;
    
    var txtPriceBegin = document.getElementById('txtPriceBegin').value;
    var txtPriceEnd = document.getElementById('txtPriceEnd').value;
    
    var txtDateBegin = document.getElementById('txtDateBegin').value;//开始日期
    var txtDateEnd = document.getElementById('txtDateEnd').value;//结束日期
    
    if(txtNumBegin.length>0)
    {
        if(!PositiveInteger(txtNumBegin))
        {
            isFlag = false;
            fieldText = fieldText +  "订单数量区间|";
   		    msgText = msgText + "订单数量必须为正整数开始|";  
        }
    } 
     if(txtNumEnd.length>0)
    {
        if(!PositiveInteger(txtNumEnd))
        {
            isFlag = false;
            fieldText = fieldText +  "订单数量区间|";
   		    msgText = msgText + "订单数量必须为正整数结束|";  
        }
    }
    if(txtPriceBegin.length>0)
    {
        if(!IsNumeric(txtPriceBegin,20,2))
        {
            isFlag = false;
            fieldText = fieldText +  "购买金额|";
   		    msgText = msgText + "购买金额精度输入的格式不正确|";  
        }
        if(parseInt(txtPriceBegin).toString().length > 20)
        {
            isFlag = false;
            fieldText = fieldText +  "购买金额|";
   		    msgText = msgText + "购买金额整数部分过长|";
        }
    }
    if(txtPriceEnd.length>0)
    {
        if(!IsNumeric(txtPriceEnd,20,2))
        {
            isFlag = false;
            fieldText = fieldText +  "购买金额|";
   		    msgText = msgText + "购买金额精度输入的格式不正确|";  
        }
        if(parseInt(txtPriceEnd).toString().length > 20)
        {
            isFlag = false;
            fieldText = fieldText +  "购买金额|";
   		    msgText = msgText + "购买金额整数部分过长|";
        }
    }
     if(txtDateBegin.length>0 && isDate(txtDateBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "起始日期|";
	    msgText = msgText + "起始日期格式不正确|";
    }
    if(txtDateEnd.length>0 && isDate(txtDateEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "结束日期|";
	    msgText = msgText + "结束日期格式不正确|";
    }
    if(!compareDate(txtDateBegin,txtDateEnd))
    {
        isFlag = false;
        fieldText = fieldText + "日期|";
	    msgText = msgText + "起始日期不能大于结束日期|";
    }
    
    if(!isFlag)
    {        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}