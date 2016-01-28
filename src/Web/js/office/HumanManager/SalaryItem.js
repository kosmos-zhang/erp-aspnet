/*
* 添加工资项
*/
function DoAdd(){
    //获取表格
    table = document.getElementById("tblSalary");
    //获取行号
  
    var no = table.rows.length;
    rowNo = no;
    //获取最后未被删除的行号
    for (var i = no - 1; i > 0; i--)
    {
   
	    //行未被删除时
	    if (table.rows[i].style.display != "none")
	    {
	        rowNo = parseInt(document.getElementById("tdRowNo_" + i).innerHTML) + 1;
	        break;   
	    }
	    else
	    {
	    if (i==1)
	    {
	    rowNo=1;
	    break ;
	    }
	    }
    }
    var s=rowNo ;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtItemNo_" + no + "' value='' />"
	                + "<input type='hidden' id='txtEditFlag_" + no + "' value='0' />"
	                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "'>";
	                
	   	//工资项编号
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
//	objTD.id = "txtItem2No_" + no;
	objTD.innerHTML = "<input type='text' readonly='readonly'  id='txtItem2No_" +no + "'  class='tdinput'  value='' /> "                                          
	//排列顺序
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.id = "tdRowNo_" + no;
	objTD.innerHTML = rowNo;
	//名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='50' style='width:98%;' class='tdinput' id='txtSalaryName_" + no + "' />";
	//计算公式
	objTD = objTR.insertCell(-1);
//	objTD .style.display="none";
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtCalculate_" + no + "'><input type='hidden' id='txtCalculateParam_" + no + "'    ><a href='#' onclick='DoEditCalculate(+\"" +''+ "\" ,\"" + no + "\");'>编辑计算公式</a>";
	//备注
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='text' maxlength='100' style='width:98%;' class='tdinput' id='txtRemark_" + no + "' />";
	//是否扣款
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select id='ddlPayFlag_" + no + "'><option value='0'>否</option><option value='1'>是</option></select>";
	//是否固定
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<select id='ddlChangeFlag_" + no + "'><option value='0'>否</option><option value='1'>是</option></select>";
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
 
 function checkDelete(itemNo)
 { 


var list=new Array ();
  if (itemNo =="" || itemNo =="undefined")
               {
        return list ;
               }
   table = document.getElementById("tblSalary");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var salaryCount = 0;
 
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长工资项数
            salaryCount++;
	        //工资编号    
               var     itemNoAll=document.getElementById("txtItemNo_" + i).value.Trim();
               var name=document.getElementById("txtSalaryName_" + i).value.Trim();
               if (itemNoAll==itemNo )
               {
               continue ;
               }
               var  caculateParam=document.getElementById("txtCalculateParam_" + i).value.Trim();
               if (caculateParam =="" || caculateParam =="undefined")
               {
               continue ;
               }
               else
               {
                       var sArray=new Array ();
                       sArray =caculateParam.split(",");
                       for (var a=0;a<sArray .length;a++)
                       {
                          if (sArray[a] ==itemNo )
                          {
                          list .push (name );
                          }
                       }
               }
               
               
            }
      }      
              return list ;
 }


function DoDelete(){
//      if(!confirm("删除后不可恢复，确认删除吗！"))
//      {
//      return ;
//      }
//    debugger ;
 
    var ArrayList=new  Array ();
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
	          var name=document.getElementById("txtSalaryName_" + row).value.Trim();
	     // alert (itemNo );
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	    
	  var s=  checkDelete(itemNo );
	  if (s.length >0)
	  {
	  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",s+" 固定工资项计算公式里已引用  "+name+"，不能被删除 !");
	  return ;
	  }
//  var itemNo= chkControl.value;
//  alert (itemNo );
//  chkSelect_
  //document .getElementById ("txtItemNo_"+row ).value;
   //ArrayList.push (itemNo );
////	           var ss=   isDetete(ItemNo);
////	           for (var i=0;i<50000;i++)
////	           {
////	           
////	           }
////	           alert (ss);
  var postParam = "Action=IsDelete&row="+row+"&itemNo=" + escape(itemNo);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryItem.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('请求发生错误！');
            }, 
            success:function(data) 
            { 
      
   
               if (data .sta==1)
               {
                 showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此项已被工资标准中引用，不能被删除!");
            
               }
               else
               {
              
                     var newRow=data.data;
                     //alert (newRow );
                    table.rows[newRow].style.display = "none";
	       //checkbox状态改为未选中状态，也免再删除其他记录时出现错误
	             //  chkControl.checked = false;
	               document.getElementById("chkSelect_" + newRow).checked = false;
	       //改变排列顺序
//	        debugger ;
	                ChangeRowNo(newRow);
               }
            } 
        });
        
        
//	       //删除行，实际是隐藏该行
//	     table.rows[row].style.display = "none";
//	       //checkbox状态改为未选中状态，也免再删除其他记录时出现错误
//	               chkControl.checked = false;
//	       //改变排列顺序
//	                ChangeRowNo(row);
	                
	             
	    }
	}
//	   isDetete (ArrayList );
}

/*
* 改变行号
*/
function ChangeRowNo(row)
{

    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (row < count)
	{
	    //遍历行号大于删除行的，修改其排列顺序
		for (var i =parseInt (row,10)  + 1 ; i < count ; i++)
		{
		    //行未被删除时
		    if (table.rows[i].style.display != "none")
		    {
		        //当前排列顺序减一
			    document.getElementById("tdRowNo_" + i).innerHTML = parseInt(document.getElementById("tdRowNo_" + i).innerHTML) - 1;
		    }
		}
	}
}

/*
* 上移，下移操作
*/
function DoChangeRow(flag)
{  
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var selectRow = 0;
    //遍历表格中的数据，判断是否选中
    for (var row = 1; row < count; row++)
    {
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    if (chkControl.checked)
	    {
	        //已选择了一行时
	        if (selectRow > 0)
	        {
              //  popMsgObj.ShowMsg("一次只能移动一条记录！");
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","一次只能移动一条记录！");
	            return;
	        }
	        //未有选择行时，将值改为选中行
	        else
	        {
	            selectRow = row;
	        }
	    }
    }
    //一行都未选择时
    if (selectRow == 0)
    {
       // popMsgObj.ShowMsg("请选择一条记录！");
             showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一条记录！");
        return;
    }
    //执行 换行
    SwapRow(selectRow, flag);
}

/*
* 上移或者下移处理
* rowno 行号
* flag  上移 up 下移 down
*/
function SwapRow(rowno, flag)
{
    //获取表格
    table = document.getElementById("tblSalary");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	if (rowno > count || rowno < 1) return;
	var otherRow = 0;
	if ("up" == flag)
	{
	    //遍历表格中的数据，判断是否选中
	    for (var row = rowno - 1; row > 0; row--)
	    {
            //非隐藏行时，交换两行值  = "none"
            displayName = table.rows[row].style.display;
            if ("none" != displayName)
            {
                otherRow = row;
                break;
            }
	    }
        if(otherRow == 0)
        {
        //    popMsgObj.ShowMsg("首记录不能上移！");
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","首记录不能上移！");
            return;
        }
	}
	else
	{
	    //遍历表格中的数据，判断是否选中
	    for (var row = rowno + 1; row < count; row++)
	    {
            //非隐藏行时，交换两行值  = "none"
            displayName = table.rows[row].style.display;
            if ("none" != displayName)
            {
                otherRow = row;
                break;
            }
	    }
	    if(otherRow == 0)
	    {
          //  popMsgObj.ShowMsg("末记录不能下移！");
                 showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","末记录不能下移！");
            return;
	    }
	}
	//交换两行值
	ChangeRowData(rowno, otherRow);
}

/*
* 交换两行值
* x 交换行
* y 被交换行
*/
function ChangeRowData(x,y)
{
	var objX,objY;
	//工资编号
	objX = document.getElementById("txtItemNo_" + x);
	objY = document.getElementById("txtItemNo_" + y);
	var str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//编辑模式
	objX = document.getElementById("txtEditFlag_" + x);
	objY = document.getElementById("txtEditFlag_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//选择
	objX = document.getElementById("chkSelect_" + x);
	objY = document.getElementById("chkSelect_" + y);
	var flag = objX.checked;
	objX.checked = objY.checked;
	objY.checked = flag;
	//名称
	objX = document.getElementById("txtSalaryName_" + x);
	objY = document.getElementById("txtSalaryName_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//是否扣款
	objX = document.getElementById("ddlPayFlag_" + x);
	objY = document.getElementById("ddlPayFlag_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//是否固定
	objX = document.getElementById("ddlChangeFlag_" + x);
	objY = document.getElementById("ddlChangeFlag_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//计算公式
//	objX = document.getElementById("txtCalculate_" + x);
//	objY = document.getElementById("txtCalculate_" + y);
//	str = objX.value;
//	objX.value = objY.value;
//	objY.value = str;
	//启用状态
	objX = document.getElementById("ddlUsedStatus_" + x);
	objY = document.getElementById("ddlUsedStatus_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
	//备注
	objX = document.getElementById("txtRemark_" + x);
	objY = document.getElementById("txtRemark_" + y);
	str = objX.value;
	objX.value = objY.value;
	objY.value = str;
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
        url: "../../../Handler/Office/HumanManager/SalaryItem.ashx" ,
        data : postParams,//目标地址
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
       if(data.EditStatus == 2) 
       {
       //alert (data .itemName)
          hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功,工资项"+data.ItemNo+"已被引用，不可删除或停用");
       }
        else   if(data.EditStatus == 1) 
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
	                    objTD.innerHTML = "<input type='hidden' id='txtItemNo_" + no + "' value='" + item.ItemNo + "' />"
	                                    + "<input type='hidden' id='txtEditFlag_" + no + "' value='1' />"
	                                    + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + no + "'   title='" + item.ItemNo + "' />";
	                                     	//工资项编号
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
//	objTD.id = "txtItem2No_" + no;
	objTD.innerHTML = "<input type='text' readonly='readonly'  id='txtItem2No_" +no + "'  class='tdinput'   value='" + item.ItemNo + "'  /> "           
	                    //排列顺序
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.id = "tdRowNo_" + no;
	                    objTD.innerHTML = item.ItemOrder;
	                    //名称
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='50' style='width:98%;' class='tdinput' id='txtSalaryName_" + no + "' value='" + item.ItemName + "' />";
	                    //计算公式
	                    
//	                    objTD = objTR.insertCell(-1);
//	                    objTD .style.display="none";
//	                    objTD.className = "tdColInputCenter";
//	                    objTD.innerHTML = "<input type='hidden' id='txtCalculate_" + no + "'  value='" + item.Calculate + "' /><a href='#' onclick='DoEditCalculate(\"" + no + "\");'>编辑计算公式</a>";

var s=item.Calculate;
//计算公式    
	objTD = objTR.insertCell(-1);
//	objTD .style.display="none";
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='hidden' id='txtCalculate_" + no + "'  value='" + s.replace(/A/g,   "+") + "' ><input type='hidden' id='txtCalculateParam_" + no + "'  value='" + item.ParamsList+ "' ><a href='#' onclick='DoEditCalculate(+\"" + item.ItemNo + "\" ,\"" + no + "\");'>编辑计算公式</a>";

	                    //备注
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    objTD.innerHTML = "<input type='text' maxlength='100' style='width:98%;' class='tdinput' id='txtRemark_" + no + "'  value='" + item.Remark + "' />";
	                    //是否扣款
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    var selectOne = "";
	                    var selectZero = "";
	                    if ("0" == item.PayFlag)
	                    {
	                        selectOne = "";
	                        selectZero = "selected";
	                    }
	                    else
	                    {
	                        selectOne = "selected";
	                        selectZero = "";
	                    }
	                    objTD.innerHTML = "<select id='ddlPayFlag_" + no + "'><option value='0' " + selectZero + ">否</option><option value='1' " + selectOne + ">是</option></select>";
	                    //是否固定
	                    objTD = objTR.insertCell(-1);
	                    objTD.className = "tdColInputCenter";
	                    if ("0" == item.ChangeFlag)
	                    {
	                        selectOne = "";
	                        selectZero = "selected";
	                    }
	                    else
	                    {
	                        selectOne = "selected";
	                        selectZero = "";
	                    }
	                    objTD.innerHTML = "<select id='ddlChangeFlag_" + no + "'><option value='0' " + selectZero + ">否</option><option value='1' " + selectOne + ">是</option></select>";
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
	                    objTD.innerHTML = "<input type='hidden' id='txtUsedStatusModify_" + no + "' value='" + item.UsedStatus +"' / >"
	                                    + "<select id='ddlUsedStatus_" + no + "'><option value='0' " + selectZero + ">停用</option><option value='1' " + selectOne + ">启用</option></select>";
	                
                });
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
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
            strParams += "&ItemNo_" + salaryCount + "=" +escape ( document.getElementById("txtItemNo_" + i).value.Trim());
	        //编辑模式
            strParams += "&EditFlag_" + salaryCount + "=" + escape(document.getElementById("txtEditFlag_" + i).value.Trim());
            //名称
            strParams += "&SalaryName_" + salaryCount + "=" + escape(document.getElementById("txtSalaryName_" + i).value.Trim());
            //是否扣款
            strParams += "&PayFlag_" + salaryCount + "=" + escape(document.getElementById("ddlPayFlag_" + i).value.Trim());
            //是否固定
            strParams += "&ChangeFlag_" + salaryCount + "=" + escape(document.getElementById("ddlChangeFlag_" + i).value.Trim());
            //计算公式
    

            strParams += "&Calculate_" + salaryCount + "=" +   (document.getElementById("txtCalculate_" + i).value.replace(/\+/g,   "A"));
             strParams += "&CalculateParam_" + salaryCount + "=" +   (document.getElementById("txtCalculateParam_" + i).value);
            //启用状态
            strParams += "&UsedStatus_" + salaryCount + "=" + escape(document.getElementById("ddlUsedStatus_" + i).value.Trim());
            //备注
            strParams += "&Remark_" + salaryCount + "=" + escape(document.getElementById("txtRemark_" + i).value.Trim());
            strParams += "&UsedStatusModify_" + salaryCount + "=" + document.getElementById("txtUsedStatusModify_" + i).value.Trim();
	    }
	    else
	    {
	        //获取编辑模式
	        editFlag = document.getElementById("txtEditFlag_" + i).value;
	        //新建又被删除时，忽略不计，既有的数据，执行数据库删除
	        if (editFlag == "1")
	        {
                //增长工资项数
                salaryCount++;
	            //工资编号
                strParams += "&ItemNo_" + salaryCount + "=" + escape ( document.getElementById("txtItemNo_" + i).value.Trim());
	            //编辑模式
                strParams += "&EditFlag_" + salaryCount + "=3";
                strParams += "&UsedStatusModify_" + salaryCount + "=" +escape (  document.getElementById("txtUsedStatusModify_" + i).value.Trim());
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
           if (count ==1)
    {
//    alert (count );
                 isErrorFlag = true;
                fieldText += "提示|";
                msgText += "请添加新项|";  
    }
    else
    {
	for (var i = 1 ; i < count ; i++)
	{
	    //行未被删除时
	    if (table.rows[i].style.display != "none")
	    {
	        salaryCount++;
	        //工资项名称
	        itemName = document.getElementById("txtSalaryName_" + i).value.Trim();
	        //工资项名称未输入时
	        if (itemName == null || itemName == "")
	        {
                isErrorFlag = true;
                fieldText += "行 " + salaryCount + "：名称|";
                msgText += "请输入名称|";  
	        }
	        else
	        {
	            for (var j = i + 1; j < count; j++)
	            {
	                //行未被删除时
	                if (table.rows[j].style.display != "none")
	                {
	                    tempName = document.getElementById("txtSalaryName_" + j).value.Trim();
	                    //如果名称一样时，提示错误信息
	                    if (itemName == tempName)
	                    {
                            isErrorFlag = true;
                            fieldText += "行 " + salaryCount + "：名称|";
                            msgText += "您输入了重复的名称|";
	                    }
	                }
	            }
	        }
	    }
	}
	}
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);
        
	    document.getElementById("spanMsg").style.display = "block";
        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").style.top = "240px";
	    document.getElementById("spanMsg").style.left = "450px";
	    document.getElementById("spanMsg").style.width = "290px";
	    document.getElementById("spanMsg").style.position = "absolute";
	    document.getElementById("spanMsg").style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true)";
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
* 编辑计算公式
*/
function DoEditCalculate(itemNo,rowNo)
{ 

 if (itemNo =='')
 {
 alert("请点击保存后，再编辑公式");
 return ;
 }
 
//    AlertMsg();
    document .getElementById ("hidItemNo").value=itemNo;
    document .getElementById ("hidRowNo").value=rowNo;
    var roeid="txtCalculate_"+rowNo;
        document .getElementById ("inCaculator1").value=document .getElementById (roeid ).value ;
    document .getElementById ("divEditCheckItem").style.display="Block";
    
 
}

function checkString(str)
{

var a=str .indexOf("{");
var b=str .indexOf("}");
if (a>=0 && b>0)
{
return true ;
}
else
{

return false  ;
}
 
}
  var content=new Array ();
function  checkParam(str)
{
content .length =0;
var  leftCodeList=new Array ();
var  rightCodeList=new Array ();
    for (var  i =0;i<str .length;i++)
    {
              var st=str.charAt(i);
               if (st=="{")
               {
                   leftCodeList .push (i);
               }
               else if (st=="}")
               {
               rightCodeList.push (i); 
               }
    }
    if (leftCodeList.length  !=rightCodeList .length )
    {
    return 1;
    }
    if (leftCodeList .length <2)
    {
    return 5;
    }
  
for (var a=0;a<leftCodeList .length ;a++)
{
    var rContent=   rightCodeList [a];
    var lContent=   leftCodeList  [a];
    if (rContent <lContent )
    {
    return 2;
    break ;
    }
    else 
    {
     var  number=str.slice(lContent+1,rContent);
     content .push (number );
     if (!checkIsNum(number ))
     {
     
     return 3;
     break ;
     }
 
     var tempRight;
     if (a==0)
     {
     tempRight =rContent ;
     }
     else
     {
     var middleContent=str.slice(tempRight+1 ,lContent );
     tempRight =rContent ;
     if (!checkMiddleContent (middleContent ))
     {
     return 4;
     }
 }
 
 
 
 
}


}




}

function checkMiddleContent(content)
{
if ((content .indexOf("+")<0) && (content .indexOf("-")<0) && (content .indexOf("*")<0) &&(content .indexOf("/")<0)    )
{
return false ;
}
else
{
return true ;
}
}

function checkIsNum (number)
{
if (number =="")
{
return false ;
}
else if (number =="@")
{
return true;
}


  table = document.getElementById("tblSalary");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var salaryCount = 0;
    var sign=false ;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，添加信息
	    if (table.rows[i].style.display != "none")
	    {
            //增长工资项数
            salaryCount++;
	        //工资编号
             if (document.getElementById("txtItemNo_" + i).value.Trim()==number )
             {
             sign= true ;
             break ;
             }
             else
             {
             continue ;
             }
             
	        //编辑模式
	        }
	 }       
	 
	return sign ;
	 
}

function CtoHSign(obj) {
    var str =document .getElementById (obj).value.Trim();
    var result = "";
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) == 12288) {
            result += String.fromCharCode(str.charCodeAt(i) - 12256);
            continue;
        }
        if (str.charCodeAt(i) > 65280 && str.charCodeAt(i) < 65375)
            result += String.fromCharCode(str.charCodeAt(i) - 65248);
        else result += String.fromCharCode(str.charCodeAt(i));
    }
    return result;

}

function checkSpecialParam(str)
{
var special=new Array ();
  for (var  i =0;i<str .length;i++)
    {
              var st=str.charAt(i);
               if (st=="@")
               {
                    if (i==0||i==str.length-1)
                    {
                    return true ;
                    }
                    else
                    {
                         var stLfet=str.charAt((i-1));
                         var stRight=str.charAt((i+1));
                         if ((stLfet !="{") || (stRight !="}") )
                         {
                         return true  ;
                         }
                    }
               }
              
    }
    return false ;

 
}
function DoCheck()
{    
 
var  row=document .getElementById ("hidRowNo").value.Trim();
var roeid="txtCalculate_"+row;
var rowParam="txtCalculateParam_"+row;

  document .getElementById ("inCaculator1").value=CtoHSign("inCaculator1") ;
 
   document .getElementById ("inCaculator1").value=   (document .getElementById ("inCaculator1").value).replace(/\s/g,"");
var getCacu=document .getElementById ("inCaculator1").value.Trim();

 
 
if (!CaculatorCheck(getCacu ))
{
 alert("公式有错误,请核对！");
return ;
}
if (checkSpecialParam(getCacu ))
{
 alert("特定参数配置错误,请核对！");
return ;
}

if ( !checkString (getCacu ))
{
alert ("公式没有参数,请核对");
return ;
}

var c=checkParam (getCacu );
if (c==1)
{
alert ("参数符号不配对,请核对！");
return ;
}
else if (c==2)
{
alert ("参数符号位置错误,请核对！");
return ;
}
else if (c==3)
{
alert ("参数符号配置的项目不存在,请核对！");
return ;
}
else if (c==4)
{
alert ("配置参数之间没有运算符号 ,请核对！");
return ;
}
else if (c==5)
{
alert ("最低配置两个参数 ,请核对！");
return ;
}

   var  postParams = "&Calculate="+getCacu.replace(/\+/g,   "A")+"&numberlist="+content  ;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryItem.ashx?Action=Check" ,
        data : postParams,//目标地址
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
              hidePopup();
                      if (data .sta==1)
               {
                           alert("公式有错误,请核对！");
                           return ;
            
               }
               else
               {
               document .getElementById (roeid).value=data .data ;
                      document .getElementById (rowParam).value=data .info  ;
               
               }
            
        
              
      
        } 
    }); 
    
           
 





DoBack();
}


 


function ClearElemInfo()
{
    document.getElementById("inCaculator1").value = "";
    document.getElementById("hidItemNo").value = "";
    document.getElementById("hidRowNo").value = "";
}
function DoBack()
{
    //清除页面输入
    ClearElemInfo();
    //隐藏修改页面
    document.getElementById("divEditCheckItem").style.display = "none"; 
  
}
//function AlertMsg(){

//	   /**第一步：创建DIV遮罩层。*/
//		var sWidth,sHeight;
//		sWidth = window.screen.availWidth;
//		//屏幕可用工作区高度： window.screen.availHeight;
//		//屏幕可用工作区宽度： window.screen.availWidth;
//		//网页正文全文宽：     document.body.scrollWidth;
//		//网页正文全文高：     document.body.scrollHeight;
//		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
//			sHeight = window.screen.availHeight;  
//		}else{//当高度大于一屏
//			sHeight = document.body.scrollHeight;   
//		}
//		//创建遮罩背景
//		var maskObj = document.createElement("div");
//		maskObj.setAttribute('id','BigDiv');
//		maskObj.style.position = "absolute";
//		maskObj.style.top = "0";
//		maskObj.style.left = "0";
//		maskObj.style.background = "#777";
//		maskObj.style.filter = "Alpha(opacity=30);";
//		maskObj.style.opacity = "0.3";
//		maskObj.style.width = sWidth + "px";
//		maskObj.style.height = sHeight + "px";
//		maskObj.style.zIndex = "200";
//		document.body.appendChild(maskObj);
//		
//	}
//	function CloseDiv(){
//		var Bigdiv = document.getElementById("BigDiv");
//		//var Mydiv = document.getElementById("div_Add");
//		if (Bigdiv)
//		{
//		document.body.removeChild(Bigdiv);
//		} 
////         Bigdiv.style.display = "none";
//		///Mydiv.style.display="none";
//	}

function isDetete(itemNo)
{
         var postParam = "Action=IsDelete&itemNo=" + escape(itemNo);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryItem.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('请求发生错误！');
            }, 
            success:function(data) 
            { 
////            alert (data .sta);
////           alert (data .data);
          var mess=data.data;
         var  messd =mess.split("|");
//          alert (messd );
           
            } 
        });
}