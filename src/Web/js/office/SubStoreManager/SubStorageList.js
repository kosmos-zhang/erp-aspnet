var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var currentpageCount = 10;
var action = "";//操作
var orderBy = "";//排序字段
var Isliebiao ;

var ifdel="0";//是否删除
var issearch="";
    
$(document).ready(function()
{
    fnGetExtAttr();
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
   
   $.ajax({
   type: "POST",//用POST方式传输
   dataType:"json",//数据格式:JSON
   url:  '../../../Handler/Office/SubStoreManager/SubStorageList.ashx',//目标地址
   cache:false,
   data: "pageIndex="+pageIndex
            +"&pageCount="+currentpageCount
            +"&action="+action
            +"&orderby="+orderBy
            +document.getElementById("hidSearchCondition").value,
   
   beforeSend:function(){AddPop();},//发送数据之前
   success: function(msg){
        //数据获取完毕，填充页面据显示
        //数据列表
        $("#pageDataList1 tbody").find("tr.newrow").remove();
        var j =1;
        $.each(msg.data,function(i,item){
            if(item.ProductID != null && item.ProductID != "")
            var DeptName = item.DeptName;
            
            if (DeptName != null) {
            if (DeptName.length > 6) {
                DeptName = DeptName.substring(0, 6) + '...';
                }
            }
            var ProductNo = item.ProductNo;
            if (ProductNo != null) {
            if (ProductNo.length > 10) {
                ProductNo = ProductNo.substring(0, 10) + '...';
                }
            }
            $("<tr class='newrow'></tr>").append(
            "<td height='22' align='center'><span title=\"" + item.DeptName + "\">"+DeptName+"</a></td>"+
            "<td height='22' align='center'><span title=\"" + item.ProductNo + "\">"+ ProductNo +"</a></td>"+
            "<td height='22' align='center'>"+ item.ProductName +"</a></td>"+
            "<td height='22' align='center'>"+ item.Specification +"</a></td>"+
            "<td height='22' align='center'>"+ item.UnitName +"</a></td>"+
            "<td height='22' align='center'>"+ FormatAfterDotNumber(item.ProductCount2,selPoint) +"</a></td>"+
            "<td height='22' align='center'>"+ item.BatchNo +"</a></td>").appendTo($("#pageDataList1 tbody"));
       });
        //页码
       ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
       "<%= Request.Url.AbsolutePath %>",//[url]
        {style:pagerStyle,mark:"pageDataList1Mark",
        totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
        onclick:"TurnToPage({pageindex});return false;"}//[attr]
        );
      totalRecord = msg.totalCount;
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
    
    linkParam = "ProviderProductAdd.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
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

function SearchSubStorageProductData()
    {
    if(!fnCheck())
        return;
    //检索条件
    issearch=1;

    var DeptID = document.getElementById("HidDeptID").value;//分店ID
    if(DeptID == undefined)
    {
        DeptID="";
    }
    if(DeptID == "")
    {
        DeptID='00';
    }
    var ProductNo = document.getElementById("txtProductNo").value;//物品编号
    var ProductID = document.getElementById("HidProductID").value;//物品ID
    if(ProductID == undefined)
    {
        ProductID="";
    }
    var ProductName = document.getElementById("txtProductName").value;//物品名称
    
    
    var Isliebiao = 1;
    var URLParams = "&Isliebiao="+escape(Isliebiao)
                +"&DeptID="+escape(DeptID)
                +"&ProductNo="+escape(ProductNo)
                +"&ProductID="+escape(ProductID)
                +"&ProductName="+escape(ProductName)
                +"&BatchNo="+escape($("#txtBatchNo").val())
                +""; 
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
 
    //新建供应商物品推荐
   function CreateProviderProduct()
   {
 
    var CustName = document.getElementById("txtCustName").value;//供应商名称
    var CustNo = document.getElementById("txtCustNo").value;//供应商编号
    if(CustNo == undefined)
    {
        CustNo="";
    }
    var ProductName = document.getElementById("txtProductName").value;//物品名称
    var ProductID = document.getElementById("HidProductID").value;//物品ID
    if(ProductID == undefined)
    {
        ProductID="";
    }
    var Grade = document.getElementById("drpGrade").value;//推荐程度
    if(Grade == 0)
    {
        Grade="";
    }
    var Joiner = document.getElementById("HidJoiner").value;//推荐人id
    if(Joiner == undefined)
    {
        Joiner="";
    }
    var JoinerName = document.getElementById("UsertxtJoiner").value;//推荐人名称
    var StartJoinDate = document.getElementById("txtStartJoinDate").value;//推荐时间 起
    var EndJoinDate = document.getElementById("txtEndJoinDate").value;//推荐时间 止
    var Isliebiao = 1;
    var URLParams = "Isliebiao="+escape(Isliebiao)
                    +"&CustName="+escape(CustName)
                    +"&CustNo="+escape(CustNo)
                    +"&ProductName="+escape(ProductName)
                    +"&ProductID="+escape(ProductID)
                    +"&Grade="+escape(Grade)
                    +"&Joiner="+escape(Joiner)
                    +"&JoinerName="+escape(JoinerName)
                    +"&StartJoinDate="+escape(StartJoinDate)
                    +"&BatchNo="+escape($("#txtBatchNo").val())
                    +"&EndJoinDate="+escape(EndJoinDate)+""; 
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
   //    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
        window.location.href='ProviderProductAdd.aspx?'+URLParams ;
   }

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtCustName").value = providername;
    document.getElementById("txtCustNo").value = providerno;
    
    closeProviderdiv();
}



function  DelProviderProduct()
{ 
     
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
               var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        x=ck2;
        var str = ck2.substring(0,ck2.length-1);
        if(x.length-1<1)
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据再删除！");
        else
          {
             CodePublic=str;
        var action = "Delete";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/PurchaseManager/ProviderProductInfo.ashx?str="+str,//目标地址
                  dataType:'json',//返回json格式数据
                  data: "action="+escape(action),//数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        SearchSubStorageProductData();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
            
          }
        }  
        else   return   false;   

    
}






//选择物品
function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = id;
    document.getElementById("txtProductNo").value = no;
}
function IsOut() //判断第一次导出
{     
    if(parseFloat(totalRecord)<1)
    {       
        alert('请先检索！');
        return false;
    }
}

// 物品选择界面清除
function ClearPkroductInfo()
{
    $("#txtProductNo").val("");
    $("#HidProductID").val("");

    // 关闭界面
    closeProductdiv();
}