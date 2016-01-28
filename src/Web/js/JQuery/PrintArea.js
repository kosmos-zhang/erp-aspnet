/**
 *  Version 2.0
 *      -Contributors: "mindinquiring" : filter to exclude any stylesheet other than print.
 *  Tested ONLY in IE 8 and FF 3.5.3. No official support for other browsers, but will
 *      TRY to accomodate challenges in other browsers.
 *  Example:
 *      Print Button: <div id="print_button">Print</div>
 *      Print Area  : <div class="PrintArea"> ... html ... </div>
 *      Javascript  : <script>
 *                       $("div#print_button").click(function(){
 *                           $("div.PrintArea").printArea( [OPTIONS] );
 *                       });
 *                     </script>
 *  options are passed as json (json example: {mode: "popup", popClose: false})
 *
 *  {OPTIONS} | [type]    | (default), values    | Explanation
 *  --------- | --------- | -------------------- | -----------
 *  @mode     | [string]  | ("iframe"), "popup"  | printable window is either iframe or browser popup
 *  @popHt    | [number]  | (500)                | popup window height
 *  @popWd    | [number]  | (400)                | popup window width
 *  @popX     | [number]  | (500)                | popup window screen X position
 *  @popY     | [number]  | (500)                | popup window screen Y position
 *  @popTitle | [string]  | ('')                 | popup window title element
 *  @popClose | [boolean] | (false), true        | popup window close after printing
 */
 
(function($) {
    var counter = 0;
    var modes = { iframe : "iframe", popup : "popup" };
    var defaults = { mode     : modes.iframe,
                     popHt    : 500,
                     popWd    : 400,
                     popX     : 200,
                     popY     : 200,
                     popTitle : '',
                     popClose : false };

    var settings = {};//global settings

    $.fn.printArea = function( options )
        {
            $.extend( settings, defaults, options );
            
            counter++;
            var idPrefix = "printArea_";
            $( "[id^=" + idPrefix + "]" ).remove();
            var ele = $(this);
           
            settings.id = idPrefix + counter;

            var writeDoc;
            var printWindow;

            switch ( settings.mode )
            {
                case modes.iframe :
                    var f = new Iframe();
                    writeDoc = f.doc;
                    printWindow = f.contentWindow || f;
                    break;
                case modes.popup :
                    printWindow = new Popup();
                    writeDoc = printWindow.doc;
            }
            
            writeDoc.open();
            writeDoc.write("<html>" + getHead() + getBody(ele) + "</html>" );
            writeDoc.close();
            try
            {
                writeDoc.all.WebBrowser.ExecWB(8, 1);
            }catch(E){
                alert("IE浏览器禁止打印！\n解决办法：打开网页浏览器，操作步骤如下\n\菜单栏\"工具\"--->\"Internet选项\"--->\"安全\"--->\"自定义级别\"--->将里面的ActiveX全部设置为启用");
                return false;
            }
            try
            {
                writeDoc.getElementById("printhead").style.display='block';
            }catch(e){};
            printWindow.focus();
            printWindow.print();

            if ( settings.mode == modes.popup && settings.popClose )
                printWindow.close();
        }

    function getHead()
    {
        var head = "<head><title>" + settings.popTitle + "</title>";
        
        head += "</head>";
        return head;
    }

    function getBody( printElement )
    {
        var body = "<body>";
        body += '<div class="' + $(printElement).attr("class") + '"> <OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0 VIEWASTEXT VIEWASTEXT></OBJECT>' + $(printElement).html()+ '</div>';
        body += "</body>";
        return body;
    }

    function Iframe()
    {
        var frameId = settings.id;

        var iframeStyle = 'border:0;position:absolute;width:0px;height:0px;left:0px;top:0px;';
        var iframe;

        try
        {
            iframe = document.createElement('iframe');
            document.body.appendChild(iframe);
            $(iframe).attr({ style: iframeStyle, id: frameId, src: "" });
            iframe.doc = null;
            iframe.doc = iframe.contentDocument ? iframe.contentDocument : ( iframe.contentWindow ? iframe.contentWindow.document : iframe.document);
        }
        catch( e ) { throw e + ". iframes may not be supported in this browser."; }

        if ( iframe.doc == null ) throw "Cannot find document.";

        return iframe;
    }

    function Popup()
    {
        var windowAttr = "location=yes,statusbar=no,directories=no,menubar=no,titlebar=no,toolbar=no,dependent=no";
        windowAttr += ",width=" + settings.popWd + ",height=" + settings.popHt;
        windowAttr += ",resizable=yes,screenX=" + settings.popX + ",screenY=" + settings.popY + ",personalbar=no,scrollbars=no";

        var newWin = window.open( "", "_blank",  windowAttr );

        newWin.doc = newWin.document;

        return newWin;
    }
})(jQuery);
