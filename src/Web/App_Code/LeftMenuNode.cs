using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections;

public class LeftMenuNode
{
    private ArrayList _subNodes = new ArrayList() ;
    public ArrayList SubNodes
    {
        get { return _subNodes; }
        set { _subNodes = value; }
    }

    private string _nodeName;
    public string NodeName
    {
        get { return _nodeName; }
        set { _nodeName = value; }
    }

    private string _nodeUrl;
    public string NodeUrl
    {
        get { return _nodeUrl; }
        set { _nodeUrl = value; }
    }

    private string _nodeIcon;
    public string NodeIcon
    {
        get { return _nodeIcon; }
        set { _nodeIcon = value; }
    }

    private string _data;
    public string Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }


}
