<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPerformanceQuery.aspx.cs" Inherits="Pages_Office_HumanManager_PrintPerformanceQuery" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>绩效查询报表</title>
     <style type="text/css">
        @media print
        {
            .onlyShow
            {
                display: none;
            }
            .onlyPrint
            {
                page-break-before: always;
            }
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            border: 0px;
        }
        .noprint2
        {
            display: none;
        }
        td
        {
            height: 20px;
            overflow: hidden;
        }
        .printtable
        {
            table-layout: fixed;
        }
        .printplace
        {
            word-wrap: normal;
            width: 100px;
            height: 20px;
        }
        .printstation
        {
            width: 100px;
            overflow: hidden;
            margin-top: 0px;
        }
        .leftplace
        {
            text-align: left;
            width: 61px;
        }
        .sta
        {
            height: 20px;
            width: 80px;
            clear: both;
            overflow: hidden;
            margin-top: 0px;
        }
        .placewidth
        {
            width: 80px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function prepare() {
            preView();
        }
        function pageSetup() {

            try {
                if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
                    var hkey_key;
                    var hkey_root = "HKEY_CURRENT_USER";
                    var hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
                    //设置网页打印的页眉页脚为空
                    var RegWsh = new ActiveXObject("WScript.Shell");
                    hkey_key = "header";
                    RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
                    hkey_key = "footer";
                    var Creator = document.getElementById("HiddenCreator").value;
                    RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "制表人:" + Creator + " &b第&p页,共&P页&b 打印日期:&d  ");
                }
                else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
                    alert("您的浏览器为FireFox！");
                }
                window.print();
            }
            catch (e) {
                alert("您的浏览器不支持此功能,请选择：文件→打印(P)…")
            }
        }

        function preView() {
            try {
                if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
                    var hkey_key;
                    var hkey_root = "HKEY_CURRENT_USER";
                    var hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
                    //设置网页打印的页眉页脚为空
                    var RegWsh = new ActiveXObject("WScript.Shell");
                    hkey_key = "header";
                    RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
                    hkey_key = "footer";
                    var Creator = document.getElementById("HiddenCreator").value;
                    RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "制表人:" + Creator + " &b第&p页,共&P页&b 打印日期:&d  ");
                }
                else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
                    alert("您的浏览器为FireFox！");
                }
                WB.ExecWB(7, 1)
            } catch (e) { alert("您的浏览器不支持此功能，请选择：文件→打印预览(V)") }
        }
    </script>

    <style type="text/css">
        body
        {
            font-size: 13px;
        }
        .botton02
        {
            font-size: 12px;
            color: #000000;
            background-image: url(../images/but02.gif);
            height: 21px;
            width: 58px;
            border: none;
        }
        /*两个字的按钮*/.td
        {
            font-size: 15px;
            font-weight: normal;
        }
        .td1
        {
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            font-size: 15px;
        }
        .td2
        {
            border: 1px solid #000;
        }
        .td3
        {
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            font-size: 15px;
        }
        .td4
        {
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-right: 1px solid #000;
            font-size: 15px;
        }
        .td5
        {
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-right: 1px solid #000;
            border-bottom: 1px solid #000;
            font-size: 15px;
        }
        .font1
        {
            font-size: 10px;
        }
        .mg
        {
            padding-top: 1px;
        }
        .pS
        {
            font-size: 22px;
            font-family: "楷体_GB2312";
            height: 60px;
        }
    </style>

</head>
<body>
 <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WB" width="0"></object>
    <form id="form1" runat="server">
    <div>
     <span class="noprint2"  style="text-align:center; margin-right:50px; margin-top:4px; width:100%;">
      <input id="linesPerPage" type="hidden"  value="30" runat="server" />
      <img src="../../../images/Button/Main_btn_print.jpg" onclick="pageSetup()"  runat="server" id="print"/>
       <img src="../../../Images/Button/Main_btn_dyyl.jpg" onclick="prepare()"  runat="server" id="printview" />
      <asp:ImageButton  ID="btnOutWord" runat="server" OnClick="btnOutWord_Click" ImageUrl="~/Images/Button/Main_btn_out.jpg" />
   <input id="HiddenCurryTypeID" type="hidden" runat="server" />
     </span>
     <asp:Label ID="Label2" runat="server"></asp:Label>
     <asp:Label ID="Label1" runat="server"></asp:Label>
     <input id="HiddenCreator" value="" type="hidden" runat="server" />
    </div>
    </form>
</body>
</html>
