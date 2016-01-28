$(document).ready(function(){TurnToPage(1);
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
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    { document.getElementById("btnAll").checked=false;
           currentPageIndex = pageIndex;
           var BankNo= document.getElementById("txt_BankNo").value;
           var UsedStatus=document .getElementById("UsedStatus").value;
           var BankName=document.getElementById("txt_BankName").value;
           var create=document.getElementById("txt_lx").value;
           var py=document.getElementById("txt_py").value;
           ActionFlag="Search"
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/BankInfo.ashx?str='+str,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&BankNo="+escape(BankNo)+"&UsedStatus="+escape(UsedStatus)+"&BankName="+escape(BankName)+"&ActionFlag="+escape(ActionFlag)+"&create="+escape(create)+"&py="+escape(py),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != ""){
                           if(item.UsedStatus=='0')
                        item.UsedStatus='停用';
                        else if(item.UsedStatus=='1')
                        item.UsedStatus='启用';
                        var temp="'"+item.ID+"','"+item.CustNo+"','"+item.CustName+"','"+item.CustNam+"','"+item.ContactName+"','"+item.Tel+"','"+item.Fax+"','"+item.Mobile+"','"+item.Addr+"','"+item.Remark+"','"+item.UsedStatus+"','"+item.Creator+"','"+item.CreateDate.substring(0,10)+"','"+item.CreaterName+"','"+item.PYShort+"'";
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.CustNo+"\"><a href='#' onclick=\"Show("+temp+");\">"+fnjiequ(item.CustNo,10)+"</a></td>"+
                        "<td height='22' align='center' title=\""+item.CustName+"\">" + fnjiequ(item.CustName,10) + "</td>"+
                       "<td height='22' align='center'  title=\""+item.ContactName+"\">" + fnjiequ(item.ContactName,10) + "</td>"+
                       "<td height='22' align='center'>" + item.Tel + "</td>"+
                       "<td height='22' align='center'>" + item.UsedStatus + "</td>"+
                       "<td height='22' align='center'>" + item.CreaterName + "</td>"+
                       "<td height='22' align='center'>"+item.CreateDate.substring(0,10) +"</td>").appendTo($("#pageDataList1 tbody"));}
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
                 document.getElementById('Text2').value=msg.totalCount;
//                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                    ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
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
///验证电话号码
//只允许输入0-9和‘-’号   
// 任何一个字符不符合返回true                       
function isvalidtel(inputs) //校验电话号码    //add by taochun
{ 
var i,temp; 
var isvalidtel = false; 
inputstr=trim(inputs); 
if(inputstr.length==null||inputstr.length==0) return true; 
for(i=0;i<inputstr.length;i++) 
{ 
temp=inputstr.substring(i,i+1); 
if(!(temp>="0" && temp<="9" || temp=="-")) 
{ 
isvalidtel=true; 
break; 
} 
} 
return isvalidtel; 
} 

function Show(ID,CustNo,CustName,CustNam,ContactName,Tel,Fax,Mobile,Addr,Remark,UsedStatus,Creater,CreateDate,CreaterName,PYShort)
{  
   flag="1";
   openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
   document.getElementById('div_Add').style.display='block';
      document.getElementById("div_Add").style.zIndex="2";
   document.getElementById("divBackShadow").style.zIndex="1";
    document.getElementById("txt_CustNo").disabled=false;
    if(typeof(ID)!="undefined")
    {
      document.getElementById("hf_ID").value=ID;
      document.getElementById("txt_CustNo").value=CustNo;
      document.getElementById("txt_CustNo").disabled=true;
      document.getElementById("txt_CustName").value=CustName;
      document.getElementById("txt_CustNam").value=CustNam;
      document.getElementById("txt_PYShort").value=PYShort;
      document.getElementById("txt_ContactName").value=ContactName;
      document.getElementById("txt_Tel").value=Tel;
      document.getElementById("txt_Fax").value=Fax;
      document.getElementById("txt_Mobile").value=Mobile;
      document.getElementById("txt_Addr").value=Addr;
      document.getElementById("txt_Remark").value=Remark;
      document.getElementById("txtPrincipal").value=Creater;
      document.getElementById("UserPrincipal").value=CreaterName;
      document.getElementById("txt_CreateDate").value=CreateDate;
    
      if(UsedStatus=="启用")
      {
      document.getElementById("rd_use").checked=true;
      }
      else if(UsedStatus=="停用")
      {
         document.getElementById("rd_notuse").checked=true;
      }
      flag="2";
      
    }
}

function Fun_Search_BankInfo(aa)
{
      var fieldText = "";
      var msgText = "";
      var isFlag = true;
      var BankNo= trim(document.getElementById("txt_BankNo").value);
      var UsedStatus=document .getElementById("UsedStatus").value;
      var BankName=trim(document.getElementById("txt_BankName").value);
      var create=trim(document.getElementById("txt_lx").value);
      var py=trim(document.getElementById("txt_py").value);
    if(BankNo!="")
    {
     if(!CheckSpecialWord(document.getElementById("txt_BankNo").value))
     {
            isFlag = false;
              fieldText = fieldText +  "银行编号|";
   		       msgText = msgText + "银行编号不能含有特殊字符|";   
     }
    }
    if(BankName!="")
    {
     if(!CheckSpecialWord(BankName))
     {
            isFlag = false;
              fieldText = fieldText +  "银行名称|";
   		       msgText = msgText + "银行名称不能含有特殊字符|";   
     }
    }
     if(create!="")
    {
     if(!CheckSpecialWord(create))
     {
            isFlag = false;
              fieldText = fieldText +  "联系人|";
   		       msgText = msgText + "联系人不能含有特殊字符|";   
     }
    }
     if(py!="")
    {
     if(!CheckSpecialWord(py))
     {
            isFlag = false;
              fieldText = fieldText +  "拼音缩写|";
   		       msgText = msgText + "拼音缩写不能含有特殊字符|";   
     }
    }
      if(!isFlag)
      {
           popMsgObj.Show(fieldText,msgText);
           return;
      }
      search="1";
      TurnToPage(1);
}
 function DelBankInfo()
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除银行信息至少要选择一项！");
        else
          {
             CodePublic=str;
      ActionFlag="Del";
        $.ajax({ 
                  type: "POST",
                url:  '../../../Handler/Office/SupplyChain/BankInfo.ashx?str='+str,//目标地址
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
                        Fun_Search_BankInfo();
                       document.getElementById("btnAll").checked=false;
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
    

    
//    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
        if(newPageCount <=0 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
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

//   




		function CloseDiv(){
//		var Bigdiv = document.getElementById("BigDiv");
//		var Mydiv = document.getElementById("div_Add");
//		document.body.removeChild(Bigdiv); 
//		Mydiv.style.display="none";
        closeRotoscopingDiv(false,'divBackShadow');
	}





function InsertBank()
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
   
  var CustNo=document.getElementById("txt_CustNo").value;
  var CustName=document.getElementById("txt_CustName").value;
  var CustNam=document.getElementById("txt_CustNam").value;
  var PYShort=document.getElementById("txt_PYShort").value;
  var ContactName=document.getElementById("txt_ContactName").value;
  var Tel=document.getElementById("txt_Tel").value;
  var Fax=document.getElementById("txt_Fax").value;
  var Mobile=document.getElementById("txt_Mobile").value;
  var Addr=document.getElementById("txt_Addr").value;
  var Remark=document.getElementById("txt_Remark").value;
  var Creator=document.getElementById("txtPrincipal").value;
  var CreateDate=document.getElementById("txt_CreateDate").value;
   if(CustNo=="")
    {
        isFlag = false;
        fieldText = fieldText + "银行编号|";
   		msgText = msgText +  "请输入银行编号|";
   		
    }
    else
    {
      if(!CodeCheck(CustNo))
        {
            isFlag = false;
           fieldText = fieldText + "银行编号|";
   		    msgText = msgText +  "银行编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
       }
    }
     
      if(ActionFlag=="Add")
    {
        if( document.getElementById('txtPlanNoHidden').value=="0")
        {
          isFlag = false;
          fieldText = fieldText +  "银行编号|";
          msgText = msgText + "银行编号已经存在|"; 
        }
    }
     if(strlen(CustNo)>50)
    {
        isFlag = false;
        fieldText = fieldText + "银行编号|";
   		msgText = msgText +  "银行编号仅限于50个字符以内|";      
    }
     if(CustName=="")
    {
        isFlag = false;
        fieldText = fieldText + "银行名称|";
   		msgText = msgText +  "请输入银行名称|";
   		
    }
     if(strlen(CustName)>100)
    {
        isFlag = false;
        fieldText = fieldText + "银行名称|";
   		msgText = msgText +  "银行名称仅限于100个字符以内|";      
    }
     if(CustNam!="")
    {
       if(strlen(CustNam)>50)
    {
        isFlag = false;
        fieldText = fieldText + "银行简称|";
   		msgText = msgText +  "银行简称仅限于50个字符以内|";      
    }
    }
    
       if(CustNam!="")
    {
       if(strlen(CustNam)>50)
    {
        isFlag = false;
        fieldText = fieldText + "银行简称|";
   		msgText = msgText +  "银行简称仅限于50个字符以内|";      
    }
    }
    
    if(Tel!="")
    {
    if(isvalidtel(Tel))
    {
        isFlag = false;
        fieldText = fieldText + "电话号码|";
        msgText = msgText +  "电话号码格式不对|";      
    }
    }
   if(Fax!="")
   {
    if(!IsNumeric(Fax))
    {
      isFlag = false;
        fieldText = fieldText + "传真|";
        msgText = msgText +  "传真格式不对|";
    }
   }
    if(Mobile!="")
    if(!checkMobile(Mobile))
    {
      isFlag = false;
      fieldText = fieldText + "手机|";
   	  msgText = msgText +  "手机号码有错误|";
    }
    if(Creator=="")
    {
       isFlag = false;
        fieldText = fieldText + "建档人|";
   		msgText = msgText +  "请选择建档人|";
    }
     if(CreateDate=="")
    {
       isFlag = false;
        fieldText = fieldText + "建档日期|";
   		msgText = msgText +  "请选择建档日期|";
    }
    
      if(ContactName!="")
    {
       if(strlen(ContactName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "联系人|";
   		msgText = msgText +  "联系人仅限于50个字符以内|";      
    }
    }
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
        return;
    }
   
  var ID= document.getElementById("hf_ID").value;
  if(document.getElementById("rd_use").checked)
  {
   UseStatus="1";
  }
  else if(document.getElementById("rd_notuse").checked)
  {
     UseStatus="0";
  }
    var UrlParam = "CustNo="+CustNo+"\
                        &CustName="+CustName+"\
                        &CustNam="+CustNam+"\
                        &PYShort="+PYShort+"\
                        &ContactName="+ContactName+"\
                        &Tel="+Tel+"\
                        &Fax="+Fax+"\
                        &Mobile="+Mobile+"\
                        &Addr="+Addr+"\
                        &Remark="+Remark+"\
                        &Creator="+Creator+"\
                        &UseStatus="+UseStatus+"\
                        &ID="+ID+"\
                         &str="+str+"\
                         &ActionFlag="+ActionFlag+"\
                        &CreateDate="+CreateDate+"";
    $.ajax({ 
                  type: "POST",
                 url: "../../../Handler/Office/SupplyChain/BankInfo.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                   data: "ActionFlag="+escape(ActionFlag),//数据
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
                        Fun_Search_BankInfo();
                        Hide();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
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
      document.getElementById("hf_ID").value="";
      document.getElementById("txt_CustNo").value="";
      document.getElementById("txt_CustName").value="";
      document.getElementById("txt_CustNam").value="";
      document.getElementById("txt_PYShort").value="";
      document.getElementById("txt_ContactName").value="";
      document.getElementById("txt_Tel").value="";
      document.getElementById("txt_Fax").value="";
      document.getElementById("txt_Mobile").value="";
      document.getElementById("txt_Addr").value="";
      document.getElementById("txt_Remark").value="";
//      document.getElementById("txtPrincipal").value="";
//      document.getElementById("UserPrincipal").value="";
     // document.getElementById("txt_CreateDate").value="";
    document.getElementById("rd_use").checked=true;
    }
    function ClearQueryInfo()
    {
        $(":text").each(function(){ 
    this.value=""; 
    }); 
     document.getElementById("UsedStatus").value='1';
     
     
        $("#pageDataList1 tbody").find("tr.newrow").remove();
    }
    //若是中文则自动填充拼音缩写
function LoadPYShort()
{
    var txt_CustName = $("#txt_CustName").val();
    if(txt_CustName.length>0 && isChinese(txt_CustName))
    {
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/PYShort.ashx?Text="+escape(txt_CustName),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function(){}, 
                  success:function(data) 
                  { 
                    document.getElementById('txt_PYShort').value = data.info;
                  } 
               });
     }
}
function checkonly()
{
    var PlanNo=document.getElementById('txt_CustNo').value; 
    var TableName="officedba.BankInfo";
    var ColumName="CustNo";
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/CheckOnlyOne.ashx?strcode='"+PlanNo+"'&colname="+ColumName+"&tablename="+TableName+"",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              {  
              }, 
            error: function() 
            {
            }, 
            success:function(data) 
            { 
                if(data.sta!=1) 
                { 
//                popMsgObj.ShowMsg("往来单位编号已经存在");
                    document.getElementById('txtPlanNoHidden').value = data.sta;
                }
                else
                {
                    document.getElementById('txtPlanNoHidden').value = "";
                } 
            } 
           });
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
function checkMobile( s ){   
var regu =/^[1]([3][0-9]{1}|59|58)[0-9]{8}$/;
var re = new RegExp(regu);
if (re.test(s)) {
   return true;
}else{
   return false;
}
}