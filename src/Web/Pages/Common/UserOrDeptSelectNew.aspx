<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserOrDeptSelectNew.aspx.cs" Inherits="Pages_Common_UserOrDeptSelectNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户部门选择</title>
    <script language="javascript" type="text/javascript">
    function GetInfo()
    {
        var select = GetSelectValue();
        var TypeID= document.getElementById("txtTypeID").value;
        if (select == null || select == "")
        {
            alert("请选择部门或人员!");
            return false;
        }
        else
        {
            if(TypeID=="1")
            {
            var name="";
            var Info=select.replace(/ /g,"");
            var controlid= window.parent.document.getElementById("txtUserDept").value;
            if(controlid.indexOf("User") >= 0)
            {
                if(Info.indexOf("User") >= 0 && Info.indexOf("Dept") < 0)
                {
                    var getinfo = Info.split(",");
                    for(var i=0; i < getinfo.length; i++)
                    {
                        var c = getinfo[i].toString();
                        name += c.substring(c.indexOf("|")+1,c.indexOf("-"))+",";
                    }
                    name = name.substring(0, name.length - 1);
                    var ControlID = controlid.split(",");
                    window.parent.document.getElementById(ControlID[0]).value = name.replace(/ /g,"");
                    var splitinfo = Info.split(",");
                    var ID="";
                    for(var i=0; i<splitinfo.length; i++)
                    {
                        ID += splitinfo[i].substring(0, splitinfo[i].toString().indexOf("|")) + ",";
                    }
                    ID = ID.substring(0, ID.length-1);
                    ID=ID.split("_");
                    window.parent.document.getElementById(ControlID[1]).value = ID[1].toString();
                  CloseHidenDiv();
                }
            }
            if(controlid.indexOf("Dept") >= 0)
            {
                if(Info.indexOf("Dept") >= 0 && Info.indexOf("User") < 0)
                {
                    var getinfo = Info.split(",");
                    for(var i = 0; i < getinfo.length; i++)
                    {
                        var c = getinfo[i].toString();
                        name += c.substring(c.indexOf("|") + 1, c.indexOf("-")) + ",";
                    }
                    name = name.substring(0, name.length - 1);
                    var ControlID = controlid.split(",");
                    window.parent.document.getElementById(ControlID[0]).value=name.replace(/ /g,"");
                    var splitinfo = Info.split(",");
                    var ID = "";
                    for(var i = 0; i < splitinfo.length; i++)
                    {
                        ID += splitinfo[i].substring(0, splitinfo[i].toString().indexOf("|")) + ",";
                    }
                    ID = ID.substring(0, ID.length - 1);
                    ID=ID.split("_");
                    window.parent.document.getElementById(ControlID[1]).value = ID[1].toString();
                   CloseHidenDiv();
                }
            }
        } 
         if(TypeID=="2")
         {
                    var name="";
                    var Info=select.replace(/ /g,"");
                    var controlid= window.parent.document.getElementById("txtUserDept").value;
       
                    var getinfo = Info.split(",");
                    for(var i=0; i < getinfo.length; i++)
                    {
                        var c = getinfo[i].toString();
                        name += c.substring(c.indexOf("|")+1,c.indexOf("-"))+",";
                    }
                    name = name.substring(0, name.length - 1);
                    var ControlID = controlid.split(",");
                    window.parent.document.getElementById(ControlID[0]).value = name.replace(/ /g,"");
                    var splitinfo = Info.split(",");
                    var ID="";
                    for(var i=0; i<splitinfo.length; i++)
                    {
                        ID += splitinfo[i].substring(0, splitinfo[i].toString().indexOf("|"))+"|";
                    }
                    ID = ID.substring(0, ID.length-1);
                    ID=ID.split("_");
                  
                    window.parent.document.getElementById(ControlID[1]).value =ID;
                   CloseHidenDiv();

         }
      } 
    }
    
    
    
    /* 
    * 
    */
    function GetUserOrDept()
    {
        var ControlID = window.parent.document.getElementById("txtCpntrolID").value;
        var UserOrDept;
        var select = GetSelectValue();
        var name = "";
        if (select == null || select == "")
        {
            alert("请选择部门或人员!");
            return false;
        }
        else
        {
            var Info = select;
            var name;
            var valuecontrol;
            if(Info.indexOf("Dept") >= 0 && Info.indexOf("User") < 0)
            {
                var Dept=Info.split(",");
                for(var i=0;i<Dept.length;i++)
                {
                    var c= Dept[i].toString();
                    name +=c.substring(c.indexOf("|")+1,c.indexOf("-"))+",";;
                }
                name = name.substring(0,name.length-1);
                if(ControlID != null && ControlID != "")
                {
                    var control = ControlID.split(",");
                    for(var i = 0; i < control.length; i++)
                    {
                        var c= control[i].toString();
                        if(c.indexOf("Dept") >= 0)
                        {
                            valuecontrol = c.substring(c.indexOf("|") + 1, c.length);
                        }
                    }
                }
                window.parent.document.getElementById(valuecontrol).value=name.replace(/ /g,"");
                window.parent.document.getElementById('txtHiddenFieldID').value=Info.replace(/ /g,"");
                Hide();
            }
            if(Info.indexOf("User") >= 0 && Info.indexOf("Dept") < 0)
            {
                var Info=select;
                var name;
                var valuecontrol;
                var User=Info.split(",");
                var Dept;
                for(var i = 0; i < User.length; i++)
                {
                    var c = User[i].toString();
                    name += c.substring(c.indexOf("|") + 1, c.indexOf("-")) + ",";
                }
                name = name.substring(0,name.length - 1);
                if(ControlID != null && ControlID != "")
                {
                    var control = ControlID.split(",");
                    for(var i = 0; i < control.length; i++)
                    {
                        var c   = control[i].toString();
                        if(c.indexOf("User") >= 0)
                        {
                            valuecontrol = c.substring(c.indexOf("|") + 1, c.length);
                            window.parent.document.getElementById(valuecontrol).value = name.replace(/ /g,"");;
                        }
                        if(c.indexOf("Dept") >= 0)
                        {
                            valuecontrol = c.substring(c.indexOf("|") + 1, c.length);
                            var Dept = Info.split(",");
                            var Deptname="";
                            var temp="";
                            var change="";
                            for(var i = 0; i < Dept.length; i++)
                            {
                                temp = Dept[i].toString();
                                change = temp.substring(temp.indexOf("-") + 1, temp.length)+",";
                                if(Deptname.indexOf(change) < 0)
                                {
                                    Deptname += change;
                                }
                            }
                            Deptname = Deptname.substring(0,Deptname.length - 1);
                            window.parent.document.getElementById(valuecontrol).value = Deptname.replace(/ /g,"");
                        }
                    }
                }
                window.parent.document.getElementById('txtHiddenFieldID').value=Info.replace(/ /g,"");;
                Hide();   
            }
            
            if(Info.indexOf("Dept")>=0  && Info.indexOf("User")>=0)
            {
                 var Info = select;
                 var name;
                 var valuecontrol;
                 var UserOrDept = Info.split(",");
                 for(var i =0; i < UserOrDept.length; i++)
                 {
                    var c = UserOrDept[i].toString();
                    name += c.substring(c.indexOf("|") + 1, c.indexOf("-")) + ",";
                 }
                 name = name.substring(0, name.length - 1);
                 if(ControlID !=null && ControlID !="")
                 {
                    var control = ControlID.split(",");
                    for(var i = 0; i < control.length; i++)
                    {
                        var c = control[i].toString();
                        if(c.indexOf("User") >= 0)
                        {
                            valuecontrol = c.substring(c.indexOf("|") + 1, c.length);
                            var user = name.substring(name.indexOf(",") + 1, name.length);
                            window.parent.document.getElementById(valuecontrol).value = user.replace(/ /g,"");;
                        }
                        if(c.indexOf("Dept") >= 0)
                        {
                            valuecontrol = c.substring(c.indexOf("|") + 1, c.length);
                            var dept = name.substring(0, name.indexOf(","));
                            window.parent.document.getElementById(valuecontrol).value = dept.replace(/ /g,"");;
                        }
                    }
                }
                window.parent.document.getElementById('txtHiddenFieldID').value=Info.replace(/ /g,"");;
                Hide(); 
            }
        }
    }
     
    /*
    * 获取选择值
    */  
    function GetSelectValue()
    {
        var select = "";
        //获取一览表
        var table = document.getElementById("dtDeptUser");
        //判断表示是否存在，不存在时返回null
        if (table == "undefined" || table == null)
        {
            return null;
        }
        //获取表格的行
        var row = document.getElementById("dtDeptUser").getElementsByTagName("tr")
        //遍历所有行，获取
        for(var rows = 0; rows < row.length; rows++)
        {
            //获取当前行的列数
            var cols = row[rows].getElementsByTagName("td");
            //遍历所有列，以获取值
            for(var col = 0;col < cols.length; col++)
            {
                //获取列的input控件
                var objs = cols[col].getElementsByTagName('input');
                //遍历所有控件
                for(var i = 0; i < objs.length; i++)
                {
                    //判断是否是选中的checkbox
                    if(objs[i].getAttribute("type") == "checkbox" && objs[i].checked)
                    {
                        //获取列的值
                        var values = objs[i].value + "-" + cols[0].innerText;
                        
                        select += values.toString() + ",";
                    }
                     //判断是否是选中的radiobutton
                    if(objs[i].getAttribute("type") == "radio" && objs[i].checked)
                    {
                        //获取列的值
                        var values = objs[i].value + "-" + cols[0].innerText;
                        
                        select += values.toString() + ",";
                    }
                }
            }
        }
        if (select.length > 1)
        {
            return select.substring(0, select.length - 1);
        }
        else
            return select;
    }
    
    
    

    function Hide()
    {
        window.parent.document.getElementById("DivSel").style.display = "none";
    }
    
    //关闭DIV
    function CloseHidenDiv(){
		//var closedivvv = window.parent.document.getElementById("HidenDiv");
	//	var Mydiv = document.getElementById("DivSel");
		//window.parent.document.body.removeChild(closedivvv); 
		
//		div.parentNode.removeChild(div);   
    //   closedivvv.parentNode.removeChild(closedivvv);

    
        window.parent.document.getElementById("div_ZCDiv").style.display = "none";
		window.parent.document.getElementById("DivSel").style.display = "none";
	}
	
	//清空
	function ClearInfo()
	{
	     var controlid= window.parent.document.getElementById("txtUserDept").value;
	     var ControlID = controlid.split(",");
	     window.parent.document.getElementById(ControlID[1]).value ="";
	     window.parent.document.getElementById(ControlID[0]).value ="";
	      CloseHidenDiv();
	}
	
	
	
	
</script>
    <style type="text/css">
       .FontSize
        {
        	 font-family: "tahoma";
              font-size: 12px;
       	}
        #btnOk
        {
            width: 47px;
        }
        #btnNO
        {
            width: 49px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <div id="divDeptUser" runat="server"   class="FontSize"
     style="z-index:1;background-color:white;position:absolute; height:200px; top: 15px; left: 10px;" >   
   <div id="" style="top:auto;left:5px;">
   <input id="btnOk" type="button"  value="确 认"  onclick="GetInfo();" /> 
    &nbsp;<input id="btnNO" type="button" value="取 消" onclick=" CloseHidenDiv();"  />
     &nbsp;<input id="btnClear" type="button" value="清 空"  onclick="ClearInfo();"  />
</div>
     </div> 
     <p>
       <input id="txtTypeID"  type="hidden" runat="server" />
     </p>
    </form>
</body>
</html>
