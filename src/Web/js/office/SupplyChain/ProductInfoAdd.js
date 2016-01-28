$(document).ready(function() {
    try {
        if (document.getElementById('sel_CheckStatus').value == "1") {
            document.getElementById("product_btnunsure").style.display = "none";
        }
        requestobj = GetRequest();
        recordnoparam = requestobj['intOtherCorpInfoID'];
        if (recordnoparam > 0) {
            LoadProductInfo(recordnoparam);
            //Fun_GroupList_Content();
            document.getElementById("divInputNo").style.display = "block";
            document.getElementById('product_btnback').style.display = 'block';
            document.getElementById("CodingRuleControl1_ddlCodeRule").style.display = 'none';
            document.getElementById("divNo").style.display = 'none';
            document.getElementById("CodingRuleControl1_txtCode").style.display = 'block';
            document.getElementById("CodingRuleControl1_txtCode").value = document.getElementById("divNo").innerHTML;
            document.getElementById('CodingRuleControl1_txtCode').className = 'tdinput';
            document.getElementById('CodingRuleControl1_txtCode').style.width = '90%';
            document.getElementById('sel_UsedStatus').disabled = false;
            var searh = requestobj['serch'];
            if (typeof (searh) != "undefined") {
                document.getElementById("hidSearchCondition").value = searh;
            }
            if (document.getElementById('sel_CheckStatus').value == "1") {
                document.getElementById("product_btnunsure").style.display = "block";
                document.getElementById("product_btn_AD").style.display = "none";

            }
            else if (document.getElementById('sel_CheckStatus').value == "0") {
                document.getElementById("product_btnunsure").style.display = "none";
                document.getElementById("product_btn_AD").style.display = "block";
            }
            if (document.getElementById('txt_IsConfirmProduct').value == "1") {
                if (isMoreUnit) {
                    document.getElementById('sel_UnitID').disabled = true;
                }
            }

        }
        else {
            GetExtAttr('officedba.ProductInfo', null);
        }
    }
    catch (e) { }
});


/*加载采购入库单*/
function LoadProductInfo(inID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/SupplyChain/ProductInfo.ashx?Action=Info&ID=" + inID, //目标地址
        cache: false,
        success: function(msg) {
            var rowsCount = 0;
            /*填充主表信息数据*/
            if (typeof (msg.dataBase) != 'undefined') {
                var TableName = "officedba.ProductInfo";
                GetExtAttr(TableName, msg.dataBase[0]);
            }
        },
        error: function() { },
        complete: function() { }
    });
}
function Fun_GroupList_Content() {
    var UnitNo = $("#HdGroupNo").val();
    if (UnitNo == "" || UnitNo == null) {
        //填充物品基本单位
        //    var ProductUntID= $("#sel_UnitID").val();
        //    var ProductName=document.getElementById("sel_UnitID").options[document.getElementById("sel_UnitID").selectedIndex].text;
        //     document.getElementById("selStorageUnit").options.length = 0;
        //     document.getElementById("selSellUnit").options.length = 0;
        //     document.getElementById("selPurchseUnit").options.length = 0;
        //     document.getElementById("selProductUnit").options.length = 0;
        //     document.getElementById("selStorageUnit").options.add(new Option(ProductName,ProductUntID));
        //     document.getElementById("selSellUnit").options.add(new Option(ProductName,ProductUntID));
        //     document.getElementById("selPurchseUnit").options.add(new Option(ProductName,ProductUntID));
        //     document.getElementById("selProductUnit").options.add(new Option(ProductName,ProductUntID));
    }
    else {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SupplyChain/ProductInfo.ashx?action=LoadUnit&GroupUnitNo=' + UnitNo, //目标地址
            cache: false,
            data: '', //数据
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
                $.each(msg.data, function(i, item) {
                    document.getElementById("selStorageUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selSellUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selPurchseUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selProductUnit").options.add(new Option(item.CodeName, item.UnitID));
                });
            },
            error: function() {
                howPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
            },
            complete: function() {

            } //接收数据完毕
        });
    }

}
//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    try {
        $("#ExtField1").val(data.ExtField1);
        $("#ExtField2").val(data.ExtField2);
        $("#ExtField3").val(data.ExtField3);
        $("#ExtField4").val(data.ExtField4);
        $("#ExtField5").val(data.ExtField5);
        $("#ExtField6").val(data.ExtField6);
        $("#ExtField7").val(data.ExtField7);
        $("#ExtField8").val(data.ExtField8);
        $("#ExtField9").val(data.ExtField9);
        $("#ExtField10").val(data.ExtField10);
        $("#ExtField11").val(data.ExtField11);
        $("#ExtField12").val(data.ExtField12);
        $("#ExtField13").val(data.ExtField13);
        $("#ExtField14").val(data.ExtField14);
        $("#ExtField15").val(data.ExtField15);
        $("#ExtField16").val(data.ExtField16);
        $("#ExtField17").val(data.ExtField17);
        $("#ExtField18").val(data.ExtField18);
        $("#ExtField19").val(data.ExtField19);
        $("#ExtField20").val(data.ExtField20);
        $("#ExtField21").val(data.ExtField21);
        $("#ExtField22").val(data.ExtField22);
        $("#ExtField23").val(data.ExtField23);
        $("#ExtField24").val(data.ExtField24);
        $("#ExtField25").val(data.ExtField25);
        $("#ExtField26").val(data.ExtField26);
        $("#ExtField27").val(data.ExtField27);
        $("#ExtField28").val(data.ExtField28);
        $("#ExtField29").val(data.ExtField29);
        $("#ExtField30").val(data.ExtField30);
    }
    catch (Error) { }
    //    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
    //    alert(data.ExtField1);
    //    var arrKey = strKey.split('|');
    //    if(typeof(data)!='undefined')
    //    {
    //       $.each(data,function(i,item){
    //            for (var t = 0; t < arrKey.length; t++) {
    //                //不为空的字段名才取值
    //                if ($.trim(arrKey[t]) != '') {
    //                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

    //                }
    //            }

    //       });
    //    }
    //}
}
//--------------------扩展属性操作----------------------------------------------------------------------------//


//---------Start  工艺明细处理 ----------
function findObj(theObj, theDoc) {
    var p, i, foundObj;
    if (!theDoc) theDoc = document;
    if ((p = theObj.indexOf("?")) > 0 && parent.frames.length) {
        theDoc = parent.frames[theObj.substring(p + 1)].document;
        theObj = theObj.substring(0, p);
    }

    if (!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj];
    for (i = 0; !foundObj && i < theDoc.forms.length; i++)
        foundObj = theDoc.forms[i][theObj]; for (i = 0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++)
        foundObj = findObj(theObj, theDoc.layers[i].document);
    if (!foundObj && document.getElementById) foundObj = document.getElementById(theObj);
    return foundObj;
}
//全选
function SelectAll() {
    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log", document);
    if (document.getElementById("checkall").checked) {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else {
        for (var i = 0; i < txtTRLastIndex - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                var objRadio = 'chk_Option_' + (i + 1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}


//function AddSignRow()
//{
// popTechObj.ShowListCheckSpecial('SignItem_TD_Sequ_Text_"+rowID+"','Check');
//  //alert(document.getElementById("HdProductInfoID").value);
//}
//function GetValue()
//{
//   var ck = document.getElementsByName("CheckboxProdID");
//        var strarr=''; 
//        var str = "";
//        for( var i = 0; i < ck.length; i++ )
//        {
//            if ( ck[i].checked )
//            {
//               strarr += ck[i].value+',';
//            }
//        }
//         str = strarr.substring(0,strarr.length-1);
//          $.ajax({
//           type: "POST",//用POST方式传输
//           dataType:"json",//数据格式:JSON
//           url:  '../../../Handler/Office/SupplyChain/Handler.ashx?str='+str,//目标地址
//           cache:false,
//               data:'',//数据
//               beforeSend:function(){},//发送数据之前
//         
//           success: function(msg){
//                    //数据获取完毕，填充页面据显示
//                    //数据列表
//                    $.each(msg.data,function(i,item)
//                    {
//                       alert(item.CodeTypeName);
//                        AddSignCheckRow(item.ProdNo);
//                   });
//                     
//                     
//                   
//                      //document.getElementById('tdResult').style.display='block';
//                      },
//             
//               complete:function(){}//接收数据完毕
//           });
//      
//}  

//添加行var rowID = parseInt(i)+1;


//验证唯一性

//function ShowTechInfo(techID)
//{
//    if(techID!=null && techID!='')
//    {
//        
//       $.ajax({
//       type: "POST",//用POST方式传输
//       dataType:"json",//数据格式:JSON
//       url:"../../../Handler/Office/ProductionManager/TechnicsArchivesInfo.ashx?ID="+techID,//目标地址
//       cache:false,          
//       success: function(msg){
//                var rowsCount = 0;
//                //数据获取完毕，填充页面据显示
//                $.each(msg.data,function(i,item){
//                        document.getElementById('TechDetailTechNo').innerHTML = item.TechNo;
//                        document.getElementById('TechDetailTechName').innerHTML = item.TechName;
//               });
//              },
//       error: function() {}, 
//       complete:function(){}
//       });
//        document.getElementById('divTechDetail').style.display='block';
//    }
//}
//若是中文则自动填充拼音缩写
function LoadPYShort() {
    var txt_ProductName = $("#txt_ProductName").val();
    if (txt_ProductName.length > 0 && isChinese(txt_ProductName)) {
        $.ajax({
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?Text=" + escape(txt_ProductName),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                //AddPop();
            },
            //complete :function(){ //hidePopup();},
            error: function() { },
            success: function(data) {
                document.getElementById('txt_PYShort').value = data.info;
            }
        });
    }
}
/*
* 相片处理
*/
function DealEmployeePhoto(flag) {
    if (flag == "undefined" || flag == "") {
        return;
    }
    //上传相片
    else if ("upload" == flag) {
        document.getElementById("uploadKind").value = "PHOTO";
        ShowUploadPhoto();
    }
    //清除相片
    else if ("clear" == flag) {
        document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";
        document.getElementById("hfPagePhotoUrl").value = '';
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url) {
    kind = document.getElementById("uploadKind").value;
    if ("PHOTO" == kind) {
        if (url != "") {
            document.getElementById("imgPhoto").src = "../../../Images/Photo/" + url;
            document.getElementById("hfPagePhotoUrl").value = url;
        }
    }
}
function Fun_Save_ProductInfo() {
    var ProdNo="";
    var fieldText="";
    var msgText="";
    var isFlag=true;
    var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
    //基本信息
    requestobj=GetRequest();
    recordnoparam=requestobj['intOtherCorpInfoID'];
    if (typeof(recordnoparam)=="undefined") {
        Action='Add';
    }
    else {
        Action='Edit';
    }
    if (document.getElementById('txtIndentityID').value != "0") {
        Action='Edit';
        ProdNo=document.getElementById("divNo").innerHTML;
    }
    if (Action=="Edit") {
        ProdNo=document.getElementById("divNo").innerHTML;
    }
    var PYShort=$("#txt_PYShort").val();
    var ProductName=trim($("#txt_ProductName").val());
    var ShortNam=trim($("#txt_ShortNam").val());
    var BarCode=$("#txt_BarCode").val();
    var TypeID=$("#txt_Code").val();
    var BigType=$("#txt_BigType").val();
    var GradeID=$("#sel_GradeID").val();
    var UnitID=$("#sel_UnitID").val();
    var Brand=$("#sel_Brand").val();
    var ColorID=$("#sel_ColorID").val();
    var Specification=$("#txt_Specification").val();
    var Size=$("#txt_Size").val();
    var Source=$("#sel_Source").val();
    var FromAddr=$("#txt_FromAddr").val();
    var DrawingNum=$("#txt_DrawingNum").val();
    var ImgUrl=document.getElementById("hfPagePhotoUrl").value;
    var FileNo=$("#txt_FileNo").val();
    var PricePolicy=$("#txt_PricePolicy").val();
    var Params=$("#txt_Params").val();
    var Questions=$("#txt_Questions").val();
    var ReplaceName=$("#txt_ReplaceName").val();
    var Description=$("#txt_Description").val();
    var StorageID=$("#sel_StorageID").val();
    var SafeStockNum=$("#txt_SafeStockNum").val();
    var MinStockNum=$("#txt_MinStockNum").val();
    var MaxStockNum=$("#txt_MaxStockNum").val();
    var ABCType=$("#sel_ABCType").val();
    var CalcPriceWays=$("#sel_CalcPriceWays").val();
    var StandardCost=$("#txt_StandardCost").val();
    var PlanCost=$("#txt_PlanCost").val();
    var StandardSell=$("#txt_StandardSell").val();
    var SellMin=$("#txt_SellMin").val();
    var SellMax=$("#txt_SellMax").val();
    var TaxRate=$("#txt_TaxRate").val();
    var InTaxRate=$("#txt_InTaxRate").val();
    var SellTax=$("#txt_SellTax").val();
    var SellPrice=$("#txt_SellPrice").val();
    var TransfrePrice=$("#txt_TransfrePrice").val();
    var Discount=$("#txt_Discount").val();
    var StandardBuy=$("#txt_StandardBuy").val();
    var TaxBuy=$("#txt_TaxBuy").val();
    var BuyMax=$("#txt_BuyMax").val();
    var Remark=$("#txt_Remark").val();
    var Creator=$("#txtPrincipal").val();
    var CreateDate=$("#txt_CreateDate").val();
    var CheckStatus=$("#sel_CheckStatus").val();
    var CheckUser="";
    var CheckDate="";
    var UsedStatus=$("#sel_UsedStatus").val();
    var StockIs="";
    var MinusIs="";
    var IsBatchNo="";
    var Manufacturer=$("#txt_Manufacturer").val();      //厂家
    var Material=$("#sel_Material").val();             //材质
    var GroupNo=$("#HdGroupNo").val();                 //计量单位组编号
    var StorageUnit=$("#selStorageUnit").val();        //库存计量单位
    var SellUnit=$("#selSellUnit").val();           //销售计量单位
    var PurchseUnit=$("#selPurchseUnit").val();     //采购计量单位
    var ProductUnit=$("#selProductUnit").val();     //生产完工计量单位
    if (document.getElementById('rd_StockIs').checked) {
        StockIs="1";
    }
    else if (document.getElementById('rd_notStockIs').checked) {
        StockIs="0";
    }
    if (document.getElementById('rd_MinusIs').checked) {
        MinusIs="1";
    }
    else if (document.getElementById('rd_notMinusIs').checked) {
        MinusIs="0";
    }
    if (document.getElementById('rd_MinusIs').checked) {
        MinusIs="1";
    }
    else if (document.getElementById('rd_notMinusIs').checked) {
        MinusIs="0";
    }
    if (document.getElementById('RdUseBatch').checked) {
        IsBatchNo="1";
    }
    else if (document.getElementById('RdNotUseBatch').checked) {
        IsBatchNo="0";
    }

    /*****************************************************************************物品控件新建验证Start**************************************************************************/
    if (CodeType == "") {
        ProdNo=trim($("#CodingRuleControl1_txtCode").val());
        if (ProdNo=="") {
            isFlag = false;
            fieldText += "物品编号|";
            msgText += "请输入物品编号|";
        }
        if (strlen(ProdNo) > 50) {
            isFlag = false;
            fieldText += "物品编号|";
            msgText += "物品编号仅限于50个字符以内|";
        }
        if (strlen(ProdNo) > 0) {
            if (!CodeCheck(ProdNo)) {
                isFlag = false;
                fieldText = fieldText + "物品编号|";
                msgText = msgText + "物品编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
            }
        }
    }
    if (ProductName == "") {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "请输入物品名称|";
    }
    if (strlen(ProductName) > 100) {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "物品名称仅限于100个字符以内|";
    }
    if (StorageID == null) {
        isFlag = false;
        fieldText += "主放仓库|";
        msgText += "请先创建主放仓库|";
    }
    if (strlen(ShortNam) > 100) {
        isFlag = false;
        fieldText += "名称简称|";
        msgText += "名称简称仅限于50个字符以内|";
    }
    if (strlen(Specification) > 100) {
        isFlag = false;
        fieldText += "规格型号|";
        msgText += "规格型号仅限于100个字符以内|";
    }
    if (!CheckSpecification(Specification)) {
        isFlag = false;
        fieldText += "规格型号|";
        msgText += "包含特殊字符";
    }
    //    if (TypeID == "") {
    //        isFlag = false;
    //        fieldText += "物品分类|";
    //        msgText += "请选择物品分类|";
    //    }
    var tmpSpecification = '';
 
    /*处理规格*/
    for (var i = 0; i < Specification.length; i++) {
        if (Specification.charAt(i) == '+') {
            tmpSpecification = tmpSpecification + '＋';
        }
        else {
            tmpSpecification = tmpSpecification + Specification.charAt(i);
        }
    }

    Specification = tmpSpecification.replace('×', '&#174');
    
    if (UnitID == "" || UnitID == null) {
        isFlag = false;
        fieldText += "基本单位|";
        msgText += "请选择基本单位|";
    }
    if (SafeStockNum != "") {
        if (!IsNumeric(SafeStockNum)) {
            isFlag = false;
            fieldText += "安全库存量|";
            msgText += "安全库存量格式不对|";
        }
        else {
            if (SafeStockNum.indexOf('.') > -1) {
                if (!IsNumeric(SafeStockNum, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "安全库存量|";
                    msgText += "安全库存量格式不对|";
                }
            }
            else if (SafeStockNum.indexOf('.') == -1) {
                if (strlen(SafeStockNum) > 8) {
                    isFlag = false;
                    fieldText += "安全库存量|";
                    msgText += "安全库存量整数部分不能超过8位|";
                }
            }
        }
    }

    if (MinStockNum != "") {
        if (!IsNumeric(MinStockNum)) {
            isFlag = false;
            fieldText += "最低库存量|";
            msgText += "最低库存量格式不对|";
        }
        else {
            if (MinStockNum.indexOf('.') > -1) {
                if (!IsNumeric(MinStockNum, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最低库存量|";
                    msgText += "最低库存量格式不对|";
                }
            }
            else if (MinStockNum.indexOf('.') == -1) {
                if (strlen(MinStockNum) > 8) {
                    isFlag = false;
                    fieldText += "最低库存量|";
                    msgText += "最低库存量整数部分不能超过8位|";
                }
            }
        }
    }

    if (MaxStockNum != "") {
        if (!IsNumeric(MaxStockNum)) {
            isFlag = false;
            fieldText += "最高库存量|";
            msgText += "最高库存量格式不对|";
        }
        else {
            if (MaxStockNum.indexOf('.') > -1) {
                if (!IsNumeric(MaxStockNum, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最高库存量|";
                    msgText += "最高库存量格式不对|";
                }
            }
            else if (MaxStockNum.indexOf('.') == -1) {
                if (strlen(MaxStockNum) > 8) {
                    isFlag = false;
                    fieldText += "最高库存量|";
                    msgText += "最高库存量量整数部分不能超过8位|";
                }
            }
        }
    }
    if (StandardCost != "") {
        if (!IsNumeric(StandardCost)) {
            isFlag = false;
            fieldText += "标准成本|";
            msgText += "标准成本格式不对|";
        }
        else {
            if (StandardCost.indexOf('.') > -1) {
                if (!IsNumeric(StandardCost, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "标准成本|";
                    msgText += "标准成本格式不对|";
                }
            }
            else if (StandardCost.indexOf('.') == -1) {
                if (strlen(StandardCost) > 8) {
                    isFlag = false;
                    fieldText += "标准成本|";
                    msgText += "标准成本整数部分不能超过8位|";
                }
            }
        }
    }
    if (PlanCost != "") {
        if (!IsNumeric(PlanCost)) {
            isFlag = false;
            fieldText += "计划成本|";
            msgText += "计划成本格式不对|";
        }
        else {
            if (PlanCost.indexOf('.') > -1) {
                if (!IsNumeric(PlanCost, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "计划成本|";
                    msgText += "计划成本格式不对|";
                }
            }
            else if (PlanCost.indexOf('.') == -1) {
                if (strlen(PlanCost) > 8) {
                    isFlag = false;
                    fieldText += "计划成本|";
                    msgText += "计划成本整数部分不能超过8位|";
                }
            }
        }
    }

    if (StandardSell != "") {
        if (!IsNumeric(StandardSell)) {
            isFlag = false;
            fieldText += "去税售价|";
            msgText += "去税售价格式不对|";
        }
        else {
            if (StandardSell.indexOf('.') > -1) {
                if (!IsNumeric(StandardSell, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "去税售价|";
                    msgText += "去税售价格式不对|";
                }
            }
            else if (StandardSell.indexOf('.') == -1) {
                if (strlen(StandardSell) > 8) {
                    isFlag = false;
                    fieldText += "去税售价|";
                    msgText += "去税售价整数部分不能超过8位|";
                }
            }
        }
    }
    if (SellMin != "") {
        if (!IsNumeric(SellMin)) {
            isFlag = false;
            fieldText += "最低售价限制|";
            msgText += "最低售价限制格式不对|";
        }
        else {
            if (SellMin.indexOf('.') > -1) {
                if (!IsNumeric(SellMin, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最低售价限制|";
                    msgText += "最低售价限制格式不对|";
                }
            }
            else if (SellMin.indexOf('.') == -1) {
                if (strlen(SellMin) > 8) {
                    isFlag = false;
                    fieldText += "最低售价限制|";
                    msgText += "最低售价限制整数部分不能超过8位|";
                }
            }
        }
    }
    if (SellMax != "") {
        if (!IsNumeric(SellMax)) {
            isFlag = false;
            fieldText += "最高售价限制|";
            msgText += "最高售价限制格式不对|";
        }
        else {
            if (SellMax.indexOf('.') > -1) {
                if (!IsNumeric(SellMax, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最高售价限制|";
                    msgText += "最高售价限制格式不对|";
                }
            }
            else if (SellMax.indexOf('.') == -1) {
                if (strlen(SellMax) > 8) {
                    isFlag = false;
                    fieldText += "最高售价限制|";
                    msgText += "最高售价限制整数部分不能超过8位|";
                }
            }
        }
    }
    if (TaxRate != "") {
        if (!IsNumeric(TaxRate)) {
            isFlag = false;
            fieldText += "销项税率|";
            msgText += "销项税率格式不对|";
        } else {
            if (parseFloat(TaxRate) > 100) {
                isFlag = false;
                fieldText += "销项税率|";
                msgText += "销项税率不能超过100|";
            }

            if (TaxRate.indexOf('.') > -1) {
                if (!IsNumeric(TaxRate, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "销项税率|";
                    msgText += "销项税率格式不对|";
                }
            }
            else if (TaxRate.indexOf('.') == -1) {
                if (strlen(TaxRate) > 8) {
                    isFlag = false;
                    fieldText += "销项税率|";
                    msgText += "销项税率整数部分不能超过8位|";
                }
            }
        }
    }
    if (InTaxRate != "") {
        if (parseFloat(InTaxRate) > 100) {
            isFlag = false;
            fieldText += "进项税率|";
            msgText += "进项税率不能超过100|";
        }

        if (!IsNumeric(InTaxRate)) {
            isFlag = false;
            fieldText += "进项税率|";
            msgText += "进项税率格式不对|";
        }
        else {
            if (InTaxRate.indexOf('.') > -1) {
                if (!IsNumeric(InTaxRate, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "进项税率|";
                    msgText += "进项税率格式不对|";
                }
            }
            else if (InTaxRate.indexOf('.') == -1) {
                if (strlen(InTaxRate) > 8) {
                    isFlag = false;
                    fieldText += "进项税率|";
                    msgText += "进项税率整数部分不能超过8位|";
                }
            }

        }
    }
    if (SellTax != "") {
        if (!IsNumeric(SellTax)) {
            isFlag = false;
            fieldText += "含税售价|";
            msgText += "含税售价格式不对|";
        }
        else {
            if (SellTax.indexOf('.') > -1) {
                if (!IsNumeric(SellTax, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "含税售价|";
                    msgText += "含税售价格式不对|";
                }
            }
            else if (SellTax.indexOf('.') == -1) {
                if (strlen(SellTax) > 8) {
                    isFlag = false;
                    fieldText += "含税售价|";
                    msgText += "含税售价整数部分不能超过8位|";
                }
            }
        }
    }
    if (SellPrice != "") {
        if (!IsNumeric(SellPrice)) {
            isFlag = false;
            fieldText += "零售价|";
            msgText += "零售价价格式不对|";
        }
        else {
            if (SellPrice.indexOf('.') > -1) {
                if (!IsNumeric(SellPrice, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "零售价|";
                    msgText += "零售价价格式不对|";
                }
            }
            else if (SellPrice.indexOf('.') == -1) {
                if (strlen(SellPrice) > 8) {
                    isFlag = false;
                    fieldText += "零售价|";
                    msgText += "零售价整数部分不能超过8位|";
                }
            }

        }
    }
    if (TransfrePrice != "") {
        if (!IsNumeric(TransfrePrice)) {
            isFlag = false;
            fieldText += "调拨单价|";
            msgText += "调拨单价价格式不对|";
        }
        else {
            if (TransfrePrice.indexOf('.') > -1) {
                if (!IsNumeric(TransfrePrice, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "调拨单价|";
                    msgText += "调拨单价价格式不对|";
                }
            }
            else if (TransfrePrice.indexOf('.') == -1) {
                if (strlen(TransfrePrice) > 8) {
                    isFlag = false;
                    fieldText += "调拨单价|";
                    msgText += "调拨单价整数部分不能超过8位|";
                }
            }
        }
    }
    if (Discount != "") {
        if (parseFloat(Discount) > 100) {
            isFlag = false;
            fieldText += "销售折扣率|";
            msgText += "销售折扣率不能超过100|";
        }
        else {
            if (!IsNumeric(Discount)) {
                isFlag = false;
                fieldText += "销售折扣率|";
                msgText += "销售折扣率价格式不对|";
            }

            if (Discount.indexOf('.') > -1) {
                if (!IsNumeric(Discount, 6, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "销售折扣率|";
                    msgText += "销售折扣率价格式不对|";
                }
            }
            else if (Discount.indexOf('.') == -1) {
                if (strlen(Discount) > 4) {
                    isFlag = false;
                    fieldText += "销售折扣率|";
                    msgText += "销售折扣率整数部分不能超过4位|";
                }
            }
        }
    }
    if (StandardBuy != "") {
        if (!IsNumeric(StandardBuy)) {
            isFlag = false;
            fieldText += "含税进价|";
            msgText += "含税进价价格式不对|";
        }
        else {
            if (StandardBuy.indexOf('.') > -1) {
                if (!IsNumeric(StandardBuy, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "含税进价|";
                    msgText += "含税进价价格式不对|";
                }
            }
            else if (StandardBuy.indexOf('.') == -1) {
                if (strlen(StandardBuy) > 8) {
                    isFlag = false;
                    fieldText += "含税进价|";
                    msgText += "含税进价整数部分不能超过8位|";
                }
            }
        }
    }
    if (TaxBuy != "") {
        if (!IsNumeric(TaxBuy)) {
            isFlag = false;
            fieldText += "去税进价|";
            msgText += "去税进价价格式不对|";
        }
        else {
            if (TaxBuy.indexOf('.') > -1) {
                if (!IsNumeric(TaxBuy, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "去税进价|";
                    msgText += "去税进价价格式不对|";
                }
            }
            else if (TaxBuy.indexOf('.') == -1) {
                if (strlen(TaxBuy) > 8) {
                    isFlag = false;
                    fieldText += "去税进价|";
                    msgText += "去税进价整数部分不能超过8位|";
                }
            }
        }
    }
    if (BuyMax != "") {
        if (!IsNumeric(BuyMax)) {
            isFlag = false;
            fieldText += "最高采购价限制|";
            msgText += "最高采购价限制价格式不对|";
        }
        else {
            if (BuyMax.indexOf('.') > -1) {
                if (!IsNumeric(BuyMax, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最高采购价限制|";
                    msgText += "最高采购价限制价格式不对|";
                }
            }
            else if (BuyMax.indexOf('.') == -1) {
                if (strlen(BuyMax) > 8) {
                    isFlag = false;
                    fieldText += "最高采购价限制|";
                    msgText += "最高采购价限制整数部分不能超过8位|";
                }
            }
        }
    }
    if (parseFloat(SellMin) > parseFloat(SellMax)) {
        isFlag = false;
        fieldText += "物品售价|";
        msgText += "最低售价限制不能大于最高售价限制|";
    }
    if (parseFloat(MinStockNum) > parseFloat(MaxStockNum)) {
        isFlag = false;
        fieldText += "库存量|";
        msgText += "最低库存量不能大于最高库存量|";
    }
    if (parseFloat(SafeStockNum) < parseFloat(MinStockNum) || (parseFloat(SafeStockNum) > parseFloat(MaxStockNum))) {
        isFlag = false;
        fieldText += "库存量|";
        msgText += "安全库存量不能大于最高库存量且小于最低库存量|";
    }
    if (CreateDate == "") {
        isFlag = false;
        fieldText = fieldText + "建档日期|";
        msgText = msgText + "请选择建档日期|";
    }
    if (strlen(CheckDate) > 0) {
        if (!isDate(CheckDate)) {
            isFlag = false;
            fieldText = fieldText + "审核日期|";
            msgText = msgText + "审核日期式不正确|";
        }
    }
    if (strlen(CreateDate) > 0) {
        if (!isDate(CreateDate)) {
            isFlag = false;
            fieldText = fieldText + "建档日期|";
            msgText = msgText + "建档日期格式不正确|";
        }
    }
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
    /*****************************************************************************物品控件新建验证End**************************************************************************/
    if (StandardCost == "" && (StandardSell == "") && (TaxBuy == "")) {
        if (!window.confirm('标准成本、去税售价、去税进价都为空值，是否继续保存？')) {
            return false;
        }
    }


    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx",
        data:'ProdNo='+escape(ProdNo)+
         '&PYShort='+escape(PYShort)+
         '&ProductName='+escape(ProductName)+
         '&ShortNam='+escape(ShortNam)+
         '&BarCode='+escape(BarCode)+
         '&TypeID='+escape(TypeID)+
         '&BigType='+escape(BigType)+
         '&GradeID='+escape(GradeID)+
         '&UnitID='+escape(UnitID)+
         '&Brand='+escape(Brand)+
         '&ColorID='+escape(ColorID)+
         '&Specification='+escape(Specification)+'&Size='+escape(Size)+
         '&Source='+escape(Source)+
         '&FromAddr='+escape(FromAddr)+
         '&DrawingNum='+escape(DrawingNum)+
         '&ImgUrl='+escape(ImgUrl)+
         '&FileNo='+escape(FileNo)+
         '&PricePolicy='+escape(PricePolicy)+
         '&Params='+escape(Params)+
         '&Questions='+escape(Questions)+
         '&ReplaceName='+escape(ReplaceName)+
         '&Description='+escape(Description)+
         '&StockIs='+escape(StockIs)+
         '&MinusIs='+escape(MinusIs)+
         '&StorageID='+escape(StorageID)+
         '&SafeStockNum='+escape(SafeStockNum)+
         '&MinStockNum='+escape(MinStockNum)+
         '&MaxStockNum='+escape(MaxStockNum)+
         '&ABCType='+escape(ABCType)+
         '&CalcPriceWays='+escape(CalcPriceWays)+
         '&StandardCost='+escape(StandardCost)+
         '&PlanCost='+escape(PlanCost)+
         '&StandardSell='+escape(StandardSell)+
         '&SellMin='+escape(SellMin)+
         '&SellMax='+escape(SellMax)+
         '&TaxRate='+escape(TaxRate)+
         '&InTaxRate='+escape(InTaxRate)+
         '&SellTax='+escape(SellTax)+
         '&SellPrice='+escape(SellPrice)+
         '&TransfrePrice='+escape(TransfrePrice)+
         '&Discount='+escape(Discount)+
         '&StandardBuy='+escape(StandardBuy)+
         '&TaxBuy='+escape(TaxBuy)+
         '&BuyMax='+escape(BuyMax)+
         '&Remark='+escape(Remark)+
         '&Creator='+escape(Creator)+
         '&CreateDate='+escape(CreateDate)+
         '&CheckStatus='+escape(CheckStatus)+
         '&CheckUser='+escape(CheckUser)+
         '&CheckDate='+escape(CheckDate)+
         '&UsedStatus='+escape(UsedStatus)+
         '&CodeType='+escape(CodeType)+
         '&Manufacturer='+escape(Manufacturer)+
         '&Material='+escape(Material)+
         '&GroupNo='+escape(GroupNo)+
         '&StorageUnit='+escape(StorageUnit)+
         '&SellUnit='+escape(SellUnit)+
         '&PurchseUnit='+escape(PurchseUnit)+'&ProductUnit='+escape(ProductUnit)+'&IsBatchNo='+escape(IsBatchNo)+'&Action='+Action+GetExtAttrValue(),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误!');
        },
        success: function(data) {
            if (data.sta > 0) {
                popMsgObj.ShowMsg(data.info);
                if (Action == 'Add') {
                    document.getElementById('txtIndentityID').value = data.sta;
                }
                document.getElementById("product_btn_AD").style.display = "block";
                document.getElementById("product_btnunsure").style.display = "none";
                document.getElementById("divNo").innerHTML = data.data;
                document.getElementById("divInputNo").style.display = "block";
                document.getElementById("divNo").style.display = "none";
                document.getElementById('CodingRuleControl1_ddlCodeRule').style.display = 'none';
                document.getElementById('CodingRuleControl1_txtCode').style.display = 'block';
                document.getElementById('CodingRuleControl1_txtCode').value = data.data;
                document.getElementById('CodingRuleControl1_txtCode').className = 'tdinput';
                document.getElementById('CodingRuleControl1_txtCode').style.width = '90%';
                //                        document.getElementById("txt_TypeID").disabled=true;
                if (Action == "Edit") {
                    document.getElementById('sel_CheckStatus').value = "0";
                }
                if (document.getElementById('sel_CheckStatus').value == "0") {
                    document.getElementById("product_btn_AD").style.display = "block";
                }
                else if (document.getElementById('sel_CheckStatus').value == "1") {
                    document.getElementById("product_btnunsure").style.display = "none";
                }
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });

}

function ChangeStatus() {
    var CheckDate = $("#txt_CheckDate").val();
    var StorageID = $("#sel_StorageID").val();
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if (strlen(CheckDate) > 0) {
        if (!isDate(CheckDate)) {
            isFlag = false;
            fieldText = fieldText + "审核日期|";
            msgText = msgText + "审核日期式不正确|";
        }
    }
    else {
        isFlag = false;
        fieldText = fieldText + "审核日期|";
        msgText = msgText + "请选择审核日期|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    if (typeof (recordnoparam) != "undefined") {
        intOtherCorpInfoID = recordnoparam;
    }
    else {
        intOtherCorpInfoID = document.getElementById('txtIndentityID').value
    }
    var Action = "ChangeStatus";
    var CheckUser = $("#txt_CheckUser").val();
    var SellTaxNew = document.getElementById("txt_SellTax").value; //含税售价
    var StandardSell = document.getElementById("txt_StandardSell").value; //去税售价
    var TaxRate = document.getElementById("txt_TaxRate").value; //销项税率
    var sum = parseFloat(StandardSell) * (parseFloat(TaxRate) / 100 + 1);
    var SellTax = document.getElementById("txt_TaxBuy").value; //含税售价
    var Standard = document.getElementById("txt_StandardBuy").value; //去税售价
    var Tax = document.getElementById("txt_InTaxRate").value; //销项税率
    var sub = parseFloat(SellTax) * (parseFloat(Tax) / 100 + 1);


    if (parseFloat(document.getElementById("txt_SellTax").value).toFixed(2) != sum.toFixed(2) || (isNaN(sum) || isNaN(parseFloat(document.getElementById("txt_SellTax").value).toFixed(2))) || (parseFloat(document.getElementById("txt_StandardBuy").value).toFixed(2) != sub.toFixed(2) || (isNaN(sub) || isNaN(parseFloat(document.getElementById("txt_StandardBuy").value).toFixed(2))))) {
        if (SellTaxNew == "" && (TaxRate == "") && (StandardSell == "") && (SellTax == "" && (Tax == "") && (Standard == ""))) {
            var msg = window.confirm("是否确认该记录？")
            if (msg == true) {
                var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                    },
                    error: function() {
                        popMsgObj.ShowMsg('请求发生错误');
                    },
                    success: function(data) {
                        if (data.sta > 0) {
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('sel_CheckStatus').value = data.sta;
                            document.getElementById('sel_UsedStatus').value = "1";
                            document.getElementById("divConfirmor").style.display = "block";
                            removesel();
                            document.getElementById('sel_UsedStatus').disabled = false;
                            document.getElementById("product_btn_AD").style.display = "none";
                            document.getElementById("product_btnunsure").style.display = "block";
                            if (isMoreUnit) {
                                document.getElementById('sel_UnitID').disabled = true;
                            }
                        }
                        else {
                            popMsgObj.ShowMsg(data.info);
                        }
                    }
                });
            }
        }
        else {
            var msg = window.confirm("调整后的含税售价不等于去税售价*（1+销项税率）或调整后的含税进价不等于去税进价*（1+进项税率），是否继续操作？")
            if (msg == true) {
                var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                    },
                    error: function() {
                        popMsgObj.ShowMsg('请求发生错误');
                    },
                    success: function(data) {
                        if (data.sta > 0) {
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('sel_CheckStatus').value = data.sta;
                            document.getElementById('sel_UsedStatus').value = "1";
                            document.getElementById("divConfirmor").style.display = "block";
                            removesel();
                            document.getElementById('sel_UsedStatus').disabled = false;
                            document.getElementById("product_btn_AD").style.display = "none";
                            document.getElementById("product_btnunsure").style.display = "block";
                        }
                        else {
                            popMsgObj.ShowMsg(data.info);
                        }
                    }
                });
            }
        }


    }
    else {
        var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
        var msg = window.confirm("是否确认该记录？")
        if (msg == true) {
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');
                },
                success: function(data) {
                    if (data.sta > 0) {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById('sel_CheckStatus').value = data.sta;
                        document.getElementById('sel_UsedStatus').value = "1";
                        document.getElementById("divConfirmor").style.display = "block";
                        removesel();
                        document.getElementById('sel_UsedStatus').disabled = false;
                        document.getElementById("product_btn_AD").style.display = "none";
                        document.getElementById("product_btnunsure").style.display = "block";
                    }
                    else {
                        popMsgObj.ShowMsg(data.info);
                    }
                }
            });
        }

    }

}

function removesel() {
    for (var i = 0; i < document.getElementById("sel_UsedStatus").options.length; i++) {
        if (document.getElementById("sel_UsedStatus").options[i].value == "") {
            document.getElementById("sel_UsedStatus").options.remove(i);
            break;
        }

    }
}
///含税售价、去税售价、税率的相互转换
function LoadSellTaxNew(isLeftToRight) {

    var StandardSellNew = document.getElementById("txt_StandardSell").value; //去税售价
    var TaxRateNew = document.getElementById("txt_TaxRate").value; //销项税率
    var SellTaxNew = document.getElementById("txt_SellTax").value; //含税售价

    var sub = parseFloat(StandardSellNew) * (parseFloat(TaxRateNew) / 100 + 1);

    if (isLeftToRight) {
        /*左：有 && 中：有*/
        if (StandardSellNew != "" && TaxRateNew != "") 
        {
            document.getElementById("txt_SellTax").value = sub.toFixed(glb_SelPoint);/*计算右边*/
        }
        /*左：空 && 中：有 && 右：有*/
        if (StandardSellNew == "" && TaxRateNew != "" && SellTaxNew != "") {
            StandardSellNew = parseFloat(SellTaxNew / (TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_StandardSell").value = StandardSellNew;/*计算左边*/
        }
        /*左：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew=="" && SellTaxNew != "") {
            document.getElementById('txt_TaxRate').value = 100 * parseFloat((SellTaxNew / StandardSellNew) - 1).toFixed(glb_SelPoint);/*计算中间*/
        }
        
    }
    else {
        /*中：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew != "") {
            StandardSellNew = parseFloat(parseFloat(SellTaxNew) / parseFloat(TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_StandardSell").value = StandardSellNew;/*计算左边*/
        }
        /*左：有 && 中：有 && 右：无*/
        if (StandardSellNew != "" && TaxRateNew != "" && SellTaxNew == "") {
            document.getElementById("txt_SellTax").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew == "" && SellTaxNew != "") {
            document.getElementById('txt_TaxRate').value = 100 * parseFloat((SellTaxNew / StandardSellNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
    }
}


function LoadSellTax(isLeftToRight) {
    var SellTaxNew = document.getElementById("txt_TaxBuy").value; //去税进价
    var TaxRateNew = document.getElementById("txt_InTaxRate").value; //进项税率
    var StandardSellNew = document.getElementById("txt_StandardBuy").value; //含税进价

    
    var sub = parseFloat(SellTaxNew) * (parseFloat(TaxRateNew) / 100 + 1);

    if (isLeftToRight) {
        /*左：有 && 中：有*/
        if (SellTaxNew != '' && TaxRateNew != '') {
            document.getElementById("txt_StandardBuy").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：空 && 中：有 && 右：有*/
        if (SellTaxNew == "" && TaxRateNew != "" && StandardSellNew != "") {
            SellTaxNew = parseFloat(StandardSellNew / (TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_TaxBuy").value = SellTaxNew; /*计算左边*/
            
        }
        /*左：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew=="" && StandardSellNew != "") {
            document.getElementById('txt_InTaxRate').value = 100 * parseFloat((StandardSellNew / SellTaxNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
        
    }
    else {
        /*中：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew != "") {
            SellTaxNew = parseFloat(parseFloat(StandardSellNew) / parseFloat(TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_TaxBuy").value = SellTaxNew; /*计算左边*/
        }
        /*左：有 && 中：有 && 右：无*/
        if (SellTaxNew != "" && TaxRateNew != "" && StandardSellNew == "") {
            document.getElementById("txt_StandardBuy").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew == "" && StandardSellNew != "") {
            document.getElementById('txt_InTaxRate').value = 100 * parseFloat((StandardSellNew / SellTaxNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
    }

}

/*验证规格只允许+和*特殊字符可以输入*/
//function CheckSpecification(str) {
//    var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
//    for (var i = 0; i < SpWord.length; i++) {
//        for (var j = 0; j < str.length; j++) {
//            if (SpWord[i] == str.charAt(j)) {
//                return false;
//                break;
//            }
//        }
//    }
//    return true;
//}



/*返回*/
function DoBack() {
    var searchCondition = document.getElementById("hidSearchCondition").value;
    if (SysModuleID>0) {
        //获取模块功能ID
        window.location.href = "../SystemManager/InitGuid.aspx?ModuleID=" + SysModuleID;
    }
    else {
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "ProductInfoList.aspx?ModuleID=" + ModuleID + searchCondition;
    }
}

