<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Message.ascx.cs" Inherits="UserControl_Message" %>
<div id="divMsgShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="MsgShadowIframe" frameborder="0"
        width="100%"></iframe>
</div>

<script src="../../../js/common/Common.js" type="text/javascript"></script>

<script src="../../../js/common/message.js" type="text/javascript"></script>

<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="mydiv" style="border: solid 10px #898989; background: #fff; padding: 10px;width: 330px; z-index: 1001; position: absolute; display: none;">
    </div>
    <!--提示信息弹出详情end-->
</div>
