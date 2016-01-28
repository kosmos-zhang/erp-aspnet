$(document).ready(function(){
TurnToPage(1);
});

/*
* 保存操作
*/
function DoSave()
{ 
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckInput())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = "Action=Save&" + GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/InputPersonTrueIncomeTax.ashx?"+postParams ,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    }); 
}
/*
* 输入信息校验
*/
function CheckInput()
{ 
     //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;    
   table = document.getElementById("tblInsuDetail");
    //获取行号
     var no = table.rows.length;
     
  
     for (var i =1;i<no-1;i++)
     {
           var  SalaryCount=document .getElementById ("txtSalaryCount_"+i ).value.Trim();
//           var TaxPercent=  document .getElementById ("txtTaxPercent_"+Row ).value;
//           var TaxCount=  document .getElementById ("txtTaxCount_"+Row ).value;
           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
               var TaxCountHid=  document .getElementById ("txtTaxCount_"+i ).value.Trim();
              if (SalaryCount =="" || SalaryCount ==null || SalaryCount =="undefine")
              {
                     if (StartDate !="")
                  {
                     isErrorFlag = true;
                    fieldText += "第"+(i)+ "行缴税基数项|";
                    msgText += "请输入缴税基数！|";
                    break ;
                  }
              }else
              {
              if ( parseFloat (TaxCountHid,2)=="0")
              {
                  
               }
               else
               {
               
                  if (StartDate =="" || StartDate ==null || StartDate =="undefine")
                  {
                     isErrorFlag = true;
                  fieldText += "第"+(i)+ "行开始缴税日期项|";
                    msgText += "请输入开始缴税日期！|";
                    break ;
                  }
               
               }
              
              
              
              }
           
     }
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);
        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }

    return isErrorFlag;
}

///////创建层（）
function CreateErrorMsgDiv(fieldText, msgText)
{
    errorMsg = "";
    if(fieldText != null && fieldText != "" && msgText != null && msgText != "")
    {
        var fieldArray = fieldText.split("|");
        var alertArray = msgText.split("|");
        for(var i = 0; i < fieldArray.length - 1; i++)
        {
            errorMsg += "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString()
                        + "</font><strong>]</strong>：" + alertArray[i].toString() + "<br />";
        }
    }
    table = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>"
            + "<tr><td align='center' height='1'>&nbsp;</td></tr>"
            + "<tr><td align='center'>" + errorMsg + "</td></tr>"
            + "<tr><td align='right'>"
            + "<img src='../../../Images/Button/closelabel.gif' onclick=\"document.getElementById('spanMsg').style.display='none';\" />"
            + "&nbsp;&nbsp;</td></tr></table>";
	
	return table;
} 

/*
* 获取提交的基本信息
*/
function GetPostParams()
{    

       table = document.getElementById("tblInsuDetail");
    //获取行号
     var no = table.rows.length;
     var getInfo=new Array ();
     
     for (var i =1;i<no-1;i++)
     {
     
          var  EmplID=document .getElementById ("txtEmplID_"+i ).value.Trim();
           var  SalaryCount=document .getElementById ("txtSalaryCount_"+i ).value.Trim();
           var TaxPercent=  document .getElementById ("txtTaxPercent_"+i ).value.Trim();
           var TaxCount=  document .getElementById ("txtTaxCount_"+i ).value.Trim();
           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
           getInfo .push ( EmplID,SalaryCount,TaxPercent,TaxCount,StartDate);
   }
   var strParams="TaxInfo="+getInfo ;
    //返回参数字符串
    return strParams;
}

/*
* 检索操作
*/
function DoSearch()
{ 
var isFlag=true ;
 var fieldText="";
    var msgText="";
   var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
   		 
   		    
    }
 if(!isFlag)
    {
 document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").style.top = "240px";
	    document.getElementById("spanMsg").style.left = "450px";
	    document.getElementById("spanMsg").style.width = "290px";
	    document.getElementById("spanMsg").style.position = "absolute";
	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
      return;
    }
    /* 获取参数 */
    //员工编号
    postParam =  "Action=Search" + "&EmployeeNo=" +escape( document.getElementById("txtEmployeeNo").value.Trim());
    //员工姓名
    postParam += "&EmployeeName=" + escape(document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&OpenDate=" +escape( document.getElementById("txtOpenDate").value.Trim());
    postParam += "&CloseDate=" +escape( document.getElementById("txtCloseDate").value.Trim());
    //保险名称
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputPersonalRoyalty.ashx',
        data : postParam,//目标地址
        dataType:"string",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(insuInfo)
        {
            //设置工资项内容
            document.getElementById("divInsuDetail").innerHTML = insuInfo;
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
        }
    });
}

function insertData()
{
//debugger ;
table = document.getElementById("tblInsuDetail");
    //获取行号
var no = table.rows.length;
var txtNum= document .getElementById ("txtNum").value.Trim();
var txtPStartDate= document .getElementById ("txtPStartDate").value.Trim();
if (txtNum =="" || txtNum ==null || txtNum =="undefine")
{
   if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
    {
    return ;
    }
}
else
{
   if (!IsNumeric(txtNum ,12,2))
   {
       document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
  // alert ("请输入正确的缴税基数");
  return;
   }
}


    if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
    {
 
    }
    else
    {
        for (var i=1;i<no-1;i++)
        {
        document .getElementById ("txtStartDate_"+i ).value=txtPStartDate;
        }
    }
    
if (txtNum =="" || txtNum ==null || txtNum =="undefine")
{

}
else
{
for (var i=1;i<no-1;i++)
{
document .getElementById ("txtSalaryCount_"+i ).value=txtNum;
Caculate(txtNum, i);
}
}

}
function Caculate(num ,Row)
{
//debugger ;
var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
var allTaxInfo=taxInfo.split("@");
var flage=false ;
        for (var i=0;i<allTaxInfo.length;i++)
        {
           var oneTaxInfo=allTaxInfo [i].split(",");
           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
           {
           flage =true ;
       
           document .getElementById ("txtTaxPercent_"+Row ).value=oneTaxInfo[2];
             document .getElementById ("txtTaxCount_"+Row ).value=oneTaxInfo[3];
             break ;
           }
        }
        if (!flage )
        {
         document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
        }

}
function CalculateTotalSalary(obj ,Row)
{
//debugger ;
var num=obj.value;
if (num =="" || num ==null || num =="undefine")
{
     document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
return ;
}
if (!IsNumeric(num,12,2))
{
        document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
	    return ;
}

var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
var allTaxInfo=taxInfo.split("@");
var flage=false ;
        for (var i=0;i<allTaxInfo.length;i++)
        {
           var oneTaxInfo=allTaxInfo [i].split(",");
           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
           {
           flage =true ;
       
           document .getElementById ("txtTaxPercent_"+Row ).value=oneTaxInfo[2];
             document .getElementById ("txtTaxCount_"+Row ).value=oneTaxInfo[3];
             break ;
           }
        }
        if (!flage )
        {
         document .getElementById ("txtStartDate_"+Row ).value="";
        document .getElementById ("txtSalaryCount_"+Row ).value="";
            document .getElementById ("txtTaxPercent_"+Row ).value="";
             document .getElementById ("txtTaxCount_"+Row ).value="";
        }

}

// onblur =\"CalculateTotalSalary('" + (i + 1).ToString() + "')\"

//function CalculateTotalSalary( row)
//{
//document .getElementById ("")



//}
//弹出客户订单
function ShowSellOrder()
{
 openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
 document.getElementById("divBackShadow").style.zIndex="1";
  document.getElementById("div_Add").style.display = "block";
}
//检索客户订单
var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
//    if (!CheckInput()) {
//        return;
//    }
    currentPageIndex = pageIndex;
    $("#btnAll").removeAttr("checked")
    var Action = "getinfo";

    var strUrl = getSearchParams1() + '&pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy)
    $("#hiddUrl").val(strUrl);

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/HumanManager/InputPersonalRoyalty.ashx?Action='+Action+'', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
            if (item.OrderNo != null) {

                    var OrderNo = item.OrderNo;
                    var Title = item.Title;
                    var FromBillNo = item.FromBillNo;
                    var CustName = item.CustName;
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1' name='Checkbox1'  value="+item.OrderNo+" onclick=IfSelectAll('Checkbox1','btnAll')  type='checkbox'/>" + "</td>" +
                    "<td height='22' align='center'  title=\""+OrderNo+"\">" + fnjiequ(OrderNo,15) + "</td>" +
                    "<td height='22' align='center' title=\""+Title+"\">" + fnjiequ(Title,15) + "</a></td>" +
                    "<td height='22' align='center' title=\""+CustName+"\">" + fnjiequ(CustName,10) + "</td>" +
                    "<td height='22' align='center'>" + item.OrderDate + "</td>" +
                  
                    "<td height='22' align='center'>" + FormatAfterDotNumber(item.TotalPrice, 2) + "</td>" +
                      "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                    "<td height='22' align='center'>" + item.CurrencyName + "</td>" +
//                    "<td height='22' align='center'>" + item.OrderDate + "</td>" +
//                     "<td height='22' align='center'>" + item.BillStatusText + "</td>" +
                    "<td height='22' align='center'>" + item.Rate + "</td>").appendTo($("#pageDataList1 tbody"));
                }
            });
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageSellOffcount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.frmMain.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });

}
//构造查询条件的URL参数
function getSearchParams1() {

    var orderNo = $("#orderNo").val(); //单据编号         
    var SellerName = $("#UserSeller").val(); //业务员
//    if (SellerName != '') {
//        var Seller = $("#Seller").val(); //业务员
//    }
//    else {
//        var Seller = '';
//    }
//    var FromType = $("#FromType").val();      //来源单据类型

//    var BillStatus = $("#BillStatus").val();      //单据状态

//    var FlowStatus = $("#FlowStatus").val();      //单据状态
//    var FromBillNo = $("#FromBillID").val(); //来源单据编号
//    if (FromBillNo != '') {
//        var FromBillID = $("#FromBillID").attr('title'); //来源单据编号
//    }
//    else {
//        var FromBillID = '';
//    }
//    var TotalPrice = $("#TotalPrice").val(); //金额                 
//    var TotalPrice1 = $("#TotalPrice1").val(); //金额                
   var OpenDate=document .getElementById("txtOpenDate0").value;
   var CloseDate=document.getElementById("txtCloseDate0").value;

    var strUrl = 'orderNo=' + escape(orderNo) + '&SellerName=' + escape(SellerName) + '&OpenDate=' + escape(OpenDate) + '&CloseDate=' + escape(CloseDate);
    $("#hiddExpOrderNo").val(orderNo);
//    $("#hiddExpTitle").val(Title);
    return strUrl;
}
function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
            document.getElementById("pageSellOffcount").style.display = "none";
        }
        else
        {
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pageSellOffcount').style.display = "block";
        }
    }
    //改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    $("#btnAll").removeAttr("checked")
    if (!IsNumber(newPageIndex) || newPageIndex == 0) {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}

//排序
function OrderBy(orderColum, orderTip) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var ordering = "d";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↑") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    else {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    orderBy = orderColum + "_" + ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
}
function OptionCheck()
{

  if(document.getElementById("btnAll").checked)
  {
     var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=true ;
        }
  }
  else if(!document.getElementById("btnAll").checked)
  {
    var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=false ;
        }
  }
}
function closeProductdiv()
{
  document.getElementById("div_Add").style.display = "none";
  closeRotoscopingDiv(false,'divBackShadow');
}

function GetValue()
{
  var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        Action="SynchronizerSell";
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项数据！");
       else
       {
         var UrlParam ='';
       var UrlParam = "&str="+str+"\
                     &Action="+Action
        $.ajax({ 
                type: "POST",
                url:  '../../../Handler/Office/HumanManager/InputPersonalRoyalty.ashx?str='+UrlParam,//目标地址
              dataType:'json',//返回json格式数据
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
                popMsgObj.ShowMsg(data.info);
                document.getElementById("btnAll").checked=false;
                DoSearch();
              } 
           });
    }
}

function LoadRax()
{

       var ck = document.getElementsByName("chkSelect");
        var x=Array(); 
        var ck2 = "";
        var str="";
        var j="";
        Action="SynchronizerRate";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
//               j=i;
//               j=j+","
               j+=i+',';
            }
        }
        x=ck2;
        var str = ck2.substring(0,ck2.length-1);
        var RowNum=j.substring(0,j.length-1);
          if(x.length-1<1)
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项数据！");
       else
       {
         var UrlParam ='';
         var UrlParam = "&str="+str+"\
                      &RowNum="+RowNum+"\
                     &Action="+Action
        $.ajax({ 
                type: "POST",
                url:  '../../../Handler/Office/HumanManager/InputPersonalRoyalty.ashx?str='+UrlParam,//目标地址
              dataType:'json',//返回json格式数据
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
                DoSearch();
              }
              else if(data.sta==2)
              {
                Msg(data.data);
              }
//popMsgObj.Show("dd|","发酵法律的撒范德萨飞飞飞阿是飞 安静了房间了盛大辅导费案件");
              } 
           });
    }
}

function Msg(msg)
{
  var fieldText = "";
  var msgText = "";
  var isFlag = true;
  var Msg = msg.substring(0,msg.length-1);
  var str =new Array();
  str=Msg.split(","); 
  for ( var i=0;i<str.length ;i++ )    
  {    
     isFlag = false;
     fieldText = fieldText +  "个人业务提成设置 |";
     msgText = msgText + "第"+(parseInt(str[i])+1)+"行未设置业绩范围|";    
  }    
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
}
///全选同步过来的订单
function SelectAll()
{
  if(document.getElementById("chkAll").checked)
  {
     var ck = document.getElementsByName("chkSelect");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=true ;
        }
  }
  else if(!document.getElementById("chkAll").checked)
  {
    var ck = document.getElementsByName("chkSelect");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=false ;
        }
  }
}