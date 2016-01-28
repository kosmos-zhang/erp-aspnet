$(document).ready(function(){
Fun_Search_UserInfo();
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
    var TypeFlag=document.getElementById("hf_typeflag").value;
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           document.getElementById("btnAll").checked=false;
           var serch=document.getElementById("hidSearchCondition").value;
           ActionFlag="Search"
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SystemManager/CodePublicType.ashx?str='+str,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&ActionFlag="+escape(ActionFlag)+"&flag="+escape(flag)+"&" + serch,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表 
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.UsedStatus=='0')item.UsedStatus='停用';
                             if(item.UsedStatus=='1')item.UsedStatus='启用';
                        if(item.TypeFlag != null && item.TypeFlag != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a href='#' onclick=\"Show('"+item.TypeCode+"','"+item.TypeName+"','"+item.UsedStatus+"','"+item.Description+"','"+item.TypeFlag+"','"+item.ID+"');\">"+ item.typecodeflag+"</a></td>"+
                        "<td height='22' align='center' title=\""+item.TypeName+"\">" +fnjiequ(item.TypeName,10)+ "</td>"+
                        "<td height='22' align='center'>"+item.UsedStatus+"</td>").appendTo($("#pageDataList1 tbody"));
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
           error: function() { showPopup(  "../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){ hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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
          var TypeName= document.getElementById("txt_typeflagname").value;
           var UsedStatus=document .getElementById("UsedStatus").value;
           var TypeFlag=document.getElementById("hf_typeflag").value;
          search+="TypeName="+escape(TypeName);
          search+="&UsedStatus="+escape(UsedStatus); 
          search+="&TypeFlag="+escape(TypeFlag); 
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
                  url: "../../../Handler/Office/SystemManager/CodePublicType.ashx?str="+str,
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


	function AlertMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','BigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "900";
		document.body.appendChild(maskObj);
		
	}

		function CloseDiv(){
	 closeRotoscopingDiv(false,'divBackShadow');
	}

var valArr="";
function Show(typecode,typename,usestatus,description,typeflag,ID)
{   
   flag="1";
     openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
     document.getElementById("div_Add").style.zIndex="2";
     document.getElementById("divBackShadow").style.zIndex="1";
     
   document.getElementById('drp_typecode').value=document.getElementById("hf_typeflag").value;
   document.getElementById('drp_typecode').disabled=false;
   document.getElementById('div_Add').style.display='block';
   
    for(var i=0;i< document.getElementById("drp_typecode").options.length;i++)
        {
        if(document.getElementById("drp_typecode").options[i].value=="1")
        {
         document.getElementById("drp_typecode").options[i].selected = true; 
        }
        }
   
   var typeflagVal=document.getElementById("hf_typeflag").value;
    if(typeof(typecode)!="undefined")
    {
        /*若是供应链模块和个人桌面模块的分类属性，则执行以下代码（过滤或填充费用分类）START */
        if(typeflagVal=="5" || typeflagVal=="1")
        {
            if(valArr != "")
            {
                var newValArr=valArr.split(',');
                var oOption = document.createElement("OPTION");
                document.getElementById("drp_typecode").options.add(oOption);
                oOption.innerText =newValArr[1];
                oOption.value = newValArr[0];
                valArr="";
            }
        }
        /*若是供应链模块和个人桌面模块的分类属性，则执行以下代码（过滤或填充费用分类）END */
      document.getElementById("drp_typecode").value=typecode;
      document.getElementById("txt_TypeName").value=typename;
      document.getElementById("txt_Description").value=description;
       document.getElementById("drp_typecode").disabled=true;
      document.getElementById("hf_ID").value=ID;
      if(usestatus=="停用")
      {
      document.getElementById("rd_notuse").checked=true;
      }
      else if(usestatus=="启用")
      {
         document.getElementById("rd_use").checked=true;
      }
      flag="2";
      document.getElementById("drp_typecode").readOnly="readOnly";
    }
    else
    {
    /*若是供应链模块和个人桌面模块的分类属性，则执行以下代码（过滤或填充费用分类）START */
        if(typeflagVal=="5" || typeflagVal=="1")
        {
          for(var i=0;i< document.getElementById("drp_typecode").options.length;i++)
            {
                for(var j=document.getElementById("drp_typecode").options.length-1;j>i;j--)
                {
                   if(document.getElementById("drp_typecode").options[i].text==document.getElementById("drp_typecode").options[j].text)
                   {
                     valArr=document.getElementById("drp_typecode").options[j].value+","+document.getElementById("drp_typecode").options[j].text;
                     document.getElementById("drp_typecode").options[j]=null;
                   }
                }
            }  
        }
        /*若是供应链模块和个人桌面模块的分类属性，则执行以下代码（过滤或填充费用分类）END */
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
  var TypeFlag=trim(document.getElementById("hf_typeflag").value);
  var TypeCode=trim(document.getElementById("drp_typecode").value);
  var TypeName=trim(document.getElementById("txt_TypeName").value);
  var Description=document.getElementById("txt_Description").value;
  var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
   if(TypeCode=="")
    {
        isFlag = false;
        fieldText = fieldText + "分类类别|";
   		msgText = msgText +  "请选择分类类别|";
   		
    }
   if(TypeName=="")
    {
        isFlag = false;
        fieldText = fieldText + "分类名称|";
   		msgText = msgText +  "请输入分类名称|";
   		
    }
     if(strlen(TypeName)>50)
    {
        isFlag = false;
        fieldText = fieldText + "分类名称|";
   		msgText = msgText +  "分类名称仅限于50个字符以内|";      
    }
     if(TypeFlag=="4"&&(TypeCode=="3"))
  {
   if(!IsNumber(TypeName))
   {
      isFlag = false;
        fieldText = fieldText + "分类名称|";
   		msgText = msgText +  "客户联络期限分类名称必须是正整数或0|";    
   }
   }
      if(TypeFlag=="7"&&(TypeCode=="3"))
  {
   if(!IsNumber(TypeName))
   {
      isFlag = false;
        fieldText = fieldText + "分类名称|";
   		msgText = msgText +  "供应商联络期限分类名称必须是正整数或0|";    
   }
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
    $.ajax({ 
                  type: "POST",
                 url: "../../../Handler/Office/SystemManager/CodePublicType.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data: "ActionFlag="+escape(ActionFlag)+"&TypeFlag="+escape(TypeFlag)+"&TypeCode="+escape(TypeCode)+"&TypeName="+escape(TypeName)+"&Description="+escape(Description)+"&UseStatus="+escape(UseStatus)+"&ID="+ID,//数据
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
                        Fun_Search_UserInfo();
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
    document.getElementById("txt_TypeName").value="";
    document.getElementById("txt_Description").value="";
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