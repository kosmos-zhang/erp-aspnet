var orderBy = "";//排序字段
var CustType = "";
var TypeTitleName = "";
  
function SearchCustType(aa)
{
CustType = aa;
    switch(aa)
    {    
        case 1:
        document.getElementById("oCustTypeName").innerHTML = "客户管理分类";
        TypeTitleName = "客户管理分类";
        break;
        case 2:
        document.getElementById("oCustTypeName").innerHTML = "客户营销分类";
        TypeTitleName = "客户营销分类";
        break;
        case 3:
        document.getElementById("oCustTypeName").innerHTML = "客户时间分类";
        TypeTitleName = "客户时间分类";
        break;
        default:
        document.getElementById("oCustTypeName").innerHTML = "客户管理分类";
        TypeTitleName = "客户管理分类";
        break;        
    }
    TurnToPage(aa);
}

//jQuery-ajax获取JSON数据
function TurnToPage(CustType2)
{
       document.getElementById("hdPara").value = "orderby="+escape(orderBy)+"&CustTypeManage="+escape(CustType2);
       var CustTypeManage = CustType2;

        pageSetup();

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
    
    //document.getElementById("oCustTypeName").innerHTML = TypeTitleName;
    $("#oCustTypeName").html(TypeTitleName);   
            
    TurnToPage(CustType);
}

//打印 
function pageSetup()
{
var Url = document.getElementById("hdPara").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请检索数据后再预览|");
        return;
     }
    window.open("CustTypeManagePrint.aspx?" + Url);
}