/**********************************************
* 类作用：   删除指定文件(附件)
* 建立人：   lysong
* 建立时间： 2009-7-15 
* All rights reserved
***********************************************/
function DeleteUploadFile(FilePath, FileName) {

    var action = "DeleteFile";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Common/DeleteFile.ashx",
        data: 'FilePath=' + escape(FilePath) +
              '&FileName=' + escape(FileName) + '&action=' + escape(action),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        // complete :function(){hidePopup();},
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.sta == "1") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "附件删除成功！");
                try {
                    //设置附件路径
                    document.getElementById("hfPageAttachment").value = "";
                    //下载删除不显示
                    document.getElementById("divDealAttachment").style.display = "none";
                    //上传显示
                    document.getElementById("divUploadAttachment").style.display = "block";
                }
                catch (e) { }
                try {

                    //库存盘点就是个性，需要我xhm来改。。真麻烦，靠2010-02-23
                    UpLoadFileUrl = "";
                    var objActive = document.getElementById("tdUpLoadFile");
                    objActive.innerHTML = "<a href=\"javascript:ShowUploadFile();\" href=\"javascript:void(0);\">上传附件</>";

                }
                catch (e) { }
                return "1"; //删除成功
            }
            else if (data.sta = "0") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "附件删除失败！");
                return "0"; //删除失败
            }
            else if (data.sta = "2") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "文件已不存在！");
                return "2"; //文件不存在
            }
        }
    });
}