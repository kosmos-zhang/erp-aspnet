function CheckData()
{
    var productno = $("#productNo").val();
    var productname = $("#productname").val();
    if(productno == "" | productno == undefined)
    {
        alert("产品编号必填!");
        return false;
    }
    
    if(productname == "" | productname == undefined)
    {
        alert("产品名称必填!");
        return false;
    }
    var price = $("#price").val();
    if(price !="" && price !=undefined)
    {
        if(!Isfloat(price))
        {
            alert("价格必须为数字!");
            return false;
        }
    }
    
    var RetVal = CheckSpecialWords();
    if (RetVal != "")
    {
        alert(RetVal+"不能含有特殊字符");
        return false;
    }
    
    var bref = $("#brief").val();
    if(bref.length>800)
    {
        alert("产品说明内容过多");
        return false;
    }
    
    var bref = $("#memo").val();
    if(bref.length>800)
    {
        alert("备注内容过多");
        return false;
    }
    return true;
}

function DataOP()
{
    if(!CheckData())
    {
        return;
    }
    
    var productno = $("#productNo").val();
    var productname = $("#productname").val();
    var price = $("#price").val();
    var brief = $("#brief").val();
    var memo = $("#memo").val();
    var id = $("#HidID").val();
    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellReport/SellProductInfo.ashx",

        data: "action=addtable&productno=" +escape(productno)+"&productname="+escape(productname)+"&price="+price+"&brief="+escape(brief)+"&memo="+escape(memo)+"&id="+id,
        dataType: 'string', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(data) {
            if(data!="0")
            {
                alert("保存成功!");
                $("#productNo").val("");
                $("#productname").val("");
                $("#price").val("");
                $("#brief").val("");
                $("#memo").val("");
                window.location.href='SellProductInfo.aspx?ModuleID=2032301';
            }
            else
            {
                alert("保存失败!");
            }
        }
    });
} 