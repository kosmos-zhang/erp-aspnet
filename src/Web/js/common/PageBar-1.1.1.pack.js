/*
 插件名称：jPagerBar
 主要功能：配合AJAX/JSON等响应方式及数据格式，自动生成页码栏。可以在同一网页中重复使用，互不影响。
               API包括：
               页码栏容器ID、GET请求URL
               以及：页码栏样式、记录总数、每页显示记录数、显示相邻页码数量阀值、当前页、onclick事件、
               页码栏定位标签、无记录提示、“上一页”“下一页”按钮的表现文字（或HTML）。
 当前版本号：1.1.1
 发布日期：2008/2/22

         作者：TNT2 (SZW on cnblogs)  QQ：63408537（加位好友请说明来意）   Email:szw2003@163.com    www.56MAX.com
         
版权及相关说明：
1、作者对此插件保留所有权利。本插件本着开源、交流、共同进步的宗旨，以免费形式为大家无偿提供。修改、引用请保留以上说明信息，否则将视同为主动盗用本插件。
2、为保证本插件的完整性、安全性和版本统一性，谢绝任何单位和个人将此插件代码修改后以个人名义或“jPagerBar”及类似名称发布，一旦发现，作者将不遗余进行力地进行追查、打击、曝光
3、作者对此插件保留最终解释权。
         
         
如有任何问题或意见、建议，欢迎与作者取得联系！让我们共同进步！
======================================================================================================================
*/
eval(function(p,a,c,k,e,d){e=function(c){return(c<a?"":e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--){d[e(c)]=k[c]||e(c)}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('K V(A,h,5){4 v=(5["v"]==b)?"L":5["v"];4 m=(5["m"]==b||5["m"]==0)?0:5["m"];4 e=(5["e"]==b||5["e"]==0)?y:5["e"];4 7=(5["7"]==b||5["7"]==0)?y:5["7"];4 2=5["2"];4 6=5["6"];4 g=5["M"];4 x=5["x"];4 r=(5["r"]==b)?" < ":5["r"];4 q=(5["q"]==b)?" > ":5["q"];4 8=A+"N";$("#"+A).F("<E O=\\""+8+"\\" u=\\""+v+"\\"></E>");c(m==0){$("#"+8).F(x);B I}e=(e==b||e==0)?y:e;4 9=R((m-1)/e)+1; 7=(7==b||7==0)?3:7;2=(2==b||2<=0||2>9)?1:2;4 H=(2<=1)?"G":"";4 D=(2>=9)?"G":"";4 o=0;4 j=0;4 k=0;4 p=0;j=(2-7<=1)?1:2-7;k=(2+7>=9)?9:2+7;c(j>1){c(j-7<=1)o=j-1;l o=7}l{o=0}c(k<9){c(k+7>=9)p=k+1;l p=9-7+1}l{p=9+1}c(2<=1)$("<d u=\\""+H+"\\">"+r+"</d>").f($("#"+8));l $(n(2-1,2,r,6,h,g)).f($("#"+8));C(4 i=1;i<=o;i++)$(n(i,2,i,6,h,g)).f($("#"+8));c(o+1<j)$("<d>... </d>").f($("#"+8));C(4 i=j;i<=k;i++)$(n(i,2,i,6,h,g)).f($("#"+8));c(k+1<p)$("<d>... </d>").f($("#"+8));C(4 i=p;i<=9;i++)$(n(i,2,i,6,h,g)).f($("#"+8));c(2>=9)$("<d u=\\""+D+"\\">"+q+"</d>").f($("#"+8));l $(n(2+1,2,q,6,h,g)).f($("#"+8))}K n(t,2,z,6,h,g){4 J="?P=";6=(6!=b)?"6=\\""+6+"\\"":"";6=6.Q("{S}",t);s=(6!=b&&6.T("B I")!=-1)?"s=\\"#"+g+"\\" ":"s=\\""+h+J+t+"\\" ";4 w="";c(t==2)w="<d u=\\"U\\">"+z+"</d>";l w="<a "+s+6+">"+z+"</a>";B w}',58,58,'||currentPageIndex||var|attr|onclick|showPageNumber|barID|totalPage||null|if|span|pageCount|appendTo|barMark|url||bodyDisplayPageStart|bodyDisplayPageEnd|else|totalCount|GetPageLink|firstDisplayPageEnd|endDisplayPageStart|nextWord|preWord|href|linkPageIndex|class|style|linkHTML|noRecordTip|20|text|containerId|return|for|nextPageStyle|div|html|disabled|backPageStyle|false|pageData|function|technorati|mark|_pageBar|id|page|replace|parseInt|pageindex|indexOf|current|ShowPageBar'.split('|'),0,{}))
