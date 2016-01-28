<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeptQuarterSelSingleElection.ascx.cs" Inherits="UserControl_Human_DeptQuarterSelSingleElection" %>
<style type="text/css">
        #treeDiv TD{padding:2px;}
        
        BODY {font-size:12px;}
    </style>           
        
<div id="treeDiv"  style="text-align:left; display:none; width:300px;margin:0;height:400px;overflow:auto;background:#f1f1f1;border:solid 1px #3366cc;">    
</div>

<script language="javascript"  type="text/javascript">
 
var treeveiwPopUp = null;

 function alerDeptQuarter()
{
   $.ajax({ 
    type: "POST",
    url: "../../../Handler/Common/UserDept.ashx?action=deptquarter",
    dataType:'string',
    data: '',
    cache:false,
    success:function(data) 
    {                          
        var result = null;
        eval("result = "+data);
        
        if(result.result)                    
        {                      
            LoadNodes(result.data);                     
        }else{                  
               alert(result.data);               
        }
    },
    error:function(data)
    {
         alert(data.responseText);
    }
 });            
}

function LoadNodes(nodes)
{   
/// <param name="containerID">TreeView的容器元素的ID</param>
/// <param name="nodes">节点数组</param>
/// <param name="selMode">选择模式0:多选;1:单选</param>
/// <param name="selNodeType">可选节点类型:0不限制</param>
/// <param name="expandLevel">默认展开层级</param>      
/// <param name="mode">弹出 OR 平板 显示方式</param>      
/// <param name="valNodeType">取值节点类型</param>     
/// <param name="selDuplicate">取值是否允许重复（1:启用；0:禁用）</param>
/// <param name="enableLinkage">是否启用联动效果(1:启用；0:禁用,默认：1)</param>
   
   //treeveiwPopUp = new TreeView("treeDiv",nodes,1,0,2,1,-1);
   treeveiwPopUp = new TreeView("treeDiv",nodes,1,0,2,1,0,1,0);
   
   //treeveiwPopUp = new TreeView("treeDiv",nodes,0,0,2,1,2);
   //treeveiwPopUp = new TreeView("treeDiv",nodes,0,2,3,1);
   //treeveiwPopUp = new TreeView("treeDiv",nodes,0,2,3,1,2);
   //treeveiwPopUp = new TreeView("treeDiv",nodes,0,1,2,1,2);
   
   //treeveiwPopUp = new TreeView("treeDiv",nodes,1,0,2,1);   
   //treeveiwPopUp = new TreeView("treeDiv",nodes,1,1,2,1);              
   
}

 </script>