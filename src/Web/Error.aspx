<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="XBase.WebSite.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link rel="stylesheet" type="text/css" href="css/default.css" />
</head>
<body id="logpage" class="page">
  <!--body end-->
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td style="background-image: url('Images/login/bg_top.jpg')">
        <img src="Images/login/bg_top.jpg" alt="" width="29" height="78" /></td>
    </tr>
  </table>
  <form id="form1" runat="server">
    <table width="795" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="135" style="background-image: url('Images/login/bar_bg.jpg')">
                <img src="Images/login/logo.jpg" alt="" width="129" height="45" /></td>
              <td align="right" style="background-image: url('Images/login/bar_bg.jpg')" class="white">欢迎您使用进销存系统，更多产品描述和帮助信息请参考产品服务网站</td>
              <td width="23" align="right" style="background-image: url('Images/login/bar_bg.jpg')">
                <img src="Images/login/bar_right.jpg" alt="" width="23" height="45" /></td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td>
          <img src="Images/error.jpg" alt="" border="0" usemap="#MapMapMap" />
          <map name="MapMapMap" id="MapMapMap">
            <area shape="rect" coords="412,233,466,269" alt="" target="_parent" />
            <area shape="rect" coords="357,233,411,269" alt="" target="_parent" />
          </map>
          <map name="MapMap" id="MapMap">
            <area shape="rect" coords="412,233,466,269" alt="" target="_parent" />
            <area shape="rect" coords="357,233,411,269" alt="" target="_parent" />
          </map>
        </td>
      </tr>
      <tr>
        <td>
          <asp:Label ForeColor="Red" ID="lblErrorInfo" runat="server" Text=""></asp:Label>
          <br />
          <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr>
              <td width="300" height="40" valign="top" class="white"></td>
              <td align="right" valign="top" class="white"></td>
            </tr>
            <tr>
              <td height="1" colspan="2" bgcolor="#CCCCCC" class="white"></td>
            </tr>
            <tr>
              <td height="30" class="white"></td>
              <td width="450" align="right" class="white">推荐使用IE 7.0浏览器，建议1280 X 800及以上分辨率 </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <map name="Map" id="Map">
      <area shape="rect" coords="277,209,331,245" alt="" target="_parent" />
      <area shape="rect" coords="332,209,386,245" href="#" alt="" />
    </map>
    <div>
    </div>
  </form>
</body>
</html>
