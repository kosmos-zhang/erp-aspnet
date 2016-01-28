<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewTest.aspx.cs" Inherits="Pages_Common_NewTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script src="../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
/*   function SelectUserOrDept()
   {
 
       var url="../../../Pages/Common/SelectUserOrDept.aspx";
     window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px");
        // var returnValue =
     /*  if(returnValue!="" && returnValue!=null)
       {
          document.getElementById("txtBankName").value="";
          var info=returnValue.split("|");
          document.getElementById("txtBankName").value=info[1].toString();
       }
      
   }
   */
   
   function test()
   {
      var aaa="123,456"
   }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="UserTextBox1" runat="server"  onclick="alertdiv('UserTextBox1,TextBox2,2');"></asp:TextBox>
        
        
        
        
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
