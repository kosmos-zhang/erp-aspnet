/*
* 添加社会保险
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
	//保险名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='50' style='width:98%;' class='tdinput' id='txtInsuranceName_" + no + "'  />";
	//单位比例
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' class='tdinput' onchange='Number_round(this,2);'   id='txtCompanyPayRate_" + no + "'  />";
	//个人比例
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' class='tdinput' onchange='Number_round(this,2);'  id='txtPersonPayRate_" + no + "'  />";
	//投保方式
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select id='ddlInsuranceWay_" + no + "'><option value='1' selected>按月</option><option value='2'>按年</option></select>";
	//启用状态
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtUsedStatusModify_" + no + "' value='' / >"
	                + "<select id='ddlUsedStatus_" + no + "'><option value='0'>停用</option><option value='1' selected>启用</option></select>";

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
	    //行未被删除时
	    if (table.rows[row].style.display != "none")
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
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	       //删除行，实际是隐藏该行
	       table.rows[row].style.display = "none";
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
        url: "../../../Handler/Office/HumanManager/SalaryInsuSocial.ashx?" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
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
                //设置社会保险信息
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
	                    //保险名称
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='50' style='width:98%;' class='tdinput' id='txtInsuranceName_" + no + "' value='" + item.InsuranceName + "' />";
	                    //单位比例
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' class='tdinput' id='txtCompanyPayRate_" + no + "' value='" + item.CompanyPayRate + "' />";
	                    //个人比例
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='6' style='width:98%;' class='tdinput' id='txtPersonPayRate_" + no + "' value='" + item.PersonPayRate + "' />";
	                    //投保方式
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    var selectOne = "";
	                    var selectTwo = "";
	                    if ("1" == item.InsuranceWay)
	                    {
	                        selectOne = "selected";
	                        selectTwo = "";
	                    }
	                    else
	                    {
	                        selectTwo = "selected";
	                        selectOne = "";
	                    }
	                    objTD.innerHTML = "<select id='ddlInsuranceWay_" + no + "'><option value='1' " + selectOne + ">按月</option><option value='2' " + selectTwo + ">按年</option></select>";
	                    //启用状态
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    if ("0" == item.UsedStatus)
	                    {
	                        selectOne = "selected";
	                        selectTwo = "";
	                    }
	                    else
	                    {
	                        selectOne = "";
	                        selectTwo = "selected";
	                    }
	                    objTD.innerHTML = "<input type='hidden' id='txtUsedStatusModify_" + no + "' value='" + item.UsedStatus + "' / >"
	                                + "<select id='ddlUsedStatus_" + no + "'><option value='0' " + selectOne + ">停用</option><option value='1' " + selectTwo + ">启用</option></select>";
	                
                });
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.EditStatus == 2) 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif", data.InsuranceName + " 已经被使用,您不能将其删除或改为停用,请确认！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
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
    var insuSocialCount = 0;
    var strParams = "RandNum=" + Math.random();
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长社会保险数
            insuSocialCount++;
	        //社会保险ID
            strParams += "&SalaryID_" + insuSocialCount + "=" +escape (  document.getElementById("txtSalaryID_" + i).value.Trim());
	        //编辑模式
            strParams += "&EditFlag_" + insuSocialCount + "=" + escape ( document.getElementById("txtEditFlag_" + i).value.Trim());
            //社会保险名称
            strParams += "&InsuranceName_" + insuSocialCount + "=" +escape (  document.getElementById("txtInsuranceName_" + i).value.Trim());
            //单位比例
            strParams += "&CompanyPayRate_" + insuSocialCount + "=" + escape ( document.getElementById("txtCompanyPayRate_" + i).value.Trim());
            //个人比例
            strParams += "&PersonPayRate_" + insuSocialCount + "=" + escape ( document.getElementById("txtPersonPayRate_" + i).value.Trim());
            //投保方式
            strParams += "&InsuranceWay_" + insuSocialCount + "=" + escape ( document.getElementById("ddlInsuranceWay_" + i).value.Trim());
            //启用状态
            strParams += "&UsedStatus_" + insuSocialCount + "=" + escape ( document.getElementById("ddlUsedStatus_" + i).value.Trim());
            strParams += "&UsedStatusModify_" + insuSocialCount + "=" + escape ( document.getElementById("txtUsedStatusModify_" + i).value.Trim());
	    }
	    else
	    {
	        //获取编辑模式
	        editFlag = document.getElementById("txtEditFlag_" + i).value;
	        //新建又被删除时，忽略不计，既有的数据，执行数据库删除
	        if (editFlag == "1")
	        {
                //增长社会保险数
                insuSocialCount++;
	            //工资编号
                strParams += "&SalaryID_" + insuSocialCount + "=" + escape ( document.getElementById("txtSalaryID_" + i).value.Trim());
	            //编辑模式
                strParams += "&EditFlag_" + insuSocialCount + "=3";
                //社会保险名称
                strParams += "&InsuranceName_" + insuSocialCount + "=" +escape (  document.getElementById("txtInsuranceName_" + i).value.Trim());
                strParams += "&UsedStatusModify_" + insuSocialCount + "=" + escape ( document.getElementById("txtUsedStatusModify_" + i).value.Trim());
	        }
	    }
	}
	//社会保险记录数
    strParams += "&InsuSocialCount=" + insuSocialCount;
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
    var insuSocialCount = 0;
    
        if (count ==1)
    {
//    alert (count );
                 isErrorFlag = true;
                fieldText += "提示|";
                msgText += "请添加新项|";  
    }
    else
    {
    //遍历行号大于删除行的，修改其排列顺序
	for (var i = 1 ; i < count ; i++)
	{
	    //行未被删除时
	    if (table.rows[i].style.display != "none")
	    {
	        insuSocialCount++;
	        //社会保险名称
	        itemName = document.getElementById("txtInsuranceName_" + i).value.Trim();
	        //社会保险名称未输入时
	        if (itemName == null || itemName == "")
	        {
                isErrorFlag = true;
                fieldText += "行 " + insuSocialCount + "：保险名称|";
                msgText += "请输入保险名称|";  
	        }
	        else
	        {
	            for (var j = i + 1; j < count; j++)
	            {
	                //行未被删除时
	                if (table.rows[j].style.display != "none")
	                {
	                    tempName = document.getElementById("txtInsuranceName_" + j).value.Trim();
	                    //如果名称一样时，提示错误信息
	                    if (itemName == tempName)
	                    {
                            isErrorFlag = true;
                            fieldText += "行 " + insuSocialCount + "：保险名称|";
                            msgText += "存在重复的保险名称|";
	                    }
	                }
	            }
	        }
	        //单位比例
	        companyRate = document.getElementById("txtCompanyPayRate_" + i).value.Trim();
	        //未输入时
	        if (companyRate == null || companyRate == "")
	        {
                isErrorFlag = true;
                fieldText += "行 " + insuSocialCount + "：单位比例|";
                msgText += "请输入单位比例|";  
	        }
	        else if (!IsNumeric(companyRate, 3, 2))
	        {
                isErrorFlag = true;
                fieldText += "行 " + insuSocialCount + "：单位比例|";
                msgText += "请输入正确的单位比例|";  
	        }
	        //个人比例
	        personRate = document.getElementById("txtPersonPayRate_" + i).value.Trim();
	        //未输入时
	        if (personRate == null || personRate == "")
	        {
                isErrorFlag = true;
                fieldText += "行 " + insuSocialCount + "：个人比例|";
                msgText += "请输入个人比例|";  
	        }
	        else if (!IsNumeric(personRate, 3, 2))
	        {
                isErrorFlag = true;
                fieldText += "行 " + insuSocialCount + "：个人比例|";
                msgText += "请输入正确的个人比例|";  
	        }
	        
//	        if((personRate==null ||personRate=="")||  (companyRate==null ||companyRate==""))
//	        {}
//	        else{
//	                    if (parseFloat (personRate )+parseFloat (companyRate )!=parseFloat (100))
//	                    { 
//	                        isErrorFlag = true;
//                            fieldText += "行 " + insuSocialCount + "：错误|";
//                            msgText += "单位比例与个人比例和为100|";  
//	                    }
//	        }
	        
	        
	        
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