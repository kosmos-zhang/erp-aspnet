var popMsgObj = new Object();
var popMsgObj1 = new Object();
popMsgObj1.content = "";
popMsgObj.content = "";
//popMsgObj.content ="<strong>[</strong><font color=\"red\">设备编号</font><strong>]</strong>：设备编号不可为空!<br /><strong>[</strong><font color=\"red\">购入日期</font><strong>]</strong>：日期格式不正确!";

//调用方法一(字段,提示信息)
popMsgObj.Show = function(msgField, msgAlert) {

    CenterToDocument('mydiv', false);
    openRotoscopingDiv(false, 'divMsgShadow', 'MsgShadowIframe');
    popMsgObj.content = "";
    if (msgField != null && msgField != "" && msgAlert != null && msgAlert != "") {
        var fieldArray = msgField.split('|');
        var alertArray = msgAlert.split('|');
        for (var i = 0; i < fieldArray.length - 1; i++) {
            popMsgObj.content = popMsgObj.content + "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString() + "</font><strong>]</strong>：<span style=\"word-wrap:break-word;\">" + alertArray[i].toString() + "</span><br />";
        }
    }
    document.getElementById('mydiv').innerHTML = "<table width=\"310\" border=\"0\" cellspacing=\"10\" cellpadding=\"0\" ><tr><td width=\"310\">" + popMsgObj.content + "</td> </tr></table><table width=\"280\"><tr><td align=\"right\"><img src=\"../../../Images/Button/closelabel.gif\" onclick=\"document.getElementById('mydiv').style.display='none';closeRotoscopingDiv(false,'divMsgShadow');\" /></td></tr></table>";
    document.getElementById('mydiv').style.display = 'block';
}
//调用方法二(全部提示信息)
popMsgObj.ShowMsg = function(msgInfo) {
    CenterToDocument('mydiv', false);
    openRotoscopingDiv(false, 'divMsgShadow', 'MsgShadowIframe');
    popMsgObj.content = "";
    if (msgInfo != null) {
        popMsgObj.content = msgInfo;
    }
    document.getElementById('mydiv').innerHTML = "<table width=\"310\" border=\"0\" cellspacing=\"10\" cellpadding=\"0\" ><tr><td width=\"290\" style=\"word-wrap:break-word;\">" + popMsgObj.content + "</td> </tr></table><table width=\"200\"><tr><td align=\"right\"><img src=\"../../../Images/Button/closelabel.gif\" onclick=\"document.getElementById('mydiv').style.display='none';closeRotoscopingDiv(false,'divMsgShadow');\" /></td></tr></table>";
    document.getElementById('mydiv').style.display = 'block';
}
//调用方法三(字段,提示信息)
popMsgObj1.Show = function(msgField, msgAlert) {
    CenterToDocument('mydiv', false);
    openRotoscopingDiv(false, 'divMsgShadow', 'MsgShadowIframe');
    popMsgObj1.content = "";
    if (msgField != null && msgField != "" && msgAlert != null && msgAlert != "") {
        var fieldArray = msgField.split('|');
        var alertArray = msgAlert.split('|');
        for (var i = 0; i < fieldArray.length - 1; i++) {
            popMsgObj1.content = popMsgObj1.content + "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString() + "</font><strong>]</strong>：<span style=\"word-wrap:break-word;\">" + alertArray[i].toString() + "</span><br />";
        }
    }
    document.getElementById('mydiv').innerHTML = "<table width=\"310\" border=\"0\" cellspacing=\"10\" cellpadding=\"0\"><tr><td >" + popMsgObj1.content + "</td></tr></table><table width=\"310\"><tr><td align=\"center\" style=\" width:155px;\"><img  alt=\"确定\" onclick=\"fnMsgConfim()\" src=\"../../../Images/Button/surelabel.gif\" /></td><td align=\"center\" style=\" width:155px;\"><img alt=\"取消\" src=\"../../../Images/Button/cancellabel.gif\" onclick=\"document.getElementById('mydiv').style.display='none';closeRotoscopingDiv(false,'divMsgShadow');\" /></td></tr></table>";
    document.getElementById('mydiv').style.display = 'block';
}