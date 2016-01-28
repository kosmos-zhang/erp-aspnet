/* 页面初期显示 */
$(document).ready(function(){
      ShowDeptTree("");
});

/*
* 获取组织机构树
*/
function ShowDeptTree(deptID)
{
    //判断组织机构ID输入时，判断样式
    if (deptID != "")
    {
        //获取样式
        divDeal = document.getElementById("divSuper_" + deptID);
        //获取样式表单
        css = divDeal.className;
        //样式表单为打开状态时，不进行查询数据，隐藏菜单
        if ("folder_open" == css)
        {
            //设置表单样式为关闭状态
            divDeal.className = "folder_close";
            //隐藏子组织机构
            document.getElementById("divSub_" + deptID).style.display = "none";
            //返回不执行查询
            return;
        }
        //表单样式为关闭状态时
        else
        {
            //设置表单样式为打开状态
            divDeal.className = "folder_open";
        }
    }
    //执行查询数据
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptInfo_Query.ashx?Action=InitTree&DeptID=" + deptID,
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            //隐藏提示框  
         //   hidePopup();
            //显示组织机构树
            //第一次初期化时
            if (deptID == null || "" == deptID)
            {
                //设置子组织机构信息
                document.getElementById("divDeptTree").innerHTML = data;
            }
            //点击节点时
            else
            {
                //设置子组织机构信息
                document.getElementById("divSub_" + deptID).innerHTML = data;
                //将自组织机构div设置成可见
                document.getElementById("divSub_" + deptID).style.display = "block";
            }
        } 
    });  
}
/**/
var temCount="0";
function SetSelectValue(deptID, deptName, superDeptID)
{
 if (temCount =="0")
 {
 temCount ="1";
 }
 else
 {
 temCount ="0";
 }
// alert (temCount );
    //上级机构ID
    document.getElementById("hidSelectValue").value = deptID + "|" + deptName + "|" + superDeptID;
    //显示选中项
    if (deptName == "") deptName = "全部组织机构";
    document.getElementById("divSelectName").innerHTML = deptName;
}

/*
* 基本信息校验
*/
function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
            var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isErrorFlag = true;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    if ("UPDATE"==editFlag )
    {
        var superID= document.getElementById("txtSuperDeptID").value.Trim();
    var getMessage=document.getElementById("hidSelectValue").value.Trim();
                   if (getMessage !='')
                   {
                   var sd=getMessage .split("|");
                   if (superID ==sd[0])
                   {
                       isErrorFlag = true;
                fieldText += "上级机构|";
   		        msgText += "设置上级机构不允许设置为修改的机构|"; 
                   }
                   }
    }
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codeRule_txtCode").value.Trim();
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "机构编号|";
   		        msgText += "请输入机构编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "机构编号|";
                    msgText += "机构编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
        
    //机构名称必须输入
    if (document.getElementById("txtDeptName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "机构名称|";
        msgText += "请输入机构名称|";  
    }
    //拼音代码
    if (document.getElementById("txtPYShort").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "拼音代码|";
        msgText += "请输入拼音代码|";  
    }
    else
    {
           if (!CodeCheck(document.getElementById("txtPYShort").value.Trim()))
                {
                    isErrorFlag = true;
                    fieldText += "拼音代码|";
                    msgText += "拼音代码只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
    }
    
    //邮编
    post = document.getElementById("txtPost").value.Trim();
    if (post != "" && !IsNumber(post))
    {
        isErrorFlag = true;
        fieldText += "邮编|";
        msgText += "请输入正确的邮编|";  
    }
    //Email
    mail = document.getElementById("txtEmail").value.Trim();
    if (mail != "" && !IsEmail(mail))
    {
        isErrorFlag = true;
        fieldText += "Email|";
        msgText += "请输入正确的Email|";
    }


    tel = document.getElementById("txtTel").value.Trim();
    if (tel != "" && !phonecheck(tel)) {
        isErrorFlag = true;
        fieldText += "电话号码|";
        msgText += "请输入正确的电话号码|";
    }


    if (strlen(document.getElementById("txtDescription").value) > 200) {
        isErrorFlag = true;
        fieldText += "描述信息|";
        msgText += "描述信息不允许大于200字符|";
    }


    if (strlen(document.getElementById("txtDuty").value) > 100) {
        isErrorFlag = true;
        fieldText += "机构职责|";
        msgText += "机构职责不允许大于100字符|";
    }
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }

    return isErrorFlag;
}



function phonecheck(s) {
    var str = s;
    var reg = /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/;
    //alert(reg.test(str));
    if (reg.test(str) == false) {
        //alert("请检查您输入的电话号码。");
        return false;
    }
    else {

        return true; 
    }
}


/*
* 保存信息
*/
function DoSaveInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    }
    
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptInfo_Edit.ashx?" + postParams,
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
            //隐藏提示框
           // hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
           
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置考核编号
                     var hf=data.data .split("|");
                document.getElementById("txtDisplayCode").value = hf[3];
                document.getElementById("txtDisplayCode").disabled = true;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
               //  document.getElementById("hidSelectValue").value = deptID + "|" + deptName + "|" + superDeptID;
               
                document.getElementById("hidSelectValue").value =hf [0]+"|"+hf[1]+"|"+hf[2];
            
    //显示选中项
  
    if (hf[1] == "")
    {
  document.getElementById("divSelectName").innerHTML = "全部组织机构";
  }
  else
  {
    document.getElementById("divSelectName").innerHTML = hf[1];
    }
             
                 if (document .getElementById ("txtSuperHistroyID").value.Trim()==document .getElementById ("txtSuperDeptID").value.Trim() )
                 {
                   var getMessage=document.getElementById("hidSelectValue").value.Trim();
                   if (getMessage =='')
                   {
                     ShowDeptTree("");
                   }
               var sd=getMessage .split("|");
               
      
          
          
             if (sd[0]!="null" || sd[0]!='')
             {
             if (temCount =="0")//添加同级
             {
                 ShowDeptTree(sd[2]);
                      ShowDeptTree(sd[2]);
             }
             else if (temCount =="1")//添加下级
             {
               ShowDeptTree(sd[2]);
                      ShowDeptTree(sd[2]);
             }
              else if (temCount =="2")//修改
             {
               ShowDeptTree(sd[2]);
                      ShowDeptTree(sd[2]);
             }
            else if (temCount =="3")//新建
            {
            ShowDeptTree("");
            } 
            
             
       
             }
             else
             {
              ShowDeptTree("");
             }
             }
             else
             {
                    ShowDeptTree("");
             }
                  
                //设置提示信息
              
            }
            //编号已存在
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
            //保存失败
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            }
        } 
    });  
}

/*
* 基本信息参数
*/
function GetBaseInfoParams()
{
 
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value.Trim();
    //新建时，编号选择手工输入时
    var param = "EditFlag=" + editFlag;
    //更新的时候
    if ("INSERT" != editFlag)
    {
        //人员编号
        param += "&DeptNo=" + document.getElementById("txtDisplayCode").value.Trim();
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            param += "&DeptNo=" + document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    
    //上级机构
 
    param += "&SuperDeptID=" + document.getElementById("txtSuperDeptID").value.Trim();   

        
               
    //独立核算
    param += "&AccountFlag=" + document.getElementById("ddlAccountFlag").value.Trim();
    //机构名称
    param += "&DeptName=" + escape(document.getElementById("txtDeptName").value.Trim());
    //拼音代码
    param += "&PYShort=" + document.getElementById("txtPYShort").value.Trim();
    //机构职责
    param += "&Duty=" + escape(document.getElementById("txtDuty").value.Trim());
    //是否分公司
    param += "&SubFlag=" + document.getElementById("ddlSubFlag").value.Trim();
    //是否零售
    param += "&SaleFlag=" + document.getElementById("ddlSaleFlag").value.Trim();
    //描述信息
    param += "&Description=" + escape(document.getElementById("txtDescription").value.Trim());
    //地址
    param += "&Address=" + escape(document.getElementById("txtAddress").value.Trim());
    //邮编
    param += "&Post=" + escape(document.getElementById("txtPost").value.Trim());
    //联系人
    param += "&LinkMan=" + escape(document.getElementById("txtLinkMan").value.Trim());
    //电话
    param += "&Tel=" + escape(document.getElementById("txtTel").value.Trim());
    //传真
    param += "&Fax=" + escape(document.getElementById("txtFax").value.Trim());
    //Email
    param += "&Email=" + escape(document.getElementById("txtEmail").value.Trim());
    //启用状态
    param += "&UsedStatus=" + document.getElementById("ddlUsedStatus").value.Trim();
    
    return param;
}

/*
* 获取拼音缩写
*/
function GetPYShort() {

    if (!CheckSpecialWord(document.getElementById("txtDeptName").value.Trim())) {
        return;
    }
    
    
    //获取机构名称
    deptName = escape(document.getElementById("txtDeptName").value.Trim());
    //未输入时，返回不获取拼音缩写
    if (deptName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + deptName;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?" + postParams,
            dataType:'json',//返回json格式数据
            cache:false,
            error: function()
            {
            }, 
            success:function(data) 
            {
                //获取成功时，设置拼音缩写
                if(data.sta == 1)
                {
                    //设置拼音缩写
                    document.getElementById("txtPYShort").value = data.info;
                }
            } 
        });
    }
}

/*
* 返回处理
*/
function DoBack()
{
    //隐藏页面
    CloseReclickDeptDiv();
    document.getElementById("divEditDept").style.display = "none";
    

    //设置页面显示数据
//    ShowDeptTree("");
}

/*
* 获取组织信息
*/
function SetBaseDept()
{
    //设置选中的值
    SetSelectValue("","","");
}

/*
* 
*/
function DoEditDept(flag)
{

if (flag =="3")
{
   document.getElementById("hidSelectValue").value = '';
  
    document.getElementById("divSelectName").innerHTML = '';
}
temCount =flag ;
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value;
    //alert (selectValue);

    //没有选择机构时

    //解析组织机构信息
    deptInfo = selectValue.split("|");
    var editDeptID = "";
    var editDeptName = "";
    //添加同级
    if ("0" == flag)
    {
        if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择组织机构！");
        return;
    }
        //上级机构ID
        superDeptID = deptInfo[2];
        editDeptID = superDeptID;
        document .getElementById ("ddlUsedStatus").value="1";
        //上级机构名称
        if (editDeptID != "")
            editDeptName = document.getElementById("divDeptName_" + superDeptID).innerHTML;
    }
    //添加下级
    else if ("1" == flag)
    {
        if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择组织机构！");
        return;
    }
        //上级机构ID
        editDeptID = deptInfo[0];
        //上级机构名称
        editDeptName = deptInfo[1];
    }
    //修改
    if ("2" == flag)
    {
    
           if (selectValue == "||")
    {
   
      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","此项为默认项，不可编辑！");
     return;
    }
        if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择组织机构！");
        return;
    }
        //编辑模式
        document.getElementById("hidEditFlag").value = "UPDATE";
        /* 设置编号的显示 */ 
        //显示考核的编号 考核编号DIV可见              
        document.getElementById("divCodeNo").style.display = "block";
        //编码规则DIV不可见
        document.getElementById("divCodeRule").style.display = "none";
        //设置组织机构信息
        InitDeptInfo(deptInfo[0]);
    }
    //添加时
    else 
    {
    
    
        //清除添加页面的信息
        ClearEditInfo();
      
    
    
        //上级机构ID
        document.getElementById("txtSuperDeptID").value = editDeptID;
        document .getElementById ("txtSuperHistroyID").value=editDeptID ;
        //上级机构名称
        document.getElementById("DeptTxtSuperName").value = editDeptName;
        //编辑模式
        document.getElementById("hidEditFlag").value = "INSERT";
        /* 设置编号的显示 */ 
        //显示考核的编号 考核编号DIV可见              
        document.getElementById("divCodeNo").style.display = "none";
        //编码规则DIV不可见
        document.getElementById("divCodeRule").style.display = "block";
    }
    ShowReclickDeptDiv();
    //显示修改页面
    document.getElementById("divEditDept").style.display = "block";
}

/*
* 清除页面输入内容
*/
function ClearEditInfo()
{
    //独立核算
    document.getElementById("ddlAccountFlag").value = "";
    //机构编号
    document.getElementById("codeRule_txtCode").value = "";
    //机构名称
    document.getElementById("txtDeptName").value = "";
    //拼音代码
    document.getElementById("txtPYShort").value = "";
    //机构职责
    document.getElementById("txtDuty").value = "";
    //是否分公司
    document.getElementById("ddlSubFlag").value = "";
    //是否零售
    document.getElementById("ddlSaleFlag").value = "";
    //描述信息
    document.getElementById("txtDescription").value = "";
    //地址
    document.getElementById("txtAddress").value = "";
    //邮编
    document.getElementById("txtPost").value = "";
    //联系人
    document.getElementById("txtLinkMan").value = "";
    //电话
    document.getElementById("txtTel").value = "";
    //传真
    document.getElementById("txtFax").value = "";
    //Email
    document.getElementById("txtEmail").value = "";
    //启用状态
    document.getElementById("ddlUsedStatus").value = "1";
}

/*
* 设置组织机构信息
*/
function InitDeptInfo(deptID)
{
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptInfo_Query.ashx?Action=GetDeptInfo&DeptID=" + deptID,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置组织机构信息 */
            $.each(msg.data, function(i,item){
                //机构编号
                document.getElementById("txtDisplayCode").value = item.DeptNO;
                document.getElementById("txtDisplayCode").disabled = true;
                //上级机构
                document.getElementById("DeptTxtSuperName").value = item.SuperDeptName;
                document.getElementById("txtSuperDeptID").value = item.SuperDeptID;
                //独立核算
                document.getElementById("ddlAccountFlag").value = item.AccountFlag;
                //机构名称
                document.getElementById("txtDeptName").value = item.DeptName;
                //拼音代码
                document.getElementById("txtPYShort").value = item.PYShort;
                //机构职责
                document.getElementById("txtDuty").value = item.Duty;
                //是否分公司
                document.getElementById("ddlSubFlag").value = item.SubFlag;
                //是否零售
                document.getElementById("ddlSaleFlag").value = item.SaleFlag;
                //描述信息
                document.getElementById("txtDescription").value = item.Description;
                //地址
                document.getElementById("txtAddress").value = item.Address;
                //邮编
                document.getElementById("txtPost").value = item.Post;
                //联系人
                document.getElementById("txtLinkMan").value = item.LinkMan;
                //电话
                document.getElementById("txtTel").value = item.Tel;
                //传真
                document.getElementById("txtFax").value = item.Fax;
                //Email
                document.getElementById("txtEmail").value = item.Email;
                //启用状态
                document.getElementById("ddlUsedStatus").value = item.UsedStatus;
            });
        }
    }); 
}

/*
* 删除组织机构
*/
function DoDelete()
{
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value;
    //没有选择机构时
    if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择组织机构！");
        return;
    }
    if(!confirm("删除后将不可恢复，确认删除吗！"))
    {
        return;
    }
    //解析组织机构信息
    deptInfo = selectValue.split("|");
    //执行删除
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptInfo_Query.ashx?Action=DeleteDeptInfo&DeptID=" + deptInfo[0],
        dataType:'json',
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            //存在子组织机构时
            if(msg.sta == 2) 
            {
                //显示提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该机构存在子机构,请删除子机构后再删除！");
            }
            //删除成功
            else if(msg.sta == 1) 
            {
        
                //设置页面显示数据
                ShowDeptTree(deptInfo[2]);
                  ShowDeptTree(deptInfo[2]);
                //显示提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
            }
                  //删除成功
            else if(msg.sta == 3) 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该部门已被引用,无法删除！");
            }
                 //删除成功
            else if(msg.sta == 4) 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif"," 删除失败，该部门中已设置岗位，请先删除对应的岗位后再尝试！");
            }
               else if(msg.sta == 5) 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif"," 删除失败，该部门中已设置员工，请先删除对应的员工后再尝试！！");
            }
            //删除失败
            else
            {
                //显示提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
            } 
        }
    }); 
}

function ShowReclickDeptDiv(){

   /**第一步：创建DIV遮罩层。*/
	var sWidth,sHeight;
	sWidth = window.screen.availWidth-300;
	if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
		sHeight = window.screen.availHeight-200;  
	}else{//当高度大于一屏
		sHeight = document.body.scrollHeight-200;   
	}
	
	 obj = document.getElementById("divPreventReclickDept");
    //隐藏信息提示DIV
    if ( obj != null && typeof(obj) != "undefined") obj.style.display = "block";
    else
    {
        // 整个div的大小和位子
        var preventReclick = "<div id='divPreventReclickDept' style='position:absolute;z-index:1; background-color:#777;"
                        + "left:0;top:0;filter:Alpha(opacity=0);width:" + sWidth + ";height:" + sHeight + ";' >";
        preventReclick += "<iframe style='position: absolute; z-index:-1; width:"+sWidth+"; height:"+sHeight+";' frameborder='0'>  </iframe>";
        preventReclick += "</div>";

        insertHtml("afterBegin", document.body, preventReclick);
    }
}

function CloseReclickDeptDiv(){
	 obj = document.getElementById("divPreventReclickDept");
    //隐藏信息提示DIV
    if ( obj != null && typeof(obj) != "undefined") obj.style.display = "none";
}