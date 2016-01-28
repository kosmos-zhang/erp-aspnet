<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company_Edit.aspx.cs" Inherits="Pages_SystemManager_Company_Edit" %>
<%@ Register src="../../UserControl/ProvinceControl.ascx" tagname="ProvinceControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息编辑</title>
      <link href="../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../js/common/Ajax.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
 
 function GetCity()
 { 
    var SetProvalue = document.form1.SetPro.value;
    var url = "../../Handler/SystemManager/City.ashx?ProCode="+SetProvalue;
    SendRequest("POST", url, null, true, SendCompleted, "text/xml");
 }
 
 function GetCityCode()
 {
   var Code=document.form1.SetCity.value;
   document.getElementById("HideAction").value=Code;
 }
 
 /*
* 处理服务器返回的响应
* xmlHttp XMLHttpRequst对象
*/
function SendCompleted(xmlHttp) 
{
    var msgText = xmlHttp.responseText;
    //仅当在响应的类型是text/xml，application/xml或以+xml结尾时，这个responseXML才可用。
      document.all.SetCity.length = 0 ;
     tmpcityArray = msgText.split("|");
     if(tmpcityArray.length>1)
     {
      for(j=0;j<tmpcityArray.length;j++)
      {
       //填充 城市 下拉
            var cityname=tmpcityArray[j].split(",");  
           document.all.SetCity.options[document.all.SetCity.length] = new Option(cityname[0],cityname[1]);
       }
       
       }
       else
       {  
          for(j=0;j<tmpcityArray.length;j++)
          {
            var cityname=tmpcityArray[j].split(",");
            var varItem = new Option(cityname[0],cityname[1]);
            document.all.SetCity.options.add(varItem);
          }
     
       }
}

	    $(document).ready(function(){
			$.formValidator.initConfig({formid:"form1",onerror:function(){}});
			$("#txtCompanyCD").formValidator({tipid:"CompanyCDTip",onshow:"请输入公司编码"}).inputValidator({min:1,onerror:"公司编码不能为空,请确认"}).functionValidator({
			   
			}).ajaxValidator({
	            type : "get",
		        url : "../../Handler/SystemManager/Company.ashx",
		        datatype : "CompanyCD",
		        success : function(data){	
                    if( data == "0" )
			        {
                        return true;
			        }
                    else
			        {
                        return false;
			        }
		        },
		        onerror : "该公司编码已经存在，请更换公司编码",
		        onwait : "正在对公司编码进行合法性校验，请稍候..."
			});

    $("#txtCompanyCn").formValidator({tipid:"CompanyCnTip",onshow:"请输入公司中文名称",onfocus:"公司中文名称不能为空"}).inputValidator({min:1,onerror:"企业中文名称不能为空,请确认"});
    
    	$("#txtCompanyEn").formValidator({tipid:"CompanyEnTip",onfocus:"必须由英文字母组成"}).functionValidator({
			    fun:function(val,elem){
			        var letterOrNumber = /[a-zA-Z]/.test(val);
			        if (!letterOrNumber){
			            return "请不要输入数字。";
			        }
		
			        return true;
			    }	});
			    
			 $("#SetPro").formValidator({tipid:"ProvCDTip",onshow:"请选择所属省份",onfocus:"所属省份必须选择"}).inputValidator({min:1,onerror: "所属省份不能为空,请确认"});
    
    		//$("#SetCity").formValidator({tipid:"CityCDTip",onshow:"请选择所属地市",onfocus:"所属地市必须选择"}).inputValidator({min:1,onerror: "所属地市不能为空,请确认"});
	        $("#txtTel").formValidator({tipid:"txtTelTip",onshow:"请输入联系电话",onfocus:"格式例如：0551-8888888"}).inputValidator({min:1,onerror: "联系电话不能为空,请确认"}).regexValidator({regexp:"^[[0-9]{3}-|\[0-9]{4}-]?([0-9]{8}|[0-9]{7})?$",onerror:"联系电话格式不正确"});
	        $("#txtFax").formValidator({tipid:"txtFaxTip",onshow:"请输入传真",onfocus:"格式例如：0551-8888888"}).inputValidator({min:1,onerror: "传真不能为空,请确认"}).regexValidator({regexp:"^[[0-9]{3}-|\[0-9]{4}-]?([0-9]{8}|[0-9]{7})?$",onerror:"传真格式不正确"});
    	    $("#txtPost").formValidator({tipid:"txtPostTip",onshow:"请输入邮编",onfocus:"6位数字组成的"}).inputValidator({min:1,onerror: "邮编不能为空,请确认"}).regexValidator({regexp:"zipcode",datatype:"enum",onerror:"邮编格式不正确"});
    	    
    	    
    	 $("#txtMail").formValidator({tipid:"txtMailTip",onshow:" ",onfocus:"请注意你输入的email格式，例如:wzmaodong@126.com",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
                if (val.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)!= -1)
                {
                   //return "Email输入正确";
                }
                else
                 return "Email输入不正确";  
	        }
	        return true;
	    }}); 
	    
	    
	     $("#txtHomePage").formValidator({tipid:"HomePageTip",onshow:" ",onfocus:"请你输入正确的网址格式，例如:www.163.com",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
	        var Expression=/http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/; 
               var objExp=new RegExp(Expression);
              if(!objExp.test(val))
              {
                 return "网址输入不正确"; 
              }
//              else
//                  return "网址输入正确";  
	        }
	        return true;
	    }}); 
	    
	    
	        
	     $("#txtQQ").formValidator({tipid:"QQTip",onshow:" ",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
	          var Expression=/^[1-9]*[1-9][0-9]*$/; 
               var objExp=new RegExp(Expression);
              if(!objExp.test(val))
              {
                  return "QQ输入不正确"; 
              }
//              else
//                  return "QQ输入正确";  
	        }
	        return true;
	    }});
	    
	       	 $("#txtMSN").formValidator({tipid:"MSNTip",onshow:" ",onfocus:"请输入正确的格式，例如:wzmaodong@126.com",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
                if (val.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)!= -1)
                {
                  // return "MSN输入正确";
                }
                else
                 return "MSN输入不正确";  
	        }
	        return true;
	    }})
	      
			 $("#txtContact").formValidator({tipid:"ContactTip",onshow:" ",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
	          var Expression= /[a-zA-Z]/; 
               var objExp=new RegExp(Expression);
              if(!objExp.test(val))
              {
               return "联系人输入不正确"; 
              }
             // else
                 //return "联系人输入正确";  
	        }
	        return true;
	    }});
	        
 $("#txtStaff").formValidator({tipid:"StaffTip",onshow:"请输入全体人员数",onfocus:"必须为正整数"}).inputValidator({min:1,onerror: "全体人员数不能为空,请确认"}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"格式不正确"});


 $("#txtSale").formValidator({tipid:"SaleTip",onshow:" ",oncorrect:" "}).functionValidator({
	         fun:function(val){
	        if (val!="")
	        {
	          var Expression=/^\d+(\.\d+)?$/; 
               var objExp=new RegExp(Expression);
              if(!objExp.test(val))
              {
               return "年销售额输入不正确"; 
              }
             // else
                 //return "联系人输入正确";  
	        }
	        return true;
	    }});
	    
	
    	
  //  	    
    	    
    	    
    		});	
    </script>
    <style type="text/css">
        #SetPro
        {
            width: 76px;
        }
        #SetCity
        {
            width: 76px;
        }
    </style>
</head>
<body>
    <form id="form1" name="form1" runat="server">
  <div class="divbox" style="width:700px;">
    <div class="divboxtitle"><span>企业信息</span><div class="clearbox"></div></div>
    <div class="divboxbody">
    <div class="divboxbodyleft">
            <ul>
                <li id="OneColumnName">企业编码</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtCompanyCD"  MaxLength="8" runat="server" Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="CompanyCDTip" runat="server"></div>
                </li>
             </ul>
             <ul>
                <li id="OneColumnName">企业中文名</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtCompanyCn"  MaxLength="100" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="CompanyCnTip" runat="server"></div>
                </li>
             </ul>
             
             
             <ul >
                <li id="OneColumnName">企业英文名<li id="OneColumnInput">
                    <asp:TextBox ID="txtCompanyEn"  MaxLength="100" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="CompanyEnTip" runat="server"></div>
                </li>
             </ul>
             <ul >
             
             
                <li id="OneColumnName">所属省份</li>
                <li id="OneColumnInput">
    <select id="SetPro" name="SetPro" runat="server" onchange="javascript:GetCity();">
        <option>
     
        </option>
    </select></li>
                <li id="CommonMessage">
                    <div id="ProvCDTip" runat="server"></div>
                </li>
             </ul>
             <ul>
                <li id="OneColumnName">所属地市</li>
                <li id="OneColumnInput">
    <select id="SetCity" name="SetCity"  runat="server" onchange="javascript:GetCityCode();">
        <option ></option>
    </select></li>
                <li id="CommonMessage">
                    <div id="CityCDTip" runat="server"></div>
                </li>
              </ul>
            <ul>
            
                <ul>
                <li id="OneColumnName">联系电话</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtTel" MaxLength="12" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="txtTelTip" runat="server"></div>
                </li>
              </ul>
              
              <ul>
                <li id="OneColumnName">传真</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtFax" MaxLength="12" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="txtFaxTip" runat="server"></div>
                </li>
              </ul>
              
              
                <ul>
                <li id="OneColumnName">邮编</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtPost" MaxLength="6" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="txtPostTip" runat="server"></div>
                </li>
              </ul>
              
            
                <ul>
                <li id="OneColumnName">企业网址</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtHomePage" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="HomePageTip" runat="server"></div>
                </li>
              </ul>
            
                <ul>
                <li id="OneColumnName">Email</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMail" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="txtMailTip" runat="server"></div>
                </li>
              </ul>
              
            
              <ul>
                <li id="OneColumnName">QQ号</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtQQ" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="QQTip" runat="server"></div>
                </li>
              </ul>
              
              
               <ul>
                <li id="OneColumnName">MSN号</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtMSN" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="MSNTip" runat="server"></div>
                </li>
              </ul>
              
              
               <ul>
                <li id="OneColumnName">即时通讯</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtIM" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="IMTip" runat="server"></div>
                </li>
              </ul>
              
              
               <ul>
                <li id="OneColumnName">联系人</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtContact" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="ContactTip" runat="server"></div>
                </li>
              </ul>
              
                 
               <ul>
                <li id="OneColumnName">详细地址</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtAddress" MaxLength="100" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="AddressTip" runat="server"></div>
                </li>
              </ul>
              
              
              
                <ul>
                    <li id="OneColumnName">所属行业</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtTradeCD" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="TradeTip" runat="server"></div>
                </li>
              </ul>
              
              
                            
                <ul>
                    <li id="OneColumnName">全体职员数</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtStaff" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="StaffTip" runat="server"></div>
                </li>
              </ul>
              
                                 
                <ul>
                    <li id="OneColumnName">规模</li>
                <li id="OneColumnInput">
                    <asp:DropDownList ID="DrpSize" runat="server" >
                        <asp:ListItem Value="1">大型</asp:ListItem>
                        <asp:ListItem Value="2">中型</asp:ListItem>
                        <asp:ListItem Value="3">小型</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="CommonMessage">
                    <div id="SizeTip" runat="server"></div>
                </li>
              </ul>
              
              
                 <ul>
                     <li id="OneColumnName">年产量</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtProduction" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="ProductionTip" runat="server"></div>
                </li>
              </ul>
              
              
                 <ul>
                <li id="OneColumnName">年销售额</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtSale" MaxLength="8" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="SaleTip" runat="server"></div>
                </li>
              </ul>
              
              
                  <ul>
                      <li id="OneColumnName">企业信誉</li>
                <li id="OneColumnInput">
                    <asp:TextBox ID="txtCredit" MaxLength="50" runat="server"  Width="115px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="CreditTip" runat="server"></div>
                </li>
              </ul>
              
              
            
            <ul>
            
                <li id="OneColumnName">备注<li id="OneColumnInput">
                    <asp:TextBox ID="txtRemark" MaxLength="8"  runat="server"  Width="136px" 
                        Height="25px"></asp:TextBox>
                </li>
                <li id="CommonMessage">
                    <div id="RemarkTip" runat="server"></div>
                </li>
            </ul>
            
            <div id="BtnArea">
                <asp:ImageButton ID="btnModify" runat="server"  
                    ImageUrl="~/Images/Button/Button_confirm.jpg" onclick="btnModify_Click" />
                <asp:ImageButton ID="btnBack" runat="server"  
                    ImageUrl="~/Images/button/button_Back.jpg"   OnClientClick="history.go(-1);"/>
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
    </div>
    </div>
</div>
  &nbsp;&nbsp;&nbsp;
    <p>
      <input id="HideAction" type="hidden" runat="server" />
  </p>

    </form>
  </body>
</html>
