<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleFunction_Edit.aspx.cs" Inherits="Pages_Office_SystemManager_RoleFunctionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色赋权操作</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/common/TreeView.js" type="text/javascript"></script>
 
 
    <script type="text/javascript" language="javascript">
        function msgBox(msg)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
        }
    
        function onRoleSel(v){
           var roleid = v.val;
           
            msgBox("正在处理中...");
            
           $.ajax({
               type: "POST",//用POST方式传输           
               url:  '../../../Handler/Office/SystemManager/RoleFunction.ashx',//目标地址
               cache:false,
               data: "Action=getrolefunctions&RoleID="+roleid,//数据
               
               success: function(result)
               {
                
                    var reaults;
                    eval("reaults="+result);
                    
                   // alert(reaults.data);
                   // setTimeout(function(){
                        moduleFuncTree.select(reaults.data);
                        hidePopup();
                   // },500);
                    
                    
               }
           });
           
           
        };
        
        function onSaveRoleInfo()
        {
            msgBox("正在处理中...");
            
            setTimeout(function(){
                var selvs = moduleFuncTree.getValue().val;
                var roleid= roleTree.getValue().val;
                //alert(selvs+":"+roleid);
                
                if(roleid +"" == "")
                {
                    hidePopup();
                    msgBox("没有选择角色");
                  
                    return;
                }
                
                
               
                
               $.ajax({
                   type: "POST",//用POST方式传输           
                   url:  '../../../Handler/Office/SystemManager/RoleFunction.ashx',//目标地址
                   cache:false,
                   data:"Action=setrolefunctions&RoleID="+roleid+"&functions="+selvs,//数据
                   
                   
                   success: function(data)
                   {
                    
                        var result = null;
                        eval("result = "+data);     
                        
                        
                        hidePopup();
                        msgBox(result.data);
                        
                        
                        
                   }
               });
           
            },500);
                
           
          
        }
    
    
        //var roleNodes=[];
        //var moduleNodes=[];
    
        //treeview = new TreeView("treeDiv",nodes);
        /// <param name="containerID">TreeView的容器元素的ID</param>
        /// <param name="nodes">节点数组</param>     
                 
        /// <param name="selMode">选择模式0:多选;1:单选</param>
        /// <param name="selNodeType">可选节点类型:0不限制</param>        
       /// <param name="expandLevel">默认展开层级</param>
       /// <param name="mode">弹出(1) OR 平板 显示方式(0)</param>     
       // <param name="valNodeType">取值节点类型</param>     
        var roleTree=null;
        var moduleFuncTree=null;
               
        
        $(document).ready(function(){
            roleTree = new TreeView("roleDiv",roleNodes,1,1,2,0,1);
            moduleFuncTree= new TreeView("moduleFuncDiv",moduleNodes,0,0,0,0,0);
            
            roleTree.callback = onRoleSel;
            
            
        });
        
        function selectALLchk()
        {
            var roleid= roleTree.getValue().val;
            //alert(selvs+":"+roleid);
            
            if(roleid +"" == "")
            {
                msgBox("没有选择角色");
                return;
            }
            
            
          //  msgBox("正在处理中...");
                        
           // setTimeout(function(){
               moduleFuncTree.selectAll();
               // hidePopup();
           // },500);
                    
        }
        
        
        function doPnode(_guid,nodes,i)
        {
            
           var chk = nodes[i].element;// document.getElementById(_guid+"treeview_checkbox"+i);
            if(chk)
            {
                if(chk.checked)
                    return;
                    
                chk.checked = true;
                moduleFuncTree.addSel(i);
            };
            if(nodes[i].pIndex >= 0)
            {
                doPnode(_guid,nodes,nodes[i].pIndex);
            }
        }
        
        function selAllView()
        {
            var roleid= roleTree.getValue().val;
            //alert(selvs+":"+roleid);
            
            if(roleid +"" == "")
            {
                msgBox("没有选择角色");
                return;
            }
            
           // msgBox("正在处理中...");   
            
            
           // setTimeout(function(){
             
                moduleFuncTree.unSelectAll();
            
                var nodes = moduleFuncTree.getNodes();
                var _guid = moduleFuncTree.getGuid();
                for(var i=0;i<nodes.length;i++)
                {
                    if(nodes[i].childCount ==0)
                    {        
                        if(nodes[i].value.indexOf("_1") != -1)// || nodes[i].value.indexOf("_") == -1)
                        {            
                            doPnode(_guid,nodes,i);
                        }
                        
//                        if(nodes[i].value.indexOf("_2") != -1)
//                        {
//                            if(nodes[nodes[i].pIndex].childCount == 1)
//                            {
//                                doPnode(_guid,nodes,nodes[i].pIndex);
//                            }
//                        }
                    }
                }
            
            
               // hidePopup();
            //},500);
            
            
        }
        
        function unSelAll()
        {
            moduleFuncTree.unSelectAll();
        }
        
    </script>
    
    <style type="text/css">
        #roleDiv{width:200px;height:400px;border:solid 1px #c1c1c1;overflow:auto;}
        #moduleFuncDiv{width:300px;height:400px;border:solid 1px #c1c1c1;overflow:auto;}
    </style>
</head>
<body  style="background-color:#FFFFFF">
    <form id="form1" runat="server">
    <span id="Forms" class="Spantype" name="Forms"></span>
        
    <table>
        <tr><th align="left">
            <img alt="确认" id="btn_save" runat="server" visible="false" onclick="onSaveRoleInfo()" src="../../../Images/Button/Bottom_btn_confirm.jpg" />
      
        </th><th></th><th align="left">
            
            <input type="button" value="全部查看权限" onclick="selAllView()"/>
            <input type="button" value="全部权限" onclick="selectALLchk()"/>
            <input type="button" value="清空权限" onclick="unSelAll()"/>
        </th></tr>
        <tr><td>角色列表</td><td></td><td>权限列表</td></tr>
        <tr>
            <td>                
                <div id="roleDiv"></div>
            </td>
            <td width="100">
            
            </td>
            <td>
                <div id="moduleFuncDiv"></div>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>

