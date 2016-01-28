/* 页面初期显示 */
$(document).ready(function(){
      InitPageInfo();
});

/*
* 获取机构岗位树
*/
function InitPageInfo()
{
    //执行查询数据
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptQuarterEmployee_Query.ashx",
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
//            alert(data);
//            
//            document .getElementById ("Text1").value=data;
            //设置组织机构图
            document.getElementById("divDetailInfo").innerHTML = data;
        } 
    });  
}