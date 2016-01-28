function ShowProdInfo()
{
 popTechObj.ShowListCheckSpecial('SignItem_TD_Sequ_Text_"+rowID+"','Check');
  //alert(document.getElementById("HdProductInfoID").value);
}
function GetValue()
{
   var ck = document.getElementsByName("CheckboxProdID");
        var strarr=''; 
        var str = "";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               strarr += ck[i].value+',';
            }
        }
         str = strarr.substring(0,strarr.length-1);
          $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str='+str,//目标地址
           cache:false,
               data:'',//数据
               beforeSend:function(){},//发送数据之前
         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item)
                    {
                        AddSignCheckRow(item.ProdNo,item.ProductName,item.StandardCost,item.CodeName);
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
}  
         
//添加行var rowID = parseInt(i)+1;
      
 function AddSignCheckRow(ProdNo,ProductName,StandardCost,CodeName)
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
        
        var newFitNametd=newTR.insertCell(2);//添加列:编号
        newFitNametd.className="cell";
        newFitNametd.id = 'SignItem_TD_Sequ_'+rowID;
        newFitNametd.innerHTML = "<input type=\"text\" id=\"ProdNo_SignItem_TD_Sequ_Text_"+rowID+"\" value='"+ProdNo+"' onclick=popTechObj.ShowList(); class=\"tdinput\" />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(3);//添加列:物品名称
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='ProductName_SignItem_TD_Sequ_Text_"+rowID+"' value='"+ProductName+"' type='text' size='20'class=\"tdinput\" />";//添加列内容
        
        var newFitDesctd=newTR.insertCell(4);//添加列:标准成本
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='StandardCost_SignItem_TD_Sequ_Text_"+rowID+"' value='"+StandardCost+"' type='text' size='20'class=\"tdinput\" />";//添加列内容
         
           var newFitDesctd=newTR.insertCell(5);//添加列:单位
        newFitDesctd.className="cell";
        newFitDesctd.id = 'SignItem_TD_Remark_'+rowID;
        newFitDesctd.innerHTML = "<input name='chk' id='Unit_SignItem_TD_Sequ_Text_"+rowID+"' value='"+CodeName+"' type='text' size='20'class=\"tdinput\" />";//添加列内容
        
        txtTRLastIndex.value = (rowID + 1).toString() ;//将行号推进下一行
      
 }