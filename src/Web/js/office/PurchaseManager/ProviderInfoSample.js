var GlbDetailID = new Array();


//---------Start 标准工序保存 ----------


//---------End   标准工序保存 ----------


//---------Start  拼音缩写 ----------

//---------End   拼音缩写 ----------


//清空输入框




//---------Start  工艺明细处理 ----------
function findObj(theObj, theDoc)
{ 
    var p, i, foundObj; 
    if(!theDoc) theDoc = document; 
    if( (p = theObj.indexOf("?")) > 0 && parent.frames.length) 
    {    
        theDoc = parent.frames[theObj.substring(p+1)].document;    
        theObj = theObj.substring(0,p); 
    }
             
    if(!(foundObj = theDoc[theObj]) && theDoc.all) foundObj = theDoc.all[theObj]; 
            for (i=0; !foundObj && i < theDoc.forms.length; i++)     
            foundObj = theDoc.forms[i][theObj]; for(i=0; !foundObj && theDoc.layers && i < theDoc.layers.length; i++)     
            foundObj = findObj(theObj,theDoc.layers[i].document); 
            if(!foundObj && document.getElementById) foundObj = document.getElementById(theObj);   
            return foundObj;
}       
//全选
function SelectAll()
{
    var signFrame = findObj("dg_Log",document);        
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    if(document.getElementById("checkall").checked)
    {
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i+1);
                document.getElementById(objRadio.toString()).checked = true;
            }
        }
    }
    else
    {
        for(var i=0;i<txtTRLastIndex-1;i++)
        {
            if(signFrame.rows[i+1].style.display!="none")
            {
                var objRadio = 'chk_Option_'+(i+1);
                document.getElementById(objRadio.toString()).checked = false;
            }
        }
    }
}

//删除行
function DeleteSignRow()
{
   var signFrame = findObj("dg_Log",document);        
   var ck = document.getElementsByName("chk");
    var txtTRLastIndex = document.getElementById('txtTRLastIndex').value;
    var signFrame = findObj("dg_Log",document); 
    for(var i=0;i<txtTRLastIndex-1;i++)
    {
        if(signFrame.rows[i+1].style.display!="none")
        {
            var objRadio = 'chk_Option_'+(i+1);
            if(document.getElementById(objRadio.toString()).checked)
            {
                signFrame.rows[i+1].style.display = 'none';
            }
        }
    }

}
//添加行
 function AddSignRow()
 { //读取最后一行的行号，存放在txtTRLastIndex文本框中 
        
        var txtTRLastIndex = findObj("txtTRLastIndex",document);
        var rowID = parseInt(txtTRLastIndex.value);
        var signFrame = findObj("dg_Log",document);
        var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
        newTR.id = "SignItem_Row_" + rowID;
        
        var newNameTD=newTR.insertCell(0);//添加列:序号
        newNameTD.className="cell";
        newNameTD.id = 'SignItem_TD_Index_'+rowID;
        newNameTD.innerHTML = newTR.rowIndex.toString();
        
        var newNameXH=newTR.insertCell(1);//添加列:选择
        newNameXH.className="cell";
        newNameXH.id = 'SignItem_TD_Check_'+rowID;
        newNameXH.innerHTML="<input name='chk' id='chk_Option_"+rowID+"' value=\"0\" type='checkbox' size='20' />";
        
//        var newFitNotd=newTR.insertCell(2);//添加列:查看
//        newFitNotd.className="cell";
//        newFitNotd.id = 'SignItem_TD_View_'+rowID;
//        newFitNotd.innerHTML = "<a href=\"#this\" onclick=\"alert(document.getElementById('Hidden_SignItem_TD_Sequ_Text_"+rowID+"').value);\">查看</a><input type=\"hidden\" id=\"Hidden_SignItem_TD_Sequ_Text_"+rowID+"\" value=\"\">";//添加列内容
        
        var newFitNametd=newTR.insertCell(2);//添加列:编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_Sequ_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProdNo_SignItem_TD_Sequ_Text_"+rowID+"\" value=\"\" onclick=\"popTechObj.ShowList('SignItem_TD_Sequ_Text_"+rowID+"');\" class=\"tdinput\" />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(3);//添加列:物品名称
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='ProductName_SignItem_TD_Sequ_Text_"+rowID+"' value=\"\" type='text' size='20'class=\"tdinput\" />";//添加列内容
        
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
 }

 function FillSignRow(i,ProviderID,ProviderNo,ProviderName)
 {
    GlbDetailID.push(item.ID);//填充已有的明细ID数组
    
      var rowID = parseInt(i)+1;
    var signFrame = findObj("dg_Log",document);
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "SignItem_Row_" + rowID;
    
    var newNameTD=newTR.insertCell(0);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id = 'SignItem_TD_Index_'+rowID;
    newNameTD.innerHTML = newTR.rowIndex.toString();
    
    var newNameXH=newTR.insertCell(1);//添加列:选择
    newNameXH.className="cell";
    newNameXH.id = 'SignItem_TD_Check_'+rowID;
    newNameXH.innerHTML="<input name='chk' id='chk_Option_"+rowID+"' value="+ProviderNo+" type='checkbox' size='20' />";
    
    var newFitNametd=newTR.insertCell(2);//添加列:编号
    newFitNametd.className="cell";
    newFitNametd.id = 'SignItem_TD_Sequ_'+rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProdNo_SignItem_TD_Sequ_Text_"+rowID+"\" value=\""+ProviderName+"\" onclick=\"popTechObj.ShowList('SignItem_TD_Sequ_Text_"+rowID+"');\" class=\"tdinput\" />";//添加列
    
    
   
}
 
 function Fun_FillParent_Content(id,providerno,providername)
{
    var hiddenObj = 'ProdNo_' + popTechObj.InputObj;
    var ProductName='ProductName_'+popTechObj.InputObj;
    
    document.getElementById(hiddenObj).value = providerno;
    document.getElementById(ProductName).value = providername;
    document.getElementById('divStorageProduct').style.display='none';
    
}

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