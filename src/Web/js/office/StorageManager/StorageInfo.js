$(document).ready(function(){
      //DoSearch();
    }); 
    
        /*
* 查询
*/
function DoSearch(currPage)
{
     var StorageNo=document.getElementById("txtStorageNo").value;
     var Storagename=document.getElementById("txtStorageName").value;
     var StorageType = document.getElementById("sltType").value;
     var UsedStatus = document.getElementById("sltUsedStatus").value;
     
    var search="action="+action+
           "&StorageNo="+escape(StorageNo)+"&Storagename="+escape(Storagename)+
           "&StorageType="+escape(StorageType)+
           "&UsedStatus="+escape(UsedStatus)+
           "&Flag=1";
    
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
        return false;
    }
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
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
           
           var postParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&OrderBy=" + orderBy + "&" + searchCondition;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageInfo.ashx?'+postParam,//目标地址
           cache:false,
           beforeSend:function(){
                AddPop();
                $("#pageDataList1_Pager").hide();
                },//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove(); 
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                            if(item.StorageType==0){item.StorageType="一般库"}
                            else{item.StorageType==1?item.StorageType="委托代销库":item.StorageType="贵重物品库"}
                            if(item.UsedStatus==1){item.UsedStatus="已启用"}
                            else{item.UsedStatus=" 停用"}
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1"+item.ID+"' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>"+"</td>"+
                            "<td height='22' align='center' title=\""+item.StorageNo+"\"><a href='" + GetLinkParam() + "&ID=" + item.ID + "')>"+ fnjiequ(item.StorageNo,10) +"</a></td>"+
                            "<td height='22' align='center' title=\""+item.StorageName+"\">"+fnjiequ(item.StorageName,10)+"</td>"+
                            "<td height='22' align='center'>"+item.StorageType+"</td>"+
                            "<td height='22' align='center'>"+item.UsedStatus+"</td>"+
                             "<td height='22' align='center'>"+item.CanViewUserName+"</td>"+
                            "<td height='22' align='center' title=\""+item.Remark+"\">"+fnjiequ(item.Remark,10)+"</td>").appendTo($("#pageDataList1 tbody"));
                         }
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
                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }

function Fun_Search_Storage(aa)
    {
      search="1";
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
        $("#txtorderBy").val(orderBy);//把排序字段放到隐藏域中，
        if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
        TurnToPage($('#ToPage').val());
    }

var OptionCheckArray=new Array();
function OptionCheckOnes(obj)
{
    with(obj)
    {
        if(checked)
        {
            OptionCheckArray.push(value);
        }
        else
        {
            var OptionCheckArrayTM=new Array();
            for(var i=0;i<OptionCheckArray.length;i++)
            {
                if(OptionCheckArray[i]!=value)
                {
                    OptionCheckArrayTM.push(OptionCheckArray[i]);
                }
            }
            OptionCheckArray=OptionCheckArrayTM;
        }        
    }
}

function DelStorage()
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
            var Action='Del';
            var UrlParam = "Action="+Action
                +"&ID="+IDArray;
             $.ajax({ 
              type: "POST",
              url: "../../../Handler/Office/StorageManager/StorageAdd.ashx?"+UrlParam,
              dataType:'json',//返回json格式数据
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
                popMsgObj.ShowMsg(data.info);
                if(data.sta>0)
                {
                    Fun_Search_Storage();
                }
              } 
           });
         }
   }
   else
   {
      return false;
   }
    
}

/*
* 新建
*/
function DoNew()
{
    window.location.href = GetLinkParam();
}

/*
* 获取链接的参数
*/
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    linkParam = "StorageAdd.aspx?ModuleID=" + ModuleID 
                            + "&pageIndex=" + currentPageIndex + "&pageCount=" + pageCount +"&orderBy=" + orderBy + "&" + searchCondition;
    //返回链接的字符串
    return linkParam;
}

/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/

function fnjiequ(str,strlen)
{
    return str.length>strlen?str.slice(0,strlen)+"……":str;
}

