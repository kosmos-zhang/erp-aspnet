/*
 插件名称：jPagerBar
 主要功能：配合AJAX/JSON等响应方式及数据格式，自动生成页码栏。可以在同一网页中重复使用，互不影响。
               API包括：
               页码栏容器ID、GET请求URL
               以及：页码栏样式、记录总数、每页显示记录数、显示相邻页码数量阀值、当前页、onclick事件、
               页码栏定位标签、无记录提示、“上一页”“下一页”按钮的表现文字（或HTML）。
 当前版本号：1.1.1
 发布日期：2008/2/22

         作者：TNT2 (SZW on cnblogs  - szw.cnblogs.com)  QQ：63408537（加位好友请说明来意）   Email/MSN:szw2003@163.com    www.56MAX.com
         
版权及相关说明：
1、作者对此插件保留所有权利。本插件本着开源、交流、共同进步的宗旨，以免费形式为大家无偿提供。修改、引用请保留以上说明信息，否则将视同为主动盗用本插件。
2、为保证本插件的完整性、安全性和版本统一性，谢绝任何单位和个人将此插件代码修改后以个人名义或“jPagerBar”及类似名称发布，一旦发现，作者将不遗余进行力地进行追查、打击、曝光
3、作者对此插件保留最终解释权。
         
         
如有任何问题或意见、建议，欢迎与作者取得联系！让我们共同进步！
======================================================================================================================
*/

function ShowPageBar(containerId , url , attr)
{

    var style = (attr["style"] == null)? "technorati" : attr["style"];//class样式
    var totalCount =( attr["totalCount"]==null ||  attr["totalCount"] == 0) ? 0 :  attr["totalCount"];;//parseInt()//总记录条数
    var pageCount = (attr["pageCount"] == null || attr["pageCount"] == 0) ? 20 : attr["pageCount"];//attr["pageCount"];//每页记录数
    var showPageNumber = (attr["showPageNumber"] == null || attr["showPageNumber"] == 0) ? 20 : attr["showPageNumber"];//attr["showPageNumber"];//显示页码数量
    var currentPageIndex = attr["currentPageIndex"];//当前页
    var onclick = attr["onclick"];//onclick参数，如果包含“return false”，则连接转为跳到barMark（暂留接口，其实return false后一般情况下href将失效。）+
    var barMark = attr["mark"];//onclick后跳转到的<a name="barMark"></a>标签
    var noRecordTip = attr["noRecordTip"];//没有记录提示（支持HTML）
    var preWord = (attr["preWord"] == null)? " < " : attr["preWord"];//上一条记录文字，默认为“ < ”
    var nextWord = (attr["nextWord"] == null)? " > " : attr["nextWord"];//下一条记录文字，默认为“ > ”
    var First=(attr["First"] == null)? " < " : attr["First"];//首页记录文字，默认为“ < ”
    var End=(attr["End"] == null)? " < " : attr["End"];//末页条记录文字，默认为“ < ”
    
   
    //输出设置
    var barID = containerId + "_pageBar";
    //var barDiv = $("#"+barID);
    //添加PageBar层
    $("#"+containerId).html("<div id=\"" + barID + "\" class=\"" + style + "\"></div>");
    //输出设置 结束
    
    //如果没有记录，返回空记录提示
    if(totalCount==0)
    {
        $("#"+barID).html(noRecordTip);
        return false;
    }
    
    pageCount = (pageCount == null || pageCount == 0) ? 20 : pageCount;//每页显示记录数
    var totalPage = parseInt((totalCount-1) / pageCount) +1;//总页数
    
    showPageNumber = (showPageNumber == null || showPageNumber == 0) ? 3 : showPageNumber;
    currentPageIndex = (currentPageIndex == null || currentPageIndex <= 0 || currentPageIndex > totalPage) ? 1 : currentPageIndex;

    var backPageStyle = (currentPageIndex <= 1) ? "disabled" : "";
    var nextPageStyle = (currentPageIndex >= totalPage) ? "disabled" : "";

    var firstDisplayPageEnd = 0;//从第1页显示到xx页
    var bodyDisplayPageStart = 0;//当前页临近最左页码
    var bodyDisplayPageEnd = 0;//当前页临近最右页码
    var endDisplayPageStart = 0;//从第xx页显示到最后一页

    //设定 bodyDisplayPageStart
    bodyDisplayPageStart = (currentPageIndex - showPageNumber <= 1) ? 1 : currentPageIndex - showPageNumber; // (ViewData.pageIndex - ViewData.showPageNumber <= ViewData.showPageNumber) ? ViewData.showPageNumber + 1 : ViewData.pageIndex - ViewData.showPageNumber;

    //设定 bodyDisplayPageEnd
    bodyDisplayPageEnd = (currentPageIndex + showPageNumber >= totalPage) ? totalPage : currentPageIndex + showPageNumber;


    //设定 firstDisplayPageEnd
    if(bodyDisplayPageStart > 1)
    {
        if(bodyDisplayPageStart - showPageNumber <= 1)
            firstDisplayPageEnd = bodyDisplayPageStart - 1;
        else
            firstDisplayPageEnd = showPageNumber;
    }
    else
    {
        firstDisplayPageEnd = 0;
    }
    
    //设定 endDisplayPageStart
    if(bodyDisplayPageEnd < totalPage)
    {
        if(bodyDisplayPageEnd + showPageNumber >= totalPage)
            endDisplayPageStart = bodyDisplayPageEnd + 1;
        else
            endDisplayPageStart = totalPage - showPageNumber + 1;
    }
    else
    {
        endDisplayPageStart = totalPage + 1;
    }
    
    
/********  备用算法 Start  ********/

//    //设定 firstDisplayPageEnd
//    if (currentPageIndex - showPageNumber > 0 && bodyDisplayPageStart > currentPageIndex - showPageNumber)
//        firstDisplayPageEnd = (showPageNumber >= totalPage) ? 0 : showPageNumber;
//    else
//        firstDisplayPageEnd = 0; 

//    //设定 endDisplayPageStart
//    if (bodyDisplayPageEnd < totalPage)
//        endDisplayPageStart = (bodyDisplayPageEnd + showPageNumber < totalPage) ?  totalPage- showPageNumber + 1 : totalPage+1;
//    else
//        endDisplayPageStart = totalPage+1;
//    
//    //alert(bodyDisplayPageEnd +"<" +totalCount +"- "+showPageNumber);
//    ////设定补充首尾
//    if(bodyDisplayPageStart > 1 && firstDisplayPageEnd == 0)
//        firstDisplayPageEnd = (bodyDisplayPageStart > showPageNumber)? showPageNumber : bodyDisplayPageStart - 1;
//    if(bodyDisplayPageEnd < totalPage && endDisplayPageStart > totalPage)
//        endDisplayPageStart = (bodyDisplayPageEnd < totalPage - showPageNumber)? totalCount - showPageNumber + 1 : bodyDisplayPageEnd + 1;//MS第一个判断有点多余    TNT2
/********  备用算法 End  ********/

    //页面参数设定结束

    //开始输出
     //alert($("#"+barID).html());

        // 首页
    if(currentPageIndex <= 1)
        $("<span class=\"" + backPageStyle + "\">" + First + "</span>").appendTo($("#"+barID));
    else
        $(GetPageLink(1,currentPageIndex,First,onclick,url,barMark)).appendTo($("#"+barID));

    // 上一条
    if(currentPageIndex <= 1)
        $("<span class=\"" + backPageStyle + "\">" + preWord + "</span>").appendTo($("#"+barID));
    else
        $(GetPageLink(currentPageIndex-1,currentPageIndex,preWord,onclick,url,barMark)).appendTo($("#"+barID));

    //first
    //for (var i = 1; i <= firstDisplayPageEnd; i++)
       // $(GetPageLink(i,currentPageIndex,i,onclick,url,barMark)).appendTo($("#"+barID));
        
    //省略号
    //if (firstDisplayPageEnd + 1 < bodyDisplayPageStart)
       // $("<span>... </span>").appendTo($("#"+barID));
        
    //body
    //for (var i = bodyDisplayPageStart; i <= bodyDisplayPageEnd; i++)
        //$(GetPageLink(i,currentPageIndex,i,onclick,url,barMark)).appendTo($("#"+barID));
        
    //省略号
   // if (bodyDisplayPageEnd + 1 < endDisplayPageStart)
      //  $("<span>... </span>").appendTo($("#"+barID));
   
    //end
    // for (var i = endDisplayPageStart; i <= totalPage; i++)
      //  $(GetPageLink(i,currentPageIndex,i,onclick,url,barMark)).appendTo($("#"+barID));
        
    // > 
    if(currentPageIndex >= totalPage)
        $("<span class=\"" + nextPageStyle + "\">" + nextWord + "</span>").appendTo($("#"+barID));
    else
        $(GetPageLink(parseInt(currentPageIndex) + 1 ,currentPageIndex,nextWord,onclick,url,barMark)).appendTo($("#"+barID));
    //尾页
    if(currentPageIndex >= totalPage)
        $("<span class=\"" + nextPageStyle + "\">" + End + "</span>").appendTo($("#"+barID));
    else
        $(GetPageLink(totalPage ,currentPageIndex,End,onclick,url,barMark)).appendTo($("#"+barID));
     //alert($("#"+barID).html());
}

//页码标签链接
function GetPageLink(linkPageIndex ,currentPageIndex,text,onclick,url,barMark)
{
    var pageData = "?page=";//string.Format("{0}page=", (Request.QueryString.Count == 0) ? "?" : "&") + "{0}";//页码参数
    
    onclick = (onclick != null)? "onclick=\"" + onclick + "\"" : "";
    
    onclick = onclick.replace("{pageindex}",linkPageIndex);
    href = (onclick != null && onclick.indexOf("return false") != -1)?"href=\"#" + barMark + "\" ":"href=\"" + url + pageData + linkPageIndex + "\" ";

    var linkHTML = "";
    
    if(linkPageIndex == currentPageIndex)
        linkHTML =  "<span class=\"current\">" + text + "</span>";
    else
        linkHTML = "<a " + href + onclick + ">" + text + "</a>";
          
    return linkHTML;
}