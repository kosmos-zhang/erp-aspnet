  //删除权限分配
   function DelRoleFunction()
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除角色权限信息至少要选择一项！");
        else
          {
        var flag="del";
        $.ajax({ 
                  type: "POST",
                 url: '../../../Handler/Office/SystemManager/RoleLicense.ashx?str='+str,
                  dataType:'json',//返回json格式数据
//                  data:flag,
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
                        popMsgObj.ShowMsg('删除成功！');
                       Fun_Search_RoleFunctionInfo();
                    }
                    else
                    {
                        popMsgObj.ShowMsg('删除失败！');
                    }
                  } 
               });
            
          }
     }
        
   }


//new code

$(document).ready(function(){
 if(document.getElementById('hf_back').value=='1')
 {
   Fun_Search_RoleFunctionInfo();
 }
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           var RoleID= document.getElementById('RoleDrpControl1_Drp_RoleInfo').value;
            var ModName=document.getElementById("uds").value;
           if(RoleID=="0")
           RoleID="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SystemManager/RoleLicenseList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&RoleID="+escape(RoleID)+"&ModName="+escape(ModName)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                   if(item.id != null && item.id != "")
                   {
                    retval=item.RoleID+"|"+item.ModuleID+"|"+item.id;
                    aa=item.id+"|"+item.RoleID;
                     if(item.ModifiedDate.substring(0,10)=='1900-01-01')
                                item.ModifiedDate='';
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value='"+item.ID+"' onclick=IfSelectAll('Checkbox1','btnAll')  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'><a href=\"RoleFunction_Edit.aspx?RoleID="+aa+"\">"+ item.RoleName+"</a></td>"+
                       "<td height='22' align='center'>" + item.ModuleName + "</td>"+
                        "<td height='22' align='center'>"+item.FunctionCD+"</td>"+
                        "<td height='22' align='center'>" + item.FunctionName + "</td>"+
//                        "<td height='22' align='center'>"+item.CompanyCD+"</td>"+
                        "<td height='22' align='center'>"+item.ModifiedUserID+"</td>"+
                        "<td height='22' align='center'>"+item.ModifiedDate+"</td>").appendTo($("#pageDataList1 tbody"));}
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

function Fun_Search_RoleFunctionInfo(aa)
{
     document.getElementById('hf_back').value='1';
      search="1";
      TurnToPage(1);
}
function Fun_ClearInput()
{
   document.getElementById('RoleDrpControl1_Drp_RoleInfo').value="0"
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

    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",'显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",'跳转页数格式不对，必须是正整数！');
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
        TurnToPage(1);
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
