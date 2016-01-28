//$(document).ready(function(){
////  DoSearch();
//});

///*
//* 保存操作
//*/
//function DoSave()
//{ 
//    /* 页面信息进行校验 */
//    //基本信息校验 有错误时，返回
//    if (CheckInput())
//    {
//        return;
//    }
//    //获取人员基本信息参数
//    postParams = "Action=Save&" + GetPostParams();
//    $.ajax({ 
//        type: "POST",
//        url: "../../../Handler/Office/HumanManager/InputDepatmentRoyalty.ashx?"+postParams ,
//        dataType:'json',//返回json格式数据
//        cache:false,
//        beforeSend:function()
//        {
//            AddPop();
//        }, 
//        error: function()
//        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
//        }, 
//        success:function(data) 
//        {
//            if(data.sta == 1) 
//            { 
//                //设置提示信息
//                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
//            }
//            else 
//            { 
//                hidePopup();
//                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
//            } 
//        } 
//    }); 
//}
///*
//* 输入信息校验
//*/
//function CheckInput()
//{ 
//     //出错字段
//    var fieldText = "";
//    //出错提示信息
//    var msgText = "";
//    //是否有错标识
//    var isErrorFlag = false;    
//   table = document.getElementById("tblInsuDetail");
//    //获取行号
//     var no = table.rows.length;
//     
//  
//     for (var i =1;i<no-1;i++)
//     {
//           var  BusinessMoney=document .getElementById ("txtBusinessMoney_"+i ).value.Trim();
////           var TaxPercent=  document .getElementById ("txtTaxPercent_"+Row ).value;
////           var TaxCount=  document .getElementById ("txtTaxCount_"+Row ).value;
//           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
//              if (BusinessMoney =="" || BusinessMoney ==null || BusinessMoney =="undefine")
//              {
//                     if (StartDate !="")
//                  {
//                     isErrorFlag = true;
//                    fieldText += "第"+(i)+ "行业务量项|";
//                    msgText += "请输入业务量！|";
//                    break ;
//                  }
//              }else
//              {
//              if ( parseFloat (RatePercent,2)=="0")
//              {
//                  
//               }
//               else
//               {
//               
//                  if (StartDate =="" || StartDate ==null || StartDate =="undefine")
//                  {
//                     isErrorFlag = true;
//                  fieldText += "第"+(i)+ "行生成日期项|";
//                    msgText += "请输入生成日期！|";
//                    break ;
//                  }
//               
//               }
//              
//              
//              
//              }
//           
//     }
//    //如果有错误，显示错误信息
//    if(isErrorFlag)
//    {
//        //显示错误信息
//        //popMsgObj.Show(fieldText, msgText);
//        
//	    document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
//    }

//    return isErrorFlag;
//}

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

///*
//* 获取提交的基本信息
//*/
//function GetPostParams()
//{    

//       table = document.getElementById("tblInsuDetail");
//    //获取行号
//     var no = table.rows.length;
//     var getInfo=new Array ();
//     
//     for (var i =1;i<no-1;i++)
//     {
//     
//          var  DeptID=document .getElementById ("txtDeptID_"+i ).value.Trim();
//           var  BusinessMoney=document .getElementById ("txtBusinessMoney_"+i ).value.Trim();
//           var StartDate=  document .getElementById ("txtStartDate_"+i ).value.Trim();
//           getInfo .push ( DeptID,BusinessMoney,StartDate);
//   }
//   var strParams="TaxInfo="+getInfo ;
//    //返回参数字符串
//    return strParams;
//}

///*
//* 检索操作
//*/
//function DoSearch()
//{ 
//    var isFlag=true ;
//    var fieldText="";
//    var msgText="";
//    var OpenDate=document.getElementById("txtStartDate").value;
//    var CloseDate = document.getElementById("txtEndDate").value;
//    var RetVal=CheckSpecialWords();
//    if(RetVal!="")
//    {
//            isFlag = false;
//            fieldText = fieldText + RetVal+"|";
//   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
//   		 
//   		    
//    }
//    var  DeptID=document .getElementById ("DeptID").value.Trim();
//    if(DeptID=="")
//    {
//            isFlag = false;
//             fieldText += "部门名称|";
//   		    msgText += "请选择部门名称|";
//    }
//    if(!compareDate(OpenDate,CloseDate))
//    {
//            isFlag = false;
//            fieldText += "查询日期|";
//   		    msgText += "开始时间不能大于结束时间|";
//    }
//    if(!isFlag)
//    {
// document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").style.top = "240px";
//	    document.getElementById("spanMsg").style.left = "450px";
//	    document.getElementById("spanMsg").style.width = "290px";
//	    document.getElementById("spanMsg").style.position = "absolute";
//	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
//      return;
//    }
//    /* 获取参数 */
//    //部门ID
//    postParam =  "Action=Search" + "&DeptID=" +escape( document.getElementById("DeptID").value.Trim());
//    //开始时间
//    postParam += "&StartDate=" +escape( document.getElementById("txtStartDate").value.Trim());
//    //结束时间
//    postParam += "&EndDate=" +escape( document.getElementById("txtEndDate").value.Trim());
//    //保险名称

//    //进行查询获取数据
//    $.ajax({
//        type: "POST",//用POST方式传输
//        url:  '../../../Handler/Office/HumanManager/InputDepatmentRoyalty.ashx',
//        data : postParam,//目标地址
//        dataType:"string",//数据格式:JSON
//        cache:false,
//        beforeSend:function()
//        {
//            AddPop();
//        },//发送数据之前
//        success: function(insuInfo)
//        {
//            //设置工资项内容
//            document.getElementById("divSalaryDetail").innerHTML = insuInfo;
//        },
//        error: function() 
//        {
//            popMsgObj.ShowMsg('请求发生错误！');
//        },
//        complete:function(){
//            hidePopup();
//        }
//    });
//}

//function insertData()
//{
////debugger ;
//table = document.getElementById("tblInsuDetail");
//    //获取行号
//var no = table.rows.length;
//var txtNum= document .getElementById ("txtNum").value.Trim();
//var txtPStartDate= document .getElementById ("txtPStartDate").value.Trim();
//if (txtNum =="" || txtNum ==null || txtNum =="undefine")
//{
//   if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
//    {
//    return ;
//    }
//}
//else
//{
//   if (!IsNumeric(txtNum ,12,2))
//   {
//       document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的缴税基数|");
//  // alert ("请输入正确的缴税基数");
//  return;
//   }
//}


//    if (txtPStartDate =="" || txtPStartDate ==null || txtPStartDate =="undefine")
//    {
// 
//    }
//    else
//    {
//        for (var i=1;i<no-1;i++)
//        {
//        document .getElementById ("txtStartDate_"+i ).value=txtPStartDate;
//        }
//    }
//    
//if (txtNum =="" || txtNum ==null || txtNum =="undefine")
//{

//}
//else
//{
//for (var i=1;i<no-1;i++)
//{
//document .getElementById ("txtSalaryCount_"+i ).value=txtNum;
//Caculate(txtNum, i);
//}
//}

//}
//function Caculate(num ,Row)
//{
////debugger ;
//var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
//var allTaxInfo=taxInfo.split("@");
//var flage=false ;
//        for (var i=0;i<allTaxInfo.length;i++)
//        {
//           var oneTaxInfo=allTaxInfo [i].split(",");
//           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
//           {
//           flage =true ;
//       
//           document .getElementById ("txtTaxPercent_"+Row ).value=oneTaxInfo[2];
//             document .getElementById ("txtTaxCount_"+Row ).value=oneTaxInfo[3];
//             break ;
//           }
//        }
//        if (!flage )
//        {
//         document .getElementById ("txtStartDate_"+Row ).value="";
//        document .getElementById ("txtSalaryCount_"+Row ).value="";
//            document .getElementById ("txtTaxPercent_"+Row ).value="";
//             document .getElementById ("txtTaxCount_"+Row ).value="";
//        }

//}
//function CalculateTotalSalary(obj ,Row)
//{
//var num=obj.value;
//if (num =="" || num ==null || num =="undefine")
//{
//     document .getElementById ("txtStartDate_"+Row ).value="";
//        document .getElementById ("txtBusinessMoney_"+Row ).value="";
//            document .getElementById ("txtRatePercent_"+Row ).value="";
//return ;
//}
//if (!IsNumeric(num,12,2))
//{
//        document.getElementById("spanMsg").style.display = "block";
//	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("错误|", "请输入正确的业务量|");
//	    return ;
//}

//var taxInfo=document.getElementById ("hidTaxInfo").value.Trim();
//var allTaxInfo=taxInfo.split("@");
//var flage=false ;
//        for (var i=0;i<allTaxInfo.length;i++)
//        {
//           var oneTaxInfo=allTaxInfo [i].split(",");
//           if ( parseFloat (  num )>=  parseFloat (  oneTaxInfo [0]) && parseFloat (  num )<= parseFloat (  oneTaxInfo [1]))
//           {
//           flage =true ;
//           document .getElementById ("txtRatePercent_"+Row ).value=oneTaxInfo[2];
//             break ;
//           }
//        }
//        if (!flage )
//        {
//         document .getElementById ("txtStartDate_"+Row ).value="";
//         document .getElementById ("txtBusinessMoney_"+Row ).value="";
//         document .getElementById ("txtRatePercent_"+Row ).value="";
//        }

//}














$(document).ready(function(){
//Fun_Search_UserInfo();
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var flag="";
     var ActionFlag=""
     var str="";
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
//    var TypeFlag=document.getElementById("hf_typeflag").value;
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           document.getElementById("btnAll").checked=false;
           var serch=document.getElementById("hidSearchCondition").value;
           ActionFlag="Search"
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputDepatmentRoyalty.ashx?str='+str,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&ActionFlag="+escape(ActionFlag)+"&flag="+escape(flag)+"&" + serch,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.DeptID != null && item.DeptID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a href='#' onclick=\"Show('"+item.DeptID+"','"+item.DeptName+"','"+item.BusinessMoney+"','"+item.CreateTime+"','"+item.ID+"');\">"+ item.DeptName+"</a></td>"+
                        "<td height='22' align='center'>" +item.BusinessMoney+ "</td>"+
                        "<td height='22' align='center'>"+item.CreateTime+"</td>").appendTo($("#pageDataList1 tbody"));
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
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                   document.getElementById('Text2').value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_UserInfo(aa)
{
       var search = "";
          var DeptID= document.getElementById("DeptID").value;
           var OpenDate=document .getElementById("txtOpenDate").value;
           var CloseDate=document.getElementById("txtCloseDate").value;
          search+="DeptID="+escape(DeptID);
          search+="&OpenDate="+escape(OpenDate); 
          search+="&CloseDate="+escape(CloseDate); 
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    TurnToPage(1);
}
 function DelCodePubInfo()
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除分类信息至少要选择一项！");
        else
          {
             CodePublic=str;
      ActionFlag="Del";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/HumanManager/InputDepatmentRoyalty.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  data: "ActionFlag="+escape(ActionFlag),//数据
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
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
//                        alert("删除信息成功！") 
//                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除信息成功！");
                        Fun_Search_UserInfo();
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
    
 function OptionCheckAll()
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
    
//    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
//          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
//         alert("显示条数格式不对，必须是正整数！")
 showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示条数格式不对，必须是正整数！");
          return;
       }
       if(!IsZint(newPageIndex))
       {
//       alert("跳转页数格式不对，必须是正整数！")
//          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
 showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","跳转页数格式不对，必须是正整数！");
          return;
       }
       if(newPageCount <=0 )
        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
            return false;
        }
        if(newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
             document.getElementById("btnAll").checked=false;
        }
    }
//    //排序
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
        TurnToPage(1);
    }

   




		function CloseDiv(){
	 closeRotoscopingDiv(false,'divBackShadow');
	}


function Show(DeptID,DeptName,BusinessMoney,CreateTime,ID)
{   
   flag="1";
     openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
     document.getElementById("div_Add").style.zIndex="100000";
     document.getElementById("divBackShadow").style.zIndex="1";
     
    document.getElementById('div_Add').style.display='';
    if(typeof(DeptID)!="undefined")
    {
      document.getElementById("Dept_ID").value=DeptID;
      document.getElementById("Dept_Name").value=DeptName;
      document.getElementById("txtBusinessMoney").value=BusinessMoney;
       document.getElementById("txtCreateTime").value=CreateTime;
      document.getElementById("hf_ID").value=ID;
      flag="2";
    }
}


function InsertCodePublicType()
{
   var fieldText = "";
   var msgText = "";
   var isFlag = true;
   var UseStatus=null;
    if(flag=="1")
    {
      ActionFlag="Add"
    }
   else if(flag=="2")
   {
      ActionFlag="Edit";
   }
  var DeptID=trim(document.getElementById("Dept_ID").value);
  var BusinessMoney=trim(document.getElementById("txtBusinessMoney").value);
  var CreateTime=trim(document.getElementById("txtCreateTime").value);
  var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
   if(DeptID=="")
    {
        isFlag = false;
        fieldText = fieldText + "部门名称|";
   		msgText = msgText +  "请选择部门名称|";
   		
    }
   if(BusinessMoney=="")
    {
        isFlag = false;
        fieldText = fieldText + "业务量|";
   		msgText = msgText +  "请输入业务量|";
    }
    if (CreateTime == "")
    {
        isFlag = false;
        fieldText += "生成日期|";
        msgText += "请选择生成日期|";
    }
    if(!isFlag)
    {
       
        document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
        return;
    }
  var ID= document.getElementById("hf_ID").value;
    $.ajax({ 
                  type: "POST",
                 url: "../../../Handler/Office/HumanManager/InputDepatmentRoyalty.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data: "ActionFlag="+escape(ActionFlag)+"&DeptID="+escape(DeptID)+"&BusinessMoney="+escape(BusinessMoney)+"&CreateTime="+escape(CreateTime)+"&ID="+ID,//数据
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
//                        alert("保存数据成功！");
//                       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存数据成功！");
                        Fun_Search_UserInfo();
                        Hide();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
//                      alert("保存数据失败！");
//                      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存数据失败！");
                    }
                  } 
               });
}
    function Hide()
    {
    CloseDiv();
     document.getElementById('div_Add').style.display='none';
     New();
    }
    
    function New()
    {
    document.getElementById("Dept_ID").value="";
    document.getElementById("Dept_Name").value="";
    document.getElementById("txtBusinessMoney").value="";
    document.getElementById("txtCreateTime").value="";
    }
    
    function ClearQueryInfo()
    {
        $(":text").each(function(){ 
    this.value=""; 
    }); 
     document.getElementById("UsedStatus").value='1';
     
     
        $("#pageDataList1 tbody").find("tr.newrow").remove();
    }



function alertdiv(ControlID)
{
      var Array = ControlID.split(",");
      if(Array[0].indexOf("Dept") >= 0)
      {
          if(Array.length==2)
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
             {
             var splitInfo=returnValue.split("|");
              window.parent.window.frames[0].document.getElementById(Array[1]).value =splitInfo[0].toString();
              window.parent.window.frames[0].document.getElementById(Array[0]).value =splitInfo[1].toString();
             }
          else if(returnValue=="ClearInfo")
          {
             window.parent.window.frames[0].document.getElementById(Array[0]).value="";
             window.parent.window.frames[0].document.getElementById(Array[1]).value=""; 
          } 
           }
          else
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
           //  window.parent.document.getElementById(Array[1]).value =ID;
             if(window.parent.window.frames[2].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames[2].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
   
                       window.parent.window.frames[2].document.getElementById(Array[0]).value+=  window.parent.window.frames[2].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else 
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                   if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[2].document.getElementById(Array[0]).value+=  window.parent.window.frames[2].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
               // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
             }
             else
             {
                   window.parent.window.frames[2].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames[2].document.getElementById(Array[1]).value!="")
             {
                  // window.parent.window.frames[2].document.getElementById(Array[1]).value+=  window.parent.window.frames[2].document.getElementById(Array[1]).value =","+ID;
                var Oldvalue=window.parent.window.frames[2].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[0].document.getElementById(Array[1]).value+=  window.parent.window.frames[0].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[0].document.getElementById(Array[1]).value+=  window.parent.window.frames[0].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames[0].document.getElementById(Array[1]).value =ID;  
             } 
          }
          else if(returnValue=="ClearInfo")
          {
              window.parent.window.frames[0].document.getElementById(Array[0]).value="";
              window.parent.window.frames[0].document.getElementById(Array[1]).value=""; 
          } 
        }   
      }
}