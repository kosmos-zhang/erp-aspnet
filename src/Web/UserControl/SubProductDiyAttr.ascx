<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubProductDiyAttr.ascx.cs" Inherits="UserControl_SubProductDiyAttr" %>
<div id="divSubProductDiyAttr" style="display:none" >
<select id="SubselEFIndex"   onchange="clearSubEFDesc();"></select><input  type="text" id="SubtxtEFDesc"  style="width:30%"/>
</div>



<script type="text/javascript">
/*
* 如果页面上出现2个或者2个以上使用该控件 将失效一个
*/
function clearSubEFDesc()
{
    if($("#SubselEFIndex").val()=="-1")
   {
    return;
   }
    $("#SubtxtEFDesc").val("");
}

function SubfnGetExtAttr() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: 'action=all',
        beforeSend: function() { AddPop();  }, //发送数据之前

        success: function(msg) {
  
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("<option value=\"-1\">--请选择--</option>").appendTo($("#SubselEFIndex"));
                $("#divSubProductDiyAttr").show();
                $("#SubspanOther").show();
                $("#trSubNewAttr").show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#SubselEFIndex"));
                });
                document.getElementById("SubselEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
} 

function SubfnGetExtAttrOther(spanObj,trObj) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: 'action=all',
        beforeSend: function() { AddPop();  }, //发送数据之前

        success: function(msg) {
  
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("<option value=\"-1\">--请选择--</option>").appendTo($("#SubselEFIndex"));
                $("#divSubProductDiyAttr").show();
                $("#"+spanObj).show();
                $("#"+trObj).show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#SubselEFIndex"));
                });
                document.getElementById("SubselEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
} 
</script>