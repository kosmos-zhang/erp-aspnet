var flagtype=document.getElementById('hf_flag').value;
$(document).ready(function(){
    LoadTree();
 
    
}); 
var enableNew = true;
var enableEdit = true;
var enableDel = true;


    
function LoadTree()
{
    var flag=flagtype;
    //拼写请求URL参数
    var postParams = "Action=LoadData"+"&flag="+flag;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/SystemManager/CategorySet.ashx?"+postParams,
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
try
{
    if(!enableEdit)
        return;
    
    if(!(idx>=0))
    {
//        if(treeview_selnodeindex == -1)
//        {
//            MsgBox("请点击选择一个要修改的分类");
//            return;
//        }
        idx = treeview_selnodeindex;
    }
//    document.getElementById("curAction").innerHTML = "修改分类";

    document.getElementById("txtID").value = treenodes[idx].ID;
    document.getElementById("CodeName").value = treenodes[idx].CodeName;  
      document.getElementById("UsedStatus").value = treenodes[idx].UsedStatus;  
        document.getElementById("txt_WarningLimit").value = treenodes[idx].WarningLimit;  
          document.getElementById("txt_Description").value = treenodes[idx].Description;  
   
    //document.getElementById("slFlag").value = treenodes[idx].Flag;
    document.getElementById("slSupperTypeID").value = treenodes[idx].SupperID;
    document.getElementById("slSupperTypeID").disabled = true;
    document.getElementById("img_save").style.display = "";
    }
    catch(e)
    {}
}
function treeview_cancel()
{
    document.getElementById("txtID").value = "";
    document.getElementById("CodeName").value = "";    
    //document.getElementById("slFlag").value =  "-1";
    document.getElementById("slSupperTypeID").value = "0";
    document.getElementById("slSupperTypeID").disabled = false;
       document.getElementById("UsedStatus").value = "1";    
          document.getElementById("txt_WarningLimit").value = "0";    
             document.getElementById("txt_Description").value = "";    
    
    
//    document.getElementById("curAction").innerHTML = "新建分类";
    
    
    if(!enableNew)
    {
        
        document.getElementById("img_save").style.display = "none";
    }
}




function treeview_save()
{   
   var fieldText = "";
    var msgText = "";
    var isFlag = true;
   var tflag=flagtype;
    var tTypeName = document.getElementById("CodeName").value;    
    var tSupperTypeID = document.getElementById("slSupperTypeID").value;
    var Description=document.getElementById("txt_Description").value;
    var UsedStatus=document.getElementById("UsedStatus").value;
    var WarningLimit=trim(document .getElementById("txt_WarningLimit").value);
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
    if(WarningLimit!="")
    {
      if(!IsNumber(WarningLimit))
    {
isFlag = false;
        fieldText = fieldText + "报警下限|";
   		msgText = msgText +  "报警下限格式必须是非负整数|"
    }
    }
      if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
     var params = "TypeName="+tTypeName+"\
                        &Flag=0\
                        &SupperTypeID="+tSupperTypeID+"\
                         &flagtype="+tflag+"\
                        &Description="+Description+"\
                        &UsedStatus="+UsedStatus+"\
                        &WarningLimit="+WarningLimit+"";   
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
         url: "../../../Handler/Office/SystemManager/CategorySet.ashx?"+postParams,
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
            }
            else
            {
//                MsgBox(result.data);
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
         
    
    
    SubmitAction("DelItem","ID="+treenodes[treeview_selnodeindex].ID);
}