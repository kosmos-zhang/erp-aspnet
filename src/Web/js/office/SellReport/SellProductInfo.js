// 项目预算分析列表

var pageCount = 10;             // 每页计数
var totalRecord = 0;            // 总页数
var pagerStyle = "flickr";      // jPagerBar样式
var currentPageIndex = 1;       // 当前页数
var orderBy = "";               // 排序字段

function TurnToPage(pageIndex)
{   
    pageIndex = parseFloat(pageIndex);
    currentPageIndex=pageIndex;
    
    if(orderBy =="" || orderBy == undefined)
    {
        orderBy ="ID desc";
    }
    else
    {
        var temp = orderBy.substring(0,2);
        if(temp =="ID")
        {
            orderBy = "ID DESC";
        }
        else
        {
            var arr = orderBy.split("_");
            if(arr[1]=="d")
            {
                orderBy = arr[0]+" desc";
            }
            else
            {
                orderBy = arr[0]+" asc";
            }
        }
    }
    
    var MyData ="action=datalist"
                +"&pageIndex="+pageIndex
                +"&pageCount="+pageCount
                +"&orderBy="+orderBy;

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url:"../../../Handler/Office/SellReport/SellProductInfo.ashx",

        data: MyData,
        cache: false,
        beforeSend: function(){ AddPop();},
        success: function(msg) {
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data,function(i,item)
            {     
                  if (item.id != null && item.id != "" && parseFloat(item.id)>0) {
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + "<input name='chkitem' id='chk" + i + "'  onclick = 'fnUnSelect(this)' value='" + item.id + "'   type='checkbox' /></td>" +
                    "<td height='22' align='center'>" + item.productNum + "</td>" +
                    "<td height='22' align='center'>" + item.productName + "</td>" +
                    "<td height='22' align='center'>" + item.price + "</td>" +
                    "<td height='22' align='center'>" + item.bref + "</td>" +
                    "<td height='22' align='center'><a href='#' title='编辑' onclick=GetDetails('"+item.id+"') >编辑</a></td>").appendTo($("#pageDataList1 tbody"));
                }
            });
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex); 
        },
        error: function(msg) {},
        complete: function() { 
            hidePopup();
            pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
        });
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if(!IsNumber(newPageIndex) || newPageIndex==0)
    {
//        isFlag = false;
//        fieldText = fieldText + "跳转页面|";
//        msgText = msgText +  "必须为正整数格式|";
        alert("跳转页面必须为正整数格式");
        return;
    }
    if(!IsNumber(newPageCount) || newPageCount==0)
    {
//        isFlag = false;
//        fieldText = fieldText + "每页显示|";
//        msgText = msgText +  "必须为正整数格式|";
        alert("每页显示必须为正整数格式");
        return;
    }
    
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }   

    else
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            alert("转到页数超出查询范围");
            return false;
        }
        else
        {
            orderBy ="";
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}

function GetDetails(obj)
{
    location.href='SellProductEdit.aspx?ModuleID=2032301&ID='+obj;
}

//全选
function SelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        if(obj.disabled != true)
        {
            obj.checked = $("#checkall").attr("checked");
        }
    });
}

//去除全选按钮
function fnUnSelect(obj) {
    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }
    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 0; i < signFrame.rows.length - 1; i++) {

            iCount = iCount + 1;

            if ($("#chk" + i).attr("checked")) {
                checkCount = checkCount + 1;
            }

        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
}

function GotoProductEdit()
{
    window.location.href='SellProductEdit.aspx?ModuleID=2032301';
}

// 删除
function fnDel()
{
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
        var ck = document.getElementsByName("chkitem"); 
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
        if(str.length<1)
            alert("请至少要选择一项！");
       else
       {
         var UrlParam = "action=deldata&ID="+str;
        $.ajax({        
              type: "POST",
              dataType:'json',//返回json格式数据
              url:  '../../../Handler/Office/SellReport/SellProductInfo.ashx',//目标地址

              cache:false,
              data: UrlParam,//数据
              beforeSend:function(){ }, 
              error: function() 
              {
                alert('请求发生错误');
              }, 
              success:function(data) 
              { 
                if(data.sta!=0)
                {
                    alert("删除成功!");
                    orderBy ="";
                }
                else
                {
                    alert("删除失败!");
                }
                TurnToPage(1);
              } 
           });
           }
    }
}

//排序
function OrderBy(orderColum,orderTip)
{
    if(parseFloat(totalRecord)<1)
    {
        return false;
    }
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
    TurnToPage(currentPageIndex);
}
