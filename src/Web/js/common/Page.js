var EquipFit_Item = new Array();
var arrlength=0;
function ShowTotalPage(totalcount,pagecount,pageindex,obj)
{
    var tempresult=Math.floor(totalcount/pagecount);
    var tempresult1=totalcount/pagecount;
    if(typeof(obj)!="undefined")
    {
        if(tempresult==tempresult1)
           obj.html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+tempresult+"</font>&nbsp;页");
        else
        {
           var result=tempresult+1;
           obj.html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+result+"</font>&nbsp;页");
        }
    }
    else
    {
        if(tempresult==tempresult1)
           $("#PageCount").html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+tempresult+"</font>&nbsp;页");
        else
        {
           var result=tempresult+1;
           $("#PageCount").html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+result+"</font>&nbsp;页");
        }
    }
}
//获取明细列表
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
//checkall:标题点选按钮ID
//chk:列表点选按钮ID
function AllSelect(checkall,chk)
{
   //var signFrame = findObj(dg_Log,document);   
   var ck = document.getElementsByName(checkall);
   var ck1 = document.getElementsByName(chk);
   
   if(ck[0].checked)
   {
        for(var j=0;j<ck1.length;j++)
        {  
           if(!(ck1[j].disabled||ck1[j].readonly))
           {
               ck1[j].checked=true;          
           }          
        }
   }
   else
   {
     for(var j=0;j<ck1.length;j++)
     {  
       ck1[j].checked=false;
     }
   }  
}

/*
* 设置全选CheckBox
* obj         点击的控件
* chkName     全选控件Name
* chkAllName  全选控件Name
*/
function SetCheckAll(obj, chkDetailName, chkAllName)
{
    var chkAll;
    //获取全选择控件
    var checkAlls = document.getElementsByName(chkAllName);
    //如果全选按钮不存在则返回
    if (checkAlls == null || typeof(checkAlls) == "undefined") return;
    else chkAll = checkAlls[0];
    //如果点击的checkbox未选中
    if (!obj.checked)
    {
        chkAll.checked = false;
        return;
    }
    else
    {
        var isSelectAll = true;         
        var chkDetails = document.getElementsByName(chkDetailName);
	    //遍历表格中的数据，判断是否选中
	    for (var i = 0; i < chkDetails.length; i++)
	    { 
            //获取选择框控件
            chkControl = chkDetails[i];
            //如果未选中，则返回
            if (!chkControl.checked)
            {
                isSelectAll = false;
                break;
            }
	    }
	    //列表中全部选中时，选择checkbox选中
	    if (isSelectAll)
	    {
	        chkAll.checked = true;
	    }
	    //列表中有一个未选中时，选择checkbox未选中
	    else
	    {
	        chkAll.checked = false;
	    }
    }
}


//点击列表chk判断是否选中标题中的chk
//checkall:标题点选按钮ID
//chk:列表点选按钮ID
function IfSelectAll(chk,chkall)
{
        var ck = document.getElementsByName(chk);
        var selectallflag = "0";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( !ck[i].checked )
            {
                selectallflag="1"
            }
        }
        if(selectallflag=="0")
            document.getElementById(chkall).checked=true;
        else
            document.getElementById(chkall).checked=false;
}
function selectall(check)
{
//   var signFrame = findObj("dg_Log",document);
   var ck = document.getElementsByName("checkall");
   var ck1 = document.getElementsByName("chk");
   if(ck[0].checked)
   {
        for(var j=0;j<ck1.length;j++)
        {  
           if(!(ck1[j].disabled||ck1[j].readonly))
           {
           ck1[j].checked=true;
           }
          
        }
   }
   else
   {
     for(var j=0;j<ck1.length;j++)
        {  
           ck1[j].checked=false;
        }
   }
   
}
function selectall2(check)
{
   var signFrame = findObj("dg_Log",document);
   var ck = document.getElementsByName("checkall1");
   var ck1 = document.getElementsByName("chk2");
   if(ck[0].checked)
   {
        for(var j=0;j<ck1.length;j++)
        {  
           if(!(ck1[j].disabled||ck1[j].readonly))
           {
           ck1[j].checked=true;
           }
          
        }
   }
   else
   {
     for(var j=0;j<ck1.length;j++)
        {  
           ck1[j].checked=false;
        }
   }
   
}
//删除指定行
function DeleteSignRow(rowid){
        var signFrame = findObj("dg_Log",document);
        var signItem = findObj(rowid,document);
        var rowIndex = signItem.rowIndex;//获取将要删除的行的Index
        signFrame.deleteRow(rowIndex);//删除指定Index的行
        for(i=rowIndex;i<signFrame.rows.length;i++)
        {//重新排列序号，如果没有序号，这一步省略
            signFrame.rows[i].cells[0].innerHTML = i.toString();
//            var txtTRLastIndex = findObj("txtTRLastIndex",document);
//            txtTRLastIndex.value = (signFrame.rows.length).toString() ;//将行号推进上一行
        }
        }
//获取扩展名
function getExt(filename) 
{ 
   var dot_pos = filename.lastIndexOf(".");
   if(dot_pos == -1)
   return ""; 
   return filename.substr(dot_pos+1).toLowerCase(); 
}
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		//t[i].onclick=function(){//鼠标点击
			//if(this.x!="1"){
				//this.x="1";//
				//this.style.backgroundColor=d;
			//}else{
			//	this.x="0";
				//this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
			//}
		//}
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}
function delrow()
{
        var signFrame = findObj("dg_Log",document);        
        var ck = document.getElementsByName("chk");
        for( var i = 0; i<ck.length;i++ )
        {
            if ( ck[i].checked )
            {
               signFrame.rows[i+1].style.display="none";
               EquipFit_Item.splice(i+1,1);
            }
        }
}
function delrow2()
{
        var signFrame = findObj("dg_Log2",document);        
        var ck2 = document.getElementsByName("chk2");
        for( var i = 0; i<ck2.length;i++ )
        {
            if ( ck2[i].checked )
            {
               signFrame.rows[i+1].style.display="none";
               EquipFit_Item.splice(i+1,1);
            }
        }
}