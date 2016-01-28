  var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "ModifiedDate_d";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";


$(document).ready(function()
{
 

    requestobj = GetRequest(location.search);
    if(requestobj["ArriveNo"] != null)
    {//从编辑页面返回的
        fnSetSelectCondition(requestobj);
        SearchArriveData();
    } 
     IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
});

// 判断其他条件所在的行是否显示
function IsDisplayTr(isShow)
{
    if(isShow=="0")
    {
        $("#TROtherConditon").attr("style","display:none");
    }
    else
    {
        $("#TROtherConditon").removeAttr("style");
    }
}

//获取url中"?"符后的字串
function GetRequest(url)
{
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


function fnLinkEdit(ID)
{
    //检索条件
    
    var URLParams = $("#hidSearchCondition").val();
    URLParams += "&intMasterArriveID="+ID;
    URLParams += "&pageIndex="+document .getElementById ("ToPage").value.Trim();
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&action="+action;
    URLParams += "&orderby="+orderBy;
    URLParams += "&intFromType=1";//intFromType为1标志位来源于列表    
    var ModuleID = document.getElementById("hidModuleID").value;
    URLParams += "&ModuleID="+ModuleID;
    window.location.href='PurchaseArrive_Add.aspx?'+URLParams;
}

//获取检索条件放入隐藏域   
function fnGetSelectCondition()
{
    var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
    var strParams = "";
    strParams += "ArriveNo="+escape($("#txtArriveNo").val());
    strParams += "&Title="+escape($("#txtTitle").val());
    strParams += "&TypeID="+escape($("#drpTypeID").val());
    strParams += "&PurchaserName="+escape($("#UserPurchaser").val());
    strParams += "&Purchaser="+escape($("#HidPurchaser").val());
    strParams += "&FromType="+escape($("#ddlFromType").val());
    strParams += "&ProviderName="+escape($("#txtProviderID").val());
    strParams += "&ProviderID="+escape($("#txtHidProviderID").val());
    strParams += "&BillStatus="+escape($("#ddlBillStatus").val());
    strParams += "&UsedStatus="+escape($("#ddlUsedStatus").val());
    strParams += "&ProjectID="+escape($("#hidProjectID").val());
    strParams += "&ProjectName="+escape($("#txtProject").val());
    strParams += "&EFIndex="+escape(EFIndex);
    strParams += "&EFDesc="+escape(EFDesc); 
    
    $("#hidSearchCondition").val(strParams)
    return strParams;
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

//设置从编辑页面返回时的检索条件
function fnSetSelectCondition(requestobj)
{
    $("#txtArriveNo").val(requestobj["ArriveNo"]);
    $("#txtTitle").val(requestobj["Title"]);
    $("#drpTypeID").val(requestobj["TypeID"]);
    $("#UserPurchaser").val(requestobj["PurchaserName"]);
    $("#HidPurchaser").val(requestobj["Purchaser"]);
    $("#ddlFromType").val(requestobj["FromType"]);
    $("#txtProviderID").val(requestobj["ProviderName"]);
    $("#txtHidProviderID").val(requestobj["ProviderID"]);
    $("#ddlBillStatus").val(requestobj["BillStatus"]);
    $("#ddlUsedStatus").val(requestobj["UsedStatus"]);
    
    $("#hidProjectID").val(requestobj["ProjectID"]);
    $("#txtProject").val(requestobj["ProjectName"]);
    
    $("#GetBillExAttrControl1_SelExtValue").val(requestobj['EFIndex']); 
    $("#GetBillExAttrControl1_TxtExtValue").val(requestobj['EFDesc']);
    
    currentPageIndex = requestobj["pageIndex"];
    currentpageCount = requestobj["pageCount"];
    orderBy = requestobj["orderby"];
}

//新建采购到货通知
function CreatePurchaseArrive()
{
    //从隐藏域中获取检索条件
    var URLParams = $("#hidSearchCondition").val();
    URLParams += "&pageIndex="+currentPageIndex;
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&action="+action;
    URLParams += "&orderby="+orderBy;
    var ModuleID = document.getElementById("hidModuleID").value;
    URLParams += "&ModuleID="+ModuleID;
    URLParams += "&intMasterArriveID=0";
    URLParams += "&intFromType=1";//intFromType为1标志位来源于列表
    window.location.href='PurchaseArrive_Add.aspx?'+URLParams;
}


function SearchArriveData()
{
    if(!fnCheck())
        return;
    //检索条件
    issearch=1;
    fnGetSelectCondition();
    TurnToPage(currentPageIndex);
}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{
    if (pageIndex !="")
    {
        pageIndex =parseInt (pageIndex ,10);
    }
    else
    {
        currentPageIndex = pageIndex;
    }

    var URLParams = document.getElementById("hidSearchCondition").value;
    URLParams += "&pageIndex="+pageIndex;
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&action="+action;
    URLParams += "&orderby="+orderBy;
    
    
    $.ajax({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/PurchaseManager/PurchaseArriveInfo.ashx',//目标地址
        cache:false,
        data: URLParams,
        beforeSend:function(){AddPop();$("#pageDataList1_PagerList").hide();},//发送数据之前

        success: function(msg){
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                var j =1;
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    var Title = item.Title;
                    if (Title != null) {
                    if (Title.length > 6) {
                        Title = Title.substring(0, 6) + '...';
                        }
                    }
                    var OrderTitle = item.OrderTitle;
                    if(OrderTitle != null){
                    if (OrderTitle.length > 6) {
                        OrderTitle = OrderTitle.substring(0, 6) + '...';
                        }
                    }
                    var ArriveNo = item.ArriveNo;
                    if(ArriveNo != null){
                    if (ArriveNo.length > 20) {
                        ArriveNo = ArriveNo.substring(0, 20) + '...';
                        }
                    }
                    
                    
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='chk" + i + "' name='Checkbox'  Title='" + item.ArriveNo + "' Class='" + item.Isyinyong + "'  value='" + item.ID + "' type='checkbox' onclick=IfSelectAll('Checkbox','checkall') />"+"</td>"+
                    "<td height='22' align='center'><a href='#' onclick=fnLinkEdit('"+item.ID+"')>"+ GetStandardString(item.ArriveNo,25) +"</a></td>"+
                    "<td height='22' align='center'><span title=\"" + item.Title + "\">"+ Title +"</a></td>"+
                    "<td height='22' align='center'>"+ item.TypeName +"</a></td>"+
                    "<td height='22' align='center'>"+ item.PurchaserName +"</a></td>"+
                    "<td height='22' align='center'>"+ item.ProviderName +"</a></td>"+
                    "<td height='22' align='center'>"+ item.OrderNo +"</a></td>"+
                    "<td height='22' align='center'><span title=\"" + item.OrderTitle + "\">"+ OrderTitle +"</a></td>"+
                    "<td height='22' align='center'>"+FormatAfterDotNumber(item.TotalMoney,selPoint)+"</a></td>"+
                    "<td height='22' align='center'>"+ item.BillStatusName +"</a></td>"+
                    "<td height='22' align='center' style='display:none'>"+ item.Isyinyong +"</a></td>"+
                    "<td height='22' align='center'>"+ item.UsedStatus +"</a></td>"+
                    "<td height='22' align='center' style='display:none'>"+ item.FromTypeName +"</a></td>"+
                    "<td height='22' align='center' style='display:none'>"+ item.Rate +"</a></td>").appendTo($("#pageDataList1 tbody"));
               });
                //页码
               ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
               "<%= Request.Url.AbsolutePath %>",//[url]
                {style:pagerStyle,mark:"pageDataList1Mark",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                onclick:"TurnToPage({pageindex});return false;"}//[attr]
                );document.getElementById("checkall").checked = false;
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

// 全选
function SelectAll() 
{
    var status=$("#checkall").attr("checked");
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = status;
    });
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

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{     
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}


// 关闭供应商界面
function closeProviderdiv()
{
    document.getElementById("divProviderInfohhh").style.display="none";
}

// 清除供应商选择
function clearProviderdiv()
{
    closeProviderdiv();
    $("#txtProviderID").val("");
    $("#txtHidProviderID").val("");
}



function  DelArrive()
{ 
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DetailID = '';//id
    var DetailNo = '';//No
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            
            DetailNo += $("#chk" + i).attr("title") + ',';
            DetailID += $("#chk" + i).val() + ',';
            var FromBillText = $.trim($("#chk" + i).attr("class"));//是否引用，返回True和False
            var BillStatusText = pageDataList1.rows[i + 1].cells[9].innerText;
            var FlowInstanceText = pageDataList1.rows[i + 1].cells[11].innerText;
            if ($.trim(BillStatusText) != '制单' || $.trim(FlowInstanceText) != '' || FromBillText == 'True') {
                isFlag = false;
                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
            }
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {
        if (isFlag) {
            if (confirm("确认执行删除操作吗？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/PurchaseManager/PurchaseArriveInfo.ashx",
                    data: "action=Delete&DetailNo=" + escape(DetailNo),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

                    error: function() {

                        popMsgObj.ShowMsg('请求发生错误！');
                    },
                    success: function(data) {
                        if (data.sta == 1) {
                            popMsgObj.ShowMsg('删除成功！');
                            SearchArriveData();
                        }
                        else {
                            popMsgObj.ShowMsg('删除失败！');
                        }
                    }
                });
            }
        }
        else {
            popMsgObj.Show(fieldText, msgText);
        }
    }
    
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