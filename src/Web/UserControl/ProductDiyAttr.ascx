<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDiyAttr.ascx.cs"
    Inherits="UserControl_ProductDiyAttr" %>
<div id="divProductDiyAttr" style="display: none">
    <select id="selEFIndex" onchange="clearEFDesc();">
    </select><input type="text" id="txtEFDesc" SpecialWorkCheck="其他条件" style="width: 30%" />
</div>

<script type="text/javascript">
/*
* 如果页面上出现2个或者2个以上使用该控件 将失效一个
*/
function clearEFDesc()
{
    if($("#selEFIndex").val()=="-1")
   {
    return;
   }
    $("#txtEFDesc").val("");
}

function fnGetExtAttr(flag) {
var TableName='officedba.ProductInfo';
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
       data: 'action=all&TableName=' + TableName,
        beforeSend: function() { AddPop();  }, //发送数据之前

        success: function(msg) {
  
  
            
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("<option value=\"-1\">--请选择--</option>").appendTo($("#selEFIndex"));
                $("#divProductDiyAttr").show();
                $("#spanOther").show();
                $("#trNewAttr").show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#selEFIndex"));
                });
                document.getElementById("selEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup();/*add by Moshenlin 若未建立对应的扩展属性，清空td的id为flag的值 Start*/if(flag!=undefined&&flag!=null){if($("#selEFIndex").val()==null||$("#selEFIndex").val()==undefined||$("#selEFIndex").val()==""){document.getElementById(flag).innerHTML=""}}/*End*/ } //接收数据完毕
    });
} 

function fnGetExtAttrOther(spanObj,trObj) {
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
                $("<option value=\"-1\">--请选择--</option>").appendTo($("#selEFIndex"));
                $("#divProductDiyAttr").show();
                $("#"+spanObj).show();
                $("#"+trObj).show();
                $.each(msg.data, function(i, item) 
                {
                    $("<option value=\""+item.EFIndex+"\">"+item.EFDesc+"</option>").appendTo($("#selEFIndex"));
                });
                document.getElementById("selEFIndex").selectedIndex=0;
            }
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
} 
</script>

