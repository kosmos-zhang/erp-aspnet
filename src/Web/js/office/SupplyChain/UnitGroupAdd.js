//计量单位组设置
  
var pageCount = 10;//每页计数
var totalRecord = 0;
var pagerStyle = "flickr";//jPagerBar样式
var flag="";
var ActionFlag=""
var str="";
var currentPageIndex = 1;
var action = "";//操作
var orderBy = "";//排序字段
   
// 界面加载时
$(document).ready(function()
{
    SearchData();
});  


//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{    
    
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    
    var para="action=search"
            +"&pageIndex="+pageIndex
            +"&pageCount="+pageCount
            +"&orderby="+orderBy
            +"&GroupUnitNo="+escape($("#txtSGUNO").val())
            +"&GroupUnitName="+escape($("#txtSGUName").val())
            +"&BaseUnitID="+escape($("#selBU").val());
    $.ajax({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx',//目标地址
        cache:false,
        data: para,//数据
        beforeSend:function()
        {
            AddPop();
            $("#pageDataList1_Pager").hide();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data,function(i,item){
                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value='"+item.ID+","+item.GroupUnitNo+"' onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox' "+(item.isUse=='1'?"disabled":"")+"/></td>"+
                    "<td height='22' align='center'><a href='#' onclick='EditGU("+item.ID+")'>"+item.GroupUnitNo+"</a></td>"+
                    "<td height='22' align='center'>" + item.GroupUnitName + "</td>"+
                    "<td height='22' align='center'>" + item.CodeName + "</td>"+
                    "<td height='22' align='center'>"+item.Remark+"</td>").appendTo($("#pageDataList1 tbody"));
            });
            //页码
            ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
            "<%= Request.Url.AbsolutePath %>",//[url]
            {style:pagerStyle,mark:"pageDataList1Mark",
            totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
            onclick:"TurnToPage({pageindex});return false;"}//[attr]
            );
            totalRecord = msg.totalCount;
            ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
            document.getElementById('Text2').value=msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount,pageCount,pageIndex);
            $("#ToPage").val(pageIndex);
        },
        error: function() 
        {   
            howPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        complete:function()
        {
            hidePopup();
            $("#pageDataList1_Pager").show();
            Ifshow(document.getElementById('Text2').value);
            pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");
            $("#btnAll").attr("checked",false);
        }//接收数据完毕
    });
}

// 编辑修改
function EditGU(id)
{
    // 显示界面
    ShowDiv();
    $("#hidID").val(id);
    var para="action=searchedit"
            +"&ID="+escape($("#hidID").val());
    $.ajax
    ({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx',//目标地址
        cache:false,
        data: para,//数据
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            $.each(msg.data,function(i,item)
            {
                $("#txtGUNO").val(item.GroupUnitNo);
                $("#txtGUNO").attr("readonly","readonly");
                $("#txtGUName").val(item.GroupUnitName);
                $("#txtRemark").val(item.Remark);
                $("#selUnit").val(item.BaseUnitID);
                $("#selUnit").attr("disabled",item.isUse=='1');
                GetUnitDDL();
            });
        },
        error: function() {}, 
        complete:function()
        {
            $.ajax
            ({
                type: "POST",//用POST方式传输
                dataType:"json",//数据格式:JSON
                url:  '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx',//目标地址
                cache:false,
                data: "action=searchdetail&GroupUnitNo="+escape($("#txtGUNO").val()),//数据
                beforeSend:function()
                {
                    AddPop();
                },//发送数据之前
                success: function(msg)
                {
                    $.each(msg.data,function(i,item)
                    {
                        AddOneRowData(item);
                    });
                },
                error: function() {}, 
                complete:function()
                {
                    hidePopup();
                }//接收数据完毕
            });
        }//接收数据完毕
    });
}

// 改变基本计量单位时
function ChangeUnit()
{
    var iRow;
    GetUnitDDL();
    var temp=$("#selUnitHid").html();
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        iRow=$(this).find("input[type='hidden']").val();
        $("#trRate"+iRow).val("");
        $("#trSel"+iRow).html(temp);
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

// 查询数据
function SearchData()
{
    TurnToPage(1);
}

// 删除数据
function DeleteData()
{
    var temp="";
    var id=new Array();
    var GroupUnitNo=new Array();
    $("#pageDataList1 tbody").find("tr.newrow").find("input[type='checkbox']").each(function(i)
    {
        if($(this).attr("checked"))
        {
            temp=$(this).val();
            temp=temp.split(',');
            id.push(escape(temp[0]));
            GroupUnitNo.push(escape(temp[1]));
        }
    });
    if(id.length<1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除列表信息至少要选择一项！");
        return;
    }
    if(!window.confirm("确认执行删除操作吗？"))
    {
        return;
    }

    var para="action=del"
            +"&ID="+id.toString()
            +"&GroupUnitNo="+GroupUnitNo.toString();
    $.ajax({ 
        type: "POST",
        url: '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx',//目标地址
        dataType:'json',//返回json格式数据
        data: para,//数据
        cache:false,
        beforeSend:function()
        { 
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
                SearchData();
                $("#btnAll").attr("checked",false);
            }
            else
            {
                popMsgObj.ShowMsg(data.info);
            }
        } 
    });
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
        $("#btnAll").attr("checked",false);
    }
}
// 排序
function OrderBy(orderColum,orderTip)
{
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
    
// 隐藏新增修改界面
function Hide()
{
    CloseDiv();
    document.getElementById('divAdd').style.display='none';
}

//关闭遮挡界面
function CloseDiv()
{
    ClearText();
    closeRotoscopingDiv(false,'divBackShadow');
}

// 显示新增修改界面
function ShowDiv()
{
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    document.getElementById('divAdd').style.display='block';
    document.getElementById("divBackShadow").style.zIndex="1";
    document.getElementById("divAdd").style.zIndex="2";
    ClearText();
    GetUnitDDL();
}

// 清除文本框
function ClearText()
{
    $("#txtGUNO").val("");
    $("#txtGUNO").removeAttr("readonly");
    $("#txtGUName").val("");
    $("#txtRemark").val("");
    $("#hidID").val("");
    $("#hidRow").val("1");
    $("#checkall").removeAttr("checked");
    $("#selUnit").removeAttr("disabled");
    $("#selUnit").get(0).selectedIndex=0;
    $("#dg_Log tbody").find("tr.newrow").remove();
}

// 打开新增修改界面
function ShowAdd(isEdit)
{
    //界面处理
    ShowDiv();
}

// 添加空白行
function AddBlankRow()
{
    var iRow=$("#hidRow").val();
    AddOneRow(iRow);
    $("#hidRow").val(parseInt(iRow)+1);
    OrderLine();
    return iRow;
}

// 排序
function OrderLine()
{
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        $(this).find("input[name='trLine']").val(i+1);
    });
}

// 添加一行结构
function AddOneRow(iRow)
{
    $("<tr class='newrow'></tr>").append
        (   "<td height='22' align='center' class='cell' ><input id='trHidKey' type='hidden' value='"+iRow+"'><input id='Checkbox"+iRow+"' name='Checkbox' onclick=\"IfSelectAll('Checkbox','checkall')\"  type=\"checkbox\"  class=\"tdinput\"/></td>"
            +"<td height='22' align='center' class='cell' ><input type='text' id='trLine"+ iRow +"' name='trLine' style='width:98%;' class=\"tdinput\" align='center' readonly /></td>"
            +"<td height='22' align='center' class='cell' ><select id='trSel"+iRow+"' style='width:98%;' class=\"tdinput\" onchange='$(\"#trRate"+iRow+"\").val(\"\");' >"+$("#selUnitHid").html()+"</select></td>"
            +"<td height='22' align='center' class='cell' ><input type='text' id='trRate"+ iRow +"' style='width:98%;'  class=\"tdinput\" /></td>"
            +"<td height='22' align='center' class='cell' ><input type='text' id='trRemark"+ iRow +"' specialworkcheck='明细第"+iRow+"行备注' style='width:98%;' class=\"tdinput\"/></td>"
        ).appendTo($("#dg_Log tbody"))
}

// 添加一行数据
function AddOneRowData(data)
{
    var iRow=AddBlankRow();
    $("#trSel"+iRow).val(data.UnitID);
    $("#trRate"+iRow).val(data.ExRate);
    $("#trRemark"+iRow).val(data.Remark);
    $("#trSel"+iRow).attr("disabled",data.isUse=='1');
    $("#Checkbox"+iRow).attr("disabled",data.isUse=='1');
}

//全选
function SelectAll()
{
    if($("#checkall").attr("checked"))
    {
        $("#dg_Log tbody").find("input[type='checkbox']").each(function(i)
        {
            if(!$(this).attr("disabled"))
            {
                $(this).attr("checked",true);
            }
        });
    }
    else
    {
        $("#dg_Log tbody").find("input[type='checkbox']").each(function(i)
        {
            $(this).attr("checked",false);
        });
    }
}

// 删除选中的行
function DelRow()
{
    var flag=false;
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        if($(this).find("input[type='checkbox']").attr("checked"))
        {
            flag=true;
        }
    });
    if(!flag)
    {
        alert("请选择要删除的项");
        return ;
    }
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        if($(this).find("input[type='checkbox']").attr("checked"))
        {
            $(this).remove();
        }
    });
    
    OrderLine();
}


// 保存数据
function SaveData()
{
    if(!CheckData())
    {
        return ;
    }
    var para="action=save"
            +"&ID="+$("#hidID").val()
            +"&BaseUnitID="+escape($("#selUnit").val())
            +"&GroupUnitNo="+escape($("#txtGUNO").val())
            +"&GroupUnitName="+escape($("#txtGUName").val())
            +"&Remark="+escape($("#txtRemark").val());
    var iRow="";
    var UnitID= new Array();
    var ExRate= new Array();
    var Remark= new Array();
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        iRow=$(this).find("input[type='hidden']").val();
        UnitID.push($("#trSel"+iRow).val());
        ExRate.push($("#trRate"+iRow).val());
        Remark.push(escape($("#trRemark"+iRow).val()));
    });
    
    para += "&UnitID="+UnitID.toString()
            +"&ExRate="+ExRate.toString()
            +"&RemarkDetail="+Remark.toString();
    
    $.ajax
    ({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/SupplyChain/UnitGroupAdd.ashx',//目标地址
        cache:false,
        data: para,//数据
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            if(msg.sta=="1")
            {   
                popMsgObj.ShowMsg(msg.info);
                Hide();
                SearchData();
            }
            else
            {
                popMsgObj.ShowMsg(msg.info);
            }
        },
        error: function() {}, 
        complete:function()
        {
            hidePopup();
        }//接收数据完毕
    });
}

// 获得非基本单位的下拉框
function GetUnitDDL()
{
    $("#selUnitHid").html($("#selUnit").html());
    $("#selUnitHid option[selected]").remove();
}

function CheckData()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var temp=$("#txtGUNO").val();
    var iRow;
    
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if(temp=="")
    {
        isFlag = false;
        fieldText = fieldText + "计量单位组编号|";
        msgText = msgText +  "请输入计量单位组编号|";
    }
    if(strlen(temp)>50)
    {
        isFlag = false;
        fieldText = fieldText + "计量单位组编号|";
        msgText = msgText +  "计量单位组编号仅限于50个字符以内|";      
    }
    var temp=$("#txtGUName").val();
    if(temp=="")
    {
        isFlag = false;
        fieldText = fieldText + "计量单位组名称|";
        msgText = msgText +  "请输入计量单位组名称|";
    }
    if(strlen(temp)>50)
    {
        isFlag = false;
        fieldText = fieldText + "计量单位组名称|";
        msgText = msgText +  "计量单位组名称仅限于50个字符以内|";      
    }
    temp="";
    $("#dg_Log tbody").find("tr.newrow").each(function(i)
    {
        iRow=$(this).find("input[type='hidden']").val();
        if(temp.indexOf($("#trSel"+iRow).val()+",")>-1)
        {
            isFlag = false;
            fieldText = fieldText +$("#trSel"+iRow).find("option:selected").text()+"|";
            msgText = msgText +"计量单位名称["+$("#trSel"+iRow).find("option:selected").text()+"]重复|";
        }
        else
        {
            temp+=$("#trSel"+iRow).val()+",";
        }
        if($("#trRate"+iRow).val()=="")
        {
            isFlag = false;
            fieldText = fieldText + "换算比率|";
            msgText = msgText +  "请输入第"+(i+1)+"行换算比率|";
        }
        if(parseFloat($("#trRate"+iRow).val())<=0)
        {
            isFlag = false;
            fieldText = fieldText + "换算比率|";
            msgText = msgText +  "第"+(i+1)+"行换算比率不能等于零|";
        }
    });
    if(temp=="")
    {
        isFlag = false;
        fieldText = fieldText + "明细|";
        msgText = msgText +  "明细行不能为空|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return false;
    }
    return true;
}
// 查询界面全选
function OptionCheckAll()
{
    if(document.getElementById("btnAll").checked)
    {
        var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=!ck[i].disabled ;
        }
    }
    else 
    {
        var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
            ck[i].checked=false ;
        }
    }
}
