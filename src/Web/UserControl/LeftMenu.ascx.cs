using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;



public partial class UserControl_LeftMenu : System.Web.UI.UserControl
{
    private LeftMenuNode _rootNode;
    public LeftMenuNode RootNode
    {
        get{
            return _rootNode;
        }
        set{
            _rootNode = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void BuildNodes()
    {
        StringBuilder sb = new StringBuilder();
        foreach (LeftMenuNode node in RootNode.SubNodes)
        {
            BuildSubNodes(node, sb);
        }

        this.Controls.Add(new LiteralControl(sb.ToString()));
    }

    protected void BuildSubNodes(LeftMenuNode node,StringBuilder sb)
    { 
        DoNode(node, sb);

        foreach (LeftMenuNode node2 in node.SubNodes)
        {
            BuildSubNodes(node2,sb);
        }

        if (node.SubNodes.Count > 0)
        {
            sb.Append("</td></tr></table>");
        }
    }

    protected void DoNode(LeftMenuNode node,StringBuilder sb)
    {
        sb.Append("<table ");
        
        if(node.Level == 0)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"0\"");
        }
        if (node.Level == 1)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"0\"");
        }
        if (node.Level >= 2)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"1\"");
        }


        sb.Append("border=0><tr><td style=\"cursor:pointer;\" level=\"" + node.Level.ToString() + "\" onmouseover=\"highLight(event,0);\"  onmouseout=\"highLight(event,1);\" ");
        if (node.SubNodes.Count > 0)
        {
            if (node.Level == 0)
            {
                sb.Append(" onclick=\"expandSubMenu(event,'"+node.NodeUrl+"');\" ");
                node.NodeUrl = "";
            }
            else
            {
                sb.Append(" onclick=\"expandSubMenu(event);\" ");
            }
        }
               

        sb.Append(">");

        if (node.NodeIcon != string.Empty)
        {
            sb.Append("<img src=\"" + node.NodeIcon + "\">");
        }


        if (node.NodeUrl != string.Empty)
        {
            string url = node.NodeUrl;
            if (url.IndexOf("?") == -1)
            {
                url += "?ModuleID=" + node.Data;
            }
            else {
                url += "&ModuleID=" + node.Data;
            }

            sb.Append("<a target=\"Main\" href=\"" + url + "\">" + node.NodeName + "</a>");
        }
        else
        {
            sb.Append(node.NodeName);
        }

        if (node.Level == 0)
        {
            sb.Append("<img src=\"images/left_frame/Arrow_open.jpg\">");
        }
        
        sb.Append("</td></tr>");

        if (node.SubNodes.Count > 0)
        {
            int paddingLeft = 30;
            sb.Append("<tr><td style=\"display:none;padding-left:" + paddingLeft.ToString() + "px;\">");

        }
        else {
            sb.AppendLine("</table>");
        }
    }
}
