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

using XBase.Common;

public partial class Left : System.Web.UI.Page
{


    private int nodeLevel = 0;

    private UserInfoUtil UserInfo = null;
    private DataTable menuInfo = null;
    
    private static Hashtable menuItems = new Hashtable();

    public string ModelName
    {
        get;
        set;
    }
    public string Zxl100Path
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
            {
                case XBase.Common.ConstUtil.Ver_ERP_Guid://生产版
                    ModelName = "执行模式|办公模式|运营模式|决策模式|知识中心|智能表单";
                    Zxl100Path = string.Empty;
                    break;
                default://未匹配到
                    break;
            }
        }



        hidSessionID.Value = Session.SessionID;
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //AuthorityInfo 默认是NULL,用这个属性来判断是否已经加载
        if (UserInfo.AuthorityInfo == null)
        {
            menuInfo = XBase.Business.Common.SafeUtil.InitMenuData(UserInfo.UserID, UserInfo.CompanyCD);

            UserInfo.MenuInfo = menuInfo;
            UserInfo.AuthorityInfo = XBase.Business.Common.SafeUtil.InitPageAuthority(UserInfo.UserID, UserInfo.CompanyCD); 
            SessionUtil.Session["UserInfo"] = UserInfo;
        }
        else
        {
            menuInfo = UserInfo.MenuInfo;

            //添加自定义表单
            DataTable customdt;
            try
            {
                customdt = XBase.Business.DefManager.CustomModuleBus.GetDataTableList();
            }
            catch { customdt = null; }
            if (customdt != null && customdt.Rows.Count > 0)
            {
                for (int i = 0; i < customdt.Rows.Count; i++)
                {
                    DataRow dr = menuInfo.NewRow();
                    dr[0] = customdt.Rows[i]["ModuleID"].ToString();
                    dr[1] = customdt.Rows[i]["ModuleName"].ToString();
                    dr[2] = customdt.Rows[i]["ModuleType"].ToString();
                    dr[3] = customdt.Rows[i]["ParentID"].ToString();
                    dr[4] = customdt.Rows[i]["PropertyType"].ToString();
                    dr[5] = customdt.Rows[i]["PropertyValue"].ToString();
                    dr[6] = "";
                    dr[7] = "";
                    menuInfo.Rows.Add(dr);
                }
            }
             DataView view = menuInfo.DefaultView;
             DataTable tagetTable= view.ToTable(true, "ModuleID", "ModuleName","ParentID","ModuleType","PropertyType","PropertyValue","ImgPath","DefaultPage");
             menuInfo = tagetTable;
        }

        string moduleId = string.Empty;
        if (Request.QueryString["ModuleID"] != null)
        {
            moduleId = Request.QueryString["ModuleID"].ToString().Trim();
        }

        //moduleId = "6";

        //从菜单信息中获取Top信息
        DataRow[] rows = menuInfo.Select("ParentID is null");
        DataRow curR = null;
        if (rows.Length < 1)
        {
            XBase.Common.XgLoger.Log("用户："+UserInfo.UserID+"-顶部菜单信息未取到");
            return;
        }

        string jscode = "var defaultPage=\"\"";

        if (moduleId == string.Empty)
        {            

            moduleId = rows[0]["ModuleID"].ToString();
            curR = rows[0];
        }
        else {
            bool has = false;
            foreach (DataRow row in rows)
            {
                if (row["ModuleID"].ToString().Trim() == moduleId)
                {
                    has = true;
                    curR = row;


                    jscode = "var defaultPage=\"" + curR["defaultPage"].ToString() + "\"";
                }
            }
            if (!has)
            {
                XBase.Common.XgLoger.Log("用户："+UserInfo.UserID+"-当前ModuleID 不在授权列表中");
                return;
            }

        }


        ClientScript.RegisterClientScriptBlock(this.GetType(), "fdf", jscode, true);

            
        if (!this.IsPostBack)
        {
            if (moduleId != string.Empty)
            {
                BindMenuData(moduleId);
            }          
        }
    }

    protected void BindMenuData(string moduleId)
    {
       
        //从用户信息中获取菜单信息
        //DataTable menuInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).MenuInfo;
       
        LeftMenuNode rootNode = new LeftMenuNode();

        DataRow[] rows = menuInfo.Select("ParentID = '" + moduleId + "'");
        foreach (DataRow row in rows)
        {
            if (row["ModuleType"].ToString().ToLower() == "p")
                continue;

            LeftMenuNode node = new LeftMenuNode();
            node.NodeName = row["ModuleName"].ToString();
            node.Data = row["ModuleID"].ToString();
            node.NodeIcon = row["ImgPath"].ToString();
            node.Level = nodeLevel;
            node.NodeUrl = row["DefaultPage"].ToString();

            if (node.NodeIcon != string.Empty)
            {
                node.NodeName = "";
            }

            rootNode.SubNodes.Add(node);
            
            BuildNode(node, menuInfo);            
        }

        if (rootNode.SubNodes.Count == 0)
        {
            XBase.Common.XgLoger.Log("用户：" + UserInfo.UserID + "-没有授权的菜单项");
        }
        this.LeftMenu1.RootNode = rootNode;
        this.LeftMenu1.BuildNodes();
    }

    private void BuildSpecialNode(LeftMenuNode node, string id)
    {
        DataTable dt;

        if (node.Data.Trim() == "105")
        {
            // UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            XBase.Business.Personal.Culture.CultureType bll = new XBase.Business.Personal.Culture.CultureType();
            dt = bll.GetList("CompanyCD='" + UserInfo.CompanyCD + "'");

            nodeLevel--;
            id = "0";
            node.Data = "10590";
        } 
        else
        {
            XBase.Business.KnowledgeCenter.KnowledgeType bll = new XBase.Business.KnowledgeCenter.KnowledgeType();
            dt = bll.Select().Tables[0];
        }

        nodeLevel++;
        foreach (DataRow row in dt.Select("SupperTypeID=" + id))
        {
            BuildSpecialSubNode(node, row, dt, node.Data);
        }
        nodeLevel--;

        if (node.Data.Trim() == "10590")
        {
            nodeLevel++;
        }
    }
    
    private void BuildSpecialSubNode(LeftMenuNode node, DataRow row, DataTable dt, string moduleID)
    {

        LeftMenuNode node2 = new LeftMenuNode();
        node2.NodeName = row["TypeName"].ToString();// +":" + row["ModuleType"].ToString();
        node2.Data = moduleID;
        node2.Level = nodeLevel;
        node2.NodeIcon = string.Empty;
        switch (nodeLevel)
        {
            case 1:
                node2.NodeIcon = "Images/Left_Frame/Main_left_file.jpg";
                break;
            case 2:
                node2.NodeIcon = "Images/Left_Frame/yuan.jpg";
                break;
            default:
                node2.NodeIcon = "Images/Left_Frame/yuan.jpg";
                break;
        }

        if (moduleID.Substring(0, 1) == "1")
        {
            node2.NodeUrl = "Pages/Personal/Culture/Culture.aspx?TypeID=" + row["ID"].ToString();
        }
        node.SubNodes.Add(node2);

        if (nodeLevel >= 3)
            return;

        nodeLevel++;

        foreach (DataRow row2 in dt.Select("SupperTypeID=" + row["ID"].ToString()))
        {
            BuildSpecialSubNode(node2, row2, dt, moduleID);
        }
        nodeLevel--;
    }

    protected void BuildNode(LeftMenuNode node, DataTable dataInfo)
    {
        if (node.NodeUrl.ToLower().IndexOf("viewtypelist") != -1)
        {
            string turl = node.NodeUrl;
            if (turl.IndexOf("?") != -1)
            {
                turl = turl.Substring(turl.LastIndexOf("?")).Split('=')[1];
                BuildSpecialNode(node, turl);
            }
            return;
        }

        DataRow[] rows = dataInfo.Select("ParentID = '" + node.Data + "'");

        nodeLevel++;

        foreach (DataRow row in rows)
        {
            if (row["ModuleID"].ToString().Trim() == "10590")
            {
                BuildSpecialNode(node, "0");
                continue;
            }

            if (row["ModuleType"].ToString().ToLower() == "p")
                continue;

            LeftMenuNode node2 = new LeftMenuNode();
            node2.NodeName = row["ModuleName"].ToString();// +":" + row["ModuleType"].ToString();
            node2.Data = row["ModuleID"].ToString().Trim();
            node2.Level = nodeLevel;
            node2.NodeIcon = row["ImgPath"].ToString();
            if (node2.NodeIcon != string.Empty)
            {
                node2.NodeName = "";
            }

            switch (nodeLevel)
            {
                case 1:
                    node2.NodeIcon = "Images/Left_Frame/Main_left_file.jpg";
                    break;
                case 2:
                    node2.NodeIcon = "Images/Left_Frame/yuan.jpg";
                    break;
                default:
                    node2.NodeIcon = "Images/Left_Frame/yuan.jpg";
                    break;
            }


            if (row["PropertyType"].ToString() == "link")
            {
                if (row["PropertyValue"].ToString().Trim() != "")
                    node2.NodeUrl = row["PropertyValue"].ToString();
                else
                    node2.NodeUrl = string.Empty;

            }
            else
            {
                node2.NodeUrl = string.Empty;
            }

            node.SubNodes.Add(node2);

            BuildNode(node2, dataInfo);
        }

        nodeLevel--;

    }

    
}
