/************************************************************************
* 作    者： 朱贤兆
* 创建日期： 2010.03.25
* 描    述： 计量单位js
* 修改日期： 2010.03.25
* 版    本： 0.1.0                                                                     
************************************************************************/


/**********************************************************************
 *作用：获取计量换算下拉列表
 *参数：ProductId（产品ID）、
 *      UnitType（单位类型 <SaleUnit 销售计量单位> <InUnit 采购计量单位> <StockUnit 库存计量单位> <MakeUnit 生产计量单位>）
 *      SelectId（下拉列表ID）
 *      Func（下拉列表onchange方法）
 *      Obj（下拉表填充对象ID）
 *      val(默认填充值,没有填充空)
 
 *引用：/// <reference path="js/common/UnitGroup.js" />
 *调用： GetUnitGroupSelect('435', 'StockUnit', 'group12', 'Fun(this)', 'Obj1')
 **********************************************************************/

function GetUnitGroupSelect(ProductId, UnitType, SelectId, Func, Obj, val) 
{   
    $.post("../../../Handler/Common/UnitGroup.ashx", "action=getunitgroup&Val=" + val + "&Func=" + Func + "&SelectId=" + SelectId + "&UnitParam=" + UnitType + "&ProductId=" + ProductId, function(data) {
    $("#" + Obj).html(data);
    });
}

/**********************************************************************
*作用：扩展获取计量换算下拉列表
*参数：ProductId（产品ID）、
*      UnitType（单位类型 <SaleUnit 销售计量单位> <InUnit 采购计量单位> <StockUnit 库存计量单位> <MakeUnit 生产计量单位>）
*      SelectId（下拉列表ID）
*      Func（下拉列表onchange方法）
*      Obj（下拉表填充对象ID）
*      val(默认填充值,没有填充空)
*      InFunc(内部执行方法)

*引用：/// <reference path="js/common/UnitGroup.js" />
*调用： GetUnitGroupSelect('435', 'StockUnit', 'group12', 'Fun(this)', 'Obj1','FillVal("1","asdf")')
**********************************************************************/

function GetUnitGroupSelectEx(ProductId, UnitType, SelectId, Func, Obj, val, InFunc) 
{
    $.post("../../../Handler/Common/UnitGroup.ashx", "action=getunitgroup&Val=" + val + "&Func=" + Func + "&SelectId=" + SelectId + "&UnitParam=" + UnitType + "&ProductId=" + ProductId, function(data) {
        $("#" + Obj).html(data);
        if (InFunc != "") {
            eval(InFunc);
        }
    });
}


/**********************************************************************
*作用：根据单位和数量计算基本数量
*参数：UsedUnitId（单位下列表ID）、
*      UsedNumId（数量文本框ID）
*      NumId（基本数量文本框ID）
*      UsedPrice（单价）
*      Price（基本单价）
*引用：/// <reference path="js/common/UnitGroup.js" />
*调用： CalCulateNum('UnitId', 'NumId', 'BasicNumId')
**********************************************************************/

function CalCulateNum(UsedUnitId, UsedNumId, NumId, UsedPrice, Price, SelPoint) 
{
    if ($("#" + UsedNumId).val() != "") 
    {
        $("#" + NumId).val(FormatAfterDotNumber(parseFloat(parseFloat($("#" + UsedUnitId).val().split('|')[1]) * parseFloat($("#" + UsedNumId).val())).toString(), parseInt(SelPoint)));
    }
    if (UsedPrice != "") 
    {   
        $("#" + UsedPrice).val(FormatAfterDotNumber(parseFloat(parseFloat($("#" + UsedUnitId).val().split('|')[1]) * parseFloat($("#" + Price).val())).toString(),parseInt(SelPoint)));
    }
}
 
/**********************************************************************
*作用：添加明细行公共方法
*参数：tab（行td组合对象）
*实例：sedNumId（数量文本框ID）

*引用：/// <reference path="js/common/UnitGroup.js" />
*调用：
* var tab ={ tabId: "TableId", rowId: "1", tdDetail: [
*          { type: "chk", id: "chkDetail", controlshow: "", event: "onclick='unSelAll()'", val: rowcount }, 
*          { type: "hid", id: "DetailID", controlshow: "", event: "", val:data.ID },
*          { type: "ddl", id: "DetailSortNo", controlshow: "disabled", event: "", val:{val:"1",options:[["val","text"],["val","text"],["val","text"]]} },
*          { type: "rad", id: "ProductNo", controlshow: "disabled", event: "", val: data.ProductNo },
*          { type: "date", id: "ProcutName", controlshow: "disabled", event: "", val: data.ProductName },
*          { type: "txt", id: "Spec", controlshow: "disabled", event: "", val: data.ProductSpec },
*          { type: "", id: "Spec", controlshow: "disabled", event: "", val: data.ProductSpec }
*          ]
*          }; //对象里面如果tdDetail数组需要控制也可以用push压进去
* GBLAddAddDetailRow(tab);
**********************************************************************/

function GBLAddAddDetailRow(tab) 
{
    var tdHtml = "";
    for (var i = 0; i < tab.tdDetail.length; i++) {

        if (tab.tdDetail[i].type == "txt") //文本框
        {
            tdHtml += "<td id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"  class=\"cell\"><input type=\"text\" " + tab.tdDetail[i].controlshow +
                      " " + tab.tdDetail[i].event.replace("#index#", tab.rowId) + "  id=\"txt" + tab.tdDetail[i].id + "_" + tab.rowId + "\" class=\"tdinput " +
                      "tboxsize textAlign\" value=\"" + tab.tdDetail[i].val + "\" name=\"" + tab.tdDetail[i].id + "\" /></td>";
        }
        else if (tab.tdDetail[i].type == "ddl") //多选框
        {
            tdHtml += "<td  id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"   class=\"cell\"><select id=\"ddl" + tab.tdDetail[i].id
            + "_" + tab.rowId + "\" " + tab.tdDetail[i].event + "  " + tab.tdDetail[i].controlshow + " >";
            var valx = tab.tdDetail[i].val;
            for (var n = 0; n < valx.options.length; n++) {
                if (valx.options[n][0] == valx.val) {
                    tdHtml += "<option value='" + valx.options[n][0] + "' selected='selected'>" + valx.options[n][1] + "</option>";
                } else {
                    tdHtml += "<option value='" + valx.options[n][0] + "' >" + valx.options[n][1] + "</option>";
                }
            }
            tdHtml += "</select></td>";
        }
        else if (tab.tdDetail[i].type == "chk") //多选框
        {
            tdHtml += "<td  id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"    class=\"cell\"><input type=\"checkbox\" " + tab.tdDetail[i].event + "  "
            + tab.tdDetail[i].controlshow + " id=\"chk" + tab.tdDetail[i].id
            + "_" + tab.rowId + "\" name=\"" + tab.tdDetail[i].id + "\" value=\"" + tab.tdDetail[i].val + "\" /></td>";

        }
        else if (tab.tdDetail[i].type == "rad") //单选框
        {
            tdHtml += "<td   id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"   class=\"cell\"><input type=\"radio\" " + tab.tdDetail[i].event + "  " + 
                      tab.tdDetail[i].controlshow + " id=\"rad" + tab.tdDetail[i].id+ "_" + tab.rowId + "\" name=\"" + tab.tdDetail[i].id + "\" value=\"" + tab.tdDetail[i].val + "\" /></td>";
        }
        else if (tab.tdDetail[i].type == "date") //日期框
        {
            tdHtml += "<td  id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"   class=\"cell\"><input type=\"text\"   " + tab.tdDetail[i].controlshow + "  class=\"tdinput tboxsize textAlign\" id=\"txt" + tab.tdDetail[i].id + "_" + tab.rowId + "\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txt_" + tab.tdDetail[i].id + "_" + tab.rowId + "')})\" value=\"" + tab.tdDetail[i].val + "\"  /></td>";
        }
        else if (tab.tdDetail[i].type == "hid") //日期框
        {  
            tdHtml += "<input type=\"hidden\" id=\"hid" + tab.tdDetail[i].id + "_" + tab.rowId + "\" value=\"" + tab.tdDetail[i].val + "\" />";
        }
        else {
            tdHtml += "<td  id=\"list" + tab.tdDetail[i].id + "_" + tab.rowId + "\"  class=\"cell\">" + tab.tdDetail[i].val + "</td>";
        }
    }
   
    $("<tr id=\"tr_list_" + tab.rowId + "\" class=\"newrow\"></tr>").append(tdHtml).appendTo($("#" + tab.tabId + " tbody"));
}

/**********************************************************************
*作用：计算基本数据
*参数：UsedUnit（单位下拉列表对象）
*      Num（数量文本框对象）
*      SelPoint（精确小数位数）

*引用：/// <reference path="js/common/UnitGroup.js" />
*调用：
* CalCulateNumEx(UsedUnit, Num, SelPoint);
**********************************************************************/

function CalCulateNumEx(UsedUnit, Num, SelPoint) 
{
    return FormatAfterDotNumber(parseFloat(parseFloat($("#" + UsedUnit).val().split('|')[1]) * parseFloat($("#" + Num).val())).toString(), parseInt(SelPoint))
}


/**********************************************************************
*作用：计算单价
*参数：UsedUnit（单位下拉列表对象）
*      UnitPrice（基本单位对象）
*      SelPoint（精确小数位数）

*引用：/// <reference path="js/common/UnitGroup.js" />
*调用：
* CalCulateNumEx(UsedUnit, Num, SelPoint);
**********************************************************************/

function CalCulatePrice(UsedUnit,UnitPrice, SelPoint) {
    return FormatAfterDotNumber(parseFloat(parseFloat($("#" + UsedUnit).val().split('|')[1]) * parseFloat($("#" + UnitPrice).val())).toString(), parseInt(SelPoint))
}
