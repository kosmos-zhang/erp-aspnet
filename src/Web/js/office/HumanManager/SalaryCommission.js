/*
* 添加工资项
*/
function DoAdd(){
    //获取表格
    table = document.getElementById("tblSalary");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtSalaryID_" + no + "' value='' />"
	                + "<input type='hidden' id='txtEditFlag_" + no + "' value='0' />"
	                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "'>";
	var eventClick = "";
	var disabled = "";
	//包含销售模块时
	if(document.getElementById("txtIsHaveSell").value == "1")
	{
	    eventClick = " onclick='popTechObj.ShowList(this.id);' ";
	    disabled = " disabled ";
	}
	//项目编号
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='50'" + eventClick + "style='width:98%;' class='tdinput' id='txtItemNo_" + no + "' value='默认' readonly />";
	//项目名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='50'" + disabled + "style='width:98%;' class='tdinput' id='txtItemName_" + no + "' value='默认' />";
	//提成率
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' class='tdinput' id='txtRate_" + no + "' onchange='Number_round(this,2);'  onkeydown='Numeric_OnKeyDown();' />";
	//启用状态
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtUsedStatus_" + no + "' value='' / >"
	                    + "<select id='ddlUsedStatus_" + no + "'><option value='0'>停用</option><option value='1' selected>启用</option></select>";
	 
//	 ///来源                  
//    objTD = objTR.insertCell(-1);
//	objTD.className = "tdColInputCenter";
//	objTD.innerHTML = "<input type='text' maxlength='50'" + disabled + "style='width:98%;' class='tdinput' id='txtFlag_" + no + "' value='手动' />";

}

/*
* 设置全选CheckBox
*/
function SetCheckAll(obj){
    //获取全选择控件
    checkAll = document.getElementById("chkAll");
    //如果checkbox未选中
    if (!obj.checked)
    {
        checkAll.checked = false;
        return;
    }
    else
    {
        isSelectAll = true;        
        //获取表格
        table = document.getElementById("tblSalary");
        //获取表格行数
	    var count = table.rows.length;
	    if (count < 2) return;
	    //遍历表格中的数据，判断是否选中
	    for (var row = count - 1; row > 0; row--)
	    { 
	        //行未被删除时
	        if (table.rows[row].style.display != "none")
	        {
	            //获取选择框控件
	            chkControl = document.getElementById("chkSelect_" + row);
	            //如果未选中，则返回
	            if (!chkControl.checked)
	            {
	                isSelectAll = false;
	                break;
	            }
	        }
	    }
	    //列表中全部选中时，选择checkbox选中
	    if (isSelectAll)
	    {
	        checkAll.checked = true;
	    }
	    //列表中有一个未选中时，选择checkbox未选中
	    else
	    {
	        checkAll.checked = false;
	    }
    }
}

/*
* 全选择表格行
*/
function SelectAll(){
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById("chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
        //获取选择框控件
        chkControl = document.getElementById("chkSelect_" + row);
        //全选择
        if (isSelectAll)
        {
            chkControl.checked = true;
        }
        else
        {
            chkControl.checked = false;
        }
	}
}

/*
* 删除表格行
*/
function DoDelete(){

//  if(!confirm("删除后不可恢复，确认删除吗！"))
//      {
//      return ;
//      }
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	     var itemNo=document .getElementById ("txtItemNo_"+row ).value.Trim();
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	    	         var postParam = "Action=IsDelete&row="+row+"&itemNo=" + escape(itemNo);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryCommission.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
            ///     popMsgObj.ShowMsg('请求发生错误！');
                      //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误!");
                          document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请求发生错误!|");
            }, 
            success:function(data) 
            { 
                  
               if (data .sta==1)
               {
                  //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此项已被引用，不能被删除!");
                     document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "此项已被引用，不能被删除!|");
            	   // document.getElementById("spanMsg").style.display = "block";
	//    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "此项已被工资标准中引用，不能被删除!");
               }
               else
               {
                    try{
                     var newRow=data.data;
                     //alert (newRow );
                     //alert (newRow );
                   // table.rows[newRow].style.display = "none";
	       //checkbox状态改为未选中状态，也免再删除其他记录时出现错误
	             //  chkControl.checked = false;
	             //  document.getElementById("chkSelect_" + newRow).checked = false;
	               table.rows[newRow].style.display = "none";
	               }
	               catch(e){}
	       //改变排列顺序
//	        debugger ;
	          
               }
            } 
        });
	    
	    
	    
	    
	    
	    
	    
	       //删除行，实际是隐藏该行
	   //    table.rows[row].style.display = "none";
	    }
	}
}

/*
* 保存操作
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckInput())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryCommission.ashx",
        data : postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
           // AddPop();
        }, 
        error: function()
        {
     //       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
             document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请求发生错误！|");
        }, 
        success:function(data) 
        {
            if(data.EditStatus == 1) 
            {                
                //获取表格
                table = document.getElementById("tblSalary");
                //获取行数
                var count = table.rows.length;
                //删除既有行
                for (var x = count - 1; x > 0; x--)
                {
                    table.deleteRow(x);
                }
                //设置工资项信息
                $.each(data.DataInfo
                    ,function(i,item)
                    {
                        //获取行号
                        var no = table.rows.length;
	                    var objTR = table.insertRow(-1);
	                    //选择框
	                    var objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='hidden' id='txtSalaryID_" + no + "' value='" + item.ID + "' />"
	                                    + "<input type='hidden' id='txtEditFlag_" + no + "' value='1' />"
	                                    + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "' />";
	                                    
                        //项目编号
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='50' disabled style='width:98%;' class='tdinput' id='txtItemNo_" + no + "' value='" + item.ItemNo + "'/>";
	                    var disabled = "";
	                    //包含销售模块时
	                    if(document.getElementById("txtIsHaveSell").value.Trim() == "1")
	                    {
	                        disabled = " disabled ";
	                    }
	                    //项目名称
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='50'" + disabled + "style='width:98%;' class='tdinput' id='txtItemName_" + no + "'value='" + item.ItemName + "' />";
	                    //提成率
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='12' style='width:98%;' class='tdinput' id='txtRate_" + no + "' value='" + item.Rate + "' />";
	
	                    var selectOne = "";
	                    var selectZero = "";
	                    //启用状态
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    if ("0" == item.UsedStatus)
	                    {
	                        selectOne = "";
	                        selectZero = "selected";
	                    }
	                    else
	                    {
	                        selectOne = "selected";
	                        selectZero = "";
	                    }
	                    
	                        objTD.innerHTML = "<input type='hidden' id='txtUsedStatus_" + no + "' value='" + item.UsedStatus + "' / >"
	                            + "<select id='ddlUsedStatus_" + no + "'><option value='0' " + selectZero 
	                            + ">停用</option><option value='1' " + selectOne + ">启用</option></select>";
	                
	                
                });
                //设置提示信息
              //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                      document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存成功！|");
	       hidePopup();
            }
            else if(data.EditStatus == 2) 
            {
             //   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif", data.ItemNo + " 已经被使用,您不能将其删除或改为停用,请确认！");
                  document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", data.ItemNo + " 已经被使用,您不能将其删除或改为停用,请确认!|");
            }
            else 
            { 
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                
                   document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存失败,请确认!|");
            } 
        } 
    }); 
}

/*
* 获取提交的基本信息
*/
function GetPostParams()
{    
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var salaryCount = 0;
    var strParams = "RandNum=" + Math.random();
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长工资项数
            salaryCount++;
	        //工资编号
            strParams += "&SalaryID_" + salaryCount + "=" + escape ( document.getElementById("txtSalaryID_" + i).value.Trim());
	        //编辑模式
            strParams += "&EditFlag_" + salaryCount + "=" +escape (  document.getElementById("txtEditFlag_" + i).value.Trim());
            //项目编号
            strParams += "&ItemNo_" + salaryCount + "=" + escape ( document.getElementById("txtItemNo_" + i).value.Trim());
            //项目名称
            strParams += "&ItemName_" + salaryCount + "=" + escape ( document.getElementById("txtItemName_" + i).value.Trim());
            //单价
            strParams += "&Rate_" + salaryCount + "=" + escape ( document.getElementById("txtRate_" + i).value.Trim());
            //启用状态
            strParams += "&UsedStatus_" + salaryCount + "=" + escape ( document.getElementById("ddlUsedStatus_" + i).value.Trim());
            strParams += "&UsedStatusModify_" + salaryCount + "=" + escape ( document.getElementById("txtUsedStatus_" + i).value.Trim());
	    }
	    else
	    {
	        //获取编辑模式
	        editFlag = document.getElementById("txtEditFlag_" + i).value.Trim();
	        //新建又被删除时，忽略不计，既有的数据，执行数据库删除
	        if (editFlag == "1")
	        {
                //增长工资项数
                salaryCount++;
	            //工资编号
                strParams += "&SalaryID_" + salaryCount + "=" +escape (  document.getElementById("txtSalaryID_" + i).value.Trim());
                strParams += "&ItemNo_" + salaryCount + "=" +escape (  document.getElementById("txtItemNo_" + i).value.Trim());
	            //编辑模式
                strParams += "&EditFlag_" + salaryCount + "=3";
                strParams += "&UsedStatusModify_" + salaryCount + "=" + escape ( document.getElementById("txtUsedStatus_" + i).value.Trim());
	        }
	    }
	}
	//工资项记录数
    strParams += "&SalaryCount=" + salaryCount;
    //返回参数信息
    return strParams;
}

/*
* 输入信息校验
*/
function CheckInput()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
    //定义变量
    var salaryCount = 0;
    //遍历行号大于删除行的，修改其排列顺序
	for (var i = 1 ; i < count ; i++)
	{
	    //行未被删除时
	    if (table.rows[i].style.display != "none")
	    {
	        salaryCount++;
	        //项目编号
	        itemNo = document.getElementById("txtItemNo_" + i).value.Trim();
	        //项目编号未输入时
//	        if (itemNo == null || itemNo == "")
//	        {
//                isErrorFlag = true;
//                fieldText += "行 " + salaryCount + "：项目编号|";
//                msgText += "请输入项目编号|";  
//	        }
//	        else
//            if (itemNo != "")
//	        {
//	            //字符校验
//                if (isnumberorLetters(itemNo))
//                {
//                    isErrorFlag = true;
//                    fieldText += "行 " + salaryCount + "：项目编号|";
//                    msgText += "项目编号只能包含字母或数字！|";
//                }
//             }
             //重复性校验
            for (var j = i + 1; j < count; j++)
            {
                //行未被删除时
                if (table.rows[j].style.display != "none")
                {
                    tempNo = document.getElementById("txtItemNo_" + j).value.Trim();
                    //如果名称一样时，提示错误信息
                    
                    if (itemNo == tempNo)
                    {
                        isErrorFlag = true;
                        fieldText += "行 " + salaryCount + "：项目编号|";
                        msgText += "存在重复的项目编号|";
                    }
                }
            }
	        //项目名称
	        itemName = document.getElementById("txtItemName_" + i).value.Trim();
	        //项目名称未输入时
//	        if (itemName == null || itemName == "")
//	        {
//                isErrorFlag = true;
//                fieldText += "行 " + salaryCount + "：项目名称|";
//                msgText += "请输入项目名称|";  
//	        }
	        //单价
	        unitPrice = document.getElementById("txtRate_" + i).value.Trim();
	        //单价未输入时
	        if (unitPrice == null || unitPrice == "")
	        {
                isErrorFlag = true;
                fieldText += "行 " + salaryCount + "：提成率|";
                msgText += "请输入提成率|";  
	        }
	        else if (!IsNumeric(unitPrice, 3, 2))
	        {
                isErrorFlag = true;
                fieldText += "行 " + salaryCount + "：提成率|";
                msgText += "请输入正确的提成率|";  
	        }
	    }
	}
	
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }
    
    return isErrorFlag;
}

///////创建层（）
function CreateErrorMsgDiv(fieldText, msgText)
{
    errorMsg = "";
    if(fieldText != null && fieldText != "" && msgText != null && msgText != "")
    {
        var fieldArray = fieldText.split("|");
        var alertArray = msgText.split("|");
        for(var i = 0; i < fieldArray.length - 1; i++)
        {
            errorMsg += "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString()
                        + "</font><strong>]</strong>：" + alertArray[i].toString() + "<br />";
        }
    }
    table = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>"
            + "<tr><td align='center' height='1'>&nbsp;</td></tr>"
            + "<tr><td align='center'>" + errorMsg + "</td></tr>"
            + "<tr><td align='right'>"
            + "<img src='../../../Images/Button/closelabel.gif' onclick=\"document.getElementById('spanMsg').style.display='none';\" />"
            + "&nbsp;&nbsp;</td></tr></table>";
	
	return table;
} 

/*
* 物品选择页面返回处理
*/
function Fun_FillParent_Content(id,ProNo,ProdName,UnitPrice,UnitID,UnitName,b,c,d,Specification)
{
    //获取行号
    var rowNo = popTechObj.InputObj.substring(10, popTechObj.InputObj.length);
    //设置编号
    document.getElementById("txtItemNo_" + rowNo).value = unescape ( ProNo);
    //设置名称
    document.getElementById("txtItemName_" + rowNo).value = unescape ( ProdName);
    //隐藏物品选择DIV
    document.getElementById("divStorageProduct").style.display = "none";
}

/*载入*/
    window.onload=function(){
        fnGetExtAttr();//物品扩展属性
    }