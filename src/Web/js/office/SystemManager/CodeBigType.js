
var flagtype=document.getElementById('hf_tablename').value;
$(document).ready(function(){
    LoadTree();   
    var typeFlag=document.getElementById("hf_typeflag").value;
    if(typeFlag=="5")
    {
       for(var i=0;i< document.getElementById("selBigType").options.length;i++)
        {
           if(document.getElementById("selBigType").options[i].value == "1")
           {
            document.getElementById("selBigType").options.remove(i);
            break;
           }

        }
    }
    else if(typeFlag=="4")
    {
    var length = 6;    
    for(var i = length; i >= 0; i--)
    {    
            document.getElementById("selBigType").options[i] = null;    
    }
       document.getElementById("selBigType").options.add(new Option("客户","1"));
       document.getElementById("curAction").innerHTML="客户细分设置";
      
    }
}); 
var enableNew = true;
var enableEdit = true;
var enableDel = true;
var codetypename="";
var id="";
function LoadTree()
{
    var flag=flagtype;
        var typeFlag=document.getElementById("hf_typeflag").value;
    //拼写请求URL参数
    var postParams = "Action=LoadData"+"&TableName="+flag+"&typeFlag="+typeFlag;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/SystemManager/CideBigType.ashx?"+postParams,
        dataType:'string',
        data: '',
        cache:false,
        success:function(data) 
        {        
            var result = null;
            eval("result = "+data);
     
            if(result.result)                    
            {
                BuildTree(result.data);
                document.getElementById("divSelectName").value="";
            }
            else
            {
//                MsgBox(result.data);
            }    
        },
        error:function(data)
        {
        alert('error');
//             MsgBox(data.responseText);
        }
    });
}



var treenodes = [];
var curLevel = 0;
var expandLevel = 1;
function getSep()
{
    var ret = "|";
    for (var i = 0; i < curLevel; i++)
    {
        ret += "|-";
    }
    return ret;
}

//var treenodes = [];
//var curLevel = 0;

//function getSep()
//{
//    var ret = "|";
//    for (var i = 0; i < curLevel; i++)
//    {
//        ret += "|-";
//    }
//    return ret;
//}

function BuildTree(nodes)
{
    treeview_selnode = null;
    treeview_selnodeindex = -1;

    var container = document.getElementById("treeContainer");
    
    container.innerHTML = "";    
    document.getElementById("slSupperTypeID").options.length =0;
    document.getElementById("slSupperTypeID").options.add(new Option("顶级分类","0"));
    treeview_cancel();
    var tb = document.createElement("TABLE");
    tb.cellSpacing="0";
    tb.cellPadding="0";
    container.appendChild(tb);
    for(var i=0;i<nodes.length;i++)
    {       
        BuildSubNodes(nodes[i],tb);
    }
    
}

function BuildSubNodes(node,tb)
{
    treenodes.push(node);
    var tr = tb.insertRow(-1);
     var expx = "";
     
        if(node.SubNodes.length>0)
    {
        if((curLevel+1) > expandLevel)
        {
            tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"../../../images/treeimg/WebResource5.gif\">";;
        }else{
           tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"../../../images/treeimg/WebResource6.gif\">";
        }
       expx = "onclick=\"treeview_expand(this)\" ";
       
    }else{
       tr.insertCell(-1).innerHTML = "";
    }    
    tr.insertCell(-1).innerHTML = "<a "+expx+" onmouseover=\"treeview_onmove("+(treenodes.length-1)+",1)\" onmouseout=\"treeview_onmove("+(treenodes.length-1)+",0)\" id=\"treeview_node"+(treenodes.length-1)+"\" style=\"color:black;\" href=\"javascript:treeview_onselnode("+(treenodes.length-1)+");\">"+node.CodeName+"</a>";
    if(typeof(node.ID)!="undefined")
    {
        document.getElementById("slSupperTypeID").options.add(new Option(getSep()+node.CodeName,node.ID));
    }
    curLevel++;
   if(node.SubNodes.length > 0)
    {
        var subTb = document.createElement("TABLE");
        //subTb.border="1";
        subTb.cellSpacing="0";
        subTb.cellPadding="0";
        
        if(curLevel > expandLevel)
        {
            subTb.style.display="none";
        }else{
            subTb.style.display="";
        }
        
        var ttr = tb.insertRow(-1);
        ttr.insertCell(-1);
        ttr.insertCell(-1).appendChild(subTb);
    
        for(var i=0;i<node.SubNodes.length;i++)
        {            
            BuildSubNodes(node.SubNodes[i],subTb);
        }
        
    }
    curLevel--;
}


//----------------------
function treeview_expand(obj)
{
    
      var tr = obj.parentNode.parentNode;
    var ntr = tr.nextSibling.cells[1].firstChild;
    if(obj.tagName != "IMG")
        obj = obj.parentNode.previousSibling.firstChild;
    if(ntr.style.display == "none")
    {
        ntr.style.display = "";
         obj.src = "../../../images/treeimg/WebResource6.gif";
    }else{
        ntr.style.display = "none";
       obj.src = "../../../images/treeimg/WebResource5.gif";
    }
}

var treeview_selnode = null;
var treeview_selnodeindex = -1;
function treeview_onselnode(idx)
{   
    var obj = document.getElementById("treeview_node"+idx);
    if(treeview_selnode != null)
    {
        treeview_selnode.style.color = "#000000";
    }
    
    treeview_edit(idx);
    
    obj.style.color = "#ff0000";
    treeview_selnode = obj;
    treeview_selnodeindex = idx;
           
}
function treeview_onmove(idx,flag)
{
    if(treeview_selnodeindex == idx)
        return;
        
    var obj = document.getElementById("treeview_node"+idx);
    if(flag == 1)
    {
        obj.style.color = "#ff0000";
    }else{
        obj.style.color = "#000000";
    }
}

function treeview_edit(idx)
{  
try{
    if(!enableEdit)
        return;
//    document.getElementById("btnSave").style.display = "";
    if(!(idx>=0))
    {
        if(treeview_selnodeindex == -1)
        {
          popMsgObj.ShowMsg('请点击选择一个要修改的分类！')
            return;
        }
        idx = treeview_selnodeindex;
    }
//    document.getElementById("curAction").innerHTML = "修改分类";
    id=treenodes[idx].ID;
   codetypename=treenodes[idx].CodeName;
    document.getElementById("txtID").value = treenodes[idx].ID;
    document.getElementById("CodeName").value = treenodes[idx].CodeName;  
   document.getElementById("UsedStatus").value = treenodes[idx].UsedStatus;  
   document.getElementById("txt_Description").value = treenodes[idx].Description;  
   document.getElementById("selBigType").value=treenodes[idx].TypeFlag;
   document.getElementById("slSupperTypeID").value = treenodes[idx].SupperID;
   document.getElementById("slSupperTypeID").disabled = true;
   document.getElementById("slSupperTypeID").style.display = "block";
   document.getElementById("divSelectName").style.display = "none";
   document.getElementById("hf_flagid").value="1";
    if(typeof(treenodes[idx].Description)=="undefined")
    {
    document.getElementById("txt_Description").value="";
    }
    }
    catch( e){}
}
function treeview_cancel()
{
    document.getElementById("divSelectName").style.display='block'; 
    document.getElementById("txtID").value = "";
    document.getElementById("CodeName").value = "";    
    document.getElementById("hf_flagid").value="";
    document.getElementById("selBigType").disabled = true;
    document.getElementById("UsedStatus").value = "1";    
    document.getElementById("txt_Description").value = "";   
    if(typeof(id)=="undefined")
    {
     document.getElementById("divSelectName").value = codetypename;
    }
    else
     {
       document.getElementById("divSelectName").value="";
     }
//         
//    document.getElementById("curAction").innerHTML = "新建分类";
    document.getElementById("slSupperTypeID").style.display='none';
    if(!enableNew)
    {
        document.getElementById("btnSave").style.display = "none";
    }
}

function treeview_save()
{  
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var tflag=flagtype;
    var tTypeName = trim(document.getElementById("CodeName").value);    
    var tSupperTypeID = trim(document.getElementById("slSupperTypeID").value);
    if(tSupperTypeID=="")
    {
     tSupperTypeID=0;
    }
    
    var Description=document.getElementById("txt_Description").value;
    var UsedStatus=document.getElementById("UsedStatus").value;
    var TypeFlag=document.getElementById("selBigType").value;
    
    
    
      var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(tTypeName + "" == "")
    {
 isFlag = false;
        fieldText = fieldText + "类别名称|";
   		msgText = msgText +  "请填写类别名称|";
    }
    
    if(strlen(tTypeName) > 100)
    {
isFlag = false;
        fieldText = fieldText + "类别名称|";
   		msgText = msgText +  "类别名称仅限于100个字符以内|"
    }
     if(strlen(Description)>200)
    {
isFlag = false;
        fieldText = fieldText + "描述信息|";
   		msgText = msgText +  "描述信息仅限于200个字符以内|"
    }
      if(document.getElementById("hf_flagid").value=="")
    {
     if(document.getElementById('divSelectName').value=="")
    {
isFlag = false;
        fieldText = fieldText + "上级分类|";
   		msgText = msgText +  "请选择上级分类|"
    }
    }
      if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }

    var params = "TypeName=" + escape(tTypeName) +
                 "&Flag=0"+
                 "&SupperTypeID=" + tSupperTypeID +
                 "&TableName=" + tflag +
                 "&Description=" + Description +
                 "&UsedStatus=" + UsedStatus +
                 "&TypeFlag="+TypeFlag+"";   
    if(document.getElementById("txtID").value + "" == "")
    {       
        if(!enableNew)
        { 
        popMsgObj.ShowMsg('没有新建分类的权限！')
            return;
        }
        SubmitAction("AddItem",params);
    }else{
        if(!enableEdit)
        { 
        popMsgObj.ShowMsg('没有修改分类的权限！')
            return;
        }
        var tID = document.getElementById("txtID").value;
        if(tID + "" == "")
        { 
        popMsgObj.ShowMsg('请选择要修改的分类！')
            return;
        }
        
        params += "&ID="+tID;
        SubmitAction("EditItem",params);
        
    }
    
}
function SubmitAction(action,params)
{
    //拼写请求URL参数
    var postParams = "Action="+action+"&"+params;
 
    $.ajax({ 
        type: "POST",
         url: "../../../Handler/Office/SystemManager/CideBigType.ashx?"+postParams,
        dataType:'string',
        data: '',
        cache:false,
        success:function(data) 
        {          
            var result = null;
            eval("result = "+data);
            
            if(result.result)                    
            { 
                LoadTree();
                 popMsgObj.ShowMsg(result.data)
                  document.getElementById("divSelectName").value="";
            }
            else
            {
 popMsgObj.ShowMsg(result.data)
            }    
        },
        error:function(data)
        {
//             MsgBox(data.responseText);
        }
    });
}
function treeview_delete()
{ 
    if(!enableDel)
    {
    popMsgObj.ShowMsg('没有删除分类的权限！')
        return;
    }

 
    if(treeview_selnodeindex < 0)
    {
        popMsgObj.ShowMsg('请选择要删除的分类！')
        return;
    }
    
    if(treenodes[treeview_selnodeindex].SubNodes.length > 0)
    {
        popMsgObj.ShowMsg('该分类下还有下级分类，不能删除！')
        return;
    }
    
    if(!confirm("确认删除吗?"))
    {
        return;
    }
    var tflag=flagtype;
    
    
    SubmitAction("DelItem","ID="+treenodes[treeview_selnodeindex].ID+"&TableName="+tflag);
}
function showdiv()
{
  ShowDeptTree("");
}



function ShowDeptTree(deptID)
{
   deptNO=document.getElementById("selBigType").value;
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
        url: "../../../Handler/Office/SystemManager/Tree.ashx?Action=InitTree&DeptID=" + deptID +"&deptNO="+deptNO+"&TableName="+flagtype,
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
//            hidePopup();
            //显示组织机构树
            //第一次初期化时
            if (deptID == null || "" == deptID)
            {
                    if(data=="")
                    {
                     document.getElementById("divDept").style.display = "none";
                     document.getElementById('divSelectName').readOnly=false;
//                      document.all.divSelectName.readOnly=false;
//                        document.all.divSelectName.value="0"
                    }
                    else
                    {
                     document.getElementById("divDeptTree").innerHTML = data;
                      document.getElementById("divDept").style.display = "block";
//                      document.getElementById("divSelectName").readonly=true;
                     document.getElementById('divSelectName').readOnly=true;
                    }
                //设置子组织机构信息
//                    alert(data);
                   

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
function SetSelectValue(deptID, deptName, superDeptID)
{
    //上级机构ID
    document.getElementById("hidSelectValue").value = deptID + "|" + deptName + "|" + superDeptID;
    //显示选中项
    if (deptName == "") deptName = "全部组织机构";
    document.getElementById("divSelectName").value = deptName;
    document.getElementById("hf_deptID").value = deptID;
    hideMList();
    document.getElementById("slSupperTypeID").value=deptID;
}
function hideMList()
{
    document.getElementById("divDept").style.display = "none";
}