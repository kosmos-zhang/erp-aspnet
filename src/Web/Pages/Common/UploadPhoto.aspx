<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPhoto.aspx.cs" Inherits="Pages_Common_UploadPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>照片上传</title>
    <link rel="stylesheet" type="text/css" href="../../css/default.css" />
    <script type="text/javascript">
        submitFlag = false;
        /* 确定 */
        function DoConfirm()
        {
            fileUrl = document.getElementById("uploadFileUrl").value;
            docName = document.getElementById("uploadDocName").value;
            window.parent.HideUploadPhoto(fileUrl,docName, "1");
        }
        /* 取消 */
        function PhotoCancel()
        {
            window.parent.HideUploadPhoto("", "0");
        }
        /* 校验输入内容 */
        function CheckInput()
        {
            submitFlag = false;
            //获取本地路径
            filePath = document.getElementById("flLocalFile").value;
            //未选择文件时
            if( filePath == "")
            {
                //显示错误信息
                popMsgObj.ShowMsg("请输入本地文件路径");
            }
            else
            {
                submitFlag = true;
            }
        }
        
        var popMsgObj=new Object();
        popMsgObj.content = "";
        
        //调用方法(全部提示信息)
        popMsgObj.ShowMsg = function(msgInfo)
        {
            popMsgObj.content = "";
            if(msgInfo != null)
            {
                popMsgObj.content = msgInfo;
            }
            document.getElementById('mydiv').innerHTML = "<table width=\"290\" border=\"0\" cellspacing=\"10\" cellpadding=\"0\" ><tr><td width=\"290\"><strong><font color=\"red\">" 
            + popMsgObj.content+"</font><strong></td> </tr></table><table width=\"200\"><tr><td align=\"right\"><img src=\"../../../Images/Button/closelabel.gif\" onclick=\"document.getElementById('mydiv').style.display='none';\" /></td></tr></table>" ;
            document.getElementById('mydiv').style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="frmMain" runat="server" onsubmit="return submitFlag;">
        <!--提示信息弹出详情start-->
        <div id="mydiv" style="border:solid 5px #898989; background:#fff; z-index:1001; position:absolute; display:none; top:25%; left:25%; ">
        </div>
        <!--提示信息弹出详情end-->
        <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin-left:5px;margin-top:5px;background-color:#F0f0f0;">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="30" align="center" colspan="2" class="Title">照片上传</td>
            </tr>
            <tr>
                <td height="20" bgcolor="#F4F0ED" class="Blue" colspan="2">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                        <tr>
                            <td>照片信息</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" class="tdColInput" height="10"></td></tr>
            <tr>
                <td class="tdColTitle">本地照片<span class="redbold">*</span></td>
                <td class="tdColInput" >
                    <asp:FileUpload ID="flLocalFile" runat="server" Width="400px" />
                </td>
            </tr>
            <tr><td colspan="2" class="tdColInput" height="10"></td></tr>
            <tr>
                <td class="tdColInput" align="center" colspan="2">
                     <asp:ImageButton ImageUrl="~/Images/Button/Main_btn_upload.jpg" ID="btnUploadPhoto" runat="server" OnClientClick="CheckInput();" onclick="btnUploadPhoto_Click" />
                     <img src="../../Images/Button/Bottom_btn_cancel.jpg" style="cursor:hand" onclick="PhotoCancel();" id="btnCancel" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="uploadFileUrl" runat="server" />
        <input type="hidden" id="uploadDocName" runat="server" />
    </form>
</body>
</html>