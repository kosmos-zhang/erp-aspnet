/*
nodes（节点数组）:
[
    {text:"text",value:"value",nodeType:"nodeType"，subNodes:[
        {text:"text",value:"value",nodeType:"nodeType"，subNodes[]},
        {text:"text",value:"value",nodeType:"nodeType"，subNodes[]}
        ...
    ]}，
    
    {}
    
    ...
]

node(节点)
text:显示文字
value:值
nodeType：类型从1开始，如：1或2或3.。。。
subNodes:子节点数组

*/

/*
written by dcyou(游德春) at 2009.5.5
last edited at 2009.6.26,
E-Mail:dchunzi@163.com

*/




var list="";
function list_write(s)
{
    list += s;
}

function list_print()
{
    alert(list);
    list="";
}


    function hideSelects()
    {
        var sels = document.getElementsByTagName("SELECT");
        for(var i=0;i<sels.length;i++)
        {
            sels[i].style.display = "none";
        }
    }
    function showSelects()
    {
        var sels = document.getElementsByTagName("SELECT");
        for(var i=0;i<sels.length;i++)
        {
            sels[i].style.display = "block";
        }
    }
    


//ff && ie Event start here
function SearchEvent()
{    
    if(document.all)
        return event;

    func=SearchEvent.caller;
    while(func!=null)
    {
        var arg0=func.arguments[0];             
        if(arg0)
        {
            if(arg0.constructor==MouseEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==KeyboardEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==Event) // 如果就是event 对象
                return arg0;
        }
        func=func.caller;
    }
    return null;
}

function GetEventSource(evt)
{        
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
           
    return evt.target||evt.srcElement;
}

function GetEventCode(evt)
{
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
    
    return evt.keyCode || evt.charCode;
}
    
//eventName,without 'on' for: click
function AddEvent(obj,eventName,handler)
{    
	if(document.all)
	{	    
		obj.attachEvent("on"+eventName,handler);
	}else{	    
		obj.addEventListener(eventName,handler,false);
	}
}
//ff && ie Event end here
    
function Guid()
{
    var guid = "";
    for (var i = 1; i <= 32; i++)
    {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid  +=   n;
        if((i == 8) || (i == 12) || (i == 16) || (i == 20))
        {
            guid += "-";
        }
    } 
    return guid;
}

function elePos(et) {   
    var left=0;
	var top=0;
	while(et.offsetParent){
	left+=et.offsetLeft;
	top+=et.offsetTop;
	et=et.offsetParent;
	}
	left+=et.offsetLeft;
	top+=et.offsetTop;
	return {x:left,y:top}; 
};

/// <summary>
/// TreeView
/// </summary>
/// <param name="containerID">TreeView的容器元素的ID</param>
/// <param name="nodes">节点数组</param>
/// <param name="selMode">选择模式0:多选;1:单选</param>
/// <param name="selNodeType">可选节点类型:0不限制</param>
/// <param name="expandLevel">默认展开层级(数字)</param>      
/// <param name="mode">弹出(0) OR 平板(1) 显示方式</param>      
/// <param name="valNodeType">取值节点类型</param>     
/// <param name="selDuplicate">取值是否允许重复（1:启用；0:禁用）</param>
/// <param name="enableLinkage">是否启用联动效果(1:启用；0:禁用,默认：1)</param>
var TreeView = function(containerID,nodes,selMode,selNodeType,expandLevel,mode,valNodeType,selDuplicate,enableLinkage){


    var _this = this; //对象自身的引用
    var _guid = Guid();
    this.getGuid = function(){
        return _guid;
    };
    
    var treeNodes = [];//所有节点的序列
    treeNodes.hashList = {};//节点值和索引的键值对
    
    var selNodes = [];//选择节点在 treeNodes  中的索引
    
    var selMode = selMode?selMode:0;//0:多选，1：单选
    var selNodeType= selNodeType?selNodeType:0;//0:all,或者 node.nodeType的其中一个值
    var mode = mode?mode:0;//0:默认 平板，1：弹出
    var valNodeType= valNodeType?valNodeType:0;//取值节点类型
    var selDuplicate = selDuplicate?selDuplicate:false;//取值是否允许重复
    
    var expandLevel =expandLevel?expandLevel:1;
    
    var enableLinkage = (enableLinkage==0)?0:1;
    
    //获取容器元素并验证
    var container = document.getElementById(containerID);
    if(typeof container == undefined || container == null)
    {
        throw new Error("容器元素的ID不正确");
    }
    container.innerHTML = "";  
    if(mode == 1)
    {
        container.style.display = "none";//弹出方式下默认隐藏
        container.style.position = "absolute";
    }
    
    
    
     //获取选中的节点 值/列表
    this.getValue = function(){
        var ret = {txt:'',val:''};
        
        if(selMode == 1)
        {
            if(selNodes.length == 0)
                return ret;
            ret.val = treeNodes[selNodes[0]].value;
            ret.txt = treeNodes[selNodes[0]].text;
            return ret;
        }
        
        for(i=0;i<selNodes.length;i++)// in selNodes)
        {
            var selNode = treeNodes[selNodes[i]];
            if(selNode.nodeType == valNodeType || valNodeType == 0)
            {                
                if( (","+ret.val).indexOf(","+selNode.value+",")  != -1 && !selDuplicate)
                {
                   continue;
                }
                
                ret.val += selNode.value+",";
                ret.txt += selNode.text+",";
            }
        }
        if(ret.val.length > 0)
            ret.val = ret.val.substring(0,ret.val.length-1);
         if(ret.txt.length > 0)
            ret.txt = ret.txt.substring(0,ret.txt.length-1);       
        return ret;
    };
    
    var _targetEle = null;
    this.show = function(){
        if(mode != 1)
        return;  
        
        var ele = GetEventSource();
        var pos = elePos(ele);
        //alert(pos.x+":"+pos.y);
        _targetEle = ele;
         
         hideSelects();
                
        container.style.display = "";
        
        container.style.left = pos.x+"px";
        container.style.top = pos.y+"px";//+ele.offsetHeight;        
        
    };
    
    this.hide = function(){
      if(mode != 1)
        return;          
       
    showSelects();
        container.style.display = "none";        
      
    };
    
    var onOk = function(){
         _this.hide();
        var selValue = _this.getValue();
         _targetEle.value = selValue.txt;
        
        var _targetEle2 = document.getElementById(_targetEle.id+"Hidden");
        if(_targetEle2 == null)
            return;
        
        _targetEle2.value = selValue.val;
        
        //_targetEle.value
    };
    
    var onCancel = function(){
         _this.hide();
         //_targetEle.value  = "";
    };
    
    var onClear = function(){
        _this.hide();
        selNodes.length = 0;
        
        for(var i=0;i<treeNodes.length;i++)
        {
            var chk = treeNodes[i].element
            if(chk == null)
                continue;
            chk.checked = false;
        }
        if(_targetEle == null)
        {
            return;
        }
        _targetEle.value  = "";
        
        var _targetEle2 = document.getElementById(_targetEle.id+"Hidden");
        if(_targetEle2 == null)
            return;
        
        _targetEle2.value = "";
        
    };
    
    this.addSel = function(idx){
        selNodes.push(idx);
    };
    
    this.select = function(valArray){
        this.unSelectAll();
       
        for(var i=0;i<valArray.length;i++)
        {
            //treeNodes.hashList[node.value]=node.index;
            var nodeIndex = treeNodes.hashList[valArray[i]];
            if(nodeIndex != null)
            {
               var chk = treeNodes[nodeIndex].element;// document.getElementById(_guid+"treeview_checkbox"+nodeIndex);
               if(chk != null)
               {
                    chk.checked = true;
               }
               
               selNodes.push(nodeIndex);
            }
        };
              
    };
    
    
    
    this.unSelectAll = function()
    {
        selNodes.length = 0;
        
        for(var i=0;i<treeNodes.length;i++)
        {
            var chk = treeNodes[i].element
            if(chk == null)
                continue;
            chk.checked = false;
        }
        
    };
    this.selectAll = function()
    {
        selNodes.length = 0;
        
        for(var i=0;i<treeNodes.length;i++)
        {
            var chk = treeNodes[i].element
            if(chk == null)
                continue;
            chk.checked = true;
            
            selNodes.push(i);
        }
        
        if(_targetEle == null)
        {
            return;
        }
        _targetEle.value  = "";
        var _targetEle2 = document.getElementById(_targetEle.id+"Hidden");
        if(_targetEle2 == null)
            return;
        
        _targetEle2.value = "";
    };
    this.getNodes = function(){   
             
        return treeNodes;
    };
    this.getSelNodesIndex = function(){
        return selNodes;
    };
    
    this.checkAllSelected = function()
    {
        for(var i=0;i<treeNodes.length;i++)
        {
            var chk = treeNodes[i].element
            if(chk == null)
                continue;
            if(!chk.checked)
            {
                return false;
            }
        }
        
        return true;
        
    };
    
        
    //展开折叠子节点
    var node_expand = function(){
        var obj = GetEventSource();
        while(obj.tagName != "TD")
        {
            obj=obj.parentNode;
        }
        
       
        
        
        var img = obj.firstChild;
        if(img.tagName != "IMG")
        {
            img = obj.previousSibling.firstChild;
        }
        
        var tr = obj.parentNode;   
        var ntr = tr.nextSibling.cells[1].firstChild;
        
        var nodeIndex = parseInt(tr.cells[1].getAttribute("nodeIndex"));
             
        
        if(ntr.style.display == "none")
        {
            setAllParentBlank(nodeIndex,true);
            
            ntr.style.display = "";
            img.src = "/images/treeimg/WebResource6.gif";
        }else{
            setAllParentBlank(nodeIndex,false);
            
            ntr.style.display = "none";
            img.src = "/images/treeimg/WebResource5.gif";
        }
        
    };    
    
    var node_onmouseover = function(){
        var obj = GetEventSource();        
         obj.style.color = "#ff0000";       
    };
    var node_onmouseout = function(){
        var obj =  GetEventSource();
         obj.style.color = "#000000";
    };
    
    //选中事件handler
    var node_onsel = function(){
        var obj =  GetEventSource();                
        if(obj.tagName == "A")
        {
            obj = obj.previousSibling;
           if(!obj.checked)
           {
                    obj.checked = true;
           }else{
                if(obj.type == "checkbox")
                {
                    obj.checked = false;
                }
           }
        }
       
        //单选
        if(selMode == 1)
        {
            selNodes[0] = obj.value;         
        }else{        
            var idx = obj.value;     
            
            var obj2 = treeNodes[idx].element;
            //alert(obj.parentNode.outerHTML+"\n"+obj.checked+"\n\n"+obj2.parentNode.outerHTML+"\n"+obj2.checked);
          
            node_addSel(idx);//取得当前选中节点，并加入已选择节点索引列表
           
            if(enableLinkage == 1)
            {
                node_allchks(idx);//checkbox 连动 处理子节点
                node_allchks2(idx);//checkbox 连动 处理父节点
            }
        }
        
        if(_this.callback)
        {
            _this.callback(_this.getValue());
        }
    };
    
    
    //多选的模式下才会用到
    var node_addSel = function(idx){    
        var obj = treeNodes[idx].element;
       
        
         //多选
        if(obj.checked)
        {
            var found = false;
            for(var i=0;i <selNodes.length;i++)
            {
                if(selNodes[i] == idx)
                {                
                    found = true;
                    break;
                }
            }
            if(!found)
            {               
                selNodes.push(idx);
            }
            
            
        }else{
            //alert(selNodes+":"+idx);
            for(i=0;i<selNodes.length;i++)// m in selNodes)
            {
                if(selNodes[i] == idx)
                {
                    for(var j=i;j<selNodes.length-1;j++)
                    {        
                        selNodes[j] = selNodes[j+1];
                    }
                    selNodes.length--;                       
                    break;
                }
            }
            //alert(selNodes+":"+idx);
        }
    };
    
    //递归联动子节点
   var node_allchks = function (idx)
    {
        var chk = treeNodes[idx].element;
        if(chk == null)
        {
            return;
        }
            
        //有子节点
        if(treeNodes[idx].subNodes.length>0)
        {       
                for(var i=0; i< treeNodes[idx].subNodes.length;i++)
                {
                    var chk2 = treeNodes[treeNodes[idx].subNodes[i].index].element;
                    if(chk2 == null)
                    {
                        continue;
                    }
                    chk2.checked = chk.checked;
                    node_addSel(treeNodes[idx].subNodes[i].index);
                    
                    node_allchks(treeNodes[idx].subNodes[i].index);
                    
                }       
       } 

    }

   //递归联动父节点
   var node_allchks2 = function (idx)  
    {
        var chk = treeNodes[idx].element;
          if(chk == null)
          {      
                if(treeNodes[idx].pIndex != -1)
                {
                     node_allchks2(treeNodes[idx].pIndex);
                }
                return;
          }
        
        //有父节点
        if(treeNodes[idx].pIndex != -1)
        {
            if(!chk.checked )
            {
                var selCountOfPNode = 0;
                for(var i=0; i<treeNodes[treeNodes[idx].pIndex].subNodes.length;i++)
                {
                    var tchildIndex = treeNodes[treeNodes[idx].pIndex].subNodes[i].index;
                    var chk3 = treeNodes[tchildIndex].element;
                    if(chk3 == null)
                    {                        
                        continue;
                    }
                    if(chk3.checked)
                    {
                        selCountOfPNode++;
                    }
                }
                
                if(selCountOfPNode > 0)
                    return;
            }
        
          var chk2 =  treeNodes[treeNodes[idx].pIndex].element;
          if(chk2 != null)
          {      
            chk2.checked = chk.checked;
            node_addSel(treeNodes[idx].pIndex);
           }
                       
            node_allchks2(treeNodes[idx].pIndex);
            return;
        }
    }

    var getChildCount = function(nodes,i)
    {
        
        var cnt = nodes[i].subNodes.length;
        
        if(cnt == 0)
        {
            return cnt;
        }
        
        for(var j=0;j<nodes[i].subNodes.length;j++)
        {
            cnt += getChildCount(nodes[i].subNodes,j);
        }
        
        return cnt;
    }
    
    
    var getTChildCount = function(nodes,i)
    {      
        var cnt = nodes[i].tChildCount;
        
        //list_write(nodes[i].index+":"+cnt+"\n"); 
        
//        if(nodes[i].subNodes.length == 0)
//        {
//            return cnt;
//        }
        
        
        for(var j=0;j<nodes[i].subNodes.length;j++)
        {
            if(nodes[i].subNodes[j].length > 0)
            {
                var tt = getTChildCount(nodes[i].subNodes,j);                
                cnt += tt;
                //list_write(nodes[i].subNodes[j].index+":"+tt+"(0)\n"); 
            }
            
        }
        
        return cnt;
    }
    
    var setLeftBlank = function(index){
        var _ttNode = treeNodes[index];
        //document.getElementById(_guid + "_"+ _ttNode.index).innerHTML = _ttNode.childCount+"/"+_ttNode.tChildCount;
        var blankEle = document.getElementById(_guid + "_"+ _ttNode.index);
        
        blankEle.innerHTML = "";
        for(var i=0;i<_ttNode.tChildCount;i++)
        {
            var imgEle = document.createElement("img");
            imgEle.src = "../../../images/treeimg/_line4.gif";
            
             blankEle.appendChild(imgEle);
             blankEle.appendChild(document.createElement("br"));
        }
        
        //tChildCount
      // blankEle.appendChild(document.createTextNode(_ttNode.tChildCount));
    };
    
    
    var setParentBlank = function(index,step)
    {           
       
        treeNodes[index].tChildCount += step;                
        
        setLeftBlank(index);
        
        if(treeNodes[index].pIndex >= 0)
        {
            setParentBlank(treeNodes[index].pIndex,step);
        }
    };
    
    
    var setAllParentBlank = function(index,flag){ 
    
        var ttt = 0;
        
        
        {            
            ttt = treeNodes[index].subNodes.length;    
            //list_write(ttt+"\n");         
            if(ttt > 0)
            {
                for(var i=0;i<treeNodes[index].subNodes.length;i++)
                {                
                    t_cnt = 0;//    
                    ttt += getTChildCount(treeNodes[index].subNodes,i);
                     //list_write(ttt+"\n");      
                }
            }
        }      
       
       // list_print();
        
        treeNodes[index].tChildCount = flag?ttt:0;
               
        setLeftBlank(index);
        
       if(treeNodes[index].pIndex >= 0)
       {
           setParentBlank(treeNodes[index].pIndex,flag?ttt:0-ttt);
       };
        
    };
    
    
    
    
    
    //构建UI
    var buildTree = function(nodes){
            container.innerHTML = "";
    
            var curLevel = 0;
            var tb = document.createElement("TABLE");
            //tb.border = "1";               
            tb.cellSpacing="0";
            tb.cellPadding="0";            
            container.appendChild(tb);
            
            if(mode ==1)
            {
                var tcl = tb.insertRow(-1).insertCell(-1);
                tcl.colSpan=2;
                var ttb = tcl.appendChild(document.createElement("TABLE"));
                var trr = ttb.insertRow(-1);
                
                ttb.cellPadding=5;
                ttb.cellSpacing = 0;
                
                trr.insertCell(-1).innerHTML = "<a href=\"#\">确定</a>";
                trr.insertCell(-1).innerHTML = "<a href=\"#\">取消</a>";
                trr.insertCell(-1).innerHTML = "<a href=\"#\">清空</a>";
                
                AddEvent(trr.cells[0],"click",onOk);
                AddEvent(trr.cells[1],"click",onCancel);
                AddEvent(trr.cells[2],"click",onClear);
                
            }
            
            var addSortedNode = function(node)
            {
                node.index = treeNodes.length;
                treeNodes.push(node);
                
                treeNodes.hashList[node.value]=node.index;
                
                
            }
                          
                
            //构建子节点
            var buildSubNodes = function(nodes,ti,ttb){
                var node = nodes[ti];
                                            
                node.childCount = getChildCount(nodes,ti);                
                node.tChildCount = 0;
                
                addSortedNode(node);
                
  
                
                var tr = ttb.insertRow(-1);               
                
                //节点图片
                if(node.subNodes.length>0)
                {
                    if((curLevel+1) > expandLevel)
                    {
                        tr.insertCell(-1).innerHTML = "<img  src=\"../../../images/treeimg/WebResource5.gif\">";
                    }else{
                        tr.insertCell(-1).innerHTML = "<img  src=\"../../../images/treeimg/WebResource6.gif\">";
                   }
                   
                   AddEvent(tr.cells[tr.cells.length-1],"click",node_expand);
                   
                }else{
                   var ttcell = tr.insertCell(-1);
                   ttcell.align="center";
                   ttcell.innerHTML = "<img src=\"../../../images/treeimg/_line1.gif\">";
                }    
            
             //单选 OR 复选框
               var thtml ="<input   type=checkbox  value=\""+node.index+"\" >";//id=\""+_guid+"treeview_checkbox"+node.index+"\"
               if(selMode ==1)
               {
                    thtml ="<input  name=node_radio type=radio value=\""+node.index+"\">";// id=\""+_guid+"treeview_radio"+node.index+"\"
               }
               if(node.nodeType != selNodeType && selNodeType != 0)
               {
                    thtml="";
               }
               
               
               //节点连接
               thtml += "<a style=\"color:black;cursor:pointer;\" >"+node.text+"</a>";
               var _tttd = tr.insertCell(-1);
               _tttd.innerHTML = thtml;
               
               if(_tttd.firstChild.tagName != "A")
               {                    
                    node.element = _tttd.firstChild;
               };
               
               _tttd.setAttribute("nodeIndex",node.index);
                          
               var alink = tr.cells[tr.cells.length-1].childNodes[tr.cells[tr.cells.length-1].childNodes.length-1];
               
                if(node.nodeType == selNodeType ||  selNodeType == 0)
               {                  
                  
                   AddEvent(tr.cells[tr.cells.length-1].firstChild,"click",node_onsel); 
                   AddEvent(alink,"click",node_onsel); 
               }
               
              
              if(node.subNodes.length>0)
              {
                 AddEvent(alink,"click",node_expand);                       
              }
 
               AddEvent(alink,"mouseover",node_onmouseover);
               AddEvent(alink,"mouseout",node_onmouseout); 
                
                node.tChildCount = 0;
               
                curLevel++;
                if(node.subNodes.length > 0)
                {
                    var subTb = document.createElement("TABLE");     
                   // subTb.border = "1";               
                    subTb.cellSpacing="0";
                    subTb.cellPadding="0";
                    subTb.style.display="";
                                       
                
                    var ttr = ttb.insertRow(-1);
                    var ttcell = ttr.insertCell(-1);
                    ttcell.align="center";
                   // ttcell.innerHTML = "<img src=\"/images/treeimg/_line4.gif\">";
                    //ttcell.innerHTML= node.childCount+"/"+node.tChildCount;
                    ttcell.setAttribute("id", _guid + "_"+ node.index);
                    
                    //alert(ttcell.outerHTML);return;                    
                    
                    
                     //默认展开级别判断
                    if(curLevel > expandLevel)
                    {
                        subTb.style.display="none";
                        node.tChildCount = 0;
                    }else{
                       
                    
                        node.tChildCount = node.subNodes.length;
                        var _ttNode = node;
                        setLeftBlank(_ttNode.index);    
                          
                          
                        while (_ttNode.pIndex >= 0)
                        { 
                            _ttNode = treeNodes[_ttNode.pIndex];                            
                            _ttNode.tChildCount += node.tChildCount;                            
                          
                            setLeftBlank(_ttNode.index);                             
                          // document.getElementById(_guid + "_"+ _ttNode.index).innerHTML = _ttNode.tChildCount;
                        }
                        
                        
                    }                                        
                    
                    ttr.insertCell(-1).appendChild(subTb);
                
                    for(var i=0;i<node.subNodes.length;i++)
                    {          
                        //ttcell.appendChild(document.createElement("<img src=\"/images/treeimg/_line4.gif\">"));
                       // ttcell.appendChild(document.createElement("<br>"));
                      
                        node.subNodes[i].pIndex = node.index;                        
                        buildSubNodes(node.subNodes,i,subTb);//递归调用处理子节点
                    }
                    
                }
                curLevel--;
                
            };
            
            for(var i=0;i<nodes.length;i++)
            {
                if(typeof nodes[i] == "undefined")
                {
                    continue;
                }
                
                nodes[i].pIndex = -1;
                
                nodes[i].childCount =getChildCount(nodes,i);
                nodes[i].tChildCount = 0;
                
                buildSubNodes(nodes,i,tb);                
                
            }
            
            
            // alert(treeNodes[0].tChildCount);
    };
    
 
    buildTree(nodes);//执行构建
    
  
   
    
};

